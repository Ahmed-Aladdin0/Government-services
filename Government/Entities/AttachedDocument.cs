namespace Government.Entities
{
    public class AttachedDocument
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;  
  
        public int RequestId { get; set; }
        public Request Request { get; set; } = default!;
    }
}
