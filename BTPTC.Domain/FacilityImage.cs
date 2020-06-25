using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTPTC.Domain
{
    public class FacilityImage : Entity<Int64>
    {
     
        public string Description { get; set; }
        public string ImageGUID { get; set; }
        public string ImageName { get; set; }
        public HttpPostedFileBase LargeImage { get; set; }
        public string ImageExtension { get; set; }
        public string ImageAltTag { get; set; }
        public string Imagepath { get; set; }
    }
}
