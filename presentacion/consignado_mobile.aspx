<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="consignado_mobile.aspx.cs" Inherits="presentacion.consignado_mobile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>Consignado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width" />
    <style type="text/css">
        #form1
        {
            top: 2px;
            left: 2px;
            height: 504px;
        }
       input[type="checkbox"] {
          display:none;
        }

        input[type="checkbox"] + label {
          padding:20px; /*or however wide your graphic is*/
          background:url(CheckBox/chk_blue_false.png) no-repeat left center;
        }

        input[type="checkbox"]:checked + label {
          background:url(CheckBox/chk_blue_true.png) no-repeat left center;
        }
        input[type="text"]{margin:0px !important;}
        input[type="text"]{padding:0px !important;font-family:Arial !important;font-size:10pt !important;padding-top:0px !important;}
        .Ocultar{display:none;}
        #lblrestriccion{border-style:none;background-color:Transparent;font-size:7pt;font-family:Arial;}
    </style>
    <link href="css/jquery.mobile-1.2.0.min.css" rel="stylesheet" /> 
    <link href="js/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
    <link href="TinyBox/style.css" rel="stylesheet" />
    <script src="TinyBox/tinybox.js"></script>
    <link href="js/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
 <script type="text/javascript">
        $(function() {
            $( "#tabs" ).tabs();
        });  
        window.onload=(function(){$( "#tabs" ).tabs();}); 
        
        function cerrar()
        {
            TINY.box.hide();
            return false;
        
        }
        
        
        function colonia()
        {
             var ruta="buscar_colonia_mobile.aspx?tipo=1";
             window.open(ruta);
             return false;    
        }
        
        
        function cargar_datos()
        {
        
            document.getElementById("txtidc_colonia").value = window.opener.document.getElementById("Contenido_txtidc_colonia").value;
               var zm  = window.opener.document.getElementById("Contenido_txtzm").value;
               document.getElementById("txtzm").value=window.opener.document.getElementById("Contenido_txtzm").value;
               document.getElementById("lblrestriccion").value=window.opener.document.getElementById("Contenido_txtrestriccion").value;
               document.getElementById("txtcalle").value=window.opener.document.getElementById("Contenido_txtcalle").value;
               document.getElementById("txtnumero").value = window.opener.document.getElementById("Contenido_txtnumero").value;
               document.getElementById("txtCP").value = window.opener.document.getElementById("Contenido_txtCP").value;
               document.getElementById("txtcolonia").value = window.opener.document.getElementById("Contenido_txtcolonia").value;
               document.getElementById("txtestado").value = window.opener.document.getElementById("Contenido_txtestado").value;
               document.getElementById("txtmunicipio").value = window.opener.document.getElementById("Contenido_txtmunicipio").value;
               document.getElementById("txtpais").value = window.opener.document.getElementById("Contenido_txtpais").value;
               document.getElementById("txttoneladas").value = window.opener.document.getElementById("Contenido_txttoneladas").value;
               document.getElementById("chkton").checked = window.opener.document.getElementById("Contenido_chkton").checked;
               document.getElementById("txtproy").value = window.opener.document.getElementById("Contenido_txtproy").value;
               if (document.getElementById("txtproy").value !="" && document.getElementById("txtproy").value >0)
               {
                    document.getElementById("txtcalle").setAttribute("onfocus", "this.blur();");
                    document.getElementById("txtnumero").setAttribute("onfocus", "this.blur();");
                    document.getElementById("txtCP").setAttribute("onfocus", "this.blur();");
                    var value =document.getElementById("txtproy").value; 
                    var cbo =  document.getElementById("cboproyectos");
                    for (i=0;i<cbo.length;i++)
                    {
                        if (value == cbo.options[i].value)
                        {
                            document.getElementById("cboproyectos").options[i].selected = true;                        
                        }
                    }                    
               }
               
                   if(document.getElementById("txtzm").value=="true")
                   {
                        document.getElementById("chkzm").checked=true;           
                   }
                   else
                   {
                        document.getElementById("chkzm").checked=false;
                   }
               
               
               return false;     
        }


    
        function regresar_datos()
        {
            window.opener.document.getElementById("Contenido_txtidc_colonia").value = document.getElementById("txtidc_colonia").value;
           var zm = document.getElementById("chkzm").checked;
           window.opener.document.getElementById("Contenido_txtzm").value = zm;
           window.opener.document.getElementById("Contenido_txtrestriccion").value = document.getElementById("lblrestriccion").value;
           window.opener.document.getElementById("Contenido_txtcalle").value = document.getElementById("txtcalle").value;
           window.opener.document.getElementById("Contenido_txtnumero").value = document.getElementById("txtnumero").value;
           window.opener.document.getElementById("Contenido_txtCP").value = document.getElementById("txtCP").value;
           window.opener.document.getElementById("Contenido_txtcolonia").value = document.getElementById("txtcolonia").value;
           window.opener.document.getElementById("Contenido_txtestado").value = document.getElementById("txtestado").value;
           window.opener.document.getElementById("Contenido_txtmunicipio").value = document.getElementById("txtmunicipio").value;
           window.opener.document.getElementById("Contenido_txtpais").value = document.getElementById("txtpais").value;
           window.opener.document.getElementById("Contenido_txttoneladas").value = document.getElementById("txttoneladas").value;
           window.opener.document.getElementById("Contenido_chkton").checked = document.getElementById("chkton").checked;
           window.opener.document.getElementById("Contenido_txtproy").value = document.getElementById("txtproy").value;
           if(document.getElementById("ref").value==1)
           {
                window.opener.document.getElementById("Contenido_btnmaniobras").click();
           }
           window.close();
           return false;      
        }
        function AbreConsignados()
        {
            if (document.getElementById("<%=txtid.ClientID%>").value=='')
            {
                    alert("No Existe Cliente");
                    return false;
            }
            else
            {
                    var encodedString = btoa(document.getElementById("<%=txtid.ClientID%>").value);
                    var ruta = 'Historial_Consignados_mobile.aspx?id=' + encodedString;
                    window.open(ruta);
            }
        }
        
        function pulsar(e) 
        {
	        tecla=(document.all) ? e.keyCode : e.which;
            if(tecla==13) return false;
        }
        
   </script>
    
    
