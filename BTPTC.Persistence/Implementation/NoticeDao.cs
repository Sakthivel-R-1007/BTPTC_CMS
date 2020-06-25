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
    public class NoticeDao : INoticeDao
    {

        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public NoticeDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion




        public int CheckNoticeType(string NewsTitle, Guid GUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();
            param.Add("@NoticeType", NewsTitle.Trim(), dbType: DbType.String);

            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_CheckNoticeType]";
                result = conn.Query<int>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;

        }



        public Int64 saveNotice(Notice EA)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (EA.GUID != Guid.Empty)
                param.Add("@Guid", EA.GUID, dbType: DbType.Guid);

            //param.Add("@Guid", EA.EncryptedId, dbType: DbType.Guid)

            DataTable SortingTbl = new DataTable("NoticeTbl");
            SortingTbl.Columns.Add("Location", typeof(string));
            SortingTbl.Columns.Add("Status", typeof(string));


            if (EA.NoticeTypes != null && EA.NoticeTypes.Count() > 0)
            {
                EA.NoticeTypes.ForEach(d => {
                    {

                        SortingTbl.Rows.Add(d.Location, d.Status);

                    }
                });
            }



            param.Add("@NoticeType", EA.NoticeType, dbType: DbType.String);
            param.Add("@TownProjectTbl", SortingTbl.AsTableValuedParameter());
            param.Add("@SystemIP", EA.SystemIp, dbType: DbType.String);
            param.Add("@UserId", EA.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveTownProject]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;


        }



        public Notice GetEditTownProject(Guid id)
        {
            Notice N = new Notice();
            List<NoticeTypes> NT = new List<NoticeTypes>();

           Notice events = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", id, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditTownProject]";
                var townProject = conn.QueryMultiple(SQL, param, commandType: CommandType.StoredProcedure);
           

                if (townProject != null)
                {

                     events = townProject.Read<Notice>().FirstOrDefault();
                    if (events != null)
                    {
                        N = events;
                        N.NoticeTypes = townProject.Read<NoticeTypes>().ToList();
                    }
                }
                conn.Close();
            }
            return N;
        }



        public Notice GetEditTownProjectPopupNotice(Guid id)
        {
            Notice events = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", id, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditTownProjectNotice]";
                events = conn.Query<Notice>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return events;
        }


        //public Int64 saveNotice(Notice EA)
        //{
        //    Int64 result = 0;
        //    DynamicParameters param = new DynamicParameters();
        //    if (EA.GUID != Guid.Empty)
        //        param.Add("@Guid", EA.GUID, dbType: DbType.Guid);

        //    param.Add("@NoticeType", EA.NoticeType, dbType: DbType.String);
        //    param.Add("@Location", EA.SystemIp, dbType: DbType.String);
        //    param.Add("@Location", EA.Location, dbType: DbType.String);
        //    param.Add("@Status", EA.Status, dbType: DbType.String);
        //    param.Add("@SystemIP", EA.SystemIp, dbType: DbType.String);
        //    param.Add("@UserId", EA.UserId, dbType: DbType.Guid);

        //    // param.Add("@EventGUID", ECGuid, dbType: DbType.Guid);

        //    using (IDbConnection conn = factory.GetConnection())
        //    {
        //        conn.Open();
        //        string SQL = @"[USP_SaveNotice]";
        //        result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        conn.Close();
        //    }
        //    return result;

        //}




        public List<Notice> GetNoticeDropDowm()
        {
            List<Notice> noties = new List<Notice>();
            DynamicParameters param = new DynamicParameters();
         

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetNoticeTypeList]";
                noties = conn.Query<Notice>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return noties;
        }



        public List<Notice> GetNotice(String Year = null, int PageIndex = 1, int PageSize = 10)
        {
            List<Notice> noties = new List<Notice>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Year", Year, DbType.Int32);
            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetNotice]";
                noties = conn.Query<Notice>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return noties;
        }




        public List<Notice> GetNoticeProject(String Year = null, int PageIndex = 1, int PageSize = 10)
        {
            List<Notice> noties = new List<Notice>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Year", Year, DbType.Int32);
            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetNoticeProject]";
                noties = conn.Query<Notice>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return noties;
        }



        public Notice GetEditNotice(Guid GUID)
        {
            Notice events = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditNotice]";
                events = conn.Query<Notice>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return events;
        }





        public Int64 DeleteNotice(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteNotice]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


        //Notice project


        public Int64 saveNoticeProject(Notice EA)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (EA.GUID != Guid.Empty)
                param.Add("@Guid", EA.GUID, dbType: DbType.Guid);

            param.Add("@NoticeType", EA.NoticeType, dbType: DbType.String);
            param.Add("@Location", EA.SystemIp, dbType: DbType.String);
            param.Add("@Location", EA.Location, dbType: DbType.String);
            param.Add("@Status", EA.Status, dbType: DbType.String);
            param.Add("@SystemIP", EA.SystemIp, dbType: DbType.String);
            param.Add("@UserId", EA.UserId, dbType: DbType.Guid);

            // param.Add("@EventGUID", ECGuid, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveNoticeProject]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;

        }


        public Notice GetEditNoticeProject(Guid GUID)
        {
            Notice events = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditNoticeProject]";
                events = conn.Query<Notice>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return events;
        }

        public Int64 DeleteNoticeProject(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteNoticeProject]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }



        public List<Notice> GetViewTownProject()
        {

            List<Notice> events = new List<Notice>();
            List<NoticeTypes> Image = new List<NoticeTypes>();
            List<Notice> events1 = null;
            DynamicParameters param = new DynamicParameters();
            // param.Add("@id", id, DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetViewTownProject]";
                var result = conn.QueryMultiple(SQL, param, commandType: CommandType.StoredProcedure);
                if (result != null)
                {
                    events = result.Read<Notice>().ToList();
                    Image = result.Read<NoticeTypes>().ToList();


                    events1 = (from a in events
                               select new Notice()
                               {
                                    NoticeType = a.NoticeType,
                                    NoticeTypes = (from b in Image
                                                    where a.Id == b.Id
                                                    select new NoticeTypes()
                                                    {
                                                        Location=b.Location,
                                                        Status=b.Status,
                                                         GUID=b.GUID,
                                                         Id=b.Id
                                                    }).ToList()
                               }).ToList();

                }
                conn.Close();

            }
            return events1;
        }


    }
}
