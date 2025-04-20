using SurvayBasket.Contracts.Authentication;

namespace Government.ApplicationServices.AdminServices

{
    public interface IAdminAuthService
    {
        Task<Result<AdminLoginResponse>> GetAdminTokenAsync(string Email, string Password, CancellationToken cancellationToken = default);
        Task<Result> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken = default);
        Task<Result> ConfirmEmailAsync(ConfirmationEmailRequest ConfirmEmailRequest, CancellationToken cancellationToken = default);
        Task<Result> ResendConfirmEmailAsync(ResendConfirmationEmail ResendConfirmationEmailRequest, CancellationToken cancellationToken = default);


    }
}
