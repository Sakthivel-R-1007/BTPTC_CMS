using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Domain
{
      public   class NoticeTypes : Entity<Int64>
    {

         public int Noticeid { get; set; }
        public string Location { get; set; }

        public string Status { get; set; }
        

        public Guid UserId { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }
    }
}
