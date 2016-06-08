<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="confirmar_vehiculos.aspx.cs" Inherits="presentacion.confirmar_vehiculos" %>

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
        }

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

        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
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
                        Confirmar Entrega de Vehiculos y Herramientas </h1>
                </div>
            </div>
            <asp:Panel ID="PanelVehiculos" runat="server">
                <asp:Repeater ID="RepeatVehiculos" runat="server" OnItemDataBound="RepeatVehiculos_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-lg-12" style="margin: 0 auto;">
                                <asp:Panel ID="Panel1" runat="server" CssClass="form-group">
                                    <div class="row">
                                        <div class="col-lg-4" style="align-content: center;">
                                            <a>
                                                <asp:Image ID="imgVehiculos" runat="server" class="img-responsive" alt="Gama" Style="width: 300px; margin: 0 auto;" />
                                            </a>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:Panel ID="PanelTemp" runat="server">
                                                <div class="form-group">
                                                    <h4 style="text-align: center;"><strong>Detalles del Vehiculo</strong></h4>
                                                </div>
                                                <div class="form-group">
                                                    <h4><strong><i class="fa fa-user fa-fw"></i>Puesto Asignado: </strong>
                                                        <asp:Label ID="lblPuestoTEMP" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                    </h4>
                                                </div>
                                                <div class="form-group">
                                                    <h4><strong><i class="fa fa-list-ol fa-fw"></i>Numero Economico: </strong>
                                                        <asp:Label ID="lblNumeroEc" runat="server" Text='<%#Eval("num_economico") %>'></asp:Label>
                                                    </h4>
                                                </div>
                                                <div class="form-group">
                                                    <h4><strong><i class="fa fa-car fa-fw"></i>Descripción del Vehiculo: </strong>
                                                        <asp:Label ID="lblDescripcionVehoculo" runat="server" Text='<%#Eval("descripcion_vehiculo") %>'></asp:Label>
                                                    </h4>
                                                </div>
                                                <div class="form-group">
                                                    <h4><strong><i class="fa fa-lastfm-square fa-fw"></i>Placas: </strong>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("placas") %>'></asp:Label>
                                                    </h4>
                                                </div>
                                                <br />
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-lg-2">
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <div class="panel panel-primary">
                <div class="panel-heading" style="text-align: center">
                    <h3 class="panel-title">Listado de Vehiculos y Herramientas <i class="fa fa-list"></i></h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-xs-12" style="text-align: center;">
                            <h4><strong>Descripcion <i class="fa fa-wrench"></i></strong></h4>
                        </div>
                        <div class="col-lg-6 col-md-6 col-xs-12" style="text-align: center;">
                            <h4><strong>Confirmado </strong>
                            </h4>
                        </div>
                    </div>
                    <asp:Repeater ID="repeat_confirm_vehiculos" runat="server">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers></Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblvehiculo" runat="server" Text='<%#Eval("idc_entrega_veh") %>' Visible="false"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                        <i class="fa fa-car"></i></i></span>
                                                    <asp:TextBox ID="txt1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion_vehiculo") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                        <i class="fa fa-check-square-o"></i></span>
                                                    <asp:CheckBox ID="cbx" runat="server" class="input-sm" Text="Confirmo de Recibido" TextAlign="Right" />
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
                    <asp:Repeater ID="repeat_herramientas" runat="server">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers></Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblherra" runat="server" Text='<%#Eval("idc_entrega_veh_herr") %>' Visible="false"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                        <i class="fa fa-car"></i></i></span>
                                                    <asp:TextBox ID="txtdescrherr" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                        <i class="fa fa-check-square-o"></i></span>
                                                    <asp:CheckBox ID="cbx1" runat="server" class="input-sm" Text="Confirmo de Recibido" TextAlign="Right" />
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