using Kurama.Domain.Common;
using MediatR;

namespace Kurama.Application.Commands
{
    public class PullImageCommand : IRequest<Response<bool>>
    {
        public string ImageName { get; set; }
    }
}
