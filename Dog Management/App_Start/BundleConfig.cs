using System.Web.Optimization;

namespace Dog_Management
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/assets")
                .IncludeDirectory("~/External/Assets", "*js", false)
                .Include(
                        "~/External/angular.js"
                        , "~/External/angular-aria.js"
                        , "~/External/angular-locale_ru-ru.js"
                        , "~/External/angular-message-format.js"
                        , "~/External/angular-messages.js"
                        , "~/External/angular-route.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/applications").Include(
                        "~/Scripts/app.js"
                )
                .IncludeDirectory("~/Scripts/Modules", "*.js", true)
                .IncludeDirectory("~/Scripts/Services", "*.js", true)
                .IncludeDirectory("~/Scripts/Controllers", "*.js", true)
                );
            

            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory(
                      "~/Content", "*.css", true));
        }
    }
}