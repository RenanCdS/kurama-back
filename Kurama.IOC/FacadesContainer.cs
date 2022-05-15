using Kurama.Data.Facades;
using Kurama.Domain.Interfaces.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
