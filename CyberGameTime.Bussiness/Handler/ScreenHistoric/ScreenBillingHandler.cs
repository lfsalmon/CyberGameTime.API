using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Bussiness.Commands.ScreenHistoric;
using CyberGameTime.Bussiness.Dtos.ScreenHistory;
using CyberGameTime.Entities.enums;
using CyberGameTime.Entities.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.ScreenHistoric
{
    public class ScreenBillingHandler( IGenericRepository<RentalScreens> _screen)
        : IRequestHandler<ScreenDeviceBillingQuery, IEnumerable<ScreenHistoryBillingDto>>
    {
        public async Task<IEnumerable<ScreenHistoryBillingDto>> Handle(ScreenDeviceBillingQuery request, CancellationToken cancellationToken)
        {
            var from = request.From ?? System.DateTime.MinValue;
            var to = request.To ?? System.DateTime.MaxValue;

            return  _screen.GetByPredicate(x =>
                x.StartDate >= from &&
                x.EndDate <= to,
                false,
                "Screens"
            ).GroupBy(x => x.ScreenId).Select(x => new ScreenHistoryBillingDto()
            {
                ScreenName = x?.FirstOrDefault()?.Screens?.Name ?? string.Empty,
                Hours = Math.Round(x.Sum(x => (x.EndDate - x.StartDate).TotalHours),3),
                Minutes = Math.Round(x.Sum(x => (x.EndDate - x.StartDate).TotalMinutes),3),
                UnitAmmont=request.UnitAmmont
            }).ToList();
        }
    }
}
