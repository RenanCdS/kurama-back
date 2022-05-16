using Kurama.Domain.Common;
using Kurama.Domain.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Kurama.Application.Queries
{
    public class GetAllImagesQuery : IRequest<Response<IEnumerable<ImageDto>>>
    {
    }
}
