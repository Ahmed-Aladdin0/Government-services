using Government.ApplicationServices.RequestServices;
using Government.Contracts.Fields;
using Government.Contracts.Request;
using Government.Contracts.Request.Submiting;
using Government.Contracts.Services;
using Government.Errors;
using Government.Test.Dtos;
using Government.Test.Serv;
using Microsoft.AspNetCore.Authorization;
using MimeKit.Tnef;
using System.ComponentModel.DataAnnotations;


namespace Government.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles ="Member")]
    public class RequestsController(IRequestService requestService, AppDbContext context, ITest test) : ControllerBase
    {
        private readonly IRequestService _requestService = requestService;
        private readonly AppDbContext _context = context;
        private readonly ITest test = test;



        [HttpGet("All")]
        [Authorize]
        public async Task<IActionResult> GetUserRequests(CancellationToken cancellationToken)
        {

            var userRequests = await _requestService.GetAllUserRequests(cancellationToken);

            return Ok(userRequests.Value());

        }

        [HttpGet("{requestId}")]
        public async Task<IActionResult> GetRequest([FromRoute] int requestId, CancellationToken cancellationToken)
        {

            var result = await _requestService.GetUserRequestAsync(requestId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }


        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetRequestByStatus([FromQuery] string Status, CancellationToken cancellationToken)
        {

            var result = await _requestService.GetUserequestsByStatusAsync(Status, cancellationToken);

            return Ok(result.Value());
        }

     
        /*sorting Requests EndPoint*/

        [HttpGet("{requestId}/Attached_Fields/")]
        public async Task<IActionResult> GetUserRequestFields([FromRoute] int requestId, CancellationToken cancellationToken)
        {

            var result = await _requestService.GetUserRequestFileds(requestId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }


        [HttpGet("{requestId}/Attached_Files/")]
        public async Task<IActionResult> GetUserRequestFiles([FromRoute] int requestId, CancellationToken cancellationToken)
        {

            var result = await test.DownloadAttachedFilesAsync(requestId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }



        [HttpPut("{id}/Update_Attached_Fields")]
        public async Task<IActionResult> UpdateUserFields([FromRoute] int id, [FromBody] UserFields userFields, CancellationToken cancellationToken)
        {
            var result = await test.UpdateUserFieldsAsync(id, userFields, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }


        [HttpPut("{id}/Update_Attached_Files")]
        public async Task<IActionResult> UpdateUserFiles([FromRoute] int id, [FromForm] UserFiles userFiles, CancellationToken cancellationToken)
        {
            var result = await test.UpdateUserFilesAsync(id, userFiles, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }

        [HttpGet("For_Admin")]

        public async Task<IActionResult> GetAll( [FromQuery] RequestQueryParameters filters, CancellationToken cancellationToken)
        {

            var result = await _requestService.GetAllRequests( filters, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status404NotFound);



        }


    }

}

