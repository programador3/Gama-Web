<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos_historial.aspx.cs" Inherits="presentacion.cursos_historial" %>

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
                    <h1 class="page-header">Cursos Programados
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
                <div class="col-lg-2">
                    <asp:Button ID="btnprogramarcurso" runat="server" Text="Programar Curso" CssClass="btn btn-primary" OnClick="btnprogramarcurso_Click" Visible="False" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="table-responsive">
                        <div class="panel panel-primary ">
                            <div id="panel-head1" class="p-borrador" style="text-align: center">
                                <h3 class="panel-title">Historial <i class="fa fa-list"></i></h3>
                            </div>
                            <div class="panel panel-body">
                                <div class="table table-responsive">
                                    <asp:GridView ID="grid_cursos_historial" CssClass="gvv table table-bordered table-hover grid sortable {disableSortCols: [2]}" runat="server" AutoGenerateColumns="False" DataKeyNames="idc_curso_historial" OnRowDataBound="grid_cursos_historial_RowDataBound" OnRowCommand="grid_cursos_historial_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Aplicar" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgedit" runat="server" CausesValidation="false" CommandName="editregistro" ImageUrl="~/imagenes/btn/more.png" Text="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="idc_curso_historial" HeaderText="Idc_curso_historial" Visible="False" />
                                            <asp:BoundField DataField="curso" HeaderText="Curso">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Persona">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpersona" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Aprobado capacitación" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaprob_capacitacion" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aprobado Gerencia" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaprob_gerencia" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Estatus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblestatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="resultado" HeaderText="Resultado" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha" HeaderText="Fecha de Ingreso" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
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
                        <asp:Button ID="modal_btnaceptar" class="btn btn-success" runat="server" Text="Aceptar" />
                        <asp:Button ID="modal_btncancelar" class="btn btn-danger" runat="server" Text="Cancelar" />
                        <!--campos ocultos -->
                        <asp:HiddenField ID="oc_empleado" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>