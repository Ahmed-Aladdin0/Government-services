﻿using Government.ApplicationServices.UploadFiles;
using Government.Contracts.Services;

namespace Government.ApplicationServices.GovernmentServices
{
    public interface IService
    {

        Task<Result<IEnumerable<ServiceResponse>>> GetAllServicesAsync(CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<ServiceResponse>>> GetAllAvailableServicesAsync(ServiceSearch serviceSearch,CancellationToken cancellationToken = default);
        Task<Result<ServiceDetails>> GetServicesByIdAsync(int serviceId ,CancellationToken cancellationToken = default);
        Task<Result<ServiceResponse>> AddServiceAsync(ServiceRequest request ,CancellationToken cancellationToken = default);
        Task<Result> UpdateServiceDetailsAsync(int serviceId, ServcieDescription request ,CancellationToken cancellationToken = default);
        Task<Result> ToggleServiceAsync(int serviceId, CancellationToken cancellationToken = default);


    }
}
