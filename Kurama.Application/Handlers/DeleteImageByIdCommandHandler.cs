using Kurama.Application.Commands;
using Kurama.Domain.Common;
using Kurama.Domain.Interfaces.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kurama.Application.Handlers
{
    public class DeleteImageByIdCommandHandler : IRequestHandler<DeleteImageByIdCommand, Response<bool>>
    {
        private readonly IDockerFacade _dockerFacade;

        public DeleteImageByIdCommandHandler(IDockerFacade dockerFacade)
        {
            _dockerFacade = dockerFacade;
        }
        public async Task<Response<bool>> Handle(DeleteImageByIdCommand request, CancellationToken cancellationToken)
        {
            var deleteImageByIdResponse = await _dockerFacade.DeleteImageByIdAsync(request.ImageId);
            return deleteImageByIdResponse;
        }
    }
}
