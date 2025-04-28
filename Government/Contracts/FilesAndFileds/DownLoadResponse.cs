namespace Government.Contracts.FilesAndFileds
{
    public record DownLoadResponse
    (
        byte[] fileContent,
        string contentType,
        string fileName

        );
}
