using Kurama.Application.Queries;
using Kurama.Domain.Common;
using Kurama.Domain.DTOs;
using Kurama.Domain.Interfaces.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kurama.Application.Handlers
{
    public class GetAllImagesQueryHandler : IRequestHandler<GetAllImagesQuery, Response<IEnumerable<ImageDto>>>
    {
        private readonly IDockerFacade _dockerFacade;
        public GetAllImagesQueryHandler(IDockerFacade dockerFacade)
        {
            _dockerFacade = dockerFacade;
        }
        public async Task<Response<IEnumerable<ImageDto>>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            var getAllImagesResponse = await _dockerFacade.GetAllImagesAsync();
            return getAllImagesResponse;
        }
    }
}
