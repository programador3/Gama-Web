<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="ver_preped_x_autorizar.aspx.cs" Inherits="presentacion.ver_preped_x_autorizar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ver_preped(idc_preped, tabla, index) {
            window.open('consulta_preped.aspx?idc_preped=' + idc_preped);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="center" class="ui-header ui-bar-a" style="width: 100%; margin-bottom: 20px;">
                <asp:Label ID="lblheader" runat="server" Text="Prepedidos Por Autorizar" ForeColor="White" Font-Bold="True"></asp:Label>
            </div>
            <div class=" row">
                <div class=" col-lg-12">
                    <strong style="width:18%;">Agentes:</strong>
                    <asp:DropDownList ID="cbxagentes" runat="server" CssClass=" form-control2" AutoPostBack="True" Width="80%" OnSelectedIndexChanged="cboagentes_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class=" col-lg-12">
                    <div class="table table-responsive">
                         <asp:DataGrid ID="gridpedidos" runat="server" AutoGenerateColumns="False" style="text-align:center;"
                            Width="100%" OnItemDataBound="gridpedidos_ItemDataBound" CssClass=" table table-responsive table-bordered table-condensed">
                            <HeaderStyle ForeColor="White" BackColor="Gray" />
                            <Columns>
                                <asp:BoundColumn DataField="idc_preped" HeaderText="Prepedido"></asp:BoundColumn>
                                <asp:BoundColumn DataField="rfccliente" HeaderText="RFC"></asp:BoundColumn>
                                <asp:BoundColumn DataField="cveadi" HeaderText="Cve"></asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Nombre"></asp:BoundColumn>
                                <asp:BoundColumn DataField="fecha" HeaderText="Fecha"></asp:BoundColumn>
                                <asp:BoundColumn DataField="observ" HeaderText="Observacion"></asp:BoundColumn>
                                <asp:BoundColumn DataField="usuario" HeaderText="Usuario"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="RowStyle" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
