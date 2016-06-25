<%@ Page Title="Preparacion Herramientas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="herramientas_preparacion.aspx.cs" Inherits="presentacion.herramientas_preparacion" %>

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
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Preparación de Herramientas Pendientes</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="text-align: left">
                    <div class="form-group">
                        <h3><strong>Fecha de Solicitud: </strong>
                            <asp:Label ID="lblfechasoli" runat="server" Text=""></asp:Label>
                        </h3>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="text-align: left">
                    <div class="form-group">
                        <h3><strong>Preparar Herramientas y Activos para: </strong>
                            <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                        </h3>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PanelRevisionActivos" runat="server" class="panel panel-primary">
                <div class="panel-heading" style="text-align: center;">
                    <h3 class="panel-title">Revision de Herramientas <i class="fa fa-check-square-o"></i></h3>
                </div>
                <div class="panel-body" style="background-color: #E6E6E6">

                    <asp:Panel ID="Panel2" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel44" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbkseltodo" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkDes" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-lg-6 col-xs-12" style="text-align: center;">
                                    </div>
                                    <div class="col-lg-3 col-xs-12"></div>
                                    <div class="col-lg-3 col-xs-12" style="text-align: center;">
                                        <h5>
                                            <asp:LinkButton ID="lbkseltodo" OnClick="lbkseltodo_Click" runat="server" CssClass="btn btn-primary btn-xs">Seleccionar Todo  <i class="fa fa-check-square-o"></i></asp:LinkButton>

                                            <asp:LinkButton ID="lnkDes" OnClick="lbkdestodo_Click" runat="server" CssClass="btn btn-primary btn-xs" Visible="false">Deseleccionar Todo  <i class="fa fa-check-square-o"></i></asp:LinkButton>
                                        </h5>
                                    </div>
                                </div>

                                <asp:Repeater ID="repeatRevision" runat="server" OnItemDataBound="repeatRevision_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbx" EventName="CheckedChanged" />
                                            </Triggers>
                                            <ContentTemplate>

                                                <div class="row">
                                                    <div class="col-lg-6 col-xs-12" style="text-align: center;">
                                                        <h4><strong>Herramienta <i class="fa fa-wrench"></i></strong></h4>
                                                    </div>
                                                    <div class="col-lg-3 col-xs-12"></div>
                                                    <div class="col-lg-3 col-xs-12" style="text-align: center;">
                                                        <h4><strong>Preparado </strong>
                                                        </h4>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblactivo" runat="server" Text="Label" Visible="false"></asp:Label>
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                    <asp:LinkButton ID="lnkDetallesHerrRevision" Style="color: #fff; background-color: #337ab7;" runat="server" OnClick="lnkDetallesHerrRevision_Click"><i class="fa fa-wrench"></i></asp:LinkButton></span>
                                                                <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("subcat") %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12"></div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                                    <i class="fa fa-check-square-o"></i></span>
                                                                <asp:CheckBox ID="cbx" runat="server" class="input-sm" Text="Esta Listo" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbx_CheckedChanged" />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
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
            <div id="modalPreviewView" class="modal fade bs-example-modal-md" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                            <h4>Detalles de Herramienta/Activo con Folio:
                          <asp:Label ID="lblMDetalles" runat="server" Text=""></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <h4 style="text-align: center;"><strong>
                                        <asp:Label ID="lblMSubcat" runat="server" Text=""></asp:Label></strong></h4>
                                    <asp:Repeater ID="gridHerramientasDetalles" runat="server">
                                        <ItemTemplate>
                                            <asp:Panel ID="Panel" runat="server">
                                                <h5><i class="fa fa-caret-right"></i><strong>
                                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label></strong>
                                                    &nbsp;<asp:Label ID="lblValor" runat="server" Text='<%#Eval("valor") %>'></asp:Label>
                                                    &nbsp;<asp:Label ID="lblObs" runat="server" Text='<%#Eval("observaciones") %>'></asp:Label>
                                                </h5>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-5">
                                </div>
                                <div class="col-lg-3">
                                    <input id="btnModalAcept" class="btn btn-primary btn-block" value="Aceptar" onclick="ModalClose();" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.CONFIRMA -->
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