using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTPTC.Domain
{
    public  class NewsLetter : Entity<Int64>
    {
        public string Title { get; set; }

        public string Quarter { get; set; }

        public int Year { get; set; }
        public string  FileSize { get; set; }

        public string LargeImageGUID { get; set; }
        public string LargeImageName { get; set; }
        public HttpPostedFileBase LargeImage { get; set; }
        public string LargeImageExtension { get; set; }
        public string LargeImageAltTag { get; set; }


        public string ThumbnailImageGUID { get; set; }
        public string ThumbnailImageName { get; set; }
        public HttpPostedFileBase ThumbnailImage { get; set; }
        public string ThumbnailImageExtension { get; set; }
        public string ThumbnailImageAltTag { get; set; }

        public string PdfFileGUID { get; set; }
        public string PdfFileName { get; set; }
        public HttpPostedFileBase FileName { get; set; }
        public string PdfFileExtension { get; set; }
        public Guid UserId { get; set; }
        public Int64 RowNum { get; set; }

        public Int64 TotalCount { get; set; }
    }
}
