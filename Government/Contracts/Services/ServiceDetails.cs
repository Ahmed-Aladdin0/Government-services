using Government.Contracts.Services;
using Government.Test.Dtos;

namespace Government.Contracts.Services
{
    public record ServiceDetails
     (
        int Id,
        string ServiceName,
        string ServiceDescription,
        string category,
        decimal Fee,
        string ProcessingTime,
        string ContactInfo,
        List<FileDetails> RequiredFiles
        );
}

