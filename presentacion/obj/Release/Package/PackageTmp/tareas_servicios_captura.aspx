<%@ Page Title="Captura" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_servicios_captura.aspx.cs" Inherits="presentacion.tareas_servicios_captura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Gifts(mensaje) {
            swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '2000', showConfirmButton: false });
        }
         function Editable()
        {
            if ($('#<%= cbxeditable.ClientID %>').is(':checked')) {
                swal('Mensaje del Sistema', 'Con Esta Opción, cuando un usuario solicite una NUEVA TAREA de este servicio, el podra modificar la información(DESCRIPCION Y TIEMPO DE RESPUESTA).', 'info');
            } 
         }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
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
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class=" page-header">Captura de Tareas Servicios
        <span><input type="button" runat="server" id="btncrr" visible="false" value="Cerrar Ventana" onclick="window.close();" class="btn btn-danger" /></span>
    </h2>
            <div class="row">
                <div class=" col-lg-12" runat="server" id="div_detalles">
                    <h4><i class="fa fa-list-alt"></i>&nbsp;Descripción del Servicio</h4>
                    <asp:TextBox ID="txtdescripcion" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 1000);" placeholder="Descripcion" CssClass="form-control"
                        TextMode="MultiLine" Rows="5" runat="server" Style="resize: none; text-transform: uppercase; font-size: 12px;"></asp:TextBox>
                    <h4><i class="fa fa-clock-o"></i>&nbsp;Tiempo de Respuesta en horas</h4>
                    <asp:TextBox ID="txthoras" CssClass=" form-control" TextMode="Number" onkeypress="return validarEnteros(event);" onfocus="$(this).select();"
                        runat="server"></asp:TextBox>
                    <h4><i class="fa fa-bars"></i>&nbsp;Observaciones Generales</h4>
                    <asp:TextBox ID="txtobservaciones" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 240);" placeholder="Observaciones" CssClass="form-control"
                        TextMode="MultiLine" Rows="2" runat="server" Style="resize: none; text-transform: uppercase; font-size: 12px;"></asp:TextBox>
                    <h4><strong><i class="fa fa-question-circle" aria-hidden="true"></i>&nbsp;La información del Servicio sera Editable? </strong>
                        <small>(Al crearce la Tarea el usuario podra modificar la información)</small>
                    </h4>
                    <asp:CheckBox ID="cbxeditable" CssClass="radio3 radio-check radio-info radio-inline" onchange="Editable();" Text="La Información del Servicio Puede ser Editada" runat="server" />
                </div>
                <div class="col-lg-12" id="div_puestos" runat="server">
                    <h4><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Puestos Relacionados</strong></h4>
                    <asp:TextBox ID="txtbuscar" onfocus="$(this).select();" placeholdeR="Buscar Puestos | Empleados" AutoPostBack="true" CssClass=" form-control" runat="server" OnTextChanged="txtbuscar_TextChanged"></asp:TextBox>
                    <br />
                    <div style="padding: 20px; border: 1px solid gray;">
                        <h4 style="text-align: center;"><strong>Listado&nbsp;<i class="fa fa-list" aria-hidden="true"></i></strong>
                            <span> <asp:LinkButton ID="lnksele" CssClass="btn btn-default"  runat="server" OnClick="lnksele_Click">Seleccionar Todos los Mostrados</asp:LinkButton></span>
                        </h4>
                       
                        <div style="background-color: white; width: 100%; height: 200px; overflow-y: auto">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="always">
                                <ContentTemplate>
                                    <asp:Repeater ID="repeat_pues" runat="server">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkpuesto" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:LinkButton Style="font-size: 12px;" ID="lnkpuesto" CssClass="btn btn-default btn-block" runat="server"
                                                        CommandName='<%#Eval("idc_puesto") %>' CommandArgument='<%#Eval("descripcion_puesto_completa") %>'
                                                        Text='<%#Eval("descripcion_puesto_completa") %>' OnClick="lnkpuesto_Click">
                                                    </asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
                     <ContentTemplate>
                         <div class="col-lg-12">
                             <h4><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Puestos Relacionados&nbsp;</strong><span>

                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info" OnClick="LinkButton1_Click">Ver(Actualizar) Listado Seleccionado</asp:LinkButton>
                            </span></h4>
                             <div class=" table table-responsive">
                                 <asp:GridView ID="gridpuestos" DataKeyNames="idc_puesto" CssClass="table table-responsive table-bordered table-condensed" runat="server"
                                     AutoGenerateColumns="false" OnRowCommand="gridpuestos_RowCommand">
                                     <HeaderStyle ForeColor="White" BackColor="Gray" />
                                     <Columns>
                                         <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                                             <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                             <ItemStyle HorizontalAlign="Center" />
                                         </asp:ButtonField>
                                         <asp:BoundField DataField="puesto" HeaderText="Puesto"></asp:BoundField>
                                         <asp:BoundField DataField="idc_puesto" Visible="false"></asp:BoundField>
                                     </Columns>
                                 </asp:GridView>
                             </div>
                         </div>
                     </ContentTemplate>
                     </asp:UpdatePanel>
                
            </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="modal_title"><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4>
                                <label id="content_modal"></label>
                            </h4>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
