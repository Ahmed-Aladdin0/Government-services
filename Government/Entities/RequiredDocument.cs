namespace Government.Entities
{
    public class RequiredDocument
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public int ServiceId { get; set; }
        public Service? Service { get; set; }  
    }
}
/*
  public string FileName { get; set; } = string.Empty;  
        public string Description { get; set; } = string.Empty;  
        public string FileUrl { get; set; } = string.Empty;  
        public string DocumentType { get; set; } = string.Empty; 
        public bool IsMandatory { get; set; }  
 
 */