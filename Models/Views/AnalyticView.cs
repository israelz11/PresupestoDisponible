using PresupDisponible.Data;
using PresupDisponible.Models.Custom;
using PresupDisponible.Models.Presupuesto;
using PresupDisponible.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresupDisponible.Models.Views
{
    public class AnalyticView
    {
        private AnalyticModel analyticModel = new AnalyticModel();
        private PresupuestoModels presupuestoModel = new PresupuestoModels();
        public List<PRESUPUESTO_ANALITICOXUNIDAD> Presupuesto = new List<PRESUPUESTO_ANALITICOXUNIDAD>();
        public List<CustomData> Partids = new List<CustomData>();
        public List<CustomData> Proyects = new List<CustomData>();
        public List<CustomData> Capitulos = new List<CustomData>();
        public List<CustomData> Fuente = new List<CustomData>();
        public List<CustomData> Recurso = new List<CustomData>();
        public List<CustomData> UnidadADM = new List<CustomData>();

        public AnalyticView()
        {
            analyticModel.UpdateAnalyticDatabase();
            this.Presupuesto = presupuestoModel.getPresupuestoAnaliticoXUnidad();
            this.Partids = presupuestoModel.getPartid();
            this.Proyects = presupuestoModel.getProgPresupuest();
            this.Recurso = presupuestoModel.getRecurso();
            this.UnidadADM = presupuestoModel.getUNIDADM();
            this.Capitulos = presupuestoModel.getCapitulo();
            this.Fuente = presupuestoModel.getFuente();
        }


    }
}