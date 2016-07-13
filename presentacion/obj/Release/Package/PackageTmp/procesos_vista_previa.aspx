<%@ Page Title="Vista Previa" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="procesos_vista_previa.aspx.cs" Inherits="presentacion.procesos_vista_previa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:HiddenField ID="hiddenvalue" runat="server" />
    <h1 class="page-header">
        <asp:Label ID="lnltipo" runat="server" Text=" Tipo Borrador" CssClass="btn btn-primary"></asp:Label></h1>
</asp:Content>