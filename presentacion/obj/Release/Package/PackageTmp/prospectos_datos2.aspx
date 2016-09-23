<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prospectos_datos2.aspx.cs" MasterPageFile="~/Global.Master" Inherits="presentacion.prospectos_datos2" %>

<asp:Content ID="pdatoshea" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

       $(document).ready(function () {
           $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
               "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
           });
       });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" runat="server">

    <div class="row">
        <div class="col-lg-10">
            <h1 class="page-header">Prospectos</h1>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnagregar" runat="server" OnClick="btnagregar_Click" Text="Agregar" CssClass="btn btn-success btn-block" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnreporte" runat="server" Text="Reporte" CssClass="btn btn-primary btn-block" OnClick="btnreporte_Click" />
        </div>
    </div>
    <!-- filtros -->
    <div class="row">
        <div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>

            <label>De:</label>
            <asp:TextBox ID="txtfecha1" runat="server" type="date" CssClass="form-control"></asp:TextBox>
        </div>

        <div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>
            <label>A:</label>
            <asp:TextBox ID="txtfecha2" runat="server" type="date" CssClass="form-control"></asp:TextBox>
        </div>
        <div class='col-xs-12'>
            <asp:Button ID="btnfiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-block" OnClick="btnfiltrar_Click" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="table table-responsive">
            <div class="col-lg-12">
                <asp:GridView Style="font-size: 11px; text-align: center;" ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table table-bordered table-condensed gvv" OnRowCommand="GridView1_RowCommand" DataKeyNames="idc_prospecto">
                    <Columns>
                        <asp:ButtonField HeaderStyle-Width="20px" ButtonType="Image" ImageUrl="~/imagenes/btn/icon_buscar.png" Text="Detalle" CommandName="detalle" CausesValidation="False"></asp:ButtonField>
                        <asp:ButtonField HeaderStyle-Width="20px" ButtonType="Image" CommandName="editar" ImageUrl="~/imagenes/btn/icon_editar.png" Text="Editar"></asp:ButtonField>
                        <asp:BoundField DataField="idc_prospecto" HeaderText="ID"></asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Dirección"></asp:BoundField>
                        <asp:BoundField DataField="nombre_razon_social" HeaderText="Nombre / Razon Social"></asp:BoundField>
                        <asp:BoundField DataField="contacto" HeaderText="Contacto" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Telefono" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="tipo_obra" HeaderText="Tipo de Obra"></asp:BoundField>
                        <asp:BoundField DataField="correo" HeaderText="Correo" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="tamaño_obra" HeaderText="Tamaño de la Obra"></asp:BoundField>
                        <asp:BoundField DataField="etapa_obra" HeaderText="Etapa de la Obra"></asp:BoundField>
                        <asp:BoundField DataField="observacion" HeaderText="Observaciones"></asp:BoundField>
                        <asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro"></asp:BoundField>
                        <asp:BoundField DataField="usuario" HeaderText="Usuario"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>