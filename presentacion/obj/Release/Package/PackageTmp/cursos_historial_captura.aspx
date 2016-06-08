<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos_historial_captura.aspx.cs" Inherits="presentacion.cursos_historial_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function irSeccion(Name) {

            document.location.href = Name;

        }
    </script>
    <script type="text/javascript">
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
                <h1>
                    <asp:Literal ID="lit_titulo" runat="server"></asp:Literal></h1>
                <asp:HiddenField ID="oc_paginaprevia" runat="server" />
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">Información Basica del Curso</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label class="etiqueta">Curso</label>
                                        <asp:DropDownList ID="cboxcurso" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboxcurso_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label class="etiqueta">Puesto</label>
                                        <asp:DropDownList ID="cboxpuesto" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label class="etiqueta">Estatus</label>
                                        <asp:DropDownList ID="cboxestatus" CssClass="form-control" runat="server">
                                            <asp:ListItem>Seleccionar</asp:ListItem>
                                            <asp:ListItem Value="T">Terminado</asp:ListItem>
                                            <asp:ListItem Value="P">En proceso</asp:ListItem>
                                            <asp:ListItem Value="C">Cancelado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <div class="form-group">

                                        <h4 id="mensajealerta" runat="server" style="background-color: #D9534F; color: white"></h4>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label class="etiqueta">Mandar al jefe Directo</label>
                                        <asp:DropDownList ID="cboxaprob_capacitacion" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
                                            <asp:ListItem Value="True">Aprobar</asp:ListItem>
                                            <asp:ListItem Value="False">No aprobar</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label class="etiqueta">Resultado</label>
                                        <asp:TextBox ID="txtresultado" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label class="etiqueta">Observaciones</label>
                                        <asp:TextBox ID="txtobservaciones" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--panel body -->
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-red">
                                <div class="panel-heading" style="text-align: center;">Examenes</div>
                                <div class="panel-body">
                                    <!--grid view -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <h4><strong>Debes subir estos examenes</strong> </h4>
                                                <%--<asp:BulletedList ID="listaexamenes" runat="server"></asp:BulletedList>--%>
                                                <div class="table table-responsive">
                                                    <asp:GridView ID="gridExamenes" DataKeyNames="RUTA,descripcion,idc_examen" OnRowCommand="gridExamenes_RowCommand" CssClass=" table table-responsive table-condensed table-bordered" AutoGenerateColumns="false" runat="server">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="" CommandName="Descargar" Text="Descargar">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="idc_curso_exam" HeaderText="Extension" Visible="false">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="idc_examen" HeaderText="Extension" Visible="false">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="descripcion" HeaderText="Descripcion del Examen">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EXAMEN" HeaderText="Examen">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RUTA" HeaderText="Extension" Visible="false">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
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
                            <div class="panel panel-primary">
                                <div class="panel-heading" style="text-align: center;">Añadir Archivo</div>
                                <div class="panel-body" id="col_arch_1">
                                    <!-- archivo descripcion -->
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="etiqueta">Archivo</label>
                                                <asp:FileUpload ID="fileup_curso_arch_1" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="etiqueta">Examen</label>
                                                <asp:DropDownList ID="cboxcurso_examen" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="etiqueta">Resultado</label>
                                                <asp:TextBox ID="txtresexam" CssClass="form-control" type="number" min="0" max="100" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="etiqueta">Observaciones</label>
                                                <asp:TextBox ID="txtobsexam" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">

                                            <div class="form-group">
                                                <asp:Button ID="btnaddfile" runat="server" Text="Agregar" CssClass="btn btn-success btn-block" OnClick="btnaddfile_Click" />
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
                            <div class="table-responsive">
                                <asp:GridView ID="grid_cursos_hist_exam_archivos" CssClass="gvv table table-bordered table-hover grid sortable {disableSortCols: [4]}" runat="server" DataKeyNames="idc_curso_hist_exam" OnRowCommand="grid_cursos_hist_exam_archivos_RowCommand" AutoGenerateColumns="False" OnRowDataBound="grid_cursos_hist_exam_archivos_RowDataBound">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="elimina_archivo" ImageUrl="~/imagenes/btn/icon_delete.png" Text="Eliminar">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="Descargar">
                                            <ItemTemplate>
                                                <%-- <asp:HyperLink ID="linkdescarga"   runat="server">Descargar</asp:HyperLink>--%>
                                                <asp:LinkButton ID="linkdescarga" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="metodo_descarga" runat="server">Descargar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="examen" HeaderText="Examen">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="resultado" HeaderText="Resultado">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="observaciones" HeaderText="Observaciones">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="extension" HeaderText="Extension">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <!-- botones -->

                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btnguardar" runat="server" Text="Guardar" CssClass="btn btn-primary  btn-block" OnClick="btnguardar_Click" />
                            </div>
                            <%--<asp:Button ID="btnprueba" runat="server" Text="Prueba" OnClick="btnprueba_Click" />--%>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btncancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btncancelar_Click" />
                            </div>
                        </div>
                    </div>
                    <!-- campos ocultos -->
                    <asp:HiddenField ID="oc_idc_curso_historial" runat="server" />
                    <asp:HiddenField ID="oc_cursos_x_dar" runat="server" />
                    <asp:HiddenField ID="oc_empleado" runat="server" />
                </div>
            </div>

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