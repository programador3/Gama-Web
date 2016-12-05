<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="pendientes_tareas.aspx.cs" Inherits="presentacion.pendientes_tareas" %>

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
    <h3 class="page-header">Movimientos Pendientes de Revisar en Tareas</h3>
    <div class="row">
       <%-- <asp:Repeater ID="Repeater3" runat="server">
            <ItemTemplate>
                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                    <asp:LinkButton ID="lnkmistarea" CssClass='<%#Eval("css_class") %>' runat="server" PostBackUrl='<%#Eval("pagina") %>' ToolTip='<%#Eval("desc_completa") %>'>

                                                            <h5><%#Eval("pendiente") %></h5>
                                                            <h5>"<%#Eval("descripcion") %>"</h5>
                                                            <h5>FC: <%#Eval("fecha_compromiso") %> </h5>
                                                            <h5><%#Eval("puesto") %></h5>
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:Repeater>--%>
        <div class="col-lg-12">
            <div class=" table table-responsive">
                <asp:GridView Style="font-size: 11px; text-align:center;" ID="gridservicios"
                    CssClass=" gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server" OnRowDataBound="gridservicios_RowDataBound">
                    <Columns>
                          <asp:TemplateField HeaderText="Ver" HeaderStyle-Width="3%">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/btn/icon_buscar.png" Width="30px"
                                     PostBackUrl='<%#Eval("pagina") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="pendiente" HeaderText="Estado" HeaderStyle-Width="15%"></asp:BoundField>
                        <asp:TemplateField HeaderText="Tarea" HeaderStyle-Width="50%">
                            <ItemTemplate>
                                <asp:LinkButton ID="Dd" runat="server" CssClass="btn btn-default btn-block"
                                     Text='<%#Eval("descripcion") %>' PostBackUrl='<%#Eval("pagina") %>' ToolTip='<%#Eval("desc_completa") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="fecha_compromiso" HeaderText="Fecha Compromiso" HeaderStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField DataField="puesto" HeaderText="Usuario" HeaderStyle-Width="15%"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>