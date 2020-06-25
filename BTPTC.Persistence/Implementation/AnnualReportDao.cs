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

    public class AnnualReportDao : IAnnualReportDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public AnnualReportDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion

        public IList<AnnualReport> Get(int PageIndex = 1, int PageSize = 15)
        {
            IList<AnnualReport> result = null;
            DynamicParameters param = new DynamicParameters();

            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetAnnualReport]";
                result = conn.Query<AnnualReport>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        public int Save(AnnualReport AR, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            if (AR.GUID != Guid.Empty)
            {
                param.Add("@GUID", AR.GUID, dbType: DbType.Guid);
            }
            param.Add("@Title", AR.Title, dbType: DbType.String);
            param.Add("@ImageGUID", AR.ImageGUID, dbType: DbType.String);
            param.Add("@ImageName", AR.ImageName, dbType: DbType.String);
            param.Add("@ImageExtension", AR.ImageExtension, dbType: DbType.String);
            param.Add("@ImageAltTag", AR.ImageAltTag, dbType: DbType.String);
            param.Add("@PDFGUID", AR.PDFGUID, dbType: DbType.String);
            param.Add("@PDFName", AR.PDFName, dbType: DbType.String);
            param.Add("@PDFExtension", AR.PDFExtension, dbType: DbType.String);
            param.Add("@PDFFileSize", AR.PDFFileSize, dbType: DbType.String);
            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", AR.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveAnnualReport]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }


        public int Delete(AnnualReport AR, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            if (AR.GUID != Guid.Empty)
            {
                param.Add("@GUID", AR.GUID, dbType: DbType.Guid);
            }

            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", AR.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteAnnualReport]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }

        public AnnualReport GetbyGuid(Guid GUID)
        {
            AnnualReport result = null;
            DynamicParameters param = new DynamicParameters();

            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }          

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetAnnualReportByGuid]";
                result = conn.Query<AnnualReport>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }

        public int CheckGalleryTitle(string Title, Guid GUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Title", Title.Trim(), dbType: DbType.String);

            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_CheckGalleryTitle]";
                result = conn.Query<int>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


    }
}
