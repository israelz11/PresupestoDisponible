using Excel;
using OfficeOpenXml;
using PresupDisponible.Data;
using PresupDisponible.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PresupDisponible.Models.Presupuesto
{
    
    public class PresupuestoModels
    {
        private SIENCHAFAEntities dbcontex = new SIENCHAFAEntities();

        public async Task<string> uploadfiles(HttpPostedFileBase fileInput1, HttpPostedFileBase fileInput2, HttpServerUtilityBase server)
        {
            try
            {
                await Task.Run(async () => {

                    //Guardar el archivo como temporal
                    string TempDirectory = Path.Combine(this.GetTemporaryDirectory() + "\\");
                    string FileName1 = TempDirectory + "Layout_" + fileInput1.FileName;
                    string FileName2 = "";

                    if (fileInput2 != null)
                        FileName2 = TempDirectory + "Layout_" + fileInput2.FileName;

                    fileInput1.SaveAs(FileName1);

                    if(fileInput2!= null)
                        fileInput2.SaveAs(FileName2);

                    Stream stream1 = fileInput1.InputStream;
                    Stream stream2 = Stream.Null;
                    if (fileInput2 != null)
                        stream2 = fileInput2.InputStream;

                    // We return the interface, so that
                    IExcelDataReader reader1 = null;
                    IExcelDataReader reader2 = null;

                    if (fileInput1.FileName.EndsWith(".xls"))
                    {
                        reader1 = ExcelReaderFactory.CreateBinaryReader(stream1);
                    }
                    else if (fileInput1.FileName.EndsWith(".xlsx"))
                    {
                        reader1 = ExcelReaderFactory.CreateOpenXmlReader(stream1);
                    }
                    if (fileInput2 != null)
                    {
                        if (fileInput2.FileName.EndsWith(".xls"))
                        {
                            reader2 = ExcelReaderFactory.CreateBinaryReader(stream2);
                        }
                        else if (fileInput2.FileName.EndsWith(".xlsx"))
                        {
                            reader2 = ExcelReaderFactory.CreateOpenXmlReader(stream2);
                        }
                    }

                    reader1.IsFirstRowAsColumnNames = true;
                    if (fileInput2 != null)
                        reader2.IsFirstRowAsColumnNames = true;

                    DataSet result1 = reader1.AsDataSet();
                    DataSet result2 = null;
                    if (fileInput2 != null)
                        result2 = reader2.AsDataSet();

                    reader1.Close();
                    if (fileInput2 != null)
                        reader2.Close();

                    DataTable TSheet1 = result1.Tables[0];
                    DataTable TSheet2 = new DataTable();
                    if (fileInput2 != null)
                        TSheet2 = result2.Tables[0];

                    #region File1

                    List<DataPresupuesto> data = new List<DataPresupuesto>();

                    int currentRow = 5;

                    int totalRow = TSheet1.Rows.Count;
                    //foreach (DataRow row in TSheet1.Rows)
                    for (int nrow = currentRow; nrow < totalRow; nrow++)
                    {
                        DataRow row = TSheet1.Rows[nrow];

                        String NumProject = row[1].ToString();
                        //Encuentra la primer partida....
                        DataRow partidRow = TSheet1.Rows[nrow + 1];

                        List<LCalendario> Modificado = new List<LCalendario>();
                        List<LCalendario> Comprometido = new List<LCalendario>();
                        List<LCalendario> Devengado = new List<LCalendario>();
                        List<LCalendario> Ejercido = new List<LCalendario>();

                        DataRow modificadoRow = TSheet1.Rows[nrow + 2];
                        DataRow comprometidoRow = TSheet1.Rows[nrow + 3];
                        DataRow devengadoRow = TSheet1.Rows[nrow + 4];
                        DataRow ejercidoRow = TSheet1.Rows[nrow + 5];



                        //Sacar los valores del modificado
                        for (int col = 3; col <= 14; col++)
                            Modificado.Add(new LCalendario { Mes = (LCalendario.Meses)(col - 2), Monto = float.Parse(modificadoRow[col].ToString()) });

                        //Sacar los valores del Comprometido
                        for (int col = 3; col <= 14; col++)
                            Comprometido.Add(new LCalendario { Mes = (LCalendario.Meses)(col - 2), Monto = float.Parse(comprometidoRow[col].ToString()) });

                        //Sacar los valores del Devengado
                        for (int col = 3; col <= 14; col++)
                            Devengado.Add(new LCalendario { Mes = (LCalendario.Meses)(col - 2), Monto = float.Parse(devengadoRow[col].ToString()) });

                        //Sacar los valores del Ejercido
                        for (int col = 3; col <= 14; col++)
                            Ejercido.Add(new LCalendario { Mes = (LCalendario.Meses)(col - 2), Monto = float.Parse(ejercidoRow[col].ToString()) });




                        data.Add(new DataPresupuesto
                        {
                            IdProyecto = NumProject,
                            ClvPartida = int.Parse(partidRow[1].ToString()),
                            Modificado = Modificado.ToList(),
                            Comprometido = Comprometido.ToList(),
                            Devengado = Devengado.ToList(),
                            Ejercido = Ejercido.ToList(),
                        });

                        //Al final recorrer la lista data y guardar los valores en las tablas necesarias

                        nrow = nrow + 6;
                    }
                    #endregion file 1

                    await this.saveDatabase(data, (fileInput2 != null ? TSheet2.Rows: null));

                    //int totalRow2 = TSheet2.Rows.Count;

                });

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
 
        }

        public List<VtPresupuesto> SearchMensual(List<int> Periodos, List<string> Unidades, List<string> Proyectos, List<int> Partidas, List<int> Fuentes, List<int> Recursos, List<int> Capitulos)
        {
            try
            {
                if (Periodos[0] == 0)
                    Periodos = this.getPeriodos();

                if (Unidades[0].Equals("0"))
                    Unidades = this.getUNIDADM().Select(x => x.Key).ToList();

                if (Proyectos[0] == "0")
                    Proyectos = this.getProyect().Select(x => x.Key).ToList();

                if (Partidas[0] == 0)
                    Partidas = this.getPartid().Select(x => int.Parse(x.Key)).ToList();

                if (Fuentes[0] == 0)
                    Fuentes = this.getFuente().Select(x => int.Parse(x.Key)).ToList();

                if (Recursos[0] == 0)
                    Recursos = this.getRecurso().Select(x => int.Parse(x.Key)).ToList();

                if (Capitulos[0] == 0)
                    Capitulos = this.getCapitulo().Select(x => int.Parse(x.Key)).ToList();

                using (dbcontex = new SIENCHAFAEntities())
                {
                    PresupuestoModels presupuestoModels = new PresupuestoModels();
                    List<VtPresupuesto> Result = dbcontex.VtPresupuesto.Where(x =>
                            Proyectos.Contains(x.ID_PROYECTO) &&
                            Partidas.Contains(x.CLV_PARTID) &&
                            Unidades.Contains(x.CLV_UNIDADM) &&
                            Fuentes.Contains(x.CLV_FUENTE.Value) &&
                            Recursos.Contains(x.CLV_RECURSO.Value) &&
                            Capitulos.Contains(x.CLV_CAPITULO.Value)
                        )
                        .OrderBy(x=>x.ID_PROYECTO)
                        .ThenBy(x=>x.CLV_PARTID)
                        .ToList();

                    return Result;
                }
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

        public List<SPARTIDAS> SearchAnual(List<int> Periodos, List<string> Unidades, List<string> Proyectos, List<int> Partidas, List<int> Fuentes, List<int> Recursos, List<int> Capitulos)
        {
            try
            {
                if (Periodos[0] == 0)
                    Periodos = this.getPeriodos();

                if (Unidades[0].Equals("0"))
                    Unidades = this.getUNIDADM().Select(x => x.Key).ToList();

                if (Proyectos[0] == "0")
                    Proyectos = this.getProyect().Select(x => x.Key).ToList();

                if (Partidas[0] == 0)
                    Partidas = this.getPartid().Select(x => int.Parse(x.Key)).ToList();

                if (Fuentes[0] == 0)
                    Fuentes = this.getFuente().Select(x => int.Parse(x.Key)).ToList();

                if (Recursos[0] == 0)
                    Recursos = this.getRecurso().Select(x => int.Parse(x.Key)).ToList();

                if (Capitulos[0] == 0)
                    Capitulos = this.getCapitulo().Select(x => int.Parse(x.Key)).ToList();

                using (dbcontex = new SIENCHAFAEntities())
                {
                    PresupuestoModels presupuestoModels = new PresupuestoModels();
                    List<SPARTIDAS> Result = dbcontex.SPARTIDAS.Where(x =>
                            Proyectos.Contains(x.ID_PROYECTO) &&
                            Partidas.Contains(x.CLV_PARTID) &&
                            Unidades.Contains(x.CLV_UNIDADM) &&
                            Fuentes.Contains(x.CLV_FUENTE.Value) &&
                            Recursos.Contains(x.CLV_RECURSO.Value) &&
                            Capitulos.Contains(x.CLV_CAPITULO.Value)
                        )
                        .OrderBy(x => x.ID_PROYECTO)
                        .ThenBy(x => x.CLV_PARTID)
                        .ToList();

                    return Result;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public String Delete()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    dbcontex.Database.ExecuteSqlCommand("DELETE FROM Atuevaluacion_demo");
                    dbcontex.Database.ExecuteSqlCommand("DELETE FROM DETALLEMOMENTOS");

                    return "";
                }
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }

        #region CreateDirTemporal
        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
        #endregion CreateDirTemporal

        #region Database
        public async Task<string> saveDatabase(List<DataPresupuesto> data, DataRowCollection data2) {
            try
            {
                //List<DataPresupuesto> data2 = data.Where(x => x.IdProyecto == 1).ToList();
                List<DETALLEMOMENTOS> detallesdb = dbcontex.DETALLEMOMENTOS.ToList();
                List<Atuevaluacion_demo> autoevaDetalledb = dbcontex.Atuevaluacion_demo.ToList();


                #region file 1

                foreach (DataPresupuesto row in data)
                {

                    if (detallesdb.Where(x => x.ID_PROYECTO == row.IdProyecto && x.CLV_PARTID == row.ClvPartida).Count() == 0)
                    {
                        /*Guarda momento del modificado*/
                        if (row.Modificado.Count > 0)
                        {
                            DETALLEMOMENTOS dbmoments = new DETALLEMOMENTOS();

                            dbmoments.ID_PROYECTO = row.IdProyecto;
                            dbmoments.CLV_PARTID = row.ClvPartida;
                            dbmoments.MOMENTO = "MODIFICADO";
                            foreach (LCalendario momento in row.Modificado)
                            {
                                if (momento.Mes == LCalendario.Meses.Enero)
                                    dbmoments.ENERO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Febrero)
                                    dbmoments.FEBRERO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Marzo)
                                    dbmoments.MARZO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Abril)
                                    dbmoments.ABRIL = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Mayo)
                                    dbmoments.MAYO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Junio)
                                    dbmoments.JUNIO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Julio)
                                    dbmoments.JULIO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Agosto)
                                    dbmoments.AGOSTO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Septiembre)
                                    dbmoments.SEPTIEMBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Octubre)
                                    dbmoments.OCTUBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Noviembre)
                                    dbmoments.NOVIEMBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Diciembre)
                                    dbmoments.DICIEMBRE = momento.Monto;
                            }
                            dbcontex.DETALLEMOMENTOS.Add(dbmoments);
                            await dbcontex.SaveChangesAsync();
                        }

                        /*Guarda momento del COMPROMETIDO*/
                        if (row.Comprometido.Count > 0)
                        {
                            DETALLEMOMENTOS dbmoments = new DETALLEMOMENTOS();

                            dbmoments.ID_PROYECTO = row.IdProyecto;
                            dbmoments.CLV_PARTID = row.ClvPartida;
                            dbmoments.MOMENTO = "COMPROMETIDO";
                            foreach (LCalendario momento in row.Comprometido)
                            {
                                if (momento.Mes == LCalendario.Meses.Enero)
                                    dbmoments.ENERO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Febrero)
                                    dbmoments.FEBRERO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Marzo)
                                    dbmoments.MARZO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Abril)
                                    dbmoments.ABRIL = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Mayo)
                                    dbmoments.MAYO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Junio)
                                    dbmoments.JUNIO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Julio)
                                    dbmoments.JULIO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Agosto)
                                    dbmoments.AGOSTO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Septiembre)
                                    dbmoments.SEPTIEMBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Octubre)
                                    dbmoments.OCTUBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Noviembre)
                                    dbmoments.NOVIEMBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Diciembre)
                                    dbmoments.DICIEMBRE = momento.Monto;
                            }

                            dbcontex.DETALLEMOMENTOS.Add(dbmoments);
                            await dbcontex.SaveChangesAsync();

                        }

                        /*Guarda momento del DEVENGADO*/
                        if (row.Devengado.Count > 0)
                        {
                            DETALLEMOMENTOS dbmoments = new DETALLEMOMENTOS();

                            dbmoments.ID_PROYECTO = row.IdProyecto;
                            dbmoments.CLV_PARTID = row.ClvPartida;
                            dbmoments.MOMENTO = "DEVENGADO";
                            foreach (LCalendario momento in row.Devengado)
                            {
                                if (momento.Mes == LCalendario.Meses.Enero)
                                    dbmoments.ENERO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Febrero)
                                    dbmoments.FEBRERO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Marzo)
                                    dbmoments.MARZO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Abril)
                                    dbmoments.ABRIL = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Mayo)
                                    dbmoments.MAYO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Junio)
                                    dbmoments.JUNIO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Julio)
                                    dbmoments.JULIO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Agosto)
                                    dbmoments.AGOSTO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Septiembre)
                                    dbmoments.SEPTIEMBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Octubre)
                                    dbmoments.OCTUBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Noviembre)
                                    dbmoments.NOVIEMBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Diciembre)
                                    dbmoments.DICIEMBRE = momento.Monto;
                            }
                            dbcontex.DETALLEMOMENTOS.Add(dbmoments);
                            await dbcontex.SaveChangesAsync();
                        }

                        /*Guarda momento del EJERCIDO*/
                        if (row.Ejercido.Count > 0)
                        {
                            DETALLEMOMENTOS dbmoments = new DETALLEMOMENTOS();

                            dbmoments.ID_PROYECTO = row.IdProyecto;
                            dbmoments.CLV_PARTID = row.ClvPartida;
                            dbmoments.MOMENTO = "EJERCIDO";
                            foreach (LCalendario momento in row.Ejercido)
                            {
                                if (momento.Mes == LCalendario.Meses.Enero)
                                    dbmoments.ENERO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Febrero)
                                    dbmoments.FEBRERO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Marzo)
                                    dbmoments.MARZO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Abril)
                                    dbmoments.ABRIL = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Mayo)
                                    dbmoments.MAYO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Junio)
                                    dbmoments.JUNIO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Julio)
                                    dbmoments.JULIO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Agosto)
                                    dbmoments.AGOSTO = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Septiembre)
                                    dbmoments.SEPTIEMBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Octubre)
                                    dbmoments.OCTUBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Noviembre)
                                    dbmoments.NOVIEMBRE = momento.Monto;
                                if (momento.Mes == LCalendario.Meses.Diciembre)
                                    dbmoments.DICIEMBRE = momento.Monto;
                            }
                            dbcontex.DETALLEMOMENTOS.Add(dbmoments);
                            await dbcontex.SaveChangesAsync();
                        }
                    }

                }

                #endregion file 1

                #region file 2

                if(data2!= null)
                {
                    foreach (DataRow row in data2)
                    {

                        if (Int32.TryParse(row[0].ToString(), out int id_dependencia))
                        {
                            String iddependencia = id_dependencia.ToString();
                            if (autoevaDetalledb.Where(x => x.ID_PROYECTO.Equals(iddependencia) && x.CLV_PARTIDA.Equals(row[5])).Count() == 0)
                            {
                                Atuevaluacion_demo auto = new Atuevaluacion_demo();
                                auto.CLV_UNIDADM = row[0].ToString();
                                auto.UNIDADM = row[1].ToString();
                                auto.TIPO_GASTO = row[2].ToString();
                                auto.CLV_CAPITULO = row[3].ToString();
                                auto.CAPITULO = row[4].ToString();
                                auto.CLV_PARTIDA = row[5].ToString();
                                auto.PARTIDA = row[6].ToString();
                                auto.CLV_LOCALIDAD = row[7].ToString();
                                auto.LOCALIDAD = row[8].ToString();
                                auto.CLV_FUENTE = row[9].ToString();
                                auto.FUENTE = row[10].ToString();
                                auto.ABR_FUENTE = row[11].ToString();
                                auto.CLV_RECURSO = row[12].ToString();
                                auto.RECURSO = row[13].ToString();
                                auto.ID_PROYECTO = row[14].ToString();
                                auto.PROYECTO = row[15].ToString();
                                auto.FECHA_INICIO = row[16].ToString();
                                auto.FECHA_TERMINO = row[17].ToString();
                                auto.N_PROGRAMA = row[18].ToString();
                                auto.PROGRAMA = row[19].ToString();
                                auto.PROG_PRESUP = row[20].ToString();
                                auto.CLV_EJE = row[21].ToString();
                                auto.EJE = row[22].ToString();
                                auto.CLV_PROG_ALIN = row[23].ToString();
                                auto.PROG_ALIN = row[24].ToString();
                                auto.CLV_OBJ_ALIN = row[25].ToString();
                                auto.OBJ_ALIN = row[26].ToString();
                                auto.CLV_EST_ALIN = row[27].ToString();
                                auto.EST_ALIN = row[28].ToString();
                                auto.CLV_LIN_ACC = row[29].ToString();
                                auto.LIN_ACC = row[30].ToString();
                                auto.CLV_IND_ALIN = row[31].ToString();
                                auto.IND_ALIN = row[32].ToString();
                                auto.CLV_META_ALIN = row[33].ToString();
                                auto.META_ALIN = row[34].ToString();
                                auto.CLV_FIN = row[35].ToString();
                                auto.FINAL = row[36].ToString();
                                auto.CLV_FUN = row[37].ToString();
                                auto.FUNCION = row[38].ToString();
                                auto.CLV_SUBFUN = row[39].ToString();
                                auto.SUBFUNCION = row[40].ToString();
                                auto.CLV_ACTINST = row[41].ToString();
                                auto.ACTIVIDAD_INST = row[42].ToString();
                                auto.MODEJECUC = row[43].ToString();
                                auto.MODAINVE = row[44].ToString();
                                auto.INICIAL = decimal.Parse(row[45].ToString());
                                auto.AMPLIADO = decimal.Parse(row[46].ToString());
                                auto.REDUCIDO = decimal.Parse(row[47].ToString());
                                auto.PRESUPUESTO = decimal.Parse(row[48].ToString());
                                auto.COMPROMETIDO = decimal.Parse(row[49].ToString());
                                auto.DEVENGADO = decimal.Parse(row[50].ToString());
                                auto.EJERCIDO = decimal.Parse(row[51].ToString());
                                auto.PAGADO = decimal.Parse(row[52].ToString());
                                auto.ESTADO = row[53].ToString();

                                dbcontex.Atuevaluacion_demo.Add(auto);
                                await dbcontex.SaveChangesAsync();

                            }

                        }
                    }
                }

                #endregion file 2


                return "";

            }
            catch (Exception e)
            {
                throw (e);
            }
           
        }
        #endregion

        public List<SPARTIDAS> getPresupuesto()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    return dbcontex.SPARTIDAS.OrderBy(x=>x.ID_PROYECTO).ThenBy(x=>x.CLV_PARTID).ToList();
                }
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

        public List<PRESUPUESTO_ANALITICOXUNIDAD> getPresupuestoAnaliticoXUnidad()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    return dbcontex.PRESUPUESTO_ANALITICOXUNIDAD.OrderBy(x => x.UNIDADM).ThenBy(x => x.N_PROGRAMA).ThenBy(x=>x.CLV_PARTID).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<PRESUPUESTO_ANALITICOXUNIDAD> SearchPresupuestoAnaliticoXUnidad(List<int> Periodos, List<string> Unidades, List<string> Proyectos, List<int> Partidas, List<int> Fuentes, List<string> Recursos, List<string> Capitulos)
        {
            try
            {
                if (Periodos[0] == 0)
                    Periodos = this.getPeriodos();

                if (Unidades[0].Equals("0"))
                    Unidades = this.getUNIDADM().Select(x => x.Key).ToList();

                List<string> tempUnidades = new List<string>();
                foreach (string row in Unidades)
                {
                    if (row.Length == 1)
                        tempUnidades.Add("0" + row);
                    else
                        tempUnidades.Add(row);
                }
                Unidades = tempUnidades;


                if (Proyectos[0] == "0")
                    Proyectos = this.getProgPresupuest().Select(x => x.Key).ToList();

                if (Partidas[0] == 0)
                    Partidas = this.getPartid().Select(x => int.Parse(x.Key)).ToList();

                if (Fuentes[0] == 0)
                    Fuentes = this.getFuente().Select(x => int.Parse(x.Key)).ToList();

                if (Recursos[0] == "0")
                    Recursos = this.getRecurso().Select(x => x.Key).ToList();

                if (Capitulos[0] == "0")
                    Capitulos = this.getCapitulo().Select(x => x.Key).ToList();
                List<PRESUPUESTO_ANALITICOXUNIDAD> LPresupuesto = new List<PRESUPUESTO_ANALITICOXUNIDAD>();
                using (dbcontex = new SIENCHAFAEntities())
                {
                    LPresupuesto = dbcontex.PRESUPUESTO_ANALITICOXUNIDAD
                        .Where(x =>
                            Proyectos.Contains(x.N_PROGRAMA) &&
                            Partidas.Contains(x.CLV_PARTID) && 
                            Unidades.Contains(x.CLV_UNIDADM) &&
                            Recursos.Contains(x.CLV_RECURSO) &&
                            Capitulos.Contains(x.CLV_CAPITU)
                        )
                        .OrderBy(x => x.UNIDADM)
                        .ThenBy(x => x.N_PROGRAMA)
                        .ThenBy(x => x.CLV_PARTID)
                        .ToList();
                }

                return LPresupuesto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public  List<CustomData> getPartid() {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    return dbcontex.Atuevaluacion_demo.Select(x=>new CustomData { 
                        Key=x.CLV_PARTIDA,
                        Description= x.PARTIDA
                    })
                    .Distinct()
                    .OrderBy(x=>x.Description)
                    .ToList();
                }
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }

        public List<CustomData> getCapitulo()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    return dbcontex.Atuevaluacion_demo.Select(x => new CustomData
                    {
                        Key = x.CLV_CAPITULO,
                        Description = x.CAPITULO
                    })
                    .Distinct()
                    .OrderBy(x => x.Key)
                    .ToList();
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public List<CustomData> getRecurso()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    return dbcontex.Atuevaluacion_demo.Select(x => new CustomData
                    {
                        Key = x.CLV_RECURSO,
                        Description = x.RECURSO
                    })
                    .Distinct()
                     .OrderBy(x => x.Description)
                    .ToList();
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public List<CustomData> getFuente()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    return dbcontex.Atuevaluacion_demo.Select(x => new CustomData
                    {
                        Key = x.CLV_FUENTE,
                        Description = x.FUENTE
                    })
                    .Distinct()
                     .OrderBy(x => x.Description)
                    .ToList();
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public List<CustomData> getUNIDADM()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    return dbcontex.Atuevaluacion_demo.Select(x => new CustomData
                    {
                        Key = x.CLV_UNIDADM,
                        Description = x.UNIDADM
                    })
                    .Distinct()
                     .OrderBy(x => x.Description)
                    .ToList();
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public List<CustomData> getProyect()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    List<CustomData> data = dbcontex.Atuevaluacion_demo.Select(x => new CustomData
                    {
                        Key = x.ID_PROYECTO,
                        Description = x.PROYECTO
                    })
                    .Distinct()
                    .OrderBy(x => x.Key)
                    .ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public List<CustomData> getProgPresupuest()
        {
            try
            {
                using (dbcontex = new SIENCHAFAEntities())
                {
                    List<CustomData> data = dbcontex.Atuevaluacion_demo.Select(x => new CustomData
                    {
                        Key = x.N_PROGRAMA,
                        Description = x.PROG_PRESUP
                    })
                    .Distinct()
                    .OrderBy(x => x.Key)
                    .ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public List<int> getPeriodos()
        {
            return new List<int> { 1,2,3,4,5,6,7,8,9,10,11,12};
        }
    }
}