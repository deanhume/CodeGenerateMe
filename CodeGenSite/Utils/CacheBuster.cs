using System.Web.Mvc;

namespace CodeGenSite.Utils
{
    public static class CacheBuster
    {
        public static string GetVersion(this HtmlHelper htmlHelper)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}