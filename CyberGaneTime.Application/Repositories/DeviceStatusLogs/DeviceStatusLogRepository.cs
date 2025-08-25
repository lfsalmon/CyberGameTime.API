using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Application.Repositories.RentalScreen;
using CyberGameTime.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace CyberGameTime.Application.Repositories.DeviceStatusLogs
{
    public class DeviceStatusLogRepository : GenericRepository<DeviceStatusLog>, IDeviceStatusLogRepository
    {
        readonly ILogger<DeviceStatusLogRepository> _logger;
        public DeviceStatusLogRepository(ILogger<GenericRepository<DeviceStatusLog>> loggerBase,
                                        ILogger<DeviceStatusLogRepository> logger,
                                        IConfiguration configuration) : base(loggerBase, configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<DeviceStatusLog>> GetLogsByScreenId(long screenId)
        {
            using var context = new CyberGameContext(_DbContextSettigs.Options);
            return context.DeviceStatusLog.Where(x => x.ScreenId == screenId).ToList();
        }
        public async Task<DeviceStatusLog?> GEtLastLofOfScreenId(long screenId)
        {
            using var context = new CyberGameContext(_DbContextSettigs.Options);
            return await context.DeviceStatusLog?.Where(x => x.ScreenId == screenId)?.OrderByDescending(x => x.Id)?.FirstOrDefaultAsync();
        }

    }
}
