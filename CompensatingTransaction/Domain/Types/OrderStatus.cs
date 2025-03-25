namespace Domain.Types
{
    public enum OrderStatus
    {
        Created = 1,
        Failed,
        InPaymentProcess,
        InStockSeparationProcess,
        InDeliveryProcess,
        DeliveryConfirmed,
    }
}
