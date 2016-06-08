<%@ Page Title="Nueva Aprobación" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="aprobaciones_captura.aspx.cs" Inherits="presentacion.aprobaciones_captura" %>

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
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
    </script>
    <script type="text/javascript">
        function AlertOK(URL) {
            swal({
                title: "Mensaje del sistema",
                text: "Aprobación Guardada correctamente",
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
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>Aprobación </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Datos de Aprobación <i class="fa fa-list-alt"></i></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4>Nombre de Aprobación:
                                    </h4>
                                    <asp:TextBox ID="txtNombre" runat="server" Style="text-transform: uppercase; resize: none;" placeholder="Nombre de Nueva Aprobación" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h4>Comentarios:
                                    </h4>
                                    <asp:TextBox ID="txtComentarios" Style="text-transform: uppercase; resize: none;" TextMode="MultiLine" runat="server" placeholder="Comentarios" CssClass="form-control" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Detalles de Aprobación <i class="fa fa-list-alt"></i></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <h4>Minimo de firmas requeridas:
                                    </h4>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txtMinimoRequeridos" runat="server" class="form-control" TextMode="Number" Text="1" AutoPostBack="True" OnTextChanged="txtMinimoRequeridos_TextChanged" Width="55px"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 col-md-2 col-sm-12"></div>
                                <div class="col-lg-3 col-md-3 col-sm-12">
                                    <h4>
                                        <asp:Label ID="lblTipo_Cantidad" runat="server" Text="Faltan"></asp:Label>
                                        <asp:Label ID="lbl_Numero_Cantidad" runat="server" Text="Label"></asp:Label></h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h4><strong>Seleccione el personal que debe firmar esta aprobación:</strong></h4>
                                </div>
                            </div>
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
                                        <asp:LinkButton ID="btnEmpleado" runat="server" class="btn btn-primary" OnClick="btnEmpleado_Click">Seleccionar Personal <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                    </h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gridEmpleados" runat="server" CssClass="table table-bordered table-hover table-condensed" PageSize="5" AutoGenerateColumns="False" OnRowCommand="gridEmpleados_RowCommand" DataKeyNames="idc_puesto" OnRowDataBound="gridEmpleados_RowDataBound">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="idc_aprobacion_det" HeaderText="idc_aprobacion_det" Visible="False" />
                                                <asp:BoundField DataField="idc_puesto" HeaderText="Puesto-Empleado Seleccionado" Visible="False" />
                                                <asp:BoundField DataField="puesto" HeaderText="Puestos Seleccionados">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nuevo" HeaderText="nuevo" Visible="False" />
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
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:Button ID="btnGuardarForm" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardarForm_Click" />
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6  col-xs-12">
                    <div class="form-group">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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
                            <div class="col-lg-12">
                                <div style="width: 100%; height: 300px; overflow-y: auto">
                                    <asp:CheckBoxList ID="cblPuestos" runat="server" RepeatLayout="Flow">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:Button ID="btnAceptarEmpleado" class="btn btn-primary btn-block" runat="server" Text="Aceptar" OnClick="btnAceptarEmpleado_Click" />
                            </div>
                            <div class="col-lg-6 col-md-2 col-sm-6 col-xs-12">
                                <asp:Button ID="cancelar" class="btn btn-danger btn-block" runat="server" Text="Cancelar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<%--<%-- <%-- Modal para confirmar obligados--%>            <%--<div id="modalSelectedObligatorios" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                      <h3><asp:Label ID="lblTituloObligatorios" runat="server" Text="Seleccione los empleados que obligatoriamente deben firmar esta aprobación."></asp:Label>
                      </h3>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">

                                <asp:CheckBoxList ID="cblObligatorio" runat="server" CssClass="form-control" RepeatLayout="Flow">
                                </asp:CheckBoxList>
                            </div>
                            <div class="col-lg-2"></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                         <asp:Button ID="btnAcpetarObligatorio" class="btn btn-success" runat="server" Text="Aceptar" Onclick="btnAcpetarObligatorio_Click" />
                         <asp:Button ID="Button2" class="btn btn-danger" runat="server" Text="Cancelar" />
                    </div>
                </div>
            </div>
        </div>--%>
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
                            <div class="col-lg-6">
                                <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="btnConfirm_Click" />
                            </div>
                            <div class="col-lg-6">
                                <asp:Button ID="No" class="btn btn-danger btn-block" runat="server" Text="No" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>