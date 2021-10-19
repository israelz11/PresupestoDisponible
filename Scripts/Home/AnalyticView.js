
var banPeriodos = false;
var banUnidades = false;
var bandPartidas = false;
var banProyectos = false;
var banFuentes = false;
var banRecursos = false;
var banCapitulos = false;

var PRESUPESTO = PRESUPESTO || {
    CurrentTable: 0,
    TypeTable: {'Anual': 1, 'Mensual': 2},
    INICIALIZAR: {
        Eventos: function () {
           
            $('#cmdFound').on('click', function (event) {
                //if ($('#cboPeriodo').selectpicker('val') == 0)
                    PRESUPESTO.BASE.SEARCH.Anual();
                //else
                //    PRESUPESTO.BASE.SEARCH.Mensual();
            });
            $('#cmdExportPDF').on('click', function () {
                PRESUPESTO.REPORT.GetPDFAnalyticFormat();
            });
            $('#AnalyticTable').dataTable({
                "responsive": true,
                "scrollX": true,
                "searching": false,
                "autoWidth": false,
                "paging": true,
                "ordering": true,
                "info": true,
                "lengthChange": false,
                "pageLength": 50,
                "language": {
                    "paginate": {
                        "first": "Primera pagina",
                        "last": "Ultima pagina",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    },
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "emptyTable": "No hay registros en la tabla",
                    "zeroRecords": "No se encontraron registros",
                    "info": "Mostrando pagina _PAGE_ de _PAGES_. (_MAX_ registros totales encontrados)",
                    "infoEmpty": "(0) Registros encontrados",
                    "infoFiltered": "(Filtro de _MAX_ total de registros)",
                    "lengthMenu": 'Mostrar <select class="form-control input-sm"> ' +
                        '<option value="10">10</option>' +
                        '<option value="20">20</option>' +
                        '<option value="30">30</option>' +
                        '<option value="40">40</option>' +
                        '<option value="50">50</option>' +
                        '<option value="-1">Todo</option>' +
                        '</select> registros'
                },
                "columnDefs": [
                    { "orderable": true, "targets": [] },
                    {
                        "className": "dt-head-center",
                        "targets": []
                    },
                    {
                        "className": "dt-head-right",
                        "targets": [3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18]
                    },
                    { "width": "10%", "targets": 0 },
                    { "width": "10%", "targets": 1 },
                    { "width": "10%", "targets": 2 },
                    { "width": "10%", "targets": 3 },
                    { "width": "10%", "targets": 4 },
                    { "width": "10%", "targets": 5 },
                    { "width": "10%", "targets": 6 },
                    { "width": "10%", "targets": 7 },
                    { "width": "10%", "targets": 8 },
                    { "width": "10%", "targets": 9 },
                    { "width": "10%", "targets": 10 },
                    { "width": "10%", "targets": 11 },
                    { "width": "10%", "targets": 12 },
                    { "width": "10%", "targets": 13 },
                    { "width": "10%", "targets": 14 },
                    { "width": "10%", "targets": 15 },
                    { "width": "10%", "targets": 16 },
                    { "width": "10%", "targets": 17 },
                    { "width": "10%", "targets": 18 },

                ],
                "order": [[0, 'asc'], [1, 'asc']],
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
            $('#cboPeriodo').on('changed.bs.select', function (e) {
                var selected = $(this).find("option:selected").val();
                var Array = ($(this).selectpicker('val') != null ? $(this).selectpicker('val').toString().split(',') : []);

                if (Array.indexOf("0") != -1) {
                    if (!banPeriodos) {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                        banPeriodos = true;
                    }
                    else {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                        banPeriodos = false;
                    }
                }
                else {
                    if (Array.indexOf("0") == -1) //No se encontro 0
                    {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                    }
                    else //0 Encontrado
                    {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                    }
                }
            });
            $('#cboUnidad').on('changed.bs.select', function (e) {
                var selected = $(this).find("option:selected").val();
                var Array = ($(this).selectpicker('val') != null ? $(this).selectpicker('val').toString().split(',') : []);

                if (Array.indexOf("0") != -1) {
                    if (!banUnidades) {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                        banUnidades = true;
                    }
                    else {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                        banUnidades = false;
                    }
                }
                else {
                    if (Array.indexOf("0") == -1) //No se encontro 0
                    {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                    }
                    else //0 Encontrado
                    {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                    }
                }
            });
            $('#cboPartida').on('changed.bs.select', function (e) {
                var selected = $(this).find("option:selected").val();
                var Array = ($(this).selectpicker('val') != null ? $(this).selectpicker('val').toString().split(',') : []);

                if (Array.indexOf("0") != -1) {
                    if (!bandPartidas) {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                        bandPartidas = true;
                    }
                    else {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                        bandPartidas = false;
                    }
                }
                else {
                    if (Array.indexOf("0") == -1) //No se encontro 0
                    {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                    }
                    else //0 Encontrado
                    {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                    }
                }
            });
            $('#cboProyecto').on('changed.bs.select', function (e) {
                var selected = $(this).find("option:selected").val();
                var Array = ($(this).selectpicker('val') != null ? $(this).selectpicker('val').toString().split(',') : []);

                if (Array.indexOf("0") != -1) {
                    if (!banProyectos) {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                        banProyectos = true;
                    }
                    else {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                        banProyectos = false;
                    }
                }
                else {
                    if (Array.indexOf("0") == -1) //No se encontro 0
                    {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                    }
                    else //0 Encontrado
                    {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                    }
                }
            });
            $('#cboFuente').on('changed.bs.select', function (e) {
                var selected = $(this).find("option:selected").val();
                var Array = ($(this).selectpicker('val') != null ? $(this).selectpicker('val').toString().split(',') : []);

                if (Array.indexOf("0") != -1) {
                    if (!banFuentes) {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                        banFuentes = true;
                    }
                    else {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                        banFuentes = false;
                    }
                }
                else {
                    if (Array.indexOf("0") == -1) //No se encontro 0
                    {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                    }
                    else //0 Encontrado
                    {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                    }
                }
            });
            $('#cboRecurso').on('changed.bs.select', function (e) {
                var selected = $(this).find("option:selected").val();
                var Array = ($(this).selectpicker('val') != null ? $(this).selectpicker('val').toString().split(',') : []);

                if (Array.indexOf("0") != -1) {
                    if (!banRecursos) {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                        banRecursos = true;
                    }
                    else {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                        banRecursos = false;
                    }
                }
                else {
                    if (Array.indexOf("0") == -1) //No se encontro 0
                    {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                    }
                    else //0 Encontrado
                    {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                    }
                }
            });
            $('#cboCapitulo').on('changed.bs.select', function (e) {
                var selected = $(this).find("option:selected").val();
                var Array = ($(this).selectpicker('val') != null ? $(this).selectpicker('val').toString().split(',') : []);

                if (Array.indexOf("0") != -1) {
                    if (!banCapitulos) {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                        banCapitulos = true;
                    }
                    else {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                        banCapitulos = false;
                    }
                }
                else {
                    if (Array.indexOf("0") == -1) //No se encontro 0
                    {
                        $(this).find('option[value=0]').prop('selected', false).removeAttr('selected');
                        $(this).selectpicker('refresh');
                    }
                    else //0 Encontrado
                    {
                        $(this).selectpicker('deselectAll');
                        $(this).find('option[value=0]').prop('selected', true);
                        $(this).selectpicker('refresh');
                    }
                }
            });

            PRESUPESTO.CurrentTable = PRESUPESTO.TypeTable.Anual;

            $('#cboPeriodo').on('change', function () {
                //if ($(this).val() == 0)
                //    PRESUPESTO.BASE.TABLE.Anual();
                //else
                //    PRESUPESTO.BASE.TABLE.Mensual();
            });
        },
    },
    FUNCIONES: {
        GetMonthName: function (Month) {
            var Name = "";

            if (Month == 1)
                Name = "ENERO";
            if (Month == 2)
                Name = "FEBRERO"; 
            if (Month == 3)
                Name = "MARZO";
            if (Month == 4)
                Name = "ABRIL";
            if (Month == 5)
                Name = "MAYO";
            if (Month == 6)
                Name = "JUNIO";
            if (Month == 7)
                Name = "JULIO";
            if (Month == 8)
                Name = "AGOSTO";
            if (Month == 9)
                Name = "SEPTIEMBRE";
            if (Month == 10)
                Name = "OCTUBRE";
            if (Month == 11)
                Name = "NOVIEMBRE";
            if (Month == 12)
                Name = "DICIEMBRE"; 
            //console.log('Mes: ' + Name);
            return Name;
        }
    },
    BASE: {
        SEARCH: {
            Anual: function () {

                var table = $('#AnalyticTable').DataTable();
                $('#AnalyticTable').dataTable().fnClearTable();
                const data = {
                    Periodos: $('#cboPeriodo').selectpicker('val').toString(),
                    Unidades: $('#cboUnidad').selectpicker('val').toString(),
                    Partidas: $('#cboPartida').selectpicker('val').toString(),
                    Proyectos: $('#cboProyecto').selectpicker('val').toString(),
                    Fuentes: $('#cboFuente').selectpicker('val').toString(),
                    Recursos: $('#cboRecurso').selectpicker('val').toString(),
                    Capitulos: $('#cboCapitulo').selectpicker('val').toString(),
                };
                return fetch(UrlRelative + 'Home/SearchPresupuestoAnaliticoXUnidad?Periodos=' + data.Periodos + '&Unidades=' + data.Unidades + '&Partidas=' + data.Partidas + '&Proyectos=' + data.Proyectos + '&Fuentes=' + data.Fuentes + '&Recursos=' + data.Recursos + '&Capitulos=' + data.Capitulos, { method: 'GET' })
                    .then(response => {
                        return response.json()
                            .then((json) => {
                                return Promise.resolve(json)
                            })
                    })
                    .then(data => {
                        $(jQuery.parseJSON(JSON.stringify(data))).each(function () {
                            table.row.add(
                                [
                                    this.UNIDADM,
                                    "(" + this.N_PROGRAMA + ") " + this.PROG_PRESUP,
                                    "(" + this.CLV_PARTID + ") " + this.PARTIDA,
                                    this.MODIFICADO.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.OCTUBRE_DIS.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.NOVIEMBRE_DIS.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.DICIEMBRE_DIS.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.ENERO_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.FEBRERO_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.MARZO_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.ABRIL_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.MAYO_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.JUNIO_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.JULIO_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.AGOSTO_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.SEPTIEMBRE_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.OCTUBRE_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.NOVIEMBRE_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.DICIEMBRE_EJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                ]
                            );
                        });
                        table.page(0).draw(false);
                    })
                    .catch(error => {
                        //alert('Error: ' + error.Message);
                });
            }
        },
        TABLE: {
            Anual: function () {
                $('#Div_AnualTabla').show();
                $('#Div_MensualTabla').hide();
                CurrentTable = PRESUPESTO.TypeTable.Anual;
            },
            Mensual: function () {
                $('#Div_MensualTabla').show();
                $('#Div_AnualTabla').hide();
                CurrentTable = PRESUPESTO.TypeTable.Mensual;
                this.CreateMensual();
            },
            CreateMensual: function () {
                var values = $('#cboPeriodo').selectpicker('val').toString().split(',');
                var table = '<table id="TableMensual" class="table table-responsive">';
                table += '  <thead>';
                table += '      <th>MOMENTO</th>';
                table += '      <th>PROYECTO</th>';
                table += '      <th>PARTIDA</th>';
                values.forEach(function (Month) {
                    table += '      <th  style="text-align:right;">' + PRESUPESTO.FUNCIONES.GetMonthName(Month) + '</th>';
                });

                table += '  </thead>';
                table += '  <tbody>';
                table += '  </tbody>';
                table += '</table>';

                $('#Div_MensualTabla').html(table);
            }
        },
        Delete: function () {
            Swal.fire({
                title: '',
                html: 'Espere un momento...',
                allowOutsideClick: false,
                showCloseButton: false,
                showCancelButton: false,
                showConfirmButton: false,
                willOpen: () => {
                    Swal.showLoading()
                },
                didOpen: async () => {
                    return fetch(UrlRelative + 'Home/DeleteDatabase', { method: 'GET' })
                        .then(response => {
                            return response.json()
                                .then((json) => {
                                    if (response.ok) {
                                        return Promise.resolve(json)
                                    }
                                    return Promise.reject(json)
                                })
                        })
                        .then(data => {
                            document.location = UrlRelative + 'Home/';
                        }).catch(error => {
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                text: '' + error,
                            })
                        });
                },
            });
            
        }
    },
    REPORT: {
        GetPDFAnalyticFormat: function () {
            Swal.fire({
                title: '',
                html: 'Descargando PDF...',
                allowOutsideClick: false,
                showCloseButton: false,
                showCancelButton: false,
                showConfirmButton: false,
                willOpen: () => {
                    Swal.showLoading()
                },
                didOpen: () => {
                    var Partida = $("#cboPartida option:selected").text();
                    return fetch(UrlRelative + 'Home/GetPDFAnalyticFormat?Partida=' + Partida, { method: 'POST' })
                        .then(res => res.blob())
                        .then(blob => {
                            var url = window.URL.createObjectURL(blob);
                            var a = document.createElement('a');
                            a.href = url;
                            a.target = '_blank';
                            //a.download = "AnalyticByDependencies.pdf";
                            document.body.appendChild(a); // we need to append the element to the dom -> otherwise it will not work in firefox
                            a.click();
                            a.remove();
                            Swal.close();
                        }).catch(error => {
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                text: '' + error,
                            })
                        });
                },
            });

            
        }
    }
}

$(document).ready(function () {
    PRESUPESTO.INICIALIZAR.Eventos();
});




