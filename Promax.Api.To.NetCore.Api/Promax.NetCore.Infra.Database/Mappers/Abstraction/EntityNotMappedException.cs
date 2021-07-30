using System;

namespace Promax.NetCore.Infra.Database.Mappers
{
    [Serializable]
    public class EntityNotMappedException : Exception
    {
        public EntityNotMappedException(string entityName)
            : base($"Entity \"{entityName}\" not mapped for Entity Framework")
        {
        }
    }
}
