<%@ Page Language="VB" MasterPageFile="~/Global.Master" AutoEventWireup="false" CodeFile="Pedidos7_m.aspx.vb" Inherits="Pedidos_m2"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" Runat="Server">
    <style type="text/css" >
        .txt_search{padding-left:24px !important;background:white url(Iconos/bcksearch.png) no-repeat left center !important;}
        .ui-datepicker-trigger { position: relative; left:-20px; top:-25px; }
        .Ocultar {
            display:none;
        }
    </style>

    <script type="text/javascript">
        function aportaciones() {
            var tabla = document.getElementById('<%=grdproductos2.ClientID%>');
        var ft = document.getElementById('<%=txtmaniobras.ClientID%>').value;
        var sbt = document.getElementById('<%=txtpretotal.ClientID%>').value;
        if (tabla == null) {
            alert('No Has Agregado Articulos.');
            return false;
        }
        else {
            window.open('aportaciones.aspx?ft=' + ft + '&sbt=' + sbt);
        }
        return false;

    }

    var myvar_guardando;
    function mostrar_procesar_guard() {
        myvar_guardando = setTimeout(function () { document.getElementById('div_guardando').style.display = '' }, 0);
    }

    function myStopFunction_guard() {
        clearTimeout(myvar_busq);
        document.getElementById('div_guardando').style.display = 'none';
    }


    var myvar_busq;
    function mostrar_procesar_busq() {
        myvar_busq = setTimeout(function () { document.getElementById('div_busq').style.display = '' }, 0);
    }

    function myStopFunction_busq() {
        clearTimeout(myvar_busq);
        document.getElementById('div_busq').style.display = 'none';
    }


    var myvar_grid;
    function mostrar_procesar_grid() {
        myvar_grid = setTimeout(function () { document.getElementById('ref_grid').style.display = '' }, 0);
    }

    function myStopFunction_grid() {
        clearTimeout(myvar_grid);
        document.getElementById('ref_grid').style.display = 'none';
    }

    function validar_croquis() {

    }

    function validar_oc() {
        var oc = document.getElementById("<%=oc.ClientID%>").value;
        var txtnumeroOC = document.getElementById("<%=txtnumeroOC.ClientID%>");
        var Foliooc = document.getElementById("<%=txtFolioOc.ClientID%>").value;
        if (oc == "True") {
            if (txtnumeroOC.value == "" && (Foliooc.value == "" || Foliooc.value == 0)) {
                var ped_folio = confirm("Es Requerido Ingresar la Orden de Compra del Cliente, Es Necesario Tambien Anexar la O.C. \n \u000b \n ¿Deseas Capturar el Folio de Autorización?");
                if (ped_folio == true) {


                }

                return false;
            }
        }
        else {

        }
        mostrar_procesar_guard();
        return true;

        //return validar_croquis();

    }

    function ver_maniobras() {

        var cboentrega = document.getElementById("<%=cboentrega.ClientID%>");
        var txtmaniobras = document.getElementById("<%=txtmaniobras.ClientID%>");
        var txtFolio = document.getElementById("<%=txtFolio.ClientID%>");
        if (cboentrega.options[cboentrega.selectedIndex].value == 1) {
            if (txtmaniobras.value != 0 && txtFolio.value == "") {

                var cargar = confirm("¿Deseas Cargar al Pre-Pedido el Monto del Flete?");
                if (cargar == true) {
                    document.getElementById("<%=btncargarflete.ClientID%>").click();
                        return false;
                    }
                    else {
                        alert("El Pre-Pedido Requerira Folio de Autorización.");
                        document.getElementById("<%=txtFolio.ClientID%>").value = '-1';
                       document.getElementById("<%=tbnguardarPP.ClientID%>").click();
                        return false;
                    }

                }
            //return validar_oc();
                mostrar_procesar_guard();
            }
            else if (cboentrega.options[cboentrega.selectedIndex].value == 3) { //MIC 13-05-2015
                mostrar_procesar_guard();
            }

        }

        function confirmacion_pago() {

            //Validar Articulos con Especificaciones
            var faltan_especif = false;
            $('.img-especif').each(function () {
                var especif = $(this).attr('especif');
                var num_especif = $(this).attr('num_especif');
                if (especif == 'True') {
                    if (parseInt(num_especif) <= 0) {
                        faltan_especif = true;
                    }
                }
            });

            if (faltan_especif == true) {
                alert('Faltan Seleccionar Especificaciones para Algunos Articulos.');
                return false;
            }

            var lblconfirmacion = document.getElementById('<%=lblconfirmacion.ClientID%>');
        if (lblconfirmacion != null) {
            var txtfolioCHP = document.getElementById('<%=txtfolioCHP.ClientID%>').value;
            var grdproductos2 = document.getElementById('<%=grdproductos2.ClientID%>');
            var cboentrega = document.getElementById('<%=cboentrega.ClientID%>');
            var ventrar = cboentrega.options[cboentrega.selectedIndex].value;
            var formapago = document.getElementById('<%=txtformapago.ClientID%>').value;
            if (ventrar <= 4 && formapago == "") {
                var check = false;
                if (grdproductos2 == null) {
                    alert("La Lista de Productos no Puede Estar Vacia.");
                    return false;
                }
                else {
                    for (var i = 1; i <= grdproductos2.rows.length - 1; i++) {
                        if (grdproductos2.rows[i].cells[11].textContent == 4406) {
                            check = true;
                            break;

                        }
                    }
                    if (check == false && (txtfolioCHP == "" || txtfolioCHP == '0')) {
                        alert('El Cliente Requiere Confirmacion de Pago.');
                        window.open('confirmacion_de_pago_mobile.aspx');
                        return false;
                    }
                }
            }
        }
        return ver_maniobras();

    }


    var myvar;
    function mostrar_procesar() {
        myvar = setTimeout(function () { document.getElementById('procesando_div').style.display = '' }, 0);
    }



    function myStopFunction() {
        clearTimeout(myvar);
        document.getElementById('procesando_div').style.display = 'none';
    }
    function tipo_entrega(cboentrega) {
        var tipo = cboentrega.options[cboentrega.selectedIndex].value;
        var btnconsignado = document.getElementById('<%=btnconsignado.ClientID%>');
        if (tipo == 1) {

            btnconsignado.value = "Consignado";

        }
        else if (tipo == 2) {
            btnconsignado.value = "Consignado";
        }
        else if (tipo == 3) {
            btnconsignado.value = "Detalle Recoge Cliente";
        }
        else if (tipo == 4) {
            btnconsignado.value = "Detalle Anticipos";
        }
        return false;
    }

    function folio_maniobras() {
        var folio = confirm("Es necesario cobrar las maniobras. \n \u000b \n ¿Deseas capturar el Folio de Autorización?");
        if (folio == true) {
            document.getElementById("<%=btnfoliomaniobras.ClientID%>").click();
        }
    }

    function cargar_maniobras() {
        var cargar = confirm("¿Deseas Cargar al Pre-Pedido el Monto del Flete?");
        if (cargar == true) {
            document.getElementById("<%=btncargarmaniobras.ClientID%>").click();
        }
    }


    function cargar_detalles() {
        var detalles = confirm("El Pedido Tiene Varios Detalles: \n" + mensaje + "\n \u000B \n Completa la Información Necesaria o Pide un Folio de Autorización \n \u000B \n ¿Deseas Capturar el Folio de Autorización?");
        if (detalles == true) {
            document.getElementById("<%=btndetallespedido.ClientID%>").click();
        }
    }


    function up_files(tipo) {
        if (tipo == 1) {
            var fucroquis = document.getElementById("<%=fucroquis.ClientID%>");
            fucroquis.click();
        }
        else if (tipo == 2) {
            var fullamada = document.getElementById("<%=fullamada.ClientID %>");
                fullamada.click();
            }
        return false;

    }




    function cboentrega(combo) {
        if (combo.value == 3) {
            RecogeCliente();
        }
        else if (combo.value == 4) {
            PedidoEspecial();
        }
    }
    function editar_articulo(idc_articulo) {
        editar_precios_cantidad_1(idc_articulo);
    }
    function editar_precios_cantidad_1(idc_articulo) {


        var ruta = "editar_precios_cantidad.aspx?edit=1&cdi=" + idc_articulo;
        var width = document.width - 40;             //document.width - 50;
        var height = document.height; //- 50;
        //TINY.box.show({iframe:ruta ,boxid:'frameless',width:width,height:height,fixed:false,maskid:'bluemask',maskopacity:40,top:2,left:2})
        document.getElementById("<%=txtidc_articulo.ClientID%>").value = idc_articulo;
        window.open(ruta);
        return false;
    }


    function check_rd(rd) {
        if (rd.checked == true) {
            rd.checked = true;
            return false;
        }
        else {
            rd.checked = false;
            return false;
        }
    }



    function pageLoad(sender, args) {

        //    
        //       $(function() {
        //                $( "#tabs" ).tabs({
        //                    collapsible: true
        //                });
        //            });


        $(document).ready(function () {

            $('.img-especif').each(function () {
                var especif = $(this).attr('especif');
                var num_especif = $(this).attr('num_especif');
                var idc = $(this).attr('idc');
                if (especif == 'True') {
                    if (parseInt(num_especif) > 0) {
                        $(this).prop('src', 'imagenes/spe_green.png');
                    }
                    else {
                        $(this).prop('src', 'imagenes/spe_red.png');
                    }
                    $(this).click(function () { window.open('especificaciones.aspx?cdi=' + idc); return false; });
                }
                else {
                    $(this).prop('src', 'imagenes/spe_disabled.png');
                    $(this).click(function () { return false; });
                }
            });
        });


    }
    //    function popup_consignado()
    //    {
    //       var id  = document.getElementById("<%=txtid.ClientID%>").value;
        //       var consignado = document.getElementById("<%=txt_consignado.ClientID%>").value;
        //       var width = document.width - 50;
        //       var height = document.height;
        //       alert(width );
        //       var ruta ="Consignado_mobile.aspx?id=" + id + '&consignado=' + consignado;
        //       TINY.box.show({iframe:ruta ,boxid:'frameless',width:width,height:height,fixed:false,maskid:'bluemask',maskopacity:40})
        //       return false; 
        //    }  


        function popup_consignado() {
            var cboentrega = document.getElementById("<%=cboentrega.ClientID%>");
       var tipo = cboentrega.options[cboentrega.selectedIndex].value;
       var id = document.getElementById("<%=txtid.ClientID%>").value;
        var consignado = document.getElementById("<%=txt_consignado.ClientID%>").value;
       var ruta;
       var encodedString = btoa(id);
       var encodedString2 = btoa(consignado);
       if (tipo == 1) {
           ruta = "Consignado_mobile.aspx?id=" + encodedString + '&consignado=' + encodedString2;
           window.open(ruta);
       }
       else if (tipo == 2) {
           ruta = "Consignado_mobile.aspx?id=" + encodedString + '&consignado=' + encodedString2;
           window.open(ruta);
       }
       else if (tipo == 3) {
           RecogeCliente();
       }
       else if (tipo == 4) {
           PedidoEspecial();
       }
       return false;
   }

   function editar_precios_cantidad(tipo) {
       var cboproductos;
       if (tipo == 1) {
           cboproductos = document.getElementById("<%=cboproductos.ClientID%>");
       }
       else if (tipo == 2) {
           cboproductos = document.getElementById("<%=cbomaster.ClientID%>");
       }

    if (cboproductos.options.length > 0) {
        var idc_articulo = cboproductos.options[cboproductos.selectedIndex].value;
        var ruta = "editar_precios_cantidad.aspx?cdi=" + idc_articulo + "&t=" + tipo;
        window.open(ruta);
        return false;
    }
    else {
        return false;
    }

}





