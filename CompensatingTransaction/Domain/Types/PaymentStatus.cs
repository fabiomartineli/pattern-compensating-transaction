namespace Domain.Types
{
    public enum PaymentStatus
    {
        Processing = 1,
        Failed,
        Success,
        RefundRequested,
    }
}