</head>
<body onkeypress="return pulsar(event);">
    <form id="form1" runat="server" style="position: absolute; top: 2px; left: 2px; width: 99%;margin-bottom: 4px;">
    <div>
    
        <div data-role="header" align="center" class="ui-header ui-bar-a" style="width:100%">
	        <asp:Label ID="lblpedidos" runat="server" Text="Consignado" ForeColor="White" Font-Bold="true" ></asp:Label>
        </div>
      <div id="tabs" style="padding:0px !important;">
      
      
      
              <ul>
                <li><a href="#tabs-1" style="color: navy">Consignado</a></li>
                <li><a href="#tabs-2" style="color: navy">Proyectos</a></li>
            </ul>
        <div id="tabs-1" style="width:100%;padding:0px !important;">
            <table style="width:100%;">
                <tr>
                    <td align="right" width="80">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="Small" Text="Calle:"></asp:Label>
                                    </td>
                    <td>
                                        <asp:TextBox ID="txtcalle" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                     style="height:50px;padding:0px;font-size:"></asp:TextBox>
                                    </td>
                </tr>
                <tr>
                    <td align="right">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="Small" Text="Numero:"></asp:Label>
                                    </td>
                    <td>
                                        <asp:TextBox ID="txtnumero" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    style="height:25px;"></asp:TextBox>
                                    </td>
                </tr>
                <tr>
                    <td align="right">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="Small" Text="CP:"></asp:Label>
                                    </td>
                    <td>
                                        <asp:TextBox ID="txtCP" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                     style="height:25px;"></asp:TextBox>
                                    </td>
                </tr>
                <tr>
                    <td align="right">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="Small" Text="Colonia:"></asp:Label>
                                    </td>
                    <td>
                        <table style="width:100%;">
                            <tr>
                                <td>
                                        <asp:TextBox ID="txtcolonia" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    onfocus="this.blur();" style="height:25px;"></asp:TextBox>
                                    </td>
                                <td width="20px">
                                                    <asp:ImageButton ID="btnbuscarcol" runat="server" Height="21px" 
                                                                    ImageUrl="~/imagenes/btn/icon_buscar.png" onclientclick="return colonia();" 
                                                                    style="margin-left: 0px" Width="23px" />
                                </td>
                                <td>
                                                <asp:TextBox ID="lblrestriccion" runat="server" Width="100%" Text="Acceso Total" onfocus="this.blur();"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        </td>
                              <td align="left">
                                  <asp:CheckBox ID="chkzm" runat="server" Font-Bold="True" Font-Names="arial"  CssClass=" ui-checkbox"
                                      Font-Size="Small" Text="Zona Metropolitana" onclick="return false;" />
                              </td>
                          </tr>
                          <tr>
                              <td align="right">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="Small" Text="Municipio:"></asp:Label>
                                    </td>
                              <td>
                                        <asp:TextBox ID="txtmunicipio" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    onfocus="this.blur();" style="height:25px;"></asp:TextBox>
                                    </td>
                          </tr>
                          <tr>
                              <td align="right">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="Small" Text="Estado:"></asp:Label>
                                    </td>
                              <td>
                                        <asp:TextBox ID="txtestado" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    onfocus="this.blur();" style="height:25px;"></asp:TextBox>
                                    </td>
                          </tr>
                          <tr>
                              <td align="right">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="Small" Text="Pais:"></asp:Label>
                                    </td>
                              <td>
                                        <asp:TextBox ID="txtpais" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    onfocus="this.blur();" style="height:25px;"></asp:TextBox>
                                    </td>
                          </tr>
                          <tr>
                              <td align="right">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt" Text="Restricciones de Colonia:" Width="82px"></asp:Label>
                                    </td>
                              <td>
                                  <asp:CheckBox ID="chkton" runat="server" Font-Bold="True" Font-Names="arial" 
                                      Font-Size="Small" Text="Capacidad Maxima" onclick="return false;" />
                              </td>
                          </tr>
                          <tr>
                              <td align="right">
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="Small" Text="Toneladas:"></asp:Label>
                                    </td>
                              <td>
                                        <asp:TextBox ID="txttoneladas" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    onfocus="this.blur();" style="height:25px;"></asp:TextBox>
                              </td>
                          </tr>
                      </table>
                      <table style="width:100%">
                          <tr>
                                <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:50%;">
                                                <asp:Button ID="btnDDF" runat="server" 
                                                CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                                 Text="Direccion Fiscal" OnClick="btnDDF_Click"
                                                Width="100%" Height="32px" style="margin:0px;font-size:small;" />
                                        </td>
                                        <td style="width:50%;">
                                                <asp:Button ID="btnHC" runat="server" 
                                                CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                                 Text="Historial" 
                                                Width="100%" Height="32px" style="margin:0px;font-size:small;" 
                                                onclientclick="return AbreConsignados();" />
                                                
                                        </td>
                                    </tr>
                                
                                </table>

                                
                                </td>
                          </tr>
                          <tr>
                                <td style="padding:0px;">
                                
                                        <asp:Button ID="btnQC" runat="server" 
                                            CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                             Text="Quitar Consignado"  OnClick="btnQC_Click"
                                            Width="100%" Height="32px"  style="margin:0px;font-size:small;"/>
                                
                                </td>
                          </tr>
                      
                      </table>
        </div>
        <div id="tabs-2" style="padding:0px;height:409px;">
            <table style="width:100%;">
                <tr>
                    <td>
                        <asp:DropDownList ID="cboproyectos" runat="server" Width="100%" 
                            AutoPostBack="True" OnSelectedIndexChanged="cboproyectos_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td width="20px">
                        <asp:ImageButton 
                                        ID="imgcroquis" runat="server" Height="35px" 
                                            ImageUrl="~/imagenes/maps.jpg" Width="35px" />
                    </td>
                </tr>
            </table>
        </div>   
      </div>
             <div style="width:100%">
            <asp:Label ID="lblcap" runat="server" Text="Ya no Es Posible Modificar el Consignado." style="color:Red;font-family:Arial;font-size:small;font-weight:bold;"></asp:Label>
      </div>
     
      <div style="padding-top:5px;">
              
              <table style="width:100%">
                  <tr>
                        <td>
                        
                                    <asp:Button ID="btnaceptar" runat="server" 
                                    CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                     Text="Aceptar" 
                                    Width="100%" Height="32px"  style="margin:0px;"/>
                        </td>
                        <td>
                                    <asp:Button ID="btncancelar" runat="server" 
                                    CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                     Text="Cancelar" 
                                    Width="100%" Height="32px"  style="margin:0px;" 
                                        onclientclick="window.close();"/> 
                        </td>              
                  </tr>              
              </table>                
                      
       </div>
        
       <div class="Ocultar">
           <asp:TextBox ID="txtidc_colonia" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txtid" runat="server" Text = ""></asp:TextBox>
            <asp:TextBox ID="txtproy" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txtzm" runat="server" Text=""></asp:TextBox>
            <asp:TextBox ID="ref" runat="server" Text="0"></asp:TextBox>
       </div>
       
      
    
    </div>
    </form>
</body>
</html>