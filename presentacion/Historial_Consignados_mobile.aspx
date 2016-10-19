<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historial_Consignados_mobile.aspx.cs" Inherits="presentacion.Historial_Consignados_mobile" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selecciona Consignado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width" />
    <%---- -- Style Elements -- ----%>
    <link rel ="Stylesheet" href="css/jquery.mobile-1.2.0.min.css" />
    <link href="js/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <%----/-----------/----%>
    <style type="text/css">
        #form1
        {
            margin-bottom: 3px;
        }
        .Ocultar{display:none;}
        input[type="text"]{margin:0 !important;}
    </style>
    
    
    <script language="javascript" type="text/javascript" >
        function close_window()
        {
            window.close();    
            return false;
        }
        //Funcion: Retorna los valores, colonia, calle, CP, etc...
               
        function regresa_valores(index) 
        {
              var tabla = document.getElementById("grdconsignados");
              
              var idc_colonia=tabla.rows[index].cells[12].textContent;
              var calle=tabla.rows[index].cells[1].textContent;;
              var numero=tabla.rows[index].cells[2].textContent;;
              var cp=tabla.rows[index].cells[3].textContent;;
              var colonia =tabla.rows[index].cells[4].textContent;;
              var restriccion = tabla.rows[index].cells[8].textContent;;
              var capacidad = tabla.rows[index].cells[9].textContent;;
              var municipio= tabla.rows[index].cells[5].textContent;;
              var estado= tabla.rows[index].cells[6].textContent;;
              var pais = tabla.rows[index].cells[7].textContent;;
              var toneladas = tabla.rows[index].cells[10].textContent;;
              var zm = tabla.rows[index].cells[11].textContent;;
              window.opener.document.getElementById('txtidc_colonia').value=idc_colonia;              
              window.opener.document.getElementById('txtcalle').value=calle;
              window.opener.document.getElementById('txtnumero').value=numero;
              window.opener.document.getElementById('txtCP').value=cp;      
              window.opener.document.getElementById('txtcolonia').value=colonia;
              window.opener.document.getElementById('lblrestriccion').value=restriccion;
              window.opener.document.getElementById('txtmunicipio').value=municipio;
              window.opener.document.getElementById('txtestado').value=estado;
              window.opener.document.getElementById('txtpais').value=pais;
              window.opener.document.getElementById('txttoneladas').value=toneladas;
              window.opener.document.getElementById('lblrestriccion').value=restriccion;
              window.opener.document.getElementById('txtzm').value=zm;
              window.opener.document.getElementById('txtproy').value=0;
              window.opener.document.getElementById('cboproyectos').selectedIndex=0;
              if (capacidad=='False')
              {
              window.opener.document.getElementById('chkton').checked = false;
              }
              else
              {
              window.opener.document.getElementById('chkton').checked = true;
              }       
              window.close();
              return false;
          }

        
    </script>
</head>
<body>
    <form id="form1" runat="server" 
    style="position: absolute; top: 2px; left: 2px; width: 99%;">
    <div>
        <div data-role="header" align="center" class="ui-header ui-bar-a" style="width:100%;margin-bottom:8px;">
	        <asp:Label ID="lblpedidos" runat="server" Text="Selecciona Consignado" ForeColor="White" Font-Bold="true" ></asp:Label>
        </div>
        <table style="width: 100%;">
            <tr>
                <td width="4%">
                    
                    <asp:Label ID="Label2" runat="server" Text="Cliente:" Font-Bold="True" 
                        ForeColor="Black" Font-Names="Arial" Font-Size="Small"></asp:Label>
                    
                </td>
                <td>
                    
                                        <asp:TextBox ID="txtcliente" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    style="height:30px;" onfocus="this.blur();" Height="30px"></asp:TextBox>
                    
                </td>
            </tr>
            </table>
            <table style="width:100%;padding:0px;">
            <tr>
                <td style="padding:0px;">
                
                    <table style="width:100%;padding:0px;">
                        <tr>
                            <td style="padding:0px;">
                                <table style="width:100%;">
                                    <tr>
                                        <td width="4%" style="padding:0px;">
                    <asp:Label ID="Label1" runat="server" Text="RFC:" Font-Bold="True" 
                        ForeColor="Black" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="padding:0px;">
                                        <asp:TextBox ID="txtrfc" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    style="height:30px;" onfocus="this.blur();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%" style="padding:0px;">
                                <table style="width:100%;">
                                    <tr>
                                        <td width="4%" style="padding:0px;">
                    <asp:Label ID="Label3" runat="server" Text="Cve:" Font-Bold="True" 
                        ForeColor="Black" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="padding:0px;">
                                        <asp:TextBox ID="txtcve" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    style="height:30px;" onfocus="this.blur();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                
                </td>
            </tr>            
            <tr>
                <td>
                
                    <div>
    <div style="border-bottom: 1px solid gray; OVERFLOW: auto; overflow-x:hidden; WIDTH: 100%; HEIGHT: 250px; ">       
        
                            <asp:DataGrid ID="grdconsignados" runat="server" AutoGenerateColumns="False" 
                                Width="100%" Font-Names="Arial" OnItemDataBound="grdconsignados_ItemDataBound" >
                                <SelectedItemStyle BackColor="Yellow" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgseleccionar" runat="server" Height="25px" ImageUrl="~/imagenes/btn/icon_autorizar.png" Width="25px"/>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="calle" HeaderText="Calle">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="11px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"  Font-Size="10px"/>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="numero" HeaderText="Numero">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="11px"  />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="10px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="cod_postal" HeaderText="C.P.">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="11px"  />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="10px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="colonia" HeaderText="Colonia">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="11px"  />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="10px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="mpio" HeaderText="Municipio">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="11px"  />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"  Font-Size="10px"/>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="estado" HeaderText="Estado">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"  Font-Size="11px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="10px"/>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="pais" HeaderText="País">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="11px"  />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="10px"/>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="restriccion" HeaderText="Restriccion">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" CssClass="Ocultar" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="10px" CssClass="Ocultar"/>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="capacidad_maxima" HeaderText="Capacidad Maxima">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"  CssClass="Ocultar"/>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="10px" CssClass="Ocultar"/>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="toneladas" HeaderText="Toneladas">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"  CssClass="Ocultar"/>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"  Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Font-Size="10px" CssClass="Ocultar"/>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="zm" HeaderText="zm">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" CssClass="Ocultar" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" CssClass="Ocultar" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="idc_colonia" HeaderText="idc_colonia">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"  CssClass="Ocultar" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" CssClass="Ocultar" />
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle BackColor="Gray" Font-Bold="True" Font-Names="Arial" 
                Font-Overline="False" Font-Strikeout="False" Font-Underline="True" 
                ForeColor="White" Height="25px" Wrap="True" />
                            </asp:DataGrid>
        
        
        </div>
    
    </div>
    
        <div align="center">
        
        <asp:Button ID= "btncancelar" Text="Cancelar" runat="server" Font-Names="Arial" 
                ForeColor="Black" Font-Bold="True" Height="35px" 
                onclientclick="window.close();" 
                CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                Width="100%" />
        </div>
          
                
                
                
                
                </td>
            </tr>            
            </table>
    
    </div>
    </form>
</body>
</html>