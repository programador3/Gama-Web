<%@ Page Title="Cotizaciones" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cotizaciones_correo.aspx.cs" Inherits="presentacion.cotizaciones_correo" %>
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
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: "success",
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                allowEscapeKey: false
            },
               function () {
                   location.href = URL;
               });
        }
        function ModalClose() {
            $('#myModalSelectRecoge').modal('hide');
        }
        function ModalConfirmRecoge(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalSelectRecoge').modal('show');
            $('#myModalSelectRecoge').removeClass('modal fade modal-info');
            $('#myModalSelectRecoge').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
        function editar_articulo()
    {
       var cbomaster = document.getElementById("<%=cbomaster.ClientID%>");
       var cboproductos = document.getElementById("<%=cboproductos.ClientID%>");
       if(cbomaster!=null)
       {
            if(cbomaster.options.length>0)
            {
                window.open("editar_precios_cantidad.aspx?edit=0&cdi=" + cbomaster.options[cbomaster.selectedIndex].value);
                return false;
            }
            else
            {
                alert("Seleccionar Producto.");
                return false;
            }
       }
       else if(cboproductos!=null)
        {
                 if(cboproductos.options.length>0)
                {
                    window.open("editar_precios_cantidad.aspx?edit=0&cdi=" + cboproductos.options[cboproductos.selectedIndex].value);
                    return false;                
                }
                else
                {
                    alert("Seleccionar Producto.");
                    return false;
                }
            
        }
        
        }
           var myvar_env;
    function mostrar_procesar_env()
    {} 
    
    function myStopFunction_env()
    {
        clearTimeout(myvar_env);
        document.getElementById('env_cot').style.display ='none';
    }


    var myvar_busq;
    function mostrar_procesar_busq(tipo)
    {
        
    } 
    
    function myStopFunction_busq()
    {
        clearTimeout(myvar_busq);
        document.getElementById('proc_busq').style.display ='none';
    }
    

    var myvar_grid;
    function mostrar_procesar_grid()
    {
        myvar_grid = setTimeout(function(){document.getElementById('ref_grid').style.display =''}, 0);
    } 
    
    function myStopFunction_grid()
    {
        clearTimeout(myvar_grid);
        document.getElementById('ref_grid').style.display ='none';
    }

    function Gifts(mensaje) {
        swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '6000', showConfirmButton: false });
    }
    function ConfirmaRegresa() {
        var txt;
        var r = confirm("Desea Enviar la Cotizacion?");
        if (r == true) {
            return true;
        } else {
            return false;
        }
    }
    function editar_precios_cantidad_1(idc_articulo) {
        var ruta = "editar_precios_cantidad.aspx?edit=1&cdi=" + idc_articulo;
        //var width =screen.width- 100;             //document.width - 50;
        //var height = screen.availHeight- 100;
        //TINY.box.show({iframe:ruta ,boxid:'frameless',width:width,height:height,fixed:false,maskid:'bluemask',maskopacity:40,top:2,left:2})
        window.open(ruta);
        return false;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
        <h2 class=" page-header">Cotizaciones</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="Yes2" />
            <asp:PostBackTrigger ControlID="lnkconsignado" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class=" col-lg-12" id="div_buscar" runat="server" visible="false">
                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Buscar Cliente</strong></h5>
                    <asp:TextBox AutoPostBack="true" ReadOnly="false" OnTextChanged="lnkbuscar_Click" Width="80%" runat="server" ID="txtcliente" CssClass="form-control2"
                        placeholder="Buscar Cliente"></asp:TextBox>
                    <asp:LinkButton ID="lnkbuscar" runat="server" CssClass="btn btn-info" Width="18%" OnClick="lnkbuscar_Click"> <i class="fa fa-search" aria-hidden="true"></i>&nbsp;</asp:LinkButton>
                    <asp:DropDownList ID="cboclientes" runat="server" CssClass=" form-control" Width="100%"></asp:DropDownList>
                    <asp:LinkButton ID="lnkbuscaraceptar" CssClass="btn btn-primary btn-block" runat="server" OnClick="lnkbuscaraceptar_Click">Aceptar</asp:LinkButton>
                </div>
                <div class="col-lg-12" id="div_info" runat="server" visible="false">
                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente</strong></h5>
                    <asp:TextBox Style="font-size: 11px;" runat="server" ReadOnly="true" ID="txtnombre" CssClass="form-control2" Width="100%" onfocus="this.blur();"></asp:TextBox>
                    <label style="font-size: 14px; width: 49%"><strong><i class="fa fa-credit-card" aria-hidden="true"></i>&nbsp;RFC</strong></label>
                    <label style="font-size: 14px; width: 49%"><strong>CVE</strong></label>
                    <asp:TextBox Width="49%" Style="font-size: 11px;" ReadOnly="true" runat="server" ID="txtrfc" onfocus="this.blur();"
                        CssClass="form-control2"></asp:TextBox>

                    <asp:TextBox Width="49%" Style="font-size: 11px;" ReadOnly="true" runat="server" ID="txtcve" onfocus="this.blur();" CssClass="form-control2"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="Status:" Visible="False" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtstatus" runat="server" CssClass=" form-control" onfocus="this.blur()" Width="100%" Visible="False"></asp:TextBox>

                    <h5><strong><i class="fa fa-truck" aria-hidden="true"></i>&nbsp;Tipo de Entrega</strong></h5>

                    <asp:DropDownList ID="cboentrega" runat="server" CssClass=" form-control" AutoPostBack="true" OnSelectedIndexChanged="cboentrega_SelectedIndexChanged">
                        <asp:ListItem Value="1">Entregamos</asp:ListItem>
                        <asp:ListItem Value="3">Recoge Cliente</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="lnkconsignado" Text="Consignado" CssClass="btn btn-primary" Width="49%" runat="server" OnClick="lnkconsignado_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lnkcaptura" Text="Capturar Articulos" CssClass="btn btn-info" Width="49%" runat="server" OnClick="lnkcaptura_Click"></asp:LinkButton>
                    <div align="center" bgcolor="SteelBlue" style="background-color: #4682B4; padding-left: 2%; border-radius: 5px;">
                        <h5>
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial" BackColor="SteelBlue"
                                Font-Overline="False" Font-Size="10pt" ForeColor="White" Text="Codigo Articulo"
                                Width="100px"></asp:Label></h5>
                    </div>
                    <asp:TextBox ID="txtcodigo" runat="server" Width="100%" ReadOnly="true" CssClass=" form-control"></asp:TextBox>
                    <asp:DropDownList ID="cboproductos" runat="server" Font-Names="arial"
                        Font-Size="Small" ForeColor="Black" Height="34px" Visible="False"
                        Width="100%">
                    </asp:DropDownList>

                    <asp:DropDownList ID="cbomaster" runat="server" Font-Bold="True"
                        ForeColor="Black" Height="35px" Visible="False" Width="100%">
                    </asp:DropDownList>

                    <asp:Button ID="btn_seleccionar_master" runat="server"
                        CssClass="btn btn-primary" Text="Seleccionar Articulo" Visible="False" Width="100%" OnClick="btn_seleccionar_master_Click" />

                    <asp:Button ID="btnmaster" runat="server"
                        CssClass="btn btn-primary" Text="Master" Visible="False" Width="100%" OnClick="btnmaster_Click" />

                    <asp:Button ID="btnbuscar_codigo" runat="server" Width="100%"
                        CssClass="btn btn-default" Text="Buscar Articulo" Visible="false" OnClick="btnbuscar_codigo_Click" />                  
                    <asp:Label ID="lbliva" runat="server" Font-Bold="True" CssClass="Ocultar" Font-Names="arial"
                        Font-Size="Small" ForeColor="Black" Text="I.V.A." Visible="False"></asp:Label>
                    <h4 style="text-align:center;"><strong>Articulos</strong></h4>
                    <div class="table table-responsive">
                        <asp:DataGrid ID="gridprodcotizados" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive table-bordered table-condensed"
                            Width="100%" OnItemCommand="gridprodcotizados_ItemCommand" OnItemDataBound="gridprodcotizados_ItemDataBound">
                            <EditItemStyle CssClass="SelectedRowStyle" />
                            <SelectedItemStyle CssClass="SelectedRowStyle" />
                            <ItemStyle CssClass="AltRowStyle" Font-Names="arial" Font-Size="Small" />
                            <HeaderStyle ForeColor="White" BackColor="Gray" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Codigo">
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.codigo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Descripción">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="U.M.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.UM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Cantidad">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtcantidad" runat="server" ForeColor="Black"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.cantidad") %>' Width="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.cantidad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Precio">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.precio", "{0:N4}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Importe">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.importe", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Ult. Precio Fac.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Fecha Ult. Precio">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Ocultar" />
                                    <HeaderStyle CssClass="Ocultar" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton6" runat="server" CommandName="Guardar"
                                                        Height="26px" ImageUrl="~/Iconos/save2.ico" ToolTip="Guardar" Width="26px" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton7" runat="server" CommandName="Cancelar"
                                                        Height="26px" ImageUrl="~/Iconos/cancel.png" ToolTip="Cancelar" Width="26px" />
                                                </td>
                                            </tr>

                                        </table>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:ImageButton ID="imgmobile" runat="server" Height="26px"
                                                        ImageUrl="~/imagenes/btn/icon_editar.png" ToolTip="Editar" Width="26px" />
                                                </td>
                                                <td style="border: none;">
                                                    <asp:ImageButton ID="ImageButton5" runat="server" CommandName="Eliminar"
                                                        Height="26px" ImageUrl="~/imagenes/btn/icon_delete.png" ToolTip="Eliminar" Width="26px" />
                                                </td>
                                                <td style="border: none;">
                                                    <asp:ImageButton ID="imgult" runat="server"
                                                        Height="26px" ImageUrl="~/imagenes/calendar3.gif" ToolTip="Ultimo Precio Facturado" Width="26px" />
                                                </td>


                                            </tr>



                                        </table>
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
                            </Columns>
                            <HeaderStyle CssClass="RowStyle" />
                        </asp:DataGrid>
                    </div>
                     <div style="text-align:center">
                          <asp:Label style="text-align:left;" ID="Label14" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Medium" ForeColor="Black" Text="Total"></asp:Label>
                    <asp:TextBox ID="txttotal1" runat="server" ReadOnly="true"
                        CssClass="form-control" onfocus="this.blur()" Width="100%"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txttotalmaniobras" runat="server"
                        CssClass="form-control" Font-Bold="True" Font-Italic="True"
                        Font-Size="Small" ForeColor="Firebrick" ReadOnly="true"
                        onfocus="this.blur()" Width="100%"></asp:TextBox>
                    
                    <br />
                    <div style="border-top-style: solid; border-top-width: 2px; border-top-color: Navy;">
                        <asp:TextBox ID="txttotal" runat="server"
                            CssClass="form-control" ReadOnly="true"
                            onfocus="this.blur()" Width="100%"></asp:TextBox>
                    </div>
                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Italic="True"
                        Font-Size="Small" ForeColor="Firebrick" Text="*Maniobras"></asp:Label>

                    <asp:Label ID="lblroja" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="9pt" ForeColor="Red"
                        Text="La Sucursal de Entrega no corresponde a la Lista de Precios del Cliente."
                        Visible="False"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="Ocultar" Width=""></asp:TextBox>
                     </div>

                </div>
            </div>
            <asp:TextBox Style="font-size: 11px;" ReadOnly="true" runat="server" ID="txtid" Visible="false"></asp:TextBox>
            <asp:Button ID="btnbuscarprod" runat="server" CssClass="Ocultar" Height="23px"
                Text="Buscar" />
            <asp:Button ID="btncancelar_edit" runat="server" CssClass=" Ocultar"
                Width="10px" OnClick="btncancelar_edit_Click" />
            <asp:Button ID="btnguardar_edit" runat="server" CssClass="Ocultar" OnClick="btnguardar_edit_Click" />
            <asp:Button ID="btnref" runat="server" CssClass=" Ocultar" OnClick="btnref_Click" />
            <asp:TextBox ID="txtidc_colonia" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtzm" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtrestriccion" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtcalle" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtnumero" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtCP" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtcolonia" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtestado" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtmunicipio" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtpais" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txttoneladas" runat="server" CssClass="Ocultar" />
            <asp:CheckBox ID="chkton" runat="server" CssClass="Ocultar" />
            <asp:TextBox ID="txtproy" runat="server" CssClass="Ocultar" Text="0" />
            <asp:TextBox ID="txtsucursalr" runat="server" CssClass="Ocultar"></asp:TextBox>
            <asp:TextBox ID="txt_consignado" Text="0" runat="server" CssClass="Ocultar"></asp:TextBox>
            <asp:TextBox ID="txtlistap" runat="server" CssClass="Ocultar" Width=""></asp:TextBox>
            <asp:Label ID="lbl_idc" runat="server" CssClass="Ocultar"></asp:Label>
            <asp:TextBox ID="txtidc_articulo" runat="server" CssClass="Ocultar" Text="0"
                Width="10px"></asp:TextBox>
            <asp:Button ID="btneditar_art" runat="server" CssClass="Ocultar" />
            <asp:Label ID="lblbuscar" runat="server" CssClass="Ocultar" Text="Buscar"></asp:Label>
            <asp:Label ID="lblbuscarp" runat="server" CssClass="Ocultar" Text="buscarp"></asp:Label>
            <asp:Button ID="btnrefresh" runat="server" CssClass="Ocultar"
                Text="refrescar" />
            <asp:Button ID="btnmaniobras" runat="server" CssClass="Ocultar" />
            <table style="width: 100%; display: none;">
                <tr>
                    <td style="width: 1%;"></td>
                    <td align="center" style="width: 31%;">
                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="arial"
                            Font-Size="Medium" ForeColor="Black" Text="Subtotal"></asp:Label>
                    </td>
                    <td align="center" style="width: 31%;">
                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="arial"
                            Font-Size="Medium" ForeColor="Black" Text="I.V.A."></asp:Label>
                    </td>
                    <td align="center" style="width: 31%;">&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txtsubtotal" runat="server"
                            CssClass="form-control"
                            onfocus="this.blur()" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtiva1" runat="server"
                            CssClass="form-control"
                            onfocus="this.blur()" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="color: Firebrick; font-weight: bold;">*</td>
                    <td>
                        <asp:TextBox ID="txtmaniobrassub" runat="server"
                            CssClass="form-control"
                            onfocus="this.blur()" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtiva2" runat="server"
                            CssClass="form-control"
                            onfocus="this.blur()" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td style="border-top-style: solid; border-top-width: 2px; border-top-color: Navy;">&nbsp;</td>
                </tr>
            </table>
            <div class=" row">
                <div class="col-lg-12">
                    <asp:LinkButton ID="LinkButton1" OnClientClick="Gifts('Estamos Enviando la Cotización');" CssClass="btn btn-primary btn-block" runat="server" OnClick="LinkButton1_Click">Enviar Cotización</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-info btn-block" runat="server" OnClick="LinkButton2_Click">Nueva Cotizacion</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" CssClass="btn btn-danger btn-block" runat="server" OnClick="LinkButton3_Click">Salir</asp:LinkButton>
                </div>
            </div>

            <div class="modal fade modal-info" id="myModalSelectRecoge" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
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
                                    <asp:DropDownList ID="cbosucursales" runat="server" CssClass="form-control">
                                    </asp:DropDownList>


                                </div>
                                <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="alert alert-danger fresh-color" role="alert" id="msgerror" runat="server" visible="false">
                                        <strong>Error</strong>
                                        <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-6 col-xs-6">
                                <asp:Button ID="Yes2" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes2_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
