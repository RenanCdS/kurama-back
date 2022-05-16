using Kurama.Domain.Common;
using Kurama.Domain.DTOs;
using Kurama.Domain.Interfaces.Data;
using MediatR;
using System.Collections.Generic;

namespace Kurama.Application.Queries
{
    public class GetContainersQuery : IRequest<Response<IEnumerable<ContainerDto>>>
    {
    }
}
