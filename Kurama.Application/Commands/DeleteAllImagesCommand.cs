using Kurama.Domain.Common;
using MediatR;

namespace Kurama.Application.Commands
{
    public class DeleteAllImagesCommand : IRequest<Response<bool>>
    {
    }
}
