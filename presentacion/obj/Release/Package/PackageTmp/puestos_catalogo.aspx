<%@ Page Title="Puestos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="puestos_catalogo.aspx.cs" Inherits="presentacion.puestos_catalogo" %>

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
        }
        function MoadlPrevivew() {
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
            $('#ModalPerfil').modal('hide');
            $('#myModalAcciones').modal('hide');
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
                <h1>
                    <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                    Puestos</h1>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">
                            <h3 class="panel-title">Listado de Puestos <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <div class="btn-group">
                                            <asp:LinkButton runat="server" class="btn btn-primary btn-block" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                           Guardar <i class="fa fa-floppy-o"></i>
                                            </asp:LinkButton>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">
                                                    <asp:LinkButton ID="lnkGuardarTodo" runat="server" OnClick="lnkGuardarTodo_Click">
                                                        <asp:Image ID="Image2" runat="server" Height="25" Width="25" ImageUrl="~/imagenes/btn/excel.png" />
                                                        Excel 2007
                                                    </asp:LinkButton></a></li>
                                                <li><a href="#">
                                                    <asp:LinkButton ID="lnkPDF" runat="server" OnClick="lnkPDF_Click">
                                                        <asp:Image ID="Image1" runat="server" Height="25" Width="25" ImageUrl="~/imagenes/btn/pdf.png" />
                                                        PDF
                                                    </asp:LinkButton></a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="table table-responsive" style="text-align: center;">
                                <asp:GridView ID="gridPuestos" runat="server" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [6]}" AutoGenerateColumns="False" DataKeyNames="idc_puesto_reemplazo,perfil_solicitud,idc_herramienta,idc_puesto,idc_puestoperfil,idc_statuso,idc_empleado, descripcion,idc_prepara,idc_puesto_jefe" OnRowDataBound="gridPuestos_RowDataBound" OnRowCommand="gridPuestos_RowCommand" Font-Size="Smaller">
                                    <Columns>

                                        <asp:ButtonField Visible="false" Text="Acciones" ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Pre-Baja" HeaderStyle-Width="40px" CommandName="Acciones" />
                                        <asp:BoundField DataField="idc_puesto" HeaderText="idc_puesto" Visible="False" />
                                        <asp:ButtonField DataTextField="descripcion" ControlStyle-CssClass="btn btn-default btn-block" HeaderText="Puesto" CommandName="Puesto" />
                                        <asp:BoundField DataField="idc_puestoperfil" HeaderText="idc_puestoperfil" Visible="False" />
                                        <asp:ButtonField DataTextField="perfil" HeaderText="Perfil" CommandName="Vista" />
                                        <asp:ButtonField Visible="false" Text="Asignar Perfil" ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Asignar Perfil" HeaderStyle-Width="60px" CommandName="Cambiar Perfil" />
                                        <asp:ButtonField Visible="false" HeaderText="Herramientas" Text="Herramientas" CommandName="Herramientas" HeaderStyle-Width="80px" />
                                        <asp:ButtonField Visible="false" Text="Servicios" ButtonType="Button" ControlStyle-CssClass="btn btn-success" HeaderStyle-Width="40px" HeaderText="Servicios" CommandName="Asignar Servicios" />
                                        <asp:BoundField DataField="idc_depto" HeaderText="idc_depto" Visible="False" />
                                        <asp:BoundField DataField="depto" HeaderText="Departamento" Visible="false" />
                                        <asp:BoundField DataField="idc_sucursal" HeaderText="idc_sucursal" Visible="False" />
                                        <asp:BoundField DataField="sucursal" HeaderText="Sucursal" Visible="False" />
                                        <asp:BoundField DataField="idc_statuso" HeaderText="idc_statuso" Visible="False" />
                                        <asp:ButtonField DataTextField="status" HeaderText="Status" CommandName="Status" HeaderStyle-Width="250px" />
                                        <asp:BoundField DataField="idc_empleado" HeaderText="idc_empleado" Visible="False" />
                                        <asp:BoundField DataField="idc_prepara" HeaderText="idc_empleado" Visible="False" />
                                        <asp:BoundField DataField="idc_herramienta" HeaderText="idc_empleado" Visible="False" />
                                        <asp:BoundField DataField="idc_puesto_reemplazo" HeaderText="idc_puesto_reemplazo" Visible="False" />
                                        <asp:BoundField DataField="idc_puesto_jefe" HeaderText="idc_puesto_reemplazo" Visible="False" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.CONFIRMA -->
    <div id="ModalPerfil" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="text-align: center">
                <div class="modal-header" style="background-color: #428bca; color: white">
                    <h4><strong class="modal-title">Asignar Perfil</strong></h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="modal_cboxperfiles" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-10">
                                    <div class="form-group">
                                        <asp:Label ID="modal_lblpuesto" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="modal_lblperfil" runat="server" Text="Asignar Perfil:"></asp:Label>
                                        <asp:DropDownList ID="modal_cboxperfiles" CssClass="form-control" runat="server" OnSelectedIndexChanged="modal_cboxperfiles_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-1"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-lg-6 col-xs-6">
                            <asp:Button ID="btnAsiP" class="btn btn-primary btn-block" runat="server" Text="Asignar" OnClick="btnAsiP_Click" />
                        </div>
                        <div class="col-lg-6 col-xs-6">
                            <input id="Nop" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                        </div>
                    </div>
                    <!--campos ocultos -->
                    <asp:HiddenField ID="oc_idc_puesto" runat="server" />
                </div>
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
                            <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="btnConfirm_Click" />
                        </div>
                        <div class="col-lg-6 col-xs-6">
                            <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalAcciones" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="text-align: center">
                <div class="modal-header" style="background-color: #428bca; color: white">
                    <h4><strong class="modal-title">Acciones</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalPreviewView" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                    <h3>
                        <asp:Label ID="lblMPuesto" runat="server" Text=""></asp:Label>
                    </h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-5 col-sm-5 col-xs-12 portfolio-item">
                            <a>
                                <img id="myImage" class="img-responsive" src="imagenes/btn/default_employed.png" alt="Gama System" style="width: 120px; height: 120PX; margin: 0 auto;" />
                            </a>
                        </div>
                        <div class="col-lg-9 col-md-7 col-sm-7 col-xs-12 ">
                            <h3><strong>
                                <asp:Label ID="lblMNombre" runat="server" Text=""></asp:Label></strong></h3>
                            <h4><strong>Fecha de Nacimiento: </strong>
                                <asp:Label runat="server" ID="lblMFechaNac"></asp:Label></h4>
                            <h4><strong>Fecha de Ingreso: </strong>
                                <asp:Label runat="server" ID="lblMFechaIngreso"></asp:Label></h4>
                            <h4><strong>Departamento: </strong>
                                <asp:Label runat="server" ID="lblMDepto"></asp:Label></h4>
                            <h4><strong>Sucursal: </strong>
                                <asp:Label runat="server" ID="lblMSucursal"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lnkMPerfil" runat="server" Visible="false" CssClass="btn btn-success btn-block" OnClick="lnkMPerfil_Click">Ver Perfil <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lnkMVerHerramientas" runat="server" Visible="false" CssClass="btn btn-success btn-block" OnClick="lnkMVerHerramientas_Click">Ver Herramientas <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lnkasignarperfil" runat="server" Visible="false" CssClass="btn btn-info btn-block" OnClick="lnkasignarperfil_Click">Asignar Perfil <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lnkprebaja" runat="server" Visible="false" CssClass="btn btn-danger btn-block" OnClick="lnkprebaja_Click">Solicitar Baja <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lnkreemplazo" runat="server" Visible="false" CssClass="btn btn-info btn-block" OnClick="lnkreemplazo_Click">Solicitar Reemplazo <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lnkpmd" runat="server" Visible="false" CssClass="btn btn-success btn-block" OnClick="lnkpmd_Click">PMD <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lnkservicios" runat="server" Visible="false" CssClass="btn btn-primary btn-block" OnClick="lnkservicios_Click">Asignar Servicios <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lnkservicios_medan" runat="server" Visible="false" CssClass="btn btn-info btn-block" OnClick="lnkservicios_medan_Click">Servicios Asignados <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lnkpermiso" runat="server" Visible="false" CssClass="btn btn-success btn-block" OnClick="LinkButton1_Click">Permiso Cambio de Horario <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>