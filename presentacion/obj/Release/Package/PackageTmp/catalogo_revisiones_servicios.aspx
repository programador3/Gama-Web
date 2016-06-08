<%@ Page Title="Servicios" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_revisiones_servicios.aspx.cs" Inherits="presentacion.catalogo_revisiones_servicios" %>

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
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
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
            <div class="page-header">
                <h1>
                    <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                    Revisiones Servicios</h1>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-12">
                    <div class="form-group">

                        <asp:LinkButton ID="lnkNuevo" runat="server" CssClass="btn btn-info btn-block" PostBackUrl="~/revisiones_servicios_captura.aspx">Agregar Nueva Revisión </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">
                            <h3 class="panel-title">Listado de Servicios <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <h3 id="NOHay" runat="server" style="text-align: center;" visible="false">NO HAY GRUPOS DE PUESTOS ACTIVOS <i class="fa fa-exclamation-triangle"></i></h3>
                            <div class="table table-responsive">
                                <asp:GridView ID="gridAvisos" runat="server" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [3]}" AutoGenerateColumns="False" DataKeyNames="idc_revisionser,descripcion" OnRowCommand="gridAvisos_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Eliminar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="idc_revisionser" HeaderText="idc_revisionser" Visible="false" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Grupo" />
                                        <asp:BoundField DataField="puesto" HeaderText="Puesto Asignado" />
                                        <asp:BoundField DataField="depto" HeaderText="Departamento" />
                                        <asp:BoundField DataField="sucursal" HeaderText="Sucursal" />
                                    </Columns>
                                </asp:GridView>
                            </div>
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