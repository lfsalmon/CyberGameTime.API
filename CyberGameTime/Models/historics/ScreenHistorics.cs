using CyberGameTime.Entities.Common;
using CyberGameTime.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Entities.Models.historics
{
    public class ScreenHistorics : IEntity
    {
        public long Id { get; set; }
        public required string IpAddres { get; set; }
        public ScreenStatus CurrentStatus { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }
}
