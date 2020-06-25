using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTPTC.Domain
{
     public  class Events: Entity<Int64>
    {
        public string EventTitle { get; set; }
        public string GuestOfHonour { get; set; }
        public DateTime EventDate { get; set; }
     
        public string FromDateString { get; set; }
        public string EventDateString { get; set; }

        public DateTime StartEventTime { get; set; }
        public string StartEventString { get; set; }
        public DateTime EndEventTime { get; set; }
        public string EndEventTimeString { get; set; }
        public string Venue { get; set; }
        public string ShortDescription { get; set; }
        public string ThumbnailImageGUID { get; set; }
         public string ThumbnailImageName { get; set; }
        public HttpPostedFileBase ThumbnailImage { get; set; }
        public string ThumbnailImageExtension { get; set; }
        public string ThumbnailImageAltTag { get; set; }
        public string LargeImageGUID { get; set; }
        public string LargeImageName { get; set; }
         public HttpPostedFileBase LargeImage { get; set; }
        public string LargeImageExtension { get; set; }
        public string LargeImageAltTag { get; set; }
        public string Contents { get; set; }
      

        public Guid UserId { get; set; }

        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }
    }
}
