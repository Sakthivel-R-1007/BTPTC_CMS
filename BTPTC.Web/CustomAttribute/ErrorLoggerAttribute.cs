using BTPTC.Web.Helper;
using System.Web.Mvc;

namespace BTPTC.Web
{
    public class ErrorLoggerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            LogError(filterContext);
            string message = string.Empty;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                message = filterContext.Exception.Message;
                //filterContext.HttpContext.Response.StatusCode = 500;
                var json = new JsonResult { Data = message };
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
            }
            else
            {
                ViewResult vr = new ViewResult();
                vr.ViewName = "~/Views/Shared/Error.cshtml";
                vr.ViewData.Add("Error", filterContext.Exception);
                filterContext.Result = vr;
            }
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
        }

        public void LogError(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                LogHelper.WriteError(filterContext.Exception, "Error");
            }
        }
    }
}