using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Diplom.Web.App_Start
{
  public class BundleConfig
  {
    public static void RegisterBundles(BundleCollection bundles)
    {
      // CSS style (bootstrap/inspinia)
      bundles.Add(new StyleBundle("~/Content/cssAdmin").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/animate.css",
                "~/Content/adminPanelStyle.css"));

      // Font Awesome icons
      bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

      // jQuery
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-2.2.3.min.js"));

      // Bootstrap
      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js"));

      // SlimScroll
      bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

      // Inspinia script
      bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                "~/Scripts/plugins/metisMenu/metisMenu.min.js",
                "~/Scripts/plugins/pace/pace.min.js",
                "~/Scripts/app/inspinia.js"));

      // Inspinia skin config script
      bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                "~/Scripts/app/skin.config.min.js"));

      // dataTables css styles
      bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                "~/Content/plugins/dataTables/datatables.min.css"));

      // dataTables 
      bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                "~/Scripts/plugins/dataTables/datatables.min.js"));

      // jeditable 
      bundles.Add(new ScriptBundle("~/plugins/jeditable").Include(
                "~/Scripts/plugins/jeditable/jquery.jeditable.js"));

      // Touch Spin Styless
      bundles.Add(new StyleBundle("~/plugins/touchSpinStyles").Include(
                "~/Content/plugins/touchspin/jquery.bootstrap-touchspin.min.css"));

      // Touch Spin
      bundles.Add(new ScriptBundle("~/plugins/touchSpin").Include(
                "~/Scripts/plugins/touchspin/jquery.bootstrap-touchspin.min.js"));

      // jasnyBootstrap styles
      bundles.Add(new StyleBundle("~/plugins/jasnyBootstrapStyles").Include(
                "~/Content/plugins/jasny/jasny-bootstrap.min.css"));

      // jasnyBootstrap 
      bundles.Add(new ScriptBundle("~/plugins/jasnyBootstrap").Include(
                "~/Scripts/plugins/jasny/jasny-bootstrap.min.js"));
    }
  }
}