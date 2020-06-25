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
    public class BulkyItemRemovalServiceDao : IBulkyItemRemovalServiceDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public BulkyItemRemovalServiceDao()
        {
            this.factory = new DbConnectionFactory("DefaultDB");
        }

        #endregion
        public int Save(BulkyItemRemovalService BS)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            param.Add("@Name", BS.Name, dbType: DbType.String);
            param.Add("@Email", BS.Email, dbType: DbType.String);
            param.Add("@BlkNo", BS.BlkNo, dbType: DbType.String);
            param.Add("@UnitNo1", BS.UnitNo1, dbType: DbType.String);
            param.Add("@UnitNo2", BS.UnitNo2, dbType: DbType.String);
            param.Add("@StreetName", BS.StreetName, dbType: DbType.String);
            param.Add("@Mobile", BS.Mobile, dbType: DbType.String);
            param.Add("@OfficeResidential", BS.OfficeResidential, dbType: DbType.String);
            param.Add("@AppointmentDate", BS.AppointmentDate, dbType: DbType.DateTime);
            param.Add("@AppointmentTime", BS.AppointmentTime, dbType: DbType.String);
            param.Add("@ItemDescription1", BS.ItemDescription1, dbType: DbType.String);
            param.Add("@ItemDescription2", BS.ItemDescription2, dbType: DbType.String);
            param.Add("@ItemDescription3", BS.ItemDescription3, dbType: DbType.String);
            param.Add("@FileName1", BS.FileName1, dbType: DbType.String);
            param.Add("@FileGuid1", BS.FileGuid1, dbType: DbType.String);
            param.Add("@Extension1", BS.Extension1, dbType: DbType.String);
            param.Add("@FileName2", BS.FileName2, dbType: DbType.String);
            param.Add("@FileGuid2", BS.FileGuid2, dbType: DbType.String);
            param.Add("@Extension2", BS.Extension2, dbType: DbType.String);
            param.Add("@FileName3", BS.FileName3, dbType: DbType.String);
            param.Add("@FileGuid3", BS.FileGuid3, dbType: DbType.String);
            param.Add("@Extension3", BS.Extension3, dbType: DbType.String);
            param.Add("@SystemIp", BS.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_BulkyItemRemovalService]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }
    }
}
