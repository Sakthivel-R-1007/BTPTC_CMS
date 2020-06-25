using BTPTC.Domain;
using BTPTC.Persistence.DBConnectionFactory;
using BTPTC.Persistence.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Persistence.Implementations
{
    public class LoginDao:ILoginDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public LoginDao()
        {
            this.factory = new DbConnectionFactory("dbconn");
        }

        #endregion

        public IList<UserAccount> GetTest()
        {
            IList<UserAccount> C = null;
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
              //  string SQL = @"[USP_Test]";
               /// C = conn.Query<UserAccount>(SQL, commandType: CommandType.StoredProcedure).ToList<UserAccount>();
            }
            return C;
        }
    }
}
