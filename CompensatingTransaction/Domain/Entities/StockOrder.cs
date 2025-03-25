using Domain.Types;
using System;

namespace Domain.Entities
{
    public class StockOrder : Entity
    {
        public Guid OrderId { get; init; }
        public StockStatus Status { get; private set; }

        public StockOrder() : base()
        {
            Status = StockStatus.Processing;
        }

        public void SetStatus(StockStatus value) => Status = value;
    }
}
