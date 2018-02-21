using System.Web;
using System.Web.Optimization;

namespace SGCFVIEF.Presentacion
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/chosen.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need...........
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      //"~/Scripts/bootstrap-colorpicker.js",
                      //"~/Scripts/bootstrap-datepicker.js",
                      //"~/Scripts/excanvas.min.js",
                      //"~/Scripts/fullcalendar.min.js",
                      //"~/Scripts/jquery.dataTables.min.js",
                      //"~/Scripts/jquery.flot.min.js",
                      //"~/Scripts/jquery.flot.pie.min.js",
                      //"~/Scripts/jquery.flot.resize.min.js",
                      //"~/Scripts/jquery.gritter.min.js",
                      //"~/Scripts/jquery.peity.min.js",
                      //"~/Scripts/jquery.ui.custom.js",
                      //"~/Scripts/jquery.uniform.js",
                      //"~/Scripts/jquery.validate.js",
                      //"~/Scripts/jquery.wizard.js",
                      //"~/Scripts/maruti.calendar.js",
                      //"~/Scripts/maruti.charts.js",
                      //"~/Scripts/maruti.chat.js",
                      //"~/Scripts/maruti.dashboard.js",
                      //"~/Scripts/maruti.form_common.js",
                      //"~/Scripts/maruti.form_validation.js",
                      //"~/Scripts/maruti.interface.js",
                      //"~/Scripts/maruti.js",
                      //"~/Scripts/maruti.login.js",
                      //"~/Scripts/maruti.popover.js",
                      //"~/Scripts/maruti.tables.js",
                      //"~/Scripts/maruti.wizard.js",
                      //"~/Scripts/select2.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-responsive.min.css",
                      "~/Content/chosen.css",
                      "~/Content/themes/base/jquery-ui.min.css",
                      //"~/Content/colorpicker.css",
                      //"~/Content/datepicker.css",
                      //"~/Content/fullcalendar.css",
                      //"~/Content/jquery.gritter.css",
                      //"~/Content/maruti-login.css",
                      "~/Content/maruti-media.css",
                      "~/Content/maruti-style.css",
                      //"~/Content/select2.css",
                      //"~/Content/uniform.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/script-layaout").Include(
                                  "~/Scripts/Controladores/functions.js",
                                  "~/Scripts/Controladores/ajax-mvc.js"));

            //Mantenimiento de Canales
            bundles.Add(new ScriptBundle("~/bundles/mantenimiento-canales").Include(
                                  "~/Scripts/Controladores/Canal/canal-route.js",
                                  "~/Scripts/Controladores/Canal/canal-controller.js"));

            //Mantenimiento de SubCanales
            bundles.Add(new ScriptBundle("~/bundles/mantenimiento-subcanales").Include(
                                  "~/Scripts/Controladores/SubCanal/subcanal-route.js",
                                  "~/Scripts/Controladores/SubCanal/subcanal-controller.js"));

            //Mantenimiento de Vendedores
            bundles.Add(new ScriptBundle("~/bundles/mantenimiento-vendedores").Include(
                                  "~/Scripts/Controladores/Vendedor/vendedor-route.js",
                                  "~/Scripts/Controladores/Vendedor/vendedor-controller.js"));

            //Calculo de Comisiones
            bundles.Add(new ScriptBundle("~/bundles/calculo-comisiones").Include(
                                  "~/Scripts/Controladores/CalculoComision/calculocomision-route.js",
                                  "~/Scripts/Controladores/CalculoComision/calculocomision-controller.js"));

            //Generar Diferidos
            bundles.Add(new ScriptBundle("~/bundles/generar-diferidos").Include(
                                  "~/Scripts/Controladores/CalculoDiferido/calculodiferido-route.js",
                                  "~/Scripts/Controladores/CalculoDiferido/calculodiferido-controller.js"));

            //Reporte Solicitudes
            bundles.Add(new ScriptBundle("~/bundles/reporte-solicitud").Include(
                                  "~/Scripts/Controladores/ReporteSolicitud/reportesolicitud-controller.js",
                                  "~/Scripts/Controladores/ReporteSolicitud/reportesolicitud-route.js"));

            //Mantenimiento de Reclamo
            bundles.Add(new ScriptBundle("~/bundles/mantenimiento-reclamo").Include(
                                  "~/Scripts/Controladores/Reclamo/reclamo-controller.js",
                                  "~/Scripts/Controladores/Reclamo/reclamo-route.js"));

            //Mantenimiento de Tarifario
            bundles.Add(new ScriptBundle("~/bundles/mantenimiento-tarifa").Include(
                                  "~/Scripts/Controladores/Tarifario/tarifario-controller.js",
                                  "~/Scripts/Controladores/Tarifario/tarifario-route.js"));

            //Reporte de Solicitudes Aprobadas
            bundles.Add(new ScriptBundle("~/bundles/reporte-solicitudes-aprobadas").Include(
                                  "~/Scripts/Controladores/ReporteSolicitud/reportesolicitud-controller.js",
                                  "~/Scripts/Controladores/ReporteSolicitud/reportesolicitud-route.js"));

            //Reporte de Comisiones
            bundles.Add(new ScriptBundle("~/bundles/reporte-comisiones").Include(
                                  "~/Scripts/Controladores/ReporteComisiones/reportecomisiones-controller.js",
                                  "~/Scripts/Controladores/ReporteComisiones/reportecomisiones-route.js"));

        }
    }
}
