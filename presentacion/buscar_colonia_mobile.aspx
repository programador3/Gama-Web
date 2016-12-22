<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="buscar_colonia_mobile.aspx.cs" Inherits="presentacion.buscar_colonia_mobile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buscar Colonia</title>
    <meta name="viewport" content="width=device-width" />
    <link href="css/gridestilo.css" rel="stylesheet" />
    <link href="css/jquery.mobile-1.2.0.min.css" rel="stylesheet" /> 
    <link href="js/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
    <%----/-----------/----%>
    <script type="text/javascript" language="javascript" >
    
        function buscar_colonia(e)
        {
            var key = (document.all) ? e.keyCode : e.which;
            if (key==13)
            {
               document.getElementById("btnbuscar").click();
               return false;
            }
        }
        
        
        
        function regresa_valores() 
        {
              var tipo = document.getElementById('txttipo').value;
              if (tipo=='')
              {
                  var idc_colonia=document.getElementById("txtidc_colonia").value;
                  var colonia = document.getElementById("txtcolonia").value;
                  var estado = document.getElementById("txtestado").value; 
                  var mun = document.getElementById("txtmunicipio").value; 
                  var pais = document.getElementById("txtpais").value;         
                  window.parent.document.getElementById('txtidc_colonia').value=idc_colonia;  
                  window.parent.document.getElementById('txtcolonia').value=colonia;
                  window.parent.document.getElementById('txtmun').value=mun;
                  window.parent.document.getElementById('txtedo').value=estado;
                  window.parent.document.getElementById('txtpais').value=pais;
                  window.parent.cerrar();
                  return false;
              }
              else if(tipo==1)                         
              {
                  var idc_colonia=document.getElementById("txtidc_colonia").value;
                  var colonia = document.getElementById("txtcolonia").value;    
                  var estado = document.getElementById("txtestado").value; 
                  var mun = document.getElementById("txtmunicipio").value;
                  var pais =document.getElementById("txtpais").value;
                  var cp =document.getElementById("txtcp").value;
                  var restriccion = document.getElementById("txtrestriccion").value;
                  var capacidad = document.getElementById("txtcapacidad").value;
                  var toneladas = document.getElementById("txttoneladas").value;
                  var zm =document.getElementById("chkzm").checked;
                  window.opener.document.getElementById('txtidc_colonia').value=idc_colonia;  
                  window.opener.document.getElementById('txtcolonia').value=colonia;
                  window.opener.document.getElementById('txtmunicipio').value=mun;
                  window.opener.document.getElementById('txtestado').value=estado;
                  window.opener.document.getElementById('txtpais').value=pais;
                  window.opener.document.getElementById('txtCP').value=cp;
                  window.opener.document.getElementById('lblrestriccion').value=restriccion;
                  if (capacidad=='True')
                  {
                    window.opener.document.getElementById('chkton').checked=true;                  
                  }
                  else
                  {
                    window.opener.document.getElementById('chkton').checked=false;
                  }
                  window.opener.document.getElementById('txttoneladas').value=toneladas;
                  window.opener.document.getElementById('chkzm').checked=zm;
                  //window.parent.cerrar();
                  window.close();
                  return false;
              }
              if (tipo==2)
              {
                 var idc_colonia = $("#<%=txtidc_colonia.ClientID%>").val();//document.getElementById("Contenido_txtidc_colonia").value;
                  var colonia =$("#<%=txtcolonia.ClientID%>").val();// document.getElementById("txtcolonia").value;
                  var estado =$("#<%=txtestado.ClientID%>").val();// document.getElementById("txtestado").value; 
                  var mun =$("#<%=txtmunicipio.ClientID%>").val();// document.getElementById("txtmunicipio").value; 
                  var pais = $("#<%=txtpais.ClientID%>").val();//document.getElementById("txtpais").value;     
                  window.opener.document.getElementById('txtidc_colonia').value=idc_colonia;  
                  window.opener.document.getElementById('txtcolonia').value=colonia;
                  window.opener.document.getElementById('txtmun').value=mun;
                  window.opener.document.getElementById('txtedo').value=estado;
                  window.opener.document.getElementById('txtpais').value=pais;
                  window.close();
                  return false;
              }
              if (tipo==3)
              {
                  var idc_colonia = $("#<%=txtidc_colonia.ClientID%>").val();//document.getElementById("Contenido_txtidc_colonia").value;
                  var colonia =$("#<%=txtcolonia.ClientID%>").val();// document.getElementById("txtcolonia").value;
                  var estado =$("#<%=txtestado.ClientID%>").val();// document.getElementById("txtestado").value; 
                  var mun =$("#<%=txtmunicipio.ClientID%>").val();// document.getElementById("txtmunicipio").value; 
                  var pais = $("#<%=txtpais.ClientID%>").val();//document.getElementById("txtpais").value;

                  window.opener.document.getElementById('Contenido_txtidc_colonia').value = idc_colonia;
                  window.opener.document.getElementById('Contenido_txtcolonia').value = colonia;
                  window.opener.document.getElementById('Contenido_txtmun').value = mun;
                  window.opener.document.getElementById('Contenido_txtedo').value = estado;
                  window.opener.document.getElementById('Contenido_txtpais').value = pais;
                  window.close();
                  return false;
              }
        }  
        
    
    
    </script>
    
    
