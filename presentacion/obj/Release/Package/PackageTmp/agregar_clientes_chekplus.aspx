<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agregar_clientes_chekplus.aspx.cs" Inherits="presentacion.agregar_clientes_chekplus" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gama Materiales y Aceros</title>
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
   <%--/--------/--%>
    <style type="text/css">
                
        


   input[type="text"]{color:Black;}
   input[type="submit"]{font-weight:bold;}
   .Ocultar{display:none;}


        </style>
    
    <script type="text/javascript" language="javascript" >
    function guardar(nombre,clave,folio,calle,numero,idc_colonia)
    {
        if(nombre.value=="")
        {
            alert('El Nombre de la Persona que se Identifica no puede quedar vacio.');
            return false;        
        }
        else if(clave.value.length<18)
        {
            alert('La clave de Elector esta Incompleta');
            return false;
        }
        else if(folio.value.length<13)
        {
            alert('El Folio de Elector esta Incompleto');
            return false;        
        }
        else if(calle.value.length==0)
        {
            alert('La Calle esta Incompleta');
            return false;
        }
        else if(numero.value.length==0)
        {
            alert('El Numero esta Incompleto');
            return false;
        }
        else if(idc_colonia.value=="" || idc_colonia.value==0)
        {
            alert('Debes Seleccionar la Colonia');
            return false;            
        }
        document.getElementById('<%=btng.ClientID%>').click();    
        return false;
    }
    
    
    function soloNumeros2(e,tipo)
          {
             key = e.keyCode || e.which;
             tecla = String.fromCharCode(key).toLowerCase();
             if(tipo==true)
             {
                letras = "0123456789"; 
                especiales = [8,37,39,46,96,97,98,99,100,101,102,103,104,105,110,190];        
             }
             else
             {
                letras = "0123456789";
                especiales = [8,37,39,46,96,97,98,99,100,101,102,103,104,105];
             }
             tecla_especial = false
             for(var i in especiales)
             {
                     if(key == especiales[i])
                     {
                          tecla_especial = true;
                          break;
                     } 
              }
            var index = letras.indexOf(tecla);
            if(index!=-1 || tecla_especial==true)
            {
                return true;
            }   
            else
            {
                return false;            
            }   
            
         }
    
        function buscar_colonia()
            {
                  window.open('buscar_colonia_mobile.aspx?tipo=2');
                  //TINY.box.show({iframe:,boxid:'frameless',width:450,height:270,fixed:false,maskid:'bluemask',maskopacity:40})
                  return false;
            }
            function cerrar()
            {
                TINY.box.hide();
                return false;
            }
            function cerrar_refresh()
            {              
                window.opener.document.getElementById('Contenido_btnrefresh').click(); 
                window.close();       
            }
            function AlertGO(TextMess, URL) {
                swal({
                    title: "Mensaje del Sistema",
                    text: TextMess,
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonColor: "#428bca",
                    confirmButtonText: "Aceptar",
                    closeOnConfirm: false, allowEscapeKey: false
                },
                   function () {
                       window.opener.document.getElementById('Contenido_btnrefresh').click();
                       window.close();
                   });
            }
    </script>
    
</head>
<body>
    <form id="form1" runat="server" style="position: absolute; top: 0px; left: 0px;right: 0px;bottom:0px;">
    <div style="position: relative; top: 0px; left: 0px;right: 0px;bottom:0px;">
    <div data-role="header" align="center" class="ui-header ui-bar-a" style="width:100%;margin-bottom:8px;">
	        <asp:Label ID="lblheader" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="Small" ForeColor="White" Text="Agregar Datos de Check Plus"></asp:Label>
        </div>
    <table style="width: 100%; height: 77px;">
        <tr>
            <td style="height:7px">
                <table style="width:100%;">
                    <tr>
                        <td align="right" style="width: 5%;">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Cliente:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcliente" runat="server" Width="100%" onfocus="this.blur();" 
                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="RFC:"></asp:Label>
                        </td>
                        <td>
                                        <asp:TextBox ID="txtrfc" runat="server" Width="70%" onfocus="this.blur();" style="display:inline"
                                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                        <asp:TextBox ID="txtcve" runat="server" Width="25%" onfocus="this.blur();" style="display:inline"
                                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                                    </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Nombre de la Persona:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtnombre" runat="server" Width="100%" 
                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" 
                                            Font-Size="Small" ForeColor="Black" Text="Clave de Elector:"></asp:Label>
                        </td>
                        <td>
                                        <asp:TextBox ID="txtclave" runat="server" Width="100%" 
                                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial" 
                                            Font-Size="Small" ForeColor="Black" Text="Folio de Elector:" Width="105px"></asp:Label>
                        </td>
                        <td>
                                        <asp:TextBox ID="txtfolio" runat="server" Width="100%" type="number"
                                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height:7px">
                <asp:Label ID="Label5" runat="server" Text="Dirección" Font-Italic="True" 
                    Font-Names="arial" Font-Size="Small" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height:7px">
                <table style="width:100%;">
                    <tr>
                        <td align="right" style="width: 5%;">
                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Calle:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcalle" runat="server" Width="100%" Font-Names="arial" 
                                Font-Size="Small"  
                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Numero:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtnumero" runat="server" Width="100%" Font-Names="arial" 
                                Font-Size="Small" 
                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Colonia:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcolonia" runat="server" Width="75%" Font-Names="arial" style="display:inline"
                                Font-Size="Small" onfocus="this.blur();" 
                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                            <asp:ImageButton ID="imgbusc_colonia" runat="server" Height="22px" Width="22px" 
                                ImageUrl="imagenes/btn/icon_buscar.png" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Municipio:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtmun" runat="server" Width="100%" Font-Names="arial" 
                                Font-Size="Small" onfocus="this.blur();" 
                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Estado:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtedo" runat="server" Width="100%" Font-Names="arial" 
                                Font-Size="Small" onfocus="this.blur();" 
                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="Small" ForeColor="Black" Text="Pais:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpais" runat="server" Width="100%" Font-Names="arial" 
                                Font-Size="Small" onfocus="this.blur();" 
                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="width:100%">
                    <tr>
                        <td style="width:50%">
                
            <asp:Button ID="btnaceptar" runat="server" Text="Aceptar" Height="35px" Width="100%" 
                                CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" />
                        </td>
                        <td style="width:50%">
                <asp:Button ID="btncancelar" runat="server" Text="Cancelar" 
                    onclientclick="window.close();" Height="35px" Width="100%" 
                                CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" />
                        </td>
                    </tr>
                
                </table>
                
                <asp:Button ID="btng" runat="server" Text="g" CssClass="Ocultar"  OnClick="btnaceptar_Click" />
                <asp:TextBox ID="txtidc_colonia" runat="server" Width="80px" CssClass="Ocultar"></asp:TextBox>
                <asp:TextBox ID="txtidc_cliente" runat="server" Width="78px" CssClass="Ocultar"></asp:TextBox>
            </td>
        </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
