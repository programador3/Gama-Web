<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_filtro.aspx.cs" Inherits="presentacion.reporte_filtro" %>

<asp:Content ID="reporte_head" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="reporte_content" ContentPlaceHolderID="Contenido" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <h1>
                <asp:Label ID="lblReporte" runat="server" Text="Reporte Prospectos"></asp:Label></h1>
        </div>


    </div>
    <div class="row">
        <div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>
            De:
                <asp:TextBox ID="txtfecha1" runat="server" type="date" CssClass="form-control"></asp:TextBox>

        </div>

        <div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>
            A: 
                <asp:TextBox ID="txtfecha2" runat="server" type="date" CssClass="form-control"></asp:TextBox>
        </div>
        <div class='col-xs-12'>
            <asp:Button ID="btnfiltrar" runat="server" Text="Aceptar" CssClass="btn btn-primary btn-block" OnClick="btnfiltrar_Click" />

        </div>
    </div>


</asp:Content>