</head>
<body style="background-color: #eeeeee; font-family: Arial;">
    <form id="form1" runat="server" style="position: absolute; top: 0px; left: 0px; bottom: 0px; right: 0px; width: 100%;">
        <div style="position: relative; top: 0px; left: 0px; bottom: 0px; right: 0px; width: 100%;">
            <div data-role="header" align="center" class="ui-header ui-bar-a" style="width: 100%">
                <asp:Label ID="lblpedidos" runat="server" Text="Selecciona Consignado" ForeColor="White" Font-Bold="true"></asp:Label>
            </div>
            <div style="width: 100%;">
                <div style="width: 100%; padding:15px">
                    <asp:TextBox ID="txtvalor" runat="server" Width="80%" placeholder="Buscar Colonia"
                        CssClass="txt_search ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" OnTextChanged="txtvalor_TextChanged"></asp:TextBox>
                </div>
                <div style="width: 100%">
                    <table style="width: 100%; border-spacing: 0px;">
                        <tr>
                            <td style="width: 70%;">
                                <asp:DropDownList ID="cbocolonias" runat="server" Width="100%" Visible="False">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%;" align="center">
                                <asp:ImageButton ID="imgaceptar" runat="server" Height="25px" Style="margin-left: 10px;"
                                    ImageUrl="~/imagenes/btn/icon_autorizar.png" Visible="False" Width="25px" OnClick="imgaceptar_Click" />
                            </td>
                            <td style="width: 15%;" align="center">
                                <asp:ImageButton ID="imgcancelar" runat="server" Height="25px" Style="margin-left: 10px;"
                                    ImageUrl="~/imagenes/btn/icon_delete.png" Visible="False" Width="25px" OnClick="imgcancelar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width: 100%;">
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="Small" Text="Colonia:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcolonia" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                onfocus="this.blur();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="Small" Text="CP:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcp" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                onfocus="this.blur();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="50">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="Small" Text="Municipio:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtmunicipio" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                onfocus="this.blur();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="Small" Text="Estado:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtestado" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                onfocus="this.blur();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="Small" Text="Pais:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpais" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                onfocus="this.blur();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <div style="padding-top: 5px;">

                    <table style="width: 100%">
                        <tr>
                            <td>

                                <asp:Button ID="btnaceptar" runat="server"
                                    CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c"
                                    Text="Aceptar"
                                    Width="100%" Height="35px" Style="margin: 0px;" />
                            </td>
                            <td>
                                <asp:Button ID="btncancelar" runat="server" OnClientClick="window.close();"
                                    CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c"
                                    Text="Cancelar"
                                    Width="100%" Height="35px" Style="margin: 0px;" />
                            </td>
                        </tr>
                    </table>

                </div>
                <div style="display: none;">
                    <asp:TextBox ID="txtidc_colonia" Text="0" runat="server"></asp:TextBox>
                    <asp:Button ID="btnbuscar" runat="server" Text="Buscar" />
                    <asp:TextBox ID="txtrestriccion" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txtcapacidad" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txttoneladas" runat="server" Text=""></asp:TextBox>
                    <asp:CheckBox ID="chkzm" runat="server" />
                    <asp:TextBox ID="txttipo" runat="server" Text="1"></asp:TextBox>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
