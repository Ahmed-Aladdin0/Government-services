using Government.Abstractions;
using Government.ApplicationServices.AdminServices;

using Government.Authentication;
using SurvayBasket.Contracts.Authentication;
using SurvayBasket.UsreErrors;


namespace Government.Controllers

{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase

    {
      
        private readonly IAdminAuthService _adminauthService;

        public AuthController( IAdminAuthService AdminauthService)
        {
           
            _adminauthService = AdminauthService;
        }

        [HttpPost("AdminLogin")]

        public async Task<IActionResult> AdminLoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var authResult = await _adminauthService.GetAdminTokenAsync(loginRequest.Email, loginRequest.Password, cancellationToken);

            return (!authResult.IsSuccess) ? authResult.ToProblem(statuscode: StatusCodes.Status400BadRequest) : Ok(authResult.Value());


        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest Request, CancellationToken cancellationToken)
        {

            var authresult = await _adminauthService.RegisterAsync(Request, cancellationToken);

            if (authresult.IsSuccess)
                return Ok("Registration completed successfully , Please confirm your Email ");

            if (authresult.Error.Equals(UsersErrors.DublicatedEmail))
                return authresult.ToProblem(statuscode: StatusCodes.Status409Conflict);

            else
                return authresult.ToProblem(statuscode: StatusCodes.Status400BadRequest);

        }


        [HttpPost("Confirm-Email")]
        public async Task<IActionResult> ConfirmEmailAsync(ConfirmationEmailRequest Request, CancellationToken cancellationToken)
        {

            var authresult = await _adminauthService.ConfirmEmailAsync(Request, cancellationToken);

            if (authresult.IsSuccess)
                return Ok("Confirmation completed successfully , You can Login now ");

            if (authresult.Error.Equals(UsersErrors.InvalidCode))
                return authresult.ToProblem(statuscode: StatusCodes.Status401Unauthorized);

            else
                return authresult.ToProblem(statuscode: StatusCodes.Status400BadRequest);

        }


        [HttpPost("Resend-Confirm-Email")]
        public async Task<IActionResult> ResendConfirmEmailAsync(ResendConfirmationEmail Request, CancellationToken cancellationToken)
        {

            var authresult = await _adminauthService.ResendConfirmEmailAsync(Request, cancellationToken);

            return (authresult.IsSuccess) ? Ok("Email is Resent again") : authresult.ToProblem(statuscode: StatusCodes.Status400BadRequest);
        }


    }
}

