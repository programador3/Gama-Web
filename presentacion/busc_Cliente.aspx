<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="busc_Cliente.aspx.cs" Inherits="presentacion.busc_Cliente" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seleccionar Cliente</title>
    <meta name="viewport" content="width=device-width" />
     <link href="GridEstilo.css" rel="stylesheet" type="text/css" />
        <link href="css/jquery.mobile-1.2.0.min.css" rel="stylesheet" /> 
    <link href="js/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
     <script language="javascript" type="text/javascript" >
        function buscar(txt,btn,e)
        {
            
            var key = (document.all) ? e.keyCode : e.which;
            if (key==13)
            {
                if(txt.value != "")
                {
                    btn.click();
                }
                else
                {
                    txt.focus();
                    return false;
                }               
            }           
        
        }
        
        function regresar(index)
        {
            var tabla = document.getElementById("gridclientes");
            var tipo = document.getElementById('txttipo').value;
            if (tipo=='')
            {
                window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtrfc").value= tabla.rows[index].cells[1].textContent;
                window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtidc_cliente").value= tabla.rows[index].cells[3].textContent;
                window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtcve").value= tabla.rows[index].cells[2].textContent;
                window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtcliente").value= tabla.rows[index].cells[0].textContent;
                window.opener.cerrar();
                window.close();
                return false;             
            }
            else if (tipo=='1')
            {
                window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtcliente").value= tabla.rows[index].cells[0].textContent;
                window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtidc_cliente").value= tabla.rows[index].cells[3].textContent;
                //window.parent.cerrar();
                window.close();
                return false; 
            }
               
        }
        
        function x(evt) // Para Evitar el --Enter-- en la Pagina y que realize acciones no solicitadas....
        {
            return (evt ? evt.which : event.keyCode) != 13;
            
        }
     
     </script>
     <style type="text/css">
         .Ocultar {
            display:none;
         }
        .txt_search{padding-left:24px !important;background:white url(Iconos/bcksearch.png) no-repeat left center !important;}     
     </style>
</head>
<body onkeypress="return x(event);">
    <form id="form1" runat="server"  
    style="position: absolute; top: 2px; left: 2px;width: 100%; margin-bottom: 0px;">
    <div style="background-image: url('Iconos/btn_blueSheen.gif');width:100%">
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="Medium" ForeColor="White" Text="Selecciona el Cliente"></asp:Label>
    
    </div>
    <div style="height: 176px">
        <table style="width:100%;">
            <tr>
                <td valign="middle">
                    <asp:TextBox ID="txtbuscar" runat="server" style="display:inline"
                        onfocus="if(this.value=='Buscar...')this.value='';" 
                        onblur="if(this.value=='')this.value='Buscar...'" Width="100%" 
                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini txt_search">Buscar...</asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="23px" style="display:none;"
                        ImageUrl="~/imagenes/btn/icon_buscar.png" Width="23px" OnClick="ImageButton1_Click" />
                    <asp:Button ID="btnbuscar" runat="server" Text="Button" CssClass="Ocultar" OnClick="btnbuscar_Click" />
                    <asp:TextBox ID="txttipo" style="display:none" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="middle">
                
                
                    <asp:Button ID="btncerrar" runat="server" Font-Bold="True" Height="35px" 
                        onclientclick="window.close();" Text="Cerrar" Width="100%" 
                        CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" />
                
                
                </td>
            </tr>
            <tr>
                <td>
                <div style="overflow:auto;overflow-x:hidden;height:300px;">
                
                    <asp:DataGrid ID="gridclientes" runat="server" Width="100%" 
                        AutoGenerateColumns="False" OnItemDataBound="gridclientes_ItemDataBound">
                        <ItemStyle Font-Names="arial" Font-Size="Small" />
                        <HeaderStyle Font-Names="arial" Font-Size="Small" />
                        <Columns>
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" >
                                <ItemTemplate>                                
                                    <asp:ImageButton ID="imgselec" runat="server" Height="25px" Width="25px" ImageUrl="~/imagenes/btn/icon_autorizar.png" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="nombre" HeaderText="Cliente">
                                <ItemStyle Font-Size="8pt" Font-Names="Arial" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="rfccliente" HeaderText="R.F.C">
                                <ItemStyle Font-Size="7pt" Font-Names="Arial" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="cveadi" HeaderText="Cve.">
                                <ItemStyle Font-Size="7pt" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="idc_cliente" HeaderText="idc_cliente" Visible="false">
                                <ItemStyle CssClass="Ocultar"/>
                                <HeaderStyle CssClass="Ocultar"/>
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="RowStyle" />
                    </asp:DataGrid>
                
                </div>    
                </td>
            </tr>
            </table>
    
    
    
    
    </div>
    </form>
</body>
</html>
