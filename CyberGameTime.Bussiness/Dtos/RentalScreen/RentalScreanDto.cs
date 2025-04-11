using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Dtos.RentalScreen
{
    public class RentalScreanDto
    {
        public long Id { get; set; }
        public long ScreenId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
