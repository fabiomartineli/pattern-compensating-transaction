using System;

namespace Domain.Entities
{
    public class Stock : Entity
    {
        public Guid ProductId { get; init; }
        public int Count { get; private set; }

        public Stock() : base()
        {
        }

        public void Decrease() => --Count;
        public void Increase() => ++Count;
    }
}
