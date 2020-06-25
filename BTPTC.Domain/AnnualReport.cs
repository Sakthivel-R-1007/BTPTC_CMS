using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTPTC.Domain
{
    public class AnnualReport : Entity<Int64>
    {
        public string Title { get; set; }
        public string ImageAltTag { get; set; }
        public string ImageGUID { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public string PDFGUID { get; set; }
        public string PDFName { get; set; }
        public string PDFExtension { get; set; }
        public string PDFFileSize { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }
     
    }
}
