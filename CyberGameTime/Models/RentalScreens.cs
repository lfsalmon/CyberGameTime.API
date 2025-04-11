using CyberGameTime.Entities.Common;
using CyberGameTime.enums;
using CyberGameTime.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Entities.Models
{
    public class RentalScreens : IEntity
    {
        public long Id { get; set; }
        public virtual required long ScreenId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
       
        public virtual Screens Screens { get; set; }
    }
}
