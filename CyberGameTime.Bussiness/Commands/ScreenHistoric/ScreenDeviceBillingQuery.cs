using CyberGameTime.Bussiness.Dtos.ScreenHistory;
using MediatR;
using System;
using System.Collections.Generic;

namespace CyberGameTime.Bussiness.Commands.ScreenHistoric       
{
    public class ScreenDeviceBillingQuery : IRequest<IEnumerable<ScreenHistoryBillingDto>>
    {
        public double UnitAmmont { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
