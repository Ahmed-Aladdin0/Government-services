using Government.ApplicationServices.RequestServices;
using Government.Contracts.Fields;
using Government.Contracts.Request.Submiting;
using Government.Contracts.Services;
using Government.Errors;
using Microsoft.AspNetCore.Authorization;
using MimeKit.Tnef;
using System.ComponentModel.DataAnnotations;


namespace Government.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles ="Member")]
    public class RequestsController(IRequestService requestService, AppDbContext context, IWebHostEnvironment environment) : ControllerBase
    {
        private readonly IRequestService _requestService = requestService;
        private readonly AppDbContext _context = context;
        private readonly IWebHostEnvironment env = environment;




        //[HttpGet("{requestId}")]

        //public async Task<IActionResult> GetRequest([FromRoute] int requestId, CancellationToken cancellationToken)
        //{

        //    var result = await _requestService.GetRequestAsync(requestId, cancellationToken);

        //    return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        //}




        //[HttpGet("")]

        //public async Task<IActionResult> GetRequestByStatus([FromQuery] string requestStatus, CancellationToken cancellationToken)
        //{

        //    var result = await _requestService.GetRequestByStatusAsync(requestStatus, cancellationToken);

        //    return Ok(result.Value());
        //}


        /*
        [HttpGet]
        [Route("{serviceId}/documents")]
        [AllowAnonymous]

        public async Task<IActionResult> GetDocuments([FromRoute] int serviceId, CancellationToken cancellationToken)
        {

            var result = await fieldService.GetDocumentsAsync(serviceId, cancellationToken);

            return result.IsSuccess ?
                        Ok(result.Value())
                      : result.ToProblem(statuscode: StatusCodes.Status404NotFound);
            //}

        */
  

        //[HttpPut("Update/{id}")]

        //public async Task<IActionResult> UpdateService([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] IEnumerable<UpdateRequest> requestDto, CancellationToken cancellationToken)
        //{

        //    var result = await _requestService.UpdateRequestAsync(id, requestDto, cancellationToken);

        //    return (result.IsSuccess) ?
        //         NoContent() : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        //}


    }

}

