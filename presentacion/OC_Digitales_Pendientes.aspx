<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OC_Digitales_Pendientes.aspx.cs" Inherits="presentacion.OC_Digitales_Pendientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Pendientes</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width" />
    <%---- -- Style Elements -- ----%>
    <link href="css/jquery.mobile-1.2.0.min.css" rel="stylesheet" />
    <link href="js/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
    <link href="TinyBox/style.css" rel="stylesheet" />
    <script src="TinyBox/tinybox.js"></script>
    <link href="js/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script src="Textbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function divs() {
            var div = document.getElementById('menu');
            div.style.display = 'none';
        }


    </script>
    <style type="text/css">
        .Ocultar {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">


        <div data-role="header" align="center" class="ui-header ui-bar-a" style="width: 100%; margin-bottom: 8px;">
            <asp:Label ID="lblhead" runat="server" Text="Ordenes de Compra"
                ForeColor="White" Font-Bold="True"></asp:Label>
        </div>
        <table style="width: 100%">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td align="right" style="width: 5%">
                                <asp:Label ID="RFC" runat="server" Text="RFC:" Font-Bold="True"
                                    ForeColor="Black" Font-Names="arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtrfc" runat="server" Width="80%" Enabled="False" Style="display: inline;"
                                    BackColor="White" ForeColor="Black"
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                <asp:TextBox ID="txtclave" runat="server" Width="15%" Enabled="False" Style="display: inline;"
                                    BackColor="White" ForeColor="Black"
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label4" runat="server" Text="Cliente:" Font-Bold="True"
                                    ForeColor="Black" Font-Names="arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcliente" runat="server" Width="100%" Enabled="False"
                                    BackColor="White" ForeColor="Black"
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" style="font-family: Arial; font-size: small; font-weight: bold;">
                    <asp:Button ID="btncerrar" runat="server"
                        CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c"
                        Font-Bold="True" Font-Names="arial" Font-Size="Small" Height="35px"
                        OnClientClick="window.close();" Text="Cerrar" Width="100%" />
                </td>
            </tr>
            <tr>
                <td align="left" style="font-family: Arial; font-size: small; font-weight: bold;">
                    <strong>No OCC    &nbsp;&nbsp; ||    &nbsp;&nbsp;  Fecha</strong>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">

                    <div id="select" class="styled-select" style="width: 100%">
                        <asp:DropDownList ID="cboOC" runat="server" AutoPostBack="True" Height="35px" Width="100%" CssClass="dropdownlist"
                            Font-Bold="True" Font-Size="Small" ForeColor="Black">
                        </asp:DropDownList>

                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <img id="Orden_Compra" runat="server" alt="No Disponible"
                        src="~/OC_Pendientes/1.jpg" style="width: 100%;" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
