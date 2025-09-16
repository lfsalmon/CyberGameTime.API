using AutoMapper;
using com.clusterrr.TuyaNet;
using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Bussiness.Helpers.Conectivity.Roku;
using CyberGameTime.Entities.enums;
using CyberGameTime.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.Screen
{
    public class ScanDevicesHandler(
        IGenericRepository<Screens> _repository,
        IConfiguration configuration,
        ILogger<ScanDevicesHandler> _logger
    ) : IRequestHandler<ScanDevicesQuery, uint>
    {

        private List<Screens> result;
        private List<TuyaDeviceApiInfo> tuyaDevices;
        public async Task<uint> Handle(ScanDevicesQuery request, CancellationToken cancellationToken)
        {
            result = _repository.GetAll().ToList();
            var _tuyaAccessId = configuration["TuyaCredentials:ClientId"];
            var _tuyaApiSecret = configuration["TuyaCredentials:ClientSecret"];
            var _tuyadevice = configuration["TuyaCredentials:deviceId"];


            var api = new TuyaApi(region: TuyaApi.Region.WesternAmerica, accessId: _tuyaAccessId, apiSecret: _tuyaApiSecret);
            tuyaDevices = (await api.GetAllDevicesInfoAsync(_tuyadevice)).ToList();
            _logger.LogInformation("Api Connected success");
            var scanner = new TuyaScanner();
            scanner.OnNewDeviceInfoReceived += Scanner_OnNewDeviceInfoReceived;
            _logger.LogInformation("Starting Tuya LAN Scan...");
            scanner.Start();
            await Task.Delay(10000);
            scanner.Stop();
            _logger.LogInformation("Tuya LAN Scan Stopped.");

            return 0;
        }
        private void Scanner_OnNewDeviceInfoReceived(object? sender, TuyaDeviceScanInfo e)
        {
            try
            {
                _logger.LogInformation($"New Device Found: {e.GwId} - IP: {e.IP}");
                var _crrentDevice = tuyaDevices.FirstOrDefault(x => x.Id == e.GwId);
                if(_crrentDevice is null) return;
                if(result.Any(x => x.Roku_DeviceId == _crrentDevice.Id)) return;

                var _entity_save = _repository.Add(new Screens
                {
                    ConnectionType = ConnectionType.TuyaLan,
                    ConsoleType = ConsoleType.Unknow,
                    CurrentStatus = enums.ScreenStatus.Online,
                    IpAddres = e.IP,
                    Name = _crrentDevice?.Name ?? "Unknow",
                    IdentificationName = _crrentDevice?.Name ?? "Unknow",
                    CreateAt = System.DateTime.UtcNow,
                    UpdateAt = System.DateTime.UtcNow,
                    Roku_udn = "LAN DEVICE TUYA",
                    URL_IFTTT_Off= "LAN DEVICE TUYA",
                    URL_IFTTT_On= "LAN DEVICE TUYA",
                    Roku_DeviceId = _crrentDevice.Id,
                    Roku_SerialNumber = _crrentDevice.LocalKey,
                });
                Console.WriteLine("Device Saved");    
            }
            catch (System.Exception ex)
            {
                var test = ex.Message;

            }
        }
    }
}
