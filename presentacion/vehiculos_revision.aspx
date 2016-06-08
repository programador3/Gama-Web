<%@ Page Title="Vehiculos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="vehiculos_revision.aspx.cs" Inherits="presentacion.vehiculos_revision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
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
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
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
                        <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Revisión de Vehiculos Pendientes</h1>
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
                                        <strong>Motivo: </strong>
                                        <asp:Label ID="lblmotivo" runat="server" Text=""></asp:Label>
                                    </h5>
                                </div>
                                <div class="form-group">
                                    <h5><strong>Fecha de Baja
                                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                        : </strong>
                                        <asp:Label ID="lblfechasoli" runat="server" Text=""></asp:Label>
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
                <div class="col-lg-6">

                    <ul class="pagination">
                        <li>
                            <asp:LinkButton ID="lnkDetalles" runat="server" CausesValidation="false" class="btn btn-primary active" OnClick="lnkDetalles_Click">Listado de Vehiculos <i class="fa fa-list-alt"></i></asp:LinkButton>
                        </li>
                        <li>
                            <%-- <a id="lnkDescargarFormato" visible="false" runat="server" class="btn btn-link active" href="" target="_blank">Descargar Formato de Revisión <i class="fa fa-download"></i></a>
                            --%>
                            <asp:LinkButton ID="lnkDescargarFormato" CausesValidation="false" Visible="false" runat="server" class="btn btn-link active" OnClick="lnkDescargarFormato_Click">Descargar Formato de Revisión <i class="fa fa-download"></i></asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div class="col-lg-6">
                    <h3 style="text-align: left;">
                        <asp:Label ID="lblSelectedCel" runat="server" Text="Detalles de Vehiculos <i class='fa fa-list-alt'></i>"></asp:Label>
                    </h3>
                </div>
            </div>
            <%--   PANEL VEHICULOS--%>
            <asp:Panel ID="PanelVehiculos" runat="server">
                <asp:Repeater ID="RepeatVehiculos" runat="server" OnItemDataBound="RepeatVehiculos_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-lg-12" style="margin: 0 auto;">
                                <asp:Panel ID="Panel1" runat="server" CssClass="form-group">
                                    <div class="row">
                                        <div class="col-lg-4" style="align-content: center;">
                                            <a>
                                                <asp:Image ID="imgVehiculos" runat="server" class="img-responsive" alt="Gama" Style="width: 300px; margin: 0 auto;" />
                                            </a>
                                        </div>
                                        <div class="col-lg-8">

                                            <asp:Panel ID="PanelTemp" runat="server">
                                                <div class="form-group">
                                                    <h4 style="text-align: center;"><strong>Detalles del Vehiculo</strong></h4>
                                                </div>
                                                <div class="form-group">
                                                    <h4><strong><i class="fa fa-user fa-fw"></i>Puesto Asignado: </strong>
                                                        <asp:Label ID="lblPuestoTEMP" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                    </h4>
                                                </div>
                                                <div class="form-group">
                                                    <h4><strong><i class="fa fa-list-ol fa-fw"></i>Numero Economico: </strong>
                                                        <asp:Label ID="lblNumeroEc" runat="server" Text='<%#Eval("num_economico") %>'></asp:Label>
                                                    </h4>
                                                </div>
                                                <div class="form-group">
                                                    <h4><strong><i class="fa fa-car fa-fw"></i>Descripción del Vehiculo: </strong>
                                                        <asp:Label ID="lblDescripcionVehoculo" runat="server" Text='<%#Eval("descripcion_vehiculo") %>'></asp:Label>
                                                    </h4>
                                                </div>
                                                <div class="form-group">
                                                    <h4><strong><i class="fa fa-lastfm-square fa-fw"></i>Placas: </strong>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("placas") %>'></asp:Label>
                                                    </h4>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="lnkRevision" runat="server" class="btn btn-primary btn-block" OnClick="lnkRevision_Click">Comenzar Revisión <i class="fa fa-check-square-o"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="lnkVerHVehiculos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkVerHVehiculos_Click">Ver Herramientas <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-lg-2">
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdatePanelGlobal" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="PanelRevisaVehiculos" runat="server" class="form-group" Visible="false" Style="color: black;">
                        <asp:Label ID="lblidc_vehiculo" runat="server" Text="Label" Visible="false"></asp:Label>
                        <div class="panel panel-primary">
                            <div class="panel-heading" style="text-align: center">
                                <h3 class="panel-title">Listado de Vehiculos <i class="fa fa-list"></i></h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <asp:Repeater ID="Repeat_Revision" runat="server">
                                        <ItemTemplate>
                                            <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12">
                                                <h4><strong>El vehiculo : </strong>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("descripcion_vehiculo") %>'></asp:Label>
                                                    &nbsp;<strong> con Placas: </strong>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("placas") %>'></asp:Label>
                                                    &nbsp;<strong> con Numero Economico: </strong>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("num_economico") %>'></asp:Label>
                                                    <strong>¿Se encuentra en buenas condiciones generales?</strong>
                                                </h4>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12" style="color: black;">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                <i class="fa fa-check-square-o"></i></span>
                                            <asp:CheckBox ID="cbxBuenasCondiciones" runat="server" class="input-sm" Text="Si (Desmarcada en caso contrario)" TextAlign="Right" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h4><strong>Cuenta con algun daño en :</strong></h4>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbxPintura" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="cbxLlantas" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="cbxAccesorios" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="cbxCarroceria" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="cbxInterior" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="cbxVidrios" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="cbxFocos" EventName="CheckedChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #337ab7;"><i class="fa fa-car"></i></span>
                                                        <asp:TextBox ID="txtPintura" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text="Pintura"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                            <i class="fa fa-check-square-o"></i></span>
                                                        <asp:CheckBox ID="cbxPintura" runat="server" class="input-sm" Text="Presenta Daño" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbxPintura_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                        <asp:TextBox ID="txtCostoPintura" ReadOnly="true" runat="server" class="form-control input-sm" Text="0.00" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-file-text"></i></span>
                                                        <asp:TextBox ID="txtObservacionesPintura" runat="server" class="form-control input-sm" placeholder="Comentarios"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #337ab7;"><i class="fa fa-car"></i></span>
                                                        <asp:TextBox ID="txtLantas" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text="Llantas"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                            <i class="fa fa-check-square-o"></i></span>
                                                        <asp:CheckBox ID="cbxLlantas" runat="server" class="input-sm" Text="Presenta Daño" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbxLlantas_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                        <asp:TextBox ID="txtCostollantas" ReadOnly="true" runat="server" class="form-control input-sm" Text="0.00" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-file-text"></i></span>
                                                        <asp:TextBox ID="txtComentariosllantas" runat="server" class="form-control input-sm" placeholder="Comentarios"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #337ab7;"><i class="fa fa-car"></i></span>
                                                        <asp:TextBox ID="txtAccesorios" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text="Accesorios"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                            <i class="fa fa-check-square-o"></i></span>
                                                        <asp:CheckBox ID="cbxAccesorios" runat="server" class="input-sm" Text="Presenta Daño" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbxAccesorios_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                        <asp:TextBox ID="txtCostoAccesorios" ReadOnly="true" runat="server" class="form-control input-sm" Text="0.00" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-file-text"></i></span>
                                                        <asp:TextBox ID="txtDescripcionAccesorios" runat="server" class="form-control input-sm" placeholder="Comentarios" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #337ab7;"><i class="fa fa-car"></i></span>
                                                        <asp:TextBox ID="txtCarroceria" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text="Carroceria"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                            <i class="fa fa-check-square-o"></i></span>
                                                        <asp:CheckBox ID="cbxCarroceria" runat="server" class="input-sm" Text="Presenta Daño" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbxCarroceria_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                        <asp:TextBox ID="txtCostoCarroceria" ReadOnly="true" runat="server" class="form-control input-sm" Text="0.00" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-file-text"></i></span>
                                                        <asp:TextBox ID="txtObservacionesCarroceria" runat="server" class="form-control input-sm" placeholder="Comentarios"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #337ab7;"><i class="fa fa-car"></i></span>
                                                        <asp:TextBox ID="txtInterior" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text="Interior"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                            <i class="fa fa-check-square-o"></i></span>
                                                        <asp:CheckBox ID="cbxInterior" runat="server" class="input-sm" Text="Presenta Daño" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbxInterior_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                        <asp:TextBox ID="txtCostoInterior" ReadOnly="true" runat="server" class="form-control input-sm" Text="0.00" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-file-text"></i></span>
                                                        <asp:TextBox ID="txtObservacionesInterior" runat="server" class="form-control input-sm" placeholder="Comentarios"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #337ab7;"><i class="fa fa-car"></i></span>
                                                        <asp:TextBox ID="txtVidrios" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text="Vidrios"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                            <i class="fa fa-check-square-o"></i></span>
                                                        <asp:CheckBox ID="cbxVidrios" runat="server" class="input-sm" Text="Presenta Daño" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbxVidrios_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                        <asp:TextBox ID="txtCostoVidrios" ReadOnly="true" runat="server" class="form-control input-sm" Text="0.00" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-file-text"></i></span>
                                                        <asp:TextBox ID="txtObservacionesVidrios" runat="server" class="form-control input-sm" placeholder="Comentarios"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #337ab7;"><i class="fa fa-car"></i></span>
                                                        <asp:TextBox ID="txtFocos" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text="Focos"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                            <i class="fa fa-check-square-o"></i></span>
                                                        <asp:CheckBox ID="cbxFocos" runat="server" class="input-sm" Text="Presenta Daño" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbxFocos_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                        <asp:TextBox ID="txtCostoFocos" ReadOnly="true" runat="server" class="form-control input-sm" Text="0.00" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-file-text"></i></span>
                                                        <asp:TextBox ID="txtObservacionesFocos" runat="server" class="form-control input-sm" placeholder="Comentarios"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <%--Herramientas--%>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelRevision_herr" class="panel panel-primary" runat="server" Visible="false">

                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Herramientas de Vehiculos <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row" style="text-align: center;">
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <h4><strong>Herramienta </strong></h4>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <h4><strong>Cantidad Registrada</strong></h4>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <h4><strong>Cantidad Entregada</strong></h4>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <h4><strong>Costo Aproximado</strong></h4>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="text-align: center;">
                                <h3 runat="server" id="NonHerramientas" visible="false">Este Vehiculo no cuenta con Herramientas asigandas</h3>
                            </div>
                            <asp:Repeater ID="repeat_herramientas" runat="server">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtCantidadReal" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtCostoHerramienta" EventName="TextChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblvehiculo" runat="server" Text='<%#Eval("idc_vehiculo") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbltipoherramientas" runat="server" Text='<%#Eval("idc_tipoherramienta") %>' Visible="false"></asp:Label>
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                    <i class="fa fa-wrench"></i></span>
                                                                <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                    <i class="fa fa-database"></i></span>
                                                                <asp:TextBox ID="txtCantidadSistema" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("cantidad") %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <strong>
                                                                <asp:Label ID="lblerrorCantidadReal" runat="server" Text="NO PUEDE TENER UN VALOR NEGATIVO." Style="color: red; font: bold;" Visible="false"></asp:Label></strong>

                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                    <i class="fa fa-eye"></i></span>
                                                                <asp:TextBox ID="txtCantidadReal" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("cantidad") %>' TextMode="Number" OnTextChanged="txtCantidadReal_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <div class="form-group">
                                                            <strong>
                                                                <asp:Label ID="lblerrorcostoherr" runat="server" Text="NO PUEDE TENER UN VALOR NEGATIVO." Style="color: red; font: bold;" Visible="false"></asp:Label></strong>

                                                            <div class="input-group">
                                                                <asp:Label ID="lblcosto_herr" runat="server" Text='<%#Eval("costo") %>' Visible="false"></asp:Label>

                                                                <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                                <asp:TextBox ID="txtCostoHerramienta" runat="server" class="form-control input-sm" Text="0.00" AutoPostBack="true" OnTextChanged="txtMoney_TextChanged" TextMode="Number"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="row">
                            <div class="col-lg-9"></div>
                            <div class="col-lg-3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                    <asp:LinkButton ID="lnkVerTotal" runat="server" Style="color: white;" OnClick="lnkVerTotal_Click">Generar Total <i class="fa fa-usd"></i></asp:LinkButton></span>
                                                <asp:TextBox ID="txtTotal" runat="server" class="form-control input-sm" Text="0.00" AutoPostBack="true" ReadOnly="true" TextMode="Number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel ID="PanelPiedePagina" runat="server" Visible="false">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form-group">
                            <asp:TextBox ID="txtComentarios" placeholder="Comentarios" TextMode="MultiLine" Rows="3" Style="resize: none;" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <asp:Label ID="lblerrorupload" runat="server" Text="Debe subir un archivo de extencion JPG" Style="color: red; font: bold;" Visible="false"></asp:Label>

                        <div class="form-group">
                            <h5><strong>
                                <asp:Label ID="lblFileUP" runat="server" Text="">Adjuntar una imagen JPG del formato de revisión <i class="fa fa-upload"></i></asp:Label>
                            </strong></h5>
                            <asp:FileUpload ID="fileupload" runat="server" class="form-control" />
                            <h5><strong>
                                <asp:RequiredFieldValidator ID="RFV" runat="server" ControlToValidate="fileupload" ErrorMessage="Debe seleccionar una imagen" Style="color: red;"></asp:RequiredFieldValidator>
                            </strong></h5>
                            <h5><strong>
                                <asp:RegularExpressionValidator ID="REV" runat="server"
                                    ErrorMessage="Tipo de archivo no permitido" ControlToValidate="fileupload"
                                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.jpeg|.JPEG)$">
                                </asp:RegularExpressionValidator></strong></h5>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btnGuardar" Visible="false" runat="server" Text="Guardar" class="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btnCancelar" CausesValidation="false" Visible="false" runat="server" Text="Cancelar" class="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row">
                <div class="col-lg-12" style="margin: 0 auto;">
                    <asp:Panel ID="PanelHerramientasVehiculo" class="panel panel-green form-group" runat="server" Visible="false">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Herramientas de Vehiculo <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="PanelsinHVehiculo" runat="server" Visible="false">
                                <h2 style="text-align: center;">Este Vehiculo no tiene Herramientas <i class="fa fa-exclamation-triangle"></i></h2>
                            </asp:Panel>
                            <asp:Panel ID="PanelconHVehiculo" runat="server">
                                <div class="table table-responsive form-group">
                                    <asp:GridView ID="gridHerramientasVehiculo" runat="server" AutoGenerateColumns="false" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [3]}">
                                        <Columns>
                                            <asp:BoundField DataField="idc_gpo_herramientasd" HeaderText="idc_gpo_herramientasd" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="idc_tipoherramienta" HeaderText="idc_tipoherramienta" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad"></asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Herramienta"></asp:BoundField>
                                            <asp:BoundField DataField="activo" HeaderText="activo" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="tipo" HeaderText="tipo" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="idc_vehiculo" HeaderText="idc_vehiculo" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="costo" HeaderText="Costo"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
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