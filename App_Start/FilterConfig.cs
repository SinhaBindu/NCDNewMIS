using NCDNewMIS.Models;
using System.Web;
using System.Web.Mvc;

namespace NCDNewMIS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());//by default
           // filters.Add(new CustomErrorHandler());
        }
    }
}
