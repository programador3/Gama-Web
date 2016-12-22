<%@ Page Language="VB" MasterPageFile="~/Global.Master" AutoEventWireup="false" CodeFile="quejas_m.aspx.vb" Inherits="queja_m" title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" Runat="Server">
        <style type="text/css" >
            .div
            {
                background-color:rgb(186, 192, 194);
                border-radius:3px;
                border:solid 0px gainsboro;
                padding:5px;
            }
            input[type='text']
            {
                font-size:12px;
            }        
            .auto-style1
            {
                width: 50%;
                height: 20px;
            }
            .Ocultar {
                display:none;
            }
        </style>
        
        <script type="text/javascript" >
//            function position(objeto)
//            {
//                alert(objeto.offsetTop);
//                window.scrollTo(0,objeto.offsetTop);
//            }
           function consignado()
           {
                var txtidc_factura  = document.getElementById('<%=txtidc_factura.ClientID%>');
                if(txtidc_factura.value=='' || txtidc_factura.value==0)
                {
                    return false;
                }
           }
           
           function colonia()
            {
                 var ruta="buscar_colonia_mobile.aspx?tipo=3";
                 window.open(ruta);
                 return false;    
            }
            
            function position(objeto) 
            {
                var top = 0;
                var left = 0;
               
                while(objeto.tagName != "BODY") {
                    top += objeto.offsetTop;
                    left += objeto.offsetLeft;
                    objeto = objeto.offsetParent;
                }
                scrollTo(0,top-20);
               
                //return { top: top, left: left };
               
            }
            function ValidarBuscarFactura(){
                var txtfactura = document.getElementById('<%=txtfactura.ClientID%>');
                return ((txtfactura.value =='')?false:true);
            }
            function guardar()
            {               
               var txtproblema = document.getElementById('<%=txtproblema.ClientID%>');
               var txtagente = document.getElementById('<%=txtagente.ClientID%>');
               var txttmk = document.getElementById('<%=txttmk.ClientID%>');
               var txtcomprador = document.getElementById('<%=txtcomprador.ClientID%>');
               var txtobra = document.getElementById('<%=txtobra.ClientID%>');
               var txtidc_colonia = document.getElementById('<%=txtidc_colonia.ClientID%>');
               var txtcalle = document.getElementById('<%=txtcalle.ClientID%>');
               var txtnumero = document.getElementById('<%=txtnumero.ClientID%>');
               var txttel_a = document.getElementById('<%=txttel_a.ClientID%>');
               var txttel_c = document.getElementById('<%=txttel_c.ClientID%>');
               var txttel_t = document.getElementById('<%=txttel_t.ClientID%>');
               var txttel_o = document.getElementById('<%=txttel_o.ClientID%>');
               var griddatos = document.getElementById('<%=griddatos.ClientID%>');
               var tot = cont();
               
               if(txtproblema.value=='')
               {
                    swal('Mensaje del Sistema','El Problema o Queja no Puede quedar Vacia.','info');
                    position(txtproblema);
                    return false;
               }
               else if (tot == 0) {
                   swal('Mensaje del Sistema', 'Debes Introducir la Cantidad Dañada del Articulo', 'info');
                   position(griddatos);
                   return false;
               }
               else if (txtagente.value == '') {
                   swal('Mensaje del Sistema', 'El Agente no puede quedar vacio.', 'info');
                   position(txtagente);
                   return false;
               }
               else if (txtagente.value.length < 10) {
                   swal('Mensaje del Sistema', 'El Nombre del Agente no puede ser menor a 10 Caracteres', 'info');
                   position(txtagente);
                   return false;
               }

               else if (txtcomprador.value == '') {
                   swal('Mensaje del Sistema', 'El Comprador no puede quedar vacio.', 'info');
                   position(txtcomprador);
                   return false;
               }
               else if (txtcomprador.value.length < 10) {
                   swal('Mensaje del Sistema', 'El Nombre del Comprador no puede ser menor a 10 Caracteres', 'info');
                   position(txtcomprador);
                   return false;
               }

               else if (txtobra.value == '') {
                   swal('Mensaje del Sistema', 'El Contacto no puede quedar vacio.', 'info');
                   position(txtobra);
                   return false;
               }
               else if (txtobra.value.length < 10) {
                   swal('Mensaje del Sistema', 'El Nombre del Contacto no puede ser menor a 10 Caracteres.', 'info');
                   position(txtobra);
                   return false;
               }

               else if (txttmk.value == '') {
                   swal('Mensaje del Sistema', 'El TMK no puede quedar vacio.', 'info');
                   position(txttmk);
                   return false;
               }
               else if (txttmk.value.length < 10) {
                   swal('Mensaje del Sistema', 'El Nombre del TMK no puede ser menor a 10 Caracteres.', 'info');
                   position(txttmk);
                   return false;
               }

               else if (txttel_a.value == '') {
                   swal('Mensaje del Sistema', 'El Telefono del Agente no puede quedar vacio.', 'info');
                   position(txttel_a);
                   return false;
               }
               else if (txttel_a.value.length < 8) {
                   swal('Mensaje del Sistema', 'El Telefono del Agente no puede ser menor a 8 Caracteres.', 'info');
                   position(txttel_a);
                   return false;
               }

               else if (txttel_c.value == '') {
                   swal('Mensaje del Sistema', 'El Telefono del Comprador no puede quedar vacio', 'info');
                   position(txttel_c);
                   return false;
               }
               else if (txttel_c.value.length < 8) {
                   swal('Mensaje del Sistema', 'El Telefono del Comprador no puede ser menor a 8 Caracteres.', 'info');
                   position(txttel_c);
                   return false;
               }

               else if (txttel_o.value == '') {
                   swal('Mensaje del Sistema', 'El Telefono del Contacto no puede quedar vacio.', 'info');
                   position(txttel_o);
                   return false;
               }
               else if (txttel_o.value.length < 8) {
                   swal('Mensaje del Sistema', 'El Telefono del Contacto no puede ser menor a 8 Caracteres.', 'info');
                   position(txttel_o);
                   return false;
               }

               else if (txttel_t.value == '') {
                   swal('Mensaje del Sistema', 'El Telefono del TMK no puede quedar vacio.', 'info');
                   position(txttel_t);
                   return false;
               }
               else if (txttel_t.value.length < 8) {
                   swal('Mensaje del Sistema', 'El Telefono del TMK no puede ser menor a 8 Caracteres.', 'info');
                   position(txttel_t);
                   return false;
               }

               else if (txtidc_colonia.value == '' || txtidc_colonia.value == 0) {
                   swal('Mensaje del Sistema', 'La Colonia no puede quedar vacia', 'info');
                   return false;
               }

               else if (txtcalle.value == "") {
                   swal('Mensaje del Sistema', 'La Calle no puede quedar vacia.', 'info');
                   position(txtcalle);
                   return false;
               } else if (txtnumero.value == '') {
                   swal('Mensaje del Sistema', 'El numero no puede quedar vacio', 'info');
                   position(txtnumero);
                   return false;
               } else {
                   return confirm("¿Esta Seguro de Guardar la Queja?");
               }
            
            }
            function cont()
            {
                var griddatos = document.getElementById('<%=griddatos.ClientID%>');
                var id;
                var cantidad;
                var tot=0;
                if(griddatos!=null)
                {
                    for(var i = 1;i <= griddatos.rows.length - 1;i++)
                    {   
                       id = griddatos.rows[i].cells[5].children[0].id;
                       cantidad = document.getElementById(id).value;
                       if(cantidad>0)
                       {
                           tot++;
                       }
                    }
                }
                return tot;
            }
            
            
            
            function cant()
            {
                var griddatos = document.getElementById('<%=griddatos.ClientID%>');
                var can_max;
                var id;
                var cantidad;
                if(griddatos!=null)
                {
                    for(var i = 1;i <= griddatos.rows.length - 1;i++)
                    {
                       can_max = griddatos.rows[i].cells[7].textContent;
                       id = griddatos.rows[i].cells[5].children[0].id;
                       cantidad = document.getElementById(id).value;
                       if (cantidad - Math.floor(cantidad) != 0) {
                               alert ("no se permiten decimales");
                           }

                       if(cantidad>can_max)
                       {
                            alert('No Puedes Introducir mas de:\n \u000B \n' + can_max);
                            document.getElementById(id).value= can_max;
                       }
                    }
                }
                return false;
            }            
        </script>
    
    <asp:UpdatePanel ID="oapapa" runat="server" UpdateMode="Always">
        <Triggers>
          
            <asp:PostBackTrigger ControlID="btnsalir"  />
            <asp:PostBackTrigger ControlID="btnotra"  />
            <asp:PostBackTrigger ControlID="btnguardar"  />
        </Triggers>
        <ContentTemplate>

            <h2><strong>Formulario de Quejas</strong></h2>
            <div class="row">
                <div class="col-xs-12 col-lg-12">
                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;No. Queja</strong></h5>
                    <asp:TextBox ID="txtnum" runat="server" Width="100%" onfocus="this.blur();" Style="background-color: rgb(10, 146, 253); color: white !important;"
                        CssClass="form-control2" Font-Names="arial" ForeColor="Blue">
                    </asp:TextBox>
                    <h5><strong><i class="fa fa-book" aria-hidden="true"></i>&nbsp;Factura</strong></h5>
                    <asp:TextBox ID="txtfactura" runat="server" Width="85%" CssClass="form-control2" Font-Names="arial" ForeColor="Blue">
                    </asp:TextBox>
                    <asp:LinkButton ID="LinkButton1" OnClientClick="return ValidarBuscarFactura();" runat="server" CssClass="btn btn-primary" Width="40px"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                    <asp:Label ID="Label1" runat="server" Style="font-size: 14px; font-weight: 700;" Width="48%" Text="RFC:"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Style="font-size: 14px; font-weight: 700;" Width="49%" Text="CVE:"></asp:Label>
                    <asp:TextBox ID="txtrfc" runat="server" Width="49%" onfocus="this.blur();"
                        CssClass="form-control2"
                        Font-Bold="True" Font-Names="arial" ForeColor="Blue"></asp:TextBox>
                    <asp:TextBox ID="txtcve" runat="server" Width="49%" onfocus="this.blur();"
                        CssClass="form-control2"
                        Font-Bold="True" Font-Names="arial" ForeColor="Blue"></asp:TextBox>

                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente</strong></h5>
                    <asp:TextBox ID="txtcliente" runat="server" Width="100%" CssClass="form-control2" Font-Bold="True" Font-Names="arial" ForeColor="Blue">
                    </asp:TextBox>
                    <h5><strong><i class="fa fa-comment-o" aria-hidden="true"></i>&nbsp;Problema o Queja</strong></h5>
                    <asp:TextBox ID="txtproblema" runat="server" Width="100%" CssClass="form-control"
                        Font-Size="12px" ForeColor="Black" Style="resize: none;" Rows="6" TextMode="MultiLine">
                    </asp:TextBox>
                    <br />
                    <div class="table table-responsive">
                        <asp:DataGrid ID="griddatos" runat="server" AutoGenerateColumns="False" Style="background-color: White; border: solid .1px gainsboro;" BackColor="White" Width="100%"
                            CssClass="table table-responsive table-bordered table-condensed">
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle Font-Size="12px" />
                            <Columns>
                                <asp:BoundColumn DataField="codigo" HeaderText="Codigo"></asp:BoundColumn>
                                <asp:BoundColumn DataField="desart" HeaderText="Descripcion"></asp:BoundColumn>
                                <asp:BoundColumn DataField="unimed" HeaderText="UM">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="cantidad" HeaderText="Cantidad" DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="devo" HeaderText="Queja" DataFormatString="{0:N2}">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderStyle-Width="100px" HeaderText="Cantidad Dañada">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcantidad" TextMode="Number" runat="server" Width="100%" onfocus="this.select();"
                                            Style="border: solid 1px rgb(255, 105, 5); text-align: right; border-radius: 3px; color: Blue !important; font-weight: bold;"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.devo", "{0:N4}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="decimales" HeaderText="decimales">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="maxdev" HeaderText="maxdev">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="codigo2" HeaderText="codigo2">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="cantidadc2" HeaderText="cantidadc2">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idc_facturad" HeaderText="idc_facturad">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IDC_PAQD" HeaderText="IDC_PAQD">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="RowStyle" />
                        </asp:DataGrid>
                    </div>
                </div>

            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <div class="div">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 60%">

                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" Text="Agente:"></asp:Label>

                                    </td>
                                    <td style="width: 40%">

                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" Text="Tel.:"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <asp:TextBox ID="txtagente" runat="server" Width="100%"
                                            CssClass="form-control2"
                                            Font-Bold="True" Font-Names="arial" Font-Size="12px"
                                            ForeColor="Blue" Height="30px"></asp:TextBox>

                                    </td>
                                    <td>

                                        <asp:TextBox ID="txttel_a" runat="server" Width="100%"
                                            CssClass="form-control2" TextMode="Number" onkeypress="return validarEnteros(event)" MaxLength="10"
                                            Font-Bold="True" Font-Names="arial" Font-Size="12px"
                                            ForeColor="Blue" Height="30px"></asp:TextBox>

                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="div">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 60%">

                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" Text="TMK:"></asp:Label>

                                    </td>
                                    <td style="width: 40%">

                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" Text="Tel.:"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <asp:TextBox ID="txttmk" runat="server" Width="100%"
                                            CssClass="form-control2"
                                            Font-Bold="True" Font-Names="arial" Font-Size="12px"
                                            ForeColor="Blue" Height="30px"></asp:TextBox>

                                    </td>
                                    <td>

                                        <asp:TextBox ID="txttel_t" runat="server" Width="100%"
                                            CssClass="form-control2" TextMode="Number" onkeypress="return validarEnteros(event)" MaxLength="10"
                                            Font-Bold="True" Font-Names="arial" Font-Size="12px"
                                            ForeColor="Blue" Height="30px"></asp:TextBox>

                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="div">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 60%">

                                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" Text="Comprador:"></asp:Label>

                                    </td>
                                    <td style="width: 40%">

                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" Text="Tel.:"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <asp:TextBox ID="txtcomprador" runat="server" Width="100%"
                                            CssClass="form-control2"
                                            Font-Bold="True" Font-Names="arial" Font-Size="12px"
                                            ForeColor="Blue" Height="30px"></asp:TextBox>

                                    </td>
                                    <td>

                                        <asp:TextBox ID="txttel_c" runat="server" Width="100%"
                                            CssClass="form-control2" TextMode="Number" onkeypress="return validarEnteros(event)" MaxLength="10"
                                            Font-Bold="True" Font-Names="arial" Font-Size="12px"
                                            ForeColor="Blue" Height="30px"></asp:TextBox>

                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="div">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 60%">

                                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" Text="Contacto(Obra):"></asp:Label>

                                    </td>
                                    <td style="width: 40%">

                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="Small" Text="Tel.:"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtobra" runat="server" Width="100%" CssClass="form-control2" Font-Bold="True" Font-Names="arial" Font-Size="12px" ForeColor="Blue" Height="30px">
                                        </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttel_o"  TextMode="Number" onkeypress="return validarEnteros(event)" MaxLength="10" runat="server" Width="100%"
                                            CssClass="form-control2" Font-Bold="True" Font-Names="arial" Font-Size="12px" ForeColor="Blue" Height="30px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="Small" Text="Observaciones:">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtobs" runat="server" Width="100%" CssClass="form-control2" Font-Bold="True" Font-Names="arial" Font-Size="12px" ForeColor="Black" Height="60px" TextMode="MultiLine">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>

                        <table style="width: 100%">
                            <tr>
                                <td style="width: 45%;">

                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" Text="Dirección:" Font-Italic="True" Style="display: block;"
                                        Font-Overline="False" ForeColor="Navy"></asp:Label>
                                    <asp:Label ID="Label24" runat="server" Font-Italic="True" Font-Names="arial"
                                        Font-Size="Small" Font-Strikeout="False" Font-Underline="False"
                                        ForeColor="Gray" Text="(Ubicación Material)"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btndireccion" runat="server" Font-Bold="True" Text="Insertar Direccion Fiscal"
                                        Width="100%"
                                        CssClass="btn btn-default"
                                        OnClientClick="return consignado();" />
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr>
                    <td>

                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 70%">

                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" Text="Calle:"></asp:Label>

                                </td>
                                <td style="width: 30%">

                                    <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="Small" Text="Num.:"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>

                                    <asp:TextBox ID="txtcalle" runat="server" Width="100%"
                                        CssClass="form-control2"
                                        Font-Bold="True" Font-Names="arial" Font-Size="12px"
                                        ForeColor="Blue" Height="30px"></asp:TextBox>

                                </td>
                                <td>

                                    <asp:TextBox ID="txtnumero" runat="server" Width="100%"
                                        CssClass="form-control2"
                                        Font-Bold="True" Font-Names="arial" Font-Size="12px"
                                        ForeColor="Blue" Height="30px"></asp:TextBox>

                                </td>
                            </tr>

                        </table>

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="arial"
                            Font-Size="Small" Text="Colonia:"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 10%;" align="center">

                                    <asp:ImageButton ID="imgcolonia" runat="server" Height="30px"
                                        ImageUrl="~/imagenes/btn/icon_buscar.png" Width="30px" />

                                </td>
                                <td style="width: 90%;">
                                    <asp:TextBox ID="txtcolonia" runat="server" Width="100%" onfocus="this.blur();" CssClass="form-control2" Font-Bold="True" Font-Names="arial" Font-Size="12px" ForeColor="Blue" Height="30px">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="arial"
                            Font-Size="Small" Text="Hora de Visita:"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 33%;">
                                    <div style="width: 100%;" class="styled-select">
                                        <asp:DropDownList ID="cbohoras" runat="server" CssClass="form-control" Width="100%" Height="100%">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td style="width: 34%;">
                                    <div style="width: 100%;" class="styled-select">
                                        <asp:DropDownList ID="cbominutos" runat="server" CssClass="form-control" Width="100%" Height="100%">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td style="width: 33%;">
                                    <div style="width: 100%" class="styled-select">
                                        <asp:DropDownList ID="cbomeridiano" runat="server" CssClass="form-control" Width="100%" Height="100%">
                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>

                        </table>

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="arial"
                            Font-Size="Small" Text="Municipio:"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:TextBox ID="txtmun" runat="server" Width="100%" onfocus="this.blur();"
                            CssClass="form-control2"
                            Font-Bold="True" Font-Names="arial" Font-Size="12px"
                            ForeColor="Blue" Height="30px"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="arial"
                            Font-Size="Small" Text="Estado:"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:TextBox ID="txtedo" runat="server" Width="100%" onfocus="this.blur();"
                            CssClass="form-control2"
                            Font-Bold="True" Font-Names="arial" Font-Size="12px"
                            ForeColor="Blue" Height="30px"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="arial"
                            Font-Size="Small" Text="Municipio:"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:TextBox ID="txtpais" runat="server" Width="100%" onfocus="this.blur();"
                            CssClass="form-control2"
                            Font-Bold="True" Font-Names="arial" Font-Size="12px"
                            ForeColor="Blue" Height="30px"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnrefresh" runat="server" CssClass="Ocultar" />
                        <asp:TextBox ID="txtidc_colonia" runat="server" CssClass="Ocultar"></asp:TextBox>
                        <asp:TextBox ID="txtidc_factura" runat="server" CssClass="Ocultar"></asp:TextBox>

                    </td>
                </tr>
            </table>
    <br />
    <div class="row">
        <div class="col-xs-4 col-sm-4 col-lg-4">
            <asp:Button ID="btnguardar" runat="server" Text="Guardar"
                CssClass="btn btn-primary btn-block" />
        </div>
        <div class="col-xs-4 col-sm-4 col-lg-4">
            <asp:Button ID="btnotra" runat="server" OnClientClick="return confirm('¿Desea limpiar todo el formulario?');" Text="Otra Queja" CssClass="btn btn-success btn-block" />
        </div>
        <div class="col-xs-4 col-sm-4 col-lg-4">
            <asp:Button ID="btnsalir" runat="server" OnClientClick="return confirm('¿Desea Salir?');" Text="Salir" CssClass="btn btn-danger btn-block" />
        </div>

    </div>
            
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>

