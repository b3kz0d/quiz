using Quiz.Resolver;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace Quiz.API
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            //Component initialization via MEF
            ComponentLoader.LoadContainer(container, ".\\bin", "Quiz.API.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "Quiz.BLL.dll");

        }
    }
}