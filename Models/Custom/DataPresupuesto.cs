using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresupDisponible.Models.Custom
{
    public class DataPresupuesto
    {
        public String IdProyecto { get; set; }
        public int ClvPartida { get; set; }
        public List<LCalendario> Modificado { get; set; }
        public List<LCalendario> Comprometido { get; set; }
        public List<LCalendario> Devengado { get; set; }
        public List<LCalendario> Ejercido { get; set; }
    }

    public class LCalendario
    {
        public enum Meses
        {
            Enero = 1,
            Febrero = 2,
            Marzo = 3,
            Abril = 4,
            Mayo = 5,
            Junio = 6,
            Julio = 7,
            Agosto = 8,
            Septiembre = 9,
            Octubre = 10,
            Noviembre = 11,
            Diciembre = 12
        }
        public Meses Mes { get; set; }
        public float Monto { get; set; }
    }
}