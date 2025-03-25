using Domain.Types;
using System;

namespace Domain.Entities
{
    public class Order : Entity
    {
        public string Number { get; init; }
        public string Note { get; private set; }
        public OrderStatus Status { get; private set; }
        public Guid ProductId { get; private set; }
         
        public Order() : base()
        {
            Status = OrderStatus.Created;
        }

        public void SetStatus(OrderStatus status) => Status = status;
        public void SetNote(string note) => Note = note;
    }
}
