using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTPTC.Domain
{
    public class Facility: Entity<Int64>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageGUID { get; set; }
        public string ImageName { get; set; }
        public HttpPostedFileBase LargeImage { get; set; }
        public string ImageExtension { get; set; }
        public string ImageAltTag { get; set; }
        public string  Imagepath { get; set; }
        public Guid UserId { get; set; }

        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }

          public List<FacilityImage> FacilityImage { get; set; }
 
       

    }
}
