using Government.Contracts.Payment;

namespace Government.ApplicationServices.PaymentService
{
    public interface IPaymentService
    { 
        Task<Result<PaymentResponse>> MakeTransaction(int RequestId, decimal ServcieCost , CancellationToken cancellationToken = default!);
    }
}
