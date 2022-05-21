using Kurama.Domain.Common;
using MediatR;
using System.Collections.Generic;

namespace Kurama.Application.Commands
{
    public class CreateContainerCommand : IRequest<Response<bool>>
    {
        public string Image { get; set; }
        public IEnumerable<string> EnvironmentVariables { get; set; }
        public int ExternalPort { get; set; }
        public int InternalPort { get; set; }
    }
}
