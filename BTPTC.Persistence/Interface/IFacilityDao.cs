using BTPTC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Persistence.Interface
{
    public interface IFacilityDao
    {
        int CheckFacilityType(string NewsTitle, Guid GUID);

        IList<Facility> FacilitySorting(/*Guid GUID*/);
        Int64 saveFacility(Facility EA);
        Int64 EditSaveFacility(Facility EA);

        List<Facility> Get(String Year = null, int PageIndex = 1, int PageSize = 10);

        List<Facility> GetEditFacility(Guid id);

        Facility GetEditFacilityImage(Guid id);
        Int64 DeleteFacility(Guid GUID, Guid UserId, string SystemIp);

        List<Facility> GetViewFacility();
    }
}
