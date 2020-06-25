using System.Web;
using System.Web.Optimization;

namespace BTPTC.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region css
            #region Common

            bundles.Add(new StyleBundle("~/Adminlayoutcss").Include(
               "~/Contents/Admin/css/common.css",
               "~/Contents/Admin/css/jquery-ui.css"
            ));
            #endregion
            #endregion

            #region js

            #region Common
            bundles.Add(new ScriptBundle("~/Adminlayoutjs").Include(

                "~/Contents/Admin/js/jquery-3.4.1.min.js",
                "~/Contents/Admin/js/bootstrap/bootstrap.js",
                "~/Contents/Admin/js/jquery-ui.js"

                ));

            #endregion

            #region Gallery-Index

            bundles.Add(new ScriptBundle("~/admin-Gallery-Indexjs").Include(
                "~/Contents/Admin/js/jqueryPagination/simplePagination.js",
                "~/Contents/Admin/js/Custom/admin-gallery-index.js"
            ));
            #endregion

            #region Gallery - Add

            bundles.Add(new ScriptBundle("~/admin-Gallery-addjs").Include(
             "~/Contents/Admin/js/jqueryValidate/jquery.validate.js",
             "~/Contents/Admin/js/jqueryValidate/additional-methods.js",
              "~/Contents/Admin/js/Custom/admin-gallery-add.js"
            ));
            #endregion

            #region Gallery-Edit

            bundles.Add(new ScriptBundle("~/admin-Gallery-editjs").Include(
                "~/Contents/Admin/js/jqueryPagination/simplePagination.js",
                 "~/Contents/Admin/js/jqueryValidate/jquery.validate.js",
                 "~/Contents/Admin/js/jqueryValidate/additional-methods.js",
                 "~/Contents/Admin/js/Custom/admin-gallery-edit.js"
            ));
            #endregion
            #endregion

            #region Annual Report-Edit

            bundles.Add(new ScriptBundle("~/admin-Annualreport-indexjs").Include(
                "~/Contents/Admin/js/jqueryPagination/simplePagination.js",
                 "~/Contents/Admin/js/jqueryValidate/jquery.validate.js",
                 "~/Contents/Admin/js/jqueryValidate/additional-methods.js",
                 "~/Contents/Admin/js/Custom/admin-annualreport-index.js"
            ));
            #endregion


        }
    }
}
