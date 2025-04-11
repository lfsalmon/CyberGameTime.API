using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Bussiness.Helpers.Conectivity.Roku;
using CyberGameTime.Bussiness.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CyberGameTime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreensController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScreensController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScreenDto>>> GetAll()
        {
            var screen = await _mediator.Send(new GetScreenListQuery());
            var result = (screen is null || screen.Count() == 0) ? new List<ScreenDto>() : screen;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScreenDto>> Get(int id)
        {
            var screen = await _mediator.Send(new GetScreenByIdQuery(id));
            return Ok(screen);
        }
        [HttpPost]
        public async Task<ActionResult<ScreenDto>> CreateProduct([FromBody] AddScreanQuery command)
        {
            var screen = await _mediator.Send(command);
            return Ok(screen);
        }

        [HttpPut]
        public async Task<ActionResult<ScreenDto>> Put([FromBody] UpdateScreenRequest command)
        {
            try
            {
                var screen = await _mediator.Send(command);
                if(screen is null) return NotFound();
                return Ok(screen);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("RokuInfo/{IpAddress}")]
        public async Task<ActionResult<DeviceResponseData>> GetRokuInfo(string IpAddress)
        {
            return Ok();
            //return Ok(await RokuRequest.getInfo(IpAddress));
        }

        [HttpGet("Roku/PowerOff/{IpAddress}")]
        public async Task<ActionResult<DeviceResponseData>> PowerOff(string IpAddress)
        {
            return Ok();
           // return Ok(await RokuRequest.PowerOff(IpAddress));
        }

        [HttpGet("Roku/PowerOn/{IpAddress}")]
        public async Task<ActionResult<DeviceResponseData>> PowerOn(string IpAddress)
        {
            return Ok();
            //return Ok(await RokuRequest.PowerOn(IpAddress));
        }
    }
}
