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
    public class ImagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ImagesController(IMediator mediator)
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
            var getAllImagesQuery = new GetAllImagesQuery();
            var getAllImagesQueryResponse = await _mediator.Send(getAllImagesQuery);

            if (!getAllImagesQueryResponse.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(getAllImagesQueryResponse.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> PullImage(PullImageCommand pullImageCommand)
        {
            var pullImageResponse = await _mediator.Send(pullImageCommand);

            if (!pullImageResponse.IsSuccess)
            {
                return BadRequest(pullImageResponse.Messages);
            }
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAll()
        {
            var deleteAllUnusedImagesCommand = new DeleteAllImagesCommand();
            var deleteAllUnusedImagesCommandResponse = await _mediator.Send(deleteAllUnusedImagesCommand);

            if (!deleteAllUnusedImagesCommandResponse.IsSuccess)
            {
                return BadRequest(deleteAllUnusedImagesCommandResponse.Messages);
            }
            return Ok();
        }

        [HttpDelete("{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteById(string imageId)
        {
            var deleteImageByIdCommand = new DeleteImageByIdCommand(imageId);
            var deleteImageByIdCommandResponse = await _mediator.Send(deleteImageByIdCommand);

            if (!deleteImageByIdCommandResponse.IsSuccess)
            {
                return BadRequest(deleteImageByIdCommandResponse.Messages);
            }
            return Ok();
        }
    }
}
