using System.Web.Mvc;

namespace Apex.WebAPI.Areas.Core
{
    public class CoreAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Core";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Core_default",
                "Core/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}