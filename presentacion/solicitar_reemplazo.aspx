<%@ Page Title="Solicitud de Reemplazo" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="solicitar_reemplazo.aspx.cs" Inherits="presentacion.solicitar_reemplazo" %>

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
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Solicitud de Reemplazo de Puesto
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
                <div class="col-lg-12" id="fecha" runat="server" visible="false">
                    <h4><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;<strong>Fecha de Solicitud:</strong>
                        <asp:Label ID="lblfecha" runat="server" Text=""></asp:Label></h4>
                </div>
                <div class="col-lg-12">
                    <h4>Motivo de la Solicitud</h4>
                    <asp:TextBox ID="txtobservaciones" TextMode="MultiLine" Rows="5" onblur="imposeMaxLength(this,1000);" CssClass="form-control" placeholder="Motivo" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row" id="solicitud" runat="server">
                <div class="col-lg-6 col-md-6 col-xs-6">
                    <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-block" OnClick="lnkguardar_Click" runat="server">Guardar</asp:LinkButton>
                </div>
                <div class="col-lg-6 col-md-6 col-xs-6">
                    <asp:LinkButton ID="lnkcancelar" CssClass="btn btn-danger btn-block" OnClick="lnkcancelar_Click" runat="server">Cancelar</asp:LinkButton>
                </div>
            </div>

            <div class="row" id="cancelacion" runat="server" visible="false">
                <div class="col-lg-12">
                    <asp:LinkButton ID="lnkcancelarproceso" CssClass="btn btn-danger btn-block" OnClick="lnkcancelarproceso_Click" runat="server">Cancelar Solicitud</asp:LinkButton>
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
                        <div class="row" id="cancelaciontxt" runat="server" visible="false">
                            <div class="col-lg-12">
                                <h4>Motivo de la Cancelacion</h4>
                                <asp:TextBox ID="txtcancel" TextMode="MultiLine" Rows="5" onblur="imposeMaxLength(this,250);" CssClass="form-control" placeholder="Motivo" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-lg-6 col-xs-6">
                                <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>