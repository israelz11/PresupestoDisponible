using PresupDisponible.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresupDisponible.Models.Custom
{
    public class CustomPresupuesto : DETALLEMOMENTOS
    {
        public string NombreProyecto { get; set; }
        public string NombrePartida { get; set; }

        public string Capitulo { get; set; }
        public string UnidadAdm { get; set; }
        public string Recurso { get; set; }
        public string Fuente { get; set; }
        public string TipoGasto { get; set; }

    }
}