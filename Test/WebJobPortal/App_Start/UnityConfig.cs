using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using WebJobPortal.UserServiceReference;

namespace WebJobPortal.App_Start
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IUserService, UserServiceClient>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}