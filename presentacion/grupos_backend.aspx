<%@ Page Title="Catalgo Grupos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="grupos_backend.aspx.cs" Inherits="presentacion.grupos_backend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });

        });
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
    </script>
    <script type="text/javascript">
        function AlertOK(URL) {
            swal({
                title: "Mensaje del sistema",
                text: "Grupo Eliminado correctamente",
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
                    <h1 class="page-header">Grupos
                    </h1>
                </div>
            </div>
            <!-- /.row -->

            <div class="row">
                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:Button ID="btnaddgpo" runat="server" Text="Nuevo Grupo" CssClass="btn btn-success btn-block" OnClick="btnaddgpo_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                    <div class="table-responsive">
                        <asp:GridView ID="gridview_gruposbackend" runat="server" OnRowDataBound="gridview_gruposbackend_RowDataBound" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [3]}" AutoGenerateColumns="False" DataKeyNames="idc_perfilgpo,grupo" OnRowCommand="gridview_gruposbackend_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="editar" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Editar">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="orden" HeaderText="Orden" />

                                <asp:BoundField DataField="idc_perfilgpo" HeaderText="Id" Visible="False" />
                                <asp:BoundField DataField="grupo" HeaderText="Grupo" />
                                <asp:HyperLinkField DataTextField="total_etiquetas" DataNavigateUrlFields="idc_perfilgpo,grupo" DataNavigateUrlFormatString="etiquetas.aspx?id={0}&amp;grupo={1}" HeaderText="Etiquetas" />
                                <asp:TemplateField HeaderText="Libre">
                                    <ItemTemplate>
                                        <asp:Image ID="Libre" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="libre" HeaderText="Libre" />--%>
                                <asp:BoundField DataField="minimo_libre" HeaderText="MinimoLibre" />
                                <asp:BoundField DataField="maximo_libre" HeaderText="MaximoLibre" />
                                <asp:TemplateField HeaderText="Opciones">
                                    <ItemTemplate>
                                        <asp:Image ID="Opciones" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="opciones" HeaderText="Opcion" />--%>
                                <asp:BoundField DataField="minimo_opc" HeaderText="MinimoOpc" />
                                <asp:BoundField DataField="maximo_opc" HeaderText="MaximoOpc" />
                                <asp:TemplateField HeaderText="Publicar">
                                    <ItemTemplate>
                                        <asp:Image ID="Publicar" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--
                                 <asp:BoundField DataField="externo" HeaderText="Publicar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                        <%--                            <table class="table table-bordered table-hover table-striped">
                                <thead>
                                    <tr>
                                        <th>Grupo</th>

                                        <th>Etiquetas</th>
                                        <th>Ultima Actualización</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="success">
                                        <td>Experiencia</td>

                                        <td><a href="etiquetas.aspx">0</a></td>
                                        <td>2015-01-02</td>
                                    </tr>
                                    <tr class="success">
                                        <td>Requisitos</td>

                                        <td><a href="etiquetas.aspx">4</a></td>
                                        <td>2014-05-25</td>
                                    </tr>
                                    <tr class="success">
                                        <td>Conocimientos</td>

                                        <td>0</td>
                                        <td>2015-01-01</td>
                                    </tr>
                                    <tr class="success">
                                        <td>Habilidades</td>

                                        <td>0</td>
                                        <td>2015-02-05</td>
                                    </tr>
                                </tbody>
                            </table>--%>
                    </div>
                </div>
            </div>
            <!-- /.row -->

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
        </div>
        <!-- /.container-fluid -->
    </div>
    <!-- /#page-wrapper -->
</asp:Content>