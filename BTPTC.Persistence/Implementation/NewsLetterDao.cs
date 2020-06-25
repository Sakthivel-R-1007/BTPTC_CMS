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
    public class NewsLetterDao : INewsLetterDao
    {

        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public NewsLetterDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion


        public Int64 SaveNewsLetter(NewsLetter NL)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (NL.GUID != Guid.Empty)
                param.Add("@Guid", NL.GUID, dbType: DbType.Guid);

            param.Add("@Title", NL.Title, dbType: DbType.String);
            param.Add("@Quarter", NL.Quarter, dbType: DbType.String);
            param.Add("@Year", NL.Year, dbType: DbType.Int32);
            param.Add("@FileSize", NL.FileSize, dbType: DbType.String);

            param.Add("@ImageGUID", NL.LargeImageGUID, dbType: DbType.String);
            param.Add("@ImageName", NL.LargeImageName, dbType: DbType.String);
            param.Add("@ImageExtension", NL.LargeImageExtension, dbType: DbType.String);
            param.Add("@ImageAltTag", NL.LargeImageAltTag, dbType: DbType.String);
            param.Add("@ThumbnailImageGUID", NL.ThumbnailImageGUID, dbType: DbType.String);
            param.Add("@ThumbnailImageName", NL.ThumbnailImageName, dbType: DbType.String);
            param.Add("@ThumbnailImageExtension", NL.ThumbnailImageExtension, dbType: DbType.String);
            param.Add("@ThumbnailImageAltTag", NL.ThumbnailImageAltTag, dbType: DbType.String);
            param.Add("@PdfFileGUID", NL.PdfFileGUID, dbType: DbType.String);
            param.Add("@PdfFileName", NL.PdfFileName, dbType: DbType.String);
            param.Add("@PdfFileExtension", NL.PdfFileExtension, dbType: DbType.String);
            param.Add("@SystemIP", NL.SystemIp, dbType: DbType.String);
            param.Add("@UserId", NL.UserId, dbType: DbType.Guid);



            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveNewsLetter]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }

        public List<NewsLetter> Get(String Year = null, int PageIndex = 1, int PageSize = 10)
        {
            List<NewsLetter> newsLetter = new List<NewsLetter>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Year", Year, DbType.Int32);
            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetNewsLetter]";
                newsLetter = conn.Query<NewsLetter>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return newsLetter;
        }


        public NewsLetter GetEditNewsLetterGUID(Guid GUID)
        {
            NewsLetter newsletter = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditNewsLetter]";
                newsletter = conn.Query<NewsLetter>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return newsletter;
        }


        public Int64 DeleteNewsLetter(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteNewsLetter]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }



        public int CheckNewsTitleName(string NewsTitle, Guid GUID)
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
                string SQL = @"[USP_CheckNewsTitle]";
                result = conn.Query<int>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;

        }




        public List<NewsLetter> GetYearViewNewsLetter(int year)
        {
            List<NewsLetter> newsLetter = new List<NewsLetter>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@year", year, dbType: DbType.Int32);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetYearViewNewsLetter]";
                newsLetter = conn.Query<NewsLetter>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return newsLetter;
        }



        public List<NewsLetter> ViewNewsLetter()
        {
            List<NewsLetter> newsLetter = new List<NewsLetter>();
            DynamicParameters param = new DynamicParameters();


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetViewNewsLetter]";
                newsLetter = conn.Query<NewsLetter>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return newsLetter;
        }



    }
}
