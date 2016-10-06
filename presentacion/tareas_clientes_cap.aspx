<%@ Page Title="Agregar Tarea" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_clientes_cap.aspx.cs" Inherits="presentacion.tareas_clientes_cap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
        $(document).ready(function () {
           <%-- $('#' + '<%=txtfecha.ClientID%>').prop("type", "date");
            $('#' + '<%=btnver.ClientID%>').click(function () {
                if ($('#' + '<%=txtfecha.ClientID%>').val() == "") {
                    alert('Ingrese Fecha.');
                    return false;
                }
            });
            $('.rd-input')--%>
        });

        function validarMontoMoney(e, field) {
            key = e.keyCode ? e.keyCode : e.which
            // backspace
            if (key == 8) return true
            // 0-9
            if (key > 47 && key < 58) {
                if (field.value == "") return true
                regexp = /.[0-9]{3}$/
                return !(regexp.test(field.value))
            }
            // .
            if (key == 46) {
                if (field.value == "") return false
                regexp = /^[0-9]+$/
                return regexp.test(field.value)
            }
            // other key
            return false
        }
        //VALIDA QUE SOLO SEAN NUMEROS ENTEROS REALES
        function validarEnteros(e) {
            k = (document.all) ? e.keyCode : e.which;
            if (k == 8 || k == 0) return true;
            patron = /[0-9\s\t]/;
            n = String.fromCharCode(k);
            return patron.test(n);
        }
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Desea Guardar Sus Datos?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function GiftArt(mensaje) {
            swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '1000', showConfirmButton: false });
        }
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
    </script>
    <style type="text/css">
        
        .div-descripion {
            overflow: hidden;
            min-width: 79px;
            text-align: center;
            display: initial;
            padding: 0;
            margin: 0;
        }

        .div-V, .div-v {
            background: lime;
            color: Black;
            text-align: center;
            min-width: 20px;
        }

        .div-N, .div-n {
            background: yellow;
            color: Black;
            text-align: center;
            min-width: 20px;
        }

        .div-P, .div-p {
            background: orange;
            color: Black;
            text-align: center;
            min-width: 20px;
        }

        .flat-blue .card {
            border: 1px solid #000000;
            padding: 10px;
            background-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <h2 class=" page-header">
        Tareas de Agente
    </h2>
    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</strong></h4>
            <asp:TextBox runat="server" ID="txtfecha" CssClass="form-control" TextMode="Date">
            </asp:TextBox>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente</strong></h4>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtcliente"
                        CssClass="form-control" onfocus="this.blur();"></asp:TextBox>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <h4><strong>RFC</strong></h4>
                    <asp:TextBox ReadOnly="true" runat="server" ID="txtrfc" onfocus="this.blur();"
                        CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-lg-6 col-md-6 col-sm-12">
                    <h4><strong>CVE</strong></h4>
                    <asp:TextBox ReadOnly="true" runat="server" ID="txtcve" onfocus="this.blur();"
                        CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-lg-12">
                    <h4><strong><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Meta</strong> <small style="color: red; font: bold;"><strong>Si la meta es 0 DEBE INGRESAR OBSERVACIONES</strong></small></h4>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtmetaglobal" CssClass="form-control"
                        Text="0" onfocus="this.blur();"></asp:TextBox>
                </div>

                <div class="col-lg-12">
                    <h4><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Observaciones</strong></h4>
                    <asp:TextBox runat="server" ReadOnly="false" placeholder="Observaciones" ID="txtobservaciones_"
                        CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </div>
                <div class="col-lg-12">
                    <div style="border: 1px solid; padding: 10px;background-color:white;">
                        <h4 style="text-align: center;"><strong><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Agrega un Articulo</strong></h4>

                        <asp:TextBox placeholder="Buscar Articulo" runat="server" ID="txtbuscararticulo" AutoPostBack="true"
                            CssClass="form-control2" Width="60%" OnTextChanged="txtbuscararticulo_TextChanged"></asp:TextBox>

                        <asp:Button ID="btnbuscarm" runat="server" Style="width: 38%;" Text="Buscar Articulo"
                            CssClass="btn btn-info" OnClick="txtbuscararticulo_TextChanged" />
                        <div runat="server" id="aded" visible="false">
                            <asp:DropDownList ID="cbarticulos" Style="width: 100%"
                                runat="server" CssClass=" form-control">
                            </asp:DropDownList>

                            <asp:Button ID="btnaddarticulo" runat="server" Style="width: 100%;" Text="Agregar Articulo"
                                CssClass="btn btn-info btn-block" OnClick="btnaddarticulo_Click" />
                        </div>
                    </div>

                </div>
                
                <div class="col-lg-12">
                    <div style="border: 1px solid; padding: 10px; background-color:white;">
                        <h4 style="text-align: center;"><strong><i class="fa fa-database" aria-hidden="true"></i>&nbsp;Listado de Articulos</strong></h4>
                        <asp:Button ID="btnver" runat="server" OnClientClick="GiftArt('Estamos Cargando los Articulos');" Text="Ver Listado de Articulos" CssClass="btn btn-primary btn-block" OnClick="btnver_Click" />
                        <asp:Button ID="btnocultar" Visible="false" runat="server"  Text="Ocultar Listado de Articulos" CssClass="btn btn-primary btn-block" OnClick="btnver2_Click" />
               
                        <asp:Repeater ID="repeat" runat="server">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="upapapa" runat="server" UpdateMode="always">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtcantidad" EventName="TextChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="card">

                                            <label><strong>Codigo: </strong><%#Eval("codigo") %>  </label>
                                            <br />
                                            <label><strong>Articulo: </strong><%#Eval("desart") %>    </label>
                                            <br />
                                            <label style="width:65%"><strong>UM: </strong><%#Eval("um") %></label>
                                            <label class="div-V" style="width:30%;" ><strong>Tipo: </strong><%#Eval("tipo") %></label>
                                            <label style="width:100%"><strong>Precio: $ </strong><%#Eval("precio") %></label>
                                            <strong style="width:28%">Meta: $ </strong>
                                            <asp:TextBox ReadOnly="true" CssClass="form-control2" Width="70%" runat="server" ID="txtmeta" onfocus="this.blur();"
                                                Text='<%# Decimal.Round(Convert.ToDecimal(Eval("meta")), 2).ToString()  %>'  style="font-size:11px;"></asp:TextBox>                                            
                                            <asp:Button ID="Button1" runat="server" Text="Seleccionar Articulo" OnClick="Button1_Click"
                                                CssClass='<%# (Convert.ToDecimal(Eval("cantidad")) > 0? "btn btn-success btn-block": "btn btn-default btn-block")   %>'
                                                CommandName='<%#Eval("idc_articulo").ToString() %>' />                                          
                                            <asp:Label ID="lblprecio" runat="server" Text='<%#Eval("precio") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblid" runat="server" Text='<%#Eval("idc_articulo") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbltipo" runat="server" Text='<%#Eval("tipo") %>' Visible="false"></asp:Label>
                                            <asp:Panel ID="panelcantidad" Visible='<%# (Convert.ToDecimal(Eval("cantidad")) > 0? true: false)   %>' runat="server">
                                                <strong>Ingrese la Cantidad de Articulos</strong>
                                                <asp:TextBox onfocus="$(this).val('')" ReadOnly="false" runat="server" ID="txtcantidad"
                                                    onkeypress='<%# (Convert.ToBoolean(Eval("decimales")) == true ? "return validarMontoMoney(event,this);": "return validarEnteros(event);")   %>'
                                                    CssClass="form-control" placeholder="Cantidad" AutoPostBack="true"
                                                    OnTextChanged="txtcantidad_TextChanged"
                                                    Text='<%#Convert.ToInt32((Convert.ToDecimal(Eval("cantidad"))))  %>'></asp:TextBox>

                                                <strong>Puede Ingresar Observaciones</strong>
                                                <asp:TextBox ReadOnly="false" runat="server" ID="txtobservaciones"
                                                    CssClass="form-control" placeholder="Observaciones" TextMode="MultiLine" style="font-size:11px;"
                                                    Rows="2" Text='<%#Eval("observ") %>'></asp:TextBox>
                                            </asp:Panel>
                                            <asp:Label ID="lblerror" runat="server" Text="" Style="background-color: red; color: white;" Visible="false"></asp:Label>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <div style="text-align: right;" id="s" runat="server" visible="true">
                            <div class="div-descripion div-V">
                                Venta
                            </div>
                            <div class="div-descripion div-N">
                                Negociar
                            </div>
                            <div class="div-descripion div-P">
                                Propuesta
                            </div>
                        </div>
                    </div>
                </div>
               
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
        </div>
    </div>
            <!-- Modal -->
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
