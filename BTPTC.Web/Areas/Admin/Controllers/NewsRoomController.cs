using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTPTC.Domain;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Interface;
using System.Data;
using System.IO;
using BTPTC.Web.Helpers;
using BTPTC.Web.Filters;

namespace BTPTC.Web.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [DisableCache]
    [HandleError]
    public class NewsRoomController : Controller
    {
        // GET: Admin/NewsRoom


        #region Constructor And Private Members

        private INewsLetterDao _newsLetterDao;
        private IMediaReleaseDao _mediaReleaseDao;
        private ITenderDao _tenderDao;
        private IUtilityService _utilityService;


        public NewsRoomController(ITenderDao tenderDao, INewsLetterDao newsLetterDao, IMediaReleaseDao mediaReleaseDao, IUtilityService utilityService)
        {
             _newsLetterDao = newsLetterDao;
             _mediaReleaseDao = mediaReleaseDao;
              _tenderDao = tenderDao;
             _utilityService = utilityService;
            
        }
        #endregion
        public ActionResult ViewNewsletter()
        {

            string Year = "";
            var lastSixMonths = Enumerable.Range(1, 3).Select(i => DateTime.Now.AddYears(i - 3).ToString("yyyy"));

            ViewBag.Years = lastSixMonths.ToList();

            ViewBag.SelectedYears = Year;

            //ViewBag.NewsTypse= Enumerable.Range(1, 6).Select(i => DateTime.Now.AddYears(-i).ToString("yyyy"));

            ViewBag.NewsType = new List<string>
           {
                "Press Release"
               ,"Blog"
           }.Select(S => new SelectListItem
           {
               Text = S,
               Value = S
           }).ToList();


            List<NewsLetter> facility = _newsLetterDao.Get(null, 1, 10);
            return View(facility);
        }


        public ActionResult AddNewsLetter()
        {
            string Year = "";
            var lastSixMonths = Enumerable.Range(1, 3).Select(i => DateTime.Now.AddYears(i - 3).ToString("yyyy"));

            ViewBag.Years = lastSixMonths.ToList();

            ViewBag.SelectedYears = Year;

            //ViewBag.NewsTypse= Enumerable.Range(1, 6).Select(i => DateTime.Now.AddYears(-i).ToString("yyyy"));

            ViewBag.NewsType = new List<string>
           {
                "Press Release"
               ,"Blog"
           }.Select(S => new SelectListItem
           {
               Text = S,
               Value = S
           }).ToList();



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewsLetter(NewsLetter NL)
        {


            if (NL.ThumbnailImage != null && NL.ThumbnailImage.ContentLength > 0)
            {
                NL.ThumbnailImageExtension = Path.GetExtension(NL.ThumbnailImage.FileName).Trim('.');
                NL.ThumbnailImageName = Path.GetFileNameWithoutExtension(NL.ThumbnailImage.FileName);
                NL.ThumbnailImageGUID = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/NewsLetter/" + NL.ThumbnailImageGUID + "." + NL.ThumbnailImageExtension);
                NL.ThumbnailImage.SaveAs(ThumbnailImagelocation);
            }

            if (NL.LargeImage != null && NL.LargeImage.ContentLength > 0)
            {
                NL.LargeImageExtension = Path.GetExtension(NL.LargeImage.FileName).Trim('.');
                NL.LargeImageName = Path.GetFileNameWithoutExtension(NL.LargeImage.FileName);
                NL.LargeImageGUID = Guid.NewGuid().ToString("N");
                string LargeImagelocation = Server.MapPath("~/Resources/Images/NewsLetter/" + NL.LargeImageGUID + "." + NL.LargeImageExtension);
                NL.LargeImage.SaveAs(LargeImagelocation);
            }

            if (NL.FileName != null && NL.FileName.ContentLength > 0)
            {
                NL.PdfFileExtension = Path.GetExtension(NL.FileName.FileName).Trim('.');
                NL.PdfFileName = Path.GetFileNameWithoutExtension(NL.FileName.FileName);
                NL.PdfFileGUID = Guid.NewGuid().ToString("N");
                string PdfFileLocation = Server.MapPath("~/Resources/Documents/" + NL.PdfFileGUID + "." + NL.PdfFileExtension);
                NL.FileName.SaveAs(PdfFileLocation);

                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double len = new FileInfo(PdfFileLocation).Length;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }
                string resultMB = String.Format("{0:0.##} {1}", len, sizes[order]);
                NL.FileSize = resultMB;

            }
            Int64 result = _newsLetterDao.SaveNewsLetter(NL);
            if(result>0)
            {
                return RedirectToRoute("AdminViewNewsletter");
            }

            return View();
        }

        public ActionResult PartialViewNewsletter(int PageIndex, string Year = null)
        {
            List<NewsLetter> newsLetter = _newsLetterDao.Get((!string.IsNullOrEmpty(Year) && Year != "All" ? Year : null), PageIndex, 10);
            return View(newsLetter);
        }

        [HttpGet]
        public ActionResult EditPartialNewsLetterGUID(string EncryptedId)
        {
            string Year = "";
            var lastSixMonths = Enumerable.Range(1, 3).Select(i => DateTime.Now.AddYears(i - 3).ToString("yyyy"));

            ViewBag.Years = lastSixMonths.ToList();

            ViewBag.SelectedYears = Year;

            //ViewBag.NewsTypse= Enumerable.Range(1, 6).Select(i => DateTime.Now.AddYears(-i).ToString("yyyy"));

            ViewBag.NewsType = new List<string>
           {
                "Press Release"
               ,"Blog"
           }.Select(S => new SelectListItem
           {
               Text = S,
               Value = S
           }).ToList();




            if (!string.IsNullOrEmpty(EncryptedId))
            {
                NewsLetter newsletter= _newsLetterDao.GetEditNewsLetterGUID(new Guid(EncryptedId));
                return PartialView(newsletter);
            }
            return View();
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNewsLetter(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
           
                Guid UserId = new Guid();
                Int64 Draft = _newsLetterDao.DeleteNewsLetter(GUID, UserId, SystemIp);
                return RedirectToRoute("AdminViewNewsletter", new { EncryptedId = string.Empty });
            }
            return View();
        }

        public JsonResult CheckNewsTitle(string NewsTitle, string EncDetail)
        {
            int result = CheckNewsTit(NewsTitle, EncDetail);
            return Json(result == 0, JsonRequestBehavior.AllowGet);
        }

        public int CheckNewsTit(string NewsTitle, string EncDetail)
        {
            string Title = (!string.IsNullOrEmpty(NewsTitle) ? NewsTitle : null);
            Guid GUID = !string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty;
            int result = _newsLetterDao.CheckNewsTitleName(Title, GUID);
            return result;
        }



        public ActionResult ViewMediaRelease()
        {
            List<MediaRelease> mediaRelease = _mediaReleaseDao.Get(null, 1, 10);
            return View(mediaRelease);
        }


      
        public ActionResult AddMediaRelease()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddViewMediaRelease(MediaRelease MR)
        {
            if (MR.FileName != null && MR.FileName.ContentLength > 0)
            {
                MR.PdfFileExtension = Path.GetExtension(MR.FileName.FileName).Trim('.');
                MR.PdfFileName = Path.GetFileNameWithoutExtension(MR.FileName.FileName);
                MR.PdfFileGUID = Guid.NewGuid().ToString("N");
                string PdfFileLocation = Server.MapPath("~/Resources/Documents/" + MR.PdfFileGUID + "." + MR.PdfFileExtension);
                MR.FileName.SaveAs(PdfFileLocation);

            }
            var result = _mediaReleaseDao.SaveMediaRelease(MR);
            if (result > 0)
            {
                return RedirectToRoute("AdminViewMediaRelease");
            }
            return View();
              
        }

       
       public ActionResult EditMediaRelease(string EncDetail)
        {
            if (!string.IsNullOrEmpty(EncDetail))
            {
                Guid GUID = new Guid(EncDetail);
                MediaRelease mediaRelease = _mediaReleaseDao.GetMediaReleaseByGuid(GUID);
                return View(mediaRelease);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMediaRelease(MediaRelease MR)
        {
            if (MR.FileName != null && MR.FileName.ContentLength > 0)
            {
                MR.PdfFileExtension = Path.GetExtension(MR.FileName.FileName).Trim('.');
                MR.PdfFileName = Path.GetFileNameWithoutExtension(MR.FileName.FileName);
                MR.PdfFileGUID = Guid.NewGuid().ToString("N");
                string PdfFileLocation = Server.MapPath("~/Resources/Documents/" + MR.PdfFileGUID + "." + MR.PdfFileExtension);
                MR.FileName.SaveAs(PdfFileLocation);

            }
            var result = _mediaReleaseDao.SaveMediaRelease(MR);
            if (result > 0)
            {
                return RedirectToRoute("AdminViewMediaRelease");
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMediaRelease(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                Guid UserId = new Guid();
                Int64 Draft = _mediaReleaseDao.DeleteMediaRelease(GUID, UserId, SystemIp);
                return RedirectToRoute("AdminViewMediaRelease", new { EncryptedId = string.Empty });
            }
            return View();
        }



        public JsonResult CheckMediaTitle(string NewsTitle, string EncDetail)
        {
            int result = MediaRelease(NewsTitle, EncDetail);
            return Json(result == 0, JsonRequestBehavior.AllowGet);
        }

        public int MediaRelease(string NewsTitle, string EncDetail)
        {
            string Title = (!string.IsNullOrEmpty(NewsTitle) ? NewsTitle : null);
            Guid GUID = !string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty;
            int result = _mediaReleaseDao.CheckMediaTitleName(Title, GUID);
            return result;
        }



        public ActionResult PartialViewMediaRelease(int PageIndex, string Year = null)
        {
            List<MediaRelease> newsLetter = _mediaReleaseDao.Get((!string.IsNullOrEmpty(Year) && Year != "All" ? Year : null), PageIndex, 10);
            return View(newsLetter);
        }




        public ActionResult ViewAnnualReport()
        {
            return View();
        }

        public ActionResult ViewTender()
        {
           // List<Tender> tender = _tenderDao.Get(null, 1, 10);
            return View();
        }

        //Notice
        #region "Tender Notice"
     

        [HttpGet]
        public ActionResult AddNotice()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNotice(Tender TR)
        {
            if (TR.FileName != null && TR.FileName.ContentLength > 0)
            {
                TR.PdfFileExtension = Path.GetExtension(TR.FileName.FileName).Trim('.');
                TR.PdfFileName = Path.GetFileNameWithoutExtension(TR.FileName.FileName);
                TR.PdfFileGUID = Guid.NewGuid().ToString("N");
                string PdfFileLocation = Server.MapPath("~/Resources/Documents/" + TR.PdfFileGUID + "." + TR.PdfFileExtension);
                TR.FileName.SaveAs(PdfFileLocation);

            }
            var result = _tenderDao.SaveTenderNotice(TR);
            if(result>0)
            {
                return RedirectToRoute("AdminViewTender");
            }
            return View();
        }

       
        public ActionResult EditNotice(string EncDetail)
        {

            if (!string.IsNullOrEmpty(EncDetail))
            {
                Guid GUID = new Guid(EncDetail);
                Tender mediaRelease = _tenderDao.GetTenderNoticeByGuid(GUID);
                return View(mediaRelease);
            }
            return View();
            
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTender(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                Guid UserId = new Guid();
                Int64 Draft = _tenderDao.DeleteTender(GUID, UserId, SystemIp);
                return RedirectToRoute("AdminViewTender", new { EncryptedId = string.Empty });
            }
            return View();
        }

        public JsonResult CheckTenderTitle(string NewsTitle, string EncDetail)
        {
            int result = TenderTitle(NewsTitle, EncDetail);
            return Json(result == 0, JsonRequestBehavior.AllowGet);
        }

        public int TenderTitle(string NewsTitle, string EncDetail)
        {
            string Title = (!string.IsNullOrEmpty(NewsTitle) ? NewsTitle : null);
            Guid GUID = !string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty;
            int result = _tenderDao.CheckTenderTitleName(Title, GUID);
            return result;
        }
        

        #endregion "Notice"


        public ActionResult AddResult()
        {
            return View();
        }

        public ActionResult EditResult(string EncDetail)
        {
            if (!string.IsNullOrEmpty(EncDetail))
            {
                Guid GUID = new Guid(EncDetail);
                Tender mediaRelease = _tenderDao.GetTenderNoticeByGuid(GUID);
                return View(mediaRelease);
            }
            return View();

        }




        public ActionResult PartialViewNotice(string FilterValue)
        {
            if (!string.IsNullOrEmpty(FilterValue))
            {
                List<Tender> mediaRelease = _tenderDao.GetTendersByStatus(FilterValue);
                return PartialView(mediaRelease);
                //return Json(mediaRelease, JsonRequestBehavior.AllowGet);
            }
            return View();
        }



        public ActionResult PartialResultView(string FilterValue)
        {

            if (!string.IsNullOrEmpty(FilterValue))
            {    
                List<Tender> mediaRelease = _tenderDao.GetTendersByStatus(FilterValue);
                return PartialView(mediaRelease);
                //return Json(mediaRelease, JsonRequestBehavior.AllowGet);
            }
            return View();

        }


        public ActionResult PartialAwardedView(string FilterValue)
        {

            if (!string.IsNullOrEmpty(FilterValue))
            {
                List<Tender> mediaRelease = _tenderDao.GetTendersByStatus(FilterValue);
                return PartialView(mediaRelease);
                //return Json(mediaRelease, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        public ActionResult AddAwarded()
        {
            return View();
        }


        public ActionResult EditAwarded(string EncDetail)
        {

            if (!string.IsNullOrEmpty(EncDetail))
            {
                Guid GUID = new Guid(EncDetail);
                Tender mediaRelease = _tenderDao.GetTenderNoticeByGuid(GUID);
                return View(mediaRelease);
            }
            return View();
        }





    }
}