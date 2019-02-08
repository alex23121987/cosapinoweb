using System.Web;
using System.Web.Optimization;

namespace Cosapi.ElCosapino.UI.Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Components/JQuery").Include(
                "~/Scripts/SOD/Components/JQuery/js/jquery-3.1.1.js",
                "~/Scripts/SOD/Components/JQuery/js/jquery-ui-1.12.1.custom.js",
                "~/Scripts/SOD/Components/JQuery/js/jquery_tokeninput.js",
                "~/Scripts/SOD/Components/JQuery/js/jquery.ajax_upload.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                      "~/Scripts/moment.js",
                      "~/Scripts/moment-locale-es.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetimepicker").Include(
                      "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-datetimepicker").Include(
                      "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/alertify").Include(
                    "~/Scripts/alertify.js"));

            bundles.Add(new StyleBundle("~/Content/alertify").Include(
                      "~/Content/alertifyjs/alertify.css",
                      "~/Content/alertifyjs/themes/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/ventana-unica").Include(
                      "~/Scripts/ventana-unica.js"));

            bundles.Add(new ScriptBundle("~/FrameworkStyle/js").Include(
                 "~/Scripts/SOD/Components/Bootstrap/js/bootstrap.js"
            ));
            bundles.Add(new StyleBundle("~/Components/JQueryCss").Include(
                "~/Scripts/SOD/Components/JQuery/css/jquery-ui-1.12.1.custom.css",
                "~/Scripts/SOD/Components/JQuery/css/token-input-facebook.css",
                "~/Scripts/SOD/Components/JQuery/css/token-input.css"
            ));

            bundles.Add(new StyleBundle("~/FrameworkStyle/css").Include(
                "~/Scripts/SOD/Components/Bootstrap/css/bootstrap.css"
            ));

            bundles.Add(new ScriptBundle("~/Components/TinyMCE").Include(
                       "~/Scripts/SOD/Components/TinyMCE/tinymce.min.js"
           ));

            bundles.Add(new ScriptBundle("~/Components/Codemaleon").Include(
                "~/Scripts/SOD/Components/Codemaleon/ns.js"
            ));

            bundles.Add(new ScriptBundle("~/Components/Canvas").Include(
                "~/Scripts/SOD/Util.js",
                "~/Scripts/SOD/Components/Canvas/Dialog/Dialog.js",
                "~/Scripts/SOD/Components/Canvas/Ajax/Ajax.js",
               "~/Scripts/SOD/Components/Canvas/Grid/Grid.js",
                "~/Scripts/SOD/Components/Canvas/TinyMCE/TinyMCE.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryValidate").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/index").Include(
            "~/Scripts/index.{version}.js"));
            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de creación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jsCanvasWebGrid").Include(
                        "~/Scripts/jsCanvasWebGrid.js"));

            bundles.Add(new StyleBundle("~/Content/CanvasGrid").Include("~/Content/CanvasGRID.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Components/CanvasCss").Include(
                        "~/Scripts/SOD/Components/Canvas/Dialog/Dialog.css"
                        ));

            // jquery datataables js files
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.js"));

            // jquery datatables css file
            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/Content/DataTables/css/dataTables.bootstrap.css"));
        }
    }
}
