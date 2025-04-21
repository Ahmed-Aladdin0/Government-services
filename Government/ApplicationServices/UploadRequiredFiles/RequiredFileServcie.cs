
using Government.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Government.ApplicationServices.UploadFiles
{
    public class RequiredFileServcie(IWebHostEnvironment webHostEnvironment, AppDbContext context) : IRequiredFileServcie
    {
        private readonly AppDbContext _context = context;
        private readonly string _filesPath = $"{webHostEnvironment.WebRootPath}/uploads/RequiredFiles";
        public async Task<int> UploadAsync(IFormFile file, int ServiceId, CancellationToken cancellationToken = default!)
        {
            var uploadedFile = await SaveRequiredFiles(file, ServiceId, cancellationToken);
            await _context.AddAsync(uploadedFile, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return uploadedFile.Id;
        }
        public async Task<IEnumerable<int>> UploadManyAsync(List<IFormFile> files, int ServiceId, CancellationToken cancellationToken = default)
        {
            List<RequiredDocument> uploadedFiles = [];

            foreach (var file in files)
            {
                var uploadedFile = await SaveRequiredFiles(file, ServiceId, cancellationToken);
                uploadedFiles.Add(uploadedFile);
            }

            await _context.AddRangeAsync(uploadedFiles, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return uploadedFiles.Select(x => x.Id).ToList();
        }


        private async Task<RequiredDocument> SaveRequiredFiles(IFormFile file, int ServiceId, CancellationToken cancellationToken = default)
        {
           // var randomFileName = Path.GetRandomFileName();

            var uploadedFile = new RequiredDocument
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileExtension = Path.GetExtension(file.FileName),
                ServiceId= ServiceId
            };

            var path = Path.Combine(_filesPath, file.FileName);

            using var stream = File.Create(path);
            await file.CopyToAsync(stream, cancellationToken);

            return uploadedFile;
        }
    }
}
