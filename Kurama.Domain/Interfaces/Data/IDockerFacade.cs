using Kurama.Domain.Common;
using Kurama.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kurama.Domain.Interfaces.Data
{
    public interface IDockerFacade
    {
        Task<IEnumerable<ContainerDto>> GetContainersAsync();
        Task<Response<bool>> CreateContainerAsync(CreateContainerDto createContainerDto);
        Task<Response<bool>> DeleteAllContainersAsync();
        Task<Response<bool>> DeleteContainerByIdAsync(string containerId);
    }
}
