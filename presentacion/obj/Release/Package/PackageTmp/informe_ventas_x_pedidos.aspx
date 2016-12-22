<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="informe_ventas_x_pedidos.aspx.cs" Inherits="presentacion.informe_ventas_x_pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        #tablard {
            /*-moz-border-radius:10px; /* Firefox */
            /*-webkit-border-radius:10px; /* Safari y Chrome */
            /*-border-radius:10px;*/
            border-radius: 10px 10px 10px 10px;
        }

        #tablard2 {
            /*border-radius:5px; */
            /*-moz-border-radius:10px; /* Firefox */
            /*-webkit-border-radius:10px; /* Safari y Chrome */
            border: 2px solid gainsboro;
            background: #eee;
            border-radius: 10px 10px 10px 10px;
        }

        .redondear {
            -moz-border-radius: 10px; /* Firefox */
            -webkit-border-radius: 10px; /* Safari y Chrome */
            border-radius: 10px 10px 10px 10px;
        }

        #tablard3 {
            -moz-border-radius: 10px; /* Firefox */
            -webkit-border-radius: 10px; /* Safari y Chrome */
            border-radius: 10px 10px 10px 10px;
        }
    </style>

    <script type="text/javascript">

        function pageLoad(sender, args) {
           
        }

        function buscar_cliente() {
            window.open('busc_cliente.aspx?tipo=1');
            //TINY.box.show({iframe:'busc_cliente.aspx?tipo=1',boxid:'frameless',width:630,height:276,fixed:false,maskid:'bluemask',maskopacity:40});
            return false;
        }

        function cerrar() {
            TINY.box.hide();
            return false;
        }

        function ver_informe(txtdesde, txthasta, txtidc_cliente, chktodos, cbosuc, chktodas, chktmk, cbogrupo, btna) {



            if (txtdesde.value == "") {
                alert("Es necesario la fecha inicial.");
                return false;

            }
            if (txthasta.value == "") {
                alert("Es necesario la fecha final.");
                return false;

            }

            var numeros = txtdesde.value.match(/\d+/g);
            var desde = new Date(numeros[2], numeros[1] - 1, numeros[0]);
            //fecha_antes= fecha_antes.toLocaleFormat("%A, %B %e, %Y");

            numeros = txthasta.value.match(/\d+/g);
            var hasta = new Date(numeros[2], numeros[1] - 1, numeros[0]);

            if (desde > hasta) {
                alert("La Fecha Final no Puede Ser Menor a la Fecha Inicial.");
                return false;
            }


            if (chktodos.checked == false) {
                if (txtidc_cliente.value == "0") {
                    alert("Es Necesario Seleccionar un Cliente");
                    return false;

                }
            }
            if (chktodas.checked == false) {
                if (cbosuc.selectedindex <= 0) {
                    alert("Es Necesario Seleccionar Sucursal.");
                    return false;
                }
            }
            if (cbogrupo.selectedindex <= 0) {
                alert("Es Necesario Seleccionar Grupo.");
                return false;
            }
            btna.click();



        }
     
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h3 class=" page-header"><strong>Informe de Ventas por Pedidos</strong></h3>
            <table style="width: 100%; position: relative; left: 0px; right: 0px;">
                <tr>
                    <td>

                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="Small"
                                        Style="font-size: small; font-weight: 700;" Text="Desde:"></asp:Label>
                                </td>
                                <td style="width: 50%">
                                    <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="Small"
                                        Style="font-size: small; font-weight: 700;" Text="Hasta:" Width="40px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtdesde" runat="server" TextMode="Date" Width="100%"
                                        CssClass="form-control2"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txthasta" runat="server"  TextMode="Date"  Width="100%"
                                        CssClass="form-control2"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
                <tr id="buscar_cliente" runat="server" visible="false" >
                    <td>
                        <h4 style="text-align:center;"><strong>Buscar Cliente</strong></h4>
                         <asp:TextBox ID="txtbuscar" runat="server" style="display:inline" AutoPostBack="true" 
                        onfocus="if(this.value=='Buscar...')this.value='';" 
                        onblur="if(this.value=='')this.value='Buscar...'" Width="100%" 
                        CssClass=" form-control" OnTextChanged="txtbuscar_TextChanged"></asp:TextBox>
                        <br />
                        <div class="table table-responsive">
                            <asp:GridView DataKeyNames="idc_cliente,nombre" ID="drclientes" runat="server" Width="100%" CssClass=" table table-responsive table-condensed table-bordered"
                                Style="background-color: white;"
                                AutoGenerateColumns="False" OnRowCommand="drclientes_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                         <ItemTemplate>
                                            <asp:ImageButton CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ID="imgselec" runat="server" Height="25px" Width="25px" ImageUrl="~/imagenes/btn/icon_autorizar.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="idc_cliente" HeaderText="idc_cliente" Visible="False" />
                                    <asp:BoundField DataField="nombre" HeaderText="nombre"  />
                                    <asp:BoundField DataField="rfccliente" HeaderText="rfccliente"  />
                                    <asp:BoundField DataField="cveadi" HeaderText="cveadi"  />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td align="right" style="width: 6%">
                                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="Small"
                                        Style="font-size: small; font-weight: 700;" Text="Cliente:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcliente" runat="server" ForeColor="Black" Height="30px"
                                        onfocus="this.blur();" Width="100%"
                                        CssClass="form-control2"></asp:TextBox>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="imgcliente" runat="server" Height="30px"
                                        ImageUrl="~/imagenes/btn/icon_buscar.png" Width="30px" OnClick="imgcliente_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 6%">&nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="chktodos" runat="server" AutoPostBack="True" Checked="True"
                                         CssClass="radio3 radio-check radio-info radio-inline"
                                        Text="Todos" OnCheckedChanged="chktodos_CheckedChanged" />
                                </td>
                                <td style="width: 2%">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 6%" align="right">
                                    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="Small"
                                        Style="font-size: small; font-weight: 700;" Text="Sucursal:"></asp:Label>
                                </td>
                                <td>
                                    <div id="slct2" class="styled-select" style="width: 100%">
                                        <asp:DropDownList ID="cbosuc" runat="server" ForeColor="Black" Height="36px" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td style="width: 6%">&nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="chktodas" runat="server" AutoPostBack="True"  CssClass="radio3 radio-check radio-info radio-inline"
                                         Text="Todas" OnCheckedChanged="chktodas_CheckedChanged" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td align="right" style="width: 6%">
                                    <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="Small"
                                        Style="font-size: small; font-weight: 700;" Text="Agrupado:"></asp:Label>
                                </td>
                                <td>
                                    <div style="width: 100%;" class="styled-select">
                                        <asp:DropDownList ID="option1" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="option1_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">Pedido</asp:ListItem>
                                            <asp:ListItem Selected="False" Value="2">Cliente</asp:ListItem>
                                            <asp:ListItem Selected="False" Value="3">Agente</asp:ListItem>
                                            <asp:ListItem Selected="False" Value="4">Usuario</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td align="right" style="width: 6%">
                                    <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="Small"
                                        Style="font-size: small; font-weight: 700;" Text="Tipo:"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="option2" runat="server" AutoPostBack="True"
                                        BorderStyle="None" CssClass="redondear" Font-Names="arial" Font-Size="Small"
                                        Height="15px" RepeatDirection="Horizontal" TextAlign="right" Width="100%" OnSelectedIndexChanged="option2_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="Detallado" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Resumido" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chktmk" runat="server" Enabled="False" Font-Bold="True"  CssClass="radio3 radio-check radio-info radio-inline"
                            Font-Names="arial" Font-Size="Small" Text="Pedidos TMK contra Movil" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 6%" align="right">
                                    <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="Small"
                                        Style="font-size: small; font-weight: 700;" Text="Grupo:"></asp:Label>
                                </td>
                                <td style="width: 94%">
                                    <div id="slect" style="width: 100%" class="styled-select">
                                        <asp:DropDownList ID="cbogrupo" runat="server" ForeColor="Black" Height="36px"
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnaceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary btn-block"/>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btncerrar" runat="server"
                            CssClass="btn btn-danger btn-block" Text="Salir" ToolTip="Salir" OnClick="btncerrar_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btna" runat="server" CssClass="Ocultar" Text="aceptar" OnClick="btna_Click" />
                        <asp:TextBox ID="txtidc_cliente" runat="server" CssClass="Ocultar" Width="68px">0</asp:TextBox>
                        <%--                                <asp:RadioButtonList ID="option11" runat="server" AutoPostBack="True" 
                                    BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="2px" 
                                    CssClass="redondear" Font-Names="arial" Font-Size="Small" Height="19px" 
                                    RepeatDirection="Horizontal" TextAlign="right" Width="100%">
                                    <asp:ListItem Selected="True" Text="Pedido" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Cliente" Value="2"></asp:ListItem>
                                    <asp:ListItem Value="3">Agente</asp:ListItem>
                                    <asp:ListItem Value="4">Usuario</asp:ListItem>
                                </asp:RadioButtonList>--%>
                    </td>
                </tr>
            </table>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btna" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
