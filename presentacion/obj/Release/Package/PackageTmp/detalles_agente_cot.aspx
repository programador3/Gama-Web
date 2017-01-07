<%@ Page Language="VB" MasterPageFile="~/Adicional.Master" AutoEventWireup="false" CodeFile="detalles_agente_cot.aspx.vb" Inherits="detalles_agente_cot" title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" Runat="Server">
    <script type="text/javascript">
        function cargar_detalles()
        {
            var index = window.opener.document.getElementById('Contenido_txtindex').value;
            var griddatos = window.opener.document.getElementById('Contenido_griddatos');
            if(index!='')
            {
                if(griddatos!=null)
                {
                   var txtfecha = document.getElementById('<%=txtfecha.ClientID%>');
                   var txtprecioactual = document.getElementById('<%=txtprecioactual.ClientID%>');
                   var txtprecioauto = document.getElementById('<%=txtprecioauto.ClientID%>');
                   var txtpreciominimo = document.getElementById('<%=txtpreciominimo.ClientID%>');
                   var txtstatus = document.getElementById('<%=txtstatus.ClientID%>');
                   var txtultimodescuento = document.getElementById('<%=txtultimodescuento.ClientID%>');
                   var txtultimoprecio = document.getElementById('<%=txtultimoprecio.ClientID%>');
                    
                   txtfecha.value = griddatos.rows[index].cells[9].textContent;
                   txtprecioactual.value = griddatos.rows[index].cells[5].textContent;
                   txtprecioauto.value = griddatos.rows[index].cells[3].textContent;
                   txtpreciominimo.value = griddatos.rows[index].cells[6].textContent;
                   txtstatus.value = griddatos.rows[index].cells[4].textContent;
                   txtultimodescuento.value = griddatos.rows[index].cells[7].textContent;
                   txtultimoprecio.value = griddatos.rows[index].cells[8].textContent;
                }
            }
        
        }
        
        function divs()
        {
            var div=document.getElementById('menu');
            div.style.display='none';
        }
    
    
    
    </script>


    <div style="width:100%;position:relative;top:0px;left:0px;bottom:0px;">
    <h2 style="text-align:center;"><strong>Detalles de Cotización</strong></h2>
        <table style="width: 100%;">
            <tr>
                <td>                    
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Black" Text="Precio Actual:">
                        </asp:Label>                    
                </td>
            </tr>
            <tr>
                <td>
                    
                        <asp:TextBox ID="txtprecioactual" runat="server" Width="100%" onfocus="this.blur();" CssClass="form-control2">
                        </asp:TextBox>                                
                    
                </td>
            </tr>
            <tr>
                <td>                    
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Black" Text="Precio Minimo:">
                        </asp:Label>                    
                </td>
            </tr>
            <tr>
                <td>                    
                        <asp:TextBox ID="txtpreciominimo" runat="server" Width="100%" onfocus="this.blur();" CssClass="form-control2">
                        </asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td>
                    
                       <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Black" Text="Precio Por Autorizar:">
                       </asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>                    
                        <asp:TextBox ID="txtprecioauto" runat="server" Width="100%" onfocus="this.blur();" CssClass="form-control2">
                        </asp:TextBox>                        
                </td>
            </tr>
            <tr>
                <td>
                    
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Black" Text="Status:">
                        </asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                        <asp:TextBox ID="txtstatus" runat="server" Width="100%" onfocus="this.blur();" CssClass="form-control2">
                        </asp:TextBox>                                
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Black" Text="Ultimo Descuento:">
                        </asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                        <asp:TextBox ID="txtultimodescuento" runat="server" Width="100%" onfocus="this.blur();" CssClass="form-control2">
                        </asp:TextBox>                                
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Black" Text="Ultimo Precio:">
                        </asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                        <asp:TextBox ID="txtultimoprecio" runat="server" Width="100%" onfocus="this.blur();" CssClass="form-control2">
                        </asp:TextBox>                                
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Black" Text="Fecha:">
                        </asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                        <asp:TextBox ID="txtfecha" runat="server" Width="100%" onfocus="this.blur();" CssClass="form-control2">
                        </asp:TextBox>                                
                    
                </td>
            </tr>
            <tr>
                <td>                    
                        <asp:Button ID="btncerrar" runat="server" CssClass="btn btn-danger btn-block" 
                        Text="Cerrar" Width="100%" 
                            onclientclick="window.close();" />                                
                </td>
            </tr>
        </table>    
    </div> 
</asp:Content>

