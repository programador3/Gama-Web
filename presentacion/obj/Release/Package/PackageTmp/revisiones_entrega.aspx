<%@ Page Title="Entregas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="revisiones_entrega.aspx.cs" Inherits="presentacion.revisiones_entrega" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="js/bootbox.min.js"></script>
    <script type="text/javascript">
        var downloadURL = function downloadURL(url) {
            var hiddenIFrameID = 'hiddenDownloader',
                iframe = document.getElementById(hiddenIFrameID);
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                iframe.style.display = 'none';
                document.body.appendChild(iframe);
            }
            iframe.src = url;
        };
        function GoSection(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 500);
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
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
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
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Entrega de Servicios </h1>
                </div>
            </div>
            <asp:Panel ID="Panel" runat="server" CssClass="form-group">
                <div class="row">
                    <div class="col-lg-2" style="align-content: center;">
                        <a>
                            <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 190px; margin: 0 auto;" />
                        </a>
                    </div>
                    <div class="col-lg-10" style="text-align: left">
                        <div class="form-group">
                            <h4>
                                <strong>Nombre Empleado: </strong>
                                <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                            </h4>
                            <h4><strong>Puesto: </strong>
                                <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                            </h4>
                            <h4><strong>Fecha de Solicitud de entrega: </strong>
                                <asp:Label ID="lblfecha" runat="server" Text=""></asp:Label>
                            </h4>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Preparar el siguiente listado: <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body" style="background-color: #E6E6E6">
                            <asp:Repeater ID="repeatServicios" runat="server" OnItemDataBound="repeatServicios_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblidc_revisionser" runat="server" Text='<%#Eval("idc_revisionser") %>' Visible="false"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                        <i class="fa fa-laptop"></i></span>
                                                    <asp:TextBox ID="txt1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion_servicio") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <h4>
                                                    <asp:Label ID="lblfecharev" runat="server" Text="Label" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbltpodetalle" runat="server" Text="Tipo" Visible="false"></asp:Label></h4>
                                                <asp:TextBox ID="txtDetalles" Visible="false" runat="server" Text="" CssClass="form-control" Style="resize: none;" ReadOnly="true" PlaceholdeR="Observaciones"></asp:TextBox>
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
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnAceptar" runat="server" Text="Revisar" CssClass="btn btn-primary btn-block" OnClick="btnAceptar_Click" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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