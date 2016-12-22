<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editar_precios_cantidad.aspx.cs" Inherits="presentacion.editar_precios_cantidad" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editar</title>
   
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
    <%----/-----------/----%>
    <style type="text/css">
        input[type="text"]{padding:0px !important;}
        label{color:Black;font-size:small;font-family:Arial;font-weight:bold;} 
        #txtcodigoarticulo{border-radius:10px 10px 10px 10px;padding:0px 0px 0px 4px !important;border-left-style:none;width:100%;}
        #Label{padding:0px;margin:0px;border-style:solid;border-width:2px;}
        #text{color:Black;text-align:center;font-size:10pt;font-family:Arial;line-height:0px;}
        .Ocultar{display:none;}
        #lblnc{font-size:12px;font-family:Arial;}
    </style>
    <script type="text/javascript">
        function YaExiste() {
            swal({
                title: "Mensaje del Sistema",
                text: "El Articulo Seleccionado Ya se Encuentra En la Lista",
                type: "info",
                allowEscapeKey: false
            },
            function () {
                window.close();
            });
        }
        $(document).ready(function () {
            $('#txtprecio').on('focus', function () {
                var precioreal = $('#txtprecioreal').val();
                var precio = $('#txtprecio').val();
                if (parseFloat(precioreal) == parseFloat(precio)) {
                    $('#txtprecio').attr('iq', true);
                }
                else {
                    $('#txtprecio').attr('iq', false);
                }
            });
        });

        function aplicar_sugerida() {
            var txtaplicar = document.getElementById('txtaplicar');
            var txtsugerida = document.getElementById('txtsugerida');
            var txtcantidad = document.getElementById('txtcantidad');
            if (txtaplicar.value == 'true') {
                txtcantidad.value = txtsugerida.value;
                txtaplicar.value = '';
                txtsugerida.value = '';
                //precio_cantidad(); Validar Q Haga lo Correctoo....
                precio_cantidad();
            }

        }
        function cant_sugerida(entero, sugerida, cant, factor) {
            var conf = confirm('Sugerida: ' + sugerida + '\nPiezas Cantidad Sugerida: ' + entero + '\nPiezas x Tonelada: ' + factor + '\nPeso x Pieza: ' + cant + ' KG\n \u000B \n¿Deseas Aplicar la Cantidad Sugerida?');
            //var conf = confirm('Cantidad de Piezas: ' + entero + '\nSugerida: ' + sugerida + '\n \u000B \n¿Deseas Aplicar la Cantidad Sugerida?');
            var txtaplicar = document.getElementById('txtaplicar');
            var txtsugerida = document.getElementById('txtsugerida');
            txtaplicar.value = conf;
            txtsugerida.value = sugerida;
        }

        function pruebas_param() {
            alert('Actualizando Precios');
            return false;
        }



        function precio_cantidad2(e) {
            var txtcantidad = document.getElementById("<%=txtcantidad.ClientID%>");
                if (txtcantidad.value != "" && txtcantidad.value > 0) {
                    document.getElementById("<%=btnbuscarprecio.ClientID%>").click();
                }
                else {
                    return false;
                }

            }

            function precio_cantidad() {
                var txtcantidad = document.getElementById("<%=txtcantidad.ClientID%>");
                if (txtcantidad.value != "" && txtcantidad.value > 0) {
                    //document.getElementById("<%=btnbuscarprecio.ClientID%>").click();        
                }
                else {
                    return false;
                }

            }
            
            function movio_precios() {
                var txtmod = document.getElementById('txtmod');
                var precio = document.getElementById('txtprecio');
                var precior = document.getElementById('txtprecioreal');
                var txtpreciominimo = document.getElementById('txtpreciominimo');
                var z = txtpreciominimo.value.replace(",", "");
                var x = precio.value.replace(",", "");
                var y = precior.value.replace(",", "");
                if (txtmod.value == '0') {
                    return 0;
                }
                else {
                    if (parseFloat(z) > parseFloat(x)) {
                        return 1;
                    }
                    else if (parseFloat(z) > parseFloat(y)) {
                        return 2;
                    }
                    else {
                        return 0;
                    }

                }
            }

            function validarPrecio() {
                var txtmod = document.getElementById('txtmod');
                var precio = document.getElementById('txtprecio');
                precio.value = decimales(precio.value, 4);
                var precior = document.getElementById('txtprecioreal');
                var txtult_pre = document.getElementById('txtult_pre');
                var txtpreciominimo = document.getElementById('txtpreciominimo');
                var z = txtpreciominimo.value.replace(",", "");
                var x = precio.value.replace(",", "");
                var y = precior.value.replace(",", "");

                if (x == '') {
                    x = 0;
                }
                if (y == '') {
                    y = 0;
                }
                if (x == 0) {
                    alert("El Precio no Puede ser igual a Cero.");
                    precio.select();
                    return false;
                }
                else {
                    if (parseFloat(z) > parseFloat(x)) {
                        alert("El Precio Debe Ser Mayor o Igual al Precio Minimo");
                        precio.value = txtult_pre.value;
                        precior.value = txtult_pre.value;
                        return false;
                    }
                    else {
                        txtult_pre.value = precio.value;
                    }

                    //if(parseFloat(z) > parseFloat(y))
                    //{
                    //alert("El Precio Real Debe Ser Mayor al Precio Minimo");
                    if ($(precio).attr('iq') == 'true') {
                        precior.value = precio.value;
                    }
                    //return false;
                    //}



                    if (parseInt(x) == parseInt(y)) {
                        return false;
                    }
                    var bo = (parseInt(x) > parseInt(y));
                    if (bo == false) {
                        //alert("El Precio no Puede ser Menor al Precio Real");
                        precior.value = precio.value;
                        return false;
                    }
                    else {
                        //precior.value =  precio.value;
                    }
                    txtmod.value = '1';
                }

            }

            function validarPrecior() {
                var txtmod = document.getElementById('txtmod');
                var precio = document.getElementById('txtprecio');
                var precior = document.getElementById('txtprecioreal');
                precior.value = decimales(precior.value, 4);
                var txtpreciominimo = document.getElementById('txtpreciominimo');
                var z = txtpreciominimo.value.replace(",", "");
                var x = precio.value.replace(",", "");
                var y = precior.value.replace(",", "");
                var txtult_pre = document.getElementById('txtult_pre');


                if (x == '') {
                    x = 0;
                }
                if (y == '') {
                    y = 0;
                }

                if (parseFloat(z) > parseFloat(x)) {
                    alert("El Precio Debe Ser Mayor o Igual al Precio Minimo");
                    precio.value = txtult_pre.value;
                    precior.value = txtult_pre.value;
                    return false;
                }

                if (parseFloat(z) > parseFloat(y)) {
                    alert("El Precio Debe Ser Mayor o Igual al Precio Minimo");
                    precior.value = precio.value;
                    return false;
                }
                txtmod.value = 1;
                txtult_pre.value = precio.value;

                if (parseInt(y) > parseInt(x)) {
                    alert("El Precio Real no Puede ser Mayor al Precio del Cliente");
                    precior.value = precio.value;
                    return false;
                }

            }


            function validar_campos_vacios(txtprecio, txtcantidad, real) {
                //            var txtpreciominimo = document.getElementById('txtpreciominimo');
                //            if(txtpreciominimo.value>txtprecio.value)
                //            {
                //                alert('El Precio Debe ser Mayor al Precio Minimo.')
                //                return false;
                //            }
                if (txtprecio.value == "" || txtprecio.value <= 0) {
                    alert("El Precio Debe ser Mayor a 0.");
                    return false;
                }
                else if (txtcantidad.value == "" || txtcantidad.value <= 0) {
                    alert("La Cantidad Debe ser Mayor a 0.");
                    return false;
                }
                if (real == 1) {
                    var txtprecioreal = document.getElementById("txtprecioreal");
                    if (txtprecioreal.value == "" || txtprecioreal.value <= 0) {
                        alert("El Precio Real Debe ser Mayor a 0.");
                        return false;
                    }
                }
                var mov = movio_precios();
                if (mov == 1) {
                    alert('El Precio Debe ser Mayor o Igual al Precio Minimo.');
                    return false;
                }
                else if (mov == 2) {
                    alert('El Precio Real Debe ser Mayor o Igual al Precio Minimo.');
                    return false;
                }
                document.getElementById("btnguardar").click;
            }



            function cancelar() {
                window.opener.document.getElementById('Contenido_btncancelar_edit').click();
                window.close();
            }

            function cerrar_guardar_edit() {
                window.opener.document.getElementById('Contenido_btnguardar_edit').click();
                window.close();
            }
            function refrescar() {
                window.opener.document.getElementById('Contenido_btnref').click();
                window.close();
            }
            //VALIDA QUE SEA TIPO MONTO
            function validarMontoMoney(e) {
                k = (document.all) ? e.keyCode : e.which;
                if (k == 8 || k == 0) return true;
                patron = /[0-9.\s\t]/;
                n = String.fromCharCode(k);
                return patron.test(n);
            }

    </script>
    <script type="text/javascript">
        function movio_precios_minimo() {
            var txtmod = document.getElementById('txtmod');
            var precio = document.getElementById('txtprecio');
            var precior = document.getElementById('txtprecioreal');
            var txtpreciominimo = document.getElementById('txtpreciominimo');
            var z = txtpreciominimo.value.replace(",", "");
            var x = precio.value.replace(",", "");
            var y = precior.value.replace(",", "");
            if (y < z) {
                alert("EL PRECIO REAL NO PUEDE SER MENOR AL PRECIO MINIMO");
                $("#<%=txtprecioreal.ClientID%>").val(z);
                return false;
            } else if (x < z) {
                alert("EL PRECIO NO PUEDE SER MENOR AL PRECIO MINIMO");
                $("#<%=txtprecio.ClientID%>").val(z);
                return false;
            } else {
                return true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%" defaultbutton="def_b">
    <div style="width:100%;">
                <div  align="center" class="ui-header ui-bar-a" style="width:100%;margin-bottom:4px !important; top:0px; left:0px;">
                    <asp:Label ID="lblpedidos" runat="server" Text="Editar" ForeColor="White" 
                    Font-Bold="true" ></asp:Label>
                </div>
                <div>
                    <table style="width: 100%;">
                        <tr>
                            <td align="right" width="5%">
                                <label>
                                Codigo
                                <br />
                                Articulo:</label></td>
                            <td>
                                <asp:TextBox ID="txtcodigoarticulo" runat="server" 
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                                    onfocus="this.blur();" style="height:30px;" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                Descripcion:</label></td>
                            <td>
                                <asp:TextBox ID="txtdescripcion" runat="server" 
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                                    onfocus="this.blur();" style="height:30px;" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>
                                UM:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtum" runat="server" 
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                                    onfocus="this.blur();" style="height:30px;" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>
                                Cantidad:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcantidad"  onkeypress="return validarMontoMoney(event);" onfocus="this.select()" runat="server"  type="number" 
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                                    style="height:30px;" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            
                                <label ID="lblreal0" runat="server">
                                Precio Minimo:</label></td>
                            <td>
                                <asp:TextBox ID="txtpreciominimo"  onkeypress="return validarMontoMoney(event);"  runat="server" onfocus="this.blur();"
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                                    style="height:30px;" Text="0" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>
                                Precio:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtprecio" onfocus="this.select()" onblur="return movio_precios_minimo();" runat="server"  onkeypress="return validarMontoMoney(event);"  type="number" min="0.0001" step="0.0001"
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                                    style="height:30px;display:inline; text-align: left;" Width="80%"></asp:TextBox>
                                    <asp:ImageButton ID="imgultimo" runat="server" Height="30px" Width="30px" ImageUrl="imagenes/calendar3.gif" 
                                        style="position:relative;top:10px;right:0px"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="color:Red;">
                                <label ID="lblreal" runat="server" style="color:Red;">
                                Precio Real:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtprecioreal" onfocus="this.select()" onblur="return movio_precios_minimo();" runat="server"  onkeypress="return validarMontoMoney(event);"  type="number" step="0.0001" min="0.0001"
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" 
                                    style="height:30px; text-align: left;" Text="0" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            
                            </td>
                            <td>
                            <asp:Button id="def_b" runat="server" CssClass="Ocultar" />
                                <asp:Button ID="btnbuscarprecio" OnClick="btnbuscarprecio_Click" runat="server" CssClass="Ocultar" Text="buscar" />
                                <asp:Button ID="btnguardar" runat="server" CssClass="Ocultar" Text="guardar" />
                                <asp:Label ID="lblroja" runat="server" CssClass="Ocultar" Text="" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtrfc" runat="server" CssClass="Ocultar" Text="" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtid" runat="server" CssClass="Ocultar" Text="" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtlistap" runat="server" CssClass="Ocultar" Text="" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtcol" runat="server" CssClass="Ocultar" Text="" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtaplicar" runat="server" CssClass="Ocultar" Text="" ></asp:TextBox>    
                                <asp:TextBox ID="txtsugerida" runat="server" CssClass="Ocultar" Text="" ></asp:TextBox>                                                              
                                <asp:TextBox ID="txtult_pre" runat="server" CssClass="Ocultar" Text="" ></asp:TextBox> 
                                <asp:TextBox ID="txtmod" runat="server" CssClass="Ocultar" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="color:Red;padding-left:10px;font-family:Arial;">
                          Nota de Credito
                    </div>
                    <div>
                        <asp:Label id="lblnc"  Visible="false"  runat="server" Text="Precio de Cliente con Nota de Crédito Automatica.<br> Precio No se Permite Editar." ForeColor="Red"></asp:Label>
                    
                    </div>
                    <div style="width:100%">
                        <table style="width: 100%;">
                            <tr>
                                <td align="center" width="50%">
                                    <asp:Button ID="btnaceptar" runat="server" 
                                        CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                        Height="32px" style="margin:0px; top: -1px; left: 0px;" Text="Aceptar" 
                                        Width="100%" OnClick="btnaceptar_Click" />
                                </td>
                                <td align="center" width="50%">
                                    <asp:Button ID="btncancelar" runat="server" 
                                        CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" 
                                        Height="32px" onclientclick="return cancelar();" 
                                        style="margin:0px; top: -1px; left: 0px;" Text="Cancelar" Width="100%" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
    </div>
    </form>
</body>
</html>
