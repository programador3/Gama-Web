<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_visitas.aspx.cs" Inherits="presentacion.reporte_visitas" %>

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
    <h1 class="page-header">Reportes de rendimiento por Puesto</h1>
    <div class="row">
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <h4><strong>Selecciona el empleado</strong><small> Deje en blanco para ver todo.</small></h4>
            <asp:DropDownList ID="ddlPuestoAsigna" OnSelectedIndexChanged="ddlPuestoAsigna_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-8 col-xs-12">
            <h4>Escriba un Filtro</h4>
            <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12">
            <h4>.</h4>
            <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h4><i class="fa fa-calendar"></i>&nbsp;<strong>Seleccione una fecha de inicio</strong></h4>
            <asp:TextBox ID="txtfechainicio" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h4><i class="fa fa-calendar"></i>&nbsp;<strong>Seleccione una fecha de fin</strong></h4>
            <asp:TextBox ID="txtfechafin" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-12 col-xs-12">
            <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" CssClass="btn btn-info btn-block" runat="server">Ver Reporte <i class="fa fa-repeat" aria-hidden="true"></i></asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-indent" aria-hidden="true"></i>&nbsp;Visitas
                        <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label></strong><span>

                            <asp:LinkButton Visible="true" ID="lnkurladicinal" CssClass="btn btn-success" OnClick="lnkurladicinal_Click" runat="server">Exportar a Excel <i class="fa fa-share" aria-hidden="true"></i></asp:LinkButton>
                        </span></h4>
            <div class="table-responsive">
                <asp:GridView ID="gridvisitas" CssClass="table table-responsive table-condensed gvv" DataKeyNames="idc_visitareg" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="fecha_ingreso" HeaderStyle-Width="90px" HeaderText="Ingreso"></asp:BoundField>
                        <asp:BoundField DataField="visitante" HeaderStyle-Width="100px" HeaderText="Visitante"></asp:BoundField>
                        <asp:BoundField DataField="empresa" HeaderStyle-Width="110px" HeaderText="Empresa"></asp:BoundField>
                        <asp:BoundField DataField="motivo" HeaderStyle-Width="200px" HeaderText="Motivo"></asp:BoundField>
                        <asp:BoundField DataField="empleado" HeaderStyle-Width="200px" HeaderText="Empleado"></asp:BoundField>
                        <asp:BoundField DataField="fecha_salida" HeaderStyle-Width="60px" HeaderText="Salida"></asp:BoundField>
                        <asp:BoundField DataField="observaciones" HeaderStyle-Width="80px" HeaderText="Observaciones"></asp:BoundField>
                        <asp:BoundField DataField="idc_visitareg" Visible="false"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>