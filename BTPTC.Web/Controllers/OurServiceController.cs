using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTPTC.Web.Controllers
{
    public class OurServiceController : Controller
    {
        // GET: OurService
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ServiceConservancyCharges()
        {
            return View();
        }
        public ActionResult BookingRates()
        {
            return View();
        }
        public ActionResult HandymanServices()
        {
            return View();
        }
        public ActionResult ModesOfPayment()
        {
            return View();
        }
    }
}