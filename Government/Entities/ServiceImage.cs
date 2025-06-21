namespace Government.Entities
{
    public class ServiceImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string ImageExtension { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public int ServiceId { get; set; }
        public Service Service { get; set; } = default!;
    }
}
