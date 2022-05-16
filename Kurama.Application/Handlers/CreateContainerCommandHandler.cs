using AutoMapper;
using Kurama.Application.Commands;
using Kurama.Domain.Common;
using Kurama.Domain.DTOs;
using Kurama.Domain.Interfaces.Data;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kurama.Application.Handlers
{
    public class CreateContainerCommandHandler : IRequestHandler<CreateContainerCommand, Response<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IDockerFacade _dockerFacade;
        public CreateContainerCommandHandler(IMapper mapper, IDockerFacade dockerFacade)
        {
            _mapper = mapper;
            _dockerFacade = dockerFacade;
        }
        public async Task<Response<bool>> Handle(CreateContainerCommand request, CancellationToken cancellationToken)
        {
            var createContainerDto = _mapper.Map<CreateContainerCommand, CreateContainerDto>(request);

            var createContainerResponse = await _dockerFacade.CreateContainerAsync(createContainerDto);

            if (!createContainerResponse.IsSuccess)
            {
                return Response<bool>.Fail(createContainerResponse.Messages.FirstOrDefault());
            }

            return Response<bool>.Ok();
        }
    }
}
