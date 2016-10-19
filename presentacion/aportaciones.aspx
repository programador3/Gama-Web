<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aportaciones.aspx.cs" Inherits="presentacion.aportaciones" %>

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
    <style type="text/css">
        .Ocultar {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" >
        function divs()
        {
            var div=document.getElementById('menu');
            div.style.display='none';
        }
    </script>
    <div style="width:100%">
        <div  align="center" class="ui-header ui-bar-a" style="width:100%;margin-bottom:5px;">
	        <asp:Label ID="lblheader" runat="server" Text="Aportaciones" ForeColor="White" 
                Font-Bold="True" ></asp:Label>            
        </div>
        <table style="width: 100%;">
                <tr>
                    <td>                        
                        <asp:DataGrid ID="gridprod" runat="server" AutoGenerateColumns="False"  
                            Width="100%" AllowSorting="True">
                            <ItemStyle HorizontalAlign="Center" BackColor="White" Font-Size="12px" BorderColor="Gainsboro" BorderWidth=".1px" />
                            <HeaderStyle Font-Size="12px" />
                            <Columns>
                                <asp:BoundColumn DataField="desart" HeaderText="Descripcion" 
                                    SortExpression="desart" >
                                    <ItemStyle HorizontalAlign="Left" Font-Names="arial" Font-Size="Small" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ven" DataFormatString="{0:N2}" 
                                    HeaderText="% Apo P. Venta" SortExpression="ven">
                                    <ItemStyle HorizontalAlign="Right" Font-Names="arial" Font-Size="Small" Font-Bold="true" ForeColor="Blue"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="aportacionven" DataFormatString="{0:N2}" 
                                    HeaderText="Aportacion" SortExpression="aportacionven">
                                    <ItemStyle HorizontalAlign="Right" Font-Names="arial" Font-Size="Small" Font-Bold="true" ForeColor="Blue" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="precio_lista" DataFormatString="{0:N2}" 
                                    HeaderText="Precio Lista" SortExpression="precio_lista">
                                    <ItemStyle HorizontalAlign="Right" Font-Names="arial" Font-Size="Small" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="venlis" DataFormatString="{0:N2}" 
                                    HeaderText="% Apo P. Lista" SortExpression="venlis">
                                    <ItemStyle HorizontalAlign="Right" Font-Names="arial" Font-Size="Small" CssClass="Ocultar"/>
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="apovenlis" DataFormatString="{0:N2}" 
                                    HeaderText="Apo. P. Lista" SortExpression="apovenlis">
                                    <ItemStyle HorizontalAlign="Right" Font-Names="arial" Font-Size="Small" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="RowStyle" />
                        </asp:DataGrid>                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" 
                            Font-Size="Small" Text="Aportación"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    <asp:TextBox ID="txtapo" runat="server" 
                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                            ForeColor="Black" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" 
                            Font-Size="Small" Text="Aportación al Vender con Precios de Lista"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    <asp:TextBox ID="txtapolis" runat="server" 
                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                            ForeColor="Black" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                            Font-Size="Small" Text="% Operacion" Visible="False"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    <asp:TextBox ID="txtoperativo" runat="server" 
                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                            ForeColor="Black" Height="30px" onfocus="this.blur();" Width="100%" 
                            Visible="False"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" 
                            Font-Size="Small" Text="Vendedor" Visible="False"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    <asp:TextBox ID="txtvendedor" runat="server" 
                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                            ForeColor="Black" Height="30px" onfocus="this.blur();" Width="100%" 
                            Visible="False"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                            Font-Size="Small" Text="Total" Visible="False"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    <asp:TextBox ID="txttotal" runat="server" 
                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                            ForeColor="Black" Height="30px" onfocus="this.blur();" Width="100%" 
                            Visible="False"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                                    <asp:Button ID="btnejecutar" runat="server" 
                                    CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                    ForeColor="Black" Height="35px" Text="Cerrar" Width="100%" 
                            onclientclick="window.close();" />
                                
                    </td>
                </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
