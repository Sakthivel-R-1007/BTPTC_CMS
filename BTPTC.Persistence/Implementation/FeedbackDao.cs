
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
    public class FeedbackDao : IFeedbackDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public FeedbackDao()
        {
            this.factory = new DbConnectionFactory("DefaultDB");
        }

        #endregion
        public int Save(Feedback FB)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            param.Add("@Name", FB.Name, dbType: DbType.String);
            param.Add("@Gender", FB.Gender, dbType: DbType.String);
            param.Add("@Mobile", FB.Mobile, dbType: DbType.String);
            param.Add("@OfficeResidential", FB.OfficeResidential, dbType: DbType.String);
            param.Add("@Email", FB.Email, dbType: DbType.String);
            param.Add("@Subject", FB.Subject, dbType: DbType.String);
            param.Add("@SubjectOthers", FB.SubjectOthers, dbType: DbType.String);
            param.Add("@BlkNo", FB.BlkNo, dbType: DbType.String);
            param.Add("@UnitNo1", FB.UnitNo1, dbType: DbType.String);
            param.Add("@UnitNo2", FB.UnitNo2, dbType: DbType.String);
            param.Add("@StreetName", FB.StreetName, dbType: DbType.String);
            param.Add("@ReportedLocation", FB.ReportedLocation, dbType: DbType.String);
            param.Add("@Comments", FB.Comments, dbType: DbType.String);
            param.Add("@FileName1", FB.FileName1, dbType: DbType.String);
            param.Add("@FileGuid1", FB.FileGuid1, dbType: DbType.String);
            param.Add("@Extension1", FB.Extension1, dbType: DbType.String);
            param.Add("@FileName2", FB.FileName2, dbType: DbType.String);
            param.Add("@FileGuid2", FB.FileGuid2, dbType: DbType.String);
            param.Add("@Extension2", FB.Extension2, dbType: DbType.String);
            param.Add("@FileName3", FB.FileName3, dbType: DbType.String);
            param.Add("@FileGuid3", FB.FileGuid3, dbType: DbType.String);
            param.Add("@Extension3", FB.Extension3, dbType: DbType.String);
            param.Add("@SystemIp", FB.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveFeedback]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }
    }
}
