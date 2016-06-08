<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos_pendiente.aspx.cs" Inherits="presentacion.cursos_pendiente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--   DataTable--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[10, 25, -1], [10, 25, "All"]] //value:item pair
            });
        });
    </script>

    <script type="text/javascript">
        function Return(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Cursos pendientes por aprobar
                    </h1>
                </div>
            </div>
            <!-- /.row -->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">
                            <h3 class="panel-title">Listado de cursos por aprobar<i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <div class="table table-responsive">
                                <asp:GridView ID="grid_cursopendiente" CssClass="gvv table table-bordered table-condensed table-hover grid sortable {disableSortCols: [4]}" runat="server" DataKeyNames="idc_curso_borr" OnRowCommand="grid_cursopendiente_RowCommand" OnRowDataBound="grid_cursopendiente_RowDataBound" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" HeaderText="Comparar" ImageUrl="~/imagenes/btn/icon_comparar.png" Text="Comparar" CommandName="comparar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="Aprobación" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="btnaprobar" runat="server" CausesValidation="false" CommandName="aprobar" Text="Aprobación" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Regresar" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtn_regresar" runat="server" CausesValidation="false" CommandName="regresar" ImageUrl="~/imagenes/btn/icon_return_borr.png" Text="Regresar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancelar" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtncancelar" runat="server" CausesValidation="false" CommandName="cancelar" ImageUrl="~/imagenes/btn/icon_borrar.png" Text="" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idc_curso_borr" HeaderText="Idc_curso_borr" Visible="False" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="idc_curso" HeaderText="Idc_curso" Visible="False" />
                                        <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="idc_registro" HeaderText="Idc_registro" Visible="False">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="idc_aprobacion_soli" HeaderText="Idc_aprobacion_soli" Visible="False" />
                                    </Columns>
                                </asp:GridView>
                                <%--<asp:GridView ID="grid_perfilpendiente" runat="server" CssClass="gvv table table-bordered table-condensed table-hover grid sortable {disableSortCols: [4]}" AutoGenerateColumns="False" DataKeyNames="idc_puestoperfil_borr" OnRowCommand="grid_perfilpendiente_RowCommand" OnRowDataBound="grid_perfilpendiente_RowDataBound">
                                                           <Columns>
                                                               <asp:ButtonField ButtonType="Image" HeaderText="Comparar" ImageUrl="~/imagenes/btn/icon_comparar.png" Text="Comparar" CommandName="comparar">
                                                               <HeaderStyle HorizontalAlign="Center" />
                                                               <ItemStyle HorizontalAlign="Center" />
                                                               </asp:ButtonField>
                                                               <asp:TemplateField HeaderText="Aprobación" ShowHeader="False">
                                                                   <ItemTemplate>
                                                                       <asp:Button ID="btnaprobar" runat="server" CausesValidation="false" CommandName="aprobar" Text="Aprobación" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                   </ItemTemplate>
                                                               </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Regresar" ShowHeader="False">
                                                                   <ItemTemplate>
                                                                       <asp:ImageButton ID="imgbtn_regresar" runat="server" CausesValidation="false" CommandName="regresar" ImageUrl="~/imagenes/btn/icon_return_borr.png" Text="Regresar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                   </ItemTemplate>
                                                                   <HeaderStyle HorizontalAlign="Center" />
                                                                   <ItemStyle HorizontalAlign="Center" />
                                                               </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Cancelar" ShowHeader="False">
                                                                   <ItemTemplate>
                                                                       <asp:ImageButton ID="imgbtncancelar" runat="server" CausesValidation="false" CommandName="cancelar" ImageUrl="~/imagenes/btn/icon_borrar.png" Text="" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                   </ItemTemplate>
                                                                   <HeaderStyle HorizontalAlign="Center" />
                                                                   <ItemStyle HorizontalAlign="Center" />
                                                               </asp:TemplateField>
                                                               <asp:BoundField DataField="descripcion" HeaderText="Perfil">
                                                               <HeaderStyle HorizontalAlign="Center" />
                                                               <ItemStyle HorizontalAlign="Center" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="puesto" HeaderText="Solicitante">
                                                               <HeaderStyle HorizontalAlign="Center" />
                                                               <ItemStyle HorizontalAlign="Center" />
                                                               </asp:BoundField>
                                                           </Columns>
                                                       </asp:GridView>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- /.row -->

            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
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
                            <!-- comentarios cuando vaya a cancelar -->
                            <asp:Panel ID="panel_comentarios" runat="server" class="row" Visible="false">

                                <div class="col-lg-12">
                                    <label>Comentarios</label>
                                    <asp:TextBox ID="txtcoments" runat="server" CssClass="form-control" TextMode="MultiLine" onblur="campoVacio();"></asp:TextBox>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <div class="form-group">

                                        <asp:Button ID="Yes" class="btn btn-success" runat="server" Text="Si" OnClick="btnConfirm_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <div class="form-group">

                                        <asp:Button ID="No" class="btn btn-danger" runat="server" Text="No" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- modal numero 2 -->
            <div id="Div1" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong id="confirmTitulo_2" class="modal-title"></strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="Label1"></label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%-- <asp:Button ID="Button1" class="btn btn-success" runat="server" Text="Si" OnClick="btnConfirm_Click" />
                              <asp:Button ID="Button2" class="btn btn-danger" runat="server" Text="No" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </div>
    <!-- wrapper -->
</asp:Content>