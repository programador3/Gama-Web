<%@ Page Title="CheckPlus" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="check_plus_pre.aspx.cs" Inherits="presentacion.check_plus_pre_m" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .Ocultar {
            display:none;
        }
        .form-control3 {
            width: 100%;
            display: block;
            padding: 6px 12px;
            font-size: 11px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        }
       </style>
    <script type="text/javascript">
        function datos_persona() {
            var cbonombres = document.getElementById('<%=cbonombres.ClientID%>');
              var gridnombres = document.getElementById('<%=gridnombres.ClientID%>');
              if (cbonombres.options.length > 0) {
                  var idc_dirchekplus = cbonombres.options[cbonombres.selectedIndex].value;
                  var idc;
                  if (idc_dirchekplus > 0 && gridnombres != null) {
                      for (var i = 1; i <= gridnombres.rows.length - 1; i++) {
                          idc = gridnombres.rows[i].cells[6].textContent;
                          if (idc == idc_dirchekplus) {
                              var txtcalle = document.getElementById('<%=txtcalle.ClientID%>');
                        var txtnombrepersona = document.getElementById('<%=txtnombrepersona.ClientID%>');
                        var txtclave = document.getElementById('<%=txtclave.ClientID%>');
                        var txtnumero = document.getElementById('<%=txtnumero.ClientID%>');
                        var txtFolio2 = document.getElementById('<%=txtfolio2.ClientID%>');
                        var txtidc_colonia = document.getElementById('<%=txtidc_colonia.ClientID%>');

                        txtcalle.value = gridnombres.rows[i].cells[0].textContent;
                        txtnombrepersona.value = gridnombres.rows[i].cells[5].textContent;
                        txtclave.value = gridnombres.rows[i].cells[4].textContent;
                        txtnumero.value = gridnombres.rows[i].cells[1].textContent;
                        txtFolio2.value = gridnombres.rows[i].cells[2].textContent;
                        txtidc_colonia.value = gridnombres.rows[i].cells[3].textContent;
                        break;
                    }
                }
            }
        }

    }
    function datos() {
        var idc_cliente = document.getElementById('<%=txtidc_cliente.ClientID%>').value;
        var cliente = document.getElementById('<%=txtcliente.ClientID%>').value;
        var rfc = document.getElementById('<%=txtrfc.ClientID%>').value;
        var cve = document.getElementById('<%=txtcve.ClientID%>').value;
        var encodedString = btoa(idc_cliente);
        window.open('agregar_clientes_chekplus.aspx?id=' + encodedString + '&cliente=' + cliente + '&rfc=' + rfc + '&cve=' + cve)
        return false;
    }

    function ver_detalles() {
        var cbonombres = document.getElementById('<%=cbonombres.ClientID%>');
        var idc_cliente = document.getElementById('<%=txtidc_cliente.ClientID%>').value;
        if (cbonombres.options.length > 0 && idc_cliente > 0) {
            var cdi_dir = cbonombres.options[cbonombres.selectedIndex].value;
            window.open('datos_ch_p.aspx?cdi_dir=' + cdi_dir + '&cdi=' + idc_cliente);
        }
        return false;

    }

    function validar_length(txt, len) {
        if (txt.value.length > len) {
            txt.value = txt.value.substring(0, len);
            return false;
        }
    }
    function guardar() {
        var monto = document.getElementById("<%=txtmonto.ClientID%>").value;
        var persona = document.getElementById("<%=txtnombrepersona.ClientID%>").value;
        var cbocuentas = document.getElementById("<%=cbocuentas.ClientID%>");

        // var check_id;
        //var check;
        //var checked_count = false;
        if (cbocuentas == null) {
            alert("Seleccione una Cuenta.");
            return false;
        }
        if (cbocuentas.options.length <= 0) {
            alert("Seleccione una Cuenta.");
            return false;
        }



        //        
        //         for (var r = 1; r <= tabla.rows.length-1; r++) 
        //         {
        //            check_id= tabla.rows[r].cells[2].lastElementChild.id;
        //            check= document.getElementById(check_id);
        //           
        //                if(check.checked==true)
        //                {
        //                   checked_count=true;                   
        //                }
        //                    
        //         } 


        if (monto == "" || monto <= 0) {
            alert("El Monto no Puede ser Cero");
            return false;
        }
        else if (persona == "") {
            alert("El Nombre de la Persona que se Identifica no Puede Quedar Vacio.")
            return false;
        }
        document.getElementById("<%=btng.ClientID%>").click();
        return false; //MIC 14-05-2015 add
    }




    function buscar_cliente() {
        //TINY.box.show({iframe:'busc_Cliente.aspx',boxid:'frameless',width:630,height:265,fixed:false,maskid:'bluemask',maskopacity:40})
        $('#div_buscar').show();
        return false;
    }

    function cerrar() {
        //TINY.box.hide();
        //window.close();
        document.getElementById("<%=btncuentas.ClientID%>").click();
        return false;
    }
    function cerrar2() {
        TINY.box.hide();
        return false;
    }

    function guardar_cuenta() {

        var bancos = document.getElementById("<%=cbobancos.ClientID%>");
        var idc_cliente = document.getElementById("<%=txtidc_cliente.ClientID%>").value;
        var cuenta = document.getElementById("<%=txtcuenta.ClientID%>").value;

        if (idc_cliente == "" || idc_cliente == "0") {
            alert("Error al Tratar de Obtener el Cliente.");
            return false;
        }
        if (bancos.selectedIndex < 0) {
            alert("No se ha Seleccionado el Banco.");
            return false;
        }

        if (cuenta == "") {
            alert("No se Indico el Numero de Cuenta.");
            return false;
        }
        else if (cuenta.length < 4 || cuenta.length > 15) {
            alert("Cuenta Invalida");
            return false;
        }

        document.getElementById("<%=btnguarda_cuenta.ClientID%>").click();
        return false; //MIC 14-05-2015 add

    }


    function soloNumeros(e) {
        key = e.keyCode || e.which;
        tecla = String.fromCharCode(key).toLowerCase();
        letras = "0123456789";
        especiales = [8, 37, 39, 46, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105];

        tecla_especial = false
        if (key == 13) { guardar_cuenta(); }
        for (var i in especiales) {
            if (key == especiales[i]) {
                tecla_especial = true;
                break;
            }
        }
        var index = letras.indexOf(tecla);
        if (index != -1 || tecla_especial == true) {
            return true;
        }
        else {
            return false;
        }

    }

    function CheckBoxes(index, tabla) {
        var check_id;
        var check;
        for (var r = 1; r <= tabla.rows.length - 1; r++) {
            check_id = tabla.rows[r].cells[2].lastElementChild.id;
            check = document.getElementById(check_id);
            if (r == index) {
                if (check.checked == true) {
                    tabla.rows[r].cells[1].style.backgroundColor = 'Gainsboro';
                    tabla.rows[r].cells[0].style.backgroundColor = 'Gainsboro';
                    tabla.rows[r].cells[2].style.backgroundColor = 'Gainsboro';
                    document.getElementById("<%=txtidc_cuenta.ClientID%>").value = tabla.rows[r].cells[3].textContent;
                    }
                    else {
                        tabla.rows[r].cells[2].style.backgroundColor = '';
                        tabla.rows[r].cells[1].style.backgroundColor = '';
                        tabla.rows[r].cells[0].style.backgroundColor = '';
                    }
                }
                else {
                    check.checked = false;
                    tabla.rows[r].cells[2].style.backgroundColor = '';
                    tabla.rows[r].cells[1].style.backgroundColor = '';
                    tabla.rows[r].cells[0].style.backgroundColor = '';
                }

            }
        }


        function soloNumeros2(e, tipo) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            if (tipo == true) {
                letras = "0123456789";
                especiales = [8, 37, 39, 46, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 110, 190];
            }
            else {
                letras = "0123456789";
                especiales = [8, 37, 39, 46, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105];
            }
            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            var index = letras.indexOf(tecla);
            if (index != -1 || tecla_especial == true) {
                return true;
            }
            else {
                return false;
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

        function selecciona_datos() {
            var idc_cliente = document.getElementById("<%=txtidc_cliente.ClientID%>").value;
             var cliente = document.getElementById("<%=txtcliente.ClientID%>").value;
             var cve = document.getElementById("<%=txtcve.ClientID%>").value;
             var rfc = document.getElementById("<%=txtrfc.ClientID%>").value;
             TINY.box.show({ iframe: 'selecciona_cliente_chekplus.aspx?id=' + idc_cliente + '&cliente=' + cliente + '&cve=' + cve + '&rfc=' + rfc, boxid: 'frameless', width: 660, height: 400, fixed: false, maskid: 'bluemask', maskopacity: 40 })
             return false;
         }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class="page-header">Solicitar Pre-Autorización de Check Plus</h2>
    <div class="row">      
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;
                <asp:Label ID="Label2" runat="server" Text="Folio Gama:"></asp:Label>
            </strong></h4>
            <asp:TextBox ID="txtfolio" runat="server" onfocus="this.blur();"
                BackColor=" Gainsboro" ReadOnly="true" Font-Bold="True" ForeColor="Navy" Style="display: inline; width: 100%; font-size: 11px;"
                CssClass="form-control"></asp:TextBox>
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" CssClass="Ocultar"
                Font-Size="Small" ForeColor="Blue" Text="Plazo:"></asp:Label>
            <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;
                <asp:Label ID="Label4" runat="server" Text="Nombre:"></asp:Label></strong></h4>
            <asp:TextBox ID="txtcliente" ReadOnly="true" runat="server" Width="100%" onfocus="this.blur();" Style="width: 100%; font-size: 11px;"
                CssClass="form-control"></asp:TextBox>
            <h4><strong><i class="fa fa-credit-card" aria-hidden="true"></i>&nbsp;
                <asp:Label ID="Label3" runat="server" Width="38%" Text="RFC:"></asp:Label>
                <asp:Label ID="Label5" runat="server" Width="49%" Text="Cve:"></asp:Label>
            </strong></h4>



            <asp:TextBox ID="txtrfc" ReadOnly="true" Style="font-size: 11px;" runat="server" onfocus="this.blur();"
                CssClass="form-control2" Width="49%"></asp:TextBox>
            <asp:TextBox ID="txtcve" ReadOnly="true"
                runat="server" onfocus="this.blur();" Style="display: inline; width: 49%; font-size: 11px;"
                CssClass="form-control2"></asp:TextBox>

            <asp:UpdatePanel ID="ddd" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkbuscaraceptar" />
                </Triggers>
                <ContentTemplate>
                    <asp:LinkButton ID="lnkbuscarcliente" CssClass="btn btn-default btn-block" runat="server" OnClick="lnkbuscarcliente_Click">Buscar Cliente</asp:LinkButton>
                    <div class=" col-lg-12" style="background-color: white; border: 1px solid gray; padding: 10px;" id="div_buscar" visible="false" runat="server">
                        <h5 style="text-align: center;"><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Buscar Cliente</strong></h5>
                        <asp:TextBox AutoPostBack="true" ReadOnly="false" OnTextChanged="lnkbuscar_Click" Width="80%" runat="server" ID="txtbuscarcliente" CssClass="form-control2"
                            placeholder="Buscar Cliente"></asp:TextBox>
                        <asp:LinkButton ID="lnkbuscar" runat="server" CssClass="btn btn-info" Width="18%" OnClick="lnkbuscar_Click"> <i class="fa fa-search" aria-hidden="true"></i>&nbsp;</asp:LinkButton>
                        <asp:DropDownList ID="cboclientes" runat="server" CssClass=" form-control" Width="100%"></asp:DropDownList>
                        <asp:LinkButton ID="lnkbuscaraceptar" CssClass="btn btn-primary btn-block" runat="server" OnClick="lnkbuscaraceptar_Click">Seleccionar Cliente</asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-folder-open" aria-hidden="true"></i>&nbsp;
                <asp:Label ID="Label7" runat="server" Text="Seleccionar Cuenta"></asp:Label></strong></h4>

            <asp:DropDownList ID="cbocuentas" runat="server" Width="100%" CssClass="form-control">
            </asp:DropDownList>
            <br />
            <div style="background-color: white; border: 1px solid gray; padding: 10px;">
                <h4 style="text-align: center;"><strong>
                    <asp:Label ID="Label8" runat="server" Text="Agregar Nueva Cuenta"></asp:Label></strong></h4>

                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="Small" ForeColor="Black" Text="Banco:"></asp:Label>

                <asp:DropDownList ID="cbobancos" runat="server" Width="100%" CssClass="form-control"
                    ForeColor="Black" Enabled="False">
                </asp:DropDownList>

                <br />

                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="Small" ForeColor="Black" Text="Cuenta:"></asp:Label>

                <asp:TextBox ID="txtcuenta" runat="server" Enabled="False" type="number"
                    CssClass="form-control"></asp:TextBox>


                <br />

                <asp:Button ID="btnguardarcuenta" runat="server"
                    Text="Agregar Cuenta" Width="100%" Enabled="False"
                    CssClass="btn btn-success btn-block" />
            </div>
            <asp:Button ID="btnguarda_cuenta" runat="server" Text="Button"
                CssClass="Ocultar" OnClick="btnguarda_cuenta_Click" />
            <asp:Button ID="btncuentas" runat="server" Height="20px" Text="Button"
                Width="20px" CssClass=" Ocultar" OnClick="btncuentas_Click" />



           <h4><strong><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;
                <asp:Label ID="Label11" runat="server" Text="Monto del Cheque:"></asp:Label>

               </strong></h4>
            <asp:TextBox ID="txtmonto" runat="server" Width="100%" type="number" step="0.01" min="0.01"
                Enabled="False"
                CssClass="form-control"></asp:TextBox>


          <h4><strong>
                <asp:Label ID="Label12" runat="server" Text="Nombre de la Persona:"></asp:Label>
              </strong></h4>

            <asp:DropDownList ID="cbonombres" Width="100%" CssClass="form-control"
                runat="server">
            </asp:DropDownList>
            <br />
            <div style="text-align: center;">
                <asp:ImageButton ID="imgdetalles" runat="server" Height="33px"
                    ImageUrl="imagenes/File_info48.png" Width="35px"
                    OnClientClick="return ver_detalles()" />
                <asp:ImageButton ID="imgnuevo" runat="server" Height="30px" Style="margin-left: 20px"
                    ImageUrl="imagenes/Add_user48.png" Width="35px" />
            </div>


            <br />

            <asp:DataGrid ID="gridnombres" Style="display: none;" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundColumn DataField="calle" HeaderText="calle"></asp:BoundColumn>
                    <asp:BoundColumn DataField="numero" HeaderText="numero"></asp:BoundColumn>
                    <asp:BoundColumn DataField="folio_elector" HeaderText="Folio2"></asp:BoundColumn>
                    <asp:BoundColumn DataField="idc_colonia" HeaderText="idc_colonia"></asp:BoundColumn>
                    <asp:BoundColumn DataField="clave_elector" HeaderText="clave"></asp:BoundColumn>
                    <asp:BoundColumn DataField="nombre" HeaderText="nombre"></asp:BoundColumn>
                    <asp:BoundColumn DataField="idc_dirchekplus" HeaderText="idc"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>

            <div style="display: none;">
                <asp:TextBox ID="txtnombrepersona" runat="server" Style="display: inline; width: 80%;"
                    onfocus="this.blur();"
                    CssClass="form-control"></asp:TextBox>

                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="Small" ForeColor="Black" Text="Clave de Elector:"></asp:Label>

                <asp:TextBox ID="txtclave" runat="server" onfocus="this.blur();"
                    CssClass="form-control"></asp:TextBox>

                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="Small" ForeColor="Black" Text="Folio de Elector:"></asp:Label>

                <asp:TextBox ID="txtfolio2" runat="server"
                    onfocus="this.blur();"
                    CssClass="form-control"></asp:TextBox>

                <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="Small" ForeColor="Black" Text="Calle:"></asp:Label>

                <asp:TextBox ID="txtcalle" runat="server" onfocus="this.blur();"
                    CssClass="form-control"></asp:TextBox>

                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="Small" ForeColor="Black" Text="Numero:"></asp:Label>

                <asp:TextBox ID="txtnumero" runat="server" onfocus="this.blur();"
                    CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Button ID="btnguardar" runat="server" Text="Guardar"
                Enabled="False"
                CssClass="btn btn-primary btn-block" />
            <asp:Button ID="btncancelar" runat="server" Text="Limpiar Formulario"
                Enabled="False"
                CssClass="btn btn-info btn-block" OnClick="btncancelar_Click" />
            <asp:Button ID="btnsalir" runat="server" Text="Salir"
                CssClass="btn btn-danger btn-block" OnClick="btnsalir_Click" />

            <asp:TextBox ID="txtidc_cliente" runat="server" CssClass="Ocultar"></asp:TextBox>
            <asp:TextBox ID="txtidc_colonia" runat="server" CssClass="Ocultar"></asp:TextBox>
            <asp:Button ID="btng" runat="server" Text="guardar" CssClass=" Ocultar" OnClick="btng_Click" />
            <asp:TextBox ID="txtidc_cuenta" runat="server" CssClass="Ocultar"></asp:TextBox>
            <asp:Button ID="btnrefresh" CssClass="Ocultar" runat="server" OnClick="btnrefresh_Click" />

        </div>
    </div>
</asp:Content>
