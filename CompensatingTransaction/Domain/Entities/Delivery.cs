using Domain.Types;
using System;

namespace Domain.Entities
{
    public class Delivery : Entity
    {
        public Guid OrderId { get; init; }
        public DeliveryStatus Status { get; private set; }

        public Delivery() : base()
        {
            Status = DeliveryStatus.InRouteProcess;
        }

        public void SetStatus(DeliveryStatus status) => Status = status;
    }
}
