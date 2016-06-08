<%@ Page Title="Vehiculos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="vehiculos_entrega.aspx.cs" Inherits="presentacion.vehiculos_entrega" %>

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

        //$(document).ready(function () {
        //    $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
        //        "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
        //    });
        //});
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
                        Entrega de Vehiculos </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class="btn-group">

                        <asp:LinkButton ID="lnkDetalles" runat="server" class="btn btn-primary active" OnClick="lnkDetalles_Click">Detalles <i class="fa fa-list-alt"></i></asp:LinkButton>

                        <asp:LinkButton ID="lnkRevision" runat="server" class="btn btn-link" OnClick="lnkRevision_Click">Entregar <i class="fa fa-check-square-o"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="col-lg-8">
                    <h2 style="text-align: left;">
                        <asp:Label ID="lblSelectedCel" runat="server" Text="Detalles de Vehiculos <i class='fa fa-list-alt'></i>"></asp:Label>
                    </h2>
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
                            <asp:LinkButton ID="lnkDescargarFormato" CausesValidation="false" Visible="true" runat="server" OnClick="lnkDescargarFormato_Click" class="btn btn-info">Descargar Carta Responsiva <i class="fa fa-download"></i></asp:LinkButton>
                        </div>

                        <div class="form-group">
                        </div>
                    </div>
                </div>
            </asp:Panel>
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
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="lnkVerHVehiculos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkVerHVehiculos_Click">Ver Herramientas <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:LinkButton ID="lnkDescargarFormatoAce" CausesValidation="false" Visible="true" runat="server" OnClick="lnkDescargarFormatoAce_Click" class="btn btn-primary btn-block">Descargar Carta de Revisión <i class="fa fa-download"></i></asp:LinkButton>
                                                    </div>
                                                </div>
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
            <div class="row">
                <div class="col-lg-12">
                    <asp:Panel ID="PanelHerramientasVehiculo" class="panel panel-green form-group" runat="server" Visible="false">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Herramientas de Vehiculo <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="PanelsinHVehiculo" runat="server" Visible="false">
                                <h2 style="text-align: center;">Este Vehiculo no tiene Herramientas <i class="fa fa-exclamation-triangle"></i></h2>
                            </asp:Panel>
                            <asp:Panel ID="PanelconHVehiculo" runat="server">
                                <div class="table table-responsive form-group">
                                    <asp:GridView ID="gridHerramientasVehiculo" runat="server" AutoGenerateColumns="false" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [3]}">
                                        <Columns>
                                            <asp:BoundField DataField="idc_gpo_herramientasd" HeaderText="idc_gpo_herramientasd" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="idc_tipoherramienta" HeaderText="idc_tipoherramienta" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad"></asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Herramienta"></asp:BoundField>
                                            <asp:BoundField DataField="activo" HeaderText="activo" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="tipo" HeaderText="tipo" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="idc_vehiculo" HeaderText="idc_vehiculo" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="costo" HeaderText="Costo"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <asp:Panel ID="PanelRevision" runat="server" Visible="false" CssClass="panel panel-primary">
                <div class="panel-heading" style="text-align: center">
                    <h3 class="panel-title">Listado de Vehiculos y sus Herramientas <i class="fa fa-list"></i></h3>
                </div>
                <div class="panel-body" style="background-color: #E6E6E6">
                    <div class="row">
                        <div class="col-lg-6 col-md-4 col-xs-12" style="text-align: center;">
                            <h4><strong>Vehiculo <i class="fa fa-car"></i></strong></h4>
                        </div>
                        <div class="col-lg-3 col-md-4 col-xs-12" style="text-align: center;">
                            <h4><strong>Herramientas(Cantidad) <i class="fa fa-wrench"></i></strong></h4>
                        </div>
                        <div class="col-lg-3 col-md-4 col-xs-12" style="text-align: center;">
                            <h4><strong>Entregado </strong>
                            </h4>
                        </div>
                    </div>
                    <asp:Repeater ID="repearVehiculos_rev" runat="server" OnItemDataBound="repearVehiculos_rev_ItemDataBound">
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
                                                <asp:Label ID="lblidc_vehiculo" runat="server" Text='<%#Eval("idc_vehiculo") %>' Visible="false"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                        <i class="fa fa-car"></i></span>
                                                    <asp:TextBox ID="txt1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion_vehiculo") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                        <i class="fa fa-check-square-o"></i></span>
                                                    <asp:CheckBox ID="cbx" runat="server" class="input-sm" Text="Entregado" TextAlign="Right" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtObservaciones" runat="server" class="form-control" Text="" placeholder="Observaciones" TextMode="MultiLine" Rows="2" MaxLength="250" Style="resize: none;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="repeat_vehiculos_herramientas_rev" runat="server" OnItemDataBound="repeat_vehiculos_herramientas_rev_ItemDataBound">
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
                                                <asp:Label ID="lblcelular" runat="server" Text='<%#Eval("idc_vehiculo") %>' Visible="false"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                        <i class="fa fa-car"></i></span>
                                                    <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion_veh") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblaccesorio" runat="server" Text='<%#Eval("idc_tipoherramienta") %>' Visible="false"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                        <i class="fa fa-wrench"></i></span>
                                                    <asp:TextBox ID="TextBox1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                        <i class="fa fa-check-square-o"></i></span>
                                                    <asp:CheckBox ID="cbx1" runat="server" class="input-sm" Text="Entregado" TextAlign="Right" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtObservaciones1" runat="server" class="form-control" placeholder="Observaciones" TextMode="MultiLine" MaxLength="250" Rows="1" Style="resize: none;"></asp:TextBox>
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
            <div class="row" id="archi" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="panel panel-success fresh-color">
                        <div class="panel-heading" style="text-align: center;">
                            Anexos
                        </div>
                        <div class="panel-body">
                            <div class="row" id="s" runat="server">
                                <div class="col-lg-12">
                                    <h4>Seleccione el vehiculo al que relacionara los archivos</h4>
                                    <h4>
                                        <asp:DropDownList ID="ddlvehiculo" CssClass="form-control" runat="server"></asp:DropDownList></h4>
                                </div>
                            </div>
                            <div class="row" id="carta_respo" runat="server">
                                <div class="col-lg-12">
                                    <h4>Carta Responsiva</h4>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                                            <span class="input-group-addon" style="color: #fff; background-color: #5cb85c;">
                                                <asp:LinkButton ID="lnkGuardarPape" Style="color: #fff; background-color: #5cb85c;" OnClick="lnkGuardarPape_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="formato" runat="server">
                                <div class="col-lg-12">
                                    <h4>Formato Revision</h4>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:FileUpload ID="fupFormato" CssClass="form-control" runat="server" />
                                            <span class="input-group-addon" style="color: #fff; background-color: #5cb85c;">
                                                <asp:LinkButton ID="lnkFrmato" Style="color: #fff; background-color: #5cb85c;" OnClick="lnkFrmato_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="vehiculo,tipo, idc_vehiculo, ruta">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" Visible="false" CommandName="Editar" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar">
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
                                                <asp:BoundField DataField="idc_vehiculo" HeaderText="extension" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="vehiculo" HeaderText="Vehiculo"></asp:BoundField>
                                                <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="tipo" HeaderText="Tipo de Archivo"></asp:BoundField>
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
                        <asp:Button ID="btnGuardar" Visible="false" runat="server" Text="Guardar" class="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnCancelar" Visible="false" runat="server" Text="Cancelar" class="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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