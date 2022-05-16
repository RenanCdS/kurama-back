using Kurama.Domain.Common;
using MediatR;
namespace Kurama.Application.Commands
{
    public class DeleteContainerByIdCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
        public DeleteContainerByIdCommand(string containerId)
        {
            Id = containerId;
        }
    }
}
