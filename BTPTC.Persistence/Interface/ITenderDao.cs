using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BTPTC.Domain;

namespace BTPTC.Persistence.Interface
{
    public interface ITenderDao
    {
        int CheckTenderTitleName(string NewsTitle, Guid GUID);
        Int64 SaveTenderNotice(Tender NL);

        List<Tender> Get(String Year = null, int PageIndex = 1, int PageSize = 10);

        Tender GetTenderNoticeByGuid(Guid GUID);

        List<Tender> GetTendersByStatus(string  FilterValue);


        Int64 DeleteTender(Guid GUID, Guid UserId, string SystemIp);
    }
}
