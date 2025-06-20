namespace Government.Contracts.Services
{
    public record MostRequested
     (
         int Id,
        string ServiceName,
        string ServiceDescription,
        string category,
        decimal Fee,
        string ProcessingTime,
        List<string> RequiredFiles

     );

}
