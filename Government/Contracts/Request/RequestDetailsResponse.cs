namespace Government.Contracts.Request
{
    public record RequestDetailsResponse
    (

        int RequestId,
        string UserId,
        string ServiceName,
        DateTime RequestDate,
        string RequestStatus,
        string ResponseStatus,
        string ResponseText
    );  
}

     
   
     
  