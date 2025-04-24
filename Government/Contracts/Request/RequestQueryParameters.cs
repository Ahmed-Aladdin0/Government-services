namespace Government.Contracts.Request
{
    public record RequestQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Search { get; set; } // search By RequestId
        public string? RequestStatus { get; set; } // RequestStatus --> Pending / Completed / Rejected
        public string? ResponseStatus { get; set; } // ResponseStatus --> Approve / Reject

        public string SortBy { get; set; } = "RequestDate";
        public string SortDirection { get; set; } = "DESC"; // or "ASC"
        public bool? onlyEditedAfterRejection { get; set; } 
    }
}

