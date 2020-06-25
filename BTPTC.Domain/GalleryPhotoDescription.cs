using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Domain
{
    public class GalleryPhotoDescription : Entity<Int64>
    {

        public string Description { get; set; }

        public GalleryPhoto galleryPhoto { get; set; }

        public Int64 GalleryId { get; set; }
    }
}
