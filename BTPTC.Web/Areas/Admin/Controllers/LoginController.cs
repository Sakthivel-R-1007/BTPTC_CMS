using BTPTC.Core;
using BTPTC.Domain;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Interface;
using BTPTC.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BTPTC.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        #region Constructor And Private Members

        private IUserAccountDao _userAccountDao;
        private IUtilityService _utilityService;

        public LoginController(IUserAccountDao userAccountDao, IUtilityService utilityService)
        {
            _userAccountDao = userAccountDao;
            _utilityService = utilityService;
        }

        #endregion
        // GET: Admin/Login
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserAccount UA)
        {
            if (UA != null)
            {
                #region Validation

                List<ValidationParam> loginParam = new List<ValidationParam>{
                        new ValidationParam{
                            PropertyName="UserName",
                            Value=UA.UserName,
                            Type=typeof(string),
                            Methodologies=new Dictionary<ValidationMethodology,string>{
                               {ValidationMethodology.Required,null}
                            }
                        },
                        new ValidationParam{
                            PropertyName="Password",
                            Value=UA.Password,
                            Type=typeof(string),
                            Methodologies=new Dictionary<ValidationMethodology,string>{
                               {ValidationMethodology.Required,null}
                            }
                        }
                };

                Validator.Validate(loginParam, ModelState);
                #endregion

                if (ModelState.IsValid)
                {
                    UA.SecurityCode = string.Empty;

                    UA.Password = Security.Encrypt<string>(UA.Password);

                    UA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                    string EncDetail = Security.Encrypt<string>(UA.UserName + "|" + UA.Password + "|" + new Guid().ToString());

                    UA = _userAccountDao.AuthenticateUser(UA);

                    if (UA != null && UA.SecurityCode == "BLOCKED")
                    {
                        ModelState.AddModelError("Id", "Account is locked");
                    }
                    else if (UA == null)
                    {
                        ModelState.AddModelError("Id", "Invalid username or password");
                    }
                    else if (UA != null)
                    {

                        UA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                        UA.LastLoginStatus = _userAccountDao.CheckLoginStatus(Guid.Empty, UA.GUID);

                        if (UA.LastLoginStatus)
                        {
                            ModelState.AddModelError("Id", "User Already having an open session.");
                            ViewBag.LogDetails = EncDetail;
                        }
                        else
                        {
                            Guid SessionId;

                            if (_userAccountDao.SaveUserLoginLog(UA, out SessionId) > 0)
                            {
                                UA.SessionId = SessionId;
                                Session["UserAccount"] = UA;
                                return RedirectToRoute("ViewFacilities");
                            }
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Id", "Invalid username or password");
                    }
                }
            }
            return View(UA);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForceLogin(string EncDetail)
        {

            if (!string.IsNullOrEmpty(EncDetail))
            {

                string[] encDetailSplitUps = Security.Decrypt<string>(EncDetail).Split('|');

                UserAccount UA = new UserAccount
                {
                    UserName = encDetailSplitUps[0],
                    Password = encDetailSplitUps[1],
                    SystemIp = GetRemoteIp.GetIPAddress(HttpContext)
                };

                UA = _userAccountDao.AuthenticateUser(UA);

                UA.LastLoginStatus = true;

                _userAccountDao.UpdateUserLoginLog(UA);

                if (UA != null && UA.SecurityCode == "BLOCKED")
                {
                    ModelState.AddModelError("Id", "Account is locked");
                }
                else if (UA == null)
                {
                    ModelState.AddModelError("Id", "Invalid username or password");
                }
                else if (UA != null)
                {
                    UA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                    UA.LastLoginStatus = _userAccountDao.CheckLoginStatus(Guid.Empty, UA.GUID);

                    if (UA.LastLoginStatus)
                    {
                        ModelState.AddModelError("Id", "User Already having an open session.");
                        ViewBag.LogDetails = EncDetail;
                    }
                    else
                    {
                        Guid SessionId;

                        if (_userAccountDao.SaveUserLoginLog(UA, out SessionId) > 0)
                        {
                            UA.SessionId = SessionId;
                            Session["UserAccount"] = UA;
                            return RedirectToRoute("ViewFacilities");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("Id", "Invalid username or password");
                }
            }

            return View("Index");
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPassword FP)
        {
            dynamic error = null; bool result = false;

            if (FP != null && FP.UserAccount != null && !string.IsNullOrEmpty(FP.UserAccount.Email))
            {
                FP.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                FP = _userAccountDao.SaveForgotPassword(FP);

                if (FP == null)
                {
                    error = "Please enter valid email.";
                }
                else
                {
                    StringBuilder contents = new StringBuilder();
                    FP.Key = Security.EncryptandEncodeUrl(FP.UniqueId + "_" + FP.UserAccount.GUID);
                    contents.Append(RenderRazorViewToString("_EDMForgotPassword", FP));

                    if (_utilityService.SendEmail("BTPTC - Reset Forgot Password", contents.ToString(), FP.UserAccount.Email, true, null) == "success")
                    {
                        result = true;
                    }
                    else
                    {
                        error = "Error occured. Please try again later";
                    }
                }
            }
            // return Json(new { Valid = (ModelState.IsValid), Success = result, Error = error }, JsonRequestBehavior.DenyGet);
            return Json(new { Valid = (ModelState.IsValid), Success = result, Error = error });
        }

      

        public ActionResult Logout()
        {
            if (Session["UserAccount"] != null)
            {
                UserAccount U = ((UserAccount)Session["UserAccount"]);

                if (U != null)
                {
                    U.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                    _userAccountDao.UpdateUserLoginLog(U);
                }
            }

            Session.Abandon();

            Response.Cookies.Clear();

            return Redirect("Admin");
        }


        #region Private methods

        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);

                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);

                viewResult.View.Render(viewContext, sw);

                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion
    }
}