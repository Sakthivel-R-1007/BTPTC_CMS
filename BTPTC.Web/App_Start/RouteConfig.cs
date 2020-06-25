using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BTPTC.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                  name: "Newsletter",
                  url: "NewsRoom/Newsletter",
                  defaults: new { controller = "NewsRoom", action = "Newsletter" },
                  namespaces: new[] { "BTPTC.Web.Controllers" }
           );

           routes.MapRoute(
                  name: "AnnualReport",
                  url: "NewsRoom/AnnualReport",
                  defaults: new { controller = "NewsRoom", action = "AnnualReport" },
                  namespaces: new[] { "BTPTC.Web.Controllers" }
         );

            routes.MapRoute(
                 name: "GalleryIndex",
                 url: "Gallery",
                 defaults: new { controller = "Gallery", action = "Index" },
                 namespaces: new[] { "BTPTC.Web.Controllers" }
        );

            routes.MapRoute(
                 name: "GalleryDetail",
                 url: "Gallery/Detail/{Title}",
                 defaults: new { controller = "Gallery", action = "Detail" },
                 namespaces: new[] { "BTPTC.Web.Controllers" }
        );


            routes.MapRoute(
                 name: "ViewFrontFacilities",
                 url: "OurTown/Index",
                 defaults: new { controller = "OurTown", action = "Index" },
                 namespaces: new[] { "BTPTC.Web.Controllers" }
             );

            routes.MapRoute(
                name: "ViewnewFacilities",
                url: "OurTown/ViewFacility",
                defaults: new { controller = "OurTown", action = "ViewFacility" },
                namespaces: new[] { "BTPTC.Web.Controllers" }
            );


            routes.MapRoute(
              name: "ViewMaintenance",
              url: "OurTown/Maintenance",
              defaults: new { controller = "OurTown", action = "Maintenance" },
              namespaces: new[] { "BTPTC.Web.Controllers" }
          );

            routes.MapRoute(
                 name: "ViewTownImprovementProject",
                 url: "TownImprovementProject/TownImprovementProject",
                 defaults: new { controller = "TownImprovementProject", action = "TownImprovementProject" },
                 namespaces: new[] { "BTPTC.Web.Controllers" }
             );


            routes.MapRoute(
              name: "ViewOurEvents",
              url: "OurTown/OurEvents",
              defaults: new { controller = "OurTown", action = "OurEvents" },
              namespaces: new[] { "BTPTC.Web.Controllers" }
          );

            routes.MapRoute(
              name: "ViewNewsLetter",
              url: "NewsRoom/NewsLetter",
              defaults: new { controller = "NewsRoom", action = "NewsLetter" },
              namespaces: new[] { "BTPTC.Web.Controllers" }
          );


            routes.MapRoute(
             name: "GetyearList",
             url: "NewsRoom/GetyearList/{year}",
             defaults: new { controller = "NewsRoom", action = "GetyearList" },
             namespaces: new[] { "BTPTC.Web.Controllers" }
         );



            routes.MapRoute(
           name: "EventDetails",
           url: "OurTown/EventDetails/{guid}",
           defaults: new { controller = "OurTown", action = "EventDetails" },
           namespaces: new[] { "BTPTC.Web.Controllers" }
       );
            routes.MapRoute(
                name: "Tender",
                url: "NewsRoom/Tender",
                defaults: new { controller = "NewsRoom", action = "Tender" },
                namespaces: new[] { "BTPTC.Web.Controllers" }
          );

            routes.MapRoute(
                  name: "LatestHappenings",
                  url: "NewsRoom/LatestHappenings",
                  defaults: new { controller = "NewsRoom", action = "LatestHappenings" },
                  namespaces: new[] { "BTPTC.Web.Controllers" }
           );


            routes.MapRoute(
                   name: "TownImprovementProject",
                   url: "OurTown/TownImprovementProject",
                   defaults: new { controller = "OurTown", action = "TownImprovementProject" },
                   namespaces: new[] { "BTPTC.Web.Controllers" }
            );

            routes.MapRoute(
                      name: "Logout",
                       url: "Logout",
                  defaults: new { controller = "Login", action = "Logout" },
                namespaces: new[] { "BTPTC.Web.Controllers" }
           );
            routes.MapRoute(
                     name: "Login",
                      url: "Login",
                 defaults: new { controller = "Login", action = "Index" },
               namespaces: new[] { "BTPTC.Web.Controllers" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
