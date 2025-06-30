using CyberGameTime.Bussiness.Dtos.RentalScreen;
using CyberGameTime.Entities.enums;
using CyberGameTime.enums;

namespace CyberGameTime.Bussiness.Dtos.Screen
{
    public class ScreenDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IpAddres { get; set; }
        public string Roku_DeviceId { get; set; }
        public string Roku_udn { get; set; }
        public string Roku_SerialNumber { get; set; }
        public ScreenStatus CurrentStatus { get; set; }
        public ConsoleType ConsoleType { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public virtual RentalScreanDto? CurrentRentalScrean { get; set; }


    }
}
