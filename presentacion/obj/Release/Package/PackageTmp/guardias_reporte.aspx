<%@ Page Title="Reporte" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="guardias_reporte.aspx.cs" Inherits="presentacion.guardias_reporte" %>
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
    <h3 class=" page-header">Reporte de Guardias</h3>
    <div class="row">
        <div class="col-lg-12">
            <h5><strong>Seleccione una fecha para generar el reporte de guardias</strong></h5>
            <asp:TextBox ID="txtfecha" TextMode="Date" CssClass=" form-control2" Width="68%" runat="server"></asp:TextBox>
            <asp:LinkButton ID="lnkejecutarreporte" CssClass="btn btn-danger" Width="30%" runat="server" OnClick="lnkejecutarreporte_Click">Ejecutar</asp:LinkButton>
        </div>
        <div class="col-lg-12">
            <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="btn btn-success" OnClick="LinkButton1_Click">Descargar en Excel</asp:LinkButton>
            <div class="table table-responsive">
                <asp:GridView ID="grid_reporte" CssClass="gvv table table-responsive table-bordered table-condensed table-condensed" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="num_nomina" HeaderText="Num. Nomina"></asp:BoundField>
                        <asp:BoundField DataField="empleado" HeaderText="Empleado"></asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha"></asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
                        <asp:BoundField DataField="depto" HeaderText="Depto"></asp:BoundField>
                        <asp:BoundField DataField="sucursal" HeaderText="Sucursal"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
