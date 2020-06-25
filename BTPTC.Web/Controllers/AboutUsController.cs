using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTPTC.Web.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: AboutUs
        public ActionResult BTPTCIntro()
        {
            return View();
        }

        public ActionResult OurHistory()
        {
            return View();
        }
        public ActionResult RolesResponsibilities()
        {
            return View();
        }

        public ActionResult OurMps()
        {
            return View();
        }
        public ActionResult PersonalDataProtectionPolicy()
        {
            return View();
        }

        public ActionResult ByLaws()
        {
            return View();
        }


    }
}