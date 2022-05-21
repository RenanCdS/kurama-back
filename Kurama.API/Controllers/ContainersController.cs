using Kurama.Application.Commands;
using Kurama.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kurama.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class ContainersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContainersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var getContainersQuery = new GetContainersQuery();
            var getContainersResponse = await _mediator.Send(getContainersQuery);

            if (!getContainersResponse.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(getContainersResponse.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> Create(CreateContainerCommand createContainerCommand)
        {
            var createContainerCommandResponse = await _mediator.Send(createContainerCommand);

            if (!createContainerCommandResponse.IsSuccess)
            {
                return BadRequest();
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAll()
        {
            var deleteAllContainersCommand = new DeleteAllContainersCommand();
            var deleteAllContainersResponse = await _mediator.Send(deleteAllContainersCommand);

            if (!deleteAllContainersResponse.IsSuccess)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{containerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteById(string containerId)
        {
            var deleteContainerByIdCommand = new DeleteContainerByIdCommand(containerId);
            var deleteContainerByIdResponse = await _mediator.Send(deleteContainerByIdCommand);

            if (!deleteContainerByIdResponse.IsSuccess)
            {
                return BadRequest(deleteContainerByIdResponse.Messages);
            }
            return Ok();
        }
    }
}
