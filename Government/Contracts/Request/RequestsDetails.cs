namespace Government.Contracts.Request
{
    public record RequestsDetails
    (
        int RequestId,
        DateTime RequestDate,
        string RequestStatus,
        string ResponseStatus,
        bool IsEditedAfterRejection,
        string MemberId,
        string FirstName,
        string LastName,
        int ServiceId,
        string ServiceName

        );
}
