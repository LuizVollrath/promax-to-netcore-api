using System;

namespace Promax.NetCore.Domain.Entities.Abstraction
{
    public interface IEntity
    {
        int Id { get; set; }
        int CreatedBy { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        int? UpdatedBy { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }
}
