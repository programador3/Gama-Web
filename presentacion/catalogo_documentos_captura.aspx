<%@ Page Title="Documentos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_documentos_captura.aspx.cs" Inherits="presentacion.catalogo_documentos_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/angularjs/1.3.14/angular.min.js"></script>
    <script type="text/javascript">
        function ViewListEmpleados() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalSelectedEmployed').modal('show');
        }
        function ViewListEmpleadosSelected() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalSelectedObligatorios').modal('show');
        }
        function Return(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
    </script>
    <script type="text/javascript">
        function AlertGO(mensaje, URL) {
            swal({
                title: "Mensaje del sistema",
                text: mensaje,
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" CausesValidation="false" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Documentos</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Detalles del Documento <i class="fa fa-list-alt"></i></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-8 col-md-8 col-sm-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNombre" Rows="1" runat="server" placeholder="Nombre del Documento" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>

                                    <asp:AsyncPostBackTrigger ControlID="imgAddExtension" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="gridDocumentos" EventName="RowCommand" />
                                </Triggers>
                                <ContentTemplate>
                                    <h4><strong>Detalles</strong></h4>
                                    <h5>Puede indicar los tipos de archivos y extensiones para este documento. </h5>
                                    <h5>Seleccione una o mas extensiones(CADA EXTENSION QUE SE AGREGE, SERA UN DOCUMENTO MAS)</h5>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlTipo_Archivo" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlExtension" class="dropdown-toggle form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:ImageButton ID="imgAddExtension" ImageUrl="~/imagenes/btn/icon_agregar.png" OnClick="imgAddExtension_Click" runat="server" />
                                                Click para Agregar
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12">
                                            <div class="table table-responsive">
                                                <asp:GridView ID="gridDocumentos" runat="server" CssClass="table table-bordered table-hover table-condensed" PageSize="5" AutoGenerateColumns="False" OnRowCommand="gridDocumentos_RowCommand" DataKeyNames="descripcion,tipo_archivo">
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                                        <asp:BoundField DataField="tipo_archivo" HeaderText="Extension">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btnGuardarForm" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardarForm_Click" />
                </div>
                <div class="col-lg-4 col-md-6 col-sm-6  col-xs-6">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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
                                <div class="col-lg-6">
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                                </div>
                                <div class="col-lg-6">
                                    <asp:Button ID="No" class="btn btn-danger btn-block" runat="server" Text="No" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>