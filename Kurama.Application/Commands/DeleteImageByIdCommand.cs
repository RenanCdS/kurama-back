using Kurama.Domain.Common;
using MediatR;

namespace Kurama.Application.Commands
{
    public class DeleteImageByIdCommand : IRequest<Response<bool>>
    {
        public string ImageId { get; set; }
        public DeleteImageByIdCommand(string imageId)
        {
            ImageId = imageId;
        }
    }
}
