using System.Web;
using System.Web.Optimization;

namespace Vnet
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            ScriptBundle scriptBundle = new ScriptBundle("~/js");
            string[] scriptArray =
            {
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery-2.1.1.min.js",
                "~/Content/Layout/js/material.min.js",
                "~/Content/Layout/js/chartist.min.js",
                "~/Content/Layout/js/arrive.min.js",
                "~/Content/Layout/js/perfect-scrollbar.jquery.min.js",
                "~/Content/Layout/js/bootstrap-notify.js",
                "~/Content/Layout/js/material-dashboard.js",
                "~/Content/Layout/js/demo.js",
                "~/Scripts/bootbox.min.js",
                "~/Scripts/toastr.js",
                "~/Scripts/DataTables/jquery.datatables.min.js",
                "~/Scripts/DataTables/datatables.bootstrap.min.js",
                "~/Scripts/pestañas.js",
                "~/Scripts/select2.js",
                "~/Scripts/jquery.steps.js",
                "~/Scripts/layoutScripts.js",
                "~/Scripts/Datepicker.js",
                "~/Scripts/Scripts/jquery-ui-1.12.1.min.js",
                "~/Scripts/Scripts/jquery-ui.multidatespicker.js",
                "~/Scripts/DatatableBase.js",
                "~/Scripts/sweetalert2.js"



            };
            scriptBundle.Include(scriptArray);
            scriptBundle.IncludeDirectory("~/Scripts/", "*.js");
            bundles.Add(scriptBundle);

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Styles/css").Include(
                "~/Content/bootstrap.min.css",
                "~/content/toastr.css",
                "~/Content/Layout/css/material-dashboard.css",
            "~/Content/Layout/css/demo.css",
            "~/Content/Site.css",
            "~/Content/css/select2.css",
            "~/Content/css/bs-callout.css",
            "~/Content/DataTables/css/dataTables.bootstrap.min.css",
            "~/Content/jquery.steps.css",
            "~/Content/jquery-ui.multidatespicker.css",
            "~/Content/css/FixDataTabalesInsideSteps.css"
            ));
        }
    }
}
