using Kurama.Application.Commands;
using Kurama.Domain.Common;
using Kurama.Domain.Interfaces.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kurama.Application.Handlers
{
    public class PullImageCommandHandler : IRequestHandler<PullImageCommand, Response<bool>>
    {
        private readonly IDockerFacade _dockerFacade;
        public PullImageCommandHandler(IDockerFacade dockerFacade)
        {
            _dockerFacade = dockerFacade;
        }
        public async Task<Response<bool>> Handle(PullImageCommand request, CancellationToken cancellationToken)
        {
            var pullImageResponse = await _dockerFacade.PullImageAsync(request.ImageName);
            return pullImageResponse;
        }
    }
}
