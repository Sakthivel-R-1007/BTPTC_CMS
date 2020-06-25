using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Domain
{
    public class BulkyItemRemovalService : Entity<Int64>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string BlkNo { get; set; }
        public string UnitNo1 { get; set; }
        public string UnitNo2 { get; set; }
        public string StreetName { get; set; }
        public string Mobile { get; set; }
        public string OfficeResidential { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string ItemDescription1 { get; set; }
        public string ItemDescription2 { get; set; }
        public string ItemDescription3 { get; set; }
        public string FileName1 { get; set; }
        public string FileGuid1 { get; set; }
        public string Extension1 { get; set; }
        public string FileName2 { get; set; }
        public string FileGuid2 { get; set; }
        public string Extension2 { get; set; }
        public string FileName3 { get; set; }
        public string FileGuid3 { get; set; }
        public string Extension3 { get; set; }
        public string Captcha { get; set; }
    }
}
