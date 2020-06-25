using BTPTC.Core;
using BTPTC.Domain;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Interface;
using BTPTC.Web.Filters;
using BTPTC.Web.Helpers;
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
    public class AnnualReportController : Controller
    {
        // GET: Admin/AnnualReport
        #region Private variables and constructors

        private IAnnualReportDao _annualReportDao;
        //private IUtilityService _utilityService;
        private int _pageSize;

        public AnnualReportController(IAnnualReportDao annualReportDao)
        {
            _annualReportDao = annualReportDao;
            //_utilityService = utilityService;
        }
        #endregion
        public ActionResult Index()
        {
            return View(_annualReportDao.Get(1, 15));

        }
        public PartialViewResult PartialView(int PageIndex)
        {
            return PartialView(_annualReportDao.Get(PageIndex, 15));
        }
        [HttpGet]
        public ActionResult AddPartialView()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult EditPartialView(string EncDetail = null)
        {
       
            if (!string.IsNullOrEmpty(EncDetail))
            {
                AnnualReport AR = _annualReportDao.GetbyGuid(!string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty);
                return PartialView(AR);
            }

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AnnualReport AR, HttpPostedFileBase uploadAttach_1, HttpPostedFileBase uploadAttach_PDF_2)
        {
            if (AR != null)
            {
                string location = string.Empty;
                ValidateAnnualReport(AR, ModelState);
                if (ModelState.IsValid)
                {
                    if (uploadAttach_1 != null && uploadAttach_1.ContentLength > 0)
                    {
                        AR.ImageExtension = Path.GetExtension(uploadAttach_1.FileName).Trim('.');
                        AR.ImageName = Path.GetFileNameWithoutExtension(uploadAttach_1.FileName);
                        AR.ImageGUID = Guid.NewGuid().ToString("N");
                        location = Server.MapPath("~/Resources/Images/AnnualReport/" + AR.ImageGUID + "." + AR.ImageExtension);
                        uploadAttach_1.SaveAs(location);
                    }

                    if (uploadAttach_PDF_2 != null && uploadAttach_PDF_2.ContentLength > 0)
                    {
                        AR.PDFExtension = Path.GetExtension(uploadAttach_PDF_2.FileName).Trim('.');
                        AR.PDFName = Path.GetFileNameWithoutExtension(uploadAttach_PDF_2.FileName);
                        AR.PDFGUID = Guid.NewGuid().ToString("N");
                        AR.PDFFileSize = uploadAttach_PDF_2.ContentLength.ToString();
                        location = Server.MapPath("~/Resources/Documents/AnnualReport/" + AR.PDFGUID + "." + AR.PDFExtension);
                        uploadAttach_PDF_2.SaveAs(location);
                    }

                    string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                    AR.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                    if (_annualReportDao.Save(AR, new Guid(GUID)) > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string EncDetail)
        {
            if (!string.IsNullOrEmpty(EncDetail))
            {
                AnnualReport AR = new AnnualReport();
                AR.GUID = new Guid(EncDetail);
                string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
               _annualReportDao.Delete(AR, new Guid(GUID));
            }
            return RedirectToAction("Index");
        }


        #region Private Method
        public void ValidateAnnualReport(AnnualReport AR, ModelStateDictionary MSD)
        {
            List<ValidationParam> validationParam = new List<ValidationParam>{

                        new ValidationParam{
                            PropertyName="Title",
                            Value =AR.Title,
                            Type=typeof(string),
                            Methodologies = new Dictionary<ValidationMethodology,string> {
                                {ValidationMethodology.Required,null}
                            }
                        },
                        new ValidationParam{
                            PropertyName="ImageAltTag",
                            Value =AR.ImageAltTag,
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