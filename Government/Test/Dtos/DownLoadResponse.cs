namespace Government.Test.Dtos
{
    public record DownLoadResponse
    (
        byte[] fileContent,
        string contentType, 
        string fileName

        );
}
