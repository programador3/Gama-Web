<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="puestos.aspx.cs" Inherits="presentacion.puestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Return(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
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
                    <h1 class="page-header">Puestos
                    </h1>
                </div>
            </div>
            <!-- /.row -->

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-11">
                    <div class="table-responsive">
                        <div class="panel panel-primary ">
                            <div class="panel-heading">
                                Puestos
                            </div>
                            <div class="panel panel-body">
                                <div class="table table-responsive">
                                    <!-- GRIDVIEW DE CURSOS -->
                                    <asp:GridView ID="grid_puestos" runat="server" CssClass="gvv table table-bordered table-hover grid sortable {disableSortCols: [2]}" AutoGenerateColumns="False" DataKeyNames="idc_puesto" OnRowDataBound="grid_puestos_RowDataBound" OnRowCommand="grid_puestos_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Descripción">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnperfil" runat="server" CausesValidation="false" CommandName="" Text="Perfil" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
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
                            <div class="col-lg-1"></div>
                            <div class="col-lg-10">
                                <div class="form-group">
                                    <asp:Label ID="modal_lblpuesto" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="modal_lblperfil" runat="server" Text="Asignar Perfil:"></asp:Label>
                                    <asp:DropDownList ID="modal_cboxperfiles" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-1"></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Yes" class="btn btn-success" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                        <asp:Button ID="No" class="btn btn-danger" runat="server" Text="Cancelar" />
                        <!--campos ocultos -->
                        <asp:HiddenField ID="oc_idc_puesto" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>