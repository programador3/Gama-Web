<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Adicional.Master" AutoEventWireup="true" CodeBehind="grafica_ventas.aspx.cs" Inherits="presentacion.grafica_ventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script type="text/javascript">
        function CargarGraficaVentas(title, vventas, vtotal) {
            Highcharts.setOptions({
                colors: ['#00897b', '#eeeeee']
            });
            Highcharts.chart('container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: title
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b> {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Porcentaje',
                    colorByPoint: true,
                    data: [
                        {
                            name: 'Ventas',
                            y: vventas
                        },
                        {
                            name: '',
                            y: vtotal
                        }]
                }]
            });

        }
        function CargarGraficaVentas0(title, vventas) {
            Highcharts.setOptions({
                colors: ['#00897b']
            });
            Highcharts.chart('container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: title
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b> {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Porcentaje',
                    colorByPoint: true,
                    data: [
                        {
                            name: 'Ventas',
                            y: vventas
                        }]
                }]
            });

        }
        function CargarGraficaUtilidad(title, vventas, vtotal) {

            Highcharts.setOptions({
                colors: ['#00897b', '#eeeeee']
            });
            Highcharts.chart('container2', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: title
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b> {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Porcentaje',
                    colorByPoint: true,
                    data: [
                        {
                            name: 'Utilidad deseada',
                            y: vventas
                        },
                        {
                            name: '',
                            y: vtotal
                        }]
                }]
            });

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
   <div class="row">
       <div class="col-lg-12">
           <h3 class=" page-header text-center"><strong>Ventas por Pedidos | Alcance</strong></h3>
       </div>
       <div class="col-lg-6 col-md-6 col-sm-1w col-xs-12">
            <div id="container" style="width:100%; height: 400px; margin: 0 auto"></div>
           <h5>% de Ventas:&nbsp;<strong><asp:Label Font-Size="18px" ID="Label1" runat="server" Text="Label"></asp:Label></strong></h5>
       </div>
       <div class="col-lg-6 col-md-6 col-sm-1w col-xs-12">
            <div id="container2" style="width:100%; height: 400px; margin: 0 auto"></div>
           <h5>Utilidad Deseada:&nbsp;<strong><asp:Label Font-Size="18px"  ID="Label2" runat="server" Text="Label"></asp:Label></strong></h5>
       </div>
   </div>

    
</asp:Content>
