using CyberGameTime.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CyberGameTime.Bussiness.Responses
{
    public class DeviceResponseData
    {
        public string Udn { get; set; } = "N/A";
        public string SerialNumber { get; set; } = $" SN-{new Guid().ToString()}";
        public string DeviceId { get; set; } = "N/A";
        public string ModelName { get; set; }
        public string ModelNumber { get; set; } = "1.0000";
        public string SoftwareVersion { get; set; } = "V1.0";
        public Status Status { get; set; }
    }
}
