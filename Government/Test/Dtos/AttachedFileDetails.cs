namespace Government.Test.Dtos
{
    public record AttachedFileDetails
    (
        int Id,
       string FileName,
       string ContentType,
       string FileExtension
    );
}
