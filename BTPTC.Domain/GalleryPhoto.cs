using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTPTC.Domain
{
    public class GalleryPhoto : Entity<Int64>
    {

        public Int64 GalleryPhotoDescriptionId {get; set;}
        public string ImageGUID { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public HttpPostedFileBase[] photos { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }
        public Guid GalleryGuid { get; set; }

    }
}
