<%@ Page Title="Administrador Preparaciones" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="administrador_preparaciones.aspx.cs" Inherits="presentacion.administrador_puestos_preparacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/PanelsLTE.css" rel="stylesheet" />
    <link href="css/ionicons.css" rel="stylesheet" />
    <link href="css/ionicons.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function ProgressBar(value, valuein) {
            $('#progressincorrect').css('width', valuein);
            $('#progrescorrect').css('width', value);
            $('#pavanze').css('width', value);
            $('#lblya').text(value);
            $('#lblno').text(valuein);
        }
        var downloadURL = function downloadURL(url) {
            var hiddenIFrameID = 'hiddenDownloader',
                iframe = document.getElementById(hiddenIFrameID);
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                iframe.style.display = 'none';
                document.body.appendChild(iframe);
            }
            iframe.src = url;
        };
        function GoSection(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 500);
        }
        function AlertGO(TextMess, URL, typealert) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: typealert,
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
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalPreBaja() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
            $('#myModal_graph').modal('hide');
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
        }
        function ModalGraph() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal_graph').modal('show');
        }
    </script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#container').highcharts({
                data: {
                    table: '<%= grid_proc.ClientID%>'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Procesos de Bajas y Pre Bajas del año.'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Procesos'
                    }
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br />' +
                            this.point.y + ' ' + this.point.name.toLowerCase();
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Administrador de Preparaciones
                           <input id="inp_gr" style="visibility: hidden;" value="Ver Grafica Global" class="btn btn-primary" onclick="ModalGraph();" />
                    </h1>
                </div>
            </div>
            <asp:Panel ID="PanelPrebajas" runat="server">
                <h2 id="Noempleados" runat="server" visible="false" style="text-align: center;">No hay Preparaciones Activas <i class="fa fa-exclamation-triangle"></i></h2>
                <asp:Repeater ID="repeatpendientes" runat="server" OnItemDataBound="repeatprebajas_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-6 col-sm-6">
                            <asp:Panel ID="PanelRevisionP" runat="server" class="small-box bg-red">
                                <div class="inner">
                                    <h4>Pendiente
                                    </h4>
                                    <p>
                                        <h6>
                                            <asp:Label ID="lbl" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label></h6>
                                    </p>
                                </div>
                                <div class="icon">
                                    <asp:LinkButton ID="lnkGOdET" Style="color: white;" runat="server" OnClick="lnkGO_Click"><i class="ion ion-arrow-right-c"></i></asp:LinkButton>
                                </div>
                                <asp:LinkButton ID="lnkGO" runat="server" CssClass="small-box-footer" OnClick="lnkGO_Click">Ver Detalles <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                            </asp:Panel>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="PanelDetalles" runat="server" Visible="false">
                <div class="row">
                    <div class="col-lg-10" style="margin: 0 auto;">
                        <h4><strong>Datos del Puesto </strong>
                            <asp:LinkButton ID="lnklista" runat="server" class="btn btn-primary" Visible="false" OnClick="lnklista_Click">Ver Lista de Preparaciones <i class="fa fa-list"></i></asp:LinkButton>
                        </h4>
                        <asp:Panel ID="Panel" runat="server">
                            <div class="row">
                                <div class="col-lg-2" style="align-content: center;">
                                    <a>
                                        <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" ImageUrl="imagenes/btn/default_employed.png" Style="width: 100px; margin: 0 auto;" />
                                    </a>
                                </div>
                                <div class="col-lg-10">
                                    <div class="form-group">
                                        <h5><strong>Puesto: </strong>
                                            <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                                        </h5>
                                    </div>
                                    <div class="form-group">
                                        <h5><strong>Fecha de Solicitud : </strong>
                                            <asp:Label ID="lblfecha" runat="server" Text=""></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-lg-2">
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="form-group">
                            <h4><i class="fa fa-spinner"></i>Progreso de la Preparacion actualizado a
                                <asp:Label ID="lblfechaactual" runat="server"></asp:Label></h4>
                            <div class="progress">
                                <div id="pavanze" class="progress-bar progress-bar-striped progress-bar-primary active" style="width: 35%">
                                    <asp:Label ID="lblporcentajepregress" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <h4><i class="fa fa-cog"></i>Metrica de Revisión</h4>
                            <div class="progress">
                                <div id="progrescorrect" class="progress-bar progress-bar-striped progress-bar-success active" style="width: 35%">
                                    <label id="lblya"></label>
                                    ya Revisaron
                                </div>
                                <div id="progressincorrect" class="progress-bar progress-bar-striped progress-bar-danger active" style="width: 20%">
                                    <label id="lblno"></label>
                                    no Revisaron
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gridprebajasdetalles" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="gridprebajasdetalles" EventName="RowDataBound" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="table table-responsive">
                                    <h4 style="text-align: center;"><strong>Revisiones para esta Pre-Baja <i class="fa fa-check-square-o"></i></strong></h4>

                                    <asp:GridView ID="gridprebajasdetalles" runat="server" CssClass="table table-bordered table-hover table-condensed" AutoGenerateColumns="False" DataKeyNames="idc_prepara,puesto,tipo_prep,empleado,descripcion, idc_puestobaja,idc_puesto,fecha_revision" OnRowCommand="gridprebajasdetalles_RowCommand" OnRowDataBound="gridprebajasdetalles_RowDataBound" Font-Size="Smaller">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/more.png" HeaderText="Ver" CommandName="Ver">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="idc_prepara" HeaderText="idc_prepara" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="Empleado" HeaderText="Empleado"></asp:BoundField>
                                            <asp:BoundField DataField="puesto" HeaderText="Puesto"></asp:BoundField>
                                            <asp:BoundField DataField="tipo_prep" HeaderText="Revisi&#243;n"></asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n o Detalles"></asp:BoundField>
                                            <asp:BoundField DataField="idc_puestobaja" HeaderText="idc_puestoprebaja" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="idc_puesto" HeaderText="idc_puesto" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="FECHA_REVISION_DT" HeaderText="Preparado desde"></asp:BoundField>
                                            <asp:BoundField DataField="tiempo" HeaderText="Tiempo"></asp:BoundField>
                                        </Columns>
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-6">
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <h4><strong>Tiempo transcurrido: </strong>
                            <asp:Label ID="lblfecha_proceso" runat="server" Text=""></asp:Label></h4>
                    </div>
                </div>
            </asp:Panel>
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="confirmContenido"></label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Yes" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-6 col-xs-6">
                                            <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                                        </div>
                                        <div class="col-lg-6 col-xs-6">
                                            <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div id="myModal_graph" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="text-align: center;">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4 style="text-align: center;"><strong class="modal-title">Procesos de Bajas y Pre Bajas de
                                <asp:Label ID="lblaño" runat="server" Text=""></asp:Label></strong></h4>
                        </div>
                        <div class="modal-body" style="text-align: center;">
                            <div class="row" style="text-align: center;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <div id="container" style="height: 400px; width: auto;"></div>
                                </div>
                            </div>
                            <div class="row" style="text-align: center;">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label>Buscar Datos de Otro Año</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtaño" runat="server" CssClass="form-control" placeholder="Año" TextMode="Number"></asp:TextBox>

                                            <span class="input-group-addon" style="background-color: #3c8dbc; color: white;">
                                                <asp:LinkButton ID="lnkVerContraseña" Style="background-color: #3c8dbc; color: white;" Text="Buscar" OnClick="lnkVerContraseña_Click" runat="server"></asp:LinkButton></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="grid_proc" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-bordered  table-condensed table-hover">
                                            <Columns>
                                                <asp:BoundField DataField="MES" HeaderText=""></asp:BoundField>
                                                <asp:BoundField DataField="terminados" HeaderText="Terminados"></asp:BoundField>
                                                <asp:BoundField DataField="no_terminados" HeaderText="No Terminados o Cancelados"></asp:BoundField>
                                            </Columns>
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Yes" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <input id="Nop" class="btn btn-danger btn-block" type="button" onclick="ModalClose();" value="Cerrar" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>