using CyberGameTime.Entities.enums;
using CyberGameTime.Models;

namespace CyberGameTime.Bussiness.Services
{
    public interface ILogService
    {
        Task AddLog(Screens screen, Status CurrentStatus, string Message = "");
    }
}