function cerrar() {
    TINY.box.hide();
    return false;
}
function desh_panel() {
    var panel = document.getElementById("<%=txtbuscar.ClientID%>"); //Cambio nombre solo por el error.
        panel.enabled = false;
        panel.style.display = "block";

    }


    function validar_fecha(txtfecha, txtfecha_max) {

        var numeros = txtfecha.value.match(/\d+/g);
        var fecha = new Date(numeros[2], numeros[1] - 1, numeros[0]);



        var numeros_max = txtfecha_max.value.match(/\d+/g);
        var fecha_max = new Date(numeros_max[2], numeros_max[1] - 1, numeros_max[0]);

        if (fecha > fecha_max) {
            alert("La Fecha de Entrega Debe Ser Menor o Igual a 3 Dias.");
            txtfecha.value = txtfecha_max.value;
            return false;
        }

        fecha_hoy = new Date();

        if (fecha < fecha_hoy) {
            alert("La Fecha no Debera ser Menor al dia de Hoy.");
            var day = fecha_hoy.getDate();
            if (day < 10) {
                day = '0' + day;
            }


            var month = (fecha_hoy.getMonth()) + 1;
            if (month < 10) {
                month = '0' + month;
            }


            var year = fecha_hoy.getFullYear();

            txtfecha.value = day + '/' + month + '/' + year;
            return false;
        }


    }
    function ver_ficha(idc) {
        var ruta = "ficha_tecnica.aspx?idc=" + idc;
        window.open(ruta);
        return false;
    }

    function cursor(objeto) {

        objeto.style.cursor = 'hand';
    }

    function cursor_fuera(objeto) {
        objeto.style.cursor = 'pointer';
    }

    function Ubicacion_Cliente(nombre, direccion) {
        if (document.getElementById("<%=txtid.ClientId%>").value == '' || document.getElementById("<%=txtid.ClientId%>").value == 0)
            alert("No Existe Cliente.")
        else {
            var y = (screen.height - 425) / 2;
            var x = (screen.width - 836) / 2;
            var ruta = 'Ubicacion_Cliente.aspx?id=' + document.getElementById("<%=txtid.ClientId%>").value + '&cliente=' + nombre + '&direccion=' + direccion;
             window.open(ruta, "Orden de Compra", "width=836px,height=425px,top=" + y + ",left=" + x + ",Scrollbars=yes,title=yes,location=no");
             return false;
         }
     }
     //document.onkeydown=
     function redirecting() {
         document.getElementById("<%=btnredirecting.clientid%>").click();
    return false;
}



function buscarart(e) {

    var key = (document.all) ? e.keyCode : e.which;
    var txtcodigoarticulo = document.getElementById("<%=txtcodigoarticulo.ClientID%>").value;
    if (key == 13) {
        if (txtcodigoarticulo.length >= 3) {
            mostrar_procesar_busq();
        }
        document.getElementById("<%=btnbuscarart.clientid%>").click();
       return false;
   }

}


function agregararticulo(e) {
    var key = (document.all) ? e.keyCode : e.which;

    if (key == 13) {
        document.getElementById("<%=btnagregar.ClientId %>").click();
     return false;
 }
}



function abremodal() {
    var y = (screen.height - 40) / 2;
    var x = (screen.width - 100) / 2;
    sList = window.open("Folio_Autorizacion.aspx?tipo=95", "argumentos", "Folio_Autorización", "width=100px,height=40px,top=" + y + ",left=" + x + ",Menubar=no,Scrollbars=no,location=no");
    if (document.getElementById('<%=txtFolio.ClientID%>').value != '') {
           alert(document.getElementById('<%=txtFolio.ClientID%>').value);
       }
       else {
           alert('El Folio se agrego correctamente')
       }

   }


   //    function fileUpload2()
   //    { __doPostBack('fileUpload2','');
   //      return (true);
   //    }
   function x(evt) //Deshabilitar el Enter TextBox
   {
       return (evt ? evt.which : event.keyCode) != 13;
   }


   function mpeSeleccionOnCancel() //Boton Cancelar del ModalPopUp (Busqueda Articulos)
   {
       var txtcodigoarticulo = document.getElementById('<%=txtcodigoarticulo.ClientID%>');
    txtcodigoarticulo.value = "";
    document.getElementById('<%=txtcodigoarticulo.ClientID%>').focus();
  }

  function LostFocus() //Actualizar Valores en cambios del Grid Lista...
  {
      document.getElementById('<%=btnfocus.ClientID%>').click();
}

