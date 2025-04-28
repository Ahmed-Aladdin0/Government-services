namespace Government.Contracts.Services
{
    public record ServcieDescription
    (
        string ServiceName,
        string ServiceDescription,
        string category,
        decimal Fee,
        string ProcessingTime,
        string ContactInfo
        );
}
