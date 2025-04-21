namespace Government.Contracts
{
    public record UploadManyFiles
    (

        List<IFormFile> File,
        int serviceid

        );
}
