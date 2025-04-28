using Government.ApplicationServices.RequestServices;
using Government.Contracts.Request;
using Government.Contracts.Request.Submiting;
using Microsoft.AspNetCore.Authorization;

namespace Government.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestsController(IRequestService requestService, AppDbContext context) : ControllerBase
    {
        private readonly IRequestService _requestService = requestService;


        // Get All - Search - Filter - Sorting -onlyEditedAfterRejection
        [HttpGet("All")]
        public async Task<IActionResult> GetAllRequests([FromQuery] RequestQueryParameters filters, CancellationToken cancellationToken)
        {

            var result = await _requestService.GetAllRequests(filters, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status404NotFound);



        }

        [HttpGet("Member")]
        public async Task<IActionResult> GetUserRequests(CancellationToken cancellationToken)
        {

            var userRequests = await _requestService.GetAllUserRequests(cancellationToken);

            return Ok(userRequests.Value());

        }

        [HttpGet("{requestId}")]
        public async Task<IActionResult> GetRequestById([FromRoute] int requestId, CancellationToken cancellationToken)
        {

            var result = await _requestService.GetUserRequestAsync(requestId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }


        [HttpGet("")]
        public async Task<IActionResult> GetUserRequestByStatus([FromQuery] string Status, CancellationToken cancellationToken)
        {

            var result = await _requestService.GetUserequestsByStatusAsync(Status, cancellationToken);

            return Ok(result.Value());
        }


        [HttpPost("Submit")]
        public async Task<IActionResult> AddRequest([FromForm] SubmitRequestDto requestDto, CancellationToken cancellationToken)
        {
            var result = await _requestService.SubmitRequestAsync(requestDto, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status500InternalServerError);

        }

        /*sorting Requests EndPoint*/







    }

}

