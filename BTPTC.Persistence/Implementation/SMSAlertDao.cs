
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

namespace BTPTC.Persistence.Implementation
{
    public class SMSAlertDao : ISMSAlertDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public SMSAlertDao()
        {
            this.factory = new DbConnectionFactory("DefaultDB");
        }

        public int Save(SMSAlert SA)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            param.Add("@Name", SA.Name, dbType: DbType.String);
            param.Add("@BlkNo", SA.BlkNo, dbType: DbType.String);
            param.Add("@UnitNo1", SA.UnitNo1, dbType: DbType.String);
            param.Add("@UnitNo2", SA.UnitNo2, dbType: DbType.String);
            param.Add("@StreetName", SA.StreetName, dbType: DbType.String);
            param.Add("@Email", SA.Email, dbType: DbType.String);      
            param.Add("@Mobile", SA.Mobile, dbType: DbType.String);
            param.Add("@SystemIp", SA.SystemIp, dbType: DbType.String);


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveSMSAlert]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }

        #endregion
    }
}
