using Government.ApplicationServices.UploadFiles;
using Government.Contracts.Request.Submiting;
using Government.Contracts.Services;
using Government.Entities;
using Government.Errors;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Government.ApplicationServices.GovernmentServices
{

    public class service(AppDbContext context, IRequiredFileServcie  fileServcie , ILogger<service> logger) : IService
    {
        private readonly AppDbContext _context = context;
        private readonly IRequiredFileServcie _fileServcie = fileServcie;

        public async Task<Result<IEnumerable<ServiceResponse>>> GetAllAvailableServicesAsync(string serviceCategory, CancellationToken cancellationToken = default)
        {
            if (serviceCategory == "All")
            {
                var services = await _context.Services
                             .Where(x => x.IsAvailable)
                             .AsNoTracking()
                             .ToListAsync(cancellationToken);

                var serviceResponse = services.Adapt<IEnumerable<ServiceResponse>>();
                return Result.Success(serviceResponse);

            }
            else
            {

                var services = await _context.Services
                                        .Where(x => x.IsAvailable && x.category == serviceCategory)
                                        .AsNoTracking()
                                        .ToListAsync(cancellationToken);

                var serviceResponse = services.Adapt<IEnumerable<ServiceResponse>>();
                return Result.Success(serviceResponse);
            }


        }

        public async Task<Result<IEnumerable<ServiceResponse>>> GetAllServicesAsync(CancellationToken cancellationToken = default)
        {
            var services = await _context.Services
                                    .AsNoTracking()
                                    .ToListAsync(cancellationToken);

            var serviceResponse = services.Adapt<IEnumerable<ServiceResponse>>();

            return Result.Success(serviceResponse);
        }

        public async Task<Result<ServiceResponse>> GetServicesByIdAsync(int serviceId, CancellationToken cancellationToken = default)
        {
            var Specificservice = await _context.Services
                                .Where(x => x.IsAvailable && x.Id == serviceId)
                                .AsNoTracking()
                                .SingleOrDefaultAsync(cancellationToken);

            if (Specificservice is null)
                return Result.Falire<ServiceResponse>(ServiceError.ServiceNotFound);


            var serviceResponse = Specificservice.Adapt<ServiceResponse>();

            return Result.Success(serviceResponse);


        }

        public async Task<Result<ServiceResponse>> AddServiceAsync(ServiceRequest request, CancellationToken cancellationToken = default)
        {
            var isDuplicate = await _context.Services
                                 .AnyAsync(x => (x.ServiceName == request.ServiceName || x.ServiceDescription == request.ServiceDescription), cancellationToken);

            if (isDuplicate)
                return Result.Falire<ServiceResponse>(ServiceError.DuplicatingNameOrDescription);


            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {

                var newService = new Service
            {
                ServiceName = request.ServiceName,
                ServiceDescription = request.ServiceDescription,
                Fee = request.Fee,
                ProcessingTime = request.ProcessingTime,
                ContactInfo = request.ContactInfo,
            };
            await _context.Services.AddAsync(newService);
            await _context.SaveChangesAsync(cancellationToken);



            var Fields =  request.ServiceFields.Select(x => new Field
            {
                FieldName= x.FieldName,
                Description= x.Description,
                HtmlType= x.HtmlType,
            }).ToList();
            await _context.Fields.AddRangeAsync(Fields);
            await _context.SaveChangesAsync(cancellationToken);



            var serviceFields = Fields.Select(x => new ServiceField
            {
                ServiceId= newService.Id,
                FieldId = x.Id
            }).ToList();
            await _context.ServicesField.AddRangeAsync(serviceFields);
            await _context.SaveChangesAsync(cancellationToken);



            await  _fileServcie.UploadManyAsync(request.Files, newService.Id);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            var ServiceResponse = newService.Adapt<ServiceResponse>();
            return Result.Success(ServiceResponse);
            }

            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                logger.LogError(ex, "Error Within Adding New Service ");

                return Result.Falire<ServiceResponse>(RequestErrors.RequestNotCompleted);
            }

        }

        public async Task<Result> ToggleServiceAsync(int serviceId, CancellationToken cancellationToken = default)
        {
            var service = await _context.Services
                                 .SingleOrDefaultAsync(x => x.Id == serviceId, cancellationToken);

            if (service is null)
                return Result.Falire(ServiceError.ServiceNotFound);


            service.IsAvailable = !(service.IsAvailable);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }

        public async Task<Result> UpdateServiceAsync(int serviceId, ServiceRequest request, CancellationToken cancellationToken = default)
        {
            var service = await _context.Services.SingleOrDefaultAsync(x => x.Id == serviceId, cancellationToken); // check service id 

            if (service is null)
                return Result.Falire(ServiceError.ServiceNotFound);

            var isDuplicate = await _context.Services
                                 .AnyAsync(x => (x.ServiceName == request.ServiceName || x.ServiceDescription == request.ServiceDescription)
                                        && x.Id != serviceId, cancellationToken);

            if (isDuplicate)
                return Result.Falire(ServiceError.DuplicatingNameOrDescription);

            request.Adapt(service);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();


        }


    }
}
