using CyberGameTime.Bussiness.Commands.ScreenHistoric;
using CyberGameTime.Bussiness.Dtos.ScreenHistory;
using CyberGameTime.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CyberGameTime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenHistoricController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ScreenHistoricController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceStatusLog>>> Get([FromQuery] long? screenId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _mediator.Send(new ScreenDeviceHistoricQuery
            {
                ScreenId = screenId,
                From = from,
                To = to,
            });
            return Ok(result);
        }

        [HttpGet("BillingReport")]
        public async Task<ActionResult<IEnumerable<ScreenHistoryBillingDto>>> Get([FromQuery] double unitAmmont, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _mediator.Send(new ScreenDeviceBillingQuery
            {
                UnitAmmont = unitAmmont,
                From = from,
                To = to,
            });

            return Ok(result);
        }

        
    }
}
