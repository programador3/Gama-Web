<%@ Page Title="Celulares" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="celulares_revision.aspx.cs" Inherits="presentacion.celulares_revision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GoSection(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 500);
        }
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
        function getImage(path) {
            $("#myImage").attr("src", path);
        }

        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalPreview() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Revisión de Celulares Pendientes</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8" style="margin: 0 auto;">
                    <h5><strong>Datos del Empleado en Proceso de Pre-Baja</strong></h5>
                    <asp:Panel ID="Panel" runat="server">
                        <div class="row">
                            <div class="col-lg-2" style="align-content: center;">
                                <a>
                                    <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 100px; margin: 0 auto;" />
                                </a>
                            </div>
                            <div class="col-lg-10">
                                <div class="form-group">

                                    <h5><strong>Puesto: </strong>
                                        <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                                        &nbsp;&nbsp;<strong>Numero de Nomina:</strong>
                                        <asp:Label ID="lblnomina" runat="server" Text=""></asp:Label>
                                    </h5>
                                </div>
                                <div class="form-group">
                                    <h5><strong>Empleado Actual: </strong>
                                        <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></h5>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <strong>Sucursal: </strong>
                                        <asp:Label ID="lblsucursal" runat="server" Text=""></asp:Label>
                                    </h5>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <strong>Motivo: </strong>
                                        <asp:Label ID="lblmotivo" runat="server" Text=""></asp:Label>
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
                <div class="col-lg-4">
                    <div class="btn-group">

                        <asp:LinkButton ID="lnkDetalles" runat="server" class="btn btn-primary active" OnClick="lnkDetalles_Click">Detalles <i class="fa fa-list-alt"></i></asp:LinkButton>

                        <asp:LinkButton ID="lnkRevision" runat="server" class="btn btn-link" OnClick="lnkRevision_Click">Revisión <i class="fa fa-check-square-o"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="col-lg-8">
                    <h2 style="text-align: left;">
                        <asp:Label ID="lblSelectedCel" runat="server" Text="Detalles de Celulares y Lineas <i class='fa fa-list-alt'></i>"></asp:Label>
                    </h2>
                </div>
            </div>
            <asp:Panel ID="PanelDetallesCelular" runat="server">
                <asp:Repeater ID="repeatCelulares" runat="server" OnItemDataBound="repeatCelulares_ItemDataBound">
                    <ItemTemplate>
                        <div class="row" style="border:1px solid gray; background-color:white;" >
                            <div class="col-lg-12" style="margin: 0 auto;">
                                <asp:Panel ID="PanelT1" runat="server">
                                    <div class="row" >
                                        <div class="col-lg-2" style="align-content: center;">
                                             <asp:Image ID="imgCel" runat="server" class="img-responsive" alt="Gama" Style="max-width:150px; margin: 0 auto;" />
                                        </div>
                                        <div class="col-lg-5">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <h4 style="text-align: center;"><strong>Detalles del Celular o Linea</strong></h4>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <h5><i class="fa fa-phone-square fa-fw"></i><strong>Linea: </strong>
                                                    <asp:Label ID="linea" runat="server" Text='<%#Eval("telefono") %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="form-group">
                                                <h5><i class="fa fa-phone fa-fw"></i><strong>Marcación Corta:</strong>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("mar_corta") %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="form-group">
                                                <h5><strong><i class="fa fa-mobile fa-fw"></i>Descripción del Equipo:</strong>
                                                    <asp:Label ID="descri" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="form-group">
                                                <h5><strong><i class="fa fa-usd fa-fw"></i>Costo del Equipo:  $</strong>
                                                    <asp:Label ID="costo" runat="server" Text='<%#Eval("costo") %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="form-group">
                                                <h5><strong>IMEI: </strong>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("imei") %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="form-group">
                                                <h5><strong>Fecha de Adquisición: </strong>
                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("fecha_adquisicion") %>'></asp:Label>
                                                </h5>
                                            </div>
                                        </div>

                                        <div class="col-lg-5">
                                            <div class="form-group">
                                                <h4 style="text-align: center;"><strong>Accesorios del equipo</strong>
                                                </h4>
                                            </div>
                                            <asp:Panel ID="PanelconAccesorios" runat="server">
                                                <asp:Repeater ID="repeatAccesorios" runat="server">
                                                    <ItemTemplate>
                                                        <div class="form-group">
                                                            <h5><strong><i class="fa fa-info-circle fa-fw"></i>Descripcion: </strong>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                                &nbsp;&nbsp;<strong> Precio:  $</strong>
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("costo") %>'></asp:Label>
                                                            </h5>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>
                                            <asp:Panel ID="PanelsinAccesorios" runat="server">
                                                <h3 style="text-align: center;">Esta linea no cuenta con Equipo Celular <i class="fa fa-exclamation-triangle"></i></h3>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="PanelRevisaCelulares" runat="server" CssClass="panel panel-info fresh-color" Visible="false">
                <div class="panel-heading" style="text-align: center">
                    <h3 class="panel-title">Listado de Celulares <i class="fa fa-list"></i></h3>
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="UpdatePanelGeneral" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-xs-12" style="text-align: center;">
                                    <h4><strong>Celular <i class="fa fa-wrench"></i></strong></h4>
                                </div>
                                <div class="col-lg-3 col-md-3 col-xs-12" style="text-align: center;">
                                    <h4><strong>Accesorio <i class="fa fa-wrench"></i></strong></h4>
                                </div>
                                <div class="col-lg-3 col-md-3 col-xs-12" style="text-align: center;">
                                    <h5><strong>Entrego </strong>
                                        <asp:LinkButton ID="lbkseltodo" OnClick="lbkseltodo_Click" runat="server" CssClass="btn btn-primary btn-xs">Seleccionar Todo  <i class="fa fa-check-square-o"></i></asp:LinkButton>

                                        <asp:LinkButton ID="lnkDes" OnClick="lbkdestodo_Click" runat="server" CssClass="btn btn-primary btn-xs" Visible="false">Deseleccionar Todo  <i class="fa fa-check-square-o"></i></asp:LinkButton>
                                    </h5>
                                </div>
                                <div class="col-lg-3 col-md-3 col-xs-12" style="text-align: center;">
                                    <h4><strong>Costo Aproximado <i class="fa fa-usd"></i></strong></h4>
                                </div>
                            </div>
                            <asp:Repeater ID="repeatEquipoCelular" runat="server">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtMoney1" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cbx1" EventName="CheckedChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                                                        <asp:Label ID="lblcelular1" runat="server" Text='<%#Eval("idc_celular") %>' Visible="false"></asp:Label>
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                <i class="fa fa-mobile"></i></i></span>
                                                            <asp:TextBox ID="txt1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">

                                                        <asp:CheckBox ID="cbx1" runat="server" CssClass="radio3 radio-check radio-info radio-inline" Text="Entrego" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbx_CheckedChanged" />

                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">

                                                        <div class="input-group">
                                                            <asp:Label ID="lblcosto1" runat="server" Text='<%#Eval("costo") %>' Visible="false"></asp:Label>
                                                            <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                            <asp:TextBox ID="txtMoney1" runat="server" class="input-sm" Text='<%#Eval("costo") %>' AutoPostBack="true" OnTextChanged="txtMoney_TextChanged" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="repeat_accesorios" runat="server">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtMoney" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cbx" EventName="CheckedChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblcelular" runat="server" Text='<%#Eval("idc_celular") %>' Visible="false"></asp:Label>
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                    <asp:LinkButton ID="lnkDetallesHerrRevision" Style="color: #fff; background-color: #337ab7;" runat="server" Enabled="false"></asp:LinkButton>
                                                                    <i class="fa fa-mobile"></i>
                                                                </span>
                                                                <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("celular") %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblaccesorio" runat="server" Text='<%#Eval("idc_celulara") %>' Visible="false"></asp:Label>
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                    <i class="fa fa-mobile"></i>
                                                                </span>
                                                                <asp:TextBox ID="TextBox1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                                    <i class="fa fa-check-square-o"></i></span>
                                                                <asp:CheckBox ID="cbx" runat="server" class="input-sm" Text="Entrego" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbx_CheckedChangedAcce" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <asp:Label ID="lblcosto" runat="server" Text='<%#Eval("costo") %>' Visible="false"></asp:Label>
                                                                <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                                <asp:TextBox ID="txtMoney" runat="server" class="form-control input-sm " Text='<%#Eval("costo") %>' AutoPostBack="true" OnTextChanged="txtMoney_TextChanged" TextMode="Number"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="row">
                                <div class="col-lg-8">
                                </div>
                                <div class="col-lg-1">
                                    <h4><strong>Total</strong></h4>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #337ab7;"><i class="fa fa-usd"></i></span>
                                            <asp:TextBox ID="txtTotal" runat="server" class="form-control input-sm" Text="0.00" AutoPostBack="true" ReadOnly="true" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtComentarios" placeholder="Comentarios" TextMode="MultiLine" Rows="3" Style="resize: none;" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div id="modalPreviewView" class="modal fade bs-example-modal-md" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                            <h4>Detalles de Herramienta/Activo con Folio:
                          <asp:Label ID="lblMDetalles" runat="server" Text=""></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <h4 style="text-align: center;"><strong>
                                        <asp:Label ID="lblMSubcat" runat="server" Text=""></asp:Label></strong></h4>
                                    <asp:Repeater ID="gridHerramientasDetalles" runat="server">
                                        <ItemTemplate>
                                            <asp:Panel ID="Panel" runat="server">
                                                <h5><i class="fa fa-caret-right"></i><strong>
                                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label></strong>
                                                    &nbsp;<asp:Label ID="lblValor" runat="server" Text='<%#Eval("valor") %>'></asp:Label>
                                                    &nbsp;<asp:Label ID="lblObs" runat="server" Text='<%#Eval("observaciones") %>'></asp:Label>
                                                </h5>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-5">
                                </div>
                                <div class="col-lg-3">
                                    <input id="btnModalAcept" class="btn btn-primary btn-block" value="Aceptar" onclick="ModalClose();" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.CONFIRMA -->
                </div>
            </div>
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
                            <div class="row">
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>