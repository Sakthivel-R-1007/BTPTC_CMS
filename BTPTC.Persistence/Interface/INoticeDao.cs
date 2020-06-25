using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTPTC.Domain;

namespace BTPTC.Persistence.Interface
{
    public interface INoticeDao
    {
        int CheckNoticeType(string NewsTitle, Guid GUID);
        Int64 saveNotice(Notice NA);

        Notice GetEditTownProject(Guid id);
        Notice GetEditTownProjectPopupNotice(Guid id);

        List<Notice> GetNoticeDropDowm();
        

        List<Notice> GetNotice(String Year = null, int PageIndex = 1, int PageSize = 10);
        Notice GetEditNotice(Guid GUID);

        Int64 DeleteNotice(Guid GUID, Guid UserId, string SystemIp);

        //notice prject
        Int64 saveNoticeProject(Notice NA);

        List<Notice> GetNoticeProject(String Year = null, int PageIndex = 1, int PageSize = 10);

        Notice GetEditNoticeProject(Guid GUID);
        Int64 DeleteNoticeProject(Guid GUID, Guid UserId, string SystemIp);

        List<Notice> GetViewTownProject();
    }
}
