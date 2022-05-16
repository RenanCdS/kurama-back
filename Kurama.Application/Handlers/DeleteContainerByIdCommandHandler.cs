using Kurama.Application.Commands;
using Kurama.Domain.Common;
using Kurama.Domain.Interfaces.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kurama.Application.Handlers
{
    public class DeleteContainerByIdCommandHandler : IRequestHandler<DeleteContainerByIdCommand, Response<bool>>
    {
        private readonly IDockerFacade _dockerFacade;
        public DeleteContainerByIdCommandHandler(IDockerFacade dockerFacade)
        {
            _dockerFacade = dockerFacade;
        }
        public async Task<Response<bool>> Handle(DeleteContainerByIdCommand request, CancellationToken cancellationToken)
        {
           var deleteContainerByIdResponse = await _dockerFacade.DeleteContainerByIdAsync(request.Id);

            return deleteContainerByIdResponse;
        }
    }
}
