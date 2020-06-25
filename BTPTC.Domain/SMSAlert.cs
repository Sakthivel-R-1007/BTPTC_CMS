using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Domain
{
    public class SMSAlert : Entity<Int64>
    {
        public string Name { get; set; }
        public string BlkNo { get; set; }
        public string UnitNo1 { get; set; }
        public string UnitNo2 { get; set; }
        public string StreetName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Captcha { get; set; }

    }
}
