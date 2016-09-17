<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="Carga_de_Combustibles_m.aspx.cs" Inherits="presentacion.Carga_de_Combustibles_m" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .fc {
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
    </style>
    <script language="JavaScript" type="text/javascript">

        function pageLoad(sender, args) {
        }
    </script>

    <script type="text/javascript" language="javascript">
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
        function ModalConfirmf(cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalf').modal('show');
            $('#content_modalf').text(cContenido);
        }
        function ModalClose() {
            $('#myModal').modal('hide');
            $('#myModalf').modal('hide');
        }
        function buscar_veh(e) {

            var key = (document.all) ? e.keyCode : e.which;
            var btnb_veh = document.getElementById("<%=btnb_veh.ClientID%>");
        if (key == 13) {
            btnb_veh.click();
        }
    }

    function buscar_emp(e) {

        var key = (document.all) ? e.keyCode : e.which;
        var btnb_emp = document.getElementById("<%=btnb_emp.ClientID%>");
        if (key == 13) {
            btnb_emp.click();
        }
    }

    function visible_ob() {
        var chk = document.getElementById('<%=chkreportebc.ClientID%>');
        var Label = document.getElementById('<%=lblobservaciones.ClientID%>');
        var TextBox = document.getElementById('<%=txtobservaciones.ClientID%>');
        if (chk.checked == true) {
            Label.style.display = '';
            TextBox.style.display = '';
            return true;
        }
        else {
            Label.style.display = 'none';
            TextBox.style.display = 'none';
            return true;
        }
    }

    function mpeSeleccionOnCancel() {

    }
    function validarcantidad(caja) {
        if (caja.value < 1 || caja.value > 9999) {
            alert("La Cantidad de Litros no es Correcta.");
            caja.select();
            return false;

        }

    }

    function x(evt) //Deshabilitar el Enter TextBox
    {
        return (evt ? evt.which : event.keyCode) != 13;
    }

    function clickb(e, boton) {
        var key = (document.all) ? e.keyCode : e.which;
        if (key == 13) {
            boton.click();
            return false;
        }

    }

    function calculo() {
        try {
            var numValue = parseFloat($('#<%=txtkmactual.ClientID%>').val());
                min = parseFloat(0);
                max = parseFloat(2147483647);
                if (numValue < min || numValue > max) {

                    numValue.value = "0";
                    $('#<%=txtkmactual.ClientID%>').val(numValue);
                    swal({
                        title: "Mensaje del Sistema",
                        text: "El valor de Kilometros Actuales \nNo puede pasar de " + max + " kilometros.",
                        type: 'error',
                        showCancelButton: false,
                        confirmButtonColor: "#428bca",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false, allowEscapeKey: false
                    });
                    $('#<%=txtkmactual.ClientID%>').focus();
                }
                var kmactual = document.getElementById('<%=txtkmactual.ClientID%>').value;
                var kmanterior = document.getElementById('<%=txtkmanterior.ClientID%>').value;
                var litros = document.getElementById('<%=txtcantidadlitros.ClientID%>').value;
                var txtrn = document.getElementById('<%=txtrn.ClientID%>');
                var rendimiento = (kmactual - kmanterior) / litros;
                txtrn.value = rendimiento.toFixed(2);
                var distancia = document.getElementById('<%=txtdistancia.ClientID%>');
                var rendimientoanterior = document.getElementById('<%=txtrendimientoanterior.ClientID%>').value;
                distancia.value = kmactual - kmanterior;
                var bajo = document.getElementById("<%=lblbajor.ClientID%>").style;
                var alto = document.getElementById("<%=lblaltor.ClientID%>").style;
                var numero = new Number(txtrn.value);

                if (numero < rendimientoanterior) {
                    bajo.display = "";
                    alto.display = "none";

                }
                else {

                    bajo.display = "none";
                    alto.display = "";
                }

            }
            catch (err) {
                alert(err);
            }
            return false;
        }

        function calculoPc() {
            var combuPc = document.getElementById("<%=txtcombustibleutilizado.ClientID%>").value;
       var DistanciaPc = document.getElementById("<%=txtdistanciarecorrida.ClientID%>").value;
       var rendimientopc = document.getElementById("<%=txtrendimientopc.ClientID%>");
       var litros = document.getElementById("<%=txtcantidadlitros.ClientID%>").value;
       var faltante = document.getElementById("<%=txtfaltante.ClientID%>");
       var comburelenti = document.getElementById("<%=txtutilizadorelenti.ClientID%>").value;

       if (combuPc == "") {
           combuPc = 0;
       }

       if (DistanciaPc == "") {
           DistanciaPc = 0;
       }

       if (litros == 0) {

           litros = 0;
       }

       if (comburelenti == "") {
           comburelenti = 0;
       }

       if (DistanciaPc == 0 || combuPc == 0) {
           rendimientopc.value = "";
       }
       else {
           rendimientopc.value = (DistanciaPc / combuPc).toFixed(2);
       }

       if (combuPc > 0 || comburelenti > 0) {
           var resul = litros - (parseFloat(combuPc) + parseFloat(comburelenti));
           faltante.value = resul.toFixed(2);

       }
       return false;

   }

   function seleccionar(caja) {
       caja.select();
       return false;
   }

   function alerta() {
       var x = document.getElementById("<%=txtcantidadlitros.ClientID%>").value;
       alert(x);
       return false;

   }

   function ValidarTiempo(tiempo) {
       var str = tiempo.value;
       var partes = str.split(":");
       if (partes.length == 2) {
           if (partes[0].length == 2) {
               if ((partes[0] >= 0 && partes[0] <= 99) && partes[0].length == 2) {

               }
               else {
                   //alert("El Tiempo especificado no es correcto. \n FORMATO: HH:MM");
                   showDialog('Error', 'El Tiempo especificado no es correcto. \n FORMATO: HH:MM', 'warning');
                   tiempo.select();
                   return false;
               }
           }
           else {
               //alert("El Tiempo especificado no es correcto. \n FORMATO: HH:MM");
               showDialog('Error', 'El Tiempo especificado no es correcto. \n FORMATO: HH:MM', 'warning');
               tiempo.select();
               return false;

           }
           if ((partes[1] >= 0 && partes[1] <= 99) && partes[1].length == 2) {
               return true;
           }
           else {
               //alert("El Tiempo especificado no es correcto. \n FORMATO: HH:MM");
               showDialog('Error', 'El Tiempo especificado no es correcto. \n FORMATO: HH:MM', 'warning');
               tiempo.select();
               return false;
           }
       }
       else {
           showDialog('Error', 'El Tiempo especificado no es correcto. \n FORMATO: HH:MM', 'warning');
           //alert("El Tiempo especificado no es correcto. \n FORMATO: HH:MM");
           tiempo.select();
           return false;
       }
   }

   function validarmaxlength(textbox, maximo) //Función para limitar la cantidad de caracteres en un TextBox, recibe como parametros
   {                                   // el nombre del TextBox y la cantidad permitida.
       if (textbox.value.length > maximo) {
           textbox.value = textbox.value.substring(0, maximo);
           //alert("Solo puedes ingresar hasta un maximo de "+maximo+" caracteres");
           return false;
       }
   }

   function validaFloat(numero) {
       var string = numero.value;
       var patt1 = /^((\d+(\.\d*)?)|((\d*\.)?\d+))$/;
       if (patt1.test(string)) {
           var partes = string.split(".");
           if (partes.length == 2) {
               if (partes[1].length > 2) {
                   showDialog('Error', 'El Numero No es Valido, Solo se Permiten 2 decimales.', 'warning');
                   //alert("El Numero No es Valido, Solo se Permiten 2 decimales.");
                   numero.focus();
                   numero.select();
                   return false;
               }
               else if (partes[1].length == 0) {
                   numero.value = partes[0] + '.' + '00'
                   return true;

               }
               else if (partes[1].length == 1) {
                   numero.value = partes[0] + '.' + partes[1] + '0';
                   return true;
               }
               else { return true; }
           }
           else if (partes.length > 2) {
               showDialog('Error', 'El Numero No es Valido, Intenta Nuevamente.', 'warning');
               //alert("El Numero No es Valido, Intenta Nuevamente.");
               numero.focus();
               numero.select();
               return false;
           }
           else {
               numero.value = string + ".00";
               return true;

           }
       }
       else {
           if (isNaN(string) == true) {
               //alert("Ingresar Solo Numeros.");
               showDialog('Error', 'Ingresar Solo Numeros.', 'error');
               numero.focus();
               numero.select();
               numero.focus();
               return false;
           }
           else {

               // numero.value=string + ".00";
               return true;
           }
       }

   }
   function confirmar(tipo) {
       try {
           if (tipo == 1) {
               var y = confirm("¿Deseas Guardar el Registro?");
               if (y == true) {
                   //if ((Convert.ToDouble(txtkmactual.Text) <= Convert.ToDouble(txtkmanterior.Text)) && txtidc_Folio.Text == "" && Convert.ToBoolean(Session["Folio"]) == false)
                   var txtkmactual = document.getElementById("<%=txtkmactual.ClientID%>").value;
                    var txtkmanterior = document.getElementById("<%=txtkmanterior.ClientID%>").value;
                    var txtidc_Folio = document.getElementById("<%=txtidc_Folio.ClientID%>").value;
                    if ((txtkmactual <= txtkmanterior) && txtidc_Folio == "") {
                        var x = confirm('El Nuevo Kilometraje no Puede ser Igual o Menor al Kilometraje Anterior... \n ¿Deseas Capturar el Folio de Autorizacion?');
                        if (x == true) {
                            Folios();
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        document.getElementById("<%=btng.ClientID%>").click();
                         return false;
                     }

                 }
                 else {
                     return false;
                 }

             }
             else {
                 ModalConfirmf('El Nuevo Kilometraje no Puede ser Igual o Menor al Kilometraje Anterior... \n ¿Deseas Capturar el Folio de Autorizacion?');
             }

         }
         catch (ex) {
             alert(ex);
         }

     }
     function Folios() {
         window.open('Folios_Autorizacion.aspx?tipo=58');
         return false;
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Carga Combustible </h1>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%;">
                <div class="row">
                    <div class="col-lg-12">
                        <h4><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;
                            <asp:Label ID="lblcarga" runat="server" Text="No. de Carga:"></asp:Label></strong></h4>
                        <asp:TextBox ID="txtnocarga" runat="server" Enabled="False" Font-Bold="True"
                            ForeColor="Navy" Width="100%"
                            CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-12">
                        <h4><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;
                            <asp:Label ID="Label25" runat="server" Text="Fecha:"></asp:Label></strong></h4>
                        <asp:TextBox ID="txtfecha" runat="server"
                            Enabled="False"
                            CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-12">
                        <h4><strong><i class="fa fa-bolt" aria-hidden="true"></i>&nbsp;
                            <asp:Label ID="Label24" runat="server" Text="Tipo de Combustible: "></asp:Label></strong></h4>
                        <asp:DropDownList ID="cbotipocombustible" runat="server" Enabled="False" CssClass="form-control"
                            Font-Bold="True" Font-Italic="False">
                        </asp:DropDownList>
                    </div>

                    <div class="col-lg-12 col-sm-12">
                        <h4><strong><i class="fa fa-car" aria-hidden="true"></i>&nbsp;<asp:Label ID="Label2" runat="server" Text="Camion: "></asp:Label></strong></h4>
                        <asp:DropDownList ID="cbovehiculos" runat="server" AutoPostBack="True" CssClass="fc"
                            Font-Bold="False" Width="72%"
                            OnSelectedIndexChanged="cbovehiculos_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtbveh" runat="server" AutoPostBack="True"
                            CausesValidation="True" Width="25%"
                            placeholder="Buscar"
                            CssClass="fc"></asp:TextBox>
                    </div>

                    <div class="col-lg-12 col-sm-12">
                        <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;
                            <asp:HyperLink ID="linkChofer" runat="server" ForeColor="Navy">Chofer</asp:HyperLink></strong></h4>
                        <asp:DropDownList ID="cbochoferes" runat="server" AutoPostBack="True"
                            Font-Bold="False" Width="72%" CssClass="fc"
                            OnSelectedIndexChanged="cbochoferes_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtbchofer" runat="server" AutoPostBack="True"
                            CausesValidation="True" Enabled="False" Width="25%" CssClass="fc"
                            OnTextChanged="TextBox2_TextChanged" placeholdeR="Buscar"></asp:TextBox>
                    </div>
                    <div class="col-lg-12 ">
                        <h4><strong>
                            <asp:Label ID="Label8" runat="server" Text="Cantidad de Litros"></asp:Label></strong></h4>
                        <asp:TextBox ID="txtcantidadlitros" runat="server" Width="100%" TextMode="Number" onkeypress="return validarEnteros(event);"
                            CssClass="form-control">1</asp:TextBox>
                    </div>

                    <div class="col-lg-12">
                        <h4><strong>
                            <asp:Label ID="Label6" runat="server" Text="Kilometraje Actual:"></asp:Label></strong></h4>
                        <asp:TextBox ID="txtkmactual" runat="server" Width="100%" TextMode="Number" onkeypress="return validarEnteros(event);"
                            CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-lg-12">
                        <h4><strong>
                            <asp:Label ID="Label3" runat="server" Text="Kilometraje Anterior: "></asp:Label></strong></h4>
                        <asp:TextBox ID="txtkmanterior" runat="server" ForeColor="Black"
                            onfocus="this.blur()" Width="100%"
                            CssClass="form-control">0</asp:TextBox>
                    </div>

                    <div class="col-lg-12">
                        <h4><strong>
                            <asp:Label ID="Label4" runat="server" Text="Rendimiento Anterior: "></asp:Label></strong></h4>
                        <asp:TextBox ID="txtrendimientoanterior" runat="server" Enabled="False"
                            Width="100%"
                            CssClass="form-control">0.00</asp:TextBox>
                    </div>

                    <div class="col-lg-12">
                        <h4><strong>
                            <asp:Label ID="Label5" runat="server" Text="Litros Anterior: "></asp:Label></strong></h4>
                        <asp:TextBox ID="txtlitrosanterior" runat="server" Enabled="False"
                            OnTextChanged="txtlitrosanterior_TextChanged" Width="100%"
                            CssClass="form-control">0</asp:TextBox>
                    </div>

                    <div class="col-lg-12">
                        <h4><strong>
                            <asp:Label ID="Label7" runat="server" Text="Distancia:"></asp:Label></strong></h4>
                        <asp:TextBox ID="txtdistancia" runat="server" Enabled="False" Width="100%"
                            CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-lg-12">
                        <h4><strong>
                            <asp:Label ID="Label9" runat="server" Text="Rendimiento Actual:"></asp:Label></strong></h4>
                        <asp:TextBox ID="txtrn" runat="server" Enabled="False" Font-Bold="True"
                            ForeColor="Navy" Width="100%"
                            CssClass="form-control">0.00</asp:TextBox>
                    </div>

                    <div class="col-lg-12">
                        <h3><strong>
                            <asp:Label ID="lblaltor" runat="server" ForeColor="#009999" Style="display: none;"
                                Text="** El Rendimiento se Mejoro **"></asp:Label>
                            <asp:Label ID="lblbajor" runat="server" ForeColor="Red" Style="display: none;"
                                Text="**Atención...El Rendimiento Bajo**"></asp:Label></strong></h3>
                    </div>

                    <div class="col-lg-12">
                        <asp:Panel ID="Panel1" runat="server" ForeColor="Black"
                            GroupingText="Información de la Computadora:" Style="display: " Width="100%">
                            <div class="col-lg-12">
                                <h4><strong>
                                    <asp:Label ID="Label12" runat="server" Text="Distancia Recorrida:"></asp:Label></strong></h4>
                                <asp:TextBox ID="txtdistanciarecorrida" runat="server" Enabled="False" TextMode="Number"
                                    Width="100%" onkeypress="return validarEnteros(event);"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-12">
                                <h4><strong>
                                    <asp:Label ID="Label11" runat="server" Text="Combustible Utilizado:"></asp:Label></strong></h4>
                                <asp:TextBox ID="txtcombustibleutilizado" runat="server" Enabled="False"
                                    Width="100%" TextMode="Number" onkeypress="return validarEnteros(event);"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-12">
                                <h4><strong>
                                    <asp:Label ID="Label13" runat="server" Text="Tiempo RELENTI:"></asp:Label></strong></h4>
                                <asp:TextBox ID="txttiemporelenti" runat="server" Enabled="False" Width="100%"
                                    CssClass="form-control">00:00</asp:TextBox>
                            </div>
                            <div class="col-lg-12">
                                <h4><strong>
                                    <asp:Label ID="Label17" runat="server" Text="Rendimiento:"></asp:Label></strong></h4>
                                <asp:TextBox ID="txtrendimientopc" runat="server" Enabled="False" Width="100%"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-12">
                                <h4><strong>
                                    <asp:Label ID="Label18" runat="server" Text="Combustible Utilizado en RELENTI:"></asp:Label></strong></h4>
                                <asp:TextBox ID="txtutilizadorelenti" runat="server" Enabled="False" Text=""
                                    Width="100%"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-12">
                                <h4><strong>
                                    <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="FALTANTE:"></asp:Label></strong></h4>
                                <asp:TextBox ID="txtfaltante" runat="server" BackColor="Red" Enabled="False"
                                    ForeColor="White" Width="100%"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-lg-12">
                        <h4><strong>
                            <asp:Label ID="Label26" runat="server" Text="Tanque"></asp:Label></strong><span>
                                <asp:CheckBox ID="chkvirtual" runat="server" Enabled="False" Text="Virtual" CssClass="radio3 radio-check radio-info radio-inline" /></span></h4>
                        <asp:DropDownList ID="cbotanque" runat="server" AutoPostBack="True" CssClass="fc" Width="100%"
                            Font-Bold="False"
                            OnSelectedIndexChanged="cbotanque_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="col-lg-12">
                        <asp:Panel ID="Panel2" runat="server" BorderColor="Gray" BorderStyle="Solid"
                            BorderWidth="1px" Width="100%" Wrap="False" Style="padding: 5px;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:CheckBox CssClass="radio3 radio-check radio-info radio-inline" ID="chkcandado" runat="server" Text="Candado de Seguridad" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox CssClass="radio3 radio-check radio-info radio-inline" ID="chkcincho" runat="server" Text="Cincho de Seguridad" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox CssClass="radio3 radio-check radio-info radio-inline" ID="chkfrenomotor" runat="server" Text="Freno de Motor Activado"
                                            Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox CssClass="radio3 radio-check radio-info radio-inline" ID="chkcalibracion" runat="server"
                                            Text="Calibracion de Llantas Adecuada" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox CssClass="radio3 radio-check radio-info radio-inline" ID="chkespuma" runat="server" Text="Espuma de Seguridad" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>

                    <div class="col-lg-12">
                        <asp:CheckBox ID="chkreportebc" runat="server"
                            Text="Reporte de Rendimiento Bajo Combustible" CssClass="radio3 radio-check radio-info radio-inline" />
                        <br />
                        <h4><strong>
                            <asp:Label ID="lblobservaciones" runat="server" Style="display: none"
                                Text="Observaciones: "></asp:Label></strong></h4>
                        <asp:TextBox ID="txtobservaciones" runat="server" CssClass="form-control"
                            Style="resize: none; display: none;" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </div>
                    <div class="col-lg-12">
                        <h3><strong>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Navy"
                                Text="**RELENTI.- Cuando el Camion esta en 0 km y esta Encendido.**"></asp:Label></strong></h3>
                    </div>
                </div>

                <table style="width: 100%; visibility: hidden;">

                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td align="center">
                                        <asp:ImageButton ID="btnguardar" runat="server" BorderColor="Gray"
                                            BorderWidth="1px" Height="51px"
                                            ToolTip="Guardar" Width="46px" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnnuevo" runat="server" BorderColor="Gray"
                                            BorderWidth="1px" Height="51px"
                                            OnClick="btnnuevo_Click" ToolTip="Nuevo" Width="46px" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnsalir" runat="server" BorderColor="Gray"
                                            BorderWidth="1px" Height="51px"
                                            OnClick="btnsalir_Click" ToolTip="Salir" Width="46px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="visibility: hidden;">
                            <asp:TextBox ID="txtidc_vehiculo" runat="server" Width="47px" CssClass="Ocultar"></asp:TextBox>
                            <asp:TextBox ID="txtidc_empleado" runat="server" Width="33px" CssClass="Ocultar"></asp:TextBox>
                            <asp:TextBox ID="txt_tipo_camion" runat="server" Width="35px" CssClass="Ocultar"></asp:TextBox>
                            <asp:TextBox ID="txtidc_Folio" runat="server" Width="27px" CssClass="Ocultar"></asp:TextBox>
                            <asp:TextBox ID="txtcamion" runat="server" CssClass="Ocultar" Enabled="False"
                                Width="10px"></asp:TextBox>
                            <asp:ImageButton ID="imgbuscarcamion" runat="server" CssClass="Ocultar" Height="10px"
                                OnClick="imgbuscarcamion_Click"
                                Width="10px" />
                            <asp:ImageButton ID="btnagregarchofer" runat="server" CssClass="Ocultar" Height="10px"
                                OnClick="btnagregarchofer_Click"
                                Width="16px" />
                            <asp:TextBox ID="txtchofer" runat="server" CssClass="Ocultar" Enabled="False"
                                Width="10px"></asp:TextBox>
                            <asp:Button ID="btng" runat="server" OnClick="btng_Click" Text="guardar"
                                CssClass="Ocultar" />

                            <asp:Button ID="btnb_veh" runat="server" OnClick="btnb_veh_Click"
                                CssClass="Ocultar" />
                            <asp:Button ID="btnb_emp" runat="server" OnClick="btnb_emp_Click"
                                CssClass="Ocultar" />
                        </td>
                    </tr>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:LinkButton ID="lnkguardar" Visible="true" CssClass="btn btn-primary btn-block" runat="server" OnClick="lnkguardar_Click">Guardar</asp:LinkButton>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:LinkButton ID="lnknuevo" CssClass="btn btn-info btn-block" runat="server" OnClick="lnknuevo_Click">Nuevo</asp:LinkButton>
                </div>
            </div>
            <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 id="modal_title"><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="text-align: center;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="content_modal"></label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-6 col-xs-6">
                                <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade modal-info" id="myModalf" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="text-align: center;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="content_modalf"></label>
                                    </h4>
                                    <asp:Label ID="Label10" runat="server" Text="Folio:"></asp:Label>

                                    <asp:TextBox ID="txtfolio" runat="server" Width="100%"
                                        CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-6 col-xs-6">
                                <asp:Button ID="yesfolio" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yesfolio_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="nofolio" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cerrar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="yesfolio" />
            <asp:PostBackTrigger ControlID="Yes" />
            <asp:PostBackTrigger ControlID="cbovehiculos" />
            <asp:PostBackTrigger ControlID="btnb_veh" />
            <asp:PostBackTrigger ControlID="btnb_emp" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>