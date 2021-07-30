using System;

namespace Promax.NetCore.Domain.Entities.Abstraction
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
