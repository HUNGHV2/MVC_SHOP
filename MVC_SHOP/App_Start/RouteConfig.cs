using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_SHOP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                namespaces: new []{"MVC_SHOP"},
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //code moi
        }
    }
}
