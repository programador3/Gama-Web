<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="prospectos_captura3.aspx.cs" Inherits="presentacion.prospectos_captura3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn_transparente {
            background: transparent;
        }
        .obligado {
            color:red;
            font:bold;
        }
        #informacionadd:link {
            
            color:blue;
            font:bold;
        }

        /* visited link */
        #informacionadd:visited {
            color:blue;
            font:bold;
        }

        /* mouse over link */
        #informacionadd:hover {
            color:blue;
            font:bold;
        }

        /* selected link */
        #informacionadd:active {
            color:blue;
            font:bold;
        }
    </style>
    <script type="text/javascript">
        function ModalClose() {
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
        function confirmar(msg) {

            //var id = document.getElementById("txtidc_cliente").value;
            //var msg = (id == "") ? "¿Deseas Agregar?" : "¿Deseas Modificar?";
            var conf = confirm(msg);

            return conf;
        }
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 08); // || tecla == 46); en este caso quite el punto
        }
        // function metodo(obj) {
        //     __doPostBack(obj.name.substring(obj.name.indexOf("$") + 1), obj.value);
        // }

        function cambiarDisplay(id) {
            if (!document.getElementById) return false;
            fila = document.getElementById(id);

            fila.style.display = "block"; //mostrar fila 

        }
        window.onload = obtenUbicacion;
        function obtenUbicacion() {
            getCoordenadas();

        }
        function getCoordenadas() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(coordenadas);
            } else {
                alert('Este navegador es algo antiguo, actualiza para usar el API de localización');
            }

        }
        function coordenadas(position) {
            var lat = position.coords.latitude;
            var lon = position.coords.longitude;

            $('#<%=oclatitud.ClientID%>').val(lat);
             $('#<%=oclongitud.ClientID%>').val(lon);

        }
        function mensaje_java(texto) {

            //alert(texto);
            //$("#btnaddtel").notify("Telefono Incorrecto");
            sweetAlert("Mensaje del Sistema", texto, "info");
            //$.notify(texto);

        }
        function irSeccion(Name) {

            document.location.href = Name;

        }
        function campoVacio() {
            var marca = $('#<%= txtmarca.ClientID%>').val();
            var distrib = $('#<%= txtdistribuidor.ClientID%>').val();
            var precio = $('#<%= txtprecio.ClientID%>').val();
            if (marca == "" & distrib == "" & precio == "") {
                $('#btn_mar_dis').hide();
            } else {
                $('#btn_mar_dis').show();
            }
        }

        //add 26-10-2015
        $(document).ready(function () {
            $('#<%= txtprecio.ClientID%>').blur(function () {
                var precio = $('#<%=txtprecio.ClientID %>').val();
                var precio_moneda = numeral(precio).format('$0,0.00');
                $('#<%=txtprecio.ClientID %>').val(precio_moneda);
                campoVacio();
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h1>         <asp:HiddenField ID="rutaoculta" runat="server" />
                <asp:Label ID="lblenca" runat="server" Width="100%"></asp:Label>&nbsp;
            </h1>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

        <Triggers></Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-xs-12">
                    <h4 class="control-label obligado"><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Giro</h4>
                    <asp:DropDownList ID="cboxgirocli" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="col-lg-12 col-xs-12">
                    <h4 class="control-label  obligado"><i class="fa fa-mail-forward" aria-hidden="true"></i>&nbsp;Dirección</h4>

                    <asp:TextBox ID="txtdireccion" runat="server" CssClass="form-control" MaxLength="250"></asp:TextBox>


                </div>
                <div class="col-lg-12 col-xs-12">
                    <h4 class="control-label"><i class="fa fa-navicon" aria-hidden="true"></i>&nbsp;Nombre/Razón Social</h4>

                    <asp:TextBox ID="txtnombre" runat="server" CssClass="form-control" MaxLength="250"></asp:TextBox>


                </div>

                <div class="col-lg-12 col-xs-12">
                    <h4 class="control-label obligado"><i class="fa fa-info-circle" aria-hidden="true"></i>&nbsp;Contacto
                        <a href="ayuda/captura_de_prospectos.pdf" target="_blank" title="ayuda" class="obligado" id="informacionadd">
                             (Ver mas Información)</a> </h4>

                    <asp:TextBox ID="txtcontacto" runat="server" placeholder="Ingrese un contacto" MaxLength="250" CssClass="form-control"></asp:TextBox><asp:HiddenField ID="ocidcontacto" runat="server" />
                     <asp:Button ID="btnaddcontacto" runat="server" OnClick="btnaddcontacto_Click" Text="Agregar" CssClass="btn btn-success" Width="100%" />
                        <asp:Button ID="btneditacontacto" runat="server" Text="Actualizar Nombre" Visible="False" CssClass="btn btn-info" Width="48%" OnClick="btneditacontacto_Click" />
                        <asp:Button ID="btncancelcontacto" runat="server" Text="Cancelar" Visible="False" CssClass="btn btn-danger" Width="48%" OnClick="btncancelcontacto_Click" />

                </div>
                <div class="col-lg-12 col-sm-12 col-xs-12" id="imgbtndelcont">
                    <div class="table-responsive">
                        <asp:GridView ID="GridContacto" runat="server" CssClass="table table-bordered table-responsive table-condensed" AutoGenerateColumns="False" DataKeyNames="contacto_id" OnRowCommand="GridContacto_RowCommand" OnSelectedIndexChanged="GridContacto_SelectedIndexChanged">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="20px" CommandName="editarContacto" ImageUrl="~/imagenes/btn/icon_editar.png">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>

                                <asp:TemplateField>
                                    <HeaderStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtndelcon" runat="server" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CommandName="eliminarContacto"
                                            ImageUrl="~/imagenes/btn/icon_delete.png" OnClientClick="return confirmar('Se eliminaran los télefonos y correos de este contacto ¿Desea continuar?');" CausesValidation="False" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="contacto_id" HeaderText="Id" Visible="False" />

                                <asp:ButtonField ButtonType="Button" CausesValidation="False" DataTextField="contacto" Text="Contacto" 
                                    HeaderText="Contacto" ShowHeader="True" CommandName="clicContacto">

                                    <ControlStyle BorderStyle="None" CssClass="btn_transparente" />
                                    <ItemStyle BorderStyle="None" CssClass="btn_transparente" />
                                </asp:ButtonField>

                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="ocindicegridcontacto" runat="server" />
                    </div>
                </div>

                <div class="col-lg-12 col-xs-12">

                    <h4 class="control-label">
                        <asp:Label ID="lbltel" runat="server" Text="Telefono"></asp:Label></h4>

                    <asp:TextBox ID="txttelefono" runat="server" placeholder="Ingrese un teléfono" onkeypress="return validarEnteros(event);" MaxLength="15" TextMode="number" CssClass="form-control"></asp:TextBox>

                    <asp:Button ID="btnaddtel" runat="server" OnClick="btnaddtel_Click" Width="100%" CssClass="btn btn-info" Text="Agregar" />
                </div>
                <div class="col-lg-12 col-xs-12">
                    <div class="table-responsive table ">
                        <asp:GridView ID="GridTelefono" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-responsive table-condensed" DataKeyNames="telefono_id" OnRowCommand="GridTelefono_GridTelefono_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="telefono_id" HeaderText="Telefono_id" Visible="False" />
                                <asp:BoundField DataField="contacto_id" HeaderText="Contacto_id" Visible="False" />
                                <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                                <asp:ButtonField ButtonType="Image" CommandName="eliminarTelefono" ImageUrl="~/imagenes/btn/icon_delete.png">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>

                    </div>

                </div>
                <div class="col-lg-12 col-xs-12">
                    <h4 class="control-label">
                        <asp:Label ID="lblcorreo" runat="server" Text="Correo"></asp:Label></h4>

                    <asp:TextBox ID="txtcorreo" runat="server" placeholder="Ingrese un correo" MaxLength="250" CssClass="form-control"></asp:TextBox>

                    <asp:Button ID="btnaddcorreo" runat="server" OnClick="btnaddcorreo_Click" Width="100%" CssClass="btn btn-info" Text="Agregar" />
                </div>
                 <div class="col-lg-12 col-xs-12">
                    <div class="table-responsive table">
                        <asp:GridView ID="GridCorreo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-responsive" DataKeyNames="correo_id" OnRowCommand="GridCorreo_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="correo_id" HeaderText="Correo_id" Visible="False" />
                                <asp:BoundField DataField="contacto_id" HeaderText="Contacto_id" Visible="False" />
                                <asp:BoundField DataField="correo" HeaderText="Correo" />
                                <asp:ButtonField ButtonType="Image" CommandName="eliminarCorreo" ImageUrl="~/imagenes/btn/icon_delete.png">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                
            <!--  SECCION DE ARTICULOS  ADD 30-09-2015  -->
                <section id="articulos">
                    <div class="col-lg-12 col-xs-12">
                        <h4 class="control-label"><i class="fa fa-building" aria-hidden="true"></i>&nbsp;Articulos</h4>
                        <asp:DropDownList ID="cbox_famart" runat="server" CssClass="form-control2" Width="60%" OnSelectedIndexChanged="cbox_famart_SelectedIndexChanged"></asp:DropDownList>
                        <asp:Button ID="btnaddfamart" runat="server" Text="Agregar" CssClass="btn btn-success" Width="38%" OnClick="btnaddfamart_Click" />
                    </div>
                </section>
                <asp:Panel ID="panel_art" runat="server" Visible="False">
                    <div class="col-lg-12 col-xs-12">
                        <h4 class="control-label">Marca</h4>
                        <asp:TextBox ID="txtmarca" runat="server" CssClass="form-control" onblur="campoVacio()"></asp:TextBox>
                    </div>
                    <div class="col-lg-12 col-xs-12">
                        <h4 class="control-label">Distribuidor</h4>
                        <asp:TextBox ID="txtdistribuidor" runat="server" CssClass="form-control" onblur="campoVacio()"></asp:TextBox>
                    </div>
                    <!-- add 26-10-2015 precio -->
                    <div class="col-lg-12 col-xs-12">
                        <h4 class="control-label">Precio $</h4>
                        <asp:TextBox ID="txtprecio"  onkeypress="return validarMontoMoney(event);" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>
                    <div id="btn_mar_dis" class="col-lg-12 col-xs-12" style="display: none">
                        <br />
                        <asp:Button ID="btnaddartdet" runat="server" Text="Agregar" CssClass="btn btn-info" Width="48%" OnClick="btnaddartdet_Click" />
                        <asp:Button ID="btncancelartdet" runat="server" Text="Cancelar" CssClass="btn btn-danger" Width="48%" OnClick="btncancelartdet_Click" />
                    </div>

                </asp:Panel>
                
                <div class="col-lg-12 col-sm-12 col-xs-12">
                    <br />
                    <div class="table-responsive table">
                        <asp:GridView ID="grid_famart_det" runat="server" CssClass="table table-bordered table-responsive table-condensed" AutoGenerateColumns="False" DataKeyNames="idc_prospecto_famartd" OnRowCommand="grid_famart_det_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="eliminafamarticulo" HeaderStyle-Width="20px" ImageUrl="~/imagenes/btn/icon_delete.png" Text="Eliminar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="idc_prospecto_famartd" HeaderText="Idc_prospecto_famartd" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="idc_prospecto_famart" HeaderText="Idc_prospecto_famart" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField ButtonType="Button" CommandName="clic_famartdet"  DataTextField="descripcion" Text="Descripcion" HeaderText="Articulo">
                                    <ControlStyle BorderStyle="None" CssClass="btn_transparente" />
                                    <ItemStyle CssClass="btn_transparente" HorizontalAlign="Center" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="oc_gridfamartdet" runat="server" Value="-1" />
                    </div>
                </div>
                
                <div class="col-lg-12 col-sm-12 col-xs-12">
                    <div class="table-responsive table">
                        <asp:GridView ID="grid_famart_detmar" runat="server" CssClass="table table-bordered table-responsive table-condensed" AutoGenerateColumns="False" DataKeyNames="idc_prospecto_famartdm" OnRowCommand="grid_famart_detmar_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="eliminamarca" ImageUrl="~/imagenes/btn/icon_delete.png" Text="Eliminar">
                                    <HeaderStyle HorizontalAlign="Center" Width="25px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="idc_prospecto_famartdm" HeaderText="Idc_prospecto_famartdm" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="idc_prospecto_famartd" HeaderText="Idc_prospecto_famartd" Visible="False" />
                                <asp:BoundField DataField="marca" HeaderText="Marca">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="distribuidor" HeaderText="Distribuidor">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C2}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                
            <!-- FIN DE SECCION DE ARTICULOS -->
            <div class="col-lg-12 col-sm-12 col-xs-12">
                <h4 class="control-label">Tipo de Obra</h4>
                <!-- el texbox ya no se usa pero se oculta y manda vacio por que la columna esta en la tabla ahora se usa el select -->
                <asp:TextBox ID="txttipodeobra" runat="server" MaxLength="250" CssClass="form-control" Visible="False"> </asp:TextBox>
                <asp:DropDownList ID="cboxtipodeobra" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-lg-12 col-sm-12 col-xs-12">
                <h4 class="control-label">Tamaño de la Obra (m2)</h4>
                <div class="list-group">
                    <!-- ya no se usa el texbox de captura ahora se usan los select  add 01-10-2015-->
                    <asp:TextBox ID="txttamanodeobra" runat="server" MaxLength="250" type="number" CssClass="form-control" Visible="False"></asp:TextBox>
                    <asp:DropDownList ID="cboxobra_tipotam" runat="server" CssClass="form-control">
                        <asp:ListItem>Seleccionar</asp:ListItem>
                        <asp:ListItem>Metros Cuadrados</asp:ListItem>
                        <asp:ListItem>Unidades (Casas, Locales)</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    
                    <asp:DropDownList ID="cboxobra_tam" runat="server" CssClass="form-control">
                        <asp:ListItem>Seleccionar</asp:ListItem>
                        <asp:ListItem>Menos de 50</asp:ListItem>
                        <asp:ListItem>50-100</asp:ListItem>
                        <asp:ListItem>100-150</asp:ListItem>
                        <asp:ListItem>150-200</asp:ListItem>
                        <asp:ListItem>200-250</asp:ListItem>
                        <asp:ListItem>250-300</asp:ListItem>
                        <asp:ListItem>300-350</asp:ListItem>
                        <asp:ListItem>350-400</asp:ListItem>
                        <asp:ListItem>400-450</asp:ListItem>
                        <asp:ListItem>450-500</asp:ListItem>
                        <asp:ListItem>500-550</asp:ListItem>
                        <asp:ListItem>550-600</asp:ListItem>
                        <asp:ListItem>600-650</asp:ListItem>
                        <asp:ListItem>650-700</asp:ListItem>
                        <asp:ListItem>700-750</asp:ListItem>
                        <asp:ListItem>750-800</asp:ListItem>
                        <asp:ListItem>800-850</asp:ListItem>
                        <asp:ListItem>850-900</asp:ListItem>
                        <asp:ListItem>900-950</asp:ListItem>
                        <asp:ListItem>950-1000</asp:ListItem>
                        <asp:ListItem>&gt;1000</asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>
            <div class="col-lg-12 col-sm-12 col-xs-12">
                <h4 class="control-label">Etapa de la Obra</h4>
                <!-- este texbox se oculta y se manda un valor vacio, ahora el valor se toma de un select y se guarda como int, se deja el campo texto para que no truene 
                posteriormente se eliminara -->
                <asp:TextBox ID="txtetapadeobra" runat="server" MaxLength="250" CssClass="form-control" Visible="False"> </asp:TextBox>
                <asp:DropDownList ID="cboxetapaobra" runat="server" CssClass="form-control"></asp:DropDownList>

            </div>
            <div class="col-lg-12 col-sm-12 col-xs-12">
                <h4 class="control-label">Observaciones</h4>
                <asp:TextBox ID="txtobservaciones" runat="server" CssClass="form-control" MaxLength="250"></asp:TextBox>
            </div>
            <div class="col-lg-12 col-sm-12 col-xs-12">
                <h4 class="control-label">Mas Obras</h4>
                <asp:TextBox ID="txtaddtipoobra" runat="server" CssClass="form-control" MaxLength="250"></asp:TextBox>
                <asp:Button ID="btnaddobras" runat="server" Text="Agregar" CssClass="btn btn-success btn-block" OnClick="btnaddobras_Click" />
            </div>
            <div class="col-lg-12 col-sm-12 col-xs-12">
                <asp:ListBox ID="lsttipoobra" runat="server" CssClass="form-control" Height="102px"></asp:ListBox>
                <asp:Button ID="btndelobra" runat="server" CssClass="btn btn-danger btn-block" OnClick="btndelobra_Click" Text="Eliminar" Visible="False" />
            </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Giro add 30-09-2015 -->

  
    <div class="row">
        <div class="col-lg-12 col-sm-12 col-xs-12">
            <h4 class="control-label  obligado">
                <asp:Label ID="lblima1" runat="server" Text="Imagen de la Obra 1"></asp:Label></h4>
            <asp:FileUpload ID="filimagen" runat="server" CssClass="form-control" />
        </div>

        <div class="col-lg-12 col-sm-12 col-xs-12">
            <h4 class="control-label">
                <asp:Label ID="lblima2" runat="server" CssClass="obligado" Text="Imagen de la Obra 2"></asp:Label></h4>
            <asp:FileUpload ID="filimagen2" runat="server" CssClass="form-control" />
        </div>

        <div class="col-lg-12 col-sm-12 col-xs-12">

            <h4 class="control-label col-xs-2">
                <asp:Label ID="lblsesion" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblprospecto" runat="server" Text="lblprospecto" Visible="False"></asp:Label>

                <asp:HiddenField ID="oclatitud" runat="server" />
                <asp:HiddenField ID="oclongitud" runat="server" />

            </h4>
        </div>
        <div class="col-lg-12 col-sm-12 col-xs-12" style="text-align: center;">
            <h4 style="text-align: center;"
                class="control-label obligado"><strong>***Campos Obligados</strong></h4>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
            <asp:UpdatePanel ID="ss" runat="server" UpdateMode="always">
                <ContentTemplate>
                    <asp:Button ID="btnguardar" runat="server" CssClass="btn btn-primary btn-block" OnClientClick="ModalConfirm('Mensaje del Sistema','¿Desea Guardar este Prospecto?','modal fade modal-info');"
                        Text="Guardar" />
                </ContentTemplate>
            </asp:UpdatePanel>
           
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
            <asp:Button ID="btnlimpiar" runat="server" CssClass="btn btn-success btn-block" Text="Limpiar" OnClick="btnlimpiar_Click"
                OnClientClick="return confirmar('¿Deseas Limpiar la Pantalla?');" />


        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <asp:Button ID="btncancelar" runat="server" CssClass="btn btn-danger btn-block" Text="Cancelar" OnClick="btncancelar_Click" />

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
                                <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
