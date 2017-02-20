<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="grafica_reclutamiento.aspx.cs" Inherits="presentacion.grafica_reclutamiento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="js/jquery10.js"></script>
    <script src="https://use.fontawesome.com/697fe8fd5a.js"></script>
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
            $(value).DataTable();
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
      
    </script>
    <style type="text/css">
        body {
            font-family: 'Roboto Condensed', sans-serif;
        }

        .Ocultar {
            display: none;
        }

        .form-control2 {
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }

            .form-control2[disabled], .form-control2[readonly], fieldset[disabled] .form-control2 {
                background-color: #eee;
                opacity: 1;
            }

        .read {
            border: 1px solid #ccc;
            background-color: #eee;
            opacity: 1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" id="contenido">
            <div class="row">
                <div class="col-lg-12">
                    <div class="container">
                        <div style="text-align: center;">
                            <h3>
                                Seleccione un rango de fechas para evaluar el Rendimiento                           
                            </h3>
                        </div>
                    </div>
                    <label style="width:49%"><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicio</strong></label>
                    <label style="width:49%"><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Fin</strong></label>
                    <asp:TextBox ID="txtfechainicio" CssClass="form-control2" Width="49%" TextMode="Date" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtfechafin" CssClass="form-control2" Width="49%" TextMode="Date" runat="server"></asp:TextBox>
                    
                    <br />
                    <br />
                    <label style="width:100%"><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Reclutadores</strong></label>
                    <asp:DropDownList ID="ddlreclu" CssClass=" form-control2" runat="server"></asp:DropDownList>
                    
                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" style="text-align:left;" runat="server" OnClick="LinkButton1_Click">Generar Evaluación</asp:LinkButton>
                    
                </div>
            </div>
            <div id="div_reporte" runat="server" visible="false">

                <div class="row">
                    <div class="col-lg-12">
                        <div class="container">
                            <div class="page-header" style="text-align: center;">
                                <h3>
                                    <asp:Label ID="LBLTITLE" runat="server" Text=""></asp:Label></h3>
                                <h3>
                                    <small>Se muestran resultados de
                                    <asp:Label ID="lblrango" runat="server" Text=""></asp:Label>
                                    </small>
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
                            <asp:GridView ID="gridsistema" runat="server" CssClass="table table-responsive table-bordered" DataKeyNames="tipo_filtro"
                                OnRowCommand="gridsistema_RowCommand" ShowHeader="False" AutoGenerateColumns="False" OnRowDataBound="gridsistema_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="name" HeaderText="" ShowHeader="False"></asp:BoundField>
                                    <asp:ButtonField DataTextField="total" CommandName="Ver" ShowHeader="False"></asp:ButtonField>
                                    <asp:BoundField DataField="b_color" HeaderText="" Visible="false" ShowHeader="False"></asp:BoundField>
                                    <asp:BoundField DataField="f_color" HeaderText="" Visible="false" ShowHeader="False"></asp:BoundField>
                                    <asp:BoundField DataField="tipo_filtro" HeaderText="" Visible="false" ShowHeader="False"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
        <div class="row" id="div_details" runat="server" style="padding: 10px">
            <div class=" col-lg-12">
                <asp:PlaceHolder ID="PlaceHolder" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>