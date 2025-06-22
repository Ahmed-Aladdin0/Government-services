namespace Government.Contracts.Payment
{
    public record PaymentResponse(
        int PaymentId,
        decimal Amount,
        DateTime PaymentDate,
        string PaymentStatus
    );
}
