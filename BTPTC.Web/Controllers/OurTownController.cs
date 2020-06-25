using BTPTC.Domain;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTPTC.Web.Controllers
{
    public class OurTownController : Controller
    {
        #region Constructor And Private Members

        private IFacilityDao _facilityDao;
        private IEventsDao _eventDao;
        private IMaintenanceDao _maintenanceDao;
        private IUtilityService _utilityService;

        public OurTownController(IMaintenanceDao maintenanceDao, IFacilityDao facilityDao, IEventsDao eventDao, IUtilityService utilityService)
        {
            _facilityDao = facilityDao;
            _eventDao = eventDao;
            _maintenanceDao = maintenanceDao;
            _utilityService = utilityService;
        }
        #endregion
        // GET: OurTown

        public ActionResult ViewFacility()
        {

            List<Facility> facility = _facilityDao.GetViewFacility();
            return View(facility);

        }
        public ActionResult TownImprovementProject()
        {

            return View();
        }


        public ActionResult Maintenance()
        {
            List<MaintenanceSchedule> facility = _maintenanceDao.GetViewMaintenance();
            ViewBag.streetList = facility.Select(m => m.StreetName).Distinct();
            return View(facility);
        }



        public ActionResult OurEvents()
        {
            EventContents events = _eventDao.GetViewEvents();
            return View(events);
        }


        public ActionResult EventDetails(Guid guid)
        {
            Events events = _eventDao.GetEventDetails(guid);
            return View(events);
        }

      
    }
}