using Kurama.API.Profiles;
using Kurama.Application.Queries;
using Kurama.CrossCutting.Options;
using Kurama.IOC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Kurama.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DockerConfigOptions>(Configuration.GetSection("DockerConfig"));

            services.AddHttpClient();

            services.AddApiVersioning();

            services.AddCors();

            services.AddFacades();
            services.AddMediatR(typeof(GetContainersQuery).Assembly);
            services.AddAutoMapper(typeof(ContainersProfile).Assembly);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kurama.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kurama.API v1"));
            }

            app.UseCors(options => options.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
            app.UseRouting();
            app.UseApiVersioning();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
