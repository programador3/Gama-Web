<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="ticketserv_info.aspx.cs" Inherits="presentacion.ticketserv_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        function CerrarModal() {
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
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
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false, allowEscapeKey: false
            },
               function () {
                   location.href = URL;
               });
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

        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[10, 25, -1], [10, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header"><i class="fa fa-bookmark-o" aria-hidden="true"></i>&nbsp;&nbsp;Informacion de Ticket de Servico</h1>
   

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
                            DataKeyNames="status,idc_tareaser, idc_ticketserv"
                            OnRowCommand="gridReporte_RowCommand">
                            <%--  --%>
                            <Columns>
                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/more.png" HeaderText="Ver Detalles" CommandName="Editar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>

                                <asp:BoundField DataField="status" HeaderText="status" Visible='false' />
                                <asp:BoundField DataField="idc_tareaser" HeaderText="idc_tareaser" Visible='false' />
                                <asp:BoundField DataField="idc_ticketserv" HeaderText="idc_ticketserv" Visible='false' />
                                <asp:ButtonField DataTextField="descripcion" HeaderText="Descripcion" />
                                <asp:BoundField DataField="des_corta" HeaderText="Descripcion" Visible='false' />
                                <asp:BoundField DataField="FECHA_REP_TEXT" HeaderText="Fecha Ticket" />

                                <asp:ButtonField DataTextField="OBSERVACIONES_TICKET" HeaderText="Observaciones" />
                                <asp:BoundField DataField="OBSERVACIONES_TICKET_CORTA" Visible="false" HeaderStyle-Wrap="true" />
                                <%--reporto --%>
                                <asp:BoundField DataField="EMPLEADO_REP" HeaderText="Empleado (Reporto)" />
                                <asp:BoundField DataField="DEPTO_REP" HeaderText="Departamento (Report)" Visible='false' />

                                <asp:BoundField DataField="EMPLEADO_ATEN" HeaderText="Empleado (Atiende)" />
                                <asp:BoundField DataField="DEPTO_ATEN" HeaderText="Departamento (Atiende)" Visible='false' />

                                <asp:TemplateField HeaderText="Prog. Ticket" HeaderStyle-Width="40px">
                                    <ItemTemplate>
                                        <%--<asp:Image ID="r_sistema" runat="server" Width="40px" Height="40px" ImageUrl='<%#Eval("STATUS_TICKET") %>' />--%>
                                        <asp:Label ID="lbl_icono" runat="server"><h2><%#Eval("ICONO") %></h2></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FECHA_ATEN_TEXT" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="TIEMPO_ESTIMADO" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="TIEMPO_RESPUESTA" Visible='false'></asp:BoundField>

                            </Columns>
                        </asp:GridView>

                    </div>
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
                                                    <div runat="server" id="div_aten">
                                                        <hr />
                                                        <b>Empleado:</b>
                                                        <asp:Label runat="server" ID="lblEmple_aten"></asp:Label><br />
                                                        <b>Departamento:</b>
                                                        <asp:Label runat="server" ID="lblDepto_aten"></asp:Label><br />
                                                        <b>Fecha :</b>
                                                        <asp:Label runat="server" ID="lblFecha_aten"></asp:Label><br />
                                                        <b><asp:Label runat="server" ID="lblTiempos">Tiempos:</asp:Label></b>
                                                    <span id="lblTiempos_msg"></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <!-- empleados que dan servicio-->
                                        <div runat="server" id="div_empleados">
                                            <div class="panel panel-success fresh-color">
                                                <div class="panel-heading">
                                                    <asp:Label runat="server">EMPLEADOS QUE PUEDEN DAR SERVICIO</asp:Label>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12  col-xs-12">
                                                            <div class="table table-responsive">
                                                                <%--<h4><strong>Tabla de Resultados</strong></h4>--%>
                                                                <asp:GridView ID="gridEmpleados" runat="server" CssClass="table table-responsive table-bordered" ShowHeader="False" AutoGenerateColumns="False" OnRowDataBound="gridEmpleados_RowDataBound">

                                                                    <Columns>
                                                                        <asp:BoundField DataField="idc_empleado" HeaderText="idc_empleado" />
                                                                        <asp:BoundField DataField="EMPLEADO" HeaderText="EMPLEADO" />
                                                                        <asp:BoundField DataField="DEPTO" HeaderText="DEPTO" />

                                                                    </Columns>
                                                                </asp:GridView>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <input type="hidden" runat="server" id="H_idc_puesto_aten" />


                                       
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
</asp:Content>