function cancelarbusqueda() {
    var txtbuscar = document.getElementById('<%=txtbuscar.ClientID%>');
    txtbuscar.value = "";
    document.getElementById('<%=txtbuscar.ClientID%>').focus();

}
function validarcampos() {
    var txt = document.getElementById('<%=txtcodigoarticulo.ClientID%>').value;
    if (txt == "") {
        alert("ingrse datos");
    }

}

//Cuenta los caracteres del textbox
function WorldCount(caracter) {
    //var cont= caracter.length;
    //if (cont==2)
    //{
    //document.getElementById("<%=btnagregar.ClientID%>").click();
    //}
    var code = window.event.keycode;
    if (caracter == code) {
        document.getElementById("<%=btnagregar.ClientID%>").click();
}
}

function cambiaFoco(e) {
    /*Esta funcion funciona con KeyPress y recibe como parametro el nombre de la caja destino(que es una cadena)*/

    //Primero debes obtener el valor ascii de la tecla presionada
    var key = (document.all) ? e.keyCode : e.which;
    //var key=window.event.keyCode;//
    //Si es enter(13)
    if (key == 13)
        //Se pasa el foco a la caja destino
    {
        var caja = document.getElementById('<%=txtprecio.ClientID%>');
    if (caja.disabled == true) {
        document.getElementById("<%=btnagregar.ClientId %>").click();
        }
        else {
            caja.focus();
            caja.select();
        }
        return false;
    }
}

function AbreHija() {
    if (document.getElementById("<%=txtid.ClientID%>").value == '')
        alert("No Existe Cliente")
    else {
        var txtnumeroOC_obj = '<%= txtnumeroOC.ClientID%>';
        var txtidOc_obj = '<%= txtidOc.ClientID%>';
        var encodedString = btoa(document.getElementById("<%=txtid.ClientID%>").value);
        var ruta = 'Oc_Digitales_Pendientes_2.aspx?id=' + encodedString + '&txtnumeroOC_obj=' + txtnumeroOC_obj + '&txtidOc_obj=' + txtidOc_obj;
        //          var y= (screen.height - 500) / 2;
        //          var x= (screen.width - 800)/ 2;                                     
        //          sList=window.open(ruta, "OC", "width=800px,height=500px,top=" + y + ",left=" + x + ",Menubar=no,Scrollbars=no,location=no");
        window.open(ruta);
        return false;
    }
}

function AbreConsignados() {
    if (document.getElementById("<%=txtid.ClientId%>").value == '')
        alert("No Existe Cliente")
    else {
        var ruta = 'Historial_Consignados.aspx?idc_cliente=' + document.getElementById("<%=txtid.ClientId%>").value;
        //sList = window.open(ruta, "Historial Consignados", "width=1100,height=900,Menubar=yes,Scrollbars=yes");
        var y = (screen.height - 500) / 2;
        var x = (screen.width - 900) / 2;
        sList = window.open(ruta, null, "width=900,height=500,top=" + y + ",left=" + x + ",Menubar=no,Scrollbars=no,location=no");
    }
}

function AbreBuscarColonia() {
    if (document.getElementById("<%=txtid.ClientId%>").value == '')
        alert("No Existe Cliente")
    else {
        var y = (screen.height - 500) / 2;
        var x = (screen.width - 700) / 2;
        var ruta = 'buscar_colonia.aspx';
        sList = window.open(ruta, null, "width=700px,height=500px,top=" + y + ",left=" + x + ",Menubar=no,Scrollbars=no,location=no,status=no");
    }
}

function RecogeCliente() {
    // var ruta="Recoje_Cliente2.aspx?idc_sucursal=" + document.getElementById('<%=txtsucursalr.ClientId%>').value;
    // TINY.box.show({iframe:ruta,boxid:'frameless',width:500,height:150,fixed:false,maskid:'bluemask',maskopacity:40})
    var ruta = "recoge_cliente_mobile.aspx?idc_sucursal=" + document.getElementById('<%=txtsucursalr.ClientId%>').value;
    window.open(ruta);
    //window.open(ruta, null, "width=510px,height=173px,top=" + y + ",left=" + x + ",Menubar=no,Scrollbars=no,location=no,status=no");
    // document.getElementById('<%=btnrc.clientId%>').click(); 

}

        function PedidoEspecial() {
            //var y= (screen.height - 340) / 2;
            //var x= (screen.width - 540 )/ 2;
            //ventana= window.open("Pedido_Especial2.aspx", null, "width=540px,height=340px,top=" + y + ",left=" + x + ",Menubar=no,Scrollbars=no,location=no,status=no,title='Pedido Especial'");
            //TINY.box.show({iframe:"Pedido_Especial2.aspx",boxid:'frameless',width:490,height:334,fixed:false,maskid:'bluemask',maskopacity:40})
            window.open("pedido_especial_mobile.aspx");
            //return false;
            //document.getElementById('<%=btnsf.clientId%>').click();

}

        function remLink() {
            if (window.sList && window.sList.open && !window.sList.closed)
                window.sList.opener = null;
        }

        function AbreIMGOC() {
            if (document.getElementById("<%=txtidOC.ClientId%>").value == '' || document.getElementById("<%=txtidOC.ClientId%>").value == 0)
        alert("No ha sido seleccionada la Orden de Compra.")
    else {
        var y = (screen.height - 500) / 2;
        var x = (screen.width - 800) / 2;
        var ruta = 'IMG_Orden_Compra.aspx?img=' + document.getElementById("<%=txtidOC.ClientId%>").value;
         window.open(ruta);
         return false;
     }

 }
 function Agregarcroquis() {
     if (document.getElementById("<%=txtid.ClientID%>").value == "" || document.getElementById("<%=txtid.ClientID%>").value == "") {
        alert("Es Necesario Seleccionar Cliente.");
        return false;
    }
    else {
        var fucroquis = document.getElementById("<%=fucroquis.ClientID%>");
      var file = fucroquis.value;
      var ext = file.substring(file.length - 4, file.length);
      if (ext == ".jpg" || ext == ".png" || ext == ".gif") {
          document.getElementById("<%=btnagregarcroquis.ClientId%>").click();
    }
    else {
        alert("El Tipo de Imagen Seleccionado no es Valido.");
        fucroquis.value = "";
        return false;
    }
}
}

function Error() {
}


