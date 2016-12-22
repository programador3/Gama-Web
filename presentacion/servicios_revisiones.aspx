<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="servicios_revisiones.aspx.cs" Inherits="presentacion.servicios_revisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/highlight.css" rel="stylesheet" />
    <link href="css/bootstrap-switch.css" rel="stylesheet" />
    <link href="http://getbootstrap.com/assets/css/docs.min.css" rel="stylesheet" />

    <script src="js/bootbox.min.js"></script>
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
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
        }
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
                        Revisión</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="margin: 0 auto;">
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
                                    <h5>
                                        <strong>Sucursal: </strong>
                                        <asp:Label ID="lblsucursal" runat="server" Text=""></asp:Label>
                                    </h5>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="form-group">
                        <h3><strong>
                            <asp:Label ID="lblNombreEncargado" runat="server" Text="Hola"></asp:Label></strong>
                        </h3>
                        <h4>tiene que revisar la siguiente lista de pendientes.
                        </h4>
                    </div>
                </div>
            </div>
            <asp:Repeater ID="RepeaterServicios" runat="server" OnItemDataBound="RepeaterServicios_ItemDataBound">
                <ItemTemplate>
                    <asp:UpdatePanel ID="UpdatePanelre" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtCosto" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="txtObservaciones" EventName="TextChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-info fresh-color">
                                        <div class="panel-heading" style="text-align: center;">
                                            <%#Eval("desc_servicio") %> <i class="fa fa-laptop"></i>
                                        </div>
                                        <div class="panel-body">
                                            <asp:TextBox ID="idc_reviser" runat="server" Visible="false" Text='<%#Eval("idc_revisionser") %>'></asp:TextBox>
                                            <asp:Label ID="idc_revisionser" runat="server" Text='<%#Eval("idc_revisionser") %>' Visible="false"></asp:Label>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <h4>Tipo de Revision: <strong><%#Eval("desc_servicio") %></strong></h4>
                                                        <asp:TextBox ID="txtDescSer" Visible="false" runat="server" Text='<%#Eval("desc_servicio") %>' CssClass="form-control" Style="resize: none;" ReadOnly="true" PlaceholdeR="Observaciones"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12">
                                                    <div class="form-group">
                                                        <h4>
                                                            <asp:Label ID="lbltpodetalle" runat="server" Text="Tipo" Visible="false"></asp:Label></h4>
                                                        <asp:TextBox ID="txtDetalles" Visible="false" runat="server" TextMode="MultiLine" Rows="5" Text="" CssClass="form-control" Style="resize: none;" ReadOnly="true" PlaceholdeR="Observaciones"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="form-group">
                                                        <strong>
                                                            <asp:Label ID="lblerrorobsr" runat="server" Text="DEBE COLOCAR UNA DESCRIPCION." Style="color: red; font: bold;" Visible="false"></asp:Label></strong>

                                                        <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" Style="resize: none;" MaxLength="250" placeholder="Observaciones" OnTextChanged="txtObservaciones_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="form-group">
                                                        <asp:Panel ID="panelcosto" runat="server" Visible="false">
                                                            <strong>
                                                                <asp:Label ID="lblerrorCantidadReal" runat="server" Text="NO PUEDE TENER UN VALOR NEGATIVO." Style="color: red; font: bold;" Visible="false"></asp:Label></strong>
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                                <asp:TextBox ID="txtCosto" runat="server" Text="0.00" CssClass="form-control" Style="resize: none;" TextMode="Number" OnTextChanged="txtCosto_TextChanged"></asp:TextBox>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ItemTemplate>
            </asp:Repeater>
            <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btnAceptar" runat="server" Text="Revisar" CssClass="btn btn-info btn-block" OnClick="btnAceptar_Click" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
    <script src="js/bootstrap-switch-original.js"></script>
</asp:Content>