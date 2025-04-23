namespace Government.Entities
{
    public class Request
    {

        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestStatus { get; set; } 
        public string ResponseStatus { get; set; }
        public bool IsEditedAfterRejection { get; set; } = false;

        public string MemberId { get; set; } = string.Empty;
        public int ServiceId { get; set; }

        public Member Member { get; set; } = default!;
        public Service service { get; set; } = default!;
        public ICollection<AdminResponse> AdminResponse { get; set; } = [];
        public ICollection<ServiceData> serviceData { get; set; } = [];
        public ICollection<AttachedDocument> AttachedDocuments { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];
   
     

    }
}
