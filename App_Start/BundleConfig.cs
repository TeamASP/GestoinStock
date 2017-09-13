using System.Web;
using System.Web.Optimization;

namespace StockApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // telerik Kendo=========

            bundles.Add(new ScriptBundle("~/bundles/kendo/2016.3.1118").Include(
                "~/Scripts/kendo/2016.3.1118/kendo.all.min.js",
                "~/Scripts/kendo/2016.3.1118/kendo.datepicker.min.js",
                // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
                "~/Scripts/kendo/2016.3.1118/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/2016.3.1118/css").Include(
                "~/Content/kendo/2016.3.1118/kendo.common-bootstrap.min.css",
                "~/Content/kendo/2016.3.1118/kendo.bootstrap.min.css"));


            //add
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js",
            "~/Scripts/jquery-ui.unobtrusive-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/themes/base/all.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/datepicker-fr.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
        "~/Content/themes/base/jquery.ui.core.css",
        "~/Content/themes/base/jquery.ui.datepicker.css",
        "~/Content/themes/base/jquery.ui.theme.css")); 
            //=======

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                       "~/Scripts/angular.min.js",
                      "~/Scripts/locales/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/locales/bootstrap-datetimepicker.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                        "~/Scripts/locales/bootstrap-datepicker.fr-CH.min.js",
                        "~/Scripts/datepicker-fr.js",
                       "~/Scripts/MonScripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/site.css"));
        }
    }
}
