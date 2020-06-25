using BTPTC.Core;
using BTPTC.Domain;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Interface;
using BTPTC.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BTPTC.Web.Controllers
{
    public class GalleryController : Controller
    {
        #region Private variables and constructors

        private IGalleryDao _galleryDao;
        //private IUtilityService _utilityService;
        private int _pageSize;

        public GalleryController(IGalleryDao galleryDao)
        {
            _galleryDao = galleryDao;
            //_utilityService = utilityService;
        }
        #endregion
        // GET: NewsRoom
        public ActionResult Index()
        {
            return View(_galleryDao.GetList());
        }
        
        public ActionResult Detail(string Title)
        {

            return View(_galleryDao.GetByTitle((!string.IsNullOrEmpty(Title))? Title :""));
        }
    }
}