using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberGameTime.Bussiness.Responses;
using CyberGameTime.Entities.enums;
using CyberGameTime.Models;
using static Microsoft.Graph.Constants;

namespace CyberGameTime.Bussiness.Helpers.Conectivity.IFTTT
{
    internal class WebHooksIFTTT : GeneralConneciton
    {
        public WebHooksIFTTT(Screens _screen) : base(_screen)
        {
        }

        public override async Task<DeviceResponseData?> getInfo()
        {
            throw new NotImplementedException();
        }

        public override async Task<Status> GetStatus()
        {
            return Status.PowerOn;
        }

        public override async Task<Status> TurnOff()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(_Screen.URL_IFTTT_Off, null);
            if (response.IsSuccessStatusCode) return Status.PowerOff;
            return Status.Undefined;
        }

        public override async Task<Status> TurnOn()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(_Screen.URL_IFTTT_On, null);
            if (response.IsSuccessStatusCode) return Status.PowerOff;
            return Status.Undefined;
        }
    }
}
