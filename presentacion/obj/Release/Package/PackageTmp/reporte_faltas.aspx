<%@ Page Title="Reporte Faltas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_faltas.aspx.cs" Inherits="presentacion.reporte_faltas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Reporte de Faltas por empleado<span> <small></small></span>
                    </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-5 col-md-5 col-sm-4 col-xs-6">
                    <h4><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicio
                       <asp:TextBox ID="txtfechainicio" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox></h4>
                </div>
                <div class="col-lg-5 col-md-5 col-sm-4 col-xs-6">
                    <h4><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Fin
                       <asp:TextBox ID="txtfechafin" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox></h4>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12">
                    <h4>&nbsp;.
                       <asp:LinkButton ID="lnkfiltro" OnClick="lnkfiltro_Click" CssClass="btn btn-success btn-block" runat="server">Filtrar</asp:LinkButton></h4>
                </div>
            </div>
            <div class="row" id="row_princ" runat="server" visible="false">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:LinkButton Visible="true" ID="lnkexcel" CssClass="btn btn-success btn-block" runat="server" OnClick="lnkexcel_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i> &nbsp;Exportar a Excel</asp:LinkButton>
                </div>
                <div class="col-lg-12">
                    <h4><i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;Listado Global de Faltas</h4>
                    <div class="table table-responsive" style="text-align: center;">
                        <asp:GridView ID="grid_faltas" DataKeyNames="idc_empleado" CssClass="gvv table table-responsive table-bordered" runat="server" OnRowDataBound="grid_faltas_RowDataBound" AutoGenerateColumns="False" OnRowCommand="grid_faltas_RowCommand">
                            <Columns>
                                <asp:BoundField Visible="false" DataField="idc_empleado" HeaderText="idc_empleado"></asp:BoundField>
                                <asp:ButtonField Text="Ver Detalles" ControlStyle-CssClass="btn btn-info btn-block" HeaderText="" CommandName="Ver" HeaderStyle-Width="80px" />
                                <asp:BoundField DataField="empleado" HeaderText="Empleado"></asp:BoundField>
                                <asp:BoundField DataField="puesto" HeaderText="Puesto"></asp:BoundField>
                                <asp:BoundField DataField="depto" HeaderText="Departamento"></asp:BoundField>
                                <asp:BoundField DataField="total_faltas" HeaderText="Faltas"></asp:BoundField>
                                <asp:TemplateField HeaderText="Activo" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Image ID="activo" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row" id="row_det" runat="server" visible="false">
                <div class="col-lg-12">
                    <h4><i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;Detalles </h4>
                    <div class="table table-responsive" style="text-align: center;">
                        <asp:GridView ID="grid_detalles" DataKeyNames="justificante" CssClass="table table-responsive table-bordered" runat="server" OnRowDataBound="grid_detalles_RowDataBound" AutoGenerateColumns="False" OnRowCommand="grid_detalles_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha de la Falta" HeaderStyle-Width="150px"></asp:BoundField>
                                <asp:BoundField DataField="asistencia" HeaderText="Asistencia" HeaderStyle-Width="150px"></asp:BoundField>
                                <asp:BoundField DataField="justificacion" HeaderText="Justificación" HeaderStyle-Width="150px"></asp:BoundField>
                                <asp:BoundField DataField="Reviso" HeaderText="Reviso"></asp:BoundField>
                                <asp:BoundField DataField="justificante" HeaderText="Faltas" Visible="false"></asp:BoundField>
                                <asp:ButtonField Text="Ver Justificante" ControlStyle-CssClass="btn btn-info btn-block" HeaderText="Anexo" CommandName="Ver" HeaderStyle-Width="80px" />
                                <asp:BoundField Visible="true" DataField="observaciones" HeaderText="Observaciones"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>