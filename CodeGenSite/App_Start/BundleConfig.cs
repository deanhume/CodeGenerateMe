using System.Web.Optimization;

namespace CodeGenSite.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254726
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bootstrap/js").Include(
                "~/content/js/generate.js"
                    ));

            bundles.Add(new StyleBundle("~/bootstrap/css").Include(
                "~/content/css/bootstrap.css",
                "~/content/css/bootstrap-responsive.css"
                    ));
        }
    }
}