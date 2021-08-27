using PresupDisponible.Models.Presupuesto;
using PresupDisponible.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresupDisponible.Controllers
{
    public class HomeController : Controller
    {
        private PresupuestoModels presupuestoModels= new PresupuestoModels();
        public ActionResult Index()
        {
            IndexView indexView = new IndexView();
            return View(indexView);
        }

        #region METODOS PARA CARGAR ARCHIVOS
        public ActionResult CargarArchivos(/*HttpPostedFileBase FileInput1, HttpPostedFileBase FileInput2*/)
        {

            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase FileInput1 = Request.Files["FileInput1"];
                    HttpPostedFileBase FileInput2 = Request.Files["FileInput2"];

                    return Json(presupuestoModels.uploadfiles(FileInput1, FileInput2, Server), JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            catch (ArgumentException e)
            {
                Response.StatusCode = 400;
                return new ContentResult { Content = e.Message };
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

    }
}