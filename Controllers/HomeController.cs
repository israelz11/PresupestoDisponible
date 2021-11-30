using PresupDisponible.Data;
using PresupDisponible.Models.Presupuesto;
using PresupDisponible.Models.Report;
using PresupDisponible.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresupDisponible.Controllers
{
    public class HomeController : BaseController
    {
        #region Properties

        private PresupuestoModels presupuestoModels= new PresupuestoModels();
        private AnalyticModel analyticModel = new AnalyticModel();

        #endregion

        public ActionResult Index()
        {
            IndexView indexView = new IndexView();
            return View(indexView);
        }

        public ActionResult AnalyticView()
        {
            AnalyticView analyticView = new AnalyticView();
            Session["Presupuesto"] = analyticView.Presupuesto;
            return View(analyticView);
        }

        #region METODOS PARA CARGAR ARCHIVOS

        public async System.Threading.Tasks.Task<ActionResult> CargarArchivos(/*HttpPostedFileBase FileInput1, HttpPostedFileBase FileInput2*/)
        {

            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase FileInput1 = Request.Files["FileInput1"];
                    HttpPostedFileBase FileInput2 = Request.Files["FileInput2"];
                    await presupuestoModels.uploadfiles(FileInput1, FileInput2, Server);
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            catch (ArgumentException ex)
            {
                return ThrowJSONError(ex);
            }
        }

        #endregion
        public ActionResult DeleteDatabase()
        {
            try
            {
                return Json(presupuestoModels.Delete(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }

        public ActionResult SearchMensual(string Periodos, string Unidades, string Proyectos, string Partidas, string Fuentes, string Recursos, string Capitulos)
        {
            try
            {
                return Json(presupuestoModels.SearchMensual(Periodos.Split(',').Select(int.Parse).ToList(), Unidades.Split(',').ToList(), Proyectos.Split(',').ToList(), Partidas.Split(',').Select(int.Parse).ToList(), Fuentes.Split(',').Select(int.Parse).ToList(), Recursos.Split(',').Select(int.Parse).ToList(), Capitulos.Split(',').Select(int.Parse).ToList()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public ActionResult SearchAnual(string Periodos, string Unidades, string Proyectos, string Partidas, string Fuentes, string Recursos, string Capitulos)
        {
            try
            {
                return Json(presupuestoModels.SearchAnual(Periodos.Split(',').Select(int.Parse).ToList(), Unidades.Split(',').ToList(), Proyectos.Split(',').ToList(), Partidas.Split(',').Select(int.Parse).ToList(), Fuentes.Split(',').Select(int.Parse).ToList(), Recursos.Split(',').Select(int.Parse).ToList(), Capitulos.Split(',').Select(int.Parse).ToList()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public ActionResult SearchPresupuestoAnaliticoXUnidad(string Periodos, string Unidades, string Proyectos, string Partidas, string Fuentes, string Recursos, string Capitulos)
        {
            try
            {
                List<PRESUPUESTO_ANALITICOXUNIDAD> Presupuesto = presupuestoModels.SearchPresupuestoAnaliticoXUnidad(Periodos.Split(',').Select(int.Parse).ToList(), Unidades.Split(',').ToList(), Proyectos.Split(',').ToList(), Partidas.Split(',').Select(int.Parse).ToList(), Fuentes.Split(',').Select(int.Parse).ToList(), Recursos.Split(',').ToList(), Capitulos.Split(',').ToList());
                Session["Presupuesto"] = Presupuesto;
                return Json(Presupuesto, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public ActionResult GetPDFAnalyticFormat(string Partida)
        {
            try
            {
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return File(analyticModel.GetPDFFormat(Session["Presupuesto"] as List<PRESUPUESTO_ANALITICOXUNIDAD>, Partida), "application/pdf", "AnalyticFormat.pdf");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}