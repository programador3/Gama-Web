<%@ Page Language="VB" MasterPageFile="~/Adicional.Master" AutoEventWireup="false" CodeFile="Ficha_Tecnica.aspx.vb" Inherits="Ficha_Tecnica" title="Ficha Tecnica" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" Runat="Server">

    <script language="javascript" type="text/javascript" >
function capturar_correo()
{
    var correo=prompt("Ingresa Email:","");
    if(correo==null)
    {
        return false;
    }
    
    if(correo!='')
    {
        var valida = validarEmail(correo);
       
        if(valida==true)
        {
            document.getElementById("<%=txtcorreo.ClientID%>").value=correo;
            document.getElementById("<%=btncorreo.ClientID%>").click();
        }
        else
        {
            alert("La Dirección de Email es Incorrecta.");
            return false;
        }
    }
    return false;
}
function validarEmail(valor) 
{     
  if (/^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/.test(valor))
  {
   return true;
  }
  else
  {
   return false;
  }
}

function ver_pdf()
{
    var ruta=document.getElementById("<%=txtruta.ClientID%>").value;
    if(ruta!="")
    {
        window.open(ruta);
        return false;
    }
}
function divs()
{
    var div=document.getElementById('menu');
    div.style.display='none';
}
</script>
    <style>
        .Ocultar {
            display:none;
        }
    </style>
    <div align="center" class="ui-header ui-bar-a" style="width:100%;margin-bottom:8px;">
        <asp:Label ID="lblheader" runat="server" Text="Detalles del Producto" ForeColor="White" Font-Bold="true" ></asp:Label>
    </div>
    <table style="width:100%">
        <tr>
            <td style="height: 24px">
                <table style="width:100%;">
                    <tr>
                        <td bgcolor="#3399FF" 
                            
                            style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial; width: 50%;">
                            Producto</td>
                        <td bgcolor="#0066FF" 
                            
                            style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial; width: 50%;">
                            Caracteristicas</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblproducto" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblcaracteristicas" runat="server" Font-Bold="True" 
                                Font-Names="arial" Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                            Unidad de Medida</td>
                        <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                            Codigo GAMA</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblunidad" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblcodigo" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                            Proveedor</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblproveedor" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 24px">
                <table style="width:100%;">
                    <tr>
                        <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial; width: 50%;" 
                            width="25%" align="center">
                            Foto</td>
                        <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;" 
                                        align="center">
                            Breve Descripcion Tecnica</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="Image1" runat="server" Height="300px" Width="100%" 
                                BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtdescripcion" runat="server" Height="298px" 
                                TextMode="MultiLine" Width="100%" Onfocus="this.blur();" 
                                style="resize:none;" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Bold="False" Font-Names="arial" Font-Size="Small" ForeColor="Black"></asp:TextBox>                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                            <table style="width:100%;">
                                <tr>
                                    <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;" 
                                        width="50%">
                                        Familia</td>
                                    <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                                        Subfamilia</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblfamilia" runat="server" Font-Bold="True" Font-Names="arial" 
                                            Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblsubfamilia" runat="server" Font-Bold="True" 
                                            Font-Names="arial" Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                                        Venta&nbsp; Minima</td>
                                    <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                                        Multiplos</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblventa" runat="server" Font-Bold="True" Font-Names="arial" 
                                            Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblmultiplos" runat="server" Font-Bold="True" Font-Names="arial" 
                                            Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                                        Volumen</td>
                                    <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                                        Tiempo de Entrega</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblvolumen" runat="server" Font-Bold="True" Font-Names="arial" 
                                            Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbltiempo" runat="server" Font-Bold="True" Font-Names="arial" 
                                            Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                                        Garantia</td>
                                    <td style="background-color: #0066FF; color: white; font-weight: bold; font-style: normal; font-size: small; font-family: arial;">
                                        Vida/Caducidad</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblgarantia" runat="server" Font-Bold="True" Font-Names="arial" 
                                            Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblvida" runat="server" Font-Bold="True" Font-Names="arial" 
                                            Font-Size="Small" ForeColor="Black" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
            </td>
        </tr>
        <%--<tr>
            <td>
                            <table style="width:100%;">
                                <tr>
                                    <td style="border: 1px solid navy; overflow:auto; height:150px;" 
                                        width="100%" valign="top">
                                        <asp:DataGrid ID="gridcodigos" runat="server" Width="100%" style="display:none;"
                                            HeaderStyle-BackColor="#66CCFF" AutoGenerateColumns="False">
                                            
                                            
                                            <Columns>
                                                <asp:BoundColumn DataField="Tipo_codigo" HeaderText="Tipo de Codigo">
                                                    <ItemStyle BackColor="Navy" ForeColor="Yellow" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Codigo" HeaderText="Codigo">
                                                    <ItemStyle BackColor="Navy" ForeColor="Yellow" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            
                                        <HeaderStyle CssClass="RowStyle" Font-Bold="False" Font-Size="Small" ForeColor="Black"></HeaderStyle>
                                            
                                        </asp:DataGrid>
                                        
                                    </td>
                                </tr>
                            </table>
            </td>
        </tr>--%>
        <tr>
            <td>
                                                    <asp:Label ID="lblficha" runat="server" Text="Ficha Tecnica:" Font-Bold="True" 
                                                        Font-Names="arial" Font-Size="Small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 33.3%">
                                                    <asp:Button ID="btnenviar" runat="server"  Text="Enviar" Width="100%" 
                                                        onclientclick="return capturar_correo();" 
                                                        CssClass="btn btn-default" />
                                                </td>
                                                <td style="width: 33.3%">
                                                    <asp:Button ID="btnver" runat="server" Text="Ver" Width="100%" OnClientClick="return ver_pdf();" 
                                                        CssClass="btn btn-default" />
                                                </td>
                                                <td width="50" style="width: 33.3%">
                                                    <asp:Button ID="Button3" runat="server"  Text="Cerrar" Width="100%" onclientclick="window.close();" 
                                                        CssClass="btn btn-danger" />
                                                </td>
                                            </tr>
                                        </table>
            </td>
        </tr>
    </table>
    <asp:Button ID="btncorreo" Text="" runat="server" CssClass="Ocultar" />
    <asp:TextBox ID="txtcorreo" Text ="" runat="server" CssClass="Ocultar" ></asp:TextBox>
    <asp:TextBox ID="txtruta" Text ="" runat="server" CssClass="Ocultar" >
    </asp:TextBox>
</asp:Content>


