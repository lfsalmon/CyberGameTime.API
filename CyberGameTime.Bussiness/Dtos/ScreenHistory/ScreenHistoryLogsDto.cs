using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Dtos.ScreenHistory;

public class ScreenHistoryLogsDto
{
    public long? ScreenId { get; set; }
    public string? Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
