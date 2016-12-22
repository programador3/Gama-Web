<%@ Page Language="VB" AutoEventWireup="false" CodeFile="comisiones_esp_articulos.aspx.vb" Inherits="comisiones_esp_articulos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>  
    
    <title>Comisiones Especiales Articulos</title>     
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    
    <link href='http://fonts.googleapis.com/css?family=Roboto+Condensed:300,400' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900' rel='stylesheet' type='text/css' />
     <link href="css/gridestilo.css" rel="stylesheet" />
    <link href="css/jquery.mobile-1.2.0.min.css" rel="stylesheet" /> 
    <link href="js/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
    <link href="TinyBox/style.css" rel="stylesheet" />
    <script src="TinyBox/tinybox.js"></script>
    <link href="js/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <link rel="shortcut icon" href="imagenes/favicon.png" />

    <style type="text/css">
        .auto-style1
        {
            height: 29px;
        }
        body {
            font-family: arial;
        }
    </style>
    <script type="text/javascript">
        function PlaySound(path) {
            var audio = new Audio(path);
            audio.play();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="position: absolute; top: 0px; left: 0px; bottom: 0px; width: 100%">
        <div style="width: 100%;padding:1px;">

            <div align="center" class="ui-header ui-bar-a" style="width: 100%; margin-bottom: 20px; background-color: black">
                <asp:Label ID="lblheader" runat="server" Text="Comisiones Especiales Articulos" ForeColor="White" BackColor="Black" Font-Bold="True">
                </asp:Label>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td align="left">

                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Button ID="btncerrar" runat="server" Text="Cerrar" CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c"
                                        OnClientClick="javascript:self.close();" Font-Bold="True" Font-Names="arial" Font-Size="Small" Height="35px" Width="100%" />
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black" Font-Names="arial" Font-Size="Small"
                                        Text="Semana:"></asp:Label>
                                </td>
                                <td style="width: 90%">
                                    <asp:DropDownList ID="cbox_semana_articulos" runat="server" CssClass="dropdownlist"
                                        Font-Bold="True" ForeColor="Black" Height="30px" Width="100%" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                 <tr>
                    <td align="right" class="auto-style1">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Cantidad:" Font-Bold="True"></asp:Label></td>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txtcantidad" Width="80%" runat="server" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                    </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Aportacion:" Font-Bold="True"></asp:Label></td>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txtaportacuin" Width="80%" runat="server" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                    </td>
                            </tr>
                        </table>
                    </td>

                </tr>

                <tr>
                    <td>
                        <!-- grid -->
                        <asp:GridView ID="grid_comision_esp_articulos" runat="server" AutoGenerateColumns="False" Width="100%">
                            <RowStyle CssClass="" HorizontalAlign="Right" Font-Size="10px" Font-Names="arial" />
                            <HeaderStyle CssClass="HeaderStyle" Font-Size="12px" Font-Names="arial" />
                            <Columns>
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                                <asp:BoundField DataField="DESART" HeaderText="DesArt" />
                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                <asp:BoundField DataField="CODFAC" HeaderText="CodFac" />
                                <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />
                                <asp:BoundField DataField="APORTACION" HeaderText="Aportacion" />
                            </Columns>


                        </asp:GridView>

                    </td>
                </tr>
               


            </table>
        </div>
    </form>
</body>
</html>
