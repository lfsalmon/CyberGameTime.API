using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Entities.Models;

namespace CyberGameTime.Application.Repositories.DeviceStatusLogs
{
    public interface IDeviceStatusLogRepository: IGenericRepository<DeviceStatusLog>
    {
        Task<DeviceStatusLog?> GEtLastLofOfScreenId(long screenId);
        Task<IEnumerable<DeviceStatusLog>> GetLogsByScreenId(long screenId);
    }
}