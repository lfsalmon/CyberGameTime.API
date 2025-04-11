using CyberGameTime.Bussiness.Commands.RentalScreen;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.RentalScreen;
using CyberGameTime.Bussiness.Dtos.Screen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberGameTime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalScreendController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalScreendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<RentalScreanDto>> CreateProduct([FromBody] AddRentalScreenRequest _command)
        {
            var screen = await _mediator.Send(_command);
            if (screen != null)
                return Ok(screen);
            else return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<RentalScreanDto>> UpdateRental([FromBody] UpdateRentalScreenCommand _command)
        {
            var screen = await _mediator.Send(_command);
            if (screen != null)
                return Ok(screen);
            else return BadRequest();
        }

    }
}
