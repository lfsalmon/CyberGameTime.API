using CyberGameTime.Entities.Common;
using CyberGameTime.Entities.enums;
using CyberGameTime.Entities.Models;
using CyberGameTime.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Models
{
    public class Screens:IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }    
        public string IpAddres { get; set; }
        public string Roku_DeviceId { get; set; }
        public string Roku_udn { get; set; }
        public string Roku_SerialNumber { get; set; }

        public string URL_IFTTT_On { get; set; }
        public string URL_IFTTT_Off { get; set; }
        public ScreenStatus CurrentStatus { get; set; }
        public ConsoleType ConsoleType { get; set; }
        public ConnectionType ConnectionType { get; set; }

        public DateTime CreateAt { get ; set ; }
        public DateTime? UpdateAt { get ; set ; }
        public virtual IEnumerable<RentalScreens> RentalScreens { get; set; }
    }
}
