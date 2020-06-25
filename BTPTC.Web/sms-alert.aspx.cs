
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
using System.Web.UI.WebControls;
using Unity;

namespace BTPTC.Web
{

    public partial class sms_alert : System.Web.UI.Page
    {


        ISMSAlertDao _SMSAlertDao = new SMSAlertDao();


        IUtilityService _utilityService = new UtilityService();



        //public ISMSAlertDao _SMSAlertDao { get; set; }

      
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
                string sessionCaptcha=Convert.ToString(Session["Captcha_SMS"]);
                string captcha=Convert.ToString(Request.Form["captcha"]);
                if (Convert.ToString(Request.Form["captcha"]) == Convert.ToString(Session["Captcha_SMS"]))
                {
                    SMSAlert SA = new SMSAlert();
                    SA.Name = Request.Form["Name"];
                    SA.BlkNo = Request.Form["BlkNo"];
                    SA.UnitNo1 = Request.Form["UnitNo1"];
                    SA.UnitNo2 = Request.Form["UnitNo2"];
                    SA.StreetName = Request.Form["StreetName"];
                    SA.Email = Request.Form["Email"];
                    SA.Mobile = Request.Form["Mobile"];
                    SA.SystemIp = Request.UserHostAddress;
                    if (SA != null)
                    {
                        _SMSAlertDao.Save(SA);
                        if (SendEmail(SA) == "success")
                        {
                            Response.Redirect("sms-alert-thankyou.html", false);
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
            HttpContext.Current.Session["Captcha_SMS"] = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[randNum.Next(s.Length)]).ToArray());
            return randNum.Next(10000, 99999).ToString();
        }


        private void UpdateCaptchaText()
        {
            captcha.Text = string.Empty;
            Random randNum = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Session["Captcha_SMS"] = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[randNum.Next(s.Length)]).ToArray());
            //Store the captcha text in session to validate  
            //Session["Captcha"] = randNum.Next(10000, 99999).ToString();
            imgCaptcha.ImageUrl = "~/CaptchaHandler.ashx?type=sms&val=" + randNum.Next(10000, 99999).ToString();
        }

        private string SendEmail(SMSAlert SA)
        {
            string emailStatus = "error";
            if (SA != null && !string.IsNullOrEmpty(SA.Email))
            {
                Uri uri = HttpContext.Current.Request.Url;
                string host = string.Empty;
#if DEBUG
               // host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
#else
                    //host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
#endif
                host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
                String contents = File.ReadAllText(Server.MapPath("~/src/SmsAlertEmailTemplate.html"));
                contents = contents.Replace("#Name#", SA.Name);
                contents = contents.Replace("#Email#", SA.Email);
                contents = contents.Replace("#BlkNo#", SA.BlkNo);
                contents = contents.Replace("#UnitNo#", "#" + SA.UnitNo1 + "-" + SA.UnitNo2);
                contents = contents.Replace("#StreetName#", SA.StreetName);
                contents = contents.Replace("#MobileNo#", SA.Mobile);
                contents = contents.Replace("#logoPath#", (host + "Contents/images/common/logo.png"));
                if (_utilityService.SendEmail("BTC - SMS Alert", contents, SA.Email, true, null, null) == "success")
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