<%@ Page Title="Precios Cotizados" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="precios_cotizados.aspx.cs" Inherits="presentacion.precios_cotizados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <h2 class=" page-header">Precios Cotizados</h2>
    <div class="row">
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:DataGrid ID="gridproductos" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive table-bordered table-condensed">
                    <HeaderStyle ForeColor="White" BackColor="Gray"/>
                    <ItemStyle Font-Names="arial" Font-Size="Small" />
                    <Columns>
                        <asp:BoundColumn DataField="codigo" HeaderText="Codigo">
                            <ItemStyle HorizontalAlign="Center" CssClass="Ocultar" />
                            <HeaderStyle CssClass="Ocultar" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="desart" HeaderText="Desc">
                            <ItemStyle HorizontalAlign="Justify" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="unimed" HeaderText="UM">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="precio_cliente" DataFormatString="{0:N2}" HeaderText="Precio">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="comi" DataFormatString="{0:N2}" HeaderText="%">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ultima_venta_cantidad" DataFormatString="{0:N2}" HeaderText="Cantidad">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="dias" HeaderText="Dias">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="uf" HeaderText="Fecha Ultima Ventas">
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
             <asp:LinkButton ID="lnkregistrarvisita" CssClass="btn btn-danger btn-block" runat="server" PostBackUrl="~/ficha_cliente_m.aspx">
                <i class="fa fa-reply" aria-hidden="true"></i>&nbsp;Regresar</asp:LinkButton>
        </div>
    </div>
</asp:Content>
