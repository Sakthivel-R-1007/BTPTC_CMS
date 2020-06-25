using BTPTC.Domain;
using BTPTC.Persistence.Interface;
using BTPTC.Persistence.Implementation;
using BTPTC.Service.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;
using BTPTC.Service.Implementation;

namespace BTPTC.Web
{
    public partial class bulky_item_removal_services : System.Web.UI.Page
    {
        IBulkyItemRemovalServiceDao _bulkyItemRemovalServiceDao = new BulkyItemRemovalServiceDao();

        //public IBulkyItemRemovalServiceDao _bulkyItemRemovalServiceDao { get; set; }
      
      //  public IUtilityService _utilityService { get; set; }
        IUtilityService _utilityService = new UtilityService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UpdateCaptchaText();
            }
        }

        protected void Submit(object sender, EventArgs e)
        {
            if (Request.RequestType == "POST")
            {
                captcha_error.InnerText = "";
                string sessionCaptcha = Convert.ToString(Session["Captcha_BR"]);
                string captcha = Convert.ToString(Request.Form["captcha"]);
                if (Convert.ToString(Request.Form["captcha"]) == Convert.ToString(Session["Captcha_BR"]))
                {

                    BulkyItemRemovalService BS = new BulkyItemRemovalService();
                    BS.Name = Request.Form["Name"];
                    BS.Email = Request.Form["Email"];
                    BS.BlkNo = Request.Form["BlkNo"];
                    BS.UnitNo1 = Request.Form["UnitNo1"];
                    BS.UnitNo2 = Request.Form["UnitNo2"];
                    BS.StreetName = Request.Form["StreetName"];
                    BS.Mobile = Request.Form["Mobile"];
                    BS.OfficeResidential = Request.Form["OfficeResidential"];
                    DateTime outDate;
                    DateTime minAppointmentDate = DateTime.Now;
                    string appointDate = Request.Form["AppointmentDate"];
                    if (!string.IsNullOrEmpty(appointDate))
                    {
                        DateTime.TryParseExact(appointDate, "dd/MM/yyyy", null, DateTimeStyles.None, out outDate);
                        BS.AppointmentDate = outDate;
                        int nDays = 3;
                        int weeks = nDays / 5;
                        nDays %= 5;
                        while (minAppointmentDate.DayOfWeek == DayOfWeek.Saturday || minAppointmentDate.DayOfWeek == DayOfWeek.Sunday)
                            minAppointmentDate = minAppointmentDate.AddDays(1);

                        while (nDays-- > 0)
                        {
                            minAppointmentDate = minAppointmentDate.AddDays(1);
                            if (minAppointmentDate.DayOfWeek == DayOfWeek.Saturday)
                                minAppointmentDate = minAppointmentDate.AddDays(2);
                        }

                    }
                    else
                    {
                        int nDays = 3;
                        int weeks = nDays / 5;
                        nDays %= 5;
                        while (minAppointmentDate.DayOfWeek == DayOfWeek.Saturday || minAppointmentDate.DayOfWeek == DayOfWeek.Sunday)
                            minAppointmentDate = minAppointmentDate.AddDays(1);

                        while (nDays-- > 0)
                        {
                            minAppointmentDate = minAppointmentDate.AddDays(1);
                            if (minAppointmentDate.DayOfWeek == DayOfWeek.Saturday)
                                minAppointmentDate = minAppointmentDate.AddDays(2);
                        }
                        BS.AppointmentDate = minAppointmentDate;
                    }
                    BS.AppointmentTime = Request.Form["AppointmentTime"];
                    BS.ItemDescription1 = Request.Form["ItemDescription1"];
                    BS.ItemDescription2 = Request.Form["ItemDescription2"];
                    BS.ItemDescription3 = Request.Form["ItemDescription3"];
                    BS.SystemIp = Request.UserHostAddress;
                    HttpPostedFile file1 = Request.Files["uploadAttach_1"];
                    HttpPostedFile file2 = Request.Files["uploadAttach_2"];
                    HttpPostedFile file3 = Request.Files["uploadAttach_3"];
                    string location1 = string.Empty;
                    string location2 = string.Empty;
                    string location3 = string.Empty;
                    if (file1 != null && file1.ContentLength > 0)
                    {
                        BS.Extension1 = Path.GetExtension(file1.FileName).Trim('.');
                        BS.FileName1 = Path.GetFileNameWithoutExtension(file1.FileName);
                        BS.FileGuid1 = Guid.NewGuid().ToString("N");
                        location1 = Server.MapPath("~/Resources/Images/bulkyremoval/" + BS.FileGuid1 + "." + BS.Extension1);
                        file1.SaveAs(location1);
                    }
                    if (file2 != null && file2.ContentLength > 0)
                    {
                        BS.Extension2 = Path.GetExtension(file2.FileName).Trim('.');
                        BS.FileName2 = Path.GetFileNameWithoutExtension(file2.FileName);
                        BS.FileGuid2 = Guid.NewGuid().ToString("N");
                        location2 = Server.MapPath("~/Resources/Images/bulkyremoval/" + BS.FileGuid2 + "." + BS.Extension2);
                        file2.SaveAs(location2);
                    }
                    if (file3 != null && file3.ContentLength > 0)
                    {
                        BS.Extension3 = Path.GetExtension(file3.FileName).Trim('.');
                        BS.FileName3 = Path.GetFileNameWithoutExtension(file3.FileName);
                        BS.FileGuid3 = Guid.NewGuid().ToString("N");
                        location3 = Server.MapPath("~/Resources/Images/bulkyremoval/" + BS.FileGuid3 + "." + BS.Extension3);
                        file3.SaveAs(location3);
                    }
                    if (BS.AppointmentDate.Date >= minAppointmentDate.Date)
                    {
                        if (BS != null)
                        {
                            _bulkyItemRemovalServiceDao.Save(BS);
                            if (SendEmail(BS) == "success")
                            {
                                Response.Redirect("bulky-item-removal-services-thankyou.html", false);
                            }

                        }
                    }
                    else
                    {
                        AppointmentDate_error.InnerText = "Application Date should Be 3 from now";
                        divBTC.Style.Add("display", "none");
                        BulkyItemRemovalForm.Style.Remove("display");
                    }
                }
                else
                {
                    captcha_error.InnerText = "Please enter correct code";
                    divBTC.Style.Add("display", "none");
                    BulkyItemRemovalForm.Style.Remove("display");
                }
                UpdateCaptchaText();
            }
        }

        [System.Web.Services.WebMethod]
        public static string RefreshCaptcha()
        {
            Random randNum = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            HttpContext.Current.Session["Captcha_BR"] = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[randNum.Next(s.Length)]).ToArray());
            return randNum.Next(10000, 99999).ToString();
        }

        private void UpdateCaptchaText()
        {
            captcha.Text = string.Empty;
            Random randNum = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Session["Captcha_BR"] = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[randNum.Next(s.Length)]).ToArray());
            //Store the captcha text in session to validate  
            //Session["Captcha"] = randNum.Next(10000, 99999).ToString();
            imgCaptcha.ImageUrl = "~/CaptchaHandler.ashx?" + randNum.Next(10000, 99999).ToString();
        }

        private string SendEmail(BulkyItemRemovalService BS)
        {
            string emailStatus = "error";
            if (BS != null && !string.IsNullOrEmpty(BS.Email))
            {

                String contents = File.ReadAllText(Server.MapPath("~/src/bulkyRemovalEmailTemplate.html"));
                contents = contents.Replace("#Name#", BS.Name);
                contents = contents.Replace("#Email#", BS.Email);
                contents = contents.Replace("#BlkNo#", BS.BlkNo);
                contents = contents.Replace("#UnitNo#", "#" + BS.UnitNo1 + "-" + BS.UnitNo2);
                contents = contents.Replace("#StreetName#", BS.StreetName);
                contents = contents.Replace("#MobileNo#", BS.Mobile);
                contents = contents.Replace("#Office#", BS.OfficeResidential);
                contents = contents.Replace("#AppointmentDate#", BS.AppointmentDate != null ? BS.AppointmentDate.ToString("dd/MM/yyyy") : "-");
                contents = contents.Replace("#Item1#", BS.ItemDescription1 != null ? BS.ItemDescription1 : "-");
                contents = contents.Replace("#Item2#", BS.ItemDescription2 != null ? BS.ItemDescription2 : "-");
                contents = contents.Replace("#Item3#", BS.ItemDescription3 != null ? BS.ItemDescription3 : "-");
                string FileName1 = "";
                string filepath1 = "";
                Uri uri = HttpContext.Current.Request.Url;
                string host = string.Empty;
#if DEBUG
                //host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
#else
                    //host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
#endif
                host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
                if (!string.IsNullOrEmpty(BS.FileGuid1) && !string.IsNullOrEmpty(BS.Extension1))
                {
                    //string path = HttpContext.Current.Request.Url.ToString();
                    FileName1 = ((BS != null && BS.FileGuid1 != null ? BS.FileGuid1 : "") + '.' + (BS != null && BS.Extension1 != null ? BS.Extension1 : ""));
                    //filepath1 = Server.MapPath("~/Resources/bulkyremoval/") + FileName1;
                    filepath1 = (host + "/Resources/Images/bulkyremoval/") + FileName1;
                }
                if (!string.IsNullOrEmpty(FileName1))
                {
                    contents = contents.Replace("#Attachmenthref01#", filepath1);
                    contents = contents.Replace("#Attachment01#", BS.FileName1);
                }
                else
                {
                    contents = contents.Replace("#Attachmenthref01#", "#");
                    contents = contents.Replace("#Attachment01#", "");
                }
                string FileName2 = "";
                string filepath2 = "";
                if (!string.IsNullOrEmpty(BS.FileGuid2) && !string.IsNullOrEmpty(BS.Extension2))
                {
                    FileName2 = ((BS != null && BS.FileGuid2 != null ? BS.FileGuid2 : "") + '.' + (BS != null && BS.Extension2 != null ? BS.Extension2 : ""));
                    filepath2 = (host + "/Resources/Images/bulkyremoval/") + FileName2;
                }
                if (!string.IsNullOrEmpty(FileName2))
                {
                    contents = contents.Replace("#Attachmenthref02#", filepath2);
                    contents = contents.Replace("#Attachment02#", BS.FileName2);
                }
                else
                {
                    contents = contents.Replace("#Attachmenthref02#", "#");
                    contents = contents.Replace("#Attachment02#", "");
                }
                string FileName3 = "";
                string filepath3 = "";
                if (!string.IsNullOrEmpty(BS.FileGuid3) && !string.IsNullOrEmpty(BS.Extension3))
                {
                    FileName3 = ((BS != null && BS.FileGuid3 != null ? BS.FileGuid3 : "") + '.' + (BS != null && BS.Extension3 != null ? BS.Extension3 : ""));
                    filepath3 = (host + "/Resources/Images/bulkyremoval/") + FileName3;
                }
                if (!string.IsNullOrEmpty(FileName3))
                {
                    contents = contents.Replace("#Attachmenthref03#", filepath3);
                    contents = contents.Replace("#Attachment03#", BS.FileName3);
                }
                else
                {
                    contents = contents.Replace("#Attachmenthref03#", "#");
                    contents = contents.Replace("#Attachment03#", "");
                }
                if (string.IsNullOrEmpty(FileName1) && string.IsNullOrEmpty(FileName2) && string.IsNullOrEmpty(FileName3))
                {
                    contents = contents.Replace("Attachment", "");
                }
                contents = contents.Replace("#logoPath#", (host + "Contents/images/common/logo.png"));
                if (_utilityService.SendEmail("BTC - Bulky Item Removal Services", contents, BS.Email, true, null, null) == "success")
                {
                    emailStatus = "success";
                }
                else
                {
                    emailStatus = "error";
                }
            }
            return emailStatus;
        }
    }
}