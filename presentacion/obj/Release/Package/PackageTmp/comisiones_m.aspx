<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="comisiones_m.aspx.cs" Inherits="presentacion.comisiones_m" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#myModalEditar').modal('hide');
            $("#myModal_Esp").modal("hide");
        }

        function ModalEditar(cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalEditar').modal('show');
            $('#ModalEditar_title').text(cContenido);
            $('#content_ModalEditar').text('');
        }

        function ModalComisiones_Esp(Titulo) {

            $('#myModal_Esp').modal('show');
            $('#Modal_Esp_title').text(Titulo);

            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal_Esp').modal('show');
            $('#Modal_Esp_title').text(cContenido);
            $('#content_ModalEditar').text('');
        }
        function fn_sweetAlert_close() {
            bono();
            sweetAlert.close();
        }

        function bono() {
            
            var venta = $("#txtventa").text();
            var margen = $("#txtmargen_r").text();
            var color = "";

            if (venta > 0 && margen > 0) {
                if (venta >= 1200000 && margen > 12) {
                    color ="verde";
                    $("#txtbono1").addClass(color);
                    $("#txtcaab1").addClass(color);
                    $("#txtventa1").addClass(color);
                    $("#txtcom1").addClass(color);

                }
                else {
                    color = (margen > 12) ? "verde" : "rojo";

                    $("#txtcaab1").addClass(color);
                    $("#txtventa1").addClass(color);
                    $("#txtbono1").addClass("rojo");
                    $("#txtcaab1").addClass("rojo");

                }


                if (venta >= 1200000 & margen > 15) {
                    color = "verde";
                    $("#txtbono2").addClass(color);
                    $("#txtcaab2").addClass(color);
                    $("#txtventa2").addClass(color);
                    $("#txtcom2").addClass(color);

                }
                else {
                    color = (margen > 15) ? "verde" : "rojo";
                    $("#txtcom2").addClass(color);                    
                    color = (venta > 1200000) ? "verde" : "rojo";
                    $("#txtventa2").addClass(color);
                    color = "rojo";
                    $("#txtbono2").addClass(color);
                    $("#txtcaab2").addClass(color);
                    
                }

            }
            else {
                color = "rojo";
                $("#txtbono1").addClass(color);
                $("#txtbono2").addClass(color);
                $("#txtcaab2").addClass(color);
                $("#txtcaab1").addClass(color);
                $("#txtventa2").addClass(color);
                $("#txtcom2").addClass(color);
                $("#txtventa1").addClass(color);
                $("#txtcom1").addClass(color);
           
            }
        }


        function fn_reading() {
            
            swal({
                text: '',
                title: 'Cargando datos...',
                timer: 8000,
                showConfirmButton: false,
                imageUrl: 'imagenes/horizontal-loader.gif'
            });
            //var tim = setTimeout(fn_sweetAlert_close(), 8000);
        }

        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });

        function AsignaProgress(value) {
            $('#pavance').width(value);
        }


    </script>
    <style>
        .Ocultar {
            /*
display: none;
            */
            visibility: collapse;
        }

        .rojo {
            color: red;
        }

        .verde {
            color: green;
        }


        .form-control {
            float: right;
            text-align: right;
            font-weight: bold;
            color: blue;
        }

            .form-control + .form-control-feedback {
                left: 0;
            }

        .input-group {
            display: block;
        }

        .btn_mes {
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class="page-header" style="text-align: center;">Comisiones</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlAgente" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnVer" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnMes" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnk_esp_activacion" EventName="Click" />

        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5><i class="fa fa-user-secret" aria-hidden="true"></i>&nbsp;Agente </h5>
                    <asp:DropDownList ID="ddlAgente" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:LinkButton ID="btnVer" CssClass="btn btn-success btn-block  btn_mes" runat="server" OnClick="btnVer_OnClick" OnClientClick="fn_reading();">
                        <span class="badge"><span class="glyphicon glyphicon-calendar"></span></span>&nbsp;
                        <asp:Label ID="lblVer" runat="server"></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:LinkButton ID="btnMes" CssClass="btn btn-info btn-block btn_mes" runat="server" OnClick="btnMes_OnClick" OnClientClick="fn_reading(); ">
                        <span class="badge"><span class="glyphicon glyphicon-calendar"></span></span>&nbsp;<asp:Label ID="lblMes" runat="server"></asp:Label>
                    </asp:LinkButton>
                </div>

            </div>

            <div>
                <div class="panel panel-info  fresh-color">
                    <div class="panel-heading">
                        <asp:Label ID="lblNumAgente" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="1em">
                            
                        </asp:Label>
                    </div>

                    <div class="panel-body">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <h5><i class="fa fa-shopping-cart" aria-hidden="true"></i>&nbsp;Venta Total</h5>
                            <div class="input-group">
                                <asp:TextBox ID="txtventa" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <h5><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Comision</h5>
                            <div class="input-group">
                                <asp:TextBox ID="txtmargen" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <strong class="form-control-feedback">%</strong>
                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <h5><i class="fa fa-share-square-o" aria-hidden="true">&nbsp;</i>Aportacion</h5>
                            <div class="input-group">
                                <asp:TextBox ID="txtaportacion" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <h5><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Anticipo de Sueldos y Gastos</h5>
                            <div class="input-group">
                                <asp:TextBox ID="txttotalgastos" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                            </div>
                        </div>
                        <!--  -->
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <h5><span class="glyphicon glyphicon-check"></span>&nbsp;
                                <asp:Label ID="lblcomision" runat="server">
                                    Comisión al Dia
                                </asp:Label>
                            </h5>
                            <div class="input-group">
                                <asp:TextBox ID="txtdiferencia" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                            </div>
                        </div>
                        <!-- -->
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <h5><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Comision en Equipo</h5>
                            <div class="input-group">
                                <asp:TextBox ID="txtcomequi" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <strong class="form-control-feedback">%</strong>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <!-- grafica -->
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="align-content: center">
                    <h5 runat="server" id="h1">
                        <i class="fa fa-bar-chart" aria-hidden="true"></i>&nbsp;
                        <asp:Label ID="lblperiodo" runat="server" ForeColor="Blue">Mes calculado</asp:Label>
                    </h5>

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
                </div>
            </div>

            <!-- link button detalles-->
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                    <asp:LinkButton ID="lkbDetalles" Visible="true" CssClass="btn btn-success btn-block" runat="server" OnClick="lkbDetalles_OnClick">
                        <i class="fa fa-indent" aria-hidden="true" ></i>&nbsp; Detalles
                    </asp:LinkButton>
                </div>
            </div>

            <hr />
            <div runat="server" id="Container_com">
                <div class="panel panel-info fresh-color">
                    <div class="panel-heading">
                        <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="0.8em">
                        Si Tu % de Comision en Equipo es Mayor a 1.3632% Recibiaras La Siguiente Comision Adicional:
                        </asp:Label>
                    </div>
                    <div class="panel-body">
                        <div visible="false" runat="server" id="divAportacionAdicional">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <h5><i class="fa fa-plus-square" aria-hidden="true"></i>&nbsp;<i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Aportacion Adicional</h5>
                                <div class="input-group">
                                    <asp:TextBox ID="TextBox1" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <h5><i class="fa fa-plus-square" aria-hidden="true"></i>&nbsp;<b>%</b>&nbsp;Comision Aportacion Adicional</h5>
                                <div class="input-group">
                                    <asp:TextBox ID="txtdif_apo" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                </div>
                            </div>
                        </div>

                        <div>&nbsp;</div>

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="input-group">
                                <asp:TextBox ID="txtapo" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <%-- div_bono1--%>
            <div runat="server" id="div_bono1">
                <div class="panel panel-info  fresh-color">
                    <div class="panel-heading">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="0.8em">
                            Si Tu Venta Total es Superior $1,200,000  y tu % de Comision en Equipo es Superior a 1.3632% Recibirás el Siguiente Bono, que se sumaria a Tu Comision Final:
                        </asp:Label>
                    </div>

                    <div class="panel-body">
                        <div runat="server" id="divBono1" visible="false">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <h5><i class="fa fa-shopping-cart" aria-hidden="true"></i>&nbsp;Venta</h5>
                                <div class="input-group">
                                    <asp:TextBox ID="txtventa1" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <h5><strong>%</strong>  &nbsp;Comision</h5>
                                <div class="input-group">
                                    <asp:TextBox ID="txtcom1" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <h5><strong>%</strong>  &nbsp;<i class="fa fa-money" aria-hidden="true"></i>&nbsp;
                                    <asp:Label runat="server" ID="lblcaab1">
                                    Comisión al Dia con Aportación Adicional y Bono 1
                                    </asp:Label>
                                </h5>
                                <div class="input-group">
                                    <asp:TextBox ID="txtcaab1" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <h5><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Importe Bono 1</h5>
                            <div class="input-group">
                                <asp:TextBox ID="txtbono1" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- div_bono2--%>
            <div runat="server" id="div_bono2">
                <div class="panel panel-info  fresh-color">
                    <div class="panel-heading">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="0.8em">
                            Si Tu Venta Total es Superior $1,200,000  y tu % de Comision en Equipo es Superior a 1.704% Recibirás el Siguiente Bono, que se sumaria a Tu Comision Final:
                        </asp:Label>
                    </div>

                    <div class="panel-body">
                        <div runat="server" id="divBono2" visible="false">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <h5><i class="fa fa-shopping-cart" aria-hidden="true"></i>&nbsp;Venta</h5>
                                <div class="input-group">
                                    <asp:TextBox ID="txtventa2" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <h5><strong>%</strong> &nbsp;Comision</h5>
                                <div class="input-group">
                                    <asp:TextBox ID="txtcom2" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <h5><strong>%</strong>  &nbsp;<i class="fa fa-money" aria-hidden="true"></i>&nbsp;
                                    <asp:Label runat="server" ID="lblcaab2">
                                        Comisión al Dia con Aportación Adicional y Bono (1,2)
                                    </asp:Label>
                                </h5>

                            </div>
                        </div>

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <h5><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Importe Bono 2</h5>
                            <div class="input-group">
                                <asp:TextBox ID="txtbono2" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div id="div2">
                <div class="panel panel-info  fresh-color">
                    <div class="panel-heading">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="0.8em">
                            Si Cumples Todos los Objetivos Anteriores tu Comision Final seria de:
                        </asp:Label>
                    </div>

                    <div class="panel-body">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="input-group">
                                <asp:TextBox ID="txtcaab2" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="row">

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="input-group">
                        <asp:TextBox ID="txtmargen_r" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                        <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                    </div>
                </div>
            </div>

            <div class="row">
                <div>
                    <h4 class="col-lg-12 col-md-12 col-sm-12 col-xs-12"><strong>%</strong> &nbsp;<i class="fa fa-check-square-o" aria-hidden="true"></i> &nbsp;Comisiones especiales</h4>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:LinkButton ID="lnk_esp_Articulos" Visible="true" CssClass="btn btn-primary btn-block" runat="server" OnClick="lnk_esp_articulo_OnClick">
                        <i class="fa fa-cubes" aria-hidden="true"></i>&nbsp; Articulos
                    </asp:LinkButton>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:LinkButton ID="lnk_esp_activacion" CssClass="btn btn-success btn-block" runat="server" OnClick="lnk_esp_activacion_OnClick">
                        <i class="fa fa-toggle-off" aria-hidden="true"></i>&nbsp; Activaciones
                    </asp:LinkButton>
                </div>
            </div>
            <div class="modal fade modal-info" id="myModal_Esp" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg ">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header" style="text-align: center;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 id="Modal_Esp_title"><strong>comisiones esp</strong></h4>
                                </div>

                                <div class="modal-body">

                                    <div class="row" runat="server" id="div_ddl_Semana">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                            <div>
                                                <h5><i class="fa fa-university" aria-hidden="true"></i>&nbsp;Semana</h5>
                                                <asp:DropDownList ID="ddlSemanaComisiones" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                            <h3 id="VacioGrid_modal" runat="server" visible="false" style="text-align: center;">
                                                <asp:Label runat="server" ID="lbl_VacioGrid"> NO HAY DATOS </asp:Label><i class="fa fa-exclamation-triangle"></i></h3>
                                            <div class="table-responsive">
                                                <asp:GridView ID="grid_comision_esp_activaciones" CssClass="table table-responsive table-condensed gvv " AutoGenerateColumns="false" runat="server">
                                                    <RowStyle CssClass="" HorizontalAlign="Right" Font-Size="10px" Font-Names="arial" />
                                                    <HeaderStyle CssClass="HeaderStyle" Font-Size="12px" Font-Names="arial" />
                                                    <Columns>
                                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                                        <asp:BoundField DataField="cliente" HeaderText="Cliente" />
                                                        <asp:BoundField DataField="codfac" HeaderText="CodFac" />
                                                        <asp:BoundField DataField="monto" HeaderText="Monto" />
                                                        <asp:BoundField DataField="aportacion" HeaderText="Aportacion" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="row" runat="server" id="div_lbl_total">
                                        <div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                <h5><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;</i><asp:Label runat="server" ID="lblMonto_m"></asp:Label></h5>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtmonto_m" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                                </div>
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                <h5><i class="fa fa-share-square-o" aria-hidden="true">&nbsp;</i>Aportacion</h5>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtaportacion_m" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                                </div>
                                            </div>

                                            <!-- -->
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer">

                                    <div runat="server" id="Div1">
                                        <input id="btn_Cancelar" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <!-- ---------------------------------------------------------------------- -->
            <input type="hidden" runat="server" name="txtventa_c_h" id="txtventa_c_h" />
            <input type="hidden" runat="server" name="txtcomision_c_h" id="txtcomision_c_h" />
            <input type="hidden" runat="server" name="txtaportacion_c_h" id="txtaportacion_c_h" />
            <input type="hidden" runat="server" name="txtventa_d_h" id="txtventa_d_h" />
            <input type="hidden" runat="server" name="txtcomision_d_h" id="txtcomision_d_h" />
            <input type="hidden" runat="server" name="txtaportacion_d_h" id="txtaportacion_d_h" />
            <input type="hidden" runat="server" name="h_mes" id="h_mes" />
            <input type="hidden" runat="server" id="h_axion_esp" value="" />
            <input type="hidden" runat="server" id="h_aportacion" value="" />

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
