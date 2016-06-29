<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="empleados_reportes_pendientes.aspx.cs" Inherits="presentacion.empleados_reportes_pendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Reportes a Empleados sin Leer</h1>
    <div class="row">
        <asp:Repeater ID="repeat" runat="server">
            <ItemTemplate>
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <asp:LinkButton ID="lnkgo" CommandName='<%#Eval("idc_empleadorep") %>' CommandArgument='<%#Eval("idc_empleado") %>' OnClick="lnkgo_Click" ToolTip='<%#Eval("observaciones_completa") %>' CssClass="btn btn-info btn-block" runat="server">
                        <h5><%#Eval("observaciones") %></h5>
                        <h5><%#Eval("REPORTE") %></h5>
                        <h5><%#Eval("empleado") %></h5>
                        <h5><%#Eval("fecha_reporte") %></h5>
                        <h5><%#Eval("usuario") %></h5>
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>