using BTPTC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Persistence.Interface
{
    public interface IAnnualReportDao
    {

        IList<AnnualReport> Get(int PageIndex = 1, int PageSize = 15);
        int Save(AnnualReport G, Guid UserGUID);
        int Delete(AnnualReport G, Guid UserGUID);
        AnnualReport GetbyGuid(Guid GUID);
        int CheckGalleryTitle(string Title, Guid GUID);

    }
}
