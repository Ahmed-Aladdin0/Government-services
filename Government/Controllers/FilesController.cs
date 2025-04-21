using Government.ApplicationServices.UploadFiles;
using Government.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurvayBasket.ApplicationServices.UserAccount;
using SurvayBasket.Contracts.AccountProfile.cs;

namespace Government.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController(IRequiredFileServcie fileServcie) : ControllerBase
    {
        private readonly IRequiredFileServcie fileServcie = fileServcie;

        [HttpPost("Upload")]
      
        public async Task<ActionResult> Upload([FromForm] UploadFile request)
        {

            var userInfo = await fileServcie.UploadAsync(request.File,request.serviceid);

            return Created();


        }
        [HttpPost("UploadMany")]

        public async Task<ActionResult> UploadMany([FromForm] UploadManyFiles request)
        {

            var userInfo = await fileServcie.UploadManyAsync(request.File, request.serviceid);

            return Created();


        }
    }
}
