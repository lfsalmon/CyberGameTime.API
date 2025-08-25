using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Application.Repositories.DeviceStatusLogs;
using CyberGameTime.Entities.enums;
using CyberGameTime.Entities.Models;
using CyberGameTime.enums;
using CyberGameTime.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Services
{
    public class LogService(ILogger<LogService> loggerBase, IDeviceStatusLogRepository _repository) : ILogService
    {
        
        public async Task AddLog(Screens screen, Entities.enums.Status CurrentStatus, string Message="")
        {
            var getLastLog = await _repository.GEtLastLofOfScreenId(screen.Id);

            try
            {
                bool sendNewLog = false;
                if (screen is not null && getLastLog is not null)
                {
                    DateTime now = DateTime.UtcNow;
                    var timeDiff = now - getLastLog.CreateAt;
                    sendNewLog =  timeDiff.TotalMinutes >= 20;
                }
                if(screen.ConnectionType==ConnectionType.IFTTT) CurrentStatus= Entities.enums.Status.Undefined;

                if (getLastLog is null || getLastLog.Status != CurrentStatus.ToString() || sendNewLog)
                {
                    await _repository.Add(new DeviceStatusLog()
                    {
                        ScreenId = screen.Id,
                        Status = CurrentStatus.ToString(),
                        Message = Message,
                        CreateAt = DateTime.UtcNow,
                        IpAddress = screen.IpAddres,
                        ConnectionType = screen.ConnectionType.ToString(),
                        ScreenName = screen.Name,
                        UpdateAt = DateTime.UtcNow,
                    });
                }
                else
                {
                    getLastLog.UpdateAt = DateTime.UtcNow;
                    await _repository.Update(getLastLog);
                }


            }
            catch (Exception ex)
            {
                loggerBase.LogError(ex, "Error adding log for screen {ScreenId}", screen.Id);

                await _repository.Add(new DeviceStatusLog()
                {
                    ScreenId = screen.Id,
                    Status = CurrentStatus.ToString(),
                    CreateAt = DateTime.UtcNow,
                    IpAddress = screen.IpAddres,
                    ConnectionType = screen.ConnectionType.ToString(),
                    ScreenName = screen.Name,
                    UpdateAt = DateTime.UtcNow,
                });
            }
            loggerBase.LogInformation("Se actualizo/agrrgo log de {ScreenId}", screen.Id);
        }
    }
}
