﻿using Government.ApplicationServices.AdminServices;
using Government.Contracts.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Government.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminResponseToRequest _adminResponseToRequest;

        public AdminController(IAdminResponseToRequest adminResponseToRequest)
        {
            _adminResponseToRequest = adminResponseToRequest;
        }

        [HttpPost]
        [Route("Response-To-Request")]
  
        public async Task<IActionResult> CreateResponseByAdmin (AdminReply adminReply , CancellationToken cancellationToken)
        {
            var adminResponse = await _adminResponseToRequest.AddAdminResponseAsync(adminReply, cancellationToken);

            if (!adminResponse.IsSuccess)
            {
               // NotFound(adminResponse.Error);
                return adminResponse.ToProblem(statuscode:StatusCodes.Status404NotFound);
            }

            return Ok(adminResponse.Value());

        }
    }
}
