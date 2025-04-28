using Government.ApplicationServices.UploadFiles;
using Government.Contracts.FilesAndFileds;
using Government.Errors;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Government.ApplicationServices.Files
{
    public class FileService(AppDbContext context, IWebHostEnvironment webHostEnvironment, IRequiredFileServcie requiredFileServcie, IAttachedFileServcie attachedFileServcie) : IFileService
    {
        private readonly IRequiredFileServcie _requiredFileServcie = requiredFileServcie;
        private readonly IAttachedFileServcie _attachedFileServcie = attachedFileServcie;
        private readonly string _filesPath = $"{webHostEnvironment.WebRootPath}/uploads";
        private readonly AppDbContext _context = context;

        public async Task<Result<IEnumerable<FileDetails>>> GetAttachedFilesAsync(int RequestId, CancellationToken cancellationToken = default)
        {
            var file = await _context.Requests.FindAsync(RequestId);

            if (file is null)
                return Result.Falire<IEnumerable<FileDetails>>(RequestErrors.RequestNotFound);

            var Attachedfiles = await _context.AttachedDocuments
                             .Where(f => f.RequestId == RequestId)
                             .Select(x => new FileDetails(
                                 x.Id,
                                 x.FileName,
                                 //   Path.Combine($"{_filesPath}/RequiredFiles",x.FileName),
                                 x.ContentType,
                                 x.FileExtension
                                 ))
                             .AsNoTracking()
                             .ToListAsync(cancellationToken);


            return Result.Success<IEnumerable<FileDetails>>(Attachedfiles);
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

            if (Request.RequestStatus == "Rejected")
                Request.IsEditedAfterRejection = true;

            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<IEnumerable<DocumentsResponse>>> GetServiceFilesAsync(int serviceId, CancellationToken cancellationToken)
        {

            var service = await context.Services.SingleOrDefaultAsync(x => x.Id == serviceId, cancellationToken); // check service id 

            if (service is null)
                return Result.Falire<IEnumerable<DocumentsResponse>>(ServiceError.ServiceNotFound);

            var Documents = await context.RequiredDocuments
                             .Where(x => x.ServiceId == serviceId)
                             .ProjectToType<DocumentsResponse>()
                             .AsNoTracking()
                             .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<DocumentsResponse>>(Documents);

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

        public async Task<Result<DownLoadResponse>> DownloadAttachedFileAsync(int FileId, CancellationToken cancellationToken = default)
        {

            var file = await _context.AttachedDocuments.FindAsync(FileId);

            if (file is null)
                return Result.Falire<DownLoadResponse>(ServiceError.FileNotFound);

            var path = Path.Combine($"{_filesPath}/AttachedFiles", file.FileName);

            MemoryStream memoryStream = new();
            using FileStream fileStream = new(path, FileMode.Open);
            fileStream.CopyTo(memoryStream);

            memoryStream.Position = 0;

            var response = new DownLoadResponse(memoryStream.ToArray(), file.ContentType, file.FileName);

            return Result.Success(response);
        }


    }
}
