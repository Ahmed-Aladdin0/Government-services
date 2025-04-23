using Government.ApplicationServices.GovernmentServices;
using Government.ApplicationServices.UploadFiles;
using Government.Entities;
using Government.Errors;
using Government.Test.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Government.Test.Serv
{
    public class TestServ(AppDbContext context , IWebHostEnvironment webHostEnvironment , IRequiredFileServcie requiredFileServcie , IAttachedFileServcie attachedFileServcie) : ITest
    {
        private readonly AppDbContext _context = context;
        private readonly IRequiredFileServcie _requiredFileServcie = requiredFileServcie;
        private readonly IAttachedFileServcie _attachedFileServcie = attachedFileServcie;
        private readonly string _filesPath = $"{webHostEnvironment.WebRootPath}/uploads";

        public async Task<Result<IEnumerable<AttachedFileDetails>>> DownloadAttachedFilesAsync(int RequestId, CancellationToken cancellationToken = default)
        {
            var file = await _context.AttachedDocuments.FindAsync(RequestId);

            if (file is null)
                return Result.Falire<IEnumerable<AttachedFileDetails>>(RequestErrors.RequestNotFound);

            var Attachedfiles = await _context.AttachedDocuments
                             .Where(f => f.RequestId == RequestId)
                             .Select(x=>new AttachedFileDetails(  
                                 x.Id,
                                 x.FileName,
                              //   Path.Combine($"{_filesPath}/RequiredFiles",x.FileName),
                                 x.ContentType,
                                 x.FileExtension
                                 ))
                             .AsNoTracking()
                             .ToListAsync(cancellationToken);


            return Result.Success<IEnumerable<AttachedFileDetails>>(Attachedfiles);
        }

        public async Task<Result<DownLoadResponse>> DownloadServiceFileAsync(int FileId, CancellationToken cancellationToken = default)
        {
            var file = await _context.RequiredDocuments.FindAsync(FileId);

            if (file is null)
                return Result.Falire<DownLoadResponse>(ServiceError.FileNotFound);
           
            var path = Path.Combine($"{_filesPath}/RequiredFiles", file.FileName);

            MemoryStream memoryStream = new();
            using FileStream fileStream = new(path, FileMode.Open);
            fileStream.CopyTo(memoryStream);

            memoryStream.Position = 0;

            var response = new DownLoadResponse(memoryStream.ToArray(), file.ContentType, file.FileName);

            return Result.Success(response);
        }

        public async Task<Result> UpdateFieldsAsync(int ServcieId, FieldsTest fieldsTest, CancellationToken cancellationToken = default)
        {
          
                var service = await _context.Services.FindAsync(ServcieId);
                if (service == null)
                    return Result.Falire(ServiceError.ServiceNotFound);

               
                var existingFields = await _context.Fields.ToListAsync();

                var fieldEntities = new List<Field>();

                foreach (var field in fieldsTest.ServiceFields)
                {
                    var existing = existingFields.FirstOrDefault(f => f.FieldName == field.FieldName);
                    if (existing != null)
                    {
                        fieldEntities.Add(existing);
                    }
                    else
                    {
                   
                        var newField = new Field 
                        {
                            FieldName = field.FieldName,
                            Description = field.Description,
                            HtmlType = field.HtmlType,
                        };
                        _context.Fields.Add(newField);
                        fieldEntities.Add(newField);
                    }
                }

                await _context.SaveChangesAsync(); 

               
                var oldLinks = _context.ServicesField.Where(sf => sf.ServiceId == ServcieId);
                _context.ServicesField.RemoveRange(oldLinks);
                await _context.SaveChangesAsync();

                foreach (var field in fieldEntities)
                {
                    _context.ServicesField.Add(new ServiceField
                    {
                        ServiceId = ServcieId,
                        FieldId = field.Id
                    });
                }

                await _context.SaveChangesAsync();

            return Result.Success();
            }

        public async Task<Result> UpdateFilesAsync(int serviceId, FilesTest filesTest, CancellationToken cancellationToken = default)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
                return Result.Falire(ServiceError.ServiceNotFound);

        
            var existingFilesInDb = await _context.RequiredDocuments
                .Where(f => f.ServiceId == serviceId)
                .ToListAsync();

           
            var filesToDelete = existingFilesInDb
                .Where(f => !filesTest.newFiles.Any(file => file.FileName == f.FileName))
                .ToList();

          
            foreach (var file in filesToDelete)
            {
                var fullPath = Path.Combine($"{_filesPath}/RequiredFiles", file.FileName);
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);

                _context.RequiredDocuments.Remove(file);
            }

            List<IFormFile> files = new List<IFormFile>();
           
            foreach (var newFile in filesTest.newFiles)
            {
              
                var existingFileInDb = existingFilesInDb
                    .FirstOrDefault(f => f.FileName == newFile.FileName);

                if (existingFileInDb == null)
                {
                    files.Add(newFile);
                }
            }

            await _requiredFileServcie.UploadManyAsync(files, serviceId, cancellationToken);

            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateUserFieldsAsync(int RequestId, UserFields userFields, CancellationToken cancellationToken = default)
        {
            var request = await _context.Requests.FindAsync(RequestId,cancellationToken);
            if (request == null)
                return Result.Falire(RequestErrors.RequestNotFound); 

            foreach (var userData in userFields.ServiceData)
            {
                var existingData = await _context.ServicesData
                    .FirstOrDefaultAsync(s => s.RequestId == RequestId && s.FieldId == userData.FieldId);

                if (existingData != null)
                {
                 
                    if (existingData.FieldValueString != userData.FieldValueString)
                        existingData.FieldValueString = userData.FieldValueString;

                    if (existingData.FieldValueInt != userData.FieldValueInt)
                        existingData.FieldValueInt = userData.FieldValueInt;

                    if (existingData.FieldValueFloat != userData.FieldValueFloat)
                        existingData.FieldValueFloat = userData.FieldValueFloat;

                    if (existingData.FieldValueDate != userData.FieldValueDate)
                        existingData.FieldValueDate = userData.FieldValueDate;

                    _context.ServicesData.Update(existingData);
                }
                else
                {
                  
                    var newServiceData = new ServiceData
                    {
                        RequestId = RequestId,
                        FieldId = userData.FieldId,
                        FieldValueString = userData.FieldValueString,
                        FieldValueInt = userData.FieldValueInt,
                        FieldValueFloat = userData.FieldValueFloat,
                        FieldValueDate = userData.FieldValueDate
                    };

                    await _context.ServicesData.AddAsync(newServiceData);
                }
            }

            if (request.ResponseStatus == "Rejected")
                request.IsEditedAfterRejection = true;

            await _context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> UpdateUserFilesAsync(int RequestId, UserFiles userFiles, CancellationToken cancellationToken = default)
        {
            var Request = await _context.Requests.FindAsync(RequestId);
            if (Request == null)
                return Result.Falire(RequestErrors.RequestNotFound);


            var existingFilesInDb = await _context.AttachedDocuments
                .Where(f => f.RequestId == RequestId)
                .ToListAsync();


            var filesToDelete = existingFilesInDb
                .Where(f => !userFiles.Files.Any(file => file.FileName == f.FileName))
                .ToList();


            foreach (var file in filesToDelete)
            {
                var fullPath = Path.Combine($"{_filesPath}/AttachedFiles", file.FileName);
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);

                _context.AttachedDocuments.Remove(file);
            }

            List<IFormFile> files = new List<IFormFile>();

            foreach (var newFile in userFiles.Files)
            {

                var existingFileInDb = existingFilesInDb
                    .FirstOrDefault(f => f.FileName == newFile.FileName);

                if (existingFileInDb == null)
                {
                    files.Add(newFile);
                }
            }

            await _attachedFileServcie.UploadManyAttachedAsync(files, RequestId, cancellationToken);

            if (Request.ResponseStatus == "Rejected")
                Request.IsEditedAfterRejection = true;

            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}

