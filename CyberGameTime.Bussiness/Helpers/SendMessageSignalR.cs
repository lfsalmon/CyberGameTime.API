using CyberGameTime.Bussiness.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Helpers
{
    public interface ISendMessageSignalR
    {
        Task SendPowerOff(string _screen_name);
        Task SendPowerOn(string _screen_name);
        Task SendError(string _screen_name);
    }
    public class SendMessageSignalR(IHubContext<MessageHubs> _hubContext) : ISendMessageSignalR
    {
        public async Task SendPowerOff(string _screen_name)
        {
            await _hubContext.Clients.All.SendAsync("ConnectionPowerOff", _screen_name);
        }
        public async Task SendPowerOn(string _screen_name)
        {
            await _hubContext.Clients.All.SendAsync("ConnectionPowerOn", _screen_name);
        }
        public async Task SendError(string _screen_name)
        {
            await _hubContext.Clients.All.SendAsync("ConnectionError", _screen_name);
        }
    }
}
