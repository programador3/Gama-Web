<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="pendientes_tareas.aspx.cs" Inherits="presentacion.pendientes_tareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Movimientos Pendientes de Revisar en Tareas</h1>
    <div class="row">
        <asp:Repeater ID="Repeater3" runat="server">
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
        </asp:Repeater>
    </div>
</asp:Content>