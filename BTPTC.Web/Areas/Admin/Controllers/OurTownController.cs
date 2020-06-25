using BTPTC.Domain;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Interface;
using BTPTC.Web.Filters;
using BTPTC.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BTPTC.Web.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [DisableCache]
    [HandleError]
    public class OurTownController : Controller
    {
        // GET: Admin/OurTown
        #region Constructor And Private Members

        private IFacilityDao _facilityDao;
        private IMaintenanceDao _maintenanceDao;
        private IUtilityService _utilityService;
        private IEventsDao _eventDao;

        public OurTownController(IFacilityDao facilityDao, IMaintenanceDao maintenanceDao, IUtilityService utilityService, IEventsDao eventsDao)
        {
            _facilityDao = facilityDao;
            _maintenanceDao = maintenanceDao;
            _utilityService = utilityService;
            _eventDao = eventsDao;
        }

        #endregion
        public ActionResult Facilities()
        {
            List<Facility> facility = _facilityDao.Get(null, 1, 10);
            return View(facility);
        }

        public ActionResult AddFacility()
        {
            return View();
        }



       
        public ActionResult Maintenance()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Maintenance(HttpPostedFileBase uploadAttach_1)
        {
            string location = string.Empty;
            string Extension = string.Empty;
            string FileName = string.Empty;
            string FileGuid = string.Empty;
            List<TemplateUploadError> TUE = null;
            if (uploadAttach_1 != null && uploadAttach_1.ContentLength > 0)
            {
                Extension = Path.GetExtension(uploadAttach_1.FileName).Trim('.');
                FileName = Path.GetFileNameWithoutExtension(uploadAttach_1.FileName);
                FileGuid = Guid.NewGuid().ToString("N");
                location = Server.MapPath("~/Resources/Documents/Maintenance/" + FileGuid + "." + Extension);
                uploadAttach_1.SaveAs(location);
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                string File = FileGuid + "." + Extension;
                string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                Int64 ExcelId = _maintenanceDao.SaveExcelLog(File, FileName, SystemIp, new Guid(GUID));

                IList<MaintenanceSchedule> maintenances = ReadExcel(location, Extension, out TUE);
                if (TUE == null || (TUE != null && TUE.Count() == 0))
                {
                    if (maintenances != null && maintenances.Count > 0)
                    {
                        int result = _maintenanceDao.SaveMaintenance(maintenances, ExcelId, SystemIp, new Guid(GUID));
                        ViewBag.Success = "Maintenance Schedule Successfully";
                    }
                    else
                    {
                        TUE.Add(new TemplateUploadError { Line = 1, Column = 0, Message = "please upload valid excel." });
                    }
                }

            }
            ViewBag.Error = TUE;
            return View();
        }
        //[HttpPost]
        //public ActionResult Maintenance(MaintenanceSchedule MS)
        //{
        //    List<string> myList = new List<string>();

        //    ErrorMsg errormsg = new ErrorMsg();
        //    List<ErrorMsg> listErrorMsg = new List<ErrorMsg>();
        //    string tableColumnName = "";
        //    ViewData["ErrorMsg"] = "";

        //    if (Request.Files.Count > 0)
        //    {
        //        HttpPostedFileBase file = Request.Files[0];
        //        string fileName = Path.GetFileName(file.FileName);
        //        MS.FileGUID = Guid.NewGuid().ToString("N");
        //        MS.FileName = Path.GetFileName(file.FileName);
        //        string path = Path.Combine(Server.MapPath("~/Resources/Filepath/" + MS.FileGUID + "_" + fileName));
        //        file.SaveAs(path);



        //        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //        Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
        //        Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //        Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;


        //        int rowCount = xlRange.Rows.Count;
        //        int colCount = xlRange.Columns.Count;
        //        System.Data.DataTable MaintenanceTbl = new System.Data.DataTable("MaintenanceTbl");
        //        MaintenanceTbl.Columns.Add("Division", typeof(string));
        //        MaintenanceTbl.Columns.Add("BlkNo", typeof(string));
        //        MaintenanceTbl.Columns.Add("StreetName", typeof(string));
        //        MaintenanceTbl.Columns.Add("RC Zone", typeof(string));
        //        MaintenanceTbl.Columns.Add("Block Washing", typeof(string));
        //        MaintenanceTbl.Columns.Add("Bin Chute Fogging", typeof(string));
        //        MaintenanceTbl.Columns.Add("Bin Chute Flushing (Weekly)", typeof(string));
        //        MaintenanceTbl.Columns.Add("Lift Maintenance (Monthly)", typeof(string));
        //        int r = 0;
        //        Int64 resultErrorLog = _Maintenance.saveMaintenance(MaintenanceTbl, MS);


        //        //check columncount
        //        if (colCount != MaintenanceTbl.Columns.Count)
        //        {
        //            errormsg = new ErrorMsg();

        //            errormsg.msg = "column count  MisMatch";
        //            listErrorMsg.Add(errormsg);

        //        }


        //        if (listErrorMsg.Count > 0)
        //        {

        //            ViewData["ErrorMsg"] = listErrorMsg;
        //            return View("Maintenance");
        //        }

        //        //check column value mismatch 


        //        for (int k = 1; k <= colCount; k++)
        //        {

        //            var cellValue = (xlWorksheet.Cells[k] as Microsoft.Office.Interop.Excel.Range).Value;
        //            if (k <= 8)
        //            {
        //                tableColumnName = MaintenanceTbl.Columns[r++].ColumnName;

        //                if (cellValue != tableColumnName)
        //                {
        //                    errormsg = new ErrorMsg();
        //                    errormsg.Row = cellValue;
        //                    errormsg.msg = r + "Column" + " " + tableColumnName + " " + "mismatch";
        //                    listErrorMsg.Add(errormsg);
        //                }
        //            }
        //            else
        //            {
        //                errormsg = new ErrorMsg();
        //                errormsg.Row = cellValue;
        //                errormsg.msg = r + "Column" + " " + tableColumnName + " " + "mismatch";
        //                listErrorMsg.Add(errormsg);

        //            }
        //        }





        //        if (listErrorMsg.Count > 0)
        //        {

        //            ViewData["ErrorMsg"] = listErrorMsg;
        //            return View("Maintenance");
        //        }

        //        //// check row value Empty
        //        for (int i = 2; i <= rowCount; i++)
        //        {
        //            DataRow dr1 = MaintenanceTbl.NewRow();
        //            for (int j = 1; j <= 8; j++)
        //            {
        //                errormsg = new ErrorMsg();
        //                // var columnName = (xlWorksheet.Cells[j] as Microsoft.Office.Interop.Excel.Range).Value;
        //                var cellValue = (xlWorksheet.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Value;
        //                //if (cellValue == null)
        //                //{
        //                //    errormsg.Row = i.ToString()  ;
        //                //    errormsg.msg = columnName + " " + "value Requried";

        //                //    listErrorMsg.Add(errormsg);
        //                //}
        //                //else
        //                //{
        //                dr1[j - 1] = cellValue;
        //                // }
        //            }
        //            MaintenanceTbl.Rows.Add(dr1);
        //        }


        //        if (MaintenanceTbl.Rows.Count > 0)
        //        {
        //            int K = 0;
        //            foreach (DataRow x in MaintenanceTbl.Rows)
        //            {
        //                int R = 0;
        //                foreach (var item in x.ItemArray)
        //                {
        //                    errormsg = new ErrorMsg();
        //                    if (string.IsNullOrWhiteSpace(item.ToString()))
        //                    {
        //                        errormsg.Row = K.ToString();
        //                        errormsg.msg = x.Table.Columns[R].ColumnName + " " + "value Requried";
        //                        listErrorMsg.Add(errormsg);
        //                    }
        //                    R++;
        //                }
        //                K++;
        //            }

        //            if (listErrorMsg.Count > 0)
        //            {
        //                ViewData["ErrorMsg"] = listErrorMsg;
        //                return View("Maintenance");
        //            }
        //        }
        //        else
        //        {
        //            errormsg.msg = "Excel value Empty";
        //            listErrorMsg.Add(errormsg);
        //            ViewData["ErrorMsg"] = listErrorMsg;
        //            return View("Maintenance");
        //        }

        //        Int64 result = _Maintenance.saveMaintenance(MaintenanceTbl, MS);
        //        if (result > 0)
        //        {
        //            ViewData["ErrorMsg"] = "Upload successfully";
        //            return View("Maintenance");

        //        }
        //    }

        //    return View();
        //}


        [HttpGet]
        public JsonResult CheckFacilityTitle(string Name, string EncDetail)
        {
            int result = CheckFacilityType(Name, EncDetail);
            return Json(result == 0, JsonRequestBehavior.AllowGet);
        }


        public int CheckFacilityType(string NewsTitle, string EncDetail)
        {
            string Title = (!string.IsNullOrEmpty(NewsTitle) ? NewsTitle : null);
            Guid GUID = !string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty;
            int result = _facilityDao.CheckFacilityType(Title, GUID);
            return result;
        }



        [HttpPost]
        public ActionResult AddFacility(Facility F)
        {
            F.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            Int64 result = _facilityDao.saveFacility(F);
            if (result > 0)
            {
                return RedirectToRoute("ViewFacility");
            }

            return View();
        }



        [HttpPost]
        public ActionResult EditFacility(Facility F)
        {

            F.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            Int64 result = _facilityDao.EditSaveFacility(F);
            if (result > 0)
            {
                return RedirectToRoute("ViewFacility");
            }


            return View();
        }





        public ActionResult PartialViewFacility(int PageIndex, string Year = null)
        {
            List<Facility> Notice = _facilityDao.Get((!string.IsNullOrEmpty(Year) && Year != "All" ? Year : null), PageIndex, 10);
            return View(Notice);
        }



        [HttpPost, ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public JsonResult AddFacilityList(List<Facility> json, string facility)
        {
            if (json != null)
            {

                foreach (var EA in json)
                {
                    if (EA.Imagepath != null)
                    {
                        EA.ImageExtension = Path.GetExtension(EA.Imagepath).Trim('.');
                        EA.ImageName = Path.GetFileNameWithoutExtension(EA.Imagepath);
                        EA.ImageGUID = Guid.NewGuid().ToString("N");

                    }

                    EA.Name = facility;
                    //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                    EA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                    // EA.UserId = UserId;
                    Int64 result = _facilityDao.saveFacility(EA);
                    // }
                }
            }
            return Json(null);
        }



        [HttpPost, ValidateInput(false)]
        // [ValidateAntiForgeryToken]
        public ActionResult AddPhotoFacility(Facility EA, string EncryptedId)
        {
            //if (EA != null)
            //{
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase LargeImage = Request.Files[0];

                if (LargeImage != null && LargeImage.ContentLength > 0)
                {
                    EA.ImageExtension = Path.GetExtension(LargeImage.FileName).Trim('.');
                    EA.ImageName = Path.GetFileNameWithoutExtension(LargeImage.FileName);
                    if (System.IO.File.Exists(Server.MapPath("~/Resources/Images/Facility/" + EA.ImageGUID + "." + EA.ImageExtension)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Resources/Images/Facility/" + EA.ImageGUID + "." + EA.ImageExtension));
                        EA.LargeImage.SaveAs(Server.MapPath("~/Resources/Images/Facility/" + EA.ImageGUID + "." + EA.ImageExtension));
                    }
                    else
                    {
                        EA.ImageGUID = Guid.NewGuid().ToString("N");
                        string LargeImagelocation = Server.MapPath("~/Resources/Images/Facility/" + EA.ImageGUID + "." + EA.ImageExtension);
                        EA.LargeImage.SaveAs(LargeImagelocation);
                        EA.Imagepath = "/Resources/Images/Facility/" + EA.ImageGUID + "." + EA.ImageExtension;
                    }
                }
            }

            EA.ModifiedOn = DateTime.Now;


            EA.LargeImage = null;
            //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
            EA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            // EA.UserId = UserId;
            //  Int64 result = _eventDao.saveEvents(EA);
            // }

            return Json(EA);
            // return JsonConvert.SerializeObject(EA);
        }


        public ActionResult EditFacility(Guid id)
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetEditFacility(Guid id)
        {
            if (id != null)
            {
                List<Facility> Facility = _facilityDao.GetEditFacility(id);
                return Json(Facility, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<Facility>(), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetFaciltyImage(string EncryptedId)
        {
            if (EncryptedId != null)
            {
                Facility Facility = _facilityDao.GetEditFacilityImage(new Guid(EncryptedId));

                return Json(Facility, JsonRequestBehavior.AllowGet);
            }

            return Json(new Facility(), JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFacility(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                //  Int64 Draft = _eventDao.DeleteEvent(GUID, UserId, SystemIp);
                Guid UserId = new Guid();
                Int64 Draft = _facilityDao.DeleteFacility(GUID, UserId, SystemIp);
                return RedirectToRoute("ViewFacility", new { EncryptedId = string.Empty });
            }
            return View();
        }






        public ActionResult ViewEvents()
        {
            List<Events> eventContent = _eventDao.Get(null, 1, 10);
            return View(eventContent);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewEvents(string FilterYear = null)
        {
            List<Events> eventContent = _eventDao.Get((!string.IsNullOrEmpty(FilterYear) && FilterYear != "All" ? FilterYear : null), 1, 10);
            return View(eventContent);
        }

        public ActionResult PartialView(int PageIndex, string Year = null)
        {
            List<Events> eventContent = _eventDao.Get((!string.IsNullOrEmpty(Year) && Year != "All" ? Year : null), PageIndex, 10);
            return View(eventContent);
        }
        public ActionResult AddEvents(string EncryptedId)
        {
            Guid GUID = Guid.Empty;
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                GUID = new Guid(EncryptedId);
            }

            return View();
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddEvents(Events EA, string EncryptedId)
        {
            if (EA != null)
            {
                DateTime SDate;
                DateTime.TryParseExact(EA.FromDateString, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out SDate);
                EA.EventDate = SDate;
                if (EA.ThumbnailImage != null && EA.ThumbnailImage.ContentLength > 0)
                {
                    EA.ThumbnailImageExtension = Path.GetExtension(EA.ThumbnailImage.FileName).Trim('.');
                    EA.ThumbnailImageName = Path.GetFileNameWithoutExtension(EA.ThumbnailImage.FileName);
                    EA.ThumbnailImageGUID = Guid.NewGuid().ToString("N");
                    string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/Events/" + EA.ThumbnailImageGUID + "." + EA.ThumbnailImageExtension);
                    EA.ThumbnailImage.SaveAs(ThumbnailImagelocation);
                }

                if (EA.LargeImage != null && EA.LargeImage.ContentLength > 0)
                {
                    EA.LargeImageExtension = Path.GetExtension(EA.LargeImage.FileName).Trim('.');
                    EA.LargeImageName = Path.GetFileNameWithoutExtension(EA.LargeImage.FileName);
                    EA.LargeImageGUID = Guid.NewGuid().ToString("N");
                    string LargeImagelocation = Server.MapPath("~/Resources/Images/Events/" + EA.LargeImageGUID + "." + EA.LargeImageExtension);
                    EA.LargeImage.SaveAs(LargeImagelocation);
                }

                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                EA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                // EA.UserId = UserId;
                Int64 result = _eventDao.saveEvents(EA);
                if (result > 0)
                {
                    return RedirectToRoute("ViewEvents");
                }
            }



            return View();
        }



        [HttpGet]
        public ActionResult EditEvents(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Events eventArticle = _eventDao.GetEditEvent(new Guid(EncryptedId));
                return View(eventArticle);
            }

            return View();
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvents(Events EA)
        {
            if (EA != null)
            {

                DateTime SDate;
                DateTime.TryParseExact(EA.FromDateString, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out SDate);
                EA.EventDate = SDate;
                if (EA.ThumbnailImage != null && EA.ThumbnailImage.ContentLength > 0)
                {
                    EA.ThumbnailImageExtension = Path.GetExtension(EA.ThumbnailImage.FileName).Trim('.');
                    EA.ThumbnailImageName = Path.GetFileNameWithoutExtension(EA.ThumbnailImage.FileName);
                    EA.ThumbnailImageGUID = Guid.NewGuid().ToString("N");
                    string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/Events/" + EA.ThumbnailImageGUID + "." + EA.ThumbnailImageExtension);
                    EA.ThumbnailImage.SaveAs(ThumbnailImagelocation);
                }

                if (EA.LargeImage != null && EA.LargeImage.ContentLength > 0)
                {
                    EA.LargeImageExtension = Path.GetExtension(EA.LargeImage.FileName).Trim('.');
                    EA.LargeImageName = Path.GetFileNameWithoutExtension(EA.LargeImage.FileName);
                    EA.LargeImageGUID = Guid.NewGuid().ToString("N");
                    string LargeImagelocation = Server.MapPath("~/Resources/Images/Events/" + EA.LargeImageGUID + "." + EA.LargeImageExtension);
                    EA.LargeImage.SaveAs(LargeImagelocation);
                }

                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                EA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                //EA.UserId = UserId;

                Int64 result = _eventDao.saveEvents(EA);
                if (result > 0)
                {
                    return RedirectToRoute("ViewEvents");
                }
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvents(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                //Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                //  Int64 Draft = _eventDao.DeleteEvent(GUID, UserId, SystemIp);
                Guid UserId = new Guid();
                Int64 Draft = _eventDao.DeleteEvent(GUID, UserId, SystemIp);
                return RedirectToRoute("ViewEvents", new { EncryptedId = string.Empty });
            }
            return View();
        }







        //public ActionResult PartialArticle(string EventId, Int64 Year)
        //{
        //    if (!string.IsNullOrEmpty(EventId) && Year > 0)
        //    {
        //        Guid GUID = new Guid(EventId);
        //        IList<Events> eventArticles = _eventDao.GetEventArticles(GUID, Year);
        //        return View(eventArticles);
        //    }
        //    return View();
        //}


     
        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetFacilitySorting()
        {
            //if (!string.IsNullOrEmpty(EncryptedId))
            //{
            // Guid GUID = new Guid(EncryptedId);
            IList<Facility> staticBanner = _facilityDao.FacilitySorting();
            return View(staticBanner);
            //}
            //  return View();
        }




        #region Excel validate Method

        private List<MaintenanceSchedule> ReadExcel(string path, string fileType, out List<TemplateUploadError> TUE)
        {
            List<MaintenanceSchedule> Maintenance = null;

            ExcelReader excelReader = new ExcelReader(path, fileType);

            DataTable dt = excelReader.GetXSLXData();


            TUE = new List<TemplateUploadError>();
            if (dt != null && dt.Rows != null)
            {
                Maintenance = new List<MaintenanceSchedule>();
                TUE = ValidateExcel(dt);
                if (TUE == null || (TUE != null && TUE.Count() == 0))
                {
                    TUE = ValidateInventoryImportExcelData(dt);
                    if (TUE == null || (TUE != null && TUE.Count() == 0))
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {

                            i++;
                            MaintenanceSchedule ITS = new MaintenanceSchedule();

                            ITS.Division = Convert.ToString(dr["Division"]);
                            ITS.BlkNo = Convert.ToString(dr["BlkNo"]);
                            ITS.StreetName = Convert.ToString(dr["StreetName"]);
                            ITS.RCZone = Convert.ToString(dr["RC Zone"]);
                            ITS.BlockWashing = Convert.ToString(dr["Block Washing"]);
                            ITS.BinChuteFogging = Convert.ToString(dr["Bin Chute Fogging"]);
                            ITS.BinChuteFlushing = Convert.ToString(dr["Bin Chute Flushing (Weekly)"]);
                            ITS.LiftMaintenance = Convert.ToString(dr["Lift Maintenance (Monthly)"]);

                            Maintenance.Add(ITS);
                        }
                    }
                }
            }
            return Maintenance;
        }

        private List<TemplateUploadError> ValidateExcel(DataTable Dt)
        {
            List<TemplateUploadError> TUE = new List<TemplateUploadError>();

            if (Dt.Columns != null && Dt.Columns.Count == 8)
            {
                if (!(Dt.Columns[0].ColumnName != null && Dt.Columns[0].ColumnName.ToLower() == "Division".ToLower()))
                {
                    TUE.Add(new TemplateUploadError { Line = 1, Column = 1, Message = "Must be Division." });
                }
                if (!(Dt.Columns[1].ColumnName != null && Dt.Columns[1].ColumnName.ToLower() == "BlkNo".ToLower()))
                {
                    TUE.Add(new TemplateUploadError { Line = 1, Column = 2, Message = "Must be BlkNo." });
                }
                if (!(Dt.Columns[2].ColumnName != null && Dt.Columns[2].ColumnName.ToLower() == "StreetName".ToLower()))
                {
                    TUE.Add(new TemplateUploadError { Line = 1, Column = 3, Message = "Must be Street Name." });
                }
                if (!(Dt.Columns[3].ColumnName != null && Dt.Columns[3].ColumnName.ToLower() == "RC Zone".ToLower()))
                {
                    TUE.Add(new TemplateUploadError { Line = 1, Column = 4, Message = "Must be RC Zone." });
                }
                if (!(Dt.Columns[4].ColumnName != null && Dt.Columns[4].ColumnName.ToLower() == "Block Washing".ToLower()))
                {
                    TUE.Add(new TemplateUploadError { Line = 1, Column = 5, Message = "Must be Block Washing." });
                }
                if (!(Dt.Columns[5].ColumnName != null && Dt.Columns[5].ColumnName.ToLower() == "Bin Chute Fogging".ToLower()))
                {
                    TUE.Add(new TemplateUploadError { Line = 1, Column = 6, Message = "Must be Bin Chute Fogging." });
                }
                if (!(Dt.Columns[6].ColumnName != null && Dt.Columns[6].ColumnName.ToLower() == "Bin Chute Flushing (Weekly)".ToLower()))
                {
                    TUE.Add(new TemplateUploadError { Line = 1, Column = 7, Message = "Must be Bin Chute Flushing (Weekly)." });
                }
                if (!(Dt.Columns[7].ColumnName != null && Dt.Columns[7].ColumnName.ToLower() == "Lift Maintenance (Monthly)".ToLower()))
                {
                    TUE.Add(new TemplateUploadError { Line = 1, Column = 8, Message = "Must be Lift Maintenance (Monthly)." });
                }


            }
            else
            {
                TUE.Add(new TemplateUploadError { Line = 1, Column = 0, Message = "Invalid no of columns in upload excel." });
            }
            return TUE;
        }
        private List<TemplateUploadError> ValidateInventoryImportExcelData(DataTable dt)
        {
            List<TemplateUploadError> TUE = new List<TemplateUploadError>();
            if (dt != null)
            {
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    var castleIdValid = new List<bool>();

                    #region Division
                    string Division = Convert.ToString(dr["Division"]);
                    if (string.IsNullOrWhiteSpace(Division) || string.IsNullOrEmpty(Division))
                    {
                        TUE.Add(new TemplateUploadError { Line = i, Column = 1, Message = "Division is required" });
                        castleIdValid.Add(false);
                    }
                    #endregion
                    #region BlkNo
                    string BlkNo = Convert.ToString(dr["BlkNo"]);
                    if (string.IsNullOrWhiteSpace(BlkNo) || string.IsNullOrEmpty(BlkNo))
                    {
                        TUE.Add(new TemplateUploadError { Line = i, Column = 2, Message = "BlkNo is required" });
                        castleIdValid.Add(false);
                    }
                    #endregion
                    #region StreetName
                    string StreetName = Convert.ToString(dr["StreetName"]);
                    if (string.IsNullOrWhiteSpace(StreetName) || string.IsNullOrEmpty(StreetName))
                    {
                        TUE.Add(new TemplateUploadError { Line = i, Column = 3, Message = "Street Name is required" });
                        castleIdValid.Add(false);
                    }
                    #endregion                  
                    #region RCZone
                    string RCZone = Convert.ToString(dr["RC Zone"]);
                    if (string.IsNullOrWhiteSpace(RCZone) || string.IsNullOrEmpty(RCZone))
                    {
                        TUE.Add(new TemplateUploadError { Line = i, Column = 4, Message = "RC Zone is required" });
                        castleIdValid.Add(false);
                    }
                    #endregion                 
                    #region BlockWashing
                    string BlockWashing = Convert.ToString(dr["Block Washing"]);
                    if (string.IsNullOrWhiteSpace(BlockWashing) || string.IsNullOrEmpty(BlockWashing))
                    {
                        TUE.Add(new TemplateUploadError { Line = i, Column = 5, Message = "Block Washing is required" });
                        castleIdValid.Add(false);
                    }
                    #endregion                  
                    #region BinChuteFogging
                    string BinChuteFogging = Convert.ToString(dr["Bin Chute Fogging"]);
                    if (string.IsNullOrWhiteSpace(BinChuteFogging) || string.IsNullOrEmpty(BinChuteFogging))
                    {
                        TUE.Add(new TemplateUploadError { Line = i, Column = 6, Message = "BinChuteFogging is required" });
                        castleIdValid.Add(false);
                    }
                    #endregion                   

                    #region BinChuteFoggingWeekly
                    string BinChuteFoggingWeekly = Convert.ToString(dr["Bin Chute Flushing (Weekly)"]);
                    if (string.IsNullOrWhiteSpace(BinChuteFoggingWeekly) || string.IsNullOrEmpty(BinChuteFoggingWeekly))
                    {
                        TUE.Add(new TemplateUploadError { Line = i, Column = 7, Message = "Bin Chute Flushing (Weekly) is required" });
                        castleIdValid.Add(false);
                    }
                    #endregion                 
                    #region LiftMaintenance
                    string LiftMaintenance = Convert.ToString(dr["Lift Maintenance (Monthly)"]);
                    if (string.IsNullOrWhiteSpace(LiftMaintenance) || string.IsNullOrEmpty(LiftMaintenance))
                    {
                        TUE.Add(new TemplateUploadError { Line = i, Column = 8, Message = "Lift Maintenance (Monthly) is required" });
                        castleIdValid.Add(false);
                    }
                    #endregion




                }
            }
            return TUE;
        }


        #endregion



    }
}