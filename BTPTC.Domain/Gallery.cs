using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTPTC.Domain
{
    public class Gallery : Entity<Int64>
    {
        public DateTime Date { get; set; }

        public string DateString { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public List<GalleryPhoto> galleryPhotos { get; set; }

        public GalleryPhotoDescription galleryPhotoDescription { get; set; }

        public GalleryPhoto galleryPhoto { get; set; }

        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }

        public HttpPostedFileBase[] uploadAttach_1 { get; set; }
    }
}
