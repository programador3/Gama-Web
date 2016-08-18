<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="grafica.aspx.cs" Inherits="presentacion.grafica" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="js/jquery10.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap-theme.min.css" />
    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="js/filesaver.js"></script>
    <script src="js/html2canvas.js"></script>
    
    <link href="js/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <link href="css/jquery.dataTables.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        function Click()
        {
            $('#myModal').modal('show')
        }
        function DataTa1(value) {
            $(value).DataTable({
                "order": [[0, "desc"]]
            });
        }
        function DataTa2(value) {
            $(value).DataTable();
        }
        function DataTa3(value) {
            $(value).DataTable();
        }
        function DataTa4(value) {
            $(value).DataTable();
        }
        var sisc = 0;
        var sisi = 0;

        var usuc = 0;
        var usui = 0;

        function GraficaSistema(cor, inc) {
            sisc = cor,
            sisi = inc;
        }
        function GraficaUsuario(cor, inc) {
            usuc = cor,
            usui = inc;
        }
        $(function () {

            $(document).ready(function () {

                // Build the chart
                $('#container').highcharts({
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: 'Resultados segun el sistema'
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: true
                        }
                    },
                    series: [{
                        name: 'Porcentaje de Rendimiento',
                        colorByPoint: true,
                        data: [{
                            name: 'Tareas terminadas de manera correcta',
                            y: sisc,
                            color: '#04B45F'
                        }, {
                            name: 'Tareas terminadas de manera incorrecta',
                            y: sisi,
                            color: '#DF0101'
                        }]
                    }]
                });
            });
        });
        $(function () {

            $(document).ready(function () {

                // Build the chart
                $('#container2').highcharts({
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: 'Resultados segun los puestos que dieron el Visto Bueno'
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: true
                        }
                    },
                    series: [{
                        name: 'Porcentaje de Rendimiento',
                        colorByPoint: true,
                        data: [{
                            name: 'Tareas terminadas de manera correcta',
                            y: usuc,
                            color: '#04B45F'
                        }, {
                            name: 'Tareas terminadas de manera incorrecta',
                            y: usui,
                            color: '#DF0101'
                        }]
                    }]
                });
            });
        });
        $(function () {
            var valueda =  <%=JavaScript.Serialize(this.valores) %>
                var categories = <%=JavaScript.Serialize(this.meses) %>
                $('#container3').highcharts({
                    title: {
                        text: 'Tareas por Mes del Año Actual',
                        x: -20 //center
                    },
                    subtitle: {
                        text: '',
                        x: -20
                    },
                    xAxis: {
                        categories: categories
                    },
                    yAxis: {
                        title: {
                            text: 'Total de Tareas'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },
                    tooltip: {
                        valueSuffix: ' Tareas'
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [{
                        name: 'GAMA',
                        data: valueda
                    }]
                });
            });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" id="contenido">
            <div class="row">
                <div class="col-lg-12">
                    <div class="container">
                        <div class="page-header" style="text-align: center;">
                            <h3>
                                <asp:Label ID="LBLTITLE" runat="server" Text=""></asp:Label></h3>
                            <h3>
                                <small>Se muestran resultados de
                                    <asp:Label ID="lblrango" runat="server" Text=""></asp:Label><span>&nbsp;<button type="button" class="btn btn-primary btn-sm" onclick="Click();">
            Ver Uso de Tarea en Este Año
        </button></span></small>
                                
                            </h3>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <div id="container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto; text-align: center;"></div>
                </div>
                <div class="col-lg-5 col-md-5 col-sm-12  col-xs-12">
                    <div class="table table-responsive">

                        <h4><strong>Tabla de Resultados segun sistema</strong></h4>
                        <asp:GridView ID="gridsistema" runat="server" CssClass="table table-responsive table-bordered" OnRowCommand="gridsistema_RowCommand" ShowHeader="False" AutoGenerateColumns="False" OnRowDataBound="gridsistema_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="name" HeaderText="" ShowHeader="False"></asp:BoundField>
                                <asp:ButtonField DataTextField="total" CommandName="Ver" ShowHeader="False"></asp:ButtonField>
                                <asp:BoundField DataField="b_color" HeaderText="" Visible="false" ShowHeader="False"></asp:BoundField>
                                <asp:BoundField DataField="f_color" HeaderText="" Visible="false" ShowHeader="False"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-lg-6 col-md-6 col-sm-12  col-xs-12">
                    <div id="container2" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto; text-align: center;"></div>
                </div>
                <div class="col-lg-5 col-md-5 col-sm-12  col-xs-12">
                    <div class="table table-responsive">
                        <h4><strong>Tabla de Resultados segun Puesto que reviso</strong> </h4>
                        <asp:GridView ID="grisusuario" runat="server" CssClass="table table-responsive table-bordered" OnRowCommand="grisusuario_RowCommand" ShowHeader="False" AutoGenerateColumns="False" OnRowDataBound="gridsistema_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="name" HeaderText="" ShowHeader="False"></asp:BoundField>
                                <asp:ButtonField DataTextField="total" CommandName="Ver" ShowHeader="False"></asp:ButtonField>
                                <asp:BoundField DataField="b_color" HeaderText="" Visible="false" ShowHeader="False"></asp:BoundField>
                                <asp:BoundField DataField="f_color" HeaderText="" Visible="false" ShowHeader="False"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="padding: 25px;">
            <div class="col-lg-12">

                <h4><strong>Tabla Concentrada por Empleados Incluidos en las Graficas </strong><span>
                    <asp:LinkButton ID="lnkexport" runat="server" CssClass="btn btn-success" OnClick="lnkexport_Click">Exportar a Excel <i class="fa fa-file-excel-o" aria-hidden="true"></i></asp:LinkButton></span></h4>
                <br />
                <asp:PlaceHolder ID="table" runat="server" Visible="true" />
            </div>
        </div>
        <!-- Button trigger modal -->
    

        <!-- Modal -->
        <div class="modal fade bs-example-modal-lg" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header" style="text-align:center">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Grafica Global de Uso</h4>
                    </div>
                    <div class="modal-body">
                        <div id="container3" style="min-width: 860px; height: 400px; margin: 0 auto"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>