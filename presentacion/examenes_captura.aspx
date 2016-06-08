<%@ Page Title="Captura de Examenes" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="examenes_captura.aspx.cs" Inherits="presentacion.examenes_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalView() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
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
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Captura de Examen</h1>
                </div>
            </div>
            <div class="row">

                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h4 style="text-align: center;">Datos Principales</h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4>Nombre del Examen o Descripcion</h4>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="200" placeholder="Nombre del Examen" Style="text-transform: uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-8 col-sm-10 col-xs-12">
                                    <h4>Tipo de Examen</h4>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                                            <asp:ListItem Selected="True" Value="0" Text="-Seleccione uno"></asp:ListItem>
                                            <asp:ListItem Value="R" Text="PARA RECLUTAMIENTO"></asp:ListItem>
                                            <asp:ListItem Value="C" Text="PARA CURSOS"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-green">
                        <div class="panel-heading" style="text-align: center;">
                            Archivos
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="form-control" placeholder="Descripcion del documento"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                                            <span class="input-group-addon" style="color: #fff; background-color: #5cb85c;">
                                                <asp:LinkButton ID="lnkGuardarPape" Style="color: #fff; background-color: #5cb85c;" OnClick="lnkGuardarPape_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="id_archi,descripcion, nombre, ruta, extension">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="extension" HeaderText="extension" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre Documento"></asp:BoundField>
                                                <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion"></asp:BoundField>
                                                <asp:BoundField DataField="id_archi" HeaderText="id_archi" Visible="false"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                    </div>
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