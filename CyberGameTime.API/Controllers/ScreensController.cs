using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Bussiness.Helpers.Conectivity;
using CyberGameTime.Bussiness.Helpers.Conectivity.Roku;
using CyberGameTime.Bussiness.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CyberGameTime.API.Controllers;

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
        //return Ok();
        var screen = await _mediator.Send(new GetScreenByIpQuery(IpAddress));
        if (screen is null)
        {
            screen = new Models.Screens()
            {
                IpAddres = IpAddress,
            };
        };
        var rokurequest = ConnectivityConstructor.constructor(screen);
        return Ok(await rokurequest.getInfo());
    }

    [HttpGet("Roku/PowerOff/{IpAddress}")]
    public async Task<ActionResult<DeviceResponseData>> PowerOff(string IpAddress)
    {
        var screen = await _mediator.Send(new GetScreenByIpQuery(IpAddress));
        if (screen is null) return NotFound();
        var rokurequest = ConnectivityConstructor.constructor(screen);
        return Ok(await rokurequest.TurnOff());
    
    }

    [HttpGet("Roku/PowerOn/{IpAddress}")]
    public async Task<ActionResult<DeviceResponseData>> PowerOn(string IpAddress)
    {
        var screen = await _mediator.Send(new GetScreenByIpQuery(IpAddress));
        if (screen is null) return NotFound();
        var rokurequest = ConnectivityConstructor.constructor(screen);
        return Ok(await rokurequest.TurnOn());
    }
}
