using System.Web.Mvc;

namespace BTPTC.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region OurTown



            context.MapRoute(
                    name: "Maintenance",
                    url: "Admin/Maintenance",
                    defaults: new { controller = "OurTown", action = "Maintenance" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );


            context.MapRoute(
                    name: "ViewFacilities",
                    url: "Admin/Facilities",
                    defaults: new { controller = "OurTown", action = "Facilities" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );

            context.MapRoute(
                    name: "AddFacility",
                    url: "Admin/AddFacility",
                    defaults: new { controller = "OurTown", action = "AddFacility" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                    name: "ViewEvents",
                    url: "Admin/ViewEvents",
                    defaults: new { controller = "OurTown", action = "ViewEvents" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                    name: "AddEvents",
                    url: "Admin/AddEvents",
                    defaults: new { controller = "OurTown", action = "AddEvents" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                   name: "EditEvents",
                   url: "Admin/EditEvents/{EncryptedId}",
                   defaults: new { controller = "OurTown", action = "EditEvents", EncryptedId = UrlParameter.Optional },
                   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );

            context.MapRoute(
               name: "DeleteEvents",
               url: "Admin/DeleteEvents/{EncryptedId}",
               defaults: new { controller = "OurTown", action = "DeleteEvents" },
               namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
        );


            //context.MapRoute(
            //        name: "Maintenance",
            //        url: "Admin/Maintenance",
            //        defaults: new { controller = "OurTown", action = "Maintenance" },
            //        namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            //);


            context.MapRoute(
                  name: "ImportExcelMaintenance",
                  url: "Admin/ImportExcel",
                  defaults: new { controller = "OurTown", action = "ImportExcel" },
                  namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
          );


            context.MapRoute(
                    name: "EventsPartialView",
                     url: "OurTown/PartialView/{PageIndex}/{Year}",
                defaults: new { controller = "OurTown", action = "PartialView" },
              namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );


            context.MapRoute(
            name: "PartialArticle",
            url: "OurTown/PartialArticle/{EventId}/{Year}",
       defaults: new { controller = "OurTown", action = "PartialArticle" },
       namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
     );



            //facility

            context.MapRoute(
                   name: "EditFacility",
                   url: "Admin/EditFacility/{id}",
                   defaults: new { controller = "OurTown", action = "EditFacility" },
                   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );


            context.MapRoute(
               name: "DeleteFacility",
               url: "Admin/DeleteFacility/{EncryptedId}",
               defaults: new { controller = "OurTown", action = "DeleteFacility" },
               namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
        );


            context.MapRoute(
                  name: "ViewFacility",
                  url: "Admin/Facilities",
                  defaults: new { controller = "OurTown", action = "Facilities" },
                  namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
          );


            context.MapRoute(
                    name: "PartialViewFacility",
                     url: "OurTown/PartialViewFacility/{PageIndex}/{Year}",
                defaults: new { controller = "OurTown", action = "PartialViewFacility" },
              namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );


            context.MapRoute(
                  name: "GetFaciltyImage",
                  url: "Admin/GetFaciltyImage/{EncryptedId}",
                  defaults: new { controller = "OurTown", action = "GetFaciltyImage" },
                  namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
          );

            context.MapRoute(
                 name: "CheckFacilityTitle",
                  url: "Admin/OurTown/CheckFacilityTitle/{Name}/{EncDetail}",
             defaults: new { controller = "OurTown", action = "CheckFacilityTitle", NoticeType = UrlParameter.Optional, EncDetail = UrlParameter.Optional },
           namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
      );

            context.MapRoute(
              name: "SortFacilityList",
               url: "Admin/OurTown/GetFacilitySorting/{EncryptedId}",
          defaults: new { controller = "OurTown", action = "GetFacilitySorting", EncryptedId = UrlParameter.Optional },
        namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
   );


            //facility






            #endregion

            #region  Change Password
            context.MapRoute(
                 name: "AdminChangePassword",
                 url: "Admin/ChangePassword",
                 defaults: new { controller = "OurTown", action = "ChangePassword" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );
            #endregion
            #region Town Improvement Project


            context.MapRoute(
                  name: "TownImprovementFacilities",
                  url: "Admin/Facilities",
                  defaults: new { controller = "TownImprovementProject", action = "Facilities" },
                  namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );

            context.MapRoute(
                 name: "TownImprovementAddFacility",
                 url: "Admin/AddFacility",
                 defaults: new { controller = "TownImprovementProject", action = "AddFacility" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );





            context.MapRoute(
                    name: "AdminTownImprovementProject",
                    url: "TownImprovementProject",
                    defaults: new { controller = "OurTown", action = "TownImprovementProject" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );




            context.MapRoute(
                   name: "AddNoticeType",
                   url: "TownImprovementProject/AddNoticeType",
                   defaults: new { controller = "TownImprovementProject", action = "AddNoticeType" },
                   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );


            context.MapRoute(
                  name: "ViewType",
                  url: "TownImprovementProject/ViewType",
                  defaults: new { controller = "TownImprovementProject", action = "ViewType" },
                  namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
          );

            context.MapRoute(
            name: "EditNotice",
            url: "Admin/TownImprovementProject/EditNotice/{EncryptedId}",
            defaults: new { controller = "TownImprovementProject", action = "EditNotice" },
            namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
       );

            context.MapRoute(
                name: "ViewIndexType",
                url: "Admin/TownImprovementProject/Index",
                defaults: new { controller = "TownImprovementProject", action = "Index" },
                namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );


            context.MapRoute(
                  name: "NoticePartialView",
                   url: "Admin/TownImprovementProject/PartialView/{PageIndex}/{Year}",
              defaults: new { controller = "TownImprovementProject", action = "PartialView" },
            namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
       );


            context.MapRoute(
                  name: "CheckNoticeTitle",
                   url: "Admin/TownImprovementProject/CheckNoticeTitle/{NoticeType}/{EncDetail}",
              defaults: new { controller = "TownImprovementProject", action = "CheckNoticeTitle", NoticeType = UrlParameter.Optional, EncDetail = UrlParameter.Optional },
            namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
       );



            //project



            context.MapRoute(
                  name: "AddNoticeProject",
                  url: "Admin/TownImprovementProject/AddNoticeProject",
                  defaults: new { controller = "TownImprovementProject", action = "AddNoticeProject" },
                  namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
          );


            context.MapRoute(
              name: "view",
              url: "OurTown/Index",
              defaults: new { controller = "OurTown", action = "Index" },
              namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
      );

            context.MapRoute(
                 name: "ViewNoticeProject",
                 url: "Admin/TownImprovementProject/Index",
                 defaults: new { controller = "TownImprovementProject", action = "Index" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );

            context.MapRoute(
             name: "ViewTownProject",
             url: "Admin/TownImprovementProject/ViewTownImprovenmentProject",
             defaults: new { controller = "TownImprovementProject", action = "ViewTownImprovenmentProject" },
             namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
     );

            context.MapRoute(
        name: "ViewTownProject1",
         url: "Admin/ViewTownImprovement",
        defaults: new { controller = "TownImprovementProject", action = "ViewTownImprovement" },
        namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
);



            context.MapRoute(
              name: "EditNoticeType",
              url: "Admin/TownImprovementProject/EditNoticeType",
              defaults: new { controller = "TownImprovementProject", action = "EditNoticeType" },
              namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
      );


            context.MapRoute(
              name: "GetEditNotice",
              url: "Admin/TownImprovementProject/GetEditNoticeProject/{EncryptedId}",
              defaults: new { controller = "TownImprovementProject", action = "GetEditNoticeProject" },
              namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );




            context.MapRoute(
        name: "EditNoticeProject",
        url: "Admin/TownImprovementProject/EditNoticeProject/{EncryptedId}",
        defaults: new { controller = "TownImprovementProject", action = "EditNoticeProject" },
        namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
        );


            context.MapRoute(
                name: "NoticeProjectPartialView",
                 url: "Admin/TownImprovementProject/PartialViewNoticeProject/{PageIndex}/{Year}",
            defaults: new { controller = "TownImprovementProject", action = "PartialViewNoticeProject" },
          namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
     );



            context.MapRoute(
                  name: "EditTownProjectNoticePopUp",
                  url: "Admin/EditTownProjectNoticePopUp/{EncryptedId}",
                  defaults: new { controller = "TownImprovementProject", action = "EditTownProjectNoticePopUp" },
                  namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
          );
            #endregion

            #region Gallery

            context.MapRoute(
                    name: "AdminViewGallery",
                    url: "Admin/Index",
                    defaults: new { controller = "Gallery", action = "Index" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                    name: "AddGallery",
                    url: "Admin/Add",
                    defaults: new { controller = "Gallery", action = "Add" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                    name: "EditGallery",
                    url: "Admin/Gallery/Edit/{EncDetail}",
                    defaults: new { controller = "Gallery", action = "Edit" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                   name: "GalleryPhotoPartialView",
                   url: "Admin/Gallery/GalleryPhotoPartialView/{EncDetail}/{PageIndex}",
                   defaults: new { controller = "Gallery", action = "GalleryPhotoPartialView" },
                   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );


            context.MapRoute(
                  name: "GalleryPartialView",
                   url: "Admin/Gallery/PartialView/{PageIndex}",
                   defaults: new { controller = "Gallery", action = "PartialView" },
                   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );

            context.MapRoute(
                      name: "AddGalleryPhotoPartialView",
                        url: "Admin/Gallery/AddGalleryPhotoPartialView/{EncDetail}",
                   defaults: new { controller = "Gallery", action = "AddGalleryPhotoPartialView" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                      name: "EditGalleryPhotoPartialView",
                        url: "Admin/Gallery/EditGalleryPhotoPartialView/{EncDetail}/{EncGUID}",
                   defaults: new { controller = "Gallery", action = "EditGalleryPhotoPartialView" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            #endregion

            #region Newsroom

            context.MapRoute(
                   name: "AdminViewNewsletter",
                   url: "ViewNewsletter",
                   defaults: new { controller = "NewsRoom", action = "ViewNewsletter" },
                   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );

            context.MapRoute(
                 name: "PartialViewNewsletter",
                  url: "NewsRoom/PartialViewNewsletter/{PageIndex}/{Year}",
             defaults: new { controller = "NewsRoom", action = "PartialViewNewsletter" },
           namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
      );


            context.MapRoute(
              name: "PartialViewEditNewsletter",
               url: "NewsRoom/EditPartialNewsLetterGUID/{EncryptedId}",
          defaults: new { controller = "NewsRoom", action = "EditPartialNewsLetterGUID" },
        namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
      );


            context.MapRoute(
              name: "CheckNewsTitle",
               url: "Admin/NewsRoom/CheckNewsTitle/{NewsTitle}/{EncDetail}",
          defaults: new { controller = "NewsRoom", action = "CheckNewsTitle", NoticeType = UrlParameter.Optional, EncDetail = UrlParameter.Optional },
        namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
   );




            context.MapRoute(
         name: "AddViewMediaRelease",
          url: "Admin/NewsRoom/AddViewMediaRelease",
     defaults: new { controller = "NewsRoom", action = "AddViewMediaRelease" },
   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
);


            context.MapRoute(
                name: "EditMediaRelease",
                url: "NewsRoom/EditMediaRelease/{EncDetail}",
           defaults: new { controller = "NewsRoom", action = "EditMediaRelease" },
           namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );

            context.MapRoute(
         name: "CheckMediaTitle",
          url: "Admin/NewsRoom/CheckMediaTitle/{NewsTitle}/{EncDetail}",
     defaults: new { controller = "NewsRoom", action = "CheckMediaTitle", NoticeType = UrlParameter.Optional, EncDetail = UrlParameter.Optional },
   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
);


            context.MapRoute(
              name: "PartialMediaRelease",
               url: "NewsRoom/PartialViewMediaRelease/{PageIndex}/{Year}",
          defaults: new { controller = "NewsRoom", action = "PartialViewMediaRelease" },
        namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
   );


            context.MapRoute(
                   name: "AdminViewMediaRelease",
                   url: "ViewMediaRelease",
                   defaults: new { controller = "NewsRoom", action = "ViewMediaRelease" },
                   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );

   
            context.MapRoute(
                   name: "AdminViewTender",
                   url: "ViewTender",
                   defaults: new { controller = "NewsRoom", action = "ViewTender" },
                   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );

            context.MapRoute(
                 name: "AdminAddNotice",
                 url: "NewsRoom/AddNotice",
                 defaults: new { controller = "NewsRoom", action = "AddNotice" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );



            context.MapRoute(
               name: "EditTenderNotice",
               url: "NewsRoom/EditNotice/{EncDetail}",
          defaults: new { controller = "NewsRoom", action = "EditNotice" },
          namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
        );



            context.MapRoute(
            name: "CheckTenderTitle",
             url: "Admin/NewsRoom/CheckTenderTitle/{NewsTitle}/{EncDetail}",
          defaults: new { controller = "NewsRoom", action = "CheckTenderTitle", NoticeType = UrlParameter.Optional, EncDetail = UrlParameter.Optional },
          namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
          );




            context.MapRoute(
                 name: "AdminAddResult",
                 url: "NewsRoom/AddResult",
                 defaults: new { controller = "NewsRoom", action = "AddResult" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );


            context.MapRoute(
                 name: "AdminEditResult",
                 url: "NewsRoom/EditResult",
                 defaults: new { controller = "NewsRoom", action = "EditResult" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
         );



            context.MapRoute(
               name: "AdminAddAwardedt",
               url: "NewsRoom/AddAwarded",
               defaults: new { controller = "NewsRoom", action = "AddAwarded" },
               namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
       );

            context.MapRoute(
              name: "AdminEditAwarded",
              url: "NewsRoom/EditAwarded",
              defaults: new { controller = "NewsRoom", action = "EditAwarded" },
              namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
      );

            context.MapRoute(
               name: "PartialViewTenderResult",
               url: "NewsRoom/PartialResultView/{FilterValue}",
          defaults: new { controller = "NewsRoom", action = "PartialResultView" },
          namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
        );


            context.MapRoute(
            name: "PartialViewNotice",
            url: "NewsRoom/PartialViewNotice/{FilterValue}",
       defaults: new { controller = "NewsRoom", action = "PartialViewNotice" },
       namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
     );


            context.MapRoute(
          name: "PartialAwardedView",
          url: "NewsRoom/PartialAwardedView/{FilterValue}",
     defaults: new { controller = "NewsRoom", action = "PartialAwardedView" },
     namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
   );


            #endregion

            #region Annual Report

            context.MapRoute(
                    name: "AdminViewAnnualReport",
                    url: "Admin/NewsRoom/AnnualReport/Index",
                    defaults: new { controller = "AnnualReport", action = "Index" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                   name: "AnnualReportPartialView",
                   url: "Admin/NewsRoom/AnnualReport/PartialView/{PageIndex}",
                   defaults: new { controller = "AnnualReport", action = "PartialView" },
                   namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );


            context.MapRoute(
                      name: "AddAnnualReportPartialView",
                        url: "Admin/NewsRoom/AnnualReport/AddPartialView",
                   defaults: new { controller = "AnnualReport", action = "AddPartialView" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                      name: "EditAnnualReportPartialView",
                        url: "Admin/NewsRoom/AnnualReport/EditPartialView/{EncDetail}",
                   defaults: new { controller = "AnnualReport", action = "EditPartialView" },
                 namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                     name: "DeleteAnnualReport",
                       url: "Admin/NewsRoom/AnnualReport/Delete",
                  defaults: new { controller = "AnnualReport", action = "Delete" },
                namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
           );


            #endregion


            context.MapRoute(
                    name: "ForgetPassword",
                    url: "Admin/ForgetPassword",
                    defaults: new { controller = "Login", action = "ForgetPassword" },
                    namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                  name: "AdminLogout",
                  url: "Admin/Logout",
                  defaults: new { controller = "Login", action = "Logout" },
                  namespaces: new[] { "BTPTC.Web.Areas.Admin.Controllers" }
          );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}