using PresupDisponible.Data;
using PresupDisponible.Models.Custom;
using PresupDisponible.Models.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresupDisponible.Models.Views
{
    public class IndexView
    {
        private PresupuestoModels presupuestoModel = new PresupuestoModels();
        public List<SPARTIDAS> Presupuesto = new List<SPARTIDAS>();
        public List<CustomData> Partids = new List<CustomData>();
        public List<CustomData> Proyects = new List<CustomData>();
        public List<CustomData> Capitulos = new List<CustomData>();
        public List<CustomData> Fuente = new List<CustomData>();
        public List<CustomData> Recurso = new List<CustomData>();
        public List<CustomData> UnidadADM = new List<CustomData>();

        public IndexView()
        {
            this.Presupuesto = presupuestoModel.getPresupuesto();
            this.Partids = presupuestoModel.getPartid();
            this.Proyects = presupuestoModel.getProyect();
            this.Recurso = presupuestoModel.getRecurso();
            this.UnidadADM = presupuestoModel.getUNIDADM();
            this.Capitulos = presupuestoModel.getCapitulo();
            this.Fuente = presupuestoModel.getFuente();
        }


    }
}