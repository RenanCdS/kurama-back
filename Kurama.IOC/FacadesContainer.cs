using Kurama.Data.Facades;
using Kurama.Domain.Interfaces.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Kurama.IOC
{
    public static class FacadesContainer
    {
        public static IServiceCollection AddFacades(this IServiceCollection services)
        {
            services.AddSingleton<IDockerFacade, DockerFacade>();
            return services;
        }
    }
}
