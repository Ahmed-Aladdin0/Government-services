
using Government.ApplicationServices.GovernmentServices;
using Government.ApplicationServices.RequestServices;
using Government.Contracts.Request.Submiting;
using Government.Contracts.Services;
using Government.Errors;
using Government.Test.Serv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace Government.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicesController(IService service, ITest test,  IRequestService requestService ) : ControllerBase
    {
        private readonly IService _service = service;
        private readonly ITest test = test;
        private readonly IRequestService _requestService = requestService;

        //  private readonly AppDbContext context = context;



        /* For Members */
        [HttpGet("Available")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvailableServices([FromQuery] string servicecategory, CancellationToken cancellationToken)
        {

            var services = await _service.GetAllAvailableServicesAsync(servicecategory, cancellationToken);
            return Ok(services.Value());
        }

        [HttpGet("{id}/Details")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceDetails([FromRoute] int id, CancellationToken cancellationToken)
        {
            var services = await _service.GetServicesByIdAsync(id, cancellationToken);
            return services.IsSuccess ?
                            Ok(services.Value()) :
                            services.ToProblem(statuscode: StatusCodes.Status404NotFound);
        }

        [HttpGet("Download_File/{id}")]
        public async Task<IActionResult> DownloadServiceRequiredFile([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await test.DownloadServiceFileAsync(id, cancellationToken);

            return !result.IsSuccess ?
                    result.ToProblem(statuscode: StatusCodes.Status404NotFound) :
                    File(result.Value()!.fileContent, result.Value()!.contentType, result.Value()!.fileName);


        }

        [HttpGet("{serviceId}/Fields")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceFields([FromRoute] int serviceId, CancellationToken cancellationToken)
        {

            var result = await _service.GetServiceFieldAsync(serviceId, cancellationToken);

            return result.IsSuccess ?
                        Ok(result.Value())
                      : result.ToProblem(statuscode: StatusCodes.Status404NotFound);
        }

        [HttpGet("{serviceId}/Files")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceFiles([FromRoute] int serviceId, CancellationToken cancellationToken)
        {

            var result = await _service.GetServiceFilesAsync(serviceId, cancellationToken);

            return result.IsSuccess ?
                        Ok(result.Value())
                      : result.ToProblem(statuscode: StatusCodes.Status404NotFound);
        }


        //Payment EndPoint

        [HttpPost("Submit_Request")]
        public async Task<IActionResult> SubmitServiceRequest([FromForm] SubmitRequestDto requestDto, CancellationToken cancellationToken)
        {
            var result = await _requestService.SubmitRequestAsync(requestDto, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status500InternalServerError);

        }



        /* For Admins */

        [HttpGet]
        [Route("All")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllServices(CancellationToken cancellationToken)
        {

            var services = await _service.GetAllServicesAsync(cancellationToken);
            return Ok(services.Value());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddService([FromForm] ServiceRequest request, CancellationToken cancellationToken)
        {

            var result = await _service.AddServiceAsync(request, cancellationToken);


            return result.IsSuccess ?
                        Ok(result.Value())
                      : result.ToProblem(statuscode: StatusCodes.Status409Conflict);


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService([FromRoute] int id, [FromBody] ServiceRequest request, CancellationToken cancellationToken)
        {

            var result = await _service.UpdateServiceAsync(id, request, cancellationToken);

            if (result.IsSuccess)
                return NoContent();

            return (result.Error.Equals(ServiceError.DuplicatingNameOrDescription))
                     ? result.ToProblem(statuscode: StatusCodes.Status409Conflict)
                     : result.ToProblem(statuscode: StatusCodes.Status404NotFound);
        }

        [HttpPut("{id}/Toggle")]
        public async Task<IActionResult> ToggleService([FromRoute] int id, CancellationToken cancellationToken)

        {

            var result = await _service.ToggleServiceAsync(id, cancellationToken);

            return result.IsSuccess ?
                        NoContent()
                      : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }







    }
}
