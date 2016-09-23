<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos_captura.aspx.cs" Inherits="presentacion.cursos_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function irSeccion(Name) {

            document.location.href = Name;

        }
        function ModalClose() {
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                allowEscapeKey: false
            },
               function () {
                   location.href = URL;
               });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <div class="page-header">
                <h1><span>
                    <asp:Literal ID="lit_titulo" runat="server"></asp:Literal></span></h1>
                <asp:HiddenField ID="oc_paginaprevia" runat="server" />
            </div>

            <div class="row">
                <div class="col-lg-9">
                    <h3>
                        <asp:Label ID="lblmensaje" runat="server" Text=""></asp:Label></h3>
                </div>
                <div class="col-lg-3">

                    <asp:CheckBox ID="cbxTipo" runat="server" Text="borrador" Visible="False" />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary  fresh-color">
                        <div class="panel-heading">Cursos</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <label class="etiqueta">Descripción</label>
                                    <asp:TextBox ID="txtdesc" onkeypress="return isNumber(event);" Style="text-transform: uppercase;" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-lg-12">
                                    <label class="etiqueta">Tipo Curso</label>
                                    <asp:DropDownList ID="cbox_tipocurso" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="I">Interno</asp:ListItem>
                                        <asp:ListItem Value="E">Externo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <!--panel body -->
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary  fresh-color">
                        <div class="panel-heading">Perfiles</div>
                        <div class="panel-body">

                            <!--grid view -->
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:LinkButton ID="LBKapli" CssClass="btn btn-success" OnClick="LBKapli_Click" runat="server" Text="Aplicar a Todos"></asp:LinkButton>
                                    <div style="overflow-y: scroll; height: 250px; border: 1px solid; padding: 15px;">
                                        <div class="table-responsive">
                                            <asp:CheckBoxList ID="check_curso_perfil" runat="server" CssClass="radio3 radio-check radio-info radio-inline"></asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--panel body -->
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <asp:Panel ID="panel_obs" runat="server">

                        <div class="panel panel-primary  fresh-color">
                            <div class="panel-heading">Observaciones</div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <label class="etiqueta">Observaciones</label>
                                    <asp:TextBox TextMode="MultiLine" Rows="3" placeholder="Observaciones" ID="txtobservaciones" onkeypress="return isNumber(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <!--panel body -->
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary fresh-color">
                        <div class="panel-heading" style="text-align: center;">Examenes Aplicables</div>
                        <div class="panel-body">
                            <!--grid view -->
                            <div class="row">
                                <div class="col-lg-12" style="padding: 15px;">
                                    <asp:CheckBoxList ID="chxExamenes" CssClass="radio3 radio-check radio-info radio-inline" runat="server"></asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                        <!--panel body -->
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">

                    <div class="panel panel-primary  fresh-color">
                        <div class="panel-heading">Añadir Archivo</div>
                        <div class="panel-body">
                            <!-- archivo descripcion -->
                            <div class="row">
                                <div id="col_arch_1" class="col-lg-12">
                                    <div class="form-group">

                                        <label class="etiqueta">Archivo</label>
                                        <div class="input-group">
                                            <asp:FileUpload ID="fileup_curso_arch_1" runat="server" CssClass="form-control" />
                                            <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                <asp:LinkButton ID="lnkaddfile" Style="color: #fff; background-color: #337ab7;" OnClick="btnaddfile_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="etiqueta">Descripción</label>
                                        <asp:TextBox ID="txtdescarchivo_1" onkeypress="return isNumber(event);" CssClass="form-control" runat="server" TextMode="MultiLine" Style="resize: none;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <!--fin archivo descripcion        -->
                        </div>
                        <!--panel body -->
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grid_cursos_archivos" runat="server" DataKeyNames="id_archi, descripcion" CssClass="table table-bordered table-hover table-striped" OnRowCommand="grid_cursos_archivos_RowCommand" AutoGenerateColumns="False" OnRowDataBound="grid_cursos_archivos_RowDataBound">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="Editar" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="40px" CommandName="deletearchivo" ImageUrl="~/imagenes/btn/icon_delete.png" Text="Borrar" />
                                <asp:TemplateField HeaderText="Descargar" HeaderStyle-Width="40px">
                                    <ItemTemplate>
                                        <%-- <asp:HyperLink ID="linkdescarga"   runat="server">Descargar</asp:HyperLink>--%>
                                        <asp:LinkButton ID="linkdescarga" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="metodo_descarga" runat="server">Descargar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id_archi" HeaderText="Idc_curso_arc" Visible="False" />
                                <asp:BoundField DataField="path" HeaderText="Path" Visible="False" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                <asp:BoundField DataField="extension" HeaderText="Extensión" />
                                <asp:BoundField DataField="nuevo" HeaderText="Nuevo" Visible="False" />
                                <asp:BoundField DataField="borrado" HeaderText="Borrado" Visible="False" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- botones -->

            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-sm-12">
                    <div class="form-group">
                        <asp:Button ID="btnGuardarTodo" runat="server" Text="Guardar" CssClass="btn btn-success  btn-block" OnClick="btnGuardarTodo_Click" />
                    </div>
                    <%--<asp:Button ID="btnprueba" runat="server" Text="Prueba" OnClick="btnprueba_Click" />--%>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-sm-12">
                    <div class="form-group">
                        <asp:Button ID="btnCancelarTodo" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelarTodo_Click" />
                    </div>
                </div>
            </div>
            <!-- campos ocultos -->
            <asp:HiddenField ID="oc_idc_curso_borr" runat="server" Value="0" />
            <asp:HiddenField ID="oc_idc_curso" runat="server" Value="0" />
            <!-- /.CONFIRMA -->
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="confirmContenido"></label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>