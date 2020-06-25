using BTPTC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Persistence.Interface
{
    public interface IMediaReleaseDao
    {
        int CheckMediaTitleName(string NewsTitle, Guid GUID);
        Int64 SaveMediaRelease(MediaRelease NL);

        List<MediaRelease> Get(String Year = null, int PageIndex = 1, int PageSize = 10);

        MediaRelease GetMediaReleaseByGuid(Guid GUID);
      

        Int64 DeleteMediaRelease(Guid GUID, Guid UserId, string SystemIp);
    }
}
