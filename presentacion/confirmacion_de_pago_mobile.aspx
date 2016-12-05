<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirmacion_de_pago_mobile.aspx.cs" Inherits="presentacion.confirmacion_de_pago_mobile" %>

<!DOCTYPE html>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirmación de Pago</title>
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
    
    
    
    <%--/--------/--%>
    
    
    <style type="text/css">
        #form1
        {
            margin-bottom: 6px;
        }
        </style>
        
    <script type="text/javascript">
        function ddl_focus(cbo) {
            cbo.style.color = "red";
            return false;
        } //VALIDA QUE SEA TIPO MONTO
        function validarMontoMoney(e) {
            k = (document.all) ? e.keyCode : e.which;
            if (k == 8 || k == 0) return true;
            patron = /[0-9.\s\t]/;
            n = String.fromCharCode(k);
            return patron.test(n);
        }

        function change_cbotipo(cbotipo) {
            var cbobancos = document.getElementById("cbobancos");
            var monto = document.getElementById('txtmonto');
            var fecha = document.getElementById('txtfecha');
            var observaciones = document.getElementById('txtobservaciones');
            if (cbotipo.selectedIndex == 0) {
                cbobancos.disabled = true;
                cbobancos.style.color = "#ddd";
                monto.value = "";
                monto.disabled = true;
                observaciones.value = "";

            }
            else {
                cbobancos.disabled = false;
                cbobancos.style.color = "black";
                monto.value = "";
                monto.disabled = false;
                observaciones.value = "";
            }
        }


        function cerrar() {
            //alert('entra');       
            //var x= document.getElementById('cbobancos').value;
            //alert(x);
            window.close();
            return false;
        }

        window.onload = function () {
            var datos = window.opener.document.getElementById('Contenido_txtformapago').value;
            var datos_split = datos.split("%");
            var observaciones = document.getElementById('txtobservaciones');
            if (datos.length > 0) {
                var cbotipopago = document.getElementById("cbotipopago");
                if (datos[0] == 1) {
                    cbotipopago.selectedIndex = 0;
                    observaciones.value = datos_split[4];
                    cbobancos.style.color = "#ddd";

                }
                else {
                    var cbobancos = document.getElementById("cbobancos");
                    var monto = document.getElementById('txtmonto');
                    var fecha = document.getElementById('txtfecha');
                    for (var i = 0; i <= cbobancos.options.length - 1; i++) {
                        if (cbobancos.options[i].value == datos_split[1]) {
                            cbobancos.selectedIndex = i;
                        }
                    }
                    cbotipopago.selectedIndex = 1;
                    cbobancos.style.color = "black";
                    monto.value = datos_split[2];
                    fecha.value = datos_split[3];
                    observaciones.value = datos_split[4];
                    cbobancos.disabled = false;
                }
            }
        }

        function pago(Forma, Banco) {
            if (Forma.value == 1) {
                var fecha = document.getElementById('txtfecha').value;
                var observaciones = document.getElementById('txtobservaciones').value;
                var monto = document.getElementById('txtmonto').value;
                var datos = Forma.value + '%' + 0 + '%' + 0 + '%' + fecha + '%' + observaciones;
                window.opener.document.getElementById('Contenido_txtformapago').value = datos;
                //window.opener.document.getElementById('Contenido_tbnguardarPP').click();
                window.close();
            }

            else {
                var fecha = document.getElementById('txtfecha').value;
                var observaciones = document.getElementById('txtobservaciones').value;
                var monto = document.getElementById('txtmonto').value;
                var datos = Forma.value + '%' + Banco.value + '%' + monto + '%' + fecha + '%' + observaciones;
                if (Banco < 0) {
                    alert('Debes Seleccionar el Banco');
                    return false;
                }
                if (monto == 0 || monto == '') {
                    document.getElementById('txtmonto').value = '';
                    alert('El Monto No Puede ser Cero o Quedar Vacio.');
                    return false;
                }

                if (validaFloat(monto) == false) {
                    return false;
                }
                if (fecha == '') {
                    alert('Ingresa la Fecha');
                    return false;
                }
                window.opener.document.getElementById('Contenido_txtformapago').value = datos;
                //window.opener.document.getElementById('Contenido_tbnguardarPP').click();
                window.close();
            }

        }


        function validaFloat(numero) {
            if (!/^([0-9])*[.]?[0-9]*$/.test(numero)) {
                alert("El Monto no es valido.");
                return false;
            }
            else {
                return true;
            }
        }

        function validarnumero(txt) {
            if (isNaN(txt.value) != false) {
                alert("El Monto no es Correcto.");
                txt.focus();
                txt.select();
                return false;
            }
        }


    </script>
        
        
</head>
<body>
    <form id="form1" runat="server" 
    style="position: absolute; width: 99%; left: 2px; top: 2px">
   
    <div>
        <div data-role="header" align="center" class="ui-header ui-bar-a" style="width:100%;margin-bottom:8px;">
	        <asp:Label ID="lblhead" runat="server" Text="Confirmación de Pago" 
                ForeColor="White" Font-Bold="True" ></asp:Label>
        </div>
        <table style="width: 100%;">
            <tr>
                <td align="right" width="8%">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" 
                    Font-Names="Arial Narrow" ForeColor="Black" Text="Tipo de Pago:"></asp:Label>
                </td>
                <td>
                <div id="select" class="styled-select" style="width:100%;">
                    <asp:DropDownList ID="cbotipopago" runat="server" Font-Bold="True" CssClass="dropdownlist"
                        Font-Names="Arial Narrow" Height="30px" Width="105%">
                        <asp:ListItem Value="1">Efectivo</asp:ListItem>
                        <asp:ListItem Value="2">Deposito</asp:ListItem>
                    </asp:DropDownList>                
                </div>

                </td>
            </tr>
            <tr>
                <td align="right">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" 
                    Font-Names="Arial Narrow" Text="Banco:"></asp:Label>
                </td>
                <td>
                <div id="Div1" class="styled-select" style="width:100%;">  
                        <span style="background:red; position:relative;right:0px;top:5px;"></span>              
                        <asp:DropDownList ID="cbobancos" runat="server" Font-Bold="True" CssClass="dropdownlist"
                            Font-Names="Arial Narrow" Height="30px" Width="105%" ForeColor="#DFDFDF">
                        </asp:DropDownList>                
                </div>
                
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                        Font-Names="Arial Narrow" Text="Monto"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmonto" TextMode="Number" onkeypress="return validarMontoMoney(event);" onfocus="this.select()"
                        runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td align="right">
                <asp:Label ID="Label5" runat="server" Font-Bold="True" 
                    Font-Names="Arial Narrow" Text="Fecha:"></asp:Label>
                </td>
                <td>
                                        <asp:TextBox ID="txtfecha" runat="server" Width="100%" Enabled="true" TextMode="DateTimeLocal"  CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                <asp:Label ID="Label6" runat="server" Font-Bold="True" 
                    Font-Names="Arial Narrow" Text="Observaciones: "></asp:Label>
                </td>
                <td>
                                        <asp:TextBox ID="txtobservaciones" runat="server" Width="100%" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    style="height:50px;resize:none;" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td width="50%">
                
                                        <asp:Button ID="btnaceptar" runat="server" CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" Text="Aceptar"  Width="100%" Height="32px"  style="margin:0px;font-size:small;"/>
                                
                </td>
                <td width="width:50%">
                
                                        <asp:Button ID="btncancelar" runat="server" CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" Text="Cancelar"  Width="100%" Height="32px" style="margin:0px;font-size:small;" onclientclick="window.close();"/>
                                
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>

