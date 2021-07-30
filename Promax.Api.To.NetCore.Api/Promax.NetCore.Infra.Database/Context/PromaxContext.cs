using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Promax.NetCore.Domain.Entities.Abstraction;
using Promax.NetCore.Infra.Database.Mappers;
using System;
using System.Data;

namespace Promax.NetCore.Infra.Database.Context
{
    internal class PromaxContext : CommonContext<IEntity>, IPromaxContext
    {
        private int CurrentUser => 1;

        public PromaxContext(DbContextOptions optionsBuilder, IEntityMapperRegister entityMapperRegister) :
            base(optionsBuilder, entityMapperRegister)
        {
        }

        public int GetCurrentUser() => CurrentUser;

        protected override void OnBeforeAddEntry(EntityEntry entityEntry)
        {
            if (!(entityEntry.Entity is IEntity entity))
                return;

            entity.CreatedBy = CurrentUser;
        }

        protected override void OnBeforeEditEntry(EntityEntry entityEntry)
        {
            if (!(entityEntry.Entity is IEntity entity))
                return;

            var resultParameter = new SqlParameter("@result", SqlDbType.DateTimeOffset)
            { Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("SET @result = SYSUTCDATETIME()", resultParameter);

            entity.UpdatedAt = resultParameter.Value as DateTimeOffset?;

        }
    }
}
