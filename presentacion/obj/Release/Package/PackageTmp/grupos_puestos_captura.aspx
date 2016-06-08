<%@ Page Title="Captura Grupos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="grupos_puestos_captura.aspx.cs" Inherits="presentacion.grupos_puestos_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
       <%-- function StartTable() {
            $(document).ready(function () {
                $("[id='<%= gridAvisos.ClientID%>']").DataTable();
            });
        }--%>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <br />
            <div class="page-header">
                <h1>
                    <asp:LinkButton ID="lnkReturn" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                    Captura de Grupos</h1>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSeleccionarTodos" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDeseleccionarTodos" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkVerTodos" />
                </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:TextBox ID="txtNombre" onkeypress="return isNumber(event);" Style="text-transform: uppercase" MaxLength="250" runat="server" placeholder="Nombre del Grupo" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblErrorNombre" runat="server" Text="Debe escribir el Nombre del Grupo" CssClass="label label-danger" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <h4>Seleccione los Puestos que desea que pertenezcan a este grupo.</h4>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-sx-12">
                            <div class="form-group">
                                <asp:Button ID="btnSeleccionarTodos" runat="server" Text="Seleccionar Todos" CssClass="btn btn-warning btn-block" OnClick="btnSeleccionarTodos_Click" />
                                <asp:Button ID="btnDeseleccionarTodos" runat="server" Text="Deseleccionar Todos" CssClass="btn btn-success btn-block" Visible="false" OnClick="btnDeseleccionarTodos_Click" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <asp:TextBox ID="txtFiltro" onkeypress="return isNumber(event);" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" placeholder="Buscar"></asp:TextBox>
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
                                <%-- <h5>
                                    <asp:Label ID="lblSeleccionados" runat="server" Text="0" CssClass="label label-danger"></asp:Label>
                                    &nbsp;PUESTOS SELECIONADOS
                                </h5>--%>
                                <div style="width: 100%; height: 300px; overflow-y: auto" id="ContentDivPuestos" runat="server">

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
                                <h5>
                                    <asp:Label ID="lblContentRepeat" runat="server" Text="" CssClass="label label-danger"></asp:Label>
                                    &nbsp;PUESTOS VISUALIZADOS
                                </h5>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

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
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
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