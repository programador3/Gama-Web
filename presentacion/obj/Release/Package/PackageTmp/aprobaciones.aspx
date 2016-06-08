<%@ Page Title="Aprobaciones" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="aprobaciones.aspx.cs" Inherits="presentacion.aprobaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Return(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ViewPre() {
            $('#modalPreviewView').modal('show');
        }
        $(window).resize(function () {
            var href = $(location).attr('href');
            if ($(window).width() < 800) {
                window.location.replace(href);
            } else {
                window.location.replace(href);
            }
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">

            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Aprobaciones </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <asp:LinkButton ID="btnNew" runat="server" CssClass="btn btn-success btn-block" OnClick="btnNew_Click">Crear Nueva Aprobación <i class="fa fa-plus"></i></asp:LinkButton>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Listado de Aprobaciones <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <h1 style="text-align: center;">
                                    <asp:Label ID="lblTablaVacia" runat="server" Text="No hay Aprobaciones<i class='fa fa-file-o'></i>" Visible="false"></asp:Label>
                                </h1>
                                <asp:GridView ID="gridaprobaciones" runat="server" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [4]}" AutoGenerateColumns="False" DataKeyNames="idc_aprobacion" OnRowCommand="gridaprobaciones_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/more.png" HeaderText="Ver" CommandName="Ver">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="idc_aprobacion" HeaderText="idc_aprobacion" Visible="False" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="comentarios" HeaderText="Comentarios" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
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
                            <div class="col-lg-6">
                                <asp:Button ID="Yes" class="btn btn-success btn-block " runat="server" Text="Si" OnClick="btnConfirm_Click" />
                            </div>
                            <div class="col-lg-6">
                                <asp:Button ID="No" class="btn btn-danger btn-block" runat="server" Text="No" />
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
                        <%--<h4>  Vista Previa</h4>--%> Preview
                        <i class="fa fa-file-powerpoint-o"></i>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <h4><strong>Aprobación: </strong>
                                    <asp:Label ID="lblTituloAprobacion" runat="server" Text=""></asp:Label></h4>
                                <br />
                                <asp:TextBox ID="txtComentarios" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="true" Style="resize: none;" Rows="5"></asp:TextBox>
                                <br />
                                <h4><strong>Firmas Requeridas:</strong> </h4>
                                <div style="width: 100%; height: 200px; overflow-y: auto">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:Repeater ID="listaPuestos" runat="server">
                                            <ItemTemplate>
                                                <h5><i class="fa fa-check-circle-o"></i>
                                                    <asp:Label ID="lblPuestos_rep" runat="server" Text='<%#Eval("puesto") %>'></asp:Label></h5>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </asp:Panel>
                                </div>

                                <asp:BulletedList ID="listPuestos" runat="server"></asp:BulletedList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-lg-12">
                                <button id="ok" class="btn btn-primary btn-block">Aceptar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>