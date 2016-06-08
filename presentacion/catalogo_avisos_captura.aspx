<%@ Page Title="Captura" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_avisos_captura.aspx.cs" Inherits="presentacion.catalogo_avisos_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/angularjs/1.3.14/angular.min.js"></script>
    <script type="text/javascript">
        function ViewListEmpleados() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalSelectedEmployed').modal('show');
        }
        function ViewListEmpleadosSelected() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalSelectedObligatorios').modal('show');
        }
        function Return(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
    </script>
    <script type="text/javascript">
        function AlertGO(mensaje, URL) {
            swal({
                title: "Mensaje del sistema",
                text: mensaje,
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" CausesValidation="false" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Avisos</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Detalles del Aviso <i class="fa fa-list-alt"></i></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNombre" Rows="1" runat="server" placeholder="Titulo del Aviso" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlDepartamento" EventName="SelectedIndexChanged" />
                                    <asp:PostBackTrigger ControlID="btnEmpleado" />
                                </Triggers>
                                <ContentTemplate>
                                    <h5>Seleccione el Personal que recibira este Aviso cuando sea solicitado</h5>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-6 col-sm-12 col-xs-12">
                                            <h4>Departamento
                                            </h4>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                                            <h4>
                                                <asp:DropDownList ID="ddlDepartamento" class="dropdown-toggle form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                                                </asp:DropDownList></h4>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                            <h4>
                                                <asp:LinkButton ID="btnEmpleado" runat="server" class="btn btn-primary" OnClick="btnEmpleado_Click">Seleccionar Puestos <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                            </h4>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-lg-12 col-md-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gridEmpleados" runat="server" CssClass="table table-bordered table-hover table-condensed" PageSize="5" AutoGenerateColumns="False" OnRowCommand="gridEmpleados_RowCommand" DataKeyNames="idc_puesto">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="idc_taviso" HeaderText="idc_aprobacion_det" Visible="False" />
                                                <asp:BoundField DataField="idc_puesto" HeaderText="Puesto-Empleado Seleccionado" Visible="False" />
                                                <asp:BoundField DataField="puesto" HeaderText="Puestos Seleccionados">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="borrado" HeaderText="borrado" Visible="False" />
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
                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btnGuardarForm" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardarForm_Click" />
                </div>
                <div class="col-lg-4 col-md-6 col-sm-6  col-xs-6">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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
                                <div class="col-lg-6">
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                                </div>
                                <div class="col-lg-6">
                                    <asp:Button ID="No" class="btn btn-danger btn-block" runat="server" Text="No" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalSelectedEmployed" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                            <h3>
                                <asp:Label ID="lblDptoSeeleccionado" runat="server" Text="Label"></asp:Label>
                            </h3>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <asp:CheckBoxList ID="cblPuestos" runat="server" RepeatLayout="Flow">
                                    </asp:CheckBoxList>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <asp:Button ID="btnAceptarEmpleado" class="btn btn-primary btn-block" runat="server" Text="Aceptar" OnClick="btnAceptarEmpleado_Click" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <asp:Button ID="cancelar" class="btn btn-danger btn-block" runat="server" Text="Cancelar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>