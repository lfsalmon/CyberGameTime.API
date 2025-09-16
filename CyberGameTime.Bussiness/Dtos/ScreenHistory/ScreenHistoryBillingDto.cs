using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Dtos.ScreenHistory;

public class ScreenHistoryBillingDto
{
    public string ScreenName { get; set; }
    public double Hours{ get; set; }
    public double Minutes { get; set; }
    public double  UnitAmmont{ get; set; }  
    public double Total { get { return Hours * UnitAmmont; }  }
}
