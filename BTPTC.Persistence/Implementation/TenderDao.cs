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
    public class TenderDao : ITenderDao
    {

        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public TenderDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion

        public Int64 SaveTenderNotice(Tender TR)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (TR.GUID != Guid.Empty)
                param.Add("@Guid", TR.GUID, dbType: DbType.Guid);

            param.Add("@Project", TR.Project, dbType: DbType.String);
            param.Add("@Status", TR.Status, dbType: DbType.String);
            param.Add("@closingDate", TR.ClosingDate, dbType: DbType.DateTime);
            param.Add("@PdfFileGUID", TR.PdfFileGUID, dbType: DbType.String);
            param.Add("@PdfFileName", TR.PdfFileName, dbType: DbType.String);
            param.Add("@PdfFileExtension", TR.PdfFileExtension, dbType: DbType.String);
            param.Add("@SystemIP", TR.SystemIp, dbType: DbType.String);
            param.Add("@UserId", TR.UserId, dbType: DbType.Guid);



            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveTenderNotice]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


        public List<Tender> Get(String Year = null, int PageIndex = 1, int PageSize = 10)
        {
            List<Tender> mediaRelease = new List<Tender>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Year", Year, DbType.Int32);
            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetTender]";
                mediaRelease = conn.Query<Tender>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return mediaRelease;
        }


        

         public List<Tender> GetTendersByStatus(string  Filterval)
        {
            List<Tender> tender = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Status", Filterval, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetTenderByStatus]";
                tender = conn.Query<Tender>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return tender;
        }

        public Tender GetTenderNoticeByGuid(Guid GUID)
        {
            Tender tender = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetTenderByGuid]";
                tender = conn.Query<Tender>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return tender;
        }


        public Int64 DeleteTender(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteTender]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


        public int CheckTenderTitleName(string NewsTitle, Guid GUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();
            param.Add("@NewsTitle", NewsTitle.Trim(), dbType: DbType.String);

            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_CheckTenderTitle]";
                result = conn.Query<int>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;

        }
    }
}
