using BTPTC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Persistence.Interface
{
    public interface IGalleryDao
    {

        IList<Gallery> Get(int PageIndex = 1, int PageSize = 15);
        int Save(Gallery G, Guid UserGUID);
        int Delete(Gallery G, Guid UserGUID);
        Gallery GetbyGuid(Guid GUID, int PageIndex = 1, int PageSize = 15);
        int DeleteGalleryPhoto(GalleryPhoto GP, Guid UserGUID);
        GalleryPhoto GetGalleryPhotoByGuid(Guid GUID);
        long SaveGalleryPhoto(Gallery G, Guid UserGUID);
        long UpdateGalleryPhoto(GalleryPhoto GP, Guid UserGUID);
        int CheckGalleryTitle(string Title, Guid GUID);
        Gallery GetByTitle(string Title);
        IList<Gallery> GetList();

    }
}
