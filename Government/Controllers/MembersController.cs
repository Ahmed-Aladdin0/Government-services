using Government.ApplicationServices.RequestServices;
using Government.Contracts.Request.Submiting;
using Government.Test.Serv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Government.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(IRequestService requestService, AppDbContext context , ITest test) : ControllerBase
    {
        private readonly IRequestService _requestService = requestService;
        private readonly AppDbContext _context = context;
        private readonly ITest test = test;

        [HttpGet("MemberRequests")]
        [Authorize]
        public async Task<IActionResult> GetUserRequests(CancellationToken cancellationToken)
        {

            var userRequests = await _requestService.GetUserRequests(cancellationToken);

            return Ok(userRequests.Value());

        }


        [HttpGet("Request_Entered_Fields/{requestId}")]

        public async Task<IActionResult> GetUserRequestFields([FromRoute] int requestId, CancellationToken cancellationToken)
        {

            var result = await _requestService.GetUserRequestFileds(requestId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }



        [HttpGet("Request_Entered_Files/{requestId}")]

        public async Task<IActionResult> GetUserRequestFiles([FromRoute] int requestId, CancellationToken cancellationToken)
        {

            var result = await test.DownloadAttachedFilesAsync(requestId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }

        [HttpPost("submit")]

        public async Task<IActionResult> SubmitServiceRequest([FromForm] SubmitRequestDto requestDto, CancellationToken cancellationToken)
        {
            var result = await _requestService.SubmitRequestAsync(requestDto, cancellationToken);

            return result.IsSuccess ? Ok(result.Value()) : result.ToProblem(statuscode: StatusCodes.Status500InternalServerError);

        }

    }
}
