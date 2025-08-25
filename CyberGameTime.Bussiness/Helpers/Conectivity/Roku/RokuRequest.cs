using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Bussiness.Responses;
using CyberGameTime.Entities.enums;
using CyberGameTime.Models;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace CyberGameTime.Bussiness.Helpers.Conectivity.Roku
{
    public class RokuDevice : GeneralConneciton
    {
        public RokuDevice(Screens _screen) : base(_screen)
        {
        }

        public override async Task<Status> GetStatus()
        {
            var rokuinfo=await getInfo();
            return rokuinfo?.Status ?? Entities.enums.Status.Undefined;
        }

        private Status ValidatStatus(string powerMode) => powerMode switch
        {
            "PowerOn" => Status.PowerOn,
            _ => Status.PowerOff
        };

        public override async Task<DeviceResponseData?> getInfo()
        {
            try
            {
                RokuInfoResponse deviceInfo = null;
                using HttpClient client = new HttpClient();
                var response = await client.GetAsync($"http://{_Screen?.IpAddres}:8060/query/device-info");
                if (response is not null && response.IsSuccessStatusCode)
                {
                    var serializer = new XmlSerializer(typeof(RokuInfoResponse));
                    var responseData = await response.Content.ReadAsStringAsync();
                    using (var reader = new StringReader(responseData))
                    {
                        deviceInfo = (RokuInfoResponse)serializer.Deserialize(reader);
                    }
                }
                return deviceInfo is null ? new DeviceResponseData() : new DeviceResponseData()
                {
                    Udn = deviceInfo.Udn,
                    DeviceId = deviceInfo.DeviceId,
                    ModelName = deviceInfo.ModelName,
                    ModelNumber = deviceInfo.ModelNumber,
                    SerialNumber = deviceInfo.SerialNumber,
                    SoftwareVersion = deviceInfo.SoftwareVersion,
                    Status = ValidatStatus(deviceInfo.PowerMode)
                };
            }
            catch(HttpRequestException   ex)
            {
                return new DeviceResponseData();
            }
          
        }

        public override async Task<Status> TurnOff()
        {
            using HttpClient client = new HttpClient();
            //var response = await client.PostAsync($"http://{_Screen?.IpAddres}:8060/keypress/PowerOff", null);
            var response = await client.PostAsync($"http://{_Screen?.IpAddres}:8060/keypress/Home", null);
            if (response.IsSuccessStatusCode) return Status.PowerOff;
            return Status.Undefined;
            
        }
        public override async Task<Status> TurnOn()
        {

            using HttpClient client = new HttpClient();
            var response = await client.PostAsync($"http://{_Screen?.IpAddres}:8060/keypress/PowerOn", null);
            if (response.IsSuccessStatusCode) return Status.PowerOff;
            return Status.Undefined;
        }

    }
}
