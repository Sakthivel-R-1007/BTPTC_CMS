using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Domain
{
   public class EventContents
    {
        public List<Events> UpcomingEvents { get; set; }
        public List<Events> PastEvents { get; set; }
    }
}
