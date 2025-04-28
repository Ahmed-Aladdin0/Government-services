namespace Government.Contracts.FilesAndFileds
{
    public record FileDetails
    (
        int Id,
       string FileName,
       string ContentType,
       string FileExtension
    );
}
