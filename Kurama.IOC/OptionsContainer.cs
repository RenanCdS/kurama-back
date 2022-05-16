using Kurama.CrossCutting.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurama.IOC
{
    public static class OptionsContainer
    {
        public static IServiceCollection AddOptions(this IServiceCollection services)
        {

            return services;
        }
    }
}
