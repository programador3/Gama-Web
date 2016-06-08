<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos_revisar_gerencia.aspx.cs" Inherits="presentacion.cursos_revisar_gerencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="js/bootbox.min.js"></script>
    <style>
        .p-borrador {
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
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
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">

            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Cursos por revisar
                    </h1>
                    <div class="btn-group">
                        <asp:Button ID="btnempleado" runat="server" Text="Empleados" OnClick="btnempleado_Click" />
                        <asp:Button ID="btnpreempleado" runat="server" Text="Pre Empleados" OnClick="btnpreempleado_Click" />
                    </div>
                </div>
            </div>
            <!-- /.row -->

            <br />
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="table-responsive">
                        <div class="panel panel-primary ">
                            <div id="panel-head1" class="p-borrador" style="text-align: center">
                                <h3 class="panel-title">Cursos por revisar <i class="fa fa-list"></i></h3>
                            </div>
                            <div class="panel panel-body">
                                <div class="table table-responsive">
                                    <asp:GridView ID="grid_cursos_revisar_gerencia" CssClass="gvv table table-bordered table-hover grid sortable {disableSortCols: [2]}" runat="server" AutoGenerateColumns="False" DataKeyNames="idc_pre_empleado" OnRowCommand="grid_cursos_revisar_gerencia_RowCommand" OnRowDataBound="grid_cursos_revisar_gerencia_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="idc_pre_empleado" HeaderText="Idc_pre_empleado" Visible="False">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:ButtonField ButtonType="Image" CommandName="ver_detalle" ImageUrl="~/imagenes/btn/icon_buscar.png" Text="Detalle" />
                                            <asp:TemplateField HeaderText="Candidato">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpersona" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="puesto" HeaderText="Puesto"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!--mensaje oculto -->
                                <asp:Panel ID="panel_mensaje" runat="server" Visible="False" CssClass="row">
                                    <div class="col-lg-12">
                                        <h2>No tiene pendientes</h2>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- /.row -->
        </div>

        <!--campos ocultos -->
        <asp:HiddenField ID="oc_empleado" runat="server" />
    </div>
</asp:Content>