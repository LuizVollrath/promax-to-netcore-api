using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Promax.NetCore.Infra.Database.Mappers.Abstraction
{
    [ExcludeFromCodeCoverage]
    internal static class PropertyBuilderExtensions
    {
        public static PropertyBuilder StringDefaultMaxLength(this PropertyBuilder builder)
        {
            builder.HasMaxLength(255);
            return builder;
        }

        public static PropertyBuilder DecimalDefaultType(this PropertyBuilder builder)
        {
            builder.HasColumnType("decimal(18,2)");
            return builder;
        }
    }
}
