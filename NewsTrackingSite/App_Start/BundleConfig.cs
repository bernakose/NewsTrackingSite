using System.Web;
using System.Web.Optimization;

namespace NewsTrackingSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // !! Bunun .min.js eklenmesi gerekebilir !!
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // !! Bu kaldırılabilir !!
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            // !! Burada respond kaldırılabilir !!
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //            "~/Scripts/bootstrap.js",
            //            "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/libjs").Include(
                        "~/Scripts/jquery.easing.min.js",
                        "~/Scripts/lib/jquery.appear.js",
                        "~/Scripts/lib/owl-carousel/owl.carousel.min.js",
                        "~/Scripts/lib/magnific-popup/jquery.magnific-popup.min.js",
                        "~/Scripts/lib/video/jquery.mb.YTPlayer.js",
                        "~/Scripts/lib/flipclock/flipclock.js",
                        "~/Scripts/lib/jquery.animateNumber.js",
                        "~/Scripts/lib/waypoints.min.js",
                        "~/Scripts/main.js",
                        "~/Scripts/jquery.nivo.slider.js"));

            bundles.Add(new ScriptBundle("~/Content/libcss").Include(
                        "~/Scripts/lib/owl-carousel/owl.carousel.css",
                        "~/Scripts/lib/owl-carousel/owl.theme.css",
                        "~/Scripts/lib/owl-carousel/owl.transitions.css",
                        "~/Scripts/lib/magnific-popup/magnific-popup.css",
                        "~/Scripts/lib/video/YTPlayer.css",
                        "~/Scripts/lib/flipclock/flipclock.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/style.css",
                        "~/Content/animate.css",
                        "~/Content/slider.css",
                        "~/Content/PagedList.css"));

            //---------------------------------ADMIN------------------------------------

            bundles.Add(new ScriptBundle("~/assets/css").Include(
                        "~/Areas/Admin/assets/css/bootstrap.min.css",
                        "~/Areas/Admin/assets/css/sb-admin.css",
                        "~/Areas/Admin/assets/css/plugins/morris.css",
                        "~/Areas/Admin/assets/font-awesome-4.1.0/css/font-awesome.min.css",
                        "~/Areas/Admin/assets/css/plugins/chosen.min.css"));

            bundles.Add(new ScriptBundle("~/assets/js").Include(
                        "~/Areas/Admin/assets/js/jquery-1.11.0.js",
                        "~/Areas/Admin/assets/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/assets/js/plugins").Include(
                        "~/Areas/Admin/assets/js/plugins/morris/raphael.min.js",
                        "~/Areas/Admin/assets/js/plugins/morris/morris.js",
                        "~/Areas/Admin/assets/js/plugins/morris/morris-data.js",
                        "~/Areas/Admin/assets/js/plugins/chosen/chosen.jquery.min.js",
                        "~/Areas/Admin/assets/js/plugins/chosen/chosen.prote.min.js"));
        }
    }
}