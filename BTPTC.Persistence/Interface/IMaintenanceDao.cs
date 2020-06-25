using BTPTC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Persistence.Interface
{
    public interface IMaintenanceDao
    {
        Int64 SaveExcelLog(string FileGUID, string FileName, string SystemIp, Guid UserId);
        int SaveMaintenance(IList<MaintenanceSchedule> MS, Int64 ExcelId, string SystemIp, Guid UserId);
        IList<MaintenanceSchedule> GetStreetList();

        IList<MaintenanceSchedule> GetMaintenanceScheduleListByStreet(string StreetName, string BlkNo);
        List<MaintenanceSchedule> GetViewMaintenance();

    }
}
