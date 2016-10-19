<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="datos_ch_p.aspx.cs" Inherits="presentacion.datos_ch_p" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gama Materiales y Aceros</title>
        <link href="css/jquery.mobile-1.2.0.min.css" rel="stylesheet" /> 
    <link href="js/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
    <%----/-----------/----%>
    <meta name="viewport" content="width=device-width" />
</head>
<body>
    <form id="form1" runat="server" style="position: absolute; top: 0px; left: 0px; right: 0px; bottom: 0px;">
        <div style="position: relative; top: 0px; left: 0px; right: 0px; bottom: 0px;">
            <div data-role="header" align="center" class="ui-header ui-bar-a" style="width: 100%; margin-bottom: 8px;">
                <asp:Label ID="lblheader" runat="server" Text="Datos de Check Plus" ForeColor="White" Font-Bold="true"></asp:Label>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td align="right" style="width: 5%" valign="top">
                                    <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" ForeColor="Black" Text="Nombre de la Persona:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtnombrepersona" runat="server" onfocus="this.blur();"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 5%" valign="top">
                                    <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" ForeColor="Black" Text="Clave de Elector:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtclave" runat="server" onfocus="this.blur();"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 5%" valign="top">
                                    <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" ForeColor="Black" Text="Folio de Elector:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtfolio2" runat="server" onfocus="this.blur();"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 5%">
                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" ForeColor="Black" Text="Calle:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtcalle" runat="server" onfocus="this.blur();"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="height: 24px">
                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" ForeColor="Black" Text="Numero:"></asp:Label>
                                </td>
                                <td align="left" style="height: 24px">
                                    <asp:TextBox ID="txtnumero" runat="server" onfocus="this.blur();"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" ForeColor="Black" Text="Colonia:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtcolonia" runat="server"
                                        onfocus="this.blur();"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" ForeColor="Black" Text="Municipio:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtmunicipio" runat="server" Width="100%"
                                        onfocus="this.blur();"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" ForeColor="Black" Text="Estado:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtestado" runat="server" Width="100%" onfocus="this.blur();"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" ForeColor="Black" Text="Pais:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtpais" runat="server" Width="100%" onfocus="this.blur();"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btncancelar" runat="server" Text="Cerrar" Font-Bold="True"
                            Font-Names="arial" Font-Size="Small" ForeColor="Black" Width="100%"
                            CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c"
                            Height="35px" OnClientClick="window.close();"/>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>

