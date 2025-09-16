using CyberGameTime.Bussiness.Dtos.ScreenHistory;
using MediatR;
using System;
using System.Collections.Generic;

namespace CyberGameTime.Bussiness.Commands.ScreenHistoric       
{
    public class ScreenDeviceHistoricQuery : IRequest<IEnumerable<ScreenHistoryLogsDto>>
    {
        public long? ScreenId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
