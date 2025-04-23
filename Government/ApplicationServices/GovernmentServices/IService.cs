using Government.Contracts.Documents;
using Government.Contracts.Fields;
using Government.Contracts.Services;

namespace Government.ApplicationServices.GovernmentServices
{
    public interface IService
    {

        Task<Result<IEnumerable<ServiceResponse>>> GetAllServicesAsync(CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<ServiceResponse>>> GetAllAvailableServicesAsync(string serviceCategory,CancellationToken cancellationToken = default);
        Task<Result<ServiceResponse>> GetServicesByIdAsync(int serviceId ,CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<FieldsResponse>>> GetServiceFieldAsync(int serviceId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<DocumentsResponse>>> GetServiceFilesAsync(int serviceId, CancellationToken cancellationToken);
        Task<Result<ServiceResponse>> AddServiceAsync(ServiceRequest request ,CancellationToken cancellationToken = default);
        Task<Result> UpdateServiceAsync(int serviceId, ServiceRequest request ,CancellationToken cancellationToken = default);
        Task<Result> ToggleServiceAsync(int serviceId, CancellationToken cancellationToken = default);


    }
}
