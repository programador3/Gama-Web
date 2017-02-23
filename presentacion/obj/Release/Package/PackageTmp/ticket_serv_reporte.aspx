<%@ Page Title="Reporte" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="ticket_serv_reporte.aspx.cs" Inherits="presentacion.ticket_serv_reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Graph/highcharts.js"></script>
    <script src="Graph/modules/exporting.js"></script>

    <script type="text/javascript">
        function CerrarModal() {
            $('#myModal').modal('hide');
        }

        function ModalMostrar(cTitulo, cContenido, status, tEstimado, tRespuesta, clase) {


            var tiempo1 = tEstimado;
            var tiempo2 = 0;
            if (tRespuesta > tEstimado) {
                tiempo2 = ((tRespuesta - tEstimado) * 100) / tRespuesta
                tiempo1 = (tEstimado * 100) / tRespuesta
            }
            else {
                tiempo1 = (tRespuesta * 100) / tEstimado
            }
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
            $('#confirmContenido2').text(status.toUpperCase());
            $('#panel_rep').removeClass('panel-success panel-info panel-danger panel-warning');
            $('#panel_rep').addClass('panel ' + clase + '  fresh-color');

            var msg = convertHoras_Dias(tRespuesta) + "trancurrido(s) ";
            msg = msg + "de " + convertHoras_Dias(tEstimado) + "estimado(s).";
            $('#lblTiempos_msg').text(msg);

            $("#t_est").attr("style", "width:" + tiempo1 + "%");
            $("#t_res").attr("style", "width:" + tiempo2 + "%");


        }

        function convertHoras_Dias(horas) {
            var dias = 0;
            var msg = ""
            if (horas > 23) {
                dias = parseInt(horas / 24);
                horas = horas - (dias * 24);
                msg = (horas > 0) ? dias + " día(s) y " + horas + " hora(s)" : dias + " día(s) ";
            }
            else {
                msg = horas + " hora(s) ";
            }

            return msg;
        }

        function AsignaProgress(value) {
            $('#pavance').width(value);
        }

        function Click() {
            $('#myModal_grafica').modal('show')
        }


        function Grafica(tutulo, buenos_R, malos_R) {
            $("#titulo_grafica").text(tutulo);            
            malos_R = malos_R==0? 0:(100 - buenos_R);
            Highcharts.chart('container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: { text: 'Grafica de Resultados' },
                tooltip: { pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>' },
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
                    name: 'Porsentaje',
                    colorByPoint: false,
                    data: [{
                        name: 'Buenos Resultados', y: buenos_R, color: '#04B45F'
                    }, {
                        name: 'Malos Resultados', y: malos_R, color: '#DF0101'
                    }]
                }]
            });
        }

        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[10, 25, -1], [10, 25, "Todos"]] //value:item pair
            });
        });
    </script>
    <style>
        a#Contenido_lnkbuscarpuestos {
            margin-top: 0px;
        }

        .input-group-btn {
            font-size: 14px;
        }

        span h2 {
            margin-top: 0px;
        }

        .mostrar {
            /*/ display:block;*/
        }

        .ocultar {
            display: none;
        }

        .flotante {
            position: fixed;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <h1 class="page-header"><i class="fa fa-bookmark-o" aria-hidden="true"></i>&nbsp;&nbsp;Reporte Ticket de Servico</h1>
    <asp:UpdatePanel ID="UPDATE_DDLPUESTOS" runat="server" UpdateMode="Always">
        <Triggers>
            
            <asp:AsyncPostBackTrigger ControlID="lnkbuscarpuestos" EventName="Click" />
           
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="DEPTO" runat="server">
                    <h4><strong>Departamento</strong></h4>
                    <asp:DropDownList ID="ddldeptos" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddldeptos_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <h4><strong>
                        <asp:Label runat="server" ID="lblPuesto_Tarea"> Puesto que realiza la Tarea</asp:Label></strong>
                    </h4>

                    <asp:DropDownList ID="ddlPuestoAsigna" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged" AutoPostBack="true" >
                    </asp:DropDownList>


                </div>

                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <h4><strong><i class="fa fa-filter" aria-hidden="true"></i>&nbsp;Escriba un Filtro</strong></h4>
                    <div class="input-group">

                        <asp:TextBox AutoPostBack="true" ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                        </span>


                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h4><i class="fa fa-calendar"></i>&nbsp;<strong>Seleccione una fecha de inicio</strong></h4>
            <asp:TextBox ID="txtfechainicio" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h4><i class="fa fa-calendar"></i>&nbsp;<strong>Seleccione una fecha de fin</strong></h4>
            <asp:TextBox ID="txtfechafin" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

        <div class="col-lg-6 col-xs-12">
            <asp:LinkButton ID="lnkVer" OnClick="btnVer_Click" CssClass="btn btn-info btn-block" runat="server">
                        Ver Detalles Desglosados&nbsp; <i class="fa fa-repeat" aria-hidden="true"></i></asp:LinkButton>
        </div>

        <div class="col-lg-6 col-xs-12" visible="true" runat="server" id="div_export_excel">
            <asp:LinkButton ID="lnkExportarExcel" CssClass="btn btn-success btn-block" runat="server" OnClick="lnkexport_Click">
                Exportar a Excel&nbsp; <i class="fa fa-file-excel-o" aria-hidden="true"></i>
            </asp:LinkButton>
        </div>
        
        <div id="div_combo" class="col-lg-12" runat="server">

            <br />
            <br />
            <br />
            <h4><strong><i class="fa fa-address-card-o" aria-hidden="true"></i>&nbsp;Puede Seleccionar por los Deptos Asignados a los siguientes puestos:</strong></h4>
            <asp:DropDownList ID="ddlpuestos_deptos" CssClass=" form-control2" runat="server">
            </asp:DropDownList>
            <asp:LinkButton ID="LinkButton6" CssClass="btn btn-info" runat="server" OnClick="LinkButton6_Click">
                        Generar Graficas de sus Deptos&nbsp;<i class="fa fa-pie-chart" aria-hidden="true"></i></asp:LinkButton>
        </div>
    </div>

    <div class="row" id="div_panel_lista">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading" style="text-align: center">
                    <h3 class="panel-title">LISTA <i class="fa fa-list"></i></h3>
                </div>
                <div class="panel-body">
                    <h3 id="NO_Hay" runat="server" visible="false" style="text-align: center;">NO HAY DATOS <i class="fa fa-exclamation-triangle"></i></h3>
                    <div class="table table-responsive dataTables_wrapper no-footer">
                        <asp:GridView ID="gridReporte" Style="font-size: 12px;"
                            CssClass="gvv table table-responsive table-bordered 
                                         table table-responsive table-bordered- table-condensed "
                            AutoGenerateColumns="false"
                            runat="server"
                            DataKeyNames="status_color, idc_tareaser, idc_ticketserv, FECHA_CREADO, FECHA_CAN, FECHA_ATEN,
                                     FECHA_TERM, STATUS, descripcion, OBSERVACIONES_TICKET,   MOTIVO_CAN, OBSERVACIONES_TERM,  pendiente, TIEMPO_ESTIMADO, 
                                     EMPLEADO_REP, DEPTO_REP,
                                     EMPLEADO_CAN, DEPTO_CAN,
                                     EMPLEADO_ATEN, DEPTO_ATEN,  
                                     EMPLEADO_ATEN_CAN, DEPTO_ATEN_CAN,
                                    MOTIVO_ATEN_CAN,MOTIVO_ATEN_CAN_CORTA,FECHA_ATEN_CAN,
                                    TIEMPO_RESPUESTA, STATUS_TICKET"
                            OnRowCommand="gridReporte_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/more.png" HeaderText="Ver Detalles" CommandName="Editar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="status" HeaderText="status" Visible='false' />
                                <asp:ButtonField DataTextField="descripcion" HeaderText="Descripcion" />
                                <asp:BoundField DataField="des_corta" HeaderText="Descripcion" Visible='false' />
                                <asp:BoundField DataField="FECHA_CREADO" HeaderText="Fecha Ticket" />

                                <asp:ButtonField DataTextField="OBSERVACIONES_TICKET" HeaderText="Observaciones" />
                                <asp:BoundField DataField="OBSERVACIONES_TICKET_CORTA" Visible="false" HeaderStyle-Wrap="true" />
                                <%--reporto --%>
                                <asp:BoundField DataField="EMPLEADO_REP" HeaderText="Empleado (Reporto)" />
                                <asp:BoundField DataField="DEPTO_REP" HeaderText="Departamento (Report)" Visible='false' />
                                <%--<asp:BoundField DataField="IDC_USUARIO_REP" Visible='false' />--%>
                                <%--atendio --%>
                                <asp:BoundField DataField="EMPLEADO_ATEN" HeaderText="Empleado (Atiende)" />
                                <%--<asp:BoundField DataField="IDC_PUESTO_ATEN" HeaderText="Puesto (Atiende)" Visible='false' />--%>
                                <asp:BoundField DataField="DEPTO_ATEN" HeaderText="Departamento (Atiende)" Visible='false' />
                                <%--<asp:BoundField DataField="IDC_USUARIO_ATEN" Visible='false' />--%>
                                <%-- aten_can  --%>
                                <asp:BoundField DataField="FECHA_ATEN_CAN" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="EMPLEADO_ATEN_CAN" HeaderText="Empleado (Cancelo Atendido)" />
                                <%--<asp:BoundField DataField="IDC_PUESTO_ATEN_CAN" HeaderText="Puesto (Atendido_Cancelado)" Visible='false' />--%>
                                <asp:BoundField DataField="DEPTO_ATEN_CAN" HeaderText="Departamento (Atendido_Cancelado)" Visible='false' />
                                <%--<asp:BoundField DataField="IDC_USUARIO_ATEN_CAN" Visible='false' />--%>
                                <asp:BoundField DataField="MOTIVO_ATEN_CAN" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="MOTIVO_ATEN_CAN_CORTA" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="EMPLEADO_CAN" HeaderText="Empleado (Cancelo)" />
                                <asp:TemplateField HeaderText="Prog. Ticket" HeaderStyle-Width="40px">
                                    <ItemTemplate>
                                        <%--<asp:Image ID="r_sistema" runat="server" Width="40px" Height="40px" ImageUrl='<%#Eval("STATUS_TICKET") %>' />--%>
                                        <asp:Label ID="lbl_icono" runat="server"><h2><%#Eval("STATUS_TICKET") %></h2></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField Visible='false' DataField="idc_tareaser"></asp:BoundField>
                                <asp:BoundField Visible='false' DataField="idc_ticketserv"></asp:BoundField>
                                <%--<asp:BoundField DataField="FECHA_CREADO" Visible='false'></asp:BoundField>--%>
                                <asp:BoundField DataField="FECHA_CAN" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="FECHA_ATEN" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="FECHA_TERM" Visible='false'></asp:BoundField>


                                <%--<asp:BoundField DataField="observaciones" Visible='false'></asp:BoundField>--%>
                                <asp:BoundField DataField="status_color" Visible='false'></asp:BoundField>

                                <asp:BoundField DataField="MOTIVO_CAN" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="MOTIVO_CAN_CORTA" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="OBSERVACIONES_TERM" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="OBSERVACIONES_TERM_CORTA" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="pendiente" Visible='false'></asp:BoundField>

                                <asp:BoundField DataField="TIEMPO_ESTIMADO" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="TIEMPO_RESPUESTA" Visible='false'></asp:BoundField>

                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row" runat="server" id="div_grafica" visible="false">
        <div class="col-lg-12">
            <div class="panel panel-default fresh-color">
                <!--  -->
                <div class="panel-heading">
                    <h4><i class="fa fa-bookmark-o" aria-hidden="true"></i>&nbsp;
                            <span id="titulo_grafica"></span></h4>
                </div>
                <div class="panel-body">
                    <div class="row" runat="server" id="div_cont_grafica">
                        <div class="col-lg-6 col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div id="container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-12  col-xs-12">
                            <div class="table table-responsive">
                                <h4><strong>Tabla de Resultados</strong></h4>
                                <asp:GridView ID="gridResultados" runat="server" CssClass="table table-responsive table-bordered" ShowHeader="False" AutoGenerateColumns="False" OnRowDataBound="gridResultados_RowDataBound">
                                    <%--OnRowCommand="gridsistema_RowCommand" OnRowDataBound="gridsistema_RowDataBound"--%>
                                    <Columns>
                                        <asp:BoundField DataField="Descripcion" HeaderText="" ShowHeader="False" />
                                        <asp:BoundField DataField="Num" ShowHeader="False" />
                                        <%--CommandName="Ver"--%>
                                        <asp:BoundField DataField="B_color" HeaderText="" Visible="false" ShowHeader="False" />
                                        <asp:BoundField DataField="F_color" HeaderText="" Visible="false" ShowHeader="False" />
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>


                    </div>
                </div>
                <!--  -->
            </div>
        </div>
    </div>
    <div class="modal fade bs-example-modal-lg" id="myModal_grafica" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center">
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

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">


            <!-- Modal content-->
            <div class="modal-content" style="text-align: center">
                <div class="modal-header" style="background-color: #428bca; color: white">
                    <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gridReporte" EventName="RowCommand" />
                        <%--<asp:AsyncPostBackTrigger ControlID="lnk" EventName="Click" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row" runat="server" id="confir">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="confirmContenido"></label>
                                    </h4>
                                </div>
                            </div>

                            <div class="row" id="div_detalles" style="text-align: left;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">

                                        <!-- reportado-->
                                        <div runat="server" id="div_rep">
                                            <div class="panel panel-success  fresh-color" id="panel_rep">
                                                <div class="panel-heading">
                                                    <i class="fa fa-bookmark-o" aria-hidden="true"></i>&nbsp;
                                                            <label id="confirmContenido2"></label>
                                                </div>
                                                <div class="panel-body">
                                                    <b>Descripcion:</b>
                                                    <asp:Label runat="server" ID="lblDescr"></asp:Label><br />

                                                    <b>Observacion:</b>
                                                    <asp:Label runat="server" ID="lblObser"></asp:Label><br />
                                                    <b>Empleado:</b>
                                                    <asp:Label runat="server" ID="lblEmple_rep"></asp:Label><br />
                                                    <b>Departamento:</b>
                                                    <asp:Label runat="server" ID="lblDepto_rep"></asp:Label><br />
                                                    <b>Fecha:</b>
                                                    <asp:Label runat="server" ID="lblFecha_rep"></asp:Label><br />
                                                </div>
                                            </div>
                                        </div>
                                        <!-- atendio visible si estatus es "A"-->
                                        <div runat="server" id="div_aten">
                                            <div class="panel panel-info  fresh-color">
                                                <div class="panel-heading">
                                                    <asp:Label runat="server" ID="lblinfo">DATOS DE ATENDIDO</asp:Label>
                                                </div>
                                                <div class="panel-body">
                                                    <b>Empleado:</b>
                                                    <asp:Label runat="server" ID="lblEmple_aten"></asp:Label><br />
                                                    <b>Departamento:</b>
                                                    <asp:Label runat="server" ID="lblDepto_aten"></asp:Label><br />
                                                    <b>Fecha :</b>
                                                    <asp:Label runat="server" ID="lblFecha_aten"></asp:Label><br />
                                                </div>
                                            </div>
                                        </div>
                                        <!-- atendio visible si estatus es "C"-->
                                        <div runat="server" id="div_can">
                                            <div class="panel panel-info  fresh-color">
                                                <div class="panel-heading">
                                                    <asp:Label runat="server">DATOS DE CANCELADO</asp:Label>
                                                </div>
                                                <div class="panel-body">
                                                    <b>Empleado:</b>
                                                    <asp:Label runat="server" ID="lblEmple_can"></asp:Label><br />
                                                    <b>Departamento:</b>
                                                    <asp:Label runat="server" ID="lblDepto_can"></asp:Label><br />
                                                    <b>Fecha:</b>
                                                    <asp:Label runat="server" ID="lblFecha_can"></asp:Label><br />
                                                    <b>Motivo:</b>
                                                    <asp:Label runat="server" ID="lblMotivoCan"></asp:Label><br />
                                                </div>
                                            </div>
                                        </div>
                                        <!-- finalizo  visible si estatus es "T"-->
                                        <div runat="server" id="div_term">
                                            <div class="panel panel-info  fresh-color">
                                                <div class="panel-heading">
                                                    <asp:Label runat="server">DATOS DE TERMINADO</asp:Label>
                                                </div>
                                                <div class="panel-body">
                                                    <b>Empleado:</b>
                                                    <asp:Label runat="server" ID="lblEmple_term"> texto</asp:Label><br />
                                                    <b>Departamento:</b>
                                                    <asp:Label runat="server" ID="lblDepto_term"> texto</asp:Label><br />
                                                    <b>Fecha:</b>
                                                    <asp:Label runat="server" ID="lblFecha_term"> texto</asp:Label><br />
                                                    <b>Comentarios:</b>
                                                    <asp:Label runat="server" ID="lblComet_term"> texto</asp:Label><br />
                                                </div>
                                            </div>
                                        </div>
                                        <!-- finalizo  visible si es atendido cancelado -->
                                        <div runat="server" id="div_aten_can">
                                            <div class="panel panel-info  fresh-color">
                                                <div class="panel-heading">
                                                    <asp:Label runat="server">DATOS DE ATENDIDO CANCELADO</asp:Label>
                                                </div>
                                                <div class="panel-body">
                                                    <b>Empleado:</b>
                                                    <asp:Label runat="server" ID="lblEmple_Aten_Can"> texto</asp:Label><br />
                                                    <b>Departamento:</b>
                                                    <asp:Label runat="server" ID="lblDepto_Aten_Can"> texto</asp:Label><br />
                                                    <b>Fecha:</b>
                                                    <asp:Label runat="server" ID="lblFecha_Aten_Can"> texto</asp:Label><br />
                                                    <b>Comentarios:</b>
                                                    <asp:Label runat="server" ID="lblMotivoAten_Can"> texto</asp:Label><br />
                                                </div>
                                            </div>
                                        </div>

                                        <div id="div_progresbar" runat="server" visible="true">
                                            <div class="panel panel-default  fresh-color">
                                                <div class="panel-heading">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblTiempos">Tiempos:</asp:Label></b>
                                                    <span id="lblTiempos_msg"></span>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="progress" style="border: 1px;">
                                                        <div class="progress-bar progress-bar-success progress-bar-striped " role="progressbar" id="t_est">
                                                            Tiempo Estimado 
                                                        </div>
                                                        <div class="progress-bar progress-bar-danger progress-bar-striped active" role="progressbar" id="t_res">
                                                            Tiempo Respuesta
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-lg-6 col-xs-6">
                            &nbsp;
                                    <%--<asp:Button ID="yes" class="btn btn-success btn-block" runat="server" Text="Aceptar" />
                                    --%>
                        </div>
                        <div class="col-lg-6 col-xs-6">
                            <input type="button" id="No" class="btn btn-danger btn-block" onclick="CerrarModal();" value="Cancelar" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <%-- <input type="hidden" runat="server" id="h_n_pag" />--%>
    <input type="hidden" runat="server" id="h_strScript" />
    <input type="hidden" runat="server" id="H_strPuesto" />
    <input type="hidden" runat="server" id="H_strDpto" />

</asp:Content>
