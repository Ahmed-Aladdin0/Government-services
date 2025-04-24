using Government.ApplicationServices.UploadFiles;
using Government.Contracts.Documents;
using Government.Contracts.Fields;
using Government.Contracts.Request.Submiting;
using Government.Contracts.Services;
using Government.Entities;
using Government.Errors;
using Mapster;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Government.ApplicationServices.GovernmentServices
{

    public class service(AppDbContext context, IRequiredFileServcie fileServcie, ILogger<service> logger) : IService
    {
        private readonly AppDbContext _context = context;
        private readonly IRequiredFileServcie _fileServcie = fileServcie;

        public async Task<Result<IEnumerable<ServiceResponse>>> GetAllAvailableServicesAsync(ServiceSearch serviceSearch, CancellationToken cancellationToken = default)
        {

            var query = _context.Services
                         .Where(x => x.IsAvailable)
                         .AsNoTracking()
                         .AsQueryable();

            if (!string.IsNullOrWhiteSpace(serviceSearch.ServiceName))
            {
                var search = serviceSearch.ServiceName.Trim();

                query = query.Where(r =>
                    r.ServiceName.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(serviceSearch.serviceCategory))
            {
                query = query.Where(r => 
                    r.category == serviceSearch.serviceCategory);

            }

            var result = query
                         .ProjectToType<ServiceResponse>()
                         .AsNoTracking();

            return Result.Success<IEnumerable<ServiceResponse>>(result);

        }

        public async Task<Result<IEnumerable<ServiceResponse>>> GetAllServicesAsync(CancellationToken cancellationToken = default)
        {
            var services = await _context.Services
                                    .AsNoTracking()
                                    .ToListAsync(cancellationToken);

            var serviceResponse = services.Adapt<IEnumerable<ServiceResponse>>();

            return Result.Success(serviceResponse);
        }

        public async Task<Result<ServiceDetails>> GetServicesByIdAsync(int serviceId, CancellationToken cancellationToken = default)
        {
            var Specificservice = await _context.Services
                      .Include(x => x.RequiredDocuments)
                      .FirstOrDefaultAsync(x => x.Id == serviceId, cancellationToken);

            if (Specificservice is null)
                return Result.Falire<ServiceDetails>(ServiceError.ServiceNotFound);

            var serviceResponse = Specificservice.Adapt<ServiceDetails>();

            return Result.Success(serviceResponse)!;


        }

        public async Task<Result<ServiceResponse>> AddServiceAsync(ServiceRequest request, CancellationToken cancellationToken = default)
        {
            var isDuplicate = await _context.Services
                .AnyAsync(x => x.ServiceName == request.ServiceName || x.ServiceDescription == request.ServiceDescription, cancellationToken);

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
                    category = request.category
                };
                await _context.Services.AddAsync(newService, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var serviceFields = new List<ServiceField>();

                foreach (var fieldRequest in request.ServiceFields)
                {
                    
                    var existingField = await _context.Fields
                        .FirstOrDefaultAsync(f => f.FieldName == fieldRequest.FieldName, cancellationToken);

                    Field fieldEntity;

                    if (existingField is not null)
                    {
                        fieldEntity = existingField;
                    }
                    else
                    {
                        fieldEntity = new Field
                        {
                            FieldName = fieldRequest.FieldName,
                            Description = fieldRequest.Description,
                            HtmlType = fieldRequest.HtmlType
                        };

                        await _context.Fields.AddAsync(fieldEntity, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken); // To get the Id
                    }

                    serviceFields.Add(new ServiceField
                    {
                        ServiceId = newService.Id,
                        FieldId = fieldEntity.Id
                    });
                }

                await _context.ServicesField.AddRangeAsync(serviceFields, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await _fileServcie.UploadManyAsync(request.Files, newService.Id);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var serviceResponse = newService.Adapt<ServiceResponse>();
                return Result.Success(serviceResponse);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                logger.LogError(ex, "Error Within Adding New Service");

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

        public async Task<Result<IEnumerable<FieldsResponse>>> GetServiceFieldAsync(int serviceId, CancellationToken cancellationToken)
        {
            var service = await context.Services.SingleOrDefaultAsync(x => x.Id == serviceId, cancellationToken); // check service id 

            if (service is null)
                return Result.Falire<IEnumerable<FieldsResponse>>(ServiceError.ServiceNotFound);

            var fields = await context.ServicesField
                             .Where(x => x.ServiceId == serviceId)
                             .Select(x => new FieldsResponse(
                                 x.Field.Id,
                                 x.Field.FieldName,
                                 x.Field.Description,
                                 x.Field.HtmlType
                                 //x.Field.HtmlType == "text" || x.Field.HtmlType == "textarea" ? "string" :
                                 //x.Field.HtmlType == "number" ? "int" : // افتراض إن number بيبدأ كـ int، الـ frontend هيحدد float لو فيه كسور
                                 //x.Field.HtmlType == "date" || x.Field.HtmlType == "datetime-local" ? "date" : "unknown"
                                 ))
                             .AsNoTracking()
                             .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<FieldsResponse>>(fields);


        }

        public async Task<Result<IEnumerable<DocumentsResponse>>> GetServiceFilesAsync(int serviceId, CancellationToken cancellationToken)
        {
       
            var service = await context.Services.SingleOrDefaultAsync(x => x.Id == serviceId, cancellationToken); // check service id 

            if (service is null)
                return Result.Falire<IEnumerable<DocumentsResponse>>(ServiceError.ServiceNotFound);

            var Documents = await context.RequiredDocuments
                             .Where(x => x.ServiceId == serviceId)
                             .ProjectToType<DocumentsResponse>()
                             .AsNoTracking()
                             .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<DocumentsResponse>>(Documents);

        }
    }
    
}
