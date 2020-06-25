using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTPTC.Domain;


namespace BTPTC.Persistence.Interface
{
   public  interface IEventsDao
    {
        List<Events> Get(String Year = null, int PageIndex = 1, int PageSize = 10);
    
        Events GetEditEvent(Guid GUID);
        Int64 saveEvents(Events EA);

        Int64 DeleteEvent(Guid GUID, Guid UserId, string SystemIp);

        EventContents GetViewEvents();

        Events GetEventDetails(Guid guid);

    }
}
