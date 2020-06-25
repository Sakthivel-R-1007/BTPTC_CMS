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
    public class FacilityDao:IFacilityDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public FacilityDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion



        public int CheckFacilityType(string NewsTitle, Guid GUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();
            param.Add("@FacilityType", NewsTitle.Trim(), dbType: DbType.String);

            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_CheckFacilityType]";
                result = conn.Query<int>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


        public IList<Facility> FacilitySorting(/*Guid GUID*/)
        {
            IList<Facility> Facilty = null;

            DynamicParameters param = new DynamicParameters();
           // param.Add("@GUID", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetFacilitySortingList]";
                Facilty = conn.Query<Facility>(SQL, param, commandType: CommandType.StoredProcedure).ToList<Facility>();
                conn.Close();
            }
            return Facilty;
        }



        public Int64 saveFacility(Facility EA)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (EA.GUID != Guid.Empty)
                param.Add("@Guid", EA.GUID, dbType: DbType.Guid);

            //param.Add("@Guid", EA.EncryptedId, dbType: DbType.Guid)

            DataTable SortingTbl = new DataTable("SortingTbl");
            SortingTbl.Columns.Add("ShortDescription", typeof(string));
            SortingTbl.Columns.Add("ThumbnailImageGUID", typeof(Guid));
            SortingTbl.Columns.Add("ThumbnailImage", typeof(string));
            SortingTbl.Columns.Add("ThumbnailImageExtension", typeof(string));
            SortingTbl.Columns.Add("ThumbnailImageAltTag", typeof(string));

            if (EA.FacilityImage != null && EA.FacilityImage.Count() > 0)
            {
                EA.FacilityImage.ForEach(d => { {
                        
                        SortingTbl.Rows.Add(d.Description, d.ImageGUID, d.ImageName,d.ImageExtension, d.ImageAltTag);
                    
                    } });
            }



            param.Add("@Facility", EA.Name, dbType: DbType.String);
            param.Add("@FacilityImageTbl", SortingTbl.AsTableValuedParameter());
            param.Add("@SystemIP", EA.SystemIp, dbType: DbType.String);
            param.Add("@UserId", EA.UserId, dbType: DbType.Guid);            

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveFacility]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;


        }


          public Int64 EditSaveFacility(Facility EA)
       {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (EA.GUID != Guid.Empty)
                param.Add("@Guid", EA.GUID, dbType: DbType.Guid);
             

            //param.Add("@Guid", EA.EncryptedId, dbType: DbType.Guid)

            DataTable SortingTbl = new DataTable("SortingTbl");
            SortingTbl.Columns.Add("ShortDescription", typeof(string));
            SortingTbl.Columns.Add("ThumbnailImageGUID", typeof(Guid));
            SortingTbl.Columns.Add("ThumbnailImage", typeof(string));
            SortingTbl.Columns.Add("ThumbnailImageExtension", typeof(string));
            SortingTbl.Columns.Add("ThumbnailImageAltTag", typeof(string));

            if (EA.FacilityImage != null && EA.FacilityImage.Count() > 0)
            {
                EA.FacilityImage.ForEach(d => {
                    {

                        SortingTbl.Rows.Add(d.Description, d.ImageGUID, d.ImageName, d.ImageExtension, d.ImageAltTag);

                    }
                });
            }


            param.Add("@Editid", EA.Id, dbType: DbType.Int32);
            param.Add("@Facility", EA.Name, dbType: DbType.String);
            param.Add("@FacilityImageTbl", SortingTbl.AsTableValuedParameter());
            param.Add("@SystemIP", EA.SystemIp, dbType: DbType.String);
            param.Add("@UserId", EA.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_EditSaveFacility]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;

        }

        public List<Facility> GetEditFacility(Guid id)
        {
            List<Facility> events = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", id, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditFacility]";
                events = conn.Query<Facility>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return events;
        }

        

           public Facility GetEditFacilityImage(Guid id)
            {
            Facility events = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", id, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditFacilityImage]";
                events = conn.Query<Facility>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return events;
        }
        public List<Facility> Get(String Year = null, int PageIndex = 1, int PageSize = 10)
        {
            List<Facility> events = new List<Facility>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Year", Year, DbType.Int32);
            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetFacility1]";
                events = conn.Query<Facility>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return events;
        }


        public List<Facility> GetViewFacility()
        {

            List<Facility> events = new List<Facility>();
            List<FacilityImage> Image = new List<FacilityImage>();
            List<Facility> events1 = null;
            DynamicParameters param = new DynamicParameters();
            // param.Add("@id", id, DbType.Int32);
        
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetViewFacility]";
                var  result  = conn.QueryMultiple(SQL, param, commandType: CommandType.StoredProcedure);
                if (result != null)
                {
                     events = result.Read<Facility>().ToList();
                     Image = result.Read<FacilityImage>().ToList();

                  
                events1 = (from a in events
                                              select new Facility() {
                                                  Name = a.Name,
                                                  FacilityImage = (from b in Image
                                                                   where a.Id == b.Id
                                                                   select new FacilityImage() {
                                                                       ImageGUID = b.ImageGUID,
                                                                      ImageExtension  = b.ImageExtension,
                                                                       Description=b.Description,
                                                                        ImageAltTag=b.ImageAltTag,
                                                                         GUID=b.GUID,
                                                                          


                                                                   }).ToList()
                                              }).ToList();

                }
                conn.Close();

            }
            return events1;
        }



        public Int64 DeleteFacility(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteFacility]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


    }
}
