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
    public class NewsRoomController : Controller
    {
        #region Private variables and constructors

        private IAnnualReportDao _annualReportDao;
        //private IUtilityService _utilityService;
        private int _pageSize;

        public NewsRoomController(IAnnualReportDao annualReportDao)
        {
            _annualReportDao = annualReportDao;
            //_utilityService = utilityService;
        }
        #endregion
        // GET: NewsRoom
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnnualReport()
        {
            return View(_annualReportDao.Get(0, 0));

        }


    }
}