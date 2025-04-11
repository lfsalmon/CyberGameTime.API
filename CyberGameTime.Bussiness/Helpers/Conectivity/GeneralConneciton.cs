using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Bussiness.Responses;
using CyberGameTime.Entities.enums;
using CyberGameTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Helpers.Conectivity
{
    public abstract class GeneralConneciton
    {
        protected Screens _Screen { get; set; }
        public GeneralConneciton(Screens _screen)
        {
            _Screen = _screen;
        }

        public abstract Task<bool> TurnOff();
        public abstract Task<bool> TurnOn();
        public abstract Task<Status> GetStatus();
        public abstract Task<DeviceResponseData?> getInfo();
        
    }
}
