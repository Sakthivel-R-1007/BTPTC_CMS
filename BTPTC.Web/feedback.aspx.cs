
using BTPTC.Domain;
using BTPTC.Persistence.Implementation;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Implementation;
using BTPTC.Service.Interface;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Unity;


namespace BTPTC.Web
{
    public partial class feedback : System.Web.UI.Page
    {
        IFeedbackDao _feedbackDao = new FeedbackDao();     
        IUtilityService _utilityService = new UtilityService();

        //public IFeedbackDao _feedbackDao { get; set; }

        //public IUtilityService _utilityService { get; set; }

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
                string sessionCaptcha=Convert.ToString(Session["Captcha_FB"]);
                string captcha=Convert.ToString(Request.Form["captcha"]);
                if (Convert.ToString(Request.Form["captcha"]) == Convert.ToString(Session["Captcha_FB"]))
                {
                    Feedback FB = new Feedback();
                    FB.Name = Request.Form["Name"];
                    FB.Gender = Request.Form["Gender"];
                    FB.Mobile = Request.Form["Mobile"];
                    FB.OfficeResidential = Request.Form["OfficeResidential"];
                    FB.Email = Request.Form["Email"];
                    FB.Subject = Request.Form["Subject"];
                    FB.SubjectOthers = Request.Form["SubjectOthers"];
                    FB.BlkNo = Request.Form["BlkNo"];
                    FB.UnitNo1 = Request.Form["UnitNo1"];
                    FB.UnitNo2 = Request.Form["UnitNo2"];
                    FB.StreetName = Request.Form["StreetName"];
                    FB.ReportedLocation = Request.Form["ReportedLocation"];
                    FB.Comments = Request.Form["Comments"];
                    FB.SystemIp = Request.UserHostAddress;
                    HttpPostedFile file1 = Request.Files["uploadAttach_1"];
                    HttpPostedFile file2 = Request.Files["uploadAttach_2"];
                    HttpPostedFile file3 = Request.Files["uploadAttach_3"];
                    string location1 = string.Empty;
                    string location2 = string.Empty;
                    string location3 = string.Empty;
                    if (file1 != null && file1.ContentLength > 0)
                    {
                        FB.Extension1 = Path.GetExtension(file1.FileName).Trim('.');
                        FB.FileName1 = Path.GetFileNameWithoutExtension(file1.FileName);
                        FB.FileGuid1 = Guid.NewGuid().ToString("N");
                        location1 = Server.MapPath("~/Resources/Images/feedback/" + FB.FileGuid1 + "." + FB.Extension1);
                        file1.SaveAs(location1);
                    }
                    if (file2 != null && file2.ContentLength > 0)
                    {
                        FB.Extension2 = Path.GetExtension(file2.FileName).Trim('.');
                        FB.FileName2 = Path.GetFileNameWithoutExtension(file2.FileName);
                        FB.FileGuid2 = Guid.NewGuid().ToString("N");
                        location2 = Server.MapPath("~/Resources/Images/feedback/" + FB.FileGuid2 + "." + FB.Extension2);
                        file2.SaveAs(location2);
                    }
                    if (file3 != null && file3.ContentLength > 0)
                    {
                        FB.Extension3 = Path.GetExtension(file3.FileName).Trim('.');
                        FB.FileName3 = Path.GetFileNameWithoutExtension(file3.FileName);
                        FB.FileGuid3 = Guid.NewGuid().ToString("N");
                        location3 = Server.MapPath("~/Resources/Images/feedback/" + FB.FileGuid3 + "." + FB.Extension3);
                        file3.SaveAs(location3);
                    }
                    if (FB != null)
                    {
                        _feedbackDao.Save(FB);
                        if (SendEmail(FB) == "success")
                        {
                            Response.Redirect("feedback-thankyou.html", false);
                        }
                    }
                }
                else
                {
                    captcha_error.InnerText = "Please enter correct code";
                }
                UpdateCaptchaText();
            }
        }

        [System.Web.Services.WebMethod]
        public static string RefreshCaptcha()
        {
            Random randNum = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            HttpContext.Current.Session["Captcha_FB"] = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[randNum.Next(s.Length)]).ToArray());
            return randNum.Next(10000, 99999).ToString();
        }

        private void UpdateCaptchaText()
        {
            captcha.Text = string.Empty;
            Random randNum = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Session["Captcha_FB"] = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[randNum.Next(s.Length)]).ToArray());
            //Store the captcha text in session to validate  
            //Session["Captcha"] = randNum.Next(10000, 99999).ToString();
            imgCaptcha.ImageUrl = "~/CaptchaHandler.ashx?type=feedback&val=" + randNum.Next(10000, 99999).ToString();
        }

        private string SendEmail(Feedback FB)
        {
            string emailStatus = "error";
            if (FB != null && !string.IsNullOrEmpty(FB.Email))
            {
                String contents = File.ReadAllText(Server.MapPath("~/src/FeedbackEmailTemplate.html"));
                contents = contents.Replace("#Name#", FB.Name);
                contents = contents.Replace("#Gender#", FB.Gender);
                contents = contents.Replace("#Mobile#", FB.Mobile);
                contents = contents.Replace("#Office#", FB.OfficeResidential);
                contents = contents.Replace("#Email#", FB.Email);
                contents = contents.Replace("#Subject#", FB.Subject);
                contents = contents.Replace("#Others#", FB.SubjectOthers);
                contents = contents.Replace("#BlkNo#", FB.BlkNo);
                contents = contents.Replace("#UnitNo#", "#" + FB.UnitNo1 + "-" + FB.UnitNo2);
                contents = contents.Replace("#StreetName#", FB.StreetName);
                contents = contents.Replace("#ReportedLocation#", FB.ReportedLocation);
                contents = contents.Replace("#Comments#", FB.Comments);
                contents = contents.Replace("#Item1#", FB.FileName1 != null ? FB.FileName1 : "-");
                contents = contents.Replace("#Item2#", FB.FileName2 != null ? FB.FileName2 : "-");
                contents = contents.Replace("#Item3#", FB.FileName3 != null ? FB.FileName3 : "-");
                string FileName11 = "";
                string filepath1 = "";
                Uri uri = HttpContext.Current.Request.Url;
                string host = string.Empty;
#if DEBUG
                //host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
#else
                   // host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
#endif

                host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
                if (!string.IsNullOrEmpty(FB.FileGuid1) && !string.IsNullOrEmpty(FB.Extension1))
                {
                    //string path = HttpContext.Current.Request.Url.ToString();
                    FileName11 = ((FB != null && FB.FileGuid1 != null ? FB.FileGuid1 : "") + '.' + (FB != null && FB.Extension1 != null ? FB.Extension1 : ""));
                    //filepath1 = Server.MapPath("~/Resources/bulkyremoval/") + FileName1;
                    filepath1 = (host + "/Resources/Images/feedback/") + FileName11;
                }
                if (!string.IsNullOrEmpty(FileName11))
                {
                    contents = contents.Replace("#Attachmenthref01#", filepath1);
                    contents = contents.Replace("#Attachment01#", FB.FileName1);
                }
                else
                {
                    contents = contents.Replace("#Attachmenthref01#", "#");
                    contents = contents.Replace("#Attachment01#", "");
                }
                string FileName22 = "";
                string filepath2 = "";
                if (!string.IsNullOrEmpty(FB.FileGuid2) && !string.IsNullOrEmpty(FB.Extension2))
                {
                    FileName22 = ((FB != null && FB.FileGuid2 != null ? FB.FileGuid2 : "") + '.' + (FB != null && FB.Extension2 != null ? FB.Extension2 : ""));
                    filepath2 = (host + "/Resources/Images/feedback/") + FileName22;
                }
                if (!string.IsNullOrEmpty(FileName22))
                {
                    contents = contents.Replace("#Attachmenthref02#", filepath2);
                    contents = contents.Replace("#Attachment02#", FB.FileName2);
                }
                else
                {
                    contents = contents.Replace("#Attachmenthref02#", "#");
                    contents = contents.Replace("#Attachment02#", "");
                }
                string FileName33 = "";
                string filepath3 = "";
                if (!string.IsNullOrEmpty(FB.FileGuid3) && !string.IsNullOrEmpty(FB.Extension3))
                {
                    FileName33 = ((FB != null && FB.FileGuid3 != null ? FB.FileGuid3 : "") + '.' + (FB != null && FB.Extension3 != null ? FB.Extension3 : ""));
                    filepath3 = (host + "/Resources/Images/feedback/") + FileName33;
                }
                if (!string.IsNullOrEmpty(FileName33))
                {
                    contents = contents.Replace("#Attachmenthref03#", filepath3);
                    contents = contents.Replace("#Attachment03#", FB.FileName3);
                }
                else
                {
                    contents = contents.Replace("#Attachmenthref03#", "#");
                    contents = contents.Replace("#Attachment03#", "");
                }
                if (string.IsNullOrEmpty(FileName11) && string.IsNullOrEmpty(FileName22) && string.IsNullOrEmpty(FileName33))
                {
                    contents = contents.Replace("Attachment", "");
                }
                contents = contents.Replace("#logoPath#", (host + "Contents/images/common/logo.png"));

                if (_utilityService.SendEmail("BTC - Feedback", contents, FB.Email, true, null, null) == "success")
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