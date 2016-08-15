<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="prueba.aspx.cs" Inherits="presentacion.prueba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).on('ready', function () {

        });
    </script>
    <style type="text/css">
        .fu {
            border: 2px dotted #000000;
            width: 300px;
            height: 300px;
            background-image: url('/img/arrastra.png');
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:FileUpload runat="server" ID="fuArchivos" AllowMultiple="true" CssClass="fu" />
    <asp:Button runat="server" CssClass="btn btn-info" ID="btnSubir" Text="Subir" OnClick="btnSubir_Click" />
    <br />
    <h5>Tamaño de todos los archivos: <asp:Label ID="Label1" runat="server" Text="0"></asp:Label></h5>
        <h5>Lista de Archivos</h5>
    <asp:BulletedList ID="blist" runat="server"></asp:BulletedList>
</asp:Content>