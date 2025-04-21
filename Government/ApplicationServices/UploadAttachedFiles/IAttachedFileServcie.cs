namespace Government.ApplicationServices.UploadFiles
{
    public interface IAttachedFileServcie
    {
        Task<int> UploadAttachedAsync(IFormFile file,int RequestId,CancellationToken cancellationToken= default!);

        Task<IEnumerable<int>> UploadManyAttachedAsync(List<IFormFile> files, int RequestId,CancellationToken cancellationToken = default);
    }
}
