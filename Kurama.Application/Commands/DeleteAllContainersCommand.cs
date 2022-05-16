using Kurama.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurama.Application.Commands
{
    public class DeleteAllContainersCommand : IRequest<Response<bool>>
    {
    }
}
