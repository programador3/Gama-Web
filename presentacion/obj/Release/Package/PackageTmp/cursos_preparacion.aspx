<%@ Page Title="Preparación de Cursos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos_preparacion.aspx.cs" Inherits="presentacion.cursos_preparacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function GoSection(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 500);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: "success",
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
        function getImage(path) {
            $("#myImage").attr("src", path);
        }

        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        } ModalPreviewHeramienta
        function ModalPreviewHeramienta() {
            $('#modalPreviewView').modal('show');
        }
        function ModalPreview(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 1000);
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');

        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <br />
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" runat="server" OnClick="lnkReturn_Click" Visible="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Preparación de Cursos </h1>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12" style="text-align: left">
                    <div class="form-group">
                        <h3><strong>Fecha de Solicitud
                            <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                            : </strong>
                            <asp:Label ID="lblfechasoli" runat="server" Text=""></asp:Label>
                        </h3>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PanelRevision" runat="server" CssClass="panel panel-primary">
                <div class="panel-heading" style="text-align: center">
                    <h3 class="panel-title">Listado de Cursos <i class="fa fa-list"></i></h3>
                </div>
                <div class="panel-body" style="background-color: #E6E6E6">
                    <div class="row">
                        <div class="col-lg-6 col-md-3 col-xs-12" style="text-align: center;">
                            <h4><strong>Curso <i class="fa fa-book"></i></strong></h4>
                        </div>
                        <div class="col-lg-2 col-md-2 col-xs-12" style="text-align: center;">
                            <h4><strong>Tipo de Curso <i class="fa fa-user"></i></strong>
                            </h4>
                        </div>
                        <div class="col-lg-4 col-md-4 col-xs-12" style="text-align: center;">
                            <h4><strong>Pendiente <i class="fa fa-folder"></i></strong>
                            </h4>
                        </div>
                    </div>
                    <asp:Repeater ID="repeat_cursos" runat="server" OnItemDataBound="repeat_cursos_ItemDataBound">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblidc_curso" runat="server" Text='<%#Eval("idc_curso") %>' Visible="false"></asp:Label>
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                <i class="fa fa-book"></i></span>
                                            <asp:TextBox ID="txt1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion_curso") %>'></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                <i class="fa fa-book"></i></span>
                                            <asp:TextBox ID="txttipo" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text="Interno"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                <i class="fa fa-check-square-o"></i></span>
                                            <asp:CheckBox ID="cbx" runat="server" class="input-sm" Text="Pendiente" TextAlign="Right" Checked="false" Enabled="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                <i class="fa fa-comments-o"></i></span>
                                            <asp:TextBox ID="txtObservaciones" runat="server" class="form-control" Text="" placeholder="Observaciones" TextMode="MultiLine" Rows="2" MaxLength="250" Style="resize: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
            <asp:Panel ID="panel_archvios" CssClass="panel panel-primary" Visible="false" runat="server">
                <div class="panel-heading" style="text-align: center">
                    <h3 class="panel-title">Archivos Adjuntos al Curso <i class="fa fa-list"></i></h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-xs-12" style="text-align: center;">
                            <h4><strong>Nombre de Archivo <i class="fa fa-book"></i></strong></h4>
                        </div>
                        <div class="col-lg-3 col-md-6 col-xs-12" style="text-align: center;">
                            <h4><strong>Descarga <i class="fa fa-download"></i></strong>
                            </h4>
                        </div>
                    </div>
                    <asp:Repeater ID="repeater_archi" runat="server" OnItemDataBound="repeater_archi_ItemDataBound">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-lg-6  col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                <i class="fa fa-book"></i></span>
                                            <asp:TextBox ID="txt1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkDescargarFormato" Visible="true" runat="server" class="btn btn-info btn-block" OnClick="lnkDescargarFormato_Click">Descargar <i class="fa fa-download"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-red">
                        <div class="panel-heading" style="text-align: center;">Examenes</div>
                        <div class="panel-body">
                            <!--grid view -->
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <h4><strong>Examenes Relacionados a los Cursos</strong> </h4>
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
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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