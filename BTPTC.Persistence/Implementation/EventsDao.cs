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
    public class EventsDao : IEventsDao
    {


        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public EventsDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion

        public Events GetEventDetails(Guid GUID)
        {
            Events events = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[GetEventDetails]";
                events = conn.Query<Events>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return events;
        }


        public Events GetEditEvent(Guid GUID)
        {
            Events events = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditEvent]";
                events = conn.Query<Events>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return events;
        }


        public Int64 DeleteEvent(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteEvent]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }



        //public IList<Events> GetEventArticles(Guid GUID, Int64 Year)
        //{
        //    IList<Events> eventArticle = null;
        //    DynamicParameters param = new DynamicParameters();
        //    param.Add("@Guid", GUID, dbType: DbType.Guid);
        //    param.Add("@Year", Year, dbType: DbType.Int64);
        //    using (IDbConnection conn = factory.GetConnection())
        //    {
        //        conn.Open();
        //        string SQL = @"[USP_GetEeventArticlebyYear_btptc]";
        //        eventArticle = conn.Query<Events>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
        //        conn.Close();
        //    }
        //    return eventArticle;
        //}


        public Int64 saveEvents(Events EA)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (EA.GUID != Guid.Empty)
                param.Add("@Guid", EA.GUID, dbType: DbType.Guid);


            //param.Add("@Guid", EA.EncryptedId, dbType: DbType.Guid);
            param.Add("@EventTitle", EA.EventTitle, dbType: DbType.String);
            param.Add("@GuestOfHonour", EA.GuestOfHonour, dbType: DbType.String);
            param.Add("@EventDate", EA.EventDate, dbType: DbType.DateTime);
            param.Add("@StartEventTime", EA.StartEventTime, dbType: DbType.DateTime);
            param.Add("@EndEventTime", EA.EndEventTime, dbType: DbType.DateTime);
            param.Add("@Venue", EA.Venue, dbType: DbType.String);
            param.Add("@ShortDescription", EA.ShortDescription, dbType: DbType.String);

            param.Add("@ThumbnailImageGUID", EA.ThumbnailImageGUID, dbType: DbType.String);
            param.Add("@ThumbnailImage", EA.ThumbnailImageName, dbType: DbType.String);
            param.Add("@ThumbnailImageExtension", EA.ThumbnailImageExtension, dbType: DbType.String);
            param.Add("@ThumbnailImageAltTag", EA.ThumbnailImageAltTag, dbType: DbType.String);

            param.Add("@LargeImageGUID", EA.LargeImageGUID, dbType: DbType.String);
            param.Add("@LargeImage", EA.LargeImageName, dbType: DbType.String);
            param.Add("@LargeImageExtension", EA.LargeImageExtension, dbType: DbType.String);
            param.Add("@LargeImageAltTag", EA.LargeImageAltTag, dbType: DbType.String);

            param.Add("@Contents", EA.Contents, dbType: DbType.String);
            param.Add("@SystemIP", EA.SystemIp, dbType: DbType.String);
            param.Add("@UserId", EA.UserId, dbType: DbType.Guid);

            // param.Add("@EventGUID", ECGuid, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveEvent]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }

        public EventContents GetViewEvents()
        {

            EventContents events = new EventContents();
            DynamicParameters param = new DynamicParameters();


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetViewEvents]";
                var result = conn.QueryMultiple(SQL, param, commandType: CommandType.StoredProcedure);
                if (result != null)
                {

                    events.UpcomingEvents = result.Read<Events>().ToList();
                    events.PastEvents = result.Read<Events>().ToList();

                }

                conn.Close();
            }
            return events;
        }



        public List<Events> Get(String Year = null, int PageIndex = 1, int PageSize = 10)
        {
            List<Events> events = new List<Events>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Year", Year, DbType.Int32);
            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEvents]";
                events = conn.Query<Events>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return events;
        }

    }
}