function verCroquis() {
    var id = document.getElementById("<%=txtid.ClientID%>").value;
    var encodedString = btoa(id);
    var ruta = "IMG_Croquis.aspx?croquis=" + encodedString + "&tipo=1";
    if (document.getElementById("<%=txtid.ClientID%>").value == '')
        document.getElementById("<%=txtid.ClientID%>").value = '';
    else {
        window.open(ruta);
        return false;
    }
    return false;
}
function verCroquis2() {
    var id = document.getElementById("<%=txtid.ClientID%>").value;
     var encodedString = btoa(id);
     var ruta = "IMG_Croquis.aspx?croquis=" + encodedString + '&tipo=2';

     if (combo.options[combo.selectedIndex].value == '') {
         combo.options[combo.selectedIndex].value = '';
         return false;
     }
     else {
         //var y= (screen.height - 500) / 2;
         //var x= (screen.width - 800 )/ 2;
         //window.open(ruta,"Croquis Seleccionado","width=1100,height=900,scrollbars=yes,title=yes,location=no");  
         //window.open(ruta,null,"height=500, width=800,status=yes,resizable= no,top=" + y + ",left=" + x + ",scrollbars=yes, toolbar=no, menubar=no,location=no");
         window.open(ruta);
         return false;
     }

 }

 function AgregarLlamada() {
     if (document.getElementById("<%=txtid.ClientID %>").value == "" || document.getElementById("<%=txtid.ClientID %>").value == "") {
        alert("Es Necesario Seleccionar Cliente.");
        return false;
    }
    else {
        document.getElementById("<%=btnagregarllamada.ClientId%>").click();
          }

      }


      function validarmaxlength(textbox, maximo) //Función para limitar la cantidad de caracteres en un TextBox, recibe como parametros
      {                  // el nombre del TextBox y la cantidad permitida.
          if (textbox.value.length > maximo) {
              textbox.value = textbox.value.substring(0, maximo);
              alert("Solo puedes ingresar hasta un maximo de " + maximo + " caracteres");
          }
      }

      function reproducir_llamada() {
          var y = (screen.height - 70) / 2;
          var x = (screen.width - 340) / 2;
          var idc_cliente = document.getElementById("<%=txtid.ClientId%>").value;
            if (idc_cliente == "") {
                alert("Seleccionar Cliente");
                return false;
            }
            else {
                var ruta = "Audio_Llamada.aspx?idc_cliente=" + idc_cliente;
                //window.open(ruta,"Llamada","height=70, width=340,top=" + y + ", left=" + x + ",scrollbars=NO,titlebar=YES,resizable=NO,location=no, center=yes");
                window.open(ruta);
                return false;

            }


        }

        function validar_chekplus() {
            if (document.getElementById('<%=txtfolioCHP.ClientId%>').value != '' && document.getElementById('<%=txtfolioCHP.ClientId%>').value != '0') {
        document.getElementById('<%=btnvalidaChP.ClientId%>').click();
    }
    else {
        if (document.getElementById('<%=txtfolioCHP.ClientId%>').value == '') {
            document.getElementById('<%=txtfolioCHP.ClientId%>').value = '';
        }
        return false;
    }
}

//function validar_chekplus2(e)
//{   
//    var key = (document.all) ? e.keyCode : e.which;
//    if (key==13)
//    {
//       document.getElementById('<%=btnvalidaChP.ClientId%>').click();
        //       return false;
        //       
        //    }
        // 
        //}
        function buscar_cliente(e) {
            var key = (document.all) ? e.keyCode : e.which;
            if (key == 13) {
                document.getElementById('<%=btnbuscarcliente.ClientId%>').click();
        return false;
    }
}

