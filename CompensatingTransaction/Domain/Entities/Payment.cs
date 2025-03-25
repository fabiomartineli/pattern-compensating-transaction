using Domain.Types;
using System;

namespace Domain.Entities
{
    public class Payment : Entity
    {
        public Guid OrderId { get; init; }
        public PaymentStatus Status { get; private set; }

        public Payment() : base()
        {
            Status = PaymentStatus.Processing;
        }

        public void SetStatus(PaymentStatus value) => Status = value;
    }
}
