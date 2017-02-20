 <%@ Page Language="VB" MasterPageFile="~/Global.Master" AutoEventWireup="false" CodeFile="comisiones_m.aspx.vb" Inherits="comisiones_m" title="" %>

<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/jquery.mobile-1.2.0.min.css" rel="stylesheet" /> 
    <link href="js/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
    <link href="TinyBox/style.css" rel="stylesheet" />
    <script src="TinyBox/tinybox.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" Runat="Server">
    <script type="text/javascript" >
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
        }

        var myvar_guardando;
        function mostrar_procesar_guard()
        {
            myvar_guardando = setTimeout(function(){document.getElementById('div_act').style.display =''}, 0);
        }
        
        function myStopFunction_guard()
        {
            clearTimeout(myvar_guardando);
            document.getElementById('div_act').style.display ='none';
        }
        function ver_info()
        {
            var cboagentes = document.getElementById('<%=cboagente.ClientID%>');
            if(cboagentes!=null)
            {
                if(cboagentes.options.length>0)
                {
                    mostrar_procesar_guard();
                }
            }
            else
            {
                alert('Es Necesario Seleccionar un Agente.');
                return false;
            }
        }
    </script>
    <style type="text/css" >
            hr{padding:0px;border-color:Navy;opacity:.7;height:5px;background-color:Navy;}
            .green{background-image:url(Iconos/ico-valid.png) !important;background-position:0% 50%;background-repeat:no-repeat;color:Green;border:solid 2px green;}
            .red{background-image:url(Iconos/ico-invalid.png) !important;background-position:0% 50%;background-repeat:no-repeat;color:Red;border:solid 2px red;}
            .b_red{border:solid 2px red}
            .b_green{border:solid 2px green}
            .f_red{color:Red;}            
            .f_green{color:Green}
            #ctl00_Contenido_div_bono2,#ctl00_Contenido_div_bono1,#ctl00_Contenido_Container_com,#ctl00_Contenido_div2{border:solid .1em rgb(122, 122, 122);border-radius:3px;}
            input[type="text"]{font-weight:bold;text-align:right;}
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="position: relative; width: 100%; top: 0px; left: 0px; bottom: 0px;">
                <h3><strong>Comisiones</strong></h3>

                <table style="width: 100%;">
                    <tr>
                        <td>

                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Text="Agente">
                            </asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div id="Div1" class="styled-select" style="width: 100%;">
                                <asp:DropDownList ID="cboagente" runat="server" CssClass="form-control"
                                    Font-Bold="True" ForeColor="Black" Height="30px" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div style="width: 49.5%; float: left;">
                                <asp:Button ID="btnver" runat="server" Width="100%"
                                    CssClass="btn btn-primary" />
                            </div>
                            <div style="width: 49.5%; float: right;">

                                <asp:Button ID="btnmes" runat="server" Width="100%"
                                    CssClass="btn btn-primary" />

                            </div>

                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <span id="div_act" style="display: none; text-align: center;">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center">
                                            <img src="imagenes/loading.gif" alt="" id="Img3" align="middle" height="30px" width="30px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="middle" align="center" style="font-family: Arial; font-weight: bold; font-size: small; color: steelblue;">Cargando Información...
                                        </td>
                                    </tr>
                                </table>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">

                            <asp:Label ID="lblnumagente" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="Blue"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%;" align="center">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Text="Venta Total"></asp:Label>
                                    </td>
                                    <td style="width: 50%;" align="center">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" Text="% Comision"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;">
                                        <asp:TextBox ID="txtventa" runat="server" Style="text-align: right"
                                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                            ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:TextBox ID="txtmargen" runat="server" Style="text-align: right"
                                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                            ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;" align="center">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Small"
                                            Text="Aportacion"></asp:Label>
                                    </td>
                                    <td style="width: 50%;" align="center">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Small"
                                            Text="Anticipo de Sueldos y Gastos"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtaportacion" runat="server" Style="text-align: right"
                                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green"
                                            ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttotalgastos" runat="server" Style="text-align: right"
                                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                            ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"
                                            BackColor="White"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td align="center">
                                        <asp:Label ID="lblcomision" runat="server" Font-Bold="True" Font-Size="Small"
                                            Text="Comisión al Dia"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtdiferencia" runat="server" Style="text-align: right"
                                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                            ForeColor="White" Height="30px" onfocus="this.blur();" Width="100%"
                                            BackColor="White"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div style="float: left; width: 40%; height: 100%;">
                                <asp:Label ID="Label38" runat="server" Font-Bold="True" Font-Size="Small"
                                    Text="% Comision en Equipo:"></asp:Label>
                            </div>
                            <div style="float: right; width: 60%" align="left">
                                <asp:TextBox ID="txtcomequi" runat="server" Style="text-align: right"
                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                    ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"
                                    BackColor="White" Font-Bold="True"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">

                            <asp:Label ID="lblperiodo" runat="server" Font-Bold="True" Font-Size="Small"
                                ForeColor="Blue"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Chart ID="Chart1" runat="server" ImageType="Png" Style="width: 100%; height: 100%;"
                                Palette="Light" BackGradientStyle="DiagonalLeft"
                                BackSecondaryColor="WhiteSmoke" BorderColor="DarkKhaki" EnableViewState="true"
                                TextAntiAliasingQuality="Normal">
                                <Series>
                                    <asp:Series XValueType="Double" ChartType="Column" Name="Series1" Color="DodgerBlue" YValueType="Double">
                                        <Points>
                                            <%--<asp:DataPoint YValues="6" />
                                            <asp:DataPoint YValues="9" />--%>
                                        </Points>
                                    </asp:Series>
                                    <asp:Series XValueType="Double" ChartType="Column" LegendText="Ventas"
                                        Name="Ventas" Color="Blue" YValueType="Double" Font="Microsoft Sans Serif, 5pt">
                                        <Points>
                                            <%--<asp:DataPoint YValues="6" />
                                            <asp:DataPoint YValues="9" />--%>
                                        </Points>
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="Transparent"
                                        BorderDashStyle="Solid" BackColor="White">
                                        <Area3DStyle Enable3D="False" LightStyle="Realistic" />
                                        <AxisY Interval="10000" Enabled="False">
                                            <MajorGrid LineColor="Silver" LineDashStyle="Dash" />
                                            <MajorTickMark Enabled="False" />
                                            <MinorTickMark Enabled="True" />
                                        </AxisY>
                                        <AxisX IsMarginVisible="False" InterlacedColor="White" Enabled="True">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="False" TickMarkStyle="None" />
                                        </AxisX>
                                        <AxisX2>
                                            <MajorGrid Enabled="False" />
                                        </AxisX2>
                                        <AxisY2 Enabled="False">
                                            <MajorGrid Enabled="True" />
                                            <MajorTickMark Enabled="False" />
                                        </AxisY2>
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Titles>
                                    <asp:Title Font="Microsoft Sans Serif, 14.25pt" Text="Comisiones"></asp:Title>
                                </Titles>
                                <BorderSkin BackColor="Khaki" BackSecondaryColor="DarkGoldenrod" SkinStyle="Emboss"></BorderSkin>
                            </asp:Chart>

                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btndetalles" runat="server"
                                CssClass="btn btn-success btn-block"
                                Text="Detalles" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <hr />
                        </td>
                    </tr>
                    <tr runat="server">
                        <td align="center">
                            <div id="Container_com" runat="server">
                                <div style="width: 100%" align="center">
                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="0.7em" ForeColor="Blue"
                                        Text="Si Tu % de Comision en Equipo es Mayor a 1.3632% Recibiaras La Siguiente Comision Adicional:"></asp:Label>
                                </div>
                                <div style="width: 100%" align="center">
                                    <table style="width: 100%; display: none;">
                                        <tr>
                                            <td style="width: 50%;" align="center">
                                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="0.85em"
                                                    Text="Aportacion Adicional"></asp:Label>
                                            </td>
                                            <td style="width: 50%;" align="center">
                                                <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="0.85em"
                                                    Text="Comision Aportacion Adicional"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>

                                                <asp:TextBox ID="txtdif_apo" runat="server"
                                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                    ForeColor="Red" Height="30px" onfocus="this.blur();" Width="100%"
                                                    BackColor="White"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                    <asp:TextBox ID="txtapo" runat="server"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                        Height="30px" onfocus="this.blur();" Width="99%"
                                        BackColor="White" Font-Bold="True"></asp:TextBox>
                                </div>
                            </div>

                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div id="div_bono1" style="width: 100%;" runat="server">
                                <div style="width: 100%" align="center">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="0.7em" ForeColor="Blue"
                                        Text="Si Tu Venta Total es Superior $1,200,000  y tu % de Comision en Equipo es Superior a 1.3632% Recibirás el Siguiente Bono, que se sumaria a Tu Comision Final:"></asp:Label>
                                </div>
                                <div style="width: 100%">

                                    <table style="width: 100%; display: none;">
                                        <tr>
                                            <td style="width: 50%" align="center">

                                                <asp:Label ID="Label31" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                    Text="Venta"></asp:Label>

                                            </td>
                                            <td align="center" style="width: 50%">

                                                <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                    Text="% Comision"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%" align="center">

                                                <asp:TextBox ID="txtventa1" runat="server" Style="text-align: right"
                                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                    Height="30px" onfocus="this.blur();" Width="100%"
                                                    BackColor="White"></asp:TextBox>

                                            </td>
                                            <td align="center" style="width: 50%">

                                                <asp:TextBox ID="txtcom1" runat="server" Style="text-align: right"
                                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                    Height="30px" onfocus="this.blur();" Width="100%"
                                                    BackColor="White"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%" align="center"></td>
                                            <td align="center" style="width: 50%">

                                                <asp:Label ID="lblcaab1" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                    Text="Comisión al Dia con Aportación Adicional y Bono 1"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%" align="right"></td>
                                            <td>

                                                <asp:TextBox ID="txtcaab1" runat="server"
                                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                    Height="30px" onfocus="this.blur();" Width="100%"
                                                    BackColor="White"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 20%" align="right">
                                                <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                    Text="Importe Bono 1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="bono1" runat="server" CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" Height="30px" onfocus="this.blur();" Width="100%" BackColor="White">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="div_bono2" style="width: 100%;" runat="server">
                                <div style="width: 100%" align="center">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="0.7em" ForeColor="Blue"
                                        Text="Si Tu Venta Total es Superior $1,200,000  y tu % de Comision en Equipo es Superior a 1.704% Recibirás el Siguiente Bono, que se sumaria a Tu Comision Final:"></asp:Label>
                                </div>
                                <div style="width: 100%">

                                    <table style="width: 100%; display: none;">
                                        <tr>
                                            <td style="width: 50%" align="center">

                                                <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                    Text="Venta"></asp:Label>

                                            </td>
                                            <td align="center" style="width: 50%">

                                                <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                    Text="% Comision"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%" align="center">

                                                <asp:TextBox ID="txtventa2" runat="server" Style="text-align: right"
                                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                    Height="30px" onfocus="this.blur();" Width="100%"
                                                    BackColor="White"></asp:TextBox>

                                            </td>
                                            <td align="center" style="width: 50%">

                                                <asp:TextBox ID="txtcom2" runat="server" Style="text-align: right"
                                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                    Height="30px" onfocus="this.blur();" Width="100%"
                                                    BackColor="White"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%" align="center">&nbsp;</td>
                                            <td align="center" style="width: 50%">

                                                <asp:Label ID="lblcaab2" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                    Text="Comisión al Dia con Aportación Adicional y Bonos(1,2)"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%" align="right">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 20%;" align="right">

                                                <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                    Text="Importe Bono 2"></asp:Label>

                                            </td>
                                            <td style="width: 80%">

                                                <asp:TextBox ID="bono2" runat="server"
                                                    CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                    Height="30px" onfocus="this.blur();" Width="100%"
                                                    BackColor="White"></asp:TextBox>
                                            </td>
                                        </tr>

                                    </table>

                                </div>
                            </div>

                        </td>
                    </tr>
                    <tr>
                        <td>

                            <div id="div2" style="width: 100%;" runat="server">
                                <div style="width: 100%" align="center">
                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial"
                                        Font-Size="0.7em" ForeColor="Blue"
                                        Text="Si Cumples Todos los Objetivos Anteriores tu Comision Final seria de:"></asp:Label>
                                </div>
                                <div style="width: 100%;" align="center">


                                    <asp:TextBox ID="txtcaab2" runat="server"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                        Height="30px" onfocus="this.blur();" Width="99%"
                                        BackColor="White"></asp:TextBox>


                                </div>
                            </div>

                        </td>
                    </tr>

                    <tr>
                        <td align="center" style="width: 100%">

                            <asp:DataGrid ID="grid1" runat="server" AutoGenerateColumns="False" CssClass="Ocultar"
                                BackColor="White" Width="100%">
                                <Columns>
                                    <asp:BoundColumn DataField="codfac" HeaderText="Documento">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Venta" HeaderText="Venta" DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="apo" HeaderText="Aportacion" DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="tipod" HeaderText="tipo">
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="idc_factura" HeaderText="fac">
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="tot_ven" HeaderText="tot_ven">
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="tot_apo" HeaderText="tot_apo" DataFormatString="{0:N2}">
                                        <ItemStyle CssClass="Ocultar" />
                                        <HeaderStyle CssClass="Ocultar" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Dir">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemStyle CssClass="sorttable_nosort" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkdirecta" runat="server" onclick="return false;" Checked='<%# DataBinder.Eval(Container, "DataItem.Directa") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="RowStyle" Font-Names="arial" Font-Size="0.8em" />
                            </asp:DataGrid>
                            <div id="div_detalles" class="Ocultar">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 5%">&nbsp;</td>
                                        <td style="width: 50%" align="center">
                                            <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Size="Small" Text="Venta"></asp:Label>
                                        </td>
                                        <td style="width: 50%" align="center">
                                            <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Size="Small" Text="% Comision"></asp:Label>
                                        </td>
                                        <td class="Ocultar">
                                            <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Size="Small" Text="Aportacion" CssClass="Ocultar"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">

                                            <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                Text="Compartida:"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtventa_c" runat="server" Style="text-align: right"
                                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcomision_c" runat="server" Style="text-align: right"
                                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="Ocultar">
                                            <asp:TextBox ID="txtaportacion_c" runat="server" Style="text-align: right"
                                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">

                                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="0.8em"
                                                Text="Directa:"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtventa_d" runat="server" Style="text-align: right"
                                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcomision_d" runat="server" Style="text-align: right"
                                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="Ocultar">
                                            <asp:TextBox ID="txtaportacion_d" runat="server" Style="text-align: right"
                                                CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                                ForeColor="Blue" Height="30px" onfocus="this.blur();" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>


                            </div>
                            <asp:GridView ID="gridv1" runat="server" AutoGenerateColumns="False" CssClass="Ocultar sortable" BackColor="White" Width="100%">
                                <HeaderStyle CssClass="HeaderS" />
                                <Columns>
                                    <asp:BoundField DataField="codfac" HeaderText="Documento" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Venta" HeaderText="Venta" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="apo" HeaderText="Aportacion" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="tipod" HeaderText="tipo" HeaderStyle-CssClass="Ocultar" ItemStyle-CssClass="Ocultar" />
                                    <asp:BoundField DataField="idc_factura" HeaderText="fac" HeaderStyle-CssClass="Ocultar" ItemStyle-CssClass="Ocultar" />
                                    <asp:BoundField DataField="tot_ven" HeaderText="tot_ven" HeaderStyle-CssClass="Ocultar" ItemStyle-CssClass="Ocultar" />
                                    <asp:BoundField DataField="tot_apo" HeaderText="tot_apo" HeaderStyle-CssClass="Ocultar" ItemStyle-CssClass="Ocultar" DataFormatString="{0:N2}" />
                                    <asp:TemplateField HeaderText="Dir">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkdirecta" Checked='<%# DataBinder.Eval(Container, "DataItem.Directa") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="pedscg" HeaderText="Pedscg" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtmargen_r" runat="server" CssClass="Ocultar"></asp:TextBox>
                        </td>
                    </tr>

                    <!-- add 14-10-2015 MIC -->
                    <tr>
                        <td>
                            <div style="width: 100%" align="center">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">Comisiones especiales</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btn_esparticulo" runat="server" Text="Articulos"  Width="99%"
                                                CssClass="btn btn-primary" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btn_espactivacion" runat="server" Text="Activaciones"  Width="99%"
                                                CssClass="btn btn-primary"/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Button ID="btncomisiones" runat="server" CssClass="btn btn-success btn-block" Text="Bono de Presupuesto"  data-toggle="modal" data-target="#myModal" />
                        </td>
                    </tr>
                    <!-- fin 14-10-2015 MIC -->
                </table>
            </div>
            <input type="hidden" runat="server" name="txtventa_c_h" id="txtventa_c_h" />
            <input type="hidden" runat="server" name="txtcomision_c_h" id="txtcomision_c_h" />
            <input type="hidden" runat="server" name="txtaportacion_c_h" id="txtaportacion_c_h" />
            <input type="hidden" runat="server" name="txtventa_d_h" id="txtventa_d_h" />
            <input type="hidden" runat="server" name="txtcomision_d_h" id="txtcomision_d_h" />
            <input type="hidden" runat="server" name="txtaportacion_d_h" id="txtaportacion_d_h" />
            <input type="hidden" runat="server" name="h_mes" id="h_mes" />
            <input type="hidden" runat="server" id="h_axion_esp" value="" />
            <input type="hidden" runat="server" id="h_aportacion" value="" />
             <!-- Modal -->
            <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 id="modal_title"><strong>Información del BONO DE PRESUPUESTO</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Agente</strong></h5>
                                      <asp:TextBox ID="txtnumagente" runat="server"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                        Height="30px" onfocus="this.blur();" Width="99%"
                                        BackColor="White"></asp:TextBox>

                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Presupuesto</strong></h5>
                                      <asp:TextBox ID="txtpresupuesto" runat="server"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                        Height="30px" onfocus="this.blur();" Width="99%"
                                        BackColor="White"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Venta</strong></h5>
                                      <asp:TextBox ID="txtventa_modal" runat="server"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                        Height="30px" onfocus="this.blur();" Width="99%"
                                        BackColor="White"></asp:TextBox>
                                </div>  |   
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Bono del Presupuesto</strong></h5>
                                      <asp:TextBox ID="txtbono_presupuesto" runat="server"
                                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
                                        Height="30px" onfocus="this.blur();" Width="99%"
                                        BackColor="White"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input id="No" class="btn btn-info" data-dismiss="modal" value="Cerrar" type="button" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

