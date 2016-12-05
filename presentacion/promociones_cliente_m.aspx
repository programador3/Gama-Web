<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="promociones_cliente_m.aspx.cs" Inherits="presentacion.promociones_cliente_m" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Promociones Clientes</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width" />
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
            display: none;
        }
    </style>
    <script type="text/javascript">

        function datos(index) {
            var gridpromo = document.getElementById('<%=gridpromo.ClientID%>');
            var gridregalos = document.getElementById('<%=gridregalos.ClientID%>');
            var idc_promocion;
            if (gridpromo != null) {
                for (var i = 1; i <= gridpromo.rows.length - 1; i++) {
                    if (index == i) {
                        gridpromo.rows[i].style.backgroundColor = '#fcb814';
                        gridpromo.rows[i].style.color = 'white';
                        idc_promocion = gridpromo.rows[i].cells[0].textContent;
                    }
                    else {
                        gridpromo.rows[i].style.backgroundColor = 'white';
                        gridpromo.rows[i].style.color = 'black';
                    }
                }
            }

            var idc_promo;
            if (gridregalos != null) {
                for (var i = 1; i <= gridregalos.rows.length - 1; i++) {
                    idc_promo = gridregalos.rows[i].cells[2].textContent;
                    if (idc_promo == idc_promocion) {
                        gridregalos.rows[i].className = '';
                    }
                    else {
                        gridregalos.rows[i].className = 'Ocultar';
                    }

                }
            }
            return false;

        }



    </script>



</head>
<body style="background-color:#eeeeee;">
    <form id="form1" runat="server" style="position:absolute;left:0px;top:0px;bottom:0px;width:100%;">
    <div style="position:relative;top:0px;left:0px;bottom:0px;">
    
    
    <div  align="center" class="ui-header ui-bar-a" style="width:100%;margin-bottom:8px;">
	            <asp:Label ID="lblheader" runat="server" Text="Promociones Que Aplican Para el Cliente" 
                    ForeColor="White" Font-Bold="True" Font-Names="arial" Font-Size="Small" ></asp:Label>
    </div>
            <table style="width:100%;">
                <tr>
                    <td style="width: 3%">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                            Font-Size="Small" Text="Cliente:" ForeColor="Blue"></asp:Label>
                    </td>
                    <td style="width: 95%">
                                <asp:TextBox ID="txtcliente" runat="server" Width="100%" 
                                    Font-Bold="True" ReadOnly="True" onfocus="this.blur()" Font-Size="9pt" 
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                </td>
                </tr>
        </table>
            <table style="width:100%;">
                <tr>
                    <td>
                    <asp:DataGrid ID="gridpromo" runat="server" Width="100%" style="background-color:White;"
                            AutoGenerateColumns="False" OnItemDataBound="gridpromo_ItemDataBound">
                            <ItemStyle Font-Names="arial" Font-Size="8pt" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="RowStyle" Font-Names="arial" Font-Size="8pt" />                            
                            <Columns>
                                <asp:BoundColumn DataField="idc_promocion" HeaderText="Id. Promocion">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="articulo" HeaderText="Articulo">
                                   <ItemStyle Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechai" HeaderText="Fecha Inicial" DataFormatString="{0:d}">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechaf" HeaderText="Fecha Final" DataFormatString="{0:d}"></asp:BoundColumn>
                                <asp:BoundColumn DataField="multiplos" HeaderText="Multiplos"></asp:BoundColumn>
                            </Columns>

                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                    
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" 
                            Font-Size="Small" Text="Articulos de Regalo" ForeColor="Blue"></asp:Label>
                    
                    </td>
                </tr>
                <tr>
                    <td>
                    
                        <asp:DataGrid ID="gridregalos" runat="server" AutoGenerateColumns="False" style="background-color:White;"
                            Width="100%">
                            <ItemStyle Font-Names="arial" Font-Size="Small" HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundColumn DataField="articulo" HeaderText="Articulo"></asp:BoundColumn>
                                <asp:BoundColumn DataField="cantidad" HeaderText="Cantidad"></asp:BoundColumn>
                                <asp:BoundColumn DataField="idc_promocion" HeaderText="">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="RowStyle" Font-Names="arial" Font-Size="Small" />
                        </asp:DataGrid>
                    
                    </td>
                </tr>
                <tr>
                    <td>
                    
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                    
                        <asp:Button ID="btnpromociones" runat="server" Height="35px" Text="Cerrar" 
                            Width="100%" Font-Bold="True" Font-Size="Medium" CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" onclientclick="window.close();" />
                   </td>
                </tr>
            </table>
    
    
    </div>
    </form>
</body>
</html>
