
using BTPTC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Persistence.Interface
{
    public interface ISMSAlertDao
    {
        int Save(SMSAlert SA);
    }
}
