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
    public class MediaReleaseDao:IMediaReleaseDao
    {

        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public MediaReleaseDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion

        public Int64 SaveMediaRelease(MediaRelease MR)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (MR.GUID != Guid.Empty)
                param.Add("@Guid", MR.GUID, dbType: DbType.Guid);

            param.Add("@Title", MR.Title, dbType: DbType.String);
            param.Add("@Date", MR.Date, dbType: DbType.DateTime);
            param.Add("@PdfFileGUID", MR.PdfFileGUID, dbType: DbType.String);
            param.Add("@PdfFileName", MR.PdfFileName, dbType: DbType.String);
            param.Add("@PdfFileExtension", MR.PdfFileExtension, dbType: DbType.String);
            param.Add("@SystemIP", MR.SystemIp, dbType: DbType.String);
            param.Add("@UserId", MR.UserId, dbType: DbType.Guid);



            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveMediaRelease]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


        public List<MediaRelease> Get(String Year = null, int PageIndex = 1, int PageSize = 10)
        {
            List<MediaRelease> mediaRelease = new List<MediaRelease>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Year", Year, DbType.Int32);
            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetMediaRelease]";
                mediaRelease = conn.Query<MediaRelease>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return mediaRelease;
        }



        public MediaRelease GetMediaReleaseByGuid(Guid GUID)
        {
            MediaRelease mediaRelease = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetMediaReleaseByGuid]";
                mediaRelease = conn.Query<MediaRelease>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return mediaRelease;
        }

        public Int64 DeleteMediaRelease(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteMediaRelease]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


        public int CheckMediaTitleName(string NewsTitle, Guid GUID)
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
                string SQL = @"[USP_CheckMediaTitle]";
                result = conn.Query<int>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;

        }



        //TENDER

       

    }
}