function Hola() {
    alert('Hola mundo');
    document.getElementsByName = ('ctl00$ContentPlaceHolder1$grdproductos2$ctl02$txtprecioreal').value = 500.00;
    var valor = document.getElementById = ('ctl00_ContentPlaceHolder1_grdproductos2_ctl02_txtprecioreal').value;
    alert(valor);
    return false;
}



    </script>


    <div style="position: relative; width: 100%; top: 0px; right: 0px; bottom: 0px; left: 0px;">

        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td align="center">
                            <table style="border: 2px solid #000080; width: 100%;">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Medium" ForeColor="Blue" Height="16px" Text="No. Pre-Pedido/Pedido"
                                            Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblfolio" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Medium" ForeColor="Black" Text="No. Pedido" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>



                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 9%" align="right">
                                        <asp:Label ID="lblbuscar_cliente" runat="server" Font-Bold="True" ForeColor="Black"
                                            Text="Buscar Cliente: " Font-Names="arial" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbuscar" runat="server" AutoCompleteType="FirstName"
                                            CausesValidation="True" Font-Bold="True" Font-Size="Small"
                                            ForeColor="Black" Height="30px" Width="100%"
                                            CssClass="form-control2"></asp:TextBox>
                                        <div id="Div1" class="styled-select" style="width: 100%;" runat="server">
                                            <asp:DropDownList ID="cboclientes" runat="server" Font-Bold="True" CssClass="form-control2"
                                                ForeColor="Black" Height="35px" Width="100%">
                                            </asp:DropDownList>
                                        </div>

                                        <asp:Button ID="btnbuscarcliente" runat="server" Font-Bold="True" Style="display: none;" CssClass="btn btn-default"
                                            ForeColor="Black" Height="35px" Text="Buscar" Width="100%" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td>

                                        <asp:Button ID="btnacep_bus" runat="server" Font-Bold="True" ForeColor="Black"
                                            Height="35px" Text="Aceptar" Width="100%"
                                            CssClass="btn btn-default"
                                            Font-Names="arial" Font-Size="Small" />

                                    </td>
                                    <td>


                                        <asp:Button ID="btncan_bus" runat="server" Font-Bold="True" ForeColor="Black"
                                            Height="35px" Text="Cancelar" Visible="False" Width="100%"
                                            CssClass="btn btn-default"
                                            Font-Size="Small" />


                                    </td>
                                </tr>



                            </table>



                        </td>
                    </tr>
                    <tr>
                        <td>



                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 5%" align="right">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Black"
                                            Text="RFC: " Font-Names="arial" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtrfc" runat="server"
                                            CssClass="form-control2" onfocus="this.blur();"
                                            Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="30px" Style="display: inline !important;"
                                            ReadOnly="True" Width="70%"></asp:TextBox>
                                        <asp:TextBox ID="txtcve" runat="server"
                                            CssClass="form-control2" onfocus="this.blur();"
                                            Height="30px" ReadOnly="True" Width="10%" Font-Names="arial" Style="display: inline !important;"
                                            Font-Size="Small"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Black"
                                            Height="20px" Text="ID: " Width="21px" Font-Names="arial"
                                            Font-Size="Small" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtid" runat="server"
                                            CssClass="Ocultar" Enabled="False"
                                            Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="30px"
                                            Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Black"
                                            Text="Status" Font-Names="arial" Font-Size="Small" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtstatus" runat="server" Enabled="False" Font-Bold="True"
                                            Font-Size="Small" ForeColor="Black" Height="30px" ReadOnly="True"
                                            Width="100%"
                                            CssClass="form-control2"
                                            Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Black"
                                            Text="Nombre: " Font-Names="arial" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtnombre" runat="server" Font-Bold="True" onfocus="this.blur();"
                                            Font-Size="Small" ForeColor="Black" Height="30px" ReadOnly="True"
                                            Width="100%"
                                            CssClass="form-control2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Image ID="imgstatus" runat="server" Height="38px"
                                            ImageUrl="~/imagenes/srojo.png" Visible="False" Width="23px"
                                            CssClass="Ocultar" />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="lkverdatoscliente" runat="server" Font-Bold="True"
                                            Font-Names="arial" Font-Overline="False" Font-Size="Medium"
                                            Font-Underline="True" ForeColor="Blue" Height="30px"
                                            Style="padding-top: 10px; display: none;" CssClass="Ocultar">Ver 
                                Datos del Cliente
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </table>



                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%">
                                        <div id="Div2" class="styled-select" style="width: 99%;">
                                            <asp:DropDownList ID="cboentrega" runat="server" CssClass="form-control2"
                                                Font-Bold="True" ForeColor="Black" Height="35px" onchange="cboentrega(this);"
                                                Width="100%">
                                                <asp:ListItem Value="1">Entregamos</asp:ListItem>
                                                <asp:ListItem Value="3">Recoge Cliente</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </td>
                                    <td>
                                        <asp:Button ID="btnconsignado" runat="server" Font-Bold="True"
                                            ForeColor="Black" Height="35px" Text="Consignado" Width="99%"
                                            CssClass="btn btn-default"
                                            Font-Size="Small" Font-Overline="False" Font-Underline="False" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 50%">

                                        <asp:Button ID="btncaptArt" runat="server" Font-Bold="True" Font-Size="Small" OnClientClick="mostrar_procesar();"
                                            ForeColor="Black" Height="35px" Text="Capturar Articulos" Width="99%"
                                            CssClass="btn btn-default" />

                                    </td>
                                    <td style="width: 50%">
                                        <asp:Button ID="btnOC" runat="server" Enabled="False" Font-Bold="True"
                                            ForeColor="Black" Height="35px" Text="OC Pendientes" Width="99%"
                                            CssClass="btn btn-default"
                                            Font-Size="Small" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="X-Small" ForeColor="Blue" Height="10px" Style="line-height: 10px;"
                                Text="**Es Requerido el Cliente y el Tipo de Entrega para Poder Ingresar los Articulos."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span id="procesando_div" style="display: none; text-align: center">
                                <table>
                                    <tr>
                                        <td>
                                            <img id="procesando_gif" align="middle" alt="" height="40px"
                                                src="imagenes/loading.gif" width="40px" />
                                        </td>
                                        <td style="font-family: Arial; font-weight: bold;" valign="bottom">Cargando Productos Master...</td>
                                    </tr>
                                </table>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="txtcodigoarticulo" runat="server" AutoCompleteType="Search"
                                            CausesValidation="True"   placeholder="Buscar Articulo"
                                            CssClass="form-control2" Enabled="False"
                                            Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="30px"
                                            Width="100%"></asp:TextBox>

                                        <div id="Div3" class="styled-select" style="width: 100%;">
                                            <asp:DropDownList ID="cboproductos" runat="server" Font-Names="arial" CssClass="form-control2"
                                                Font-Size="Small" ForeColor="Black" Height="34px"
                                                Width="100%">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="cbomaster" runat="server" Font-Bold="True" CssClass="form-control2"
                                                ForeColor="Black" Height="35px" Visible="False" Width="100%">
                                            </asp:DropDownList>
                                        </div>

                                    </td>

                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <span id="div_busq" style="display: none; text-align: center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <img src="imagenes/loading.gif" alt="" id="Img2" align="middle" height="40px" width="40px" />
                                                    </td>
                                                    <td valign="bottom" style="font-family: Arial; font-weight: bold; font-size: small;">Buscando...</td>
                                                </tr>
                                            </table>
                                        </span>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_seleccionar_master" runat="server"
                                            CssClass="btn btn-default"
                                            Font-Bold="true" Font-Names="arial" Font-Size="Small" ForeColor="Black"
                                            Height="35px" Text="Seleccionar" Visible="False" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnmaster" runat="server"
                                            CssClass="btn btn-default"
                                            Font-Bold="true" Font-Names="arial" Font-Size="Small" ForeColor="Black"
                                            Height="35px" Text="Master" Visible="False" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnbuscar_codigo" runat="server"
                                            CssClass="btn btn-default"
                                            Font-Bold="true" Font-Names="arial" Font-Size="Small" ForeColor="Black"
                                            Height="35px" Text="Buscar" Visible="False" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <span id="ref_grid" style="display: none; text-align: center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <img src="imagenes/loading.gif" alt="" id="Img1" align="middle" height="40px" width="40px" />
                                                    </td>
                                                    <td valign="bottom" style="font-family: Arial; font-weight: bold;">Actualizando Productos...</td>
                                                </tr>
                                            </table>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="grdproductos2" runat="server" AutoGenerateColumns="False" style="background-color:white; font-size:12px;"
                                Height="84px" Width="100%" CssClass="table table-responsive table-bordered table-condensed">
                                <ItemStyle CssClass="AltRowStyle" ForeColor="Black" Font-Names="arial" Font-Size="Small" />
                                <HeaderStyle Font-Names="arial" Font-Size="Small" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Codigo">
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblcodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Codigo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Descripcion">
                                        <ItemStyle Width="28%" />
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Descripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="U.M.">
                                        <ItemStyle Width="4%" />
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UM") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Cantidad">
                                        <ItemStyle Width="8%" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtcantidadgrid" runat="server" ForeColor="Black" Height="20px" onkeydown="return x(event)" OnTextChanged="txtcantidadgrid_TextChanged"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>' Width="50px"></asp:TextBox>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblcantidad" runat="server" Text='<%# bind("cantidad") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Precio">
                                        <ItemStyle Width="10%" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtpreciogrid" runat="server" ForeColor="Black" Height="20px"
                                                onkeydown="return x(event)" OnTextChanged="txtpreciogrid_TextChanged"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.Precio", "{0:N4}") %>'
                                                Width="60px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblprecio" runat="server" Text='<%#Bind("precio", "{0:N4}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Importe">
                                        <ItemStyle Width="13%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblimporte" runat="server"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.Importe", "{0:N4}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Precio Real">
                                        <ItemStyle Width="13%" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtprecioreal" runat="server" ForeColor="Black" Height="20px"
                                                onkeydown="return x(event)" OnTextChanged="txtprecioreal_TextChanged"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.PrecioReal", "{0:N4}") %>'
                                                Width="60px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="precioreal" runat="server"
                                                Text='<%#Bind("precioreal", "{0:N4}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Descuento">
                                        <ItemStyle Width="12%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldescuento" runat="server"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.Descuento", "{0:N4}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Editar">
                                        <ItemStyle Width="12%" />
                                        <EditItemTemplate>
                                            <asp:Button ID="Button2" runat="server" CommandName="Guardar" Font-Bold="True"
                                                ForeColor="Black" Height="32px" Text="Guardar" Width="61px" />
                                            &nbsp;
                                    <asp:Button ID="Button3" runat="server" CausesValidation="false"
                                        CommandName="Cancelar" Font-Bold="True" ForeColor="Black" Height="32px"
                                        Text="Cancelar" Width="64px" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="Button4" runat="server" CausesValidation="false"
                                                CommandName="Edit" CssClass="Ocultar" Font-Bold="True" Font-Italic="False"
                                                ForeColor="Black" Height="28px" Text="Editar" Width="55px" />
                                            <asp:Button ID="btnmobile" runat="server" CssClass="Ocultar" Font-Bold="True"
                                                Font-Italic="False" ForeColor="Black" Height="28px" Text="Editar"
                                                Width="55px" />
                                            <asp:Button ID="btneliminar" runat="server" CssClass="Ocultar" Font-Bold="True"
                                                ForeColor="Black" Height="28px" Text="Eliminar" Width="67px" />
                                            <asp:ImageButton ID="imgespecif" runat="server" CssClass="img-especif" especif='<%# DataBinder.Eval(Container, "DataItem.tiene_especif") %>' num_especif='<%# DataBinder.Eval(Container, "DataItem.num_especif") %>'
                                                Height="30px" ImageUrl="~/imagenes/spe_green.png" ToolTip="Seleccionar Especificaciones" idc='<%# DataBinder.Eval(Container, "DataItem.idc_articulo") %>'
                                                Width="30px" />
                                            <asp:ImageButton ID="imgmobile" runat="server" CommandName="Editar"
                                                Height="30px" ImageUrl="~/imagenes/btn/icon_editar.png" ToolTip="Editar Articulo"
                                                Width="30px" />
                                            <asp:ImageButton ID="imgeliminar" runat="server" CommandName="eliminar"
                                                Height="30px" ImageUrl="~/imagenes/btn/icon_delete.png" ToolTip="Eliminar Articulo"
                                                Width="30px" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="idc_articulo">
                                        <HeaderStyle CssClass="Ocultar" />
                                        <ItemStyle CssClass="Ocultar" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.idc_articulo") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" runat="server"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.idc_articulo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="nota_credito" HeaderText="nc">
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="idc_articulo" HeaderText="">
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="RowStyle" ForeColor="Black" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="imgpromocion" runat="server" Height="40px"
                                ImageUrl="~/imagenes/promo.gif" Style="display: none;" Width="150px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox19" runat="server" BackColor="Red" Enabled="False" Style="border: none; border-radius: 3px;"
                                Height="16px" Width="16px"></asp:TextBox>
                            <asp:Label ID="Label17" runat="server" Font-Bold="True" ForeColor="Black"
                                Text="Nota de Credito Automatica" Width="206px" Font-Names="arial"
                                Font-Size="Small"></asp:Label>


                        </td>
                        <br />
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td align="center" style="width: 45%; font-size: 0.85em; font-weight: bold">Aportación</td>
                                    <td style="width: 10%">&nbsp;</td>
                                    <td align="center" style="width: 45%; font-size: 0.85em; font-weight: bold">Aportación Precios de Lista
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="txtaportacion" runat="server" Font-Bold="true" Style="text-align: right;"
                                            CssClass="form-control2"
                                            Enabled="False" Height="30px" Width="100%" ForeColor="Blue"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Button ID="btn_seleccionar_master0" runat="server" Style="border-radius: 3px !important; height: 30px !important;"
                                            CssClass="btn btn-default"
                                            Font-Bold="true" Font-Names="arial" Font-Size="Small" ForeColor="Black"
                                            Height="35px" OnClientClick="return aportaciones();" Text="..." Width="100%" />
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="txtaportacionl" runat="server" Font-Bold="true" Style="text-align: right;"
                                            CssClass="form-control2"
                                            Enabled="False" Height="30px" Width="100%" ForeColor="Blue"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 8%">
                                        <asp:Label ID="Label18" runat="server" CssClass="Ocultar" Font-Bold="True"
                                            Font-Size="X-Small" ForeColor="Blue" Text="Credito Disponible: "
                                            Visible="False" Width="131px" Font-Names="arial"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcreditodisponible" runat="server" BackColor="Green"
                                            CssClass="Ocultar" Enabled="False" Font-Bold="True" Font-Size="Small"
                                            ForeColor="White" Height="20px" Width="131px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="border-color: #C0C0C0; width: 100%; height: 93px;">
                                <tr>

                                    <td align="center" style="width: 100%">
                                        <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="Small"
                                            ForeColor="Black" Text="Total" Font-Names="arial"></asp:Label>
                                    </td>
                                </tr>
                                <tr>

                                    <td align="center"
                                        style="border-color: #FFFFFF #808080 #FFFFFF #808080;">
                                        <asp:TextBox ID="txtpretotal" runat="server" CssClass="form-control2"
                                            Enabled="False" Height="30px" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>

                                    <td align="center"
                                        style="border-color: #FFFFFF #808080 #FFFFFF #808080; height: 22px; width: 25%">
                                        <asp:TextBox ID="txttotaldescuento" runat="server" CssClass="form-control2"
                                            Enabled="False" ForeColor="Red" Height="30px" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>

                                    <td align="center"
                                        style="border-color: #FFFFFF #808080 #FFFFFF #808080; width: 25%">
                                        <asp:TextBox ID="txttotal" runat="server" CssClass="form-control2"
                                            Enabled="False" Height="30px" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center"
                                        style="border-color: #FFFFFF #808080 #FFFFFF #808080; width: 25%">
                                        <asp:Label ID="labeliva" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" ForeColor="Black" Text="IVA:"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td align="right" style="width: 7%">
                                        <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" ForeColor="Blue" Text="MANIOBRAS:"></asp:Label>
                                    </td>
                                    <td style="width: 93%">
                                        <asp:TextBox ID="txtmaniobras" runat="server"
                                            CssClass="form-control2"
                                            Enabled="False" Font-Bold="True" ForeColor="Black" Height="30px" Width="100%">0.00</asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblroja" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="9pt" ForeColor="Red"
                                Text="La Sucursal de Entrega no corresponde a la Lista de Precios del Cliente."
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 10%" align="right" valign="middle">
                                        <asp:Label ID="Label21" runat="server" Font-Bold="True" ForeColor="Black"
                                            Text="Fecha de Entrega:" Font-Names="arial"
                                            Font-Size="Small"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <div id="div_fecha" style="width: 100%" class="styled-select">
                                            <asp:DropDownList ID="cbofechas" runat="server" CssClass="form-control2" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 0%">
                                        <asp:Label ID="Label23" runat="server" BorderColor="#666666" Font-Bold="True"
                                            ForeColor="Black" Height="100%" Style="line-height: 13px; padding-top: 0px;"
                                            Text="No. de Orden de Compra del Cliente:" Width="120px"
                                            Font-Names="arial" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtnumeroOC" runat="server" ClientIDMode="Static"
                                            Enabled="False" Font-Bold="True" ForeColor="Black" Height="30px"
                                            Width="100%"
                                            CssClass="form-control2"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Button ID="btnseleccionarOC" runat="server" Font-Bold="True"
                                            ForeColor="Black" Height="35px" Text="Seleccionar OC" ToolTip="Seleccionar OC"
                                            Width="99%"
                                            CssClass="btn btn-default" />
                                    </td>
                                    <td style="width: 50%">
                                        <asp:Button ID="btnverOc" runat="server" Font-Bold="True" ForeColor="Black"
                                            Height="35px" Text="Ver OC" Width="99%"
                                            CssClass="btn btn-default" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr class="Ocultar">
                                    <td style="width: 6%" align="right">
                                        <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Black"
                                            Text="Llamada:" Font-Names="arial" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 10%">
                                                    <asp:FileUpload ID="fullamada" runat="server" Enabled="False"
                                                        Font-Bold="True" Height="19px" onchange="return AgregarLlamada();" Style="margin-left: 0px; display: none;"
                                                        Width="0%" />
                                                    <asp:ImageButton ID="imgupllamada" runat="server" Width="100%" Height="41px"
                                                        ImageUrl="~/imagenes/img_up.png" />
                                                </td>
                                                <td align="center">
                                                    <asp:Image ID="imgloading2" runat="server" Height="16px"
                                                        ImageUrl="~/imagenes/loading.gif" Style="display: none" Width="16px" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblllamada" runat="server" Font-Bold="True" ForeColor="Navy"
                                                        Style="display: none"></asp:Label>
                                                </td>
                                                <td style="width: 38.5%">
                                                    <asp:Button ID="btnescucharll" runat="server" Enabled="False" Font-Bold="True"
                                                        ForeColor="Black" Height="35px" Style="display: none" Text="Escuchar"
                                                        Width="100%"
                                                        CssClass="btn btn-default" />
                                                </td>
                                                <td style="width: 38.5%">
                                                    <asp:Button ID="btnquitarll" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="35px" Style="display: none" Text="Quitar" Width="100%"
                                                        CssClass="btn btn-default" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 6%">
                                        <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Black"
                                            Text="Croquis:" Font-Names="arial" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:FileUpload ID="fucroquis" runat="server" Enabled="False"
                                                        Height="19px" onchange="return Agregarcroquis();" Width="100%" />
                                                    <asp:ImageButton ID="imgupcroquis" runat="server" Height="40px" Width="100%" CssClass="Ocultar"
                                                        ImageUrl="~/imagenes/img_up.png" />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblruta" runat="server" Font-Bold="True" ForeColor="Navy"
                                                        Style="display: none"></asp:Label><asp:Image ID="imgloading" runat="server" Height="16px"
                                                            ImageUrl="~/imagenes/loading.gif" Style="display: none" Width="16px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnvercroquis" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Height="35px" Style="display: none;" Text="Ver Croquis"
                                                        Width="99%"
                                                        CssClass="btn btn-default" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btncambiarcroquis" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Height="35px" Style="display: none;" Text="Cambiar"
                                                        Width="99%"
                                                        CssClass="btn btn-default" />
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 8%">
                                        <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Size="7pt"
                                            ForeColor="Black" Style="line-height: 10px;"
                                            Text="No. Preautorización de cheque protegido GAMA." Font-Names="arial"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfolioCHP" runat="server" Font-Bold="True" ForeColor="Black"
                                            Height="30px" Width="100%"
                                            CssClass="form-control2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label ID="Label22" runat="server" ForeColor="Black"
                                            Style="font-weight: 700" Text="Observaciones:" Font-Names="arial"
                                            Font-Size="Small"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtobservaciones" runat="server" Height="50px" MaxLength="100"
                                            Style="resize: none;" TextMode="MultiLine" Width="100%"
                                            CssClass="form-control2"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblconfirmacion" runat="server" BackColor="Yellow"
                                Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Blue"
                                Text="El Cliente requiere confirmación de pago." Visible="False"
                                Width="100%" AssociatedControlID="btnconfirmar"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnconfirmar" runat="server"
                                CssClass="btn btn-default"
                                Font-Bold="True" ForeColor="Black" Height="35px"
                                OnClientClick="window.open('confirmacion_de_pago_mobile.aspx');return false;"
                                Text="Confirmar Pago" Width="100%" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblcheckplus" runat="server" Font-Bold="True" ForeColor="Red"
                                Text="Se aplicara en automatico un % si paga con cheque." Visible="False"
                                Width="100%" Font-Names="arial" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 44%">
                            <%--<asp:UpdateProgress ID="UpdateProgress5" runat="server" 
                        AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <center style="font-family:Arial;font-size:small;">
                                <img alt="loading" src="imagenes/loading_gif.gif" /><br />
                                Procesando...
                            </center>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                            <span id="div_guardando" style="display: none; text-align: center;">
                                <table>
                                    <tr>
                                        <td>
                                            <img src="imagenes/loading.gif" alt="" id="Img3" align="middle" height="40px" width="40px" />
                                        </td>
                                        <td valign="bottom" style="font-family: Arial; font-weight: bold; font-size: small; color: steelblue;">Guardando Pedido...</td>
                                    </tr>
                                </table>
                            </span>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="tbnguardarPP" runat="server" Enabled="False"   Text="Guardar Pre-Pedido" Width="100%"
                                CssClass="btn btn-primary" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnnuevoprepedido" runat="server" Text="Nuevo Pre-Pedido"
                                Width="100%"
                                CssClass="btn btn-sucess" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnsalir" runat="server" Text="Salir" Width="100%"
                                CssClass="btn btn-danger" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btncargarflete" runat="server" Text="Cargar Flete"
                                CssClass="Ocultar" />
                            <asp:Button ID="btncargarmaniobras" runat="server" Text="cargarmanio"
                                CssClass="Ocultar" />
                            <asp:Button ID="btnfoliomaniobras" runat="server" Text="foliomanio"
                                CssClass="Ocultar" />
                            <asp:Button ID="btndetallespedido" runat="server" Text="detallespedido"
                                CssClass="Ocultar" />
                            <asp:TextBox ID="txt_consignado" runat="server" CssClass="Ocultar" Width="">0</asp:TextBox>
                            <asp:Label ID="lblPopUp" runat="server" CssClass="Ocultar"></asp:Label>
                            <asp:Label ID="lblPopUp2" runat="server" CssClass="Ocultar"></asp:Label>
                            <table bgcolor="White"
                                style="border: 2px solid #000080; width: 99%; display: none; margin-left: 2px; background-color: white;">
                                <tr>
                                    <td align="left" style="padding-left: 60px;">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="Black"
                                            Text="Codigo del Articulo"></asp:Label>
                                    </td>
                                    <td align="center" bgcolor="White">
                                        <asp:Label ID="Label9" runat="server" CssClass="Ocultar" Font-Bold="True"
                                            ForeColor="Black" Text="Descripción"></asp:Label>
                                    </td>
                                    <td align="center" bgcolor="White">
                                        <asp:Label ID="Label10" runat="server" CssClass="Ocultar" Font-Bold="True"
                                            ForeColor="Black" Text="Uni. Med."></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label11" runat="server" CssClass="Ocultar" Font-Bold="True"
                                            ForeColor="Black" Text="Cantidad"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label12" runat="server" CssClass="Ocultar" Font-Bold="True"
                                            ForeColor="Black" Text="Precio"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnagregar" runat="server" CssClass="Ocultar" Enabled="False"
                                            Font-Bold="True" ForeColor="Black" Height="" Text="Agregar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle"></td>
                                    <td align="center">
                                        <asp:TextBox ID="txtdescripcion" runat="server" BackColor="White"
                                            CssClass="Ocultar" Enabled="False" EnableTheming="True" Font-Bold="True"
                                            Font-Size="Small" ForeColor="Black" Height=""
                                            onkeypress="cambiafoco(txtprecio);"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtUM" runat="server" CssClass="Ocultar" Enabled="False"
                                            Font-Bold="True" Font-Size="Small" ForeColor="Black" Height=""
                                            Width=""></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtcantidad" runat="server" CssClass="StyleCentrado Ocultar"
                                            Enabled="False" Font-Bold="True" Font-Size="Small" ForeColor="Black"
                                            Height="" onkeydown="return x(event)" Width=""></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtprecio" runat="server" AutoPostBack="false"
                                            CssClass="StyleCentrado Ocultar" Enabled="False" Font-Bold="True"
                                            Font-Size="Small" ForeColor="Black" Height="" MaxLength="10"
                                            onclick="this.select();" onkeydown="return x(event)"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btncancelararticulo" runat="server" CssClass="Ocultar"
                                            Enabled="False" Font-Bold="True" ForeColor="Black" Height=""
                                            Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="txtlistap" runat="server" CssClass="Ocultar" Width=""></asp:TextBox>
                            <asp:Button ID="btnref" runat="server" CssClass="Ocultar" Height=""
                                Text="ref" Width="" />
                            <asp:Button ID="btneditar_art" runat="server" CssClass="Ocultar" />
                            <asp:TextBox ID="txtidc_articulo" runat="server" CssClass="Ocultar">
                            </asp:TextBox>
                            <asp:Button ID="btnref_especif" runat="server" CssClass="Ocultar" />
                            <asp:Button ID="btnguardar_edit" runat="server" CssClass="Ocultar" />
                            <asp:Button ID="btncancelar_edit" runat="server" CssClass="Ocultar" />
                            <asp:TextBox ID="txtidOc" runat="server" CssClass="Ocultar" ForeColor="White">0</asp:TextBox>
                            <asp:TextBox ID="txtformapago" runat="server" CssClass="Ocultar"
                                Width=""></asp:TextBox>
                            <asp:Button ID="btnfocus" runat="server" CssClass="Ocultar" Height=""
                                Text="r" Width="" />
                            <asp:Button ID="btnbuscarart" runat="server" CssClass="Ocultar" Height=""
                                Text="buscar" Width="" />
                            <asp:Button ID="btnredirecting" runat="server" CssClass="Ocultar" Text="red" />
                            <asp:TextBox ID="txtfecha_max" runat="server" CssClass="Ocultar" Text=""></asp:TextBox>
                            <asp:TextBox ID="txt_oc_f" runat="server" CssClass="Ocultar" Height=""
                                Width=""></asp:TextBox>
                            <asp:Button ID="btnvalidaChP" runat="server" CssClass="Ocultar"
                                ForeColor="White" Height="" Text="ChekP" Width="" />
                            <asp:Button ID="btnagregarllamada" runat="server" CssClass="Ocultar"
                                ForeColor="White" Height="" Text="AgregarLl" Width="" />
                            <asp:Button ID="btnsf" runat="server" CssClass="Ocultar" ForeColor="White"
                                Height="" Text="SF" />
                            <asp:TextBox ID="oc" runat="server" CssClass="Ocultar" Width=""></asp:TextBox>
                            <asp:TextBox ID="croquis" runat="server" CssClass="Ocultar" Width=""></asp:TextBox>
                            <asp:Button ID="btnagregarcroquis" runat="server" CssClass="Ocultar"
                                ForeColor="White" Height="" Text="AgregarCroquis" Width="" />
                            <asp:Button ID="btnrc" runat="server" CssClass="Ocultar" ForeColor="White"
                                Height="" Text="RC" ToolTip=" " />
                            <asp:Button ID="btncalc_iva" runat="server" CssClass="Ocultar" Height=""
                                Text="Calcular_Iva" Width="" />
                            <div id="popup" runat="server" class="popup"
                                style="display: none; border: 2px solid Black; width: 0px; height: 0px; padding-left: 15px;">
                                <div>
                                    <asp:Panel ID="panel_consignado" runat="server">
                                        <table align="left" style="width: 100%; height: 0px; background-color: white;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label31" runat="server" ForeColor="Black" Text="Calle:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtcalle" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="" Width=""></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label35" runat="server" ForeColor="Black" Text="Municipio:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtmunicipio" runat="server" BackColor="#CCCCCC"
                                                        Enabled="False" Font-Bold="True" ForeColor="Black" Width=""></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr align="right">
                                                <td align="right">
                                                    <asp:Label ID="Label32" runat="server" ForeColor="Black" Text="Numero:"></asp:Label>
                                                </td>
                                                <td>
                                                    <table style="width: 100%; background-color: white;">
                                                        <tr>
                                                            <td align="left"
                                                                style="background-color: white; padding-left: 0px; margin-left: 0px;">
                                                                <asp:TextBox ID="txtnumero" runat="server" Font-Bold="True" ForeColor="Black"
                                                                    Height="" Width=""></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label33" runat="server" ForeColor="Black" Text="C.P."></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCP" runat="server" Font-Bold="True" ForeColor="Black"
                                                                    Width="">0</asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label38" runat="server" ForeColor="Black" Text="Estado:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtestado" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                        Font-Bold="True" ForeColor="Black" Width=""></asp:TextBox>
                                                </td>
                                                <td align="left"></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table style="width: 71%;">
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnagregarcol" runat="server" CssClass="Ocultar"
                                                                    Height="" ImageUrl="~/imagenes/btn/more.png" Width="" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnbuscarcol" runat="server" Height="21px"
                                                                    ImageUrl="~/imagenes/btn/icon_buscar.png" OnClientClick="AbreBuscarColonia()"
                                                                    Style="margin-left: 0px" Width="" />
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label34" runat="server" ForeColor="Black" Text="Colonia:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table style="width: 0%; height: 0px; background-color: white;">
                                                        <tr>
                                                            <td align="left" style="padding-left: 0px; margin-left: 0px;">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td align="left" style="padding-left: 0px; margin-left: 0px;">
                                                                            <asp:TextBox ID="txtcolonia" runat="server" BackColor="#CCCCCC"
                                                                                CausesValidation="True" Enabled="False" Font-Bold="True" ForeColor="Black"
                                                                                Width=""></asp:TextBox>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Label ID="lblrestriccion" runat="server" Font-Bold="True"
                                                                                Font-Italic="True"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label40" runat="server" ForeColor="Black" Text="País:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtpais" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                        Font-Bold="True" ForeColor="Black" Width=""></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtidc_colonia" runat="server" CssClass="Ocultar" Width="">0</asp:TextBox>
                                                    <asp:TextBox ID="txtzm" runat="server" CssClass="Ocultar" Width=""></asp:TextBox>
                                                    <asp:TextBox ID="txtrestriccion" runat="server" CssClass="Ocultar"
                                                        Width=""></asp:TextBox>
                                                    <asp:TextBox ID="txtFolio" runat="server" CssClass="Ocultar" Height=""
                                                        Width=""></asp:TextBox>
                                                    <asp:TextBox ID="txtFolioOc" runat="server" CssClass="Ocultar" Width=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label36" runat="server" ForeColor="Black"
                                                        Text="Restricciones Colonia:" Width="0px"></asp:Label>
                                                </td>
                                                <td>
                                                    <table align="left"
                                                        style="border-color: #FFFFFF; width: 0%; height: 0px; background-color: white;">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:CheckBox ID="chkton" runat="server" Enabled="False" ForeColor="Black" />
                                                            </td>
                                                            <td></td>
                                                            <td align="right">
                                                                <asp:Label ID="Label39" runat="server" ForeColor="Black" Text="Toneladas:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txttoneladas" runat="server" Enabled="False" Font-Bold="True"
                                                        ForeColor="Black" Width=""></asp:TextBox>
                                                </td>
                                                <td align="left" style="border-color: #FFFFFF">
                                                    <asp:ImageButton ID="imgubicacion" runat="server" Height="0px"
                                                        ImageUrl="~/imagenes/maps.jpg" Visible="False" Width="35px" />
                                                </td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtproy" runat="server" CssClass="Ocultar" Width="">0</asp:TextBox>
                                                    <asp:TextBox ID="txtnombrerecoge" runat="server" CssClass="" Width=""></asp:TextBox>
                                                    <asp:TextBox ID="txtpaternor" runat="server" CssClass="" Height="" Width=""></asp:TextBox>
                                                    <asp:TextBox ID="txtmaternor" runat="server" CssClass="" Width=""></asp:TextBox>
                                                    <asp:TextBox ID="txtsucursalr" runat="server" CssClass="" Width="">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right"></td>
                                                <td></td>
                                                <td></td>
                                                <td align="center"></td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtplazo" runat="server" CssClass="Ocultar" Height=""
                                                        Width="0px"></asp:TextBox>
                                                    <asp:TextBox ID="txtformaP" runat="server" CssClass="Ocultar" Height=""
                                                        Width="0px"></asp:TextBox>
                                                    <asp:TextBox ID="txtotro" runat="server" CssClass="Ocultar" Height=""></asp:TextBox>

                                                    <asp:TextBox ID="txtcminima" runat="server" CssClass="Ocultar" Height=""
                                                        Width="0px"></asp:TextBox>
                                                    <asp:TextBox ID="txtcontacto" runat="server" CssClass="Ocultar" Height=""
                                                        Width="0px"></asp:TextBox>
                                                    <asp:TextBox ID="txttelefono" runat="server" CssClass="Ocultar" Height=""
                                                        Width="0px"></asp:TextBox>
                                                    <asp:TextBox ID="txtcorreo" runat="server" CssClass="Ocultar" Height=""
                                                        Width="0px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                            <asp:Label ID="lbliva" runat="server" Font-Bold="True" CssClass="Ocultar" Font-Names="arial"
                                Font-Size="Small" ForeColor="Black" Text="I.V.A." Visible="False"></asp:Label>
                            <asp:TextBox ID="txtsubiva" runat="server"
                                CssClass="Ocultar"
                                Enabled="False" Height="" Visible="False" Width="100%"></asp:TextBox>
                            <asp:TextBox ID="txtivadescuento" runat="server"
                                CssClass="Ocultar"
                                Enabled="False" ForeColor="Red" Height="" Visible="False" Width="100%"></asp:TextBox>
                            <asp:TextBox ID="txtiva" runat="server"
                                CssClass="Ocultar"
                                Enabled="False" Height="" Visible="False" Width="100%"></asp:TextBox>
                            <asp:TextBox ID="txtsubt" runat="server"
                                CssClass="Ocultar"
                                Enabled="False" Height="" Visible="False" Width="100%"></asp:TextBox>
                            <asp:TextBox ID="txtsubtotaldescuento" runat="server"
                                CssClass="Ocultar"
                                Enabled="False" ForeColor="Red" Height="" Visible="False" Width="100%"></asp:TextBox>
                            <asp:TextBox ID="txtsubtotal" runat="server"
                                CssClass="Ocultar"
                                Enabled="False" Height="" Visible="False" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>


                <asp:Panel ID="Panel2" runat="server" Style="height: 0px; width: 0px;">
                </asp:Panel>



                <%--Consignado--%>











                <%--//////////--%>
            </ContentTemplate>

            <Triggers>
                <asp:PostBackTrigger ControlID="btnagregarcroquis" />
                <asp:PostBackTrigger ControlID="btnagregarllamada" />
                <asp:PostBackTrigger ControlID="btneditar_art" />
            </Triggers>

        </asp:UpdatePanel>
    </div>
</asp:Content>

