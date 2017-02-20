<%@ Page Title="Detalles Tareas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_detalles.aspx.cs" Inherits="presentacion.tareas_detalles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#myModalReasigna').modal('hide');
            $('#myModal').modal('hide');
            $('#myModalMov').modal('hide');
            $('#myModalCF').modal('hide');
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
        function ModalReasignaTarea() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalReasigna').modal('show');
        }
        
        function ModalMov(cTitulo) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalMov').modal('show');
            $('#modal_titlemov').text(cTitulo);
        }
        function ModalCF() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalCF').modal('show');
        }
        function disbalebutton() {
            $('#<%= Yes.ClientID%>').prop('disabled', false);
        }
        function campoVacio() {

            var obs = $('#<%= txtcambio_desc.ClientID%>').val().replace(/\s+/g, '');
            if (obs.length < 1) {
                //inhabilita el boton aceptar
                $('#<%= Yes.ClientID%>').prop('disabled', false);

            } else {
                $('#<%= Yes.ClientID%>').prop('disabled', false);
            }
        }
        function btn() {
            $('#<%= Yes.ClientID%>').prop('disabled', false);
        }
        function AsignaProgress(value) {
            $('#pavance').width(value);
        }
        function campoVacioVbno() {
            var obs = $('#<%= txtcomentarios_vbno.ClientID%>').val().replace(/\s+/g, '');
            if (obs.length < 1) {
                //inhabilita el boton aceptar
                $('#<%= Yes.ClientID%>').prop('disabled', false);

            } else {
                $('#<%= Yes.ClientID%>').prop('disabled', false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <h3 class="page-header">Detalles de Tarea <small>
                <asp:Label ID="lbltitle" runat="server" Text=" Tarea Autogenerada" Visible="false"></asp:Label></small> </h3>
            <div class="row">
                <div class="col-lg-12">
                    <h4><i class="fa fa-list-alt"></i>&nbsp;Descripcion de la tarea<span>
                        <asp:LinkButton Visible="false" ID="lnkurladicinal" CssClass="btn btn-success" OnClick="lnkurladicinal_Click" runat="server">
                            Ver Información Adicional <i class="fa fa-share" aria-hidden="true"></i></asp:LinkButton>
                    </span></h4>
                    <asp:TextBox ID="txtdescripcion" ReadOnly="true" placeholder="Descripcion" CssClass="form-control" TextMode="MultiLine" Rows="3" onblur="return imposeMaxLength(this, 1000);" runat="server" Style="resize: none; text-transform: uppercase;"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-xs-12">
                    <h4><i class="fa fa-calendar"></i>&nbsp;Fecha Estipulada de Compromiso
                        <span>
                            <asp:LinkButton Visible="false" ID="lnkCambiarFechaF" CssClass="btn btn-info" OnClick="lnkCambiarFechaF_Click" runat="server">Editar Fecha <i class="fa fa-pencil" aria-hidden="true"></i></asp:LinkButton>
                        </span>
                    </h4>
                    <asp:TextBox ID="txtfecha_solicompromiso" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12 col-xs-12">
                    <h4><i class="fa fa-pencil-square-o"></i>&nbsp;Puesto que Asigno la Tarea</h4>
                    <asp:TextBox ID="txtpuesto_asigna" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-12 col-sm-12 col-xs-12">
                    <h4><i class="fa fa-wrench"></i>&nbsp;Puesto Que realizara la Tarea
                         <span>
                            <asp:LinkButton Visible="false" ID="lnkreasigna" CssClass="btn btn-danger" runat="server" OnClick="lnkreasigna_Click">
                                Reasignar Tarea <i class="fa fa-refresh" aria-hidden="true"></i></asp:LinkButton>
                        </span>
                    </h4>
                    <asp:TextBox ID="txtpuesto" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row" id="DD" runat="server" visible="true">
                <div class="col-lg-12">
                    <h4><i class="fa fa-level-down"></i>&nbsp;Tareas que se generaron a partir de esta Tarea <small>Esta Tarea depende de la siguientes: </small>
                        <span>
                            <asp:LinkButton Visible="false" ID="lnkarbol" CssClass="btn btn-success" OnClick="lnkarbol_Click" runat="server">Ver Arbol de Tareas 
                                <i class="fa fa-pencil" aria-hidden="true"></i></asp:LinkButton>
                        </span>
                    </h4>
                    <h4 id="no_apen" runat="server" style="text-align: center;">No hay tareas anidadas <i class="fa fa-thumbs-o-up"></i></h4>
                    <asp:Repeater ID="repeat_mis_tareas_asignadas" runat="server">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnkmistarea_asig" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                        <asp:LinkButton ID="lnkmistarea_asig" CssClass="btn btn-success btn-block" ToolTip='<%#Eval("desc_completa") %>' runat="server" CommandName='<%#Eval("idc_tarea") %>' OnClick="lnkmistarea_Click">
                                            <h5><%#Eval("descripcion") %></h5>
                                     <h5>Fecha Compromiso: <%#Eval("fecha_compromiso") %></h5>
                                     <h5>Realiza: <%#Eval("empleado") %></h5>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row" id="file_guardssar" runat="server" visible="false">
                <div class="col-lg-12">
                    <h4><i class="fa fa-file-archive-o"></i>&nbsp;Comentarios Anexados a la Tarea</h4>
                    <h4 id="noarchivos" runat="server" style="text-align: center;">No hay comentarios anexados <i class="fa fa-thumbs-o-up"></i></h4>
                    <asp:Repeater ID="repe_archivos" runat="server">
                        <ItemTemplate>

                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12">
                                <asp:LinkButton ID="lnkarchi" CssClass="btn btn-primary btn-block" runat="server" CommandName='<%#Eval("ruta") %>' CommandArgument=' <%#Eval("archivo") %>' OnClick="lnkmistarea_asig_Click">
                                     <h5><%#Eval("descripcion") %></h5>
                             <h5> <%#Eval("empleado") %></h5>
                             <h5> <%#Eval("puesto") %></h5>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="row" id="proveedores" runat="server" visible="true">
                <div class="col-lg-12">
                    <h4><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Proveedores Externos Relacionados a esta Tarea</h4>
                    <asp:UpdatePanel ID="dddd" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Repeater ID="repeat_proovedore_info" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-info btn-block" Text='<%#Eval("nombre") %>' CommandName='<%#Eval("observaciones") %>' OnClick="Button1_Click" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <h4>
                        <i class="fa fa-random" aria-hidden="true"></i>&nbsp;Avance de la Tarea </h4>

                    <asp:Label ID="lblprogress" runat="server" Text=""></asp:Label>
                    <div class="progress">
                        <div id="pavance" class="progress-bar progress-bar-success" role="progressbar" aria-valuemin="0" aria-valuemax="100">
                        </div>
                    </div>
                    <asp:Panel ID="panel_avance" Visible="false" runat="server">
                        <h4>&nbsp;Cambiar Avance de la Tarea
                    <asp:TextBox ID="txtavance" onblur="ValidateRange(this,1,100,'El valor debe ser de 0 - 100');" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" Width="100" TextMode="Number" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lnkavanceadd" runat="server" CssClass="btn btn-success" OnClick="lnkavanceadd_Click">Agregar Avance</asp:LinkButton>
                        </h4>
                    </asp:Panel>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <h4><i class="fa fa-retweet"></i>&nbsp;Movimientos de la Tarea <small></h4>
                    <h4 id="hmov" runat="server" style="text-align: center;">No hay movimientos <i class="fa fa-thumbs-o-up"></i></h4>
                    <asp:Repeater ID="repeat_movimiento" runat="server" OnItemDataBound="repeat_movimiento_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <asp:LinkButton ID="lnkarchi2" CssClass='<%#Eval("cssclass") %>' runat="server" CommandArgument=' <%#Eval("idc_tarea_historial") %>' CommandName='<%#Eval("tipo") %>' OnClick="lnkarchi_Click">
                                     <h5><%#Eval("tipo_completo") %></h5>
                                     <h5> <%#Eval("empleado") %></h5>
                                     <h5> <%#Eval("fecha_movimiento") %></h5>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <br />

            <div class="row" id="nueva" runat="server" style="padding: 5px; border-radius: 0px 0px 0px 0px; -moz-border-radius: 0px 0px 0px 0px; -webkit-border-radius: 0px 0px 0px 0px; border: 1px solid #000000;">
                <div class="col-lg-12">
                    <h4><i class="fa fa-share-alt"></i>&nbsp; Puede Generar nuevas Tareas, teniendo como principal esta Tarea
                 </h4>
                    <asp:LinkButton ID="lnkGO" runat="server" CssClass="btn btn-info btn-block" OnClick="lnkGO_Click">Nueva Tarea  <i class="fa fa-plus-circle"></i></asp:LinkButton>
                </div>
            </div>
            <br />
            <div class="row" id="file_guardar" runat="server" visible="false" style="padding: 5px; border-radius: 0px 0px 0px 0px; -moz-border-radius: 0px 0px 0px 0px; -webkit-border-radius: 0px 0px 0px 0px; border: 1px solid #000000;">
                <div class="col-lg-12 col-xs-12">
                    <h4><i class="fa fa-file-archive-o"></i>&nbsp;Anexar Comentario(s) <small>Puede anexar archivos</small></h4>
                    <asp:TextBox ID="txtNombreArchivo" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" runat="server" Style="text-transform: uppercase;" CssClass="form-control" placeholder="Comentario"></asp:TextBox>
                </div>
                <div class="col-lg-12 col-xs-12">
                    <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                    <br />
                    <asp:LinkButton ID="lnkGuardarPape" CssClass="btn btn-primary btn-block" OnClick="lnkGuardarPape_Click" runat="server">Agregar Comentario <i class="fa fa-plus-circle"></i> </asp:LinkButton>


                </div>
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gridPapeleria" OnRowDataBound="gridPapeleria_RowDataBound" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="archivo,idc_tarea_archivo,descripcion, ruta, extension">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="extension" HeaderText="extension" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Comentario"></asp:BoundField>
                                <asp:BoundField DataField="idc_tarea_archivo" HeaderText="id_archi" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="archivo" HeaderText="id_archi" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="empleado" HeaderText="Empleado" Visible="true"></asp:BoundField>
                                <asp:BoundField DataField="puesto" HeaderText="Puesto" Visible="true"></asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" Visible="true"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br />
            <div class="row" id="col_camfechacom" runat="server" visible="false" style="padding: 5px; border-radius: 0px 0px 0px 0px; -moz-border-radius: 0px 0px 0px 0px; -webkit-border-radius: 0px 0px 0px 0px; border: 1px solid #000000;">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <h4><i class="fa fa-calendar"></i>&nbsp;Solicitar Cambio de Fecha<small> Puede cambiar Solicitar un cambio de Fecha Compromiso</small></h4>
                    <asp:TextBox ID="txtfecha_compromiso_externo" CssClass="form-control" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
                </div>
                <div class="col-lg-12 col-xs-12">
                    <asp:TextBox ID="txtobsrfechaexternoi" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Ingrese un Motivo" runat="server"></asp:TextBox>
                </div>
                <div class="col-lg-12 col-xs-12">
                    <asp:LinkButton ID="lnkcambiof" OnClick="lnkcambiof_Click" CssClass="btn btn-danger btn-block" runat="server">Solicitar Cambio de Fecha</asp:LinkButton>
                </div>
            </div>
            <asp:Panel ID="panelGuardar" runat="server" Visible="true">

                <br />
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <asp:Button ID="btnTerminar" runat="server" Text="Terminar Tarea" CssClass="btn btn-primary btn-block" OnClick="btnTerminar_Click" Visible="false" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        
                        <asp:Button ID="btnTerminarVBNO" runat="server" Text="Reasignar Tarea" CssClass="btn btn-primary btn-block" OnClick="btnTerminarVBNO_Click" Visible="false" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <asp:Button ID="btnCancelarTodo" Visible="false" runat="server" Text="Cancelar el Proceso de la Tarea" CssClass="btn btn-danger btn-block" OnClick="btnCancelarTodo_Click" />
                    </div>
                </div>
            </asp:Panel>
            <!-- Modal -->
            <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                            <asp:PostBackTrigger ControlID="Yes" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header" style="text-align: center;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 id="modal_title"><strong>Mensaje del Sistema</strong></h4>
                                </div>
                                <div class="modal-body" style="text-align: center;">
                                    <div class="row" style="text-align: center;" id="CONETNIDO" runat="server">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                            <h4>
                                                <label id="content_modal"></label>
                                            </h4>
                                        </div>
                                    </div>
                                    <div class="row" id="row_desccambio" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <asp:TextBox ID="txtfecha_pasada" Visible="false" ReadOnly="false" TextMode="DateTimeLocal" runat="server" CssClass="form-control"></asp:TextBox>
                                            <h4><i class="fa fa-list-alt"></i>&nbsp;Comentarios</h4>
                                            <asp:TextBox ID="txtcambio_desc" onfocus="btn();" onblur="campoVacio();" Visible="true" ReadOnly="false" placeholder="Comentarios" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Rows="3" runat="server" Style="resize: none; text-transform: uppercase;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="update_f" UpdateMode="Always" runat="server">
                                        <ContentTemplate>
                                            <div class="row" id="row_vbno" runat="server" visible="false">
                                                <div class="col-lg-12">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Button ID="btnfecha_est" runat="server" Text="" CssClass="btn btn-default btn-block" />
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <asp:Button ID="btnfecha_term" runat="server" Text="" CssClass="btn btn-default btn-block" />
                                                        </div>
                                                    </div>

                                                    <h4><i class="fa fa-list-alt"></i>&nbsp;Comentarios</h4>
                                                    <asp:TextBox ID="txtcomentarios_vbno" onfocus="btn();" onblur="campoVacio();" Visible="true" ReadOnly="false" placeholder="Comentarios" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Rows="2" runat="server" Style="resize: none; text-transform: uppercase;"></asp:TextBox>
                                                    <div class="row Ocultar" >
                                                        <h4><strong>¿La tarea fue realizada satisfactoriamente y en tiempo acordado?</strong></h4>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                                            <asp:Button ID="btncorrectvbno" runat="server" Text="Si" CssClass="btn btn-default btn-block" OnClick="btncorrectvbno_Click" />
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                                            <asp:Button ID="btnincorrectovbno" runat="server" Text="No" CssClass="btn btn-default btn-block" OnClick="btnincorrectovbno_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="canceladas" runat="server" visible="false">
                                                <h4><strong>¿Contar como MAL RESULTADO a el empleado?</strong></h4>
                                                <div class="col-lg-12 col-xs-12">
                                                    <asp:Button ID="btnmalcancelada" runat="server" Text="Si contar como mal resultado" CssClass="btn btn-default btn-block" OnClick="btncorrectvbno_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="modal fade modal-info" id="myModalMov" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog ">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 id="modal_titlemov"><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <h5><strong><i class="fa fa-calendar"></i>&nbsp; Fecha de Compromiso Actual: </strong>
                                <asp:Label ID="lblfecha" runat="server" Text="Label"></asp:Label></h5>
                            <h5><strong><i class="fa fa-calendar"></i>&nbsp; Fecha Original de Compromiso: </strong>
                                <asp:Label ID="lblfecha_original" runat="server" Text="Label"></asp:Label></h5>
                            <h5 id="ldinamico" runat="server"><strong><i class="fa fa-calendar"></i>&nbsp;<asp:Label ID="lbldinamico" runat="server" Text=""></asp:Label></strong></h5>
                            <h5><strong><i class="fa fa-user"></i>&nbsp;Empleado: </strong>
                                <asp:Label ID="lblempleado" runat="server" Text="Label"></asp:Label></h5>
                            <h5><strong>&nbsp;Puesto: </strong>
                                <asp:Label ID="lblpuesto" runat="server" Text="lblpuesto"></asp:Label></h5>
                            <h5><strong><i class="fa fa-pencil-square-o"></i>&nbsp;Movimiento: </strong>
                                <asp:Label ID="lbltipo" runat="server" Text="Label"></asp:Label></h5>
                            <h5><strong><i class="fa fa-list-alt"></i>&nbsp;Comentarios del Movimiento: </strong>
                                <asp:Label ID="lbldes" runat="server" Text="Label"></asp:Label></h5>
                            <div class="row">
                                <asp:Panel ID="panel_captura_fecha" runat="server" Visible="true" CssClass="col-lg-12">
                                    <h5><i class="fa fa-calendar"></i>&nbsp;Nueva Fecha de Compromiso </h5>
                                    <asp:TextBox ID="txtnueva_fecha" ReadOnly="false" TextMode="DateTimeLocal" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:UpdatePanel ID="upda_proovc" runat="server" Visible="false" UpdateMode="Always">
                                        <ContentTemplate>
                                            <h5><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Relacionar Proveedor Externo </h5>
                                            <div class="row" style="overflow-y: scroll; height: 100px;">
                                                <asp:Repeater ID="repeat_proovedores" runat="server">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnk_provee" CommandName='<%#Eval("idc_proveedor") %>' CssClass="btn btn-default btn-block" OnClick="lnk_provee_Click" runat="server">
                                                            <%#Eval("nombre") %>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <asp:TextBox ID="txtcomentarios_proo" ReadOnly="false" placeholder="Ingrese un Motivo para Relacionar Proveedor" runat="server" CssClass="form-control"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <div class="col-lg-12" id="cambiofechaextra" runat="server" visible="false">
                                    <asp:LinkButton ID="lnksolicitar_cam" Visible="false" runat="server" CssClass="btn btn-danger btn-block" OnClick="lnksolicitar_cam_Click">Cambiar Fecha Compromiso (EXTRAORDINARIO)</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                <asp:Button ID="btnGuardar" runat="server" Text="Aceptar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" Visible="false" />
                                <asp:Button ID="BTNCANCELARGUARDAR" runat="server" Text="Aceptar" CssClass="btn btn-primary btn-block" OnClick="btnCancelarTodo_Click" Visible="false" />
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                <asp:Button ID="btnCancelar" runat="server" Text="Rechazar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                <input id="Nos" type="button" class="btn btn-info btn-block" onclick="ModalClose();" value="Cerrar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade modal-info" id="myModalCF" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="text-align: center;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>Como solicitante de la tarea usted puede cambiar la Fecha Compromiso Directamente
                                    </h4>
                                    <asp:TextBox ID="txtfechacompromisodirecto" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtobsrcfd" runat="server" TextMode="MultiLine" Rows="3" placeholder="Observaciones" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="btnsifecdirecto" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="btnsifecdirecto_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="Ndddo" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                                </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade modal-info" id="myModalReasigna" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-12 col-sm-12 col-xs-12">
                                            <label>
                                                Como solicitante de la tarea usted puede cambiar la Fecha Compromiso Directamente
                                            </label>
                                            <asp:TextBox ID="txtfecha_reasigna" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <label>Selecciona un Empleado </label>
                                            <asp:DropDownList ID="ddlPuesto" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <label>Escriba un Filtro</label>
                                            <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <label></label>
                                            <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-lg-12">
                                            <label>Motivo de la Reasignacion</label>
                                            <asp:TextBox ID="txtmotivo" runat="server" CssClass="form-control" TextMode="Multiline" placeholder="Motivo" Rows="3" Style="resize: none;"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-12 col-xs-12">
                                            <label><strong>¿Contar como MAL RESULTADO a el empleado?</strong></label>
                                            <asp:Button ID="btnreasignamalresultado" runat="server" Text="Si contar como mal resultado" CssClass="btn btn-default btn-block" OnClick="btncorrectvbno_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                                <div class="col-lg-6 col-xs-6">

                                    <asp:Button ID="Button2" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Button2_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="Ndddoss" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>