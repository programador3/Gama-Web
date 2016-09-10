<%@ Page Language="VB" MasterPageFile="~/Global.master" AutoEventWireup="false" CodeFile="generar_embarque_de_pre-embarques_m.aspx.vb" Inherits="generar_embarque_de_pre_embarques_m" title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" Runat="Server">

<script type="text/javascript" >
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
            var  txtdesc = document.getElementById("<%=txtdesc.ClientID%>");
            var txtum = document.getElementById("<%=txtum.ClientID%>");
            var txtpe = document.getElementById("<%=txtpe.ClientID%>");
            var txtexistencia = document.getElementById("<%=txtexistencia.ClientID%>");
            var txtoc = document.getElementById("<%=txtoc.ClientID%>");
            
            for(var i=1;i<=hasta;i++)
            {
                if(i==txtindex.value)
                {
                    txtcodigo.value = GridView2.rows[i].cells[0].textContent;
                    txtdesc.value = GridView2.rows[i].cells[1].textContent;
                    txtum.value = GridView2.rows[i].cells[2].textContent;
                    txtpe.value = GridView2.rows[i].cells[3].textContent;
                    txtexistencia.value = GridView2.rows[i].cells[4].textContent;
                    txtoc.value = GridView2.rows[i].cells[5].textContent;
                }
            }
            txtitems.value = txtindex.value + ' de ' + hasta ;
            
        }
        return false;    
    }






</script>
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

    
<div class="row">
    <div class="col-lg-12">
        <h3 class="page-header">Generar Embarque de Pre-Embarque</h3>
    </div>
</div>
       
       
       
        
    <div id="div_contenido" runat="server" style="width: 100%;">
        <table style="width: 100%;">
            <tr>
                <td align="right">

                    <table id="disponibilidad" style="width: 100%;" runat="server">
                        <tr>
                            <td style="width: 1%;">
                                <asp:Image ID="Image2" runat="server" Height="23px"
                                    ImageUrl="~/imagenes/verde.jpg" Width="17px" />
                                <br />
                                <br />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label4" runat="server" Text="Camiones Disponibles"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/rojo.jpg" Height="23px" Width="16px" />
                                <br />
                                <br />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label3" runat="server" Text="No Disponibles"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td align="left">

                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" Text="No. Pre Embarque:"></asp:Label>
                    <br />
                    <br />

                </td>
            </tr>
            <tr>
                <td align="left">
                    <div class="styled-select" style="width: 100%">
                        <asp:DropDownList ID="cboembarques" runat="server" CssClass="form-control2"
                            Width="100%" AutoPostBack="True">
                        </asp:DropDownList>
                        <br />
                        <br />
                    </div>

                </td>
            </tr>
            <tr>
                <td align="left">
                    <table style="width: 100%;">
                        <tr>
                            <td align="right" style="width: 5%; height: 47px;">

                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Fecha:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td style="height: 47px">
                                <asp:TextBox ID="txtfecha" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Fec. Entrega:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtfecha_e" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="T. Camion:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txttipocamion" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">

                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Obs:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtobs" runat="server" Width="100%" Height="60px"
                                    Style='text-transform: uppercase; resize: none' onfocus="this.blur();"
                                    CssClass="form-control2"
                                    TextMode="MultiLine" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Tiempo:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txttiempo" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Tiempo Entrega:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txttiempoe" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Suc. Entrega:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtsuc_e" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Nivel:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtnivel" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Proveedor:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtproveedor" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Usuario:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtusuario" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="T. Entrega:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txttipoe" runat="server" Width="100%" Height="35px" Style='text-transform: uppercase' onfocus="this.blur();"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>

                <td align="center">
                    <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" ForeColor="Blue" Text="Articulos"></asp:Label>
                    <br />
                    <br />
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
                                <br />
                                <br />


                            </td>
                            <td style="width: 45%">


                                <asp:TextBox ID="txtitems" runat="server" Width="100%" ForeColor="Blue"
                                    Style='text-transform: uppercase; text-align: center;' onfocus="this.blur();"
                                    CssClass="form-control2"
                                    Font-Bold="True" Font-Italic="False" Font-Names="arial" Font-Size="Small" />
                                <br />
                                <br />


                            </td>
                            <td style="width: 25%">


                                <asp:Button ID="btnup" runat="server" Text="&gt;&gt;" Font-Bold="True" Font-Size="Medium"
                                    Height="35px" Width="100%"
                                    CssClass="btn btn-default"
                                    UseSubmitBehavior="False" />
                                <br />
                                <br />


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
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtcodigo" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Desc:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtdesc" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="UM:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtum" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Pre-Embarque:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtpe" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Existencia:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtexistencia" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Orden Compra:"></asp:Label>
                                <br />
                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtoc" runat="server" Width="100%" Height="35px"
                                    Style='text-transform: uppercase' onfocus="this.blur();"
                                    onkeyup="valid(this,'special')" onblur="valid(this,'special')"
                                    CssClass="form-control2" />
                                <br />
                                <br />
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
                    <asp:TextBox ID="txtindex" runat="server" CssClass="Ocultar" Style="visibility: hidden;" />
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                        ForeColor="#333333" Width="100%"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                        Style="font-size: medium; display: none">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                            <asp:BoundField DataField="desart" HeaderText="Descripcion" />
                            <asp:BoundField DataField="unimed" HeaderText="U.M." />
                            <asp:BoundField DataField="cantidad" HeaderText="Pre-Embarque"
                                DataFormatString="{0:n3}" />
                            <asp:BoundField DataField="pedido" HeaderText="Pedido"
                                DataFormatString="{0:n3}" Visible="False" />
                            <asp:BoundField DataField="existencia" HeaderText="Existencia"
                                DataFormatString="{0:n3}" />
                            <asp:BoundField DataField="cantidad_oc" HeaderText="Orden Compra"
                                DataFormatString="{0:n3}" NullDisplayText="&quot;&quot;" />
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="idc_preemb" ForeColor="#333333" Width="100%"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
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
                </td>
            </tr>
        </table>

    </div>
    <div align="center">
                <asp:Label ID="Label1" runat="server" Text="No Hay Pre-Embarques Pendientes" 
            Visible="False" Font-Bold="True" Font-Names="arial" Font-Size="Small" ForeColor="Blue"></asp:Label>
    </div>
    <asp:SqlDataSource ID="dsNoSearch2" runat="server" 
        SelectCommand="sp_pre_embarques_pendientes_modi_usuario" 
        SelectCommandType="StoredProcedure" 
        UpdateCommand="update update_grid set campo=1">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="pidc_sucursal" 
                SessionField="xidc_sucursal" />
            <asp:SessionParameter Name="pidc_usuario" SessionField="xidc_usuario" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

