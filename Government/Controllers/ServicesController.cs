﻿using Government.ApplicationServices.GovernmentServices;
using Government.ApplicationServices.RequestServices;
using Government.Contracts.Services;
using Government.Errors;
using Microsoft.AspNetCore.Authorization;



namespace Government.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicesController(IService service) : ControllerBase
    {
        private readonly IService _service = service;


        [HttpGet("Available")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvailableServices([FromQuery] ServiceSearch serviceSearch, CancellationToken cancellationToken)
        {

            var services = await _service.GetAllAvailableServicesAsync(serviceSearch, cancellationToken);
            return Ok(services.Value());
        }


        [HttpGet("All")]
        public async Task<IActionResult> GetAllServices(CancellationToken cancellationToken)
        {

            var services = await _service.GetAllServicesAsync(cancellationToken);
            return Ok(services.Value());
        }


        [HttpGet("{id}/Details")]
        public async Task<IActionResult> GetServiceDetails([FromRoute] int id, CancellationToken cancellationToken)
        {
            var services = await _service.GetServicesByIdAsync(id, cancellationToken);
            return services.IsSuccess ?
                            Ok(services.Value()) :
                            services.ToProblem(statuscode: StatusCodes.Status404NotFound);
        }


        [HttpPost]
        public async Task<IActionResult> AddService([FromForm] ServiceRequest request, CancellationToken cancellationToken)
        {

            var result = await _service.AddServiceAsync(request, cancellationToken);


            return result.IsSuccess ?
                        Ok(result.Value())
                      : result.ToProblem(statuscode: StatusCodes.Status409Conflict);


        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateServiceDetails([FromRoute] int id, [FromBody] ServcieDescription request, CancellationToken cancellationToken)
        {

            var result = await _service.UpdateServiceDetailsAsync(id, request, cancellationToken);

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
