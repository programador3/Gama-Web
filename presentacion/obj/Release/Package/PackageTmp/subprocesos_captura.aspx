<%@ Page Title="Sub procesos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="subprocesos_captura.aspx.cs" Inherits="presentacion.subprocesos_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalArchi(value) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#ModalArchivos').modal('show');
            $('#confirmContenidoarchivos').text(value);
        }
        function ModalClose() {
            $('#myModal').modal('hide');
            $('#ModalArchivos').modal('hide');
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
    <h1 class="page-header"> Manual de Procesos
        <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
        <span>
            <asp:Label ID="lnltipo" runat="server" Text=" Tipo Borrador" CssClass="btn btn-primary"></asp:Label></span>
    </h1>
    <asp:HiddenField ID="hiddenvalue" runat="server" />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary fresh-color">
                <div class="panel-heading" style="text-align: center;">Proceso Principal</div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <h4><strong><i class="fa fa-cog" aria-hidden="true"></i>&nbsp;Proceso Principal</strong></h4>
                            <asp:TextBox ID="txtdescproceso" placeholder="Escriba el Nombredel Proceso" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <h4><strong><i class="fa fa-files-o" aria-hidden="true"></i>&nbsp;Archivos Anexos al Proceso Principal</strong></h4>
                            <h5><strong>Subir Archivos</strong></h5>
                            <asp:TextBox ID="txtobsrarchivo" CssClass="form-control" placeholder="Ingrese una descripcion para el archivo" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 200);" runat="server"></asp:TextBox>
                            <br />
                            <div class="input-group">
                                <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                                <span class="input-group-addon" style="color: #fff; background-color: #1ABC9C;">
                                    <asp:LinkButton ID="lnkaddfile" Style="color: #fff; background-color: #1ABC9C;" OnClick="lnkaddfile_Click" runat="server">Agregar Archivo  <i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
                                </span>
                            </div>
                           
                         
                            <br />
                            <div class="table table-responsive">
                                <asp:GridView ID="gridarchivos" runat="server" DataKeyNames="idc_procesosarc,url,observaciones" CssClass="table table-responsive table-bordered" OnRowCommand="gridarchivos_RowCommand" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="observaciones" HeaderText="Descripcion"></asp:BoundField>
                                        <asp:BoundField DataField="url" HeaderText="Descripcion" Visible="false"></asp:BoundField>
                                        <asp:BoundField DataField="idc_procesosarc" HeaderText="idc_procesosarc" Visible="false"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkEditar" />
                    <asp:PostBackTrigger ControlID="lnkDescargarArchi" />
                </Triggers>
                <ContentTemplate>
                    <div class="panel panel-primary fresh-color">
                        <div class="panel-heading" style="text-align: center;">Sub Procesos</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4><strong><i class="fa fa-cogs" aria-hidden="true"></i>&nbsp;SubProceso</strong></h4>
                                    <asp:TextBox ID="txtsubproceso" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" placeholder="Escriba la descripcion o encabezado del subproceso" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Perfiles Relacionados al SubProceso</strong><small> Seleccione los perfiles relacionados</small></h4>
                                    <div style="height: 185px; overflow: scroll;">
                                        <asp:CheckBoxList ID="cbxlperfiles" CssClass="radio3 radio-check radio-info radio-inline" runat="server"></asp:CheckBoxList>
                                    </div>
                                    <br />
                                    <asp:LinkButton ID="lnkagregar" CssClass="btn btn-success btn-block" OnClick="lnkagregar_Click" runat="server">Agregar SubProceso <i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4><strong><i class="fa fa-list" aria-hidden="true"></i>&nbsp;Sub Procesos</strong></h4>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="grid_subprocesos" runat="server" DataKeyNames="idc_subproceso,descripcion,url,orden" CssClass="table table-responsive table-bordered" OnRowCommand="grid_subprocesos_RowCommand" AutoGenerateColumns="False" OnRowDataBound="grid_subprocesos_RowDataBound">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/more.png" HeaderText="Ver Perfiles" CommandName="Ver">
                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Image" CommandName="editar" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="idc_subproceso" HeaderText="Descripcion" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion" Visible="true"></asp:BoundField>
                                                <asp:BoundField DataField="orden" HeaderText="Orden" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="url" HeaderText="Descripcion" Visible="false"></asp:BoundField>
                                                <asp:ButtonField ButtonType="Image" CommandName="view_file" ImageUrl="~/imagenes/btn/icon_buscar.png" HeaderText="Ver" ShowHeader="False" Visible="true">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Image" CommandName="add_file" ImageUrl="~/imagenes/anexar.png" HeaderText="Archivo" ShowHeader="False" Visible="true">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                </asp:ButtonField>
                                                <asp:TemplateField HeaderText="Orden">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel runat="server" ID="UpId"
                                                            UpdateMode="Always" ChildrenAsTriggers="true">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtOrder" CssClass="form-control" Text="" AutoPostBack="true" OnTextChanged="txtOrder_TextChanged" onkeypress="return validarEnteros(event);" runat="server" CommandName="change_order" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="ModalArchivos" class="modal fade bs-example-modal-lg" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content" style="text-align: center">
                                <div class="modal-header" style="background-color: #428bca; color: white">
                                    <h4><strong class="modal-title">Archivos Anexados </strong></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="lnkEditar" CssClass="btn btn-warning btn-block" runat="server" OnClick="lnkEditar_Click">Editar Archivo <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="lnkDescargarArchi" CssClass="btn btn-info btn-block" runat="server" OnClick="lnkDescargarArchi_Click">Descargar Archivo <i class="fa fa-cloud-download"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <input id="noarchi" class="btn btn-primary btn-block" onclick="ModalClose();" value="Cerrar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
        </div>
    </div>
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
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>