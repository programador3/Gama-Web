<%@ Page Title="Tareas Automaticas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_automaticas.aspx.cs" Inherits="presentacion.tareas_automaticas" %>

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
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
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
    <h1 class="page-header">Catalogo de Tareas Automaticas</h1>
    <div class="row">
        <div class="col-lg-4">
            <div class="form-group">
                <asp:LinkButton ID="lnkGO" runat="server" CssClass="btn btn-info btn-block" PostBackUrl="~/tareas_automaticas_captura.aspx">Agregar Nueva Tarea <i class="fa fa-plus-circle"></i></asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridAsignacion" runat="server" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [2]}" AutoGenerateColumns="False" DataKeyNames="idc_tarea_auto,descripcion" OnRowCommand="gridAsignacion_RowCommand">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Eliminar">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:ButtonField>

                        <asp:BoundField DataField="idc_tarea_auto" HeaderText="idc_cuestionario" Visible="false" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion de la Tarea" />
                        <asp:BoundField DataField="frecuencia" HeaderText="Tipo de Tarea" />
                        <asp:BoundField DataField="empleado_realiza" HeaderText="Puesto que realiza" />
                        <asp:BoundField DataField="empleado_revisa" HeaderText="Puesto que revisa" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="modal_title"><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4>
                                <label id="content_modal"></label>
                            </h4>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
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
</asp:Content>