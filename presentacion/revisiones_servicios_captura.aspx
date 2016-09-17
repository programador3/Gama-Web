<%@ Page Title="Captura" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="revisiones_servicios_captura.aspx.cs" Inherits="presentacion.revisiones_servicios_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function getImage(path) {
            $("#myImage").attr("src", path);
            //alert(path);
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido) {
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
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
    <style type="text/css">
        .Bordeado {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <div class="page-header">
                <h1>
                    <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                    Captura de Revisiones</h1>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlPuestorevisa" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtFiltrorev" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTipoRev" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTipoaplicacion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="lnkGenraVales" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkFinal" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkVerTodos" />
                </Triggers>
                <ContentTemplate>
                    <div class="Bordeado">
                        <div class="row">
                            <div class="col-lg-12">
                                <h4><i class="fa fa-bars"></i>&nbsp;Descripción o Nombre de la Revisión
                                </h4>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDescripcion" Style="text-transform: uppercase" CssClass="form-control" placeholder="Descripcion" runat="server" onblur="return imposeMaxLength(this, 249);"></asp:TextBox>
                                    <asp:Label ID="lblDescripcion" CssClass="label label-danger" runat="server" Text="Escriba una descripcion" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <h4><i class="fa fa-check-square-o"></i>&nbsp;Seleccione el Puesto que Revisa <small>Este puesto estara encargado de revisar </small></h4>
                        <div class="row">
                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlPuestorevisa" OnSelectedIndexChanged="ddlPuestorevisa_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblRevisa" CssClass="label label-danger" runat="server" Text="Seleccione un Puesto" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtFiltrorev" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltrorev_TextChanged" placeholder="Buscar"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <h4><i class="fa fa-binoculars"></i>&nbsp;Seleccione un tipo de Revision</h4>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlTipoRev" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoRev_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblrrortiporev" CssClass="label label-danger" runat="server" Text="Seleccione un Tipo de Revision" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="Bordeado">
                        <asp:Panel ID="PanelTipos" runat="server">
                            <h4><i class="fa fa-home"></i>&nbsp;Seleccione los detalles<small></small></h4>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="list-group">
                                        <asp:LinkButton ID="lnkFinal" class="list-group-item col-lg-12" runat="server" OnClick="lnkFinal_Click">
                                            <h5 class="list-group-item-heading">
                                                <asp:CheckBox ID="cbxFinal" runat="server" Text="Es Final" />
                                                <small class="list-group-item-text">Indica si esta revision sera una validación final. </small>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkGenraVales" class="list-group-item col-lg-12" runat="server" OnClick="lnkGenraVales_Click">
                                            <h5 class="list-group-item-heading">
                                                <asp:CheckBox ID="cbxGenraVales" runat="server" Text="Genera Vales" />
                                                <small class="list-group-item-text">Indica si esta revision puede generar vales. </small>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PanelComboTipoApli" runat="server">
                            <h4><i class="fa fa-database"></i>&nbsp;Seleccione el modo de Revision <small>Puede ser a Todos los puestos, una Lista, o una Excepcion </small></h4>
                            <div class="row">
                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlTipoaplicacion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoaplicacion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="ddltipoaplica" CssClass="label label-danger" runat="server" Text="Seleccione un tipo de aplicación" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PanelTipoApli" runat="server">
                            <asp:Panel ID="PanelGrupos" runat="server">
                                <h4>Seleccione un Grupo Predefinido de Puestos
                             <small>Puede elegir un grupo de puesto como una lista o lista de excepción</small>
                                </h4>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblNOHayGrupos" CssClass="label label-danger" runat="server" Text="NO HAY GRUPOS ACTUALMENTE" Visible="true"></asp:Label>
                                            <div style="width: 100%; height: 200px; overflow-y: auto" id="PuestosGroup" runat="server">
                                                <div class="list-group">
                                                    <asp:Repeater ID="repeat_grupos" runat="server">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="cbxCheckGpo" EventName="CheckedChanged" />
                                                                    <asp:AsyncPostBackTrigger ControlID="lnkSelectedGpo" EventName="Click" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lnkSelectedGpo" class="list-group-item col-lg-12" runat="server" OnClick="lnkSelectedGpo_Click">
                                                                        <h5 class="list-group-item-heading">
                                                                            <asp:CheckBox ID="cbxCheckGpo" runat="server" Text='<%#Eval("descripcion") %>' AutoPostBack="true" OnCheckedChanged="cbxCheckGpo_CheckedChanged" />
                                                                            <asp:Label ID="lblValueGpo" runat="server" Text='<%#Eval("idc_puesto_gpo") %>' Visible="false"></asp:Label></h5>
                                                                        <small class="list-group-item-text">Puestos Relacionados: <%#Eval("numero_registros") %></small>
                                                                    </asp:LinkButton>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <h4>Seleccione los Puestos para crear una lista
                             <small>La lista puede ser para ser seleccionada o usarser como excepción</small>
                            </h4>
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-6 col-sx-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnSeleccionarTodos" runat="server" Text="Seleccionar Todos" CssClass="btn btn-warning btn-block" OnClick="btnSeleccionarTodos_Click" />
                                        <asp:Button ID="btnDeseleccionarTodos" runat="server" Text="Deseleccionar Todos" CssClass="btn btn-success btn-block" Visible="false" OnClick="btnDeseleccionarTodos_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" placeholder="Buscar"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkVerTodos" runat="server" CssClass="btn btn-info btn-block" OnClick="btnVerTodos_Click">Ver Lista de Puestos Seleccionados <i class="fa fa-list"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblnopuestos" CssClass="label label-danger" runat="server" Text="NO HUBO RESULTADO. INTENTE NUEVAMENTE" Visible="false"></asp:Label>
                                        <asp:Panel ID="PanelPuestos" runat="server">
                                            <div style="width: 100%; height: 225px; overflow-y: auto">
                                                <div class="list-group">
                                                    <asp:Repeater ID="repeat_puestos" runat="server">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="cbxCheck" EventName="CheckedChanged" />
                                                                    <asp:AsyncPostBackTrigger ControlID="lnkSelected" EventName="Click" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lnkSelected" class="list-group-item col-lg-12" runat="server" OnClick="lnkSelected_Click">
                                                                        <h5 class="list-group-item-heading">
                                                                            <asp:CheckBox ID="cbxCheck" Text='<%#Eval("descripcion") %>' runat="server" AutoPostBack="true" OnCheckedChanged="cbxCheck_CheckedChanged" />
                                                                            <asp:Label ID="lblValue" runat="server" Text='<%#Eval("idc_puesto") %>' Visible="false"></asp:Label></h5>
                                                                        <small class="list-group-item-text"><%#Eval("nombre") %> </small>
                                                                        <small class="list-group-item-text">|| <%#Eval("depto") %> </small>
                                                                        <small class="list-group-item-text">||  <%#Eval("sucursal") %> </small>
                                                                    </asp:LinkButton>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <h5>
                                            <asp:Label ID="lblContentRepeat" runat="server" Text="" CssClass="label label-danger"></asp:Label>
                                            &nbsp;PUESTOS VISUALIZADOS
                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PanelSucursal" runat="server">
                            <h4><i class="fa fa-home"></i>&nbsp;Seleccione las Sucursales<small> Sucursales a las que aplicaria la asigancion</small></h4>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" id="Sucur" runat="server" visible="false">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSucursales" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblsucursal" CssClass="label label-danger" runat="server" Text="Seleccione una Sucursal" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <asp:CheckBox ID="cbxSinSucursal" runat="server" Text="NO ASIGNAR SUCURSAL" AutoPostBack="true" OnCheckedChanged="cbxSinSucursal_CheckedChanged" Checked="true" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
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

            <div id="modalPreviewView" class="modal fade bs-example-modal-md" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                            <h4 style="text-align: center;"><strong>
                                <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4 style="text-align: center;">
                                        <strong>
                                            <asp:Label ID="lblNOhayPuestos" runat="server" Text="No hay puestos seleccionados"></asp:Label></strong></h4>
                                    <asp:Panel ID="PanelPuestosSelected" runat="server" Style="width: 100%; height: 250px; overflow-y: auto">
                                        <asp:BulletedList ID="bltPuestos" runat="server">
                                        </asp:BulletedList>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-12">
                                    <input id="btnModalAcept" type="button" class="btn btn-primary btn-block" value="Aceptar" onclick="ModalClose();" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.CONFIRMA -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>