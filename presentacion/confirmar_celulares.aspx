<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="confirmar_celulares.aspx.cs" Inherits="presentacion.confirmar_celulares" %>

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
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Confirmar Entrega de Celulares </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4"></div>
                <div class="col-lg-8">
                    <h2 style="text-align: left;">
                        <asp:Label ID="lblSelectedCel" runat="server" Text="Detalles de Celulares y Lineas <i class='fa fa-list-alt'></i>"></asp:Label>
                    </h2>
                </div>
            </div>
            <asp:Panel ID="PanelDetallesCelular" runat="server">
                <asp:Repeater ID="repeatCelulares" runat="server" OnItemDataBound="repeatCelulares_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-lg-12" style="margin: 0 auto;">
                                <asp:Panel ID="PanelT1" runat="server">
                                    <div class="row">
                                        <div class="col-lg-2" style="align-content: center;">

                                            <a style="align-content: center;">
                                                <asp:Image ID="imgCel" runat="server" class="img-responsive" alt="Gama" Style="width: 150px; margin: 0 auto;" />
                                            </a>
                                        </div>
                                        <div class="col-lg-5">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <h4 style="text-align: center;"><strong>Detalles del Celular o Linea</strong></h4>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <h5><i class="fa fa-phone-square fa-fw"></i><strong>Linea: </strong>
                                                    <asp:Label ID="linea" runat="server" Text='<%#Eval("telefono") %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="form-group">
                                                <h5><i class="fa fa-phone fa-fw"></i><strong>Marcación Corta:</strong>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("mar_corta") %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="form-group">
                                                <h5><strong><i class="fa fa-mobile fa-fw"></i>Descripción del Equipo:</strong>
                                                    <asp:Label ID="descri" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="form-group">
                                                <h5><strong><i class="fa fa-usd fa-fw"></i>Costo del Equipo:  $</strong>
                                                    <asp:Label ID="costo" runat="server" Text='<%#Eval("costo") %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="form-group">
                                                <h5><strong><i class="fa fa-money"></i>Fecha de Adquisición: </strong>
                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("fecha_adquisicion") %>'></asp:Label>
                                                </h5>
                                            </div>
                                        </div>

                                        <div class="col-lg-5">
                                            <div class="form-group">
                                                <h4 style="text-align: center;"><strong>Accesorios del equipo</strong>
                                                </h4>
                                            </div>
                                            <asp:Panel ID="PanelconAccesorios" runat="server">
                                                <asp:Repeater ID="repeatAccesorios" runat="server">
                                                    <ItemTemplate>
                                                        <div class="form-group">
                                                            <h5><strong><i class="fa fa-info-circle fa-fw"></i>Descripcion: </strong>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                            </h5>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>
                                            <asp:Panel ID="PanelsinAccesorios" runat="server">
                                                <h3 style="text-align: center;">Esta linea no cuenta con Equipo Celular <i class="fa fa-exclamation-triangle"></i></h3>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="PanelRevisaCelulares" runat="server" CssClass="panel panel-primary">
                <div class="panel-heading" style="text-align: center">
                    <h3 class="panel-title">Listado de Celulares <i class="fa fa-list"></i></h3>
                </div>
                <div class="panel-body" style="background-color: #E6E6E6">
                    <div class="row">
                        <div class="col-lg-6 col-md-4 col-xs-12" style="text-align: center;">
                            <h4><strong>Celular <i class="fa fa-wrench"></i></strong></h4>
                        </div>
                        <div class="col-lg-3 col-md-4 col-xs-12" style="text-align: center;">
                            <h4><strong>Accesorio <i class="fa fa-wrench"></i></strong></h4>
                        </div>
                        <div class="col-lg-3 col-md-4 col-xs-12" style="text-align: center;">
                            <h4><strong>Confirmado </strong>
                            </h4>
                        </div>
                    </div>
                    <asp:Repeater ID="repeatEquipoCelular" runat="server">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtObservaciones" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cbx" EventName="CheckedChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblcelular1" runat="server" Text='<%#Eval("idc_entrega_cel") %>' Visible="false"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                        <i class="fa fa-mobile"></i></i></span>
                                                    <asp:TextBox ID="txt1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12"></div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                        <i class="fa fa-check-square-o"></i></span>
                                                    <asp:CheckBox ID="cbx" runat="server" class="input-sm" Text="Recibido" TextAlign="Right" />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="repeat_accesorios" runat="server">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtObservaciones1" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cbx1" EventName="CheckedChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblcelular" runat="server" Text='<%#Eval("idc_celular") %>' Visible="false"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                        <i class="fa fa-mobile"></i></span>
                                                    <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("celular") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblaccesorio" runat="server" Text='<%#Eval("idc_entrega_celacc") %>' Visible="false"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                        <i class="fa fa-mobile"></i></span>
                                                    <asp:TextBox ID="TextBox1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                        <i class="fa fa-check-square-o"></i></span>
                                                    <asp:CheckBox ID="cbx1" runat="server" class="input-sm" Text="Recibido" TextAlign="Right" />
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
                                                    <asp:TextBox ID="txtObservaciones1" runat="server" class="form-control" Text="" placeholder="Observaciones" TextMode="MultiLine" Rows="2" MaxLength="250" Style="resize: none;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
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