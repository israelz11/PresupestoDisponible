﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PresupDisponible.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SIENCHAFAEntities : DbContext
    {
        public SIENCHAFAEntities()
            : base("name=SIENCHAFAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Proyectos> Proyectos { get; set; }
        public virtual DbSet<Atuevaluacion_demo> Atuevaluacion_demo { get; set; }
        public virtual DbSet<DETALLEMOMENTOS> DETALLEMOMENTOS { get; set; }
        public virtual DbSet<VtPresupuesto> VtPresupuesto { get; set; }
        public virtual DbSet<SPARTIDAS> SPARTIDAS { get; set; }
        public virtual DbSet<PRESUPUESTOS_ANALITICO> PRESUPUESTOS_ANALITICO { get; set; }
        public virtual DbSet<PRESUPUESTOS_ANALITICO_1000> PRESUPUESTOS_ANALITICO_1000 { get; set; }
        public virtual DbSet<PROYPRES22> PROYPRES22 { get; set; }
        public virtual DbSet<PRESUPUESTO_ANALITICOXUNIDAD> PRESUPUESTO_ANALITICOXUNIDAD { get; set; }
        public virtual DbSet<RESUMEN_COMPARATIVO> RESUMEN_COMPARATIVO { get; set; }
        public virtual DbSet<RESUMEN_COMPARATIVO_UNIDAD> RESUMEN_COMPARATIVO_UNIDAD { get; set; }
    
        public virtual int UpdateAnalytic()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateAnalytic");
        }
    }
}
