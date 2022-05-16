using Kurama.Application.Queries;
using Kurama.Domain.Common;
using Kurama.Domain.DTOs;
using Kurama.Domain.Interfaces.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kurama.Application.Handlers
{
    public class GetContainersQueryHandler : IRequestHandler<GetContainersQuery, Response<IEnumerable<ContainerDto>>>
    {
        private readonly IDockerFacade _dockerFacade;
        public GetContainersQueryHandler(IDockerFacade dockerFacade)
        {
            _dockerFacade = dockerFacade;
        }

        public async Task<Response<IEnumerable<ContainerDto>>> Handle(GetContainersQuery request, CancellationToken cancellationToken)
        {
            var containersResponse = await _dockerFacade.GetContainersAsync();
            return Response<IEnumerable<ContainerDto>>.Ok(containersResponse);
        }
    }
}
