
var banPeriodos = false;
var banUnidades = false;
var bandPartidas = false;
var banProyectos = false;
var banFuentes = false;
var banRecursos = false;
var banCapitulos = false;

var PRESUPESTO = PRESUPESTO || {
    INICIALIZAR: {
        Eventos: function () {
            $('#cmdUpload').on('click', function (event) {
                PRESUPESTO.ARCHIVOS.Enviar();
                event.stopPropagation();
            });
            $('#cmdDelete').on('click', function (event) {
                PRESUPESTO.BASE.Delete();
            });
            $('#cmdFound').on('click', function (event) {
                PRESUPESTO.BASE.SEARCH.Anual();
            });
            $('#AnualTable').dataTable({
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
                        "targets": [0,1]
                    },
                    {
                        "className": "dt-head-right",
                        "targets": [2,3,4,5,6]
                    },
                    { "width": "10%", "targets": 0 },
                    { "width": "10%", "targets": 1 },
                    { "width": "10%", "targets": 2 },
                    { "width": "10%", "targets": 3 },
                    { "width": "10%", "targets": 4 },
                    { "width": "10%", "targets": 5 },
                    { "width": "10%", "targets": 6 },

                ],
                "order": [[0, 'asc'], [1, 'asc']]
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
        },
    },
    ARCHIVOS: {

        showRequest: function (formData, jqForm, options) {
            return true;
        },
        showResponse: function (data) {
            if (typeof (data.Message) != "undefined") {
                jError(data.Message, 'Error');
            }
            else {
                $('#LegendFolio').html(data.Folio + ' <small style="color:gray; margin-left:5px; font-size:15px;font-weight: 200;">La información marcada con (*) es requerida.</small>');
                document.location = '/Home/';
            }
        },
        Validar: function () {
            if ($('#FileInput1').val() == '') {
              
                return false;
            }
            if ($('#FileInput2').val() == '') {

                return false;
            }

            return true;
        },
        Enviar: function (event) {
            
            if (this.Validar()) {

                /*
                var options = {
                    beforeSubmit: PRESUPESTO.ARCHIVOS.ShowRequest,
                    success: PRESUPESTO.ARCHIVOS.ShowResponse,
                    url: '/Home/CargarArchivos',
                    type: 'post',
                    dataType: 'json'
                };
               
                $('#frm').submit(function () {
                    $(this).ajaxSubmit(options);
                    return false;
                });
                $('#frm').submit();
                */
                alert('Archivos enviados');
                //const formData = new URLSearchParams(new FormData($('#frm').get(0)));
                const formData = new FormData();
                formData.append('FileInput1', $('#FileInput1').get(0).files[0]);
                formData.append('FileInput2', $('#FileInput2').get(0).files[0]);
                return fetch('/Home/CargarArchivos', { method: 'POST', body: formData })
                    .then(response => {
                        return response.json()
                            .then((json) => {
                                return Promise.resolve(json)
                            })
                    })
                    .then(data => {
                        alert('Archivos cargados con exito');
                    })
                    .catch(error => {
                        //alert('Error: ' + error.Message);
                    })
            }
        }
    },
    BASE: {
        SEARCH: {
            Mensual: function () {
                const data = {
                    Periodos: $('#cboPeriodo').selectpicker('val').toString(),
                    Unidades: $('#cboUnidad').selectpicker('val').toString(),
                    Partidas: $('#cboPartida').selectpicker('val').toString(),
                    Proyectos: $('#cboProyecto').selectpicker('val').toString(),
                    Fuentes: $('#cboFuente').selectpicker('val').toString(),
                    Recursos: $('#cboRecurso').selectpicker('val').toString(),
                    Capitulos: $('#cboCapitulo').selectpicker('val').toString(),
                };
                return fetch('/Home/SearchMensual', { method: 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' } })
                    .then(response => {
                        return response.json()
                            .then((json) => {
                                return Promise.resolve(json)
                            })
                    })
                    .then(data => {

                    })
                    .catch(error => {
                        //alert('Error: ' + error.Message);
                    });
            },
            Anual: function () {
                var table = $('#AnualTable').DataTable();
                $('#Div_AnualTabla').show();
                $('#AnualTable').dataTable().fnClearTable();
                const data = {
                    Periodos: $('#cboPeriodo').selectpicker('val').toString(),
                    Unidades: $('#cboUnidad').selectpicker('val').toString(),
                    Partidas: $('#cboPartida').selectpicker('val').toString(),
                    Proyectos: $('#cboProyecto').selectpicker('val').toString(),
                    Fuentes: $('#cboFuente').selectpicker('val').toString(),
                    Recursos: $('#cboRecurso').selectpicker('val').toString(),
                    Capitulos: $('#cboCapitulo').selectpicker('val').toString(),
                };
                return fetch('/Home/SearchAnual', { method: 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' } })
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
                                    this.ID_PROYECTO,
                                    this.CLV_PARTID,
                                    this.ENEMOD.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.ENECOMP.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.SEPDEV.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    this.ENEEJE.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'),
                                    '0.0'
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
        Delete: function () {
            alert('Se eliminara todo');
            $.ajax({
                type: 'GET',
                url:  '/Home/DeleteDatabase',
                async: true,
                dataType: "json",
                data: {},
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //QPLANT.Authorize = data;
                    alert('Eliminado con exito');
                    document.location = '/Home/';
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //jError((JSON.parse(xhr.responseText)).Message, 'Error');
                }
            });
        }
    }
}

$(document).ready(function () {
    PRESUPESTO.INICIALIZAR.Eventos();
});




