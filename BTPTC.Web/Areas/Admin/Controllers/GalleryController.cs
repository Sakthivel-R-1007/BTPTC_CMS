using BTPTC.Core;
using BTPTC.Domain;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Interface;
using BTPTC.Web.Helpers;
using BTPTC.Web.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BTPTC.Web.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [DisableCache]
    [HandleError]
    public class GalleryController : Controller
    {
        // GET: Admin/Gallery
        #region Constructor And Private Members

        private IGalleryDao _galleryDao;
        private IUtilityService _utilityService;

        public GalleryController(IGalleryDao galleryDao, IUtilityService utilityService)
        {
            _galleryDao = galleryDao;
            _utilityService = utilityService;
        }

        #endregion
        public ActionResult Index()
        {
            return View(_galleryDao.Get(1, 15));
        }


        public PartialViewResult PartialView(int PageIndex)
        {
            return PartialView(_galleryDao.Get(PageIndex, 15));
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(Gallery G)
        {
            if (G != null)
            {
                string location = string.Empty;
                ValidateGallery(G, ModelState);
                if (ModelState.IsValid)
                {
                    DateTime Date;
                    DateTime.TryParseExact(G.DateString, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out Date);
                    G.Date = Date;
                    string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                    G.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                    if (_galleryDao.Save(G, new Guid(GUID)) > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(G);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string EncDetail)
        {
            if (!string.IsNullOrEmpty(EncDetail))
            {
                Gallery G = new Gallery();
                G.GUID = new Guid(EncDetail);
                string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                _galleryDao.Delete(G, new Guid(GUID));

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(Guid EncDetail)
        {
            Gallery G = _galleryDao.GetbyGuid(EncDetail, 1, 15);
            return View(G);
        }

        public PartialViewResult GalleryPhotoPartialView(string EncDetail, int PageIndex)
        {
            Gallery G = _galleryDao.GetbyGuid(!string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty, PageIndex, 15);
            return PartialView(G);
        }

        [HttpGet]
        public ActionResult AddGalleryPhotoPartialView(string EncDetail = null)
        {
            Gallery G = new Gallery();
            G.GUID = !string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty;

            return PartialView(G);
        }

        [HttpGet]
        public ActionResult EditGalleryPhotoPartialView(string EncDetail = null, string EncGuid = null)
        {
            GalleryPhoto GP = _galleryDao.GetGalleryPhotoByGuid(!string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty);
            GP.GalleryGuid = !string.IsNullOrEmpty(EncGuid) ? new Guid(EncGuid) : Guid.Empty;

            return PartialView(GP);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddGalleryPhotoPartialView(Gallery G, HttpPostedFileBase[] uploadAttach_1)
        {
            List<GalleryPhoto> galleryPhotos = new List<GalleryPhoto>();
            foreach (HttpPostedFileBase Photo in uploadAttach_1)
            {
                if (Photo != null)
                {
                    string location = string.Empty;

                    GalleryPhoto galleryPhoto = new GalleryPhoto()
                    {
                        ImageExtension = Path.GetExtension(Photo.FileName).Trim('.'),
                        ImageName = Path.GetFileNameWithoutExtension(Photo.FileName),
                        ImageGUID = Guid.NewGuid().ToString("N")
                    };
                    galleryPhotos.Add(galleryPhoto);

                    location = Server.MapPath("~/Resources/Images/Gallery/" + galleryPhoto.ImageGUID + "." + galleryPhoto.ImageExtension);
                    Photo.SaveAs(location);

                }
            }
            G.galleryPhotos = galleryPhotos;
            string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
            G.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            _galleryDao.SaveGalleryPhoto(G, new Guid(GUID));

            return RedirectToAction("Edit", new { EncDetail = G.GUID });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGalleryPhotoPartialView(GalleryPhoto GP, HttpPostedFileBase uploadAttach_2)
        {
            if (GP != null)
            {
                string location = string.Empty;
                //ValidateCustomerLogo(CL, ModelState);
                //if (ModelState.IsValid)
                //{
                if (uploadAttach_2 != null && uploadAttach_2.ContentLength > 0)
                {
                    GP.ImageExtension = Path.GetExtension(uploadAttach_2.FileName).Trim('.');
                    GP.ImageName = Path.GetFileNameWithoutExtension(uploadAttach_2.FileName);
                    GP.ImageGUID = Guid.NewGuid().ToString("N");
                    location = Server.MapPath("~/Resources/Images/Gallery/" + GP.ImageGUID + "." + GP.ImageExtension);
                    uploadAttach_2.SaveAs(location);
                }

                string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                GP.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                _galleryDao.UpdateGalleryPhoto(GP, new Guid(GUID));


                // }
            }

            return RedirectToAction("Edit", new { EncDetail = GP.GalleryGuid });
        }

        public ActionResult CheckGalleryTitle(string Title, string EncDetail)
        {
            int result = CheckTitle(Title, EncDetail);
            return Json(result == 0, JsonRequestBehavior.AllowGet);
        }

        public int CheckTitle(string Title, string EncDetail)
        {
            string TitleName = (!string.IsNullOrEmpty(Title) ? Title : null);
            Guid GUID = !string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty;
            int result = _galleryDao.CheckGalleryTitle(TitleName, GUID);
            return result;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGalleryPhoto(string EncDetail)
        {
            if (!string.IsNullOrEmpty(EncDetail))
            {
                String[] EncCode = EncDetail.Split('_');
                GalleryPhoto GP = new GalleryPhoto();
                GP.GUID = new Guid(EncCode[0]);
                string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                _galleryDao.DeleteGalleryPhoto(GP, new Guid(GUID));

                return RedirectToAction("Edit", new { EncDetail = !string.IsNullOrEmpty(EncCode[1]) ? new Guid(EncCode[1]) : Guid.Empty });
            }
            return RedirectToAction("Index");

        }


        #region Private Method

        public void ValidateGallery(Gallery G, ModelStateDictionary MSD)
        {
            List<ValidationParam> validationParam = new List<ValidationParam>{

                       new ValidationParam{
                            PropertyName="Title",
                            Value =G.Title,
                            Type=typeof(string),
                            Methodologies = new Dictionary<ValidationMethodology,string> {
                                {ValidationMethodology.Required,null}
                            }
                        },
                        new ValidationParam{
                            PropertyName="DateString",
                            Value =G.DateString,
                            Type=typeof(string),
                            Methodologies = new Dictionary<ValidationMethodology,string> {
                                {ValidationMethodology.Required,null}
                            }
                        },
                        new ValidationParam{
                            PropertyName="ShortDescription",
                            Value =G.ShortDescription,
                            Type=typeof(string),
                            Methodologies = new Dictionary<ValidationMethodology,string> {
                                {ValidationMethodology.Required,null}
                            }
                        }

            };

            Validator.Validate(validationParam, MSD);
        }


        #endregion
    }
}