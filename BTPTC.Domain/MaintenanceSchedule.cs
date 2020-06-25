using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTPTC.Domain
{
  public   class MaintenanceSchedule : Entity<Int64>
    {
        
        public string FileGUID { get; set; }

        public string FileName { get; set; }
        public string Division { get; set; }
        public  string BlkNo { get; set; }
        public string StreetName { get; set; }
        public string RCZone { get; set; }
        public string BlockWashing { get; set; }
        public string BinChuteFogging { get; set; }
        public string BinChuteFlushing { get; set; }
        public string LiftMaintenance { get; set; }
        public Guid UserId { get; set; }
      

      public   List<MaintenanceSchedule> maintenanceSchedule { get; set; }
       public  List<ErrorMsg> msg { get; set; }

    }
}
