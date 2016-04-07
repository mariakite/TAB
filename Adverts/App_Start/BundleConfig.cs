using System.Web;
using System.Web.Optimization;

namespace Adverts
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region StylesBundles

            bundles.Add(new StyleBundle("~/Assets/GlobalMandatoryStyles").Include(
                    "~/Assets/global/plugins/font-awesome/css/font-awesome.min.css",
                    "~/Assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                    "~/Assets/global/plugins/bootstrap/css/bootstrap.min.css",
                    "~/Assets/global/plugins/uniform/css/uniform.default.css",
                    "~/Assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"));


            bundles.Add(new StyleBundle("~/Assets/ThemeGlobalStyles").Include(
                    "~/Assets/global/css/components-md.min.css",
                    "~/Assets/global/css/plugins-md.min.css"));

            bundles.Add(new StyleBundle("~/Assets/ThemeLayoutStyles").Include(
                    "~/Assets/layouts/layout3/css/layout.min.css",
                    "~/Assets/layouts/layout3/css/themes/default.min.css",
                    "~/Assets/layouts/layout3/css/custom.min.css",
                    "~/Assets/global/plugins/bootstrap-toastr/toastr.min.css"));

            #endregion

            #region ScriptsBundles
            bundles.Add(new ScriptBundle("~/Assets/CorePlugins").Include(
                     "~/Assets/global/plugins/jquery.min.js",
                     "~/Assets/global/plugins/bootstrap/js/bootstrap.min.js",
                     "~/Assets/global/plugins/js.cookie.min.js",
                     "~/Assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                     "~/Assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                     "~/Assets/global/plugins/jquery.blockui.min.js",
                     "~/Assets/global/plugins/uniform/jquery.uniform.min.js",
                     "~/Assets/global/plugins/bootstrap-confirmation/bootstrap-confirmation.min.js",
                     "~/Assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                     "~/Assets/global/plugins/bootstrap-toastr/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/Assets/GlobalScripts").Include(
                     "~/Assets/global/scripts/app.min.js"));

            bundles.Add(new ScriptBundle("~/Assets/LayoutScripts").Include(
                "~/Assets/global/plugins/jquery-ui/jquery-ui.customDraggable.min.js",
                    "~/Assets/layouts/layout3/scripts/layout.min.js",
                    "~/Assets/layouts/layout3/scripts/demo.min.js",
                    "~/Assets/layouts/global/scripts/quick-sidebar.min.js",
                    "~/Scripts/layout.events.js",
                    "~/Scripts/layout.settings.js"
                    )); //"~/Assets/pages/scripts/ui-toastr.min.js"
            #endregion
        }
    }
}
