using Kurama.Domain.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurama.Data.Facades
{
    public class DockerFacade : IDockerFacade
    {
        public async Task<IEnumerable<string>> GetContainersAsync()
        {
            return null;
        }
    }
}
