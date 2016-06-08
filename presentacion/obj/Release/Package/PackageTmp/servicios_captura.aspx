<%@ Page Title="Captura de Servicios" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="servicios_captura.aspx.cs" Inherits="presentacion.servicios_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
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
            //alert(path);
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
            $('#ModalPerfil').modal('hide');
        }
        function ModalPerfil() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#ModalPerfil').modal('show');
        }
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
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
            <div class="page-header">
                <h1>Asignación de Puestos de Servicio</h1>
            </div>
            <div class="row">
                <div class="col-lg-2" style="align-content: center;">
                    <a>
                        <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 160px; margin: 0 auto;" />
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
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div class="form-group">
                        <asp:LinkButton ID="lnktodo" CssClass="btn btn-default btn-block" runat="server" OnClick="lnktodo_Click">
                           Este Puesto Dara Servicio a Todos los demas Puestos</asp:LinkButton>
                    </div>
                </div>
            </div>
            <asp:Panel ID="panel_puestos" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h4 style="text-align: center;">Agregar Puestos <small style="color: white;">
                                    <asp:Label ID="lbltext" runat="server" Text="Seleccione los puestos a los que se les dara Servicio"></asp:Label></small></h4>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-10 col-md-12 col-sm-12 col-xs-8">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtfiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtfiltro_TextChanged" runat="server" placeholder="Escriba el Nombre del Puesto o Empleado"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                                        <div class="form-group">
                                            <asp:Button ID="btnbuscar" runat="server" Text="Buscar" OnClick="btnbuscar_Click" CssClass="btn btn-info btn-block" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnsele" runat="server" Text="Seleccionar Todos" OnClick="btnsele_Click" CssClass="btn btn-default btn-block" />
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div style="width: 100%; height: 300px; overflow-y: auto">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="repeat_pues" runat="server">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="lnkpuesto" EventName="Click" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lnkpuesto" CssClass="btn btn-default btn-block" runat="server" CommandName='<%#Eval("idc_puesto") %>' OnClick="lnkpuesto_Click">
                                                    <h5><%#Eval("descripcion_puesto_completa") %></h5>
                                                                    </asp:LinkButton>
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
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h4 style="text-align: center;">Puestos Actuales</h4>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gridPuestos" runat="server" AutoGenerateColumns="False" CssClass="gvv table table-responsive table-bordered table-condensed" OnRowCommand="gridPuestos_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar" HeaderStyle-Width="50px" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="idc_puesto" HeaderText="id" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="descripcion_puesto_completa" HeaderText="Puesto || Empleado Actual"></asp:BoundField>
                                                    <asp:BoundField DataField="depto" HeaderText="Departamento"></asp:BoundField>
                                                </Columns>
                                                <Columns>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnGuardar" Visible="true" runat="server" Text="Guardar" class="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnCancelar" CausesValidation="false" Visible="true" runat="server" Text="Cancelar" class="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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