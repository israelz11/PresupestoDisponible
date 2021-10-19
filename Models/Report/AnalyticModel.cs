using CrystalDecisions.CrystalReports.Engine;
using PresupDisponible.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PresupDisponible.Models.Report
{
    public class AnalyticModel
    {
        #region Properties

        private SIENCHAFAEntities dbcontex = new SIENCHAFAEntities();

        #endregion

        #region Methods

        public void UpdateAnalyticDatabase()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    dbcontex.UpdateAnalytic();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Stream GetPDFFormat(List<PRESUPUESTO_ANALITICOXUNIDAD> Presupuesto, string Partida)
        {
            try
            {
                
                using (ReportDocument rd = new ReportDocument())
                {
                    DataSet DTSource = this.GetDataSource(Presupuesto);
                    string ReportName = "";

                    ReportName = @ConfigurationManager.AppSettings["AnalyticFormatByDependency"];
                    rd.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), ReportName));
                    rd.SetDataSource(DTSource);
                    rd.SetParameterValue("P_PARTIDA", Partida);

                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    return stream;
                }   
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

        public DataSet GetDataSource(List<PRESUPUESTO_ANALITICOXUNIDAD> Presupuesto)
        {

            DTAnalyticByDependency DataSource = new DTAnalyticByDependency();
            DataTable dtPresupuesto = this.LINQToDataTable(Presupuesto);
            DataSource.Tables["PRESUPUESTO_ANALITICOXUNIDAD"].Merge(dtPresupuesto);

            return DataSource;
            
        }

        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        #endregion
    }
}