using Kurama.Application.Commands;
using Kurama.Domain.Common;
using Kurama.Domain.Interfaces.Data;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kurama.Application.Handlers
{
    public class DeleteAllImagesCommandHandler : IRequestHandler<DeleteAllImagesCommand, Response<bool>>
    {
        private readonly IDockerFacade _dockerFacade;
        public DeleteAllImagesCommandHandler(IDockerFacade dockerFacade)
        {
            _dockerFacade = dockerFacade;
        }
        public async Task<Response<bool>> Handle(DeleteAllImagesCommand request, CancellationToken cancellationToken)
        {
            var deleteAllImagesResponse = await _dockerFacade.DeleteAllImagesAsync();
            return deleteAllImagesResponse;
        }
    }
}
