using BTPTC.Persistence.Implementation;
using BTPTC.Persistence.Interface;
using BTPTC.Service.Implementation;
using BTPTC.Service.Interface;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace BTPTC.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAnnualReportDao, AnnualReportDao>();
            container.RegisterType<IGalleryDao, GalleryDao>();
            container.RegisterType<IUserAccountDao, UserAccountDao>();
            container.RegisterType<IFacilityDao, FacilityDao>();
            container.RegisterType<IMaintenanceDao, MaintenanceDao>();
            container.RegisterType<IUtilityService, UtilityService>();
            container.RegisterType<IEventsDao, EventsDao>();
            container.RegisterType<INoticeDao, NoticeDao>();
            container.RegisterType<INewsLetterDao, NewsLetterDao>();
            container.RegisterType<IMediaReleaseDao, MediaReleaseDao>();
            container.RegisterType<ITenderDao, TenderDao>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}