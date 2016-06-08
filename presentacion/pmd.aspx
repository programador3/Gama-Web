<%@ Page Title="PMD" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="pmd.aspx.cs" Inherits="presentacion.pmd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalPreview() {
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
        <div class="
            ">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Proceso de Mejora de Desempeño
                    </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="margin: 0 auto;">
                    <h3><strong>Datos del Empleado Actual</strong></h3>
                    <asp:Panel ID="Panel" runat="server">
                        <div class="row">
                            <div class="col-lg-2" style="align-content: center;">
                                <a>
                                    <img id="myImage" runat="server" class="img-responsive" alt="Gama System" style="width: 100px; margin: 0 auto;" />
                                </a>
                            </div>
                            <div class="col-lg-10">
                                <div class="form-group">
                                    <h4><strong>Puesto:</strong>
                                        <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                                    </h4>
                                </div>
                                <div class="form-group">
                                    <h4><strong>Empleado:</strong>
                                        <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></h4>
                                </div>

                                <div class="form-group">
                                    <h4><strong>Departamento:</strong>
                                        <asp:Label ID="lblDepto" runat="server" Text=""></asp:Label>
                                        <strong>Sucursal:</strong>
                                        <asp:Label ID="lblucursal" runat="server" Text=""></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h4><strong></strong>
                        <asp:Label ID="lbltipo" runat="server" Text=""></asp:Label></h4>
                    <h4><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Situacion</h4>
                    <asp:TextBox ID="txtobservaciones" TextMode="MultiLine" Rows="5" onblur="imposeMaxLength(this,1000);" CssClass="form-control" placeholder="Situación" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-success fresh-color">
                        <div class="panel-heading" style="text-align: center;">
                            Anexos
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="form-control" placeholder="Descripcion del documento" onkeypress="return isNumber(event);"></asp:TextBox>
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
                                        <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="descripcion, nombre, ruta, extension">
                                            <Columns>
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
                <div class="col-lg-6 col-md-6 col-xs-6">
                    <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-block" OnClick="lnkguardar_Click" runat="server">Guardar</asp:LinkButton>
                </div>
                <div class="col-lg-6 col-md-6 col-xs-6">
                    <asp:LinkButton ID="lnkcancelar" CssClass="btn btn-danger btn-block" OnClick="lnkcancelar_Click" runat="server">Cancelar</asp:LinkButton>
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
</asp:Content>