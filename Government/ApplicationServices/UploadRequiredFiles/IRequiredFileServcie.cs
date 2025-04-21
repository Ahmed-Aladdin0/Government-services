namespace Government.ApplicationServices.UploadFiles
{
    public interface IRequiredFileServcie
    {
        Task<int> UploadAsync(IFormFile file,int ServiceId,CancellationToken cancellationToken= default!);

        Task<IEnumerable<int>> UploadManyAsync(List<IFormFile> files, int ServiceId, CancellationToken cancellationToken = default);
    }
}
