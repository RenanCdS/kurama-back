using Kurama.Application.Commands;
using Kurama.Domain.Common;
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
    public class DeleteAllContainersCommandHandler : IRequestHandler<DeleteAllContainersCommand, Response<bool>>
    {
        private readonly IDockerFacade _dockerFacade;
        public DeleteAllContainersCommandHandler(IDockerFacade dockerFacade)
        {
            _dockerFacade = dockerFacade;
        }
        public async Task<Response<bool>> Handle(DeleteAllContainersCommand request, CancellationToken cancellationToken)
        {
            var deleteAllContainersResponse = await _dockerFacade.DeleteAllContainersAsync();

            return deleteAllContainersResponse;
        }
    }
}
