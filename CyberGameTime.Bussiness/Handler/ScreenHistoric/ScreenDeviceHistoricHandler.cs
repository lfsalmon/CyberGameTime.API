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
    public class ScreenDeviceHistoricHandler(IGenericRepository<DeviceStatusLog> _repository, IGenericRepository<RentalScreens> _screen)
        : IRequestHandler<ScreenDeviceHistoricQuery, IEnumerable<ScreenHistoryLogsDto>>
    {
        public async Task<IEnumerable<ScreenHistoryLogsDto>> Handle(ScreenDeviceHistoricQuery request, CancellationToken cancellationToken)
        {
            var from = request.From ?? System.DateTime.MinValue;
            var to = request.To ?? System.DateTime.MaxValue;

            var logs = _repository.GetByPredicate(x =>
                (!request.ScreenId.HasValue || x.ScreenId == request.ScreenId) &&
                x.CreateAt >= from &&
                x.CreateAt <= to
            ).OrderBy(x => x.CreateAt).ToList();

            var result = new List<ScreenHistoryLogsDto>();
            if (logs.Count == 0) return result;

            DeviceStatusLog lasStauts = null;
            foreach (var log in logs) 
            {
                if(lasStauts is null)             
                    result.Add(new ScreenHistoryLogsDto
                    {
                        ScreenId = log.ScreenId,
                        Status = log.Status,
                        StartDate = log.CreateAt,
                        EndDate = log.UpdateAt ?? log.CreateAt
                    });
                else
                {
                    if(lasStauts.Status == log.Status && ( log.CreateAt - (lasStauts.UpdateAt ?? lasStauts.CreateAt)).TotalMinutes<30)
                    {
                        var last = result.Last();
                        last.EndDate = log.UpdateAt ?? log.CreateAt;
                    }
                    else
                    {
                        result.Add(new ScreenHistoryLogsDto
                        {
                            ScreenId = log.ScreenId,
                            Status = log.Status,
                            StartDate = log.CreateAt,
                            EndDate = log.UpdateAt ?? log.CreateAt
                        });
                    }
                }
                lasStauts=log;

            }

            var _rentalScreensLogs= _screen.GetByPredicate(x =>
                (!request.ScreenId.HasValue || x.ScreenId == request.ScreenId) &&
                x.StartDate >= from &&
                x.EndDate <= to
            ).Select(x=>new ScreenHistoryLogsDto()
            {
                ScreenId = x.ScreenId,
                Status = Status.ScreenRent.ToString(),
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }).ToList();

            result.AddRange(_rentalScreensLogs);
            return result.OrderBy(x=>x.StartDate);
        }
    }
}
    