namespace Government.ApplicationServices.UploadServiceImage
{
    public interface Iserviceimage
    {
        Task<int> UploadAsync(IFormFile file, int ServiceId, CancellationToken cancellationToken = default!);
    }
}
