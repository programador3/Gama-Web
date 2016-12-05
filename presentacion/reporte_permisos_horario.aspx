<%@ Page Title="Mis Solicitudes Pendientes" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_permisos_horario.aspx.cs" Inherits="presentacion.solicitudes_pendientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Giftp(mensaje) {
            swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '8000', showConfirmButton: false });
            return true;
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
            //alert(path);
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#myModalinfo').modal('hide');
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
        function ModalConfirms(ctype) {
            $('#myModalinfo').removeClass();
            $('#myModalinfo').addClass(ctype);
            $('#myModalinfo').modal('show');
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            },
               function () {
                   location.href = URL;
               });
        }
        function AsignaProgress(value) {
            $('#pavance').width(value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header" style="text-align: center;">Reporte Permisos Cambio de Horarios </h1>
    <asp:UpdatePanel ID="upda" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="gridcelulares" />
            <asp:PostBackTrigger ControlID="LinkButton2" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class=" col-lg-12">  
                   <h4>Seleccione un Rango de Fechas de Aplicación</h4>  
                  <label style=" font-size:18px;width:49%;"><strong>Fecha Inicio</strong></label>
                  <label style=" font-size:18px;width:49%;"><strong>Fecha Fin</strong></label>
                    <asp:TextBox ID="txtfinicio" Width="49%" CssClass=" form-control2" TextMode="Date" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtffin" Width="49%" CssClass=" form-control2" TextMode="Date" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-danger btn-block" runat="server" OnClick="LinkButton1_Click">Ejecutar Reporte</asp:LinkButton>
                </div>
                <div class="col-lg-12">
                     <h5><strong>Filtrar</strong></h5>
                    <asp:TextBox ID="txtfiltrar" onfocus="this.select();" CssClass=" form-control2" Width="80%" placeholder="Escriba un valor para filtrar" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="lbkbuscar" CssClass="btn btn-info" Width="18%" runat="server" OnClick="lbkbuscar_Click">Buscar</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success btn-block" runat="server" OnClick="LinkButton2_Click">Exportar a Excel</asp:LinkButton>
                    <h4><strong style="color: orangered; text-align: right;">Total de Solicitudes:</strong>&nbsp;
                <strong>
                    <asp:Label ID="lbltotal" runat="server" Text="0"></asp:Label></strong>
                        </h4>
                    <div class="table table-responsive">
                        <asp:GridView ID="gridcelulares" style=" font-size:12px;" DataKeyNames="idc_horario_perm,idc_puesto,num_nomina,idc_empleado,fecha_normal, empleado" CssClass="table table-responsive table-bordered table-condensed table-responsive" 
                            runat="server"
                            AutoGenerateColumns="false" OnRowCommand="gridcelulares_RowCommand" OnRowDataBound="gridcelulares_RowDataBound">
                            <Columns>
                              <asp:TemplateField HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" OnClientClick="return Giftp('Buscando Datos en Asistencia');"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-info btn-block" runat="server" Text="Detalles" />
                                         </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="idc_horario_perm" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="num_nomina" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="fecha_normal" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="idc_puesto" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="idc_empleado" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="empleado" HeaderText="Empleado" HeaderStyle-Width="100px"></asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha para aplicar" HeaderStyle-Width="150px"></asp:BoundField>
                                <asp:BoundField DataField="empleado_solicito" HeaderText="Solicito" HeaderStyle-Width="150px"></asp:BoundField>
                                <asp:BoundField DataField="observaciones" HeaderText="Motivo" HeaderStyle-Width="200px"></asp:BoundField>
                                <asp:BoundField DataField="estado" HeaderText="Estado" HeaderStyle-Width="30px"></asp:BoundField>
                                <asp:BoundField DataField="incidencia" HeaderText="Incidencia" HeaderStyle-Width="80px"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
       

                     <div class="modal fade modal-info" id="myModalinfo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">


                        <div class="modal-content">
                            <div class="modal-header" style="text-align: center;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4><strong>Mensaje del Sistema</strong></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                        <label style="text-align: right; width: 18%; font-size: 20px"><strong>Empleado</strong></label>
                                        <asp:TextBox Style="font-size: 12px" ID="txtempleado" Width="80%" ReadOnly="true" CssClass="form-control2" runat="server"></asp:TextBox>
                                        <br />
                                        <label style="text-align: right; width: 18%; font-size: 20px"><strong>Status</strong></label>
                                        <asp:TextBox Style="font-size: 20px; text-align: center;" ID="txtstaus" Width="80%" ReadOnly="true" CssClass="form-control2" runat="server"></asp:TextBox>
                                        <asp:TextBox Style="font-size: 20px; text-align: center;" ID="txtincid" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                        <h4 id="nohay" runat="server" style="text-align: center;" visible="false"><strong>No hay Detalles de Asistencia</strong></h4>
                                        <br />
                                        <div class=" table table-responsive">
                                            <asp:GridView ID="grid" CssClass="table table-responsive table-bordered table-condensed"
                                                AutoGenerateColumns="false" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="fecha_str" HeaderText="Dia | Hora" HeaderStyle-Width="50%"></asp:BoundField>
                                                    <asp:BoundField DataField="softkey" HeaderText="Tipo" HeaderStyle-Width="10%"></asp:BoundField>
                                                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" HeaderStyle-Width="30%"></asp:BoundField>
                                                    <asp:BoundField DataField="tipo" HeaderText="Tipo" HeaderStyle-Width="10%"></asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class=" table table-responsive">
                                            <asp:GridView ID="griddetalles_permiso" CssClass="table table-responsive table-bordered table-condensed"
                                                AutoGenerateColumns="false" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="hora_entrada" HeaderText="Hora Entrada"></asp:BoundField>
                                                    <asp:BoundField DataField="hora_salida_comida" HeaderText="Salida Comer"></asp:BoundField>
                                                    <asp:BoundField DataField="hora_entrada_comida" HeaderText="Entrada de Comer"></asp:BoundField>
                                                    <asp:BoundField DataField="hora_salida" HeaderText="Hora Salida"></asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <asp:CheckBox Style="font-size: 20px; font-weight: 800;" Enabled="false" ID="cbxnocomida" Text="No Marcara Incidencia en Comida"
                                            CssClass="radio3 radio-check radio-info" runat="server" />
                                        <asp:CheckBox Style="font-size: 20px; font-weight: 800;" Enabled="false" ID="cbxnosalida" Text="No Marcara Incidencia en Salida"
                                            CssClass="radio3 radio-check radio-info" runat="server" />
                                        <asp:TextBox ID="txtidc_empleado" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtnumeronomina" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="col-lg-12">
                                    <input id="Nop" type="button" class="btn btn-info btn-block" onclick="ModalClose();" value="Cerrar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
