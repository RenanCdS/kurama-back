using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurama.Domain.Interfaces.Data
{
    public interface IDockerFacade
    {
        Task<IEnumerable<string>> GetContainersAsync()
    }
}
