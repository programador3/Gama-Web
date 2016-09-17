<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="revision_basica_de_herramientas_pendientes_m.aspx.cs" Inherits="presentacion.revision_basica_de_herramientas_pendientes_m" %>

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
    <h3 class="page-header">Revisiones Pendientes </h3>
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="table table-responsive">
                <asp:GridView Style="font-size: 11px" DataKeyNames="idc_revbasherr" ID="gridpendientes" CssClass="gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server" OnRowCommand="gridpendientes_RowCommand">
                    <Columns>
                        <asp:CommandField ButtonType="Button" SelectText="Revisar" ControlStyle-CssClass="btn btn-default btn-block" ControlStyle-Width="100%" ControlStyle-Height="35px"
                            ShowSelectButton="True" HeaderStyle-Width="20px" />
                        <asp:BoundField DataField="Num_economico" HeaderText="Num. Eco." HeaderStyle-Width="30px" />
                        <asp:BoundField DataField="Veh" HeaderText="Vehiculo" />
                        <asp:BoundField DataField="Num_nomina" HeaderText="Nomina" HeaderStyle-Width="40px" />
                        <asp:BoundField DataField="Empleado" HeaderText="Empleado" />
                        <asp:BoundField DataField="idc_sucursal" HeaderText="ID." HeaderStyle-Width="30px" />
                        <asp:BoundField DataField="Nomsuc" HeaderText="Sucursal" />
                        <asp:BoundField DataField="idc_revbasherr" Visible="False" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>