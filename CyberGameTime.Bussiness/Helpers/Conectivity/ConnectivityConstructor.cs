using CyberGameTime.Bussiness.Helpers.Conectivity.IFTTT;
using CyberGameTime.Bussiness.Helpers.Conectivity.Roku;

using CyberGameTime.Entities.enums;
using CyberGameTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Helpers.Conectivity
{
    public static class ConnectivityConstructor
    {
        public static GeneralConneciton constructor(Screens _screen)
        {
            return _screen.ConnectionType switch
            {
                ConnectionType.Roku => new RokuDevice(_screen),
                ConnectionType.IFTTT=> new WebHooksIFTTT(_screen),
                _ => new RokuDevice(_screen)

            };
        }
    }
}
