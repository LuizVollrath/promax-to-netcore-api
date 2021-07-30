using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Promax.Api.To.NetCore.Api.Filters;
using Promax.NetCore.Application.DependencyInjection;
using Promax.NetCore.Infra.Repositories.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Diagnostics.CodeAnalysis;

namespace Promax.Api.To.NetCore.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; });

            services.AddControllers(options => {
                options.Filters.Add(typeof(ExceptionFilter));
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddResponseCompression(options => { options.EnableForHttps = true; });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Promax NetCore Api Example"
                });
            });

            services.AddSqlServerPersistence();
            services.AddPromaxApplicationServices();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Content-Disposition"));

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseResponseCompression();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc");
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Promax NetCore Api Example");
                c.EnableFilter();
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
