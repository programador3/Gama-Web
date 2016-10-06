<%@ Page Title="Cotizacion" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cotizacion_clientes2_m.aspx.cs" Inherits="presentacion.cotizacion_clientes2_m" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Ocultar {
            display: none;
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

        .div-title-content {
            text-align: center;
            background: gray;
            color: white;
            border-radius: 5px 5px 0 0;
        }

        .div-content-division {
            border: solid 2px gainsboro;
            border-radius: 5px;
        }

        /*Popup Image_viewer*/
        #div_mask_, .div_mask {
            position: fixed;
            background: black;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            opacity: .45;
        }

        #div_container_ {
            position: fixed;
            top: 10px;
            left: 10px;
            right: 10px;
            bottom: 10px;
            display: table-cell;
            vertical-align: middle;
            text-align: center;
            background: white;
            border: solid 2px gray;
            border-radius: 3px;
        }

            #div_container_ img {
                position: fixed;
                top: 50%;
                left: 50%;
            }

        .div-block {
            background: whitesmoke;
            border-radius: 3px;
        }
    </style>
    <script type="text/javascript">     
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
        function HideCompetencia() {
            $('#<%=negociado.ClientID%>').removeClass("btn btn-default");
            $('#<%=requerido.ClientID%>').removeClass("btn btn-info");
            $('#<%=negociado.ClientID%>').addClass("btn btn-info");
            $('#<%=requerido.ClientID%>').addClass("btn btn-default");
            $('#<%=txtprecio.ClientID%>').val($('#<%=txtprecio_lista.ClientID%>').val());
            $('#<%=txtprecio_comp.ClientID%>').val('');
            $('#<%=txtvolumen.ClientID%>').val('');
            $('#div_competencia').hide();
        }
        function ShowCompetencia() {

            $('#<%=negociado.ClientID%>').removeClass("btn btn-info");
            $('#<%=requerido.ClientID%>').removeClass("btn btn-default");
            $('#<%=negociado.ClientID%>').addClass("btn btn-default");
            $('#<%=requerido.ClientID%>').addClass("btn btn-info");
            
            $('#<%=txtprecio.ClientID%>').val($('#<%=txtprecio_minimo.ClientID%>').val());
            $('#<%=txtprecio_comp.ClientID%>').val($('#<%=txtprecio_minimo.ClientID%>').val());
            $('#<%=txtvolumen.ClientID%>').val($('#<%=txtcantidad.ClientID%>').val());
            
            swal({
                title: "Mensaje del Sistema",
                text: "El PRECIO debe ser MENOR que el PRECIO MINIMO.",
                type: "info",
                closeOnConfirm: true
            },
            function () {
                $('#div_competencia').show('slow');
                var posicion = $("#div_competencia").offset().top;
                $("html, body").animate({
                    scrollTop: posicion
                }, 300);                
            });
        }
        function soloNumeros(e, tipo) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            if (tipo == 'true') {
                letras = "1234567890";
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

            if (letras.indexOf(tecla) == -1 && !tecla_especial)
                return false;
        }

        function validarnumero(txt) {
            if (isNaN(txt.value) != false) {
                alert("La cantidad no es correcta.");
                txt.select();
                return false;
            }
        }
          function precio_minimo_minigrid(index,item) {
            index++;
            //accedemos a la tabla
            var objtabla = document.getElementById('<%=gridcot.ClientID%>');
            var precio = item.value;//objtabla.rows[index].cells[3].textContent;
            
            //valor
            var precio_minimo =objtabla.rows[index].cells[14].textContent.trim();
            //valor
            precio = precio.replace(",", "");
            precio_minimo.replace(",","");
            //tipo de precio
            var tipoprecio = objtabla.rows[index].cells[13].textContent.trim();
            //precio de lista
            var preciolista = objtabla.rows[index].cells[3].textContent.trim();
            //validacion
            if (precio == "" || precio == 0) {
                alert("el precio no puede ser cero");
                if (tipoprecio == "N") {
                    item.value = preciolista;
                } else {
                    item.value = precio_minimo
                }
               
            }
            //requerido
            if (tipoprecio =="R") {
                if (parseFloat(precio_minimo) < parseFloat(precio)) {
                    alert('El Precio Debera Ser Menor al Precio Minimo.');
                    item.value = precio_minimo;
                    porcentaje();
                    return false;
                }
            }
            if (parseFloat(precio_minimo) > parseFloat(precio) && tipoprecio == "N") {
                alert('El Precio Deber Ser Mayor O Igual al Precio Minimo.');
                item.value = objtabla.rows[index].cells[7].textContent;
                porcentaje();
                return false;
            } 
           
            //recalcular el porcentaje
            porcentaje();

        }


        function porcentaje() {
            var tabla = document.getElementById('<%=gridcot.ClientID%>');
            var id;
            var precio;
            var margen;
            var porc;
            var costo;
            var cantidad;
            var margencompartido;
            var vventaart;  //.- Codificar Para Igualar a la otra pantalla....
            if (tabla != null) {
                for (var i = 1; i <= tabla.rows.length - 1; i++) {
                    id = tabla.rows[i].cells[3].children[0].id;
                    precio = document.getElementById(id).value;
                    costo = tabla.rows[i].cells[11].textContent;
                    id = tabla.rows[i].cells[2].children[0].id;
                    cantidad = document.getElementById(id).value;
                    vventaart = (cantidad * precio).toFixed(2);

                    //vventaart = Math.round(vventaart)/100;

                    margen = (1 - (costo / precio)) * 100
                    //margen = Math.round(margen*100)/100; 
                    if (margen < 4) {
                        margen = margen
                    }
                    else if (margen < 6) {
                        margen = 4;
                    }
                    else if (margen < 8) {
                        margen = 6;
                    }
                    else if (margen < 10) {
                        margen = 8;
                    }
                    else if (margen < 12) {
                        margen = 10;
                    }
                    margen = (margen).toFixed(2);
                    porc = (((2.5 / 22) / 100) * margen) * 100
                    margencompartido = margen * 0.1136;
                    porc = parseFloat(porc);
                    if (margen >= 12) {
                        porc = (margencompartido * 1).toFixed(4);
                    }
                    else if (margen >= 10) {
                        porc = (margencompartido * .75).toFixed(4);
                    }
                    else if (margen >= 8) {
                        porc = (margencompartido * .5).toFixed(4);
                    }
                    else if (margen >= 6) {
                        porc = (margencompartido * .25).toFixed(4);
                    }
                    else if (margen < 6) {
                        porc = (margencompartido * .10).toFixed(4);
                    }

                    //porc = Math.round(porc*100)/100;

                    // porc = precio * cantidad * (porc / 100)  
                    tabla.rows[i].cells[4].textContent = porc;
                    porc = (vventaart * porc / 100).toFixed(4);
                    tabla.rows[i].cells[5].textContent = porc; // (porc * 1).toFixed(4);
                }
            }
        }
        function ConfirmaRegresa()
        {
            var txt;
            var r = confirm("Desea Regresar a La Ficha del Cliente?");
            if (r == true) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class=" page-header">Negociacion con Clientes</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnagregar" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente</strong></h5>
                    <asp:TextBox Style="font-size: 11px;" runat="server" ReadOnly="true" ID="txtcliente"
                        CssClass="form-control" onfocus="this.blur();"></asp:TextBox>
                    <h5><strong>
                        <asp:Label ID="lbltipo" runat="server" Text="Master"></asp:Label></strong></h5>
                    <asp:DropDownList ID="cbomaster" CssClass=" form-control2" runat="server" Width="60%" AutoPostBack="true" OnSelectedIndexChanged="lnkseleccionar_Click">
                    </asp:DropDownList>
                    
                    <asp:LinkButton ID="lnkseleccionar" runat="server" CssClass="btn btn-info" Width="38%" OnClick="lnkseleccionar_Click">Seleccionar</asp:LinkButton>
                    <asp:TextBox ID="txtbuscar" runat="server" CssClass="form-control2" Width="100%" placeholder="Buscar" AutoCompleteType="Search" AutoPostBack="true" OnTextChanged="txtbuscar_TextChanged"></asp:TextBox>
                    <asp:LinkButton ID="lnkbuscar" runat="server" CssClass="btn btn-success" Width="100%" OnClick="lnkbuscar_Click">Buscar Articulo</asp:LinkButton>
                    <asp:LinkButton ID="lnkmaster" runat="server" CssClass="btn btn-info" Width="100%" OnClick="lnkmaster_Click">Ver Articulos Master</asp:LinkButton>
                </div>
                <div class=" col-lg-12"  style="text-align:center;">
                    <h4><strong>
                        <asp:Label ID="lbldesart" runat="server" ForeColor="Red"></asp:Label></strong></h4>
                    <asp:Label ID="lblcantidad" runat="server" Text="Cantidad" Font-Bold="True"
                        Font-Names="arial" Font-Size="Small" Width="98%"></asp:Label>
                    <asp:TextBox ID="txtcantidad" runat="server" type="number" step="0.01" min="0.01"
                        CssClass="form-control2"
                        Style="height: 30px;" Width="98%"></asp:TextBox>
                    <asp:TextBox ID="txtprecio_ch" runat="server" onfocus="this.blur();"
                        CssClass="Ocultar"
                        Style="height: 30px;" Width="98%"></asp:TextBox>
                    <asp:Label ID="lblpreciolista" runat="server" Text="Precio Lista" Font-Bold="True"
                        Font-Names="arial" Font-Size="Small" Width="60%" style="text-align:center;"></asp:Label>
                    <asp:Label ID="lblporcentajelista" runat="server" Text="%" Font-Bold="True"
                        Font-Names="arial" Font-Size="Small" Width="38%" style="text-align:center;"></asp:Label>

                    <asp:TextBox ID="txtprecio_lista" runat="server" onfocus="this.blur();"
                        CssClass="form-control2 right"
                        Style="height: 30px;" Width="60%"></asp:TextBox>

                    <asp:TextBox ID="txtporc_lista" runat="server" onfocus="this.blur();"
                        CssClass="form-control2 right"
                        Style="height: 30px;" Width="38%"></asp:TextBox>
                    <asp:Label ID="lblpreciominimo" runat="server" Text="Precio Minimo" Font-Bold="True"
                        Font-Names="arial" Font-Size="Small" Width="60%" style="text-align:center;"></asp:Label>

                    <asp:Label ID="lblporcentajeminimo" runat="server" Text="%" Font-Bold="True"
                        Font-Names="arial" Font-Size="Small" Width="38%" style="text-align:center;"></asp:Label>
                    <asp:TextBox ID="txtprecio_minimo" runat="server" onfocus="this.blur();"
                        CssClass="form-control2 right"
                        Style="height: 30px; display: inline !important" Width="60%"></asp:TextBox>


                    <asp:TextBox ID="txtporc_minimo" runat="server" onfocus="this.blur();"
                        CssClass="form-control2 right"
                        Style="height: 30px; display: inline !important" Width="38%"></asp:TextBox>
                    <input onclick="HideCompetencia();" style="width: 49%;" id="negociado" runat="server" value="P. Negociado" type="button" class="btn btn-info" />
                    <input onclick="ShowCompetencia();" style="width: 49%;" id="requerido" runat="server" value="P. Requerido" type="button" class="btn btn-default" />
                    <asp:RadioButton Visible="false" onclick="HideCompetencia();" ID="rdnegociado" Text="Negociado" runat="server" GroupName="tipo" Checked="true" />
                    <asp:RadioButton Visible="false" onclick="ShowCompetencia();" ID="rdrequerido" Text="Requerido" runat="server" GroupName="tipo" />
                    <asp:Label ID="lblprecio"  Width="60%" runat="server" Text="Precio" Font-Bold="True" Font-Names="arial" Font-Size="Small" Style="color: Blue; text-align:center;"></asp:Label>

                    <asp:Label ID="lblporcentajeprecio" runat="server" Text="%" Font-Bold="True" Font-Names="arial" Font-Size="Small"  Width="38%" style="text-align:center;"> </asp:Label>                    
                    <asp:TextBox ID="txtprecio" runat="server" type="number" step="0.0001" min="0.0001"
                        CssClass="form-control2 right"
                        Style="height: 30px; display: inline !important; color: Blue !important;" Width="60%"></asp:TextBox>

                    <asp:TextBox ID="txtporc_precio" runat="server" onfocus="this.blur();"
                        CssClass="form-control2 right"
                        Style="height: 30px; display: inline !important" Width="38%" AutoCompleteType="Disabled"></asp:TextBox>                    
                    <asp:Label ID="lblfecha_comp" runat="server" Text="Fecha Compromiso" Font-Bold="True"
                        Font-Names="arial" Font-Size="Small" Style="color: Blue;"  Width="100%"></asp:Label>
                    <asp:TextBox ID="txtfecha" runat="server" class="form-control2 right" Width="98%" TextMode="Date"></asp:TextBox>
                    <asp:Label ID="lblperiodo" runat="server" Text="Periodo de Compra(dias)" Font-Bold="True"
                        Font-Names="arial" Font-Size="Small" Style="color: Blue;"></asp:Label></td>
                      <asp:DropDownList ID="cboperiodo" runat="server" CssClass=" form-control2" Width="98%"> </asp:DropDownList>
                </div>
                
                 <div id="div_competencia" style="display: none;" class="col-lg-12">
                        <h4 style="text-align:center;color:white;background-color:dimgray;"><strong>
                            <asp:Label ID="lblprecio6" runat="server" Text="Competencia"></asp:Label></strong></h4>
                        <asp:Label ID="lblprecio4" runat="server" Text="Precio" Font-Bold="True"
                            Font-Names="arial" Font-Size="Small" Style="color: Blue;" Width="49%"></asp:Label>

                        <asp:Label ID="lblprecio5" runat="server" Text="Cantidad de Compra" Font-Bold="True"
                            Font-Names="arial" Font-Size="Small" Style="color: Blue;" Width="49%"></asp:Label>
                        <asp:TextBox ID="txtprecio_comp" min="0.0001" step="0.0001" type="number" Width="49%" runat="server" class="form-control2 right"></asp:TextBox>

                        <asp:TextBox ID="txtvolumen" min="0.0001" step="0.0001" type="number" Width="49%" runat="server" class="form-control2 right"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text="Competencia" Font-Bold="True"
                            Font-Names="arial" Font-Size="Small" Width="49%" Style="color: Blue;"></asp:Label>

                        <asp:Label ID="Label4" runat="server" Text="Observaciones" Font-Bold="True"
                            Font-Names="arial" Font-Size="Small" Width="49%" Style="color: Blue;"></asp:Label>

                        <asp:TextBox ID="txtnombre_comp" runat="server" Width="49%" class="form-control2 right"></asp:TextBox>

                        <asp:TextBox ID="txtobservaciones_comp" Width="49%" runat="server" class="form-control2 right"></asp:TextBox>
                        <asp:Label ID="lblfile" runat="server" Text="Cargar Factura..."></asp:Label>
                        <asp:FileUpload ID="fufactura" runat="server" Width="100%" />

                    </div>
                <div class=" col-lg-12">
                    <asp:LinkButton ID="lnkultimo" runat="server" CssClass="btn btn-info btn-block" OnClick="lnkultimo_Click">Historial de Venta&nbsp;<i class="fa fa-list-alt" aria-hidden="true"></i></asp:LinkButton>
                     
                    <asp:Button ID="btnagregar" Text="Agregar Articulo" runat="server" CssClass=" btn btn-primary btn-block" OnClick="btnagregar_Click" />
                </div>
                <div class="col-lg-12">
                    <div class="table table-responsive">

                        <asp:DataGrid ID="gridcot" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive table-condensed table-bordered" OnItemCommand="gridcot_ItemCommand" OnItemDataBound="gridcot_ItemDataBound">
                            <Columns>

                                <asp:BoundColumn DataField="idc_articulo" HeaderText="">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="desart" HeaderText="Descripcion" HeaderStyle-Width="300"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Cantidad" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcantidad" runat="server" Text='<%# Bind("cantidad") %>' type="number" min="0.01" step="0.01"
                                            CssClass="form-control2"
                                            Style="height: 30px; font-size: 10px;" Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Precio" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtprecio_grid" runat="server" Text='<%# Bind("precio") %>' type="number" min="0.0001" step="0.0001"
                                            CssClass="form-control2" onfocus="this.select();" TextMode="Number"
                                            Style="height: 30px; font-size: 10px;" Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ven" DataFormatString="{0:N2}"  HeaderStyle-Width="50px" HeaderText="% Apo P. Venta">
                                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" Font-Bold="true" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="porc" DataFormatString="{0:N2}" HeaderText="$">
                                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" Font-Bold="true" CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Ver">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemTemplate>
                                        <asp:Button ID="btnver" runat="server"
                                            CssClass="btn btn-default"
                                            Font-Bold="True" Font-Names="arial" Font-Size="Medium" Height="40px"
                                            Text="Ver" Width="100%" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="precio_lista" DataFormatString="{0:N4}" HeaderText="precio_lista">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn  HeaderStyle-Width="70px">
                                    <ItemTemplate>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td align="center">
                                                    <asp:ImageButton ID="imgultimo_precio" runat="server" Height="30px" ImageUrl="imagenes/calendar3.gif" Width="30px" />
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="imgeliminar" runat="server" Width="30px" Height="30px" CommandName="eliminar" ImageUrl="~/imagenes/btn/icon_delete.png" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ultm_precio">
                                    <ItemStyle HorizontalAlign="Center" CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fecha_ult_precio">
                                    <ItemStyle HorizontalAlign="Center" CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="costo">
                                    <ItemStyle HorizontalAlign="Center" CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ult_precio_nc">
                                    <ItemStyle HorizontalAlign="Center" CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="tipoprecio">
                                    <ItemTemplate>
                                        <asp:Label ID="gridlbltipoprecio" runat="server" Text='<%# Bind("tipoprecio") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="preciominimo">
                                    <ItemTemplate>
                                        <asp:Label ID="gridlblpreciominimo" runat="server" Text='<%# Bind("preciominimo") %>'></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:TemplateColumn>

                            </Columns>
                            <HeaderStyle CssClass="RowStyle header" />
                        </asp:DataGrid>
                    </div>
                </div>
                <div class=" col-lg-12">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="Small" ForeColor="Red" Text="**Precios No Incluyen IVA."></asp:Label>
                </div>
            </div>

            <div id="hiddens" runat="server" visible="false">
                <asp:TextBox ID="txtmovio" runat="server" Text="0"></asp:TextBox>
                <asp:TextBox ID="txtagente" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtid" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtult_pre" runat="server"></asp:TextBox>
                <asp:Button ID="btng" runat="server" />
                <asp:Button ID="btnbuscar_art" runat="server" />
                <input type="hidden" runat="server" id="txtbase64" />

                <asp:TextBox ID="txtcosto" runat="server"></asp:TextBox>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
                <div class="col-lg-12">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cotización" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click"/>
                
                    <asp:Button ID="btnCancelar" OnClientClick=" return ConfirmaRegresa();" runat="server" Text="Regresar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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
</asp:Content>