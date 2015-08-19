using System.Web;
using System.Web.Mvc;

namespace NetWebApi_Json_LD
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
