<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="confirmar_herramientas.aspx.cs" Inherits="presentacion.confirmar_herramientas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
            var audio = new Audio('sounds/modal.wav');
            audio.play();
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
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Confirmación de Entrega de Herramientas </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Listado de Herramientas <i class="fa fa-check-square-o"></i></h3>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UPREPEAT" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-6 col-xs-12" style="text-align: center;">
                                        </div>
                                        <div class="col-lg-3 col-xs-12"></div>
                                        <div class="col-lg-3 col-xs-12" style="text-align: center;">
                                            <h5>
                                                <asp:LinkButton ID="lbkseltodo" OnClick="lbkseltodo_Click" runat="server" CssClass="btn btn-primary btn-xs">Seleccionar Todo  <i class="fa fa-check-square-o"></i></asp:LinkButton>

                                                <asp:LinkButton ID="lnkDes" OnClick="lnkDes_Click" runat="server" CssClass="btn btn-primary btn-xs" Visible="false">Deseleccionar Todo  <i class="fa fa-check-square-o"></i></asp:LinkButton>
                                            </h5>
                                        </div>
                                    </div>
                                    <asp:Repeater ID="repeat_listado" runat="server">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtObservaciones" EventName="TextChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblactivo" runat="server" Text='<%#Eval("idc_entrega_eq") %>' Visible="false"></asp:Label>
                                                                <div class="input-group">
                                                                    <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                        <i class="fa fa-wrench"></i></span>
                                                                    <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="form-group">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                                        <i class="fa fa-check-square-o"></i></span>
                                                                    <asp:CheckBox ID="cbx" runat="server" class="input-sm" Text="Confirmo de Recibido" TextAlign="Right" AutoPostBack="true" />
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
                                                                    <asp:TextBox ID="txtObservaciones" AutoPostBack="true" OnTextChanged="txtObservaciones_TextChanged" runat="server" class="form-control" Text="" placeholder="Observaciones" TextMode="MultiLine" Rows="2" MaxLength="250" Style="resize: none;"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="lblError" runat="server" Text="" CssClass="label label-danger" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
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
                                <div class="ccol-lg-6 col-xs-6">
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