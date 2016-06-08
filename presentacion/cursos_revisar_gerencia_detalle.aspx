<%@ Page Title="Detalles" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos_revisar_gerencia_detalle.aspx.cs" Inherits="presentacion.cursos_revisar_gerencia_detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="grid_cursos_exam" />
                </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Aprobar Candidato</div>
                                <div class="panel-body">

                                    <!--grid view -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:RadioButtonList ID="rbtnaprobar_jefe" runat="server" CssClass="etiqueta">
                                                <asp:ListItem Value="True">Aprobar</asp:ListItem>
                                                <asp:ListItem Value="False">Rechazar</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="form-group">
                                                <label class="etiqueta">
                                                    Observaciones
                                                </label>
                                                <asp:TextBox ID="txtobs" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
                                <div class="panel-heading">Cursos</div>
                                <div class="panel-body">
                                    <!--grid view -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grid_cursos" runat="server" CssClass="table table-bordered table-hover" DataKeyNames="idc_curso_historial" OnRowCommand="grid_cursos_RowCommand" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="clic_detalle" HeaderText="Detalle" ImageUrl="~/imagenes/btn/icon_buscar.png" Text="detalle">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="curso" HeaderText="Curso">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="candidato" HeaderText="Candidato">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <!--mensaje oculto -->
                                            <asp:Panel ID="panel_mensaje" runat="server" Visible="False" CssClass="row">
                                                <div class="col-lg-12" style="text-align: center;">
                                                    <h2>No existen registros.</h2>
                                                </div>
                                            </asp:Panel>
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
                                <div class="panel-heading">Quien aprobo</div>
                                <div class="panel-body">

                                    <!--grid view -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grid_cursos_aprobocap" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:BoundField DataField="usuario" HeaderText="Capacitador">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="empleado" HeaderText="Empleado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Registro">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="resultado" HeaderText="Resultado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="observaciones" HeaderText="Observaciones">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <!--mensaje oculto -->
                                            <asp:Panel ID="panel_mensaje_quien_aprobo" runat="server" CssClass="row">
                                                <div class="col-lg-12" style="text-align: center;">
                                                    <h2>
                                                        <asp:Literal ID="lblmensaje_quienaprobo" runat="server"></asp:Literal>
                                                    </h2>
                                                </div>
                                            </asp:Panel>
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
                                <div class="panel-heading">Examenes</div>
                                <div class="panel-body">

                                    <!--grid view -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grid_cursos_exam" runat="server" CssClass="table table-bordered table-hover" DataKeyNames="idc_curso_historial" AutoGenerateColumns="False" OnRowCommand="grid_cursos_exam_RowCommand" OnRowDataBound="grid_cursos_exam_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <%-- <asp:HyperLink ID="linkdescarga"   runat="server">Descargar</asp:HyperLink>--%>
                                                                <asp:LinkButton ID="linkdescarga" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="metodo_descarga" runat="server">Descargar</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="examen" HeaderText="Examen" />
                                                        <asp:BoundField DataField="resultado" HeaderText="Resultado" />
                                                        <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                                                        <asp:BoundField DataField="extension" HeaderText="Extension" />
                                                        <asp:BoundField DataField="idc_curso_historial" HeaderText="idc_curso_historial" Visible="false" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <!--mensaje oculto -->
                                            <asp:Panel ID="panel_mensaje_examenes" runat="server" CssClass="row">
                                                <div class="col-lg-12" style="text-align: center;">
                                                    <h2>
                                                        <asp:Literal ID="lblmensaje_examenes" runat="server"></asp:Literal>
                                                    </h2>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <!--panel body -->
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- botones -->

            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnguardar" runat="server" Text="Aceptar" CssClass="btn btn-success  btn-block" OnClick="btnguardar_Click" />
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
            <asp:HiddenField ID="oc_idc" runat="server" />
            <asp:HiddenField ID="oc_tipoemp" runat="server" />

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