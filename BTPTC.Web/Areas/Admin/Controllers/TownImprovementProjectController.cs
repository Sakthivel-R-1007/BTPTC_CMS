using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTPTC.Domain;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Interface;
using BTPTC.Web.Filters;
using BTPTC.Web.Helpers;

namespace BTPTC.Web.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [DisableCache]
    [HandleError]
    public class TownImprovementProjectController : Controller
    {
        // GET: Admin/TownImprovementProject

        #region Constructor And Private Members

        private INoticeDao _townImpornmentProjectDao;
        private IUtilityService _utilityService;

        public TownImprovementProjectController(INoticeDao eventDao, IUtilityService utilityService)
        {
            _townImpornmentProjectDao = eventDao;
            _utilityService = utilityService;
        }

        #endregion




    
       public ActionResult ViewTownImprovenmentProject()
        {
            List<Notice> getnotice = _townImpornmentProjectDao.GetNotice(null, 1, 10);
            return View(getnotice);
        }


        //public ActionResult TownImprovementProject()
        //{
        //    List<Notice> getnotice = _townImpornmentProjectDao.GetNotice(null, 1, 10);
        //    return View(getnotice);

        //}

        public ActionResult ViewTownImprovement()
        {
            List<Notice> getnotice = _townImpornmentProjectDao.GetNotice(null, 1, 10);
            return View(getnotice);

        }



        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(Notice EA)
        {
            if (EA != null)
            {

                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                EA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                // EA.UserId = UserId;
                Int64 result = _townImpornmentProjectDao.saveNotice(EA);
                if (result > 0)
                {
                    return RedirectToRoute("ViewTownProject1");
                }

            }

            return View();
        }
   

        //public ActionResult EditTownImprovenmentProject(Guid id)
        //{
        //   Notice getnotice = _townImpornmentProjectDao.GetEditTownProject(id);
        //    return View(getnotice);

        //}

        public ActionResult EditProject(Guid id)
        {
            Notice getnotice = _townImpornmentProjectDao.GetEditTownProject(id);
            return View(getnotice);

        }







        [HttpGet]
        public ActionResult EditTownProjectNoticePopUp(string EncryptedId)
        {
            if (EncryptedId != null)
            {
                Notice Facility = _townImpornmentProjectDao.GetEditTownProjectPopupNotice(new Guid(EncryptedId));

                return Json(Facility, JsonRequestBehavior.AllowGet);
            }
            return Json(new Facility(), JsonRequestBehavior.AllowGet);

        }


        public ActionResult Index()
        {
            List<Notice> getnotice = _townImpornmentProjectDao.GetNoticeProject(null, 1, 10);

            IEnumerable<Notice> Solutions = _townImpornmentProjectDao.GetNoticeDropDowm();

            ViewBag.Solution = (Solutions == null) ? new List<SelectListItem>() : Solutions.Select(S => new SelectListItem
            {
                Text = S.NoticeType,
                Value = S.NoticeType
                //Value = S.GUID.ToString(),
                // Selected = (!string.IsNullOrEmpty(solution) && new Guid(solution) == S.GUID),
            }).ToList();
            return View(getnotice);

        }

        public ActionResult PartialViewNoticeProject(int PageIndex, string Year = null)
        {
            List<Notice> Notice = _townImpornmentProjectDao.GetNoticeProject((!string.IsNullOrEmpty(Year) && Year != "All" ? Year : null), PageIndex, 10);
            return View(Notice);
        }

        public ActionResult ViewType()
        {
            List<Notice> getnotice = _townImpornmentProjectDao.GetNotice(null, 1, 10);
            return View(getnotice);
            
        }


        public ActionResult PartialView(int PageIndex, string Year = null)
        {
            List<Notice> Notice = _townImpornmentProjectDao.GetNotice((!string.IsNullOrEmpty(Year) && Year != "All" ? Year : null), PageIndex, 10);
            return View(Notice);
        }


       [HttpGet]
        public JsonResult CheckNoticeTitle(string NoticeType, string EncDetail)
        {
            int result = CheckNoticeType(NoticeType, EncDetail);
            return Json(result == 0, JsonRequestBehavior.AllowGet);
        }

        public int CheckNoticeType(string NewsTitle, string EncDetail)
        {
            string Title = (!string.IsNullOrEmpty(NewsTitle) ? NewsTitle : null);
            Guid GUID = !string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty;
            int result = _townImpornmentProjectDao.CheckNoticeType(Title, GUID);
            return result;
        }



        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddNoticeType(Notice EA, string EncryptedId)
        {
            if (EA != null)
            {

                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                EA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                // EA.UserId = UserId;
                Int64 result = _townImpornmentProjectDao.saveNotice(EA);
                if (result > 0)
                {
                    return RedirectToRoute("ViewType", new { EncryptedId = string.Empty });
                }
               
            }

            return View();
        }



        [HttpGet]
        public ActionResult EditNoticeProject(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Notice eventNotice = _townImpornmentProjectDao.GetEditNotice(new Guid(EncryptedId));
                return PartialView(eventNotice);
            }
            return View();
        }




        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditNoticeType(Notice EA, string EncryptedId)
        {
            if (EA != null)
            {

                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                EA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                // EA.UserId = UserId;
                Int64 result = _townImpornmentProjectDao.saveNotice(EA);
                if (result > 0)
                {
                    return RedirectToRoute("ViewType", new { EncryptedId = string.Empty });
                }
                else
                {
                    ViewBag.errormsg = "Notice Type Name Already Exists";
                    return RedirectToRoute("ViewType", new { EncryptedId = string.Empty });
                }
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNotice(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                //  Int64 Draft = _eventDao.DeleteEvent(GUID, UserId, SystemIp);
                Guid UserId = new Guid();
                Int64 Draft = _townImpornmentProjectDao.DeleteNotice(GUID, UserId, SystemIp);
                return RedirectToRoute("ViewTownProject1", new { EncryptedId = string.Empty });
            }
            return View();
        }




        //Add notice project

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddNoticeProject(Notice EA, string EncryptedId)
        {
            if (EA != null)
            {

                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                EA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                // EA.UserId = UserId;
                Int64 result = _townImpornmentProjectDao.saveNoticeProject(EA);
                if (result > 0)
                {
                    return RedirectToRoute("ViewNoticeProject", new { EncryptedId = string.Empty });
                }
              
            }

            return View();
        }



        [HttpGet]
        public ActionResult GetEditNoticeProject(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Notice eventNotice = _townImpornmentProjectDao.GetEditNoticeProject(new Guid(EncryptedId));

                IEnumerable<Notice> Solutions = _townImpornmentProjectDao.GetNoticeDropDowm();

                ViewBag.Solution = (Solutions == null) ? new List<SelectListItem>() : Solutions.Select(S => new SelectListItem
                {
                    Text = S.NoticeType,
                    Value = S.NoticeType
                    //Value = S.GUID.ToString(),
                    // Selected = (!string.IsNullOrEmpty(solution) && new Guid(solution) == S.GUID),
                }).ToList();

                return PartialView(eventNotice);
               // return PartialView("_PartialEditNoticeProject", eventNotice);
            }
           
            return View();
        }



        [HttpPost, ValidateInput(false)]
       [ValidateAntiForgeryToken]
        public ActionResult EditNoticeProject(Notice EA, string EncryptedId)
        {
          
            if (EA != null)
            {
                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                EA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                // EA.UserId = UserId;
                Int64 result = _townImpornmentProjectDao.saveNoticeProject(EA);
                if (result > 0)
                {
                return RedirectToRoute("ViewNoticeProject", new { EncryptedId = string.Empty });
                }
               else
                {
                  
                return RedirectToRoute("ViewNoticeProject", new { EncryptedId = string.Empty });
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNoticeProject(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                //  Int64 Draft = _eventDao.DeleteEvent(GUID, UserId, SystemIp);
                Guid UserId = new Guid();
                Int64 Draft = _townImpornmentProjectDao.DeleteNoticeProject(GUID, UserId, SystemIp);
                return RedirectToRoute("ViewNoticeProject", new { EncryptedId = string.Empty });
            }
            return View();
        }



    }
}