<%@ Page Title="Ficha" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="ficha_cliente_m.aspx.cs" Inherits="presentacion.ficha_cliente_m" %>
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
       
        window.onload = obtenUbicacion;
        function obtenUbicacion() {
            getCoordenadas();

        }
        function getCoordenadas() {
            var options = {
                enableHighAccuracy: true,
                timeout: 6000,
                maximumAge: 0
            };

            navigator.geolocation.getCurrentPosition(success, error, options);

            function success(position) {
                var coordenadas = position.coords;
                var lat = coordenadas.latitude;
                var lon = coordenadas.latitude;
                 $('#<%=oclatitud.ClientID%>').val(lat);
                $('#<%=oclongitud.ClientID%>').val(lon);

            };

            function error(error) {
                if (error.code != "3")
                {
                    if (error.code == "1") {

                        alert('ERROR EN EL GPS(' + error.code + '): EL NAVEGADOR ACTUAL NO ES COMPATIBLE CON EL GPS, UTILIZE EL NAVEGADOR POR DEFECTO DE SU EQUIPO CELULAR');
                    } else {
                        alert('ERROR EN EL GPS(' + error.code + '): ' + error.message);
                    }
                }
            };

        }
          function ver_detalles() {
            var tabla = document.getElementById("<%=gridcontactos.ClientID%>");
            var cell;
            if (tabla == null) {
                return false;
            }
              var mybutton=document.getElementById ("<%=LinkButton6.ClientID %>");
            if (tabla.rows[0].cells[1].className == "Ocultar") {
                mybutton.innerHTML = "Ver Menos";
                tabla.rows[0].cells[1].className = "";
                tabla.rows[0].cells[2].className = "";
                tabla.rows[0].cells[6].className = "";
                tabla.rows[0].cells[7].className = "";
                tabla.rows[0].cells[8].className = "";
                tabla.rows[0].cells[9].className = "";
                tabla.rows[0].cells[10].className = "";
                for (var i = 1; i <= tabla.rows.length - 1; i++) {
                    tabla.rows[i].cells[1].className = "";
                    tabla.rows[i].cells[2].className = "";
                    tabla.rows[i].cells[6].className = "";
                    tabla.rows[i].cells[7].className = "";
                    tabla.rows[i].cells[8].className = "";
                    tabla.rows[i].cells[9].className = "";
                    tabla.rows[i].cells[10].className = "";
                }
            }
            else {
                mybutton.innerHTML = "Ver Mas";
                tabla.rows[0].cells[1].className = "Ocultar";
                tabla.rows[0].cells[2].className = "Ocultar";
                tabla.rows[0].cells[6].className = "Ocultar";
                tabla.rows[0].cells[7].className = "Ocultar";
                tabla.rows[0].cells[8].className = "Ocultar";
                tabla.rows[0].cells[9].className = "Ocultar";
                tabla.rows[0].cells[10].className = "Ocultar";
                for (var i = 1; i <= tabla.rows.length - 1; i++) {
                    tabla.rows[i].cells[1].className = "Ocultar";
                    tabla.rows[i].cells[2].className = "Ocultar";
                    tabla.rows[i].cells[6].className = "Ocultar";
                    tabla.rows[i].cells[7].className = "Ocultar";
                    tabla.rows[i].cells[8].className = "Ocultar";
                    tabla.rows[i].cells[9].className = "Ocultar";
                    tabla.rows[i].cells[10].className = "Ocultar";
                }
            }
            return false;
        }
        function AbSitioCliente(Label) {
            if (Label.innerHTML != '') {
                var y = (screen.height - 285) / 2;
                var x = (screen.width - 390) / 2;
                var ruta = 'http://' + Label.innerHTML;
                hidden = open(ruta, 'NewWindow', "width=390px,height=285px,top=" + y + ",left=" + x + ",Menubar=no,Scrollbars=no,location=no,status=no,title=''");
                return false;
            }
        }
          <%--function estado(index) {
            index++;
            var tabla = document.getElementById("<%=gridcontactos.ClientID%>");
            var imgedit_contacto = document.getElementById("<%=imgedit_contacto.ClientID%>");
            for (var i = 1; i <= tabla.rows.length - 1; i++) {
                if (index == i) {
                    tabla.rows[i].className = "SelectedRowStyle";
                    if (tabla.rows[i].cells[13].textContent == "True") {
                        imgedit_contacto.className = "";
                        var txtidc_telcli = document.getElementById("<%=txtidc_telcli.ClientID%>");
                        txtidc_telcli.value = tabla.rows[i].cells[11].textContent;
                    }
                    else {
                        imgedit_contacto.className = "Ocultar";
                    }
                }
                else {
                    tabla.rows[i].className = "AltRowStyle";
                }
            }
        }--%>

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class=" page-header">
        Ficha del Cliente
    </h2>
     <asp:HiddenField ID="oclatitud" runat="server" />
                <asp:HiddenField ID="oclongitud" runat="server" />
    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-list" aria-hidden="true"></i>&nbsp;Opciones</strong></h4>
            <asp:DropDownList ID="cboopciones" runat="server" CssClass="form-control">
                <asp:ListItem Value="8">Generar Pedido</asp:ListItem>
                <asp:ListItem Value="6">Negociacion con Clientes</asp:ListItem>
                <asp:ListItem Value="5">Compromisos</asp:ListItem>
                <asp:ListItem Value="1">Agendar Llamada</asp:ListItem>
                <asp:ListItem Value="4">Alta Inconvenientes</asp:ListItem>
                <asp:ListItem Value="9">Cotizaciones</asp:ListItem>
                <asp:ListItem Value="10">Check Plus</asp:ListItem>
                <asp:ListItem Value="12">Agregar Tareas</asp:ListItem>
                <asp:ListItem Value="11">Revisar Tareas</asp:ListItem>
            </asp:DropDownList>
             <asp:LinkButton ID="lnkseleccionar" CssClass="btn btn-primary btn-block" runat="server" OnClick="lnkseleccionar_Click">
                Seleccionar</asp:LinkButton>
        </div>
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-map-marker" aria-hidden="true"></i>&nbsp;Registrar Visita</strong></h4>
             <asp:LinkButton ID="lnkyaregis" CssClass="btn btn-success btn-block" runat="server">
               <i class="fa fa-map-marker" aria-hidden="true"></i>&nbsp;Visita Registrada</asp:LinkButton>
            <asp:LinkButton ID="lnkregistrarvisita" CssClass="btn btn-danger btn-block" runat="server" OnClick="lnkregistrarvisita_Click">
                <i class="fa fa-map-marker" aria-hidden="true"></i>&nbsp;Registrar</asp:LinkButton>
        </div>
        <div id="divgrupos" runat="server" class=" col-lg-12">
            <h4><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Clientes del Grupo</strong></h4>
            <asp:DropDownList ID="cbogrupos" runat="server"
                CssClass=" form-control" AutoPostBack="True">
            </asp:DropDownList>
        </div>
        <div class="col-lg-12">
            <div style="border: 2px solid #0074BA; background-color:white; width:100%; padding:5px;">
                <h4 style="text-align:center;"><strong><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Articulos</strong></h4>
                 <asp:LinkButton ID="LinkButton1" CssClass="btn btn-info btn-block" runat="server" OnClick="LinkButton1_Click">
                <i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;Master</asp:LinkButton>
                 <asp:LinkButton ID="LinkButton2" CssClass="btn btn-info btn-block" runat="server" OnClick="LinkButton2_Click">
                <i class="fa fa-hand-o-right" aria-hidden="true"></i>&nbsp;Negociados</asp:LinkButton>
                 <asp:LinkButton ID="LinkButton3" CssClass="btn btn-info btn-block" runat="server" OnClick="LinkButton3_Click">
                <i class="fa fa-usd" aria-hidden="true"></i>&nbsp;A Vender</asp:LinkButton>
                 <asp:LinkButton ID="LinkButton4" CssClass="btn btn-info btn-block" runat="server" OnClick="LinkButton4_Click">
                <i class="fa fa-align-justify" aria-hidden="true"></i>&nbsp;Historial de Ventas(6 Meses)</asp:LinkButton>
            </div>
        </div>
        <div class="col-lg-12">
            <div style="border: 2px solid #757575; background-color: white; width: 100%; padding: 10px;">
                <h4 style="text-align:center;"><strong><i class="fa fa-info-circle" aria-hidden="true"></i>&nbsp;Información del Cliente</strong></h4>
               
                <table style="width: 100%; font-size: 11px;">
                    <tr>
                        <td align="right" style="width: 8%">
                            <asp:Label ID="Label10" runat="server" Text="Cliente:" ForeColor="Black"
                                Font-Bold="True" Font-Names="arial"></asp:Label>
                        </td>
                        <td style="width: 90%">
                            <asp:TextBox ID="txtcliente" runat="server" Width="100%"
                                ReadOnly="True" onfocus="this.blur()"
                                CssClass="form-control3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="ID: " ForeColor="Black"
                                 Font-Names="arial" CssClass="Ocultar"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtid" runat="server" Width="100%" onfocus="this.blur();" Style="display: none !important;"
                                CssClass="form-control3 "></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="RCF: " ForeColor="Black"
                                Font-Bold="True" Font-Names="arial"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtrfc" runat="server" Width="80%" Style="display: inline !important;"
                                onfocus="this.blur()"
                                CssClass="form-control3"></asp:TextBox>
                            <asp:TextBox ID="txtcveadicional" runat="server" Width="15%" onfocus="this.blur()"
                                ReadOnly="True" Style="display: inline !important;"
                                CssClass="form-control3"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="labelstatus" runat="server" Text="Status: " ForeColor="Black"
                                Font-Bold="True" Font-Names="arial" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtstatus" runat="server" Width="100%"
                                ReadOnly="True" onfocus="this.blur()" Font-Bold="True"
                                CssClass="form-control3"
                                Visible="False"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label11" runat="server" Text="Tipo Pago: " Font-Bold="True"
                                ForeColor="Blue" Font-Names="arial"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttipopago" runat="server" Width="100%"
                                Font-Bold="True" ForeColor="Blue"
                                onfocus="this.blur()" ReadOnly="True"
                                CssClass="form-control3"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblgrupo" runat="server" Text="Grupo Cliente:"
                                ForeColor="Black" Font-Bold="True" Font-Names="arial"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtgrupocliente" runat="server" Width="80%" Style="display: inline !important;"
                                onfocus="this.blur()" ReadOnly="True"
                                CssClass="form-control3"></asp:TextBox>
                            <asp:TextBox ID="txtnumerogrupo" runat="server" Width="15%"
                                onfocus="this.blur()" ReadOnly="True" Style="display: inline !important;"
                                CssClass="form-control3"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label13" runat="server" Text="Agente: " ForeColor="Black"
                                Font-Bold="True" Font-Names="arial" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtagente" runat="server"
                                Width="100%" onfocus="this.blur()" ReadOnly="True"
                                CssClass="form-control3 Ocultar"
                                Visible="True"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" Text="Credito:" ForeColor="Black"
                                Font-Bold="True" Font-Names="arial"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcredito" runat="server" onfocus="this.blur();" Style="display: inline;"
                                ReadOnly="True" Width="25%"
                                CssClass="form-control3"
                                Font-Bold="True" Font-Names="arial"></asp:TextBox>
                            <asp:Button ID="btnpromociones" runat="server" Height="35px" Text="Promociones"
                                Width="70%" Font-Bold="True" Style="display: inline; background-image: -webkit-linear-gradient( rgb(240, 250, 2),rgb(235, 235, 227));"
                                CssClass="btn" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblcredito" runat="server" Text="Credito Disponible:" ForeColor="Black"
                                Font-Bold="True" Font-Names="arial"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcreditodisponible" runat="server" onfocus="this.blur();"
                                ReadOnly="True" Width="100%"
                                CssClass="form-control3"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                  <%--  <tr>
                        <td align="right">
                            <asp:Label ID="Label48" runat="server" Text="Ubicación:" ForeColor="Black"
                                Font-Bold="True" Font-Names="arial"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="imgubicacion" runat="server" Height="30px"
                                ImageUrl="imagenes/maps.jpg" Width="30px" OnClick="imgubicacion_Click"  />
                            <br />
                        </td>
                    </tr>--%>
                  
                </table>
                 <asp:LinkButton  ID="LinkButton8" CssClass="btn btn-success btn-block"  runat="server" OnClick="LinkButton8_Click">
                            Ver Ubicación <i class="fa fa-map-marker" aria-hidden="true"></i></asp:LinkButton>
                <asp:LinkButton  ID="lnkReporte" CssClass="btn btn-info btn-block"  runat="server" OnClick="lnkReporte_Click">
                            Facturas Pendientes <i class="fa fa-share" aria-hidden="true"></i></asp:LinkButton>
                <asp:Button ID="btnverprecios0" runat="server" ForeColor="Black" Width="100%"
                                Height="35px" Font-Bold="True" Text="Ver Lista de Precios" OnClientClick="return lista_precios();"
                                CssClass="btn btn-primary btn-block Ocultar" />
                       
                 <asp:LinkButton  ID="lnkenviarlista" CssClass="btn btn-primary btn-block"  runat="server">
                            Enviar Lista de Precios <i class="fa fa-bars" aria-hidden="true"></i></asp:LinkButton>
            </div>
           
        </div>
        <div class="col-lg-12">
            <div class="step">
                <ul class="nav nav-tabs nav-justified" role="tablist">
                    <li role="step" class="active">
                        <a href="#step1" id="step1-tab" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true">
                            <div class="icon fa fa-users"></div>
                            <div class="step-title">
                                <div class="title">Contactos</div>
                            </div>
                        </a>
                    </li>
                    <li role="step">
                        <a href="#step2" role="tab" id="step2-tab" data-toggle="tab" aria-controls="profile">
                            <div class="icon fa fa-info-circle"></div>
                            <div class="step-title">
                                <div class="title">Información de Internet</div>
                        </div>
                        </a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane fade in active" id="step1" aria-labelledby="home-tab">
                       <asp:UpdatePanel ID="ddedd" runat="server" UpdateMode=" always">
                           <Triggers>
                               <asp:PostBackTrigger ControlID="LinkButton7" />
                               <asp:PostBackTrigger ControlID="LinkButton5" />
                           </Triggers>
                           <ContentTemplate>
                               <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-default" Width="32%" OnClientClick=" return ver_detalles();">Ver Mas&nbsp;</asp:LinkButton>
                               <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-primary" Width="32%"
                                   OnClick="LinkButton5_Click">Nuevo&nbsp;<i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
                               <asp:LinkButton ID="LinkButton7" runat="server" CssClass="btn btn-info" Width="32%" OnClick="LinkButton7_Click" >Pendientes&nbsp;<i class="fa fa-info-circle" aria-hidden="true"></i></asp:LinkButton>
                               <div class="table table-responsive">
                                   <asp:GridView ID="gridcontactos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                       CssClass="table table-responsive table-bordered table-condensed" style="font-size:11px;"
                                       DataKeyNames="idc_telcli" OnRowCommand="gridcontactos_RowCommand">
                                       <Columns>
                                           <asp:TemplateField HeaderText="Editar" HeaderStyle-Width="30px">
                                               <ItemTemplate>
                                                   <asp:ImageButton ID="imgeditar" runat="server" Height="30px"
                                                       ImageUrl="imagenes/btn/icon_editar.png" Width="30px" />
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:BoundField DataField="tipo_contacto" HeaderText="Tipo de Contacto">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="abreviatura" HeaderText="Titulo">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                               <ControlStyle Height="30px" Width="182px" />
                                               <FooterStyle Wrap="True" />
                                               <HeaderStyle Width="200px" />
                                               <ItemStyle Height="20px" Width="350px" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                           <asp:BoundField DataField="celular" HeaderText="Celular" />
                                           <asp:BoundField DataField="email" HeaderText="E-mail">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="funciones" HeaderText="Funciones">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="fecha_cumple" HeaderText="Cumpleaños">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="hobies" HeaderText="Hobbies">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="equipo" HeaderText="Equipo Favorito">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="idc_telcli" HeaderText="idc">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="activo" HeaderText="activo">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="existe" HeaderText="existe">
                                               <HeaderStyle CssClass="Ocultar" />
                                               <ItemStyle CssClass="Ocultar" />
                                           </asp:BoundField>
                                       </Columns>
                                       <HeaderStyle CssClass="HeaderStyle" />
                                       <PagerSettings FirstPageText="" LastPageText="" />
                                       <PagerStyle CssClass="PagerStyle" />
                                       <RowStyle CssClass="AltRowStyle" ForeColor="Black" Font-Names="arial"
                                           Font-Size="Small" />
                                   </asp:GridView>
                               </div>
                           </ContentTemplate>
                       </asp:UpdatePanel>
                    </div>
                    <div role="tabpanel" class="tab-pane fade" id="step2" aria-labelledby="profile-tab">
                        <table style="width: 100%;">
                            <tr>
                                <td align="right" style="width: 3%">
                                    <asp:Image
                                        ID="imgsitio" runat="server" Height="25px"
                                        ImageUrl="~/imagenes/insert-link.png" Width="25px" /></td>
                                <td>
                                    <asp:Label ID="lblsitio"
                                        Style="cursor: pointer" runat="server" Font-Bold="True" ForeColor="Black"
                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Font-Names="arial" Font-Size="Small"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 440px">
                                    <asp:Image
                                        ID="imgfacebook" runat="server" Height="25px"
                                        ImageUrl="~/imagenes/Facebook_Logo-19.gif" Width="25px" /></td>
                                <td>
                                    <asp:Label ID="lblfacebook" runat="server" Font-Bold="True" ForeColor="Black"
                                        Font-Names="arial" Font-Size="Small"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 440px">
                                    <asp:Image
                                        ID="imgtwitter" runat="server" Height="25px"
                                        ImageUrl="~/imagenes/twitter_logo.jpg" Width="25px" /></td>
                                <td>
                                    <asp:Label ID="lbltwitter" runat="server" Font-Bold="True" ForeColor="Black"
                                        Font-Names="arial" Font-Size="Small"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
