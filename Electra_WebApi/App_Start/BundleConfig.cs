﻿using System.Web.Optimization;

namespace Electra_WebApi
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/DataTables/jquery.dataTables.min.js",
                       "~/Scripts/DataTables/dataTables.bootstrap.js",
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/bootstrap.css",
                        "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));
        }
    }
}
