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

    public class GalleryDao : IGalleryDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public GalleryDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion

        public IList<Gallery> Get(int PageIndex = 1, int PageSize = 15)
        {
            IList<Gallery> result = null;
            DynamicParameters param = new DynamicParameters();

            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetGallery]";
                result = conn.Query<Gallery>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        public int Save(Gallery G, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            if (G.GUID != Guid.Empty)
            {
                param.Add("@GUID", G.GUID, dbType: DbType.Guid);
            }

            param.Add("@Title", G.Title, dbType: DbType.String);
            param.Add("@Date", G.Date, dbType: DbType.DateTime);
            param.Add("@ShortDescription", G.ShortDescription, dbType: DbType.String);
            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", G.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveGallery]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }


        public int Delete(Gallery G, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            if (G.GUID != Guid.Empty)
            {
                param.Add("@GUID", G.GUID, dbType: DbType.Guid);
            }

            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", G.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteGallery]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }

        public Gallery GetbyGuid(Guid GUID, int PageIndex = 1, int PageSize = 15)
        {
            Gallery G = null;
            DynamicParameters _param = null;
            if (GUID != Guid.Empty)
            {
                _param = new DynamicParameters();
                _param.Add("@GUID", GUID, dbType: DbType.Guid);
                _param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
                _param.Add("@PageSize", PageSize, dbType: DbType.Int32);
            }
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetGallerybyGuid]";
                var results = conn.QueryMultiple(
                   sql: SQL,
                   param: _param,
                   commandType: CommandType.StoredProcedure);

                if (results != null)
                {
                    G = results.Read<Gallery>().FirstOrDefault();
                    if (G != null)
                    {
                        G.galleryPhotos = results.Read<GalleryPhoto>().ToList();
                    }
                }
                conn.Close();
            }
            return G;
        }

        public IList<GalleryPhoto> GetGalleryPhoto(Guid GUID, int PageIndex = 1, int PageSize = 15)
        {
            IList<GalleryPhoto> result = null;
            DynamicParameters param = new DynamicParameters();
            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }

            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetGallery]";
                result = conn.Query<GalleryPhoto>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
            }
            return result;
        }


        public int DeleteGalleryPhoto(GalleryPhoto GP, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            if (GP.GUID != Guid.Empty)
            {
                param.Add("@GUID", GP.GUID, dbType: DbType.Guid);
            }

            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", GP.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteGalleryPhoto]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }

        public GalleryPhoto GetGalleryPhotoByGuid(Guid GUID)
        {
            GalleryPhoto result = new GalleryPhoto();
            DynamicParameters param = new DynamicParameters();
            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetGalleryPhotoByGuid]";
                result = conn.Query<GalleryPhoto>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }

        public long SaveGalleryPhoto(Gallery G, Guid UserGUID)
        {
            int result = 0;
            DataTable dtGalleryPhoto = new DataTable();
            dtGalleryPhoto.Columns.Add("ImageGUID", typeof(String));
            dtGalleryPhoto.Columns.Add("ImageName", typeof(String));
            dtGalleryPhoto.Columns.Add("ImageExtension", typeof(String));

            if (G != null && G.galleryPhotos != null)
            {
                foreach (GalleryPhoto GP in G.galleryPhotos)
                {
                    DataRow drALC = dtGalleryPhoto.NewRow();
                    drALC["ImageGUID"] = GP.ImageGUID;
                    drALC["ImageName"] = GP.ImageName;
                    drALC["ImageExtension"] = GP.ImageExtension;
                    dtGalleryPhoto.Rows.Add(drALC);
                }
            }
            DynamicParameters param = new DynamicParameters();
            if (G.GUID != Guid.Empty)
            {
                param.Add("@GUID", G.GUID, dbType: DbType.Guid);
            }
            param.Add("@GalleryImageTypeTbl", dtGalleryPhoto.AsTableValuedParameter());
            //param.Add("@CustomerLogoPageType", dtCLPT.AsTableValuedParameter());
            param.Add("@Description", G.galleryPhotoDescription.Description, dbType: DbType.String);
            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", G.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveGalleryPhoto]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }


        public long UpdateGalleryPhoto(GalleryPhoto GP, Guid UserGUID)
        {
            int result = 0;           
            DynamicParameters param = new DynamicParameters();

            if (GP.GUID != Guid.Empty)
            {
                param.Add("@GUID", GP.GUID, dbType: DbType.Guid);
            }
            param.Add("@ImageGUID", GP.ImageGUID, dbType: DbType.String);
            param.Add("@ImageName", GP.ImageName, dbType: DbType.String);
            param.Add("@ImageExtension", GP.ImageExtension, dbType: DbType.String);
            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", GP.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateGalleryPhoto]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
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

        public Gallery GetByTitle(string Title)
        {
            Gallery G = null;
            DynamicParameters _param = null;
            if (!string.IsNullOrEmpty(Title))
            {
                _param = new DynamicParameters();
                _param.Add("@Title", Title, DbType.String);
            }

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetGalleryByTitle]";
                var results = conn.QueryMultiple(
                   sql: SQL,
                   param: _param,
                   commandType: CommandType.StoredProcedure);

                if (results != null)
                {
                    G = results.Read<Gallery>().FirstOrDefault();
                    if (G != null)
                    {
                        G.galleryPhotos = results.Read<GalleryPhoto>().ToList();
                    }
                }
                conn.Close();
            }
            return G;
        }

      

        public IList<Gallery> GetList()
        {
            IList<Gallery> result = null;
            DynamicParameters param = new DynamicParameters();
            //if (!string.IsNullOrEmpty(Title))
            //{
            //    param.Add("@Title", Title, DbType.String);
            //}

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetGalleryList]";
                result = conn.Query<Gallery, GalleryPhoto, Gallery>(sql: SQL,
                   param: param,
                   map: (G, GP) =>
                   {
                       if (G != null)
                       {
                           G.galleryPhoto = GP;
                       }
                       return G;
                   },
                   commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }

            return result;
        }

    }
}
