using System.Web.Mvc;

namespace Diplom.Web.Areas.SocialSpace
{
    public class SocialSpaceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SocialSpace";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SocialSpace_default",
                "SocialSpace/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}