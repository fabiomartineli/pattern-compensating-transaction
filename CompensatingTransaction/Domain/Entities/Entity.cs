using System;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; init; }
        public DateTime UpdatedAt { get; init; }


        public Entity()
        {
            Id = Guid.CreateVersion7();
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
