<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Oc_Digitales_Pendientes_2.aspx.cs" Inherits="presentacion.Oc_Digitales_Pendientes_2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
        function pick() {
            var confirmacion = confirm("¿Desea Seleccionar este Registro?")
            if (confirmacion == true) {

                var cbooc = document.getElementById("<%=cboOC.ClientID%>");
                var no_occ = cbooc.options[cbooc.selectedIndex].innerText;
                var oc = no_occ.split("||");
                var id_oc = cbooc.options[cbooc.selectedIndex].value;
                if (cbooc.selectedIndex > 0) {
                    parent.window.opener.document.getElementById('<%= Request.QueryString["txtnumeroOC_obj"]%>').value = oc[0];
                    parent.window.opener.document.getElementById('<%= Request.QueryString["txtidOc_obj"]%>').value = id_oc;
                    window.close();
                    return false;
                }
                else {
                    alert("Seleccionar Orden de Compra.");
                    return false;
                }
                return false;
            }
        }

        function cancelar() {
            window.close();
            return false;
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
            <asp:Label ID="lblhead" runat="server" Text="Ordenes de Compra Pendientes"
                ForeColor="White" Font-Bold="True"></asp:Label>
        </div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 5%;" align="right">
                                <asp:Label ID="RFC" runat="server" Text="RFC:" Font-Bold="True"
                                    ForeColor="Black" Font-Names="arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtrfc" runat="server" Width="80%" Enabled="False" Style="display: inline;"
                                    BackColor="White" ForeColor="Black"
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                <asp:TextBox ID="txtclave" runat="server" Width="15%" Enabled="False" Style="display: inline"
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
                <td align="center" style="font-family: Arial; font-size: small;">
                    <strong>&nbsp;&nbsp;&nbsp;&nbsp;No OCC &nbsp;&nbsp;&nbsp;&nbsp;|| &nbsp;&nbsp;&nbsp;&nbsp; Fecha&nbsp;
                    </strong></td>
            </tr>

            <tr>
                <td>
                    <asp:DropDownList ID="cboOC" runat="server" AutoPostBack="True" Height="35px"
                        Width="100%" Font-Bold="True" Font-Size="Small" ForeColor="Black" OnSelectedIndexChanged="cboOC_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%">
                                <asp:Button ID="btnseleccionar" runat="server" Font-Bold="True"
                                    ForeColor="Black" Text="Seleccionar" Height="35px" Width="100%"
                                    CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" />
                            </td>
                            <td style="width: 100%">
                                <asp:Button ID="btncancelar" runat="server" Font-Bold="True" ForeColor="Black" OnClientClick="return cancelar();"
                                    Text="Cancelar" Height="35px" Width="100%"
                                    CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td align="center">
                    <img id="Orden_Compra" runat="server" alt="Seleccionar Orden de Compra"
                        src="~/imagenes/128.png" style="width: 100%" /></td>
            </tr>

            <tr>
                <td>
                    <asp:TextBox ID="txtno_oc" runat="server" CssClass="Ocultar"></asp:TextBox>
                    <asp:TextBox ID="txtidoc" runat="server" CssClass="Ocultar"></asp:TextBox>
                </td>
            </tr>

        </table>

    </form>
</body>
</html>
