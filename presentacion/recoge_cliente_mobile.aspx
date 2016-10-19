<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recoge_cliente_mobile.aspx.cs" Inherits="presentacion.recoge_cliente_mobile" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nombre de Quien Recoge el Material</title>
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
    <%----/-----------/----%>
    <style type="text/css">
        #form1 {
            margin-bottom: 3px;
        }
    </style>


    <script type="text/javascript" language="javascript">
        window.onload = CargarDatos;

        function CargarDatos() {
            document.getElementById('<%=txtnombre.ClientID%>').value = window.opener.document.getElementById('Contenido_txtnombrerecoge').value;
        var paterno = document.getElementById('<%=txtpaterno.ClientID%>').value = window.opener.document.getElementById('Contenido_txtpaternor').value;
        var materno = document.getElementById('<%=txtmaterno.ClientID%>').value = window.opener.document.getElementById('Contenido_txtmaternor').value;
        var sucursal = document.getElementById('<%=cbosucursales.ClientID%>');
        sucursal.options[sucursal.selectedIndex].value = window.opener.document.getElementById('Contenido_txtsucursalr').value;
    }
    function RegresaDatos() {

        var nombre = document.getElementById('<%=txtnombre.ClientID%>').value;
        var paterno = document.getElementById('<%=txtpaterno.ClientID%>').value;
        var materno = document.getElementById('<%=txtmaterno.ClientID%>').value;
        var sucursal = document.getElementById('<%=cbosucursales.ClientID%>');
        var valor = sucursal.options[sucursal.selectedIndex].value;
        if (nombre == "" || paterno == "") {
            alert("Es necesario especificar el nombre de quien recoge el material");
            return false;
        }
        else {
            if (valor == "--- Seleccionar ---" || valor == 0) {
                alert("Especificar la sucursal donde se va a recoger");
                return false;
            }
            else {
                window.opener.document.getElementById('Contenido_txtnombrerecoge').value = nombre;
                window.opener.document.getElementById('Contenido_txtpaternor').value = paterno;
                window.opener.document.getElementById('Contenido_txtmaternor').value = materno;
                window.opener.document.getElementById('Contenido_txtsucursalr').value = valor;
                window.close();
                return false;
            }

        }


    }

    function cancelar() {
        window.close();
        return false;
    }



    </script>

</head>
<body>
    <form id="form1" runat="server" 
    style="position: absolute; width: 99%; top: 2px; left: 2px">
    <div>
    <div data-role="header" align="center" class="ui-header ui-bar-a" style="width:100%;margin-bottom:8px;">
	        <asp:Label ID="lblpedidos" runat="server" Text="Recoge Cliente" ForeColor="White" Font-Bold="true" ></asp:Label>
        </div>
     
        <table style="width: 100%;">
            <tr>
                <td width="5%" align="right">
                    <asp:Label ID="Label1" runat="server" Text="Nombre:" 
                        style="font-family:Arial;font-weight:bold;font-size:small;"></asp:Label>
                </td>
                <td>
                                        <asp:TextBox ID="txtnombre" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                    </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Text="Paterno:" 
                        style="font-family:Arial;font-weight:bold;font-size:small;"></asp:Label>
                </td>
                <td>
                                        <asp:TextBox ID="txtpaterno" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                    </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" Text="Materno:" 
                        style="font-family:Arial;font-weight:bold;font-size:small;"></asp:Label>
                </td>
                <td>
                                        <asp:TextBox ID="txtmaterno" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                    </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label4" runat="server" Text="Sucursal:" 
                        style="font-family:Arial;font-weight:bold;font-size:small;"></asp:Label>
                </td>
                <td>
                                        <asp:DropDownList ID="cbosucursales" runat="server" Width="100%" Height="30px">
                                        </asp:DropDownList>
                                    </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td>
                
                                        <asp:Button ID="btnaceptar" runat="server" 
                                            CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                             Text="Aceptar" 
                                            Width="100%" Height="32px"  style="margin:0px;font-size:small;"/>
                                
                </td>
                <td width="width:50%;">
                
                                        <asp:Button ID="btncancelar" runat="server" 
                                            CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                             Text="Cancelar" 
                                            Width="100%" Height="32px"  
                        style="margin:0px;font-size:small;" onclientclick="window.close();"/>
                                
                </td>            
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
