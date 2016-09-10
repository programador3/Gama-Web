<%@ Page Title="" Language="VB" MasterPageFile="~/Global.master" AutoEventWireup="false" CodeFile="pre_embarques_2_elite_m.aspx.vb" Inherits="pre_embarques_2_elite_m" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
  
     <style type="text/css">
        .form-control2 {
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
        .read {        
             border: 1px solid #ccc;
            background-color: #eee;
            opacity: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" Runat="Server">
      <script type="text/javascript">
        function grid2(up, inicio) {
            var GridView2 = document.getElementById('<%=GridView2.ClientID%>');
        var txtindex = document.getElementById('<%=txtindex.ClientID%>');
        if (GridView2 == null) {
            return false;
        }
        else {
            var hasta = GridView2.rows.length - 1;
            var txtitems = document.getElementById('<%=txtitems.ClientID%>');
            if (inicio == 1) {
                txtindex.value = 1;
            }
            else {
                if (up == true) {
                    txtindex.value++;
                    if (txtindex.value > hasta) {
                        txtindex.value = hasta;
                    }
                }
                else {
                    txtindex.value = txtindex.value - 1;
                    if (txtindex.value < 1) {
                        txtindex.value = 1;
                    }
                }
            }


            var txtcodigo = document.getElementById("<%=txtcodigo.ClientID%>");
            var txtdesc = document.getElementById("<%=txtdesc.ClientID%>");
            var txtmarca = document.getElementById("<%=txtmarca.ClientID%>");
            var txtcantidad = document.getElementById("<%=txtcantidad.ClientID%>");
            var txtpendiente = document.getElementById("<%=txtpendiente.ClientID%>");
            var txtex_suc = document.getElementById("<%=txtex_suc.ClientID%>");
            var txtex_total = document.getElementById("<%=txtex_total.ClientID%>");
            var txttransito = document.getElementById("<%=txttransito.ClientID%>");
            var txtdisponible = document.getElementById("<%=txtdisponible.ClientID%>");
            var txtcompras = document.getElementById("<%=txtcompras.ClientID%>");
            var txtfecha_entrega = document.getElementById("<%=txtfecha_entrega.ClientID%>");

            for (var i = 1; i <= hasta; i++) {
                if (i == txtindex.value) {
                    txtcodigo.value = GridView2.rows[i].cells[0].textContent;
                    txtdesc.value = GridView2.rows[i].cells[1].textContent;
                    txtmarca.value = GridView2.rows[i].cells[2].textContent;
                    txtcantidad.value = GridView2.rows[i].cells[3].textContent;
                    txtpendiente.value = GridView2.rows[i].cells[4].textContent;
                    txtex_suc.value = GridView2.rows[i].cells[5].textContent;
                    txtex_total.value = GridView2.rows[i].cells[6].textContent;
                    txttransito.value = GridView2.rows[i].cells[7].textContent;
                    txtdisponible.value = GridView2.rows[i].cells[8].textContent;
                    txtcompras.value = GridView2.rows[i].cells[9].textContent;
                    txtfecha_entrega.value = GridView2.rows[i].cells[10].textContent;
                }
            }
            txtitems.value = txtindex.value + ' de ' + hasta;
        }
        return false;
    }








    </script><script type="text/javascript" >
    function grid2(up,inicio)
    {
        var GridView2 = document.getElementById('<%=GridView2.ClientID%>');
        var txtindex = document.getElementById('<%=txtindex.ClientID%>');
        if(GridView2==null)
        {
            return false;
        }
        else
        {
            var hasta = GridView2.rows.length-1;
            var txtitems = document.getElementById('<%=txtitems.ClientID%>');
            if(inicio==1)
            {
                txtindex.value=1;
            }
            else
            {
                 if(up==true)
                {
                    txtindex.value++;
                    if(txtindex.value>hasta)
                    {
                        txtindex.value=hasta;
                    }
                }
                else
                {
                    txtindex.value= txtindex.value-1;
                    if(txtindex.value<1)
                    {
                        txtindex.value=1;
                    }            
                }
            }
            

            var txtcodigo = document.getElementById("<%=txtcodigo.ClientID%>");
            var txtdesc = document.getElementById("<%=txtdesc.ClientID%>");
            var txtmarca = document.getElementById("<%=txtmarca.ClientID%>");
            var txtcantidad = document.getElementById("<%=txtcantidad.ClientID%>");
            var txtpendiente = document.getElementById("<%=txtpendiente.ClientID%>");
            var txtex_suc = document.getElementById("<%=txtex_suc.ClientID%>");
            var txtex_total = document.getElementById("<%=txtex_total.ClientID%>");
            var txttransito = document.getElementById("<%=txttransito.ClientID%>");
            var txtdisponible = document.getElementById("<%=txtdisponible.ClientID%>");
            var txtcompras = document.getElementById("<%=txtcompras.ClientID%>");
            var txtfecha_entrega = document.getElementById("<%=txtfecha_entrega.ClientID%>");
            
            for(var i=1;i<=hasta;i++)
            {
                if(i==txtindex.value)
                {
                    txtcodigo.value = GridView2.rows[i].cells[0].textContent;
                    txtdesc.value = GridView2.rows[i].cells[1].textContent;
                    txtmarca.value = GridView2.rows[i].cells[2].textContent;
                    txtcantidad.value = GridView2.rows[i].cells[3].textContent;
                    txtpendiente.value = GridView2.rows[i].cells[4].textContent;
                    txtex_suc.value = GridView2.rows[i].cells[5].textContent;
                    txtex_total.value = GridView2.rows[i].cells[6].textContent;
                    txttransito.value = GridView2.rows[i].cells[7].textContent;
                    txtdisponible.value = GridView2.rows[i].cells[8].textContent;
                    txtcompras.value = GridView2.rows[i].cells[9].textContent;
                    txtfecha_entrega.value = GridView2.rows[i].cells[10].textContent;
                }
            }
            txtitems.value = txtindex.value + ' de ' + hasta ;            
        }
        return false;    
    }






</script>

    <div class="row">
       <div class="col-lg-12">
            <h3 class="page-header">Pedidos Pendientes de Entrega(Gama Elite)</h3>
       </div>
    </div>
    <div id="div_contenido" runat="server" style="width: 100%;">
        <table style="width: 100%;">
            <tr>
                <td align="left">

                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" Text="No. Pedido:"></asp:Label>
                                <br /><br />

                </td>
            </tr>
            <tr>
                <td align="left">
                    <div class="styled-select" style="width: 100%">
                        <asp:DropDownList ID="cboembarques" runat="server" CssClass="form-control2"
                            Width="100%" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    
                                <br /><br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table style="width: 100%;">
                        <tr>
                            <td align="right" style="width: 5%; height: 47px;">

                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Observaciones:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtobservaciones" runat="server" Width="100%" TextMode="Multiline" Rows="5"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Saldo:"></asp:Label>
                                
                                <br /><br />
                            </td>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                                    ForeColor="#333333" Width="100%"
                                    Style="font-size: medium; display: none">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                        <asp:BoundField DataField="desart" HeaderText="Descripcion" />
                                        <asp:BoundField DataField="marca" HeaderText="Marca" NullDisplayText="" />
                                        <asp:BoundField DataField="cantidad" HeaderText="cantidad" DataFormatString="{0:n3}" />
                                        <asp:BoundField DataField="pendiente" HeaderText="pendiente" />
                                        <asp:BoundField DataField="existencia" HeaderText="Ex. Suc. Ent." DataFormatString="{0:n3}" />
                                        <asp:BoundField DataField="exi_todo" HeaderText="Ex. Total" DataFormatString="{0:n3}" />
                                        <asp:BoundField DataField="transito" HeaderText="Transito" DataFormatString="{0:n3}" NullDisplayText="" />
                                        <asp:BoundField DataField="disponible" HeaderText="Disponible" DataFormatString="{0:n3}" NullDisplayText="" />
                                        <asp:BoundField DataField="compras" HeaderText="compras" DataFormatString="{0:n3}" NullDisplayText="" />
                                        <asp:BoundField DataField="fecha_entrega" HeaderText="fecha_entrega" DataFormatString="{0:d}" NullDisplayText="" />
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                                <asp:TextBox ID="txtsaldo" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="% Pagado:"></asp:Label>
                                
                                <br /><br />
                            </td>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="idc_preemb" ForeColor="#333333" Width="100%"
                                    Style="font-size: small; display: none;">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="idc_preemb" HeaderText="NO.">
                                            <HeaderStyle Width="105px" />
                                            <ItemStyle Width="105px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha">
                                            <HeaderStyle Width="105px" />
                                            <ItemStyle Width="105px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fecha_entrega" HeaderText="Fec. entrega" />
                                        <asp:BoundField DataField="tipo_camion" HeaderText="Tipo Camion" />
                                        <asp:BoundField DataField="observ" HeaderText="Obs." />
                                        <asp:BoundField DataField="tiempo" HeaderText="Tiempo" />
                                        <asp:BoundField DataField="tiempo_entrega" HeaderText="Tiempo Entrega">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="suc_entrega" HeaderText="Suc. Entrega">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                                        <asp:BoundField DataField="proveedor" HeaderText="Proveedor" />
                                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                                        <asp:BoundField DataField="camion" HeaderText="" />
                                        <asp:BoundField DataField="tipo_entrega" HeaderText="Tipo Entrega" />
                                        <asp:CommandField ButtonType="Button" SelectText="Mostrar" ControlStyle-CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" ControlStyle-Width="100%" ControlStyle-Height="30px"
                                            ShowSelectButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                                <asp:TextBox ID="txtpor_pagado" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Fecha:"></asp:Label>
                                
                                <br /><br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtfecha" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase; resize: none' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Fecha Pactada:"></asp:Label>
                                
                                <br /><br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtfecha_pactada" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Fecha Entrega:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtfecha_e" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">

                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Consignado:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtconsignado" runat="server" Width="100%" Height="50px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2"
                                    TextMode="MultiLine" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Usuario:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtusu" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>

                <td align="center">
                    <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" ForeColor="Blue" Text="Articulos"></asp:Label>
                </td>
            </tr>
            <tr>

                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 25%">


                                <asp:Button ID="btndown" runat="server" Text="&lt;&lt;" Font-Bold="True" Font-Size="Medium"
                                    Height="35px" Width="100%"
                                    CssClass="btn btn-default"
                                    UseSubmitBehavior="False" />
                                <br /><br />


                            </td>
                            <td style="width: 45%">


                                <asp:TextBox ID="txtitems" runat="server" Width="100%" ForeColor="Blue"
                                    Style='text-transform: uppercase; text-align: center;' onfocus="this.blur();"
                                    CssClass="form-control2"
                                    Font-Bold="True" Font-Italic="False" Font-Names="arial" Font-Size="Small" />
                                <br /><br />


                            </td>
                            <td style="width: 25%">


                                <asp:Button ID="btnup" runat="server" Text="&gt;&gt;" Font-Bold="True" Font-Size="Medium"
                                    Height="35px" Width="100%"
                                    CssClass="btn btn-default"
                                    UseSubmitBehavior="False" />
                                <br /><br />


                            </td>
                        </tr>
                    </table>
                </td>
            </tr>



            <tr>
                <td>

                    <table style="width: 100%;">
                        <tr>
                            <td align="right" style="width: 5%">

                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Codigo:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtcodigo" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Desc:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtdesc" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Marca:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtmarca" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Cantidad:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtcantidad" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Pendiente:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtpendiente" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Ex. Suc. Ent:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtex_suc" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Ex. Total:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtex_total" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Transito:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txttransito" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Disponible:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtdisponible" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Compras:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtcompras" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Fecha Entrega:"></asp:Label>
                                <br /><br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtfecha_entrega" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br /><br />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="Button1" runat="server" Text="Cerrar" Font-Bold="True" Font-Size="Medium"
                        Height="40px" Width="100%" 
                        CssClass="btn btn-danger btn-block" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtindex" style="visibility:hidden;" runat="server" CssClass="Ocultar" />
                </td>
            </tr>
        </table>

    </div>
    <div align="center">
                <asp:Label ID="Label1" runat="server" Text="No Hay Pre-Embarques Pendientes" 
            Visible="False" Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Blue"></asp:Label>
    </div>
       
    </asp:Content>

