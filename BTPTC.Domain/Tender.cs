using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTPTC.Domain
{
    public class Tender: Entity<Int64>
    {
        public string Project { get; set; }
        public string Status { get; set; }
        public DateTime ClosingDate { get; set; }
        public string  ClosingDateString { get; set; }
        public string Type { get; set; }

        public string PdfFileGUID { get; set; }
        public string PdfFileName { get; set; }
        public HttpPostedFileBase FileName { get; set; }
        public string PdfFileExtension { get; set; }
        public Guid UserId { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }

    }
}
