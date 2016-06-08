<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="revision_final.aspx.cs" Inherits="presentacion.revision_final" %>

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
            $('#myModal').modal('hide');
            location.reload();
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });

        function LimpiarTexto(txt) {
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
                        Revisión Final de Pre-Baja</h1>
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
                <div class="col-lg-12">
                    <div class="form-group">
                        <h4>Tiene que revisar la siguientes pendientes de la Pre-Baja.
                        </h4>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            Servicios <i class="fa fa-laptop"></i>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="idc_revisionser" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:UpdatePanel ID="UpdatePanelCheck" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbxRevision" EventName="CheckedChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <h4>
                                                <asp:CheckBox ID="cbxRevision" runat="server" AutoPostBack="true" Text="" Checked="true" Visible="false" />
                                            </h4>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h4><strong>Observaciones Generales:
                                            </strong></h4>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtObservaciones" CssClass="form-control" placeholder="Observaciones" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h4><strong>Generar Vale:
                                            </strong></h4>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8">
                                            <div class="form-group">
                                                <strong>
                                                    <asp:Label ID="lblerrorobsr" runat="server" Text="DEBE COLOCAR UNA DESCRIPCION." Style="color: red; font: bold;" Visible="false"></asp:Label></strong>

                                                <asp:TextBox ID="txtObsrv" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" Style="resize: none;" MaxLength="250" placeholder="Concepto" OnTextChanged="txtObsrv_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4">
                                            <div class="form-group">
                                                <asp:Panel ID="panelcosto" runat="server" Visible="true">
                                                    <strong>
                                                        <asp:Label ID="lblerrorCantidadReal" runat="server" Text="NO PUEDE TENER UN VALOR NEGATIVO." Style="color: red; font: bold;" Visible="false"></asp:Label></strong>
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                        <asp:TextBox ID="txtCosto" onkeypress="return validarMontoMoney(event);" runat="server" placeholder="0.00" CssClass="form-control" Style="resize: none;"></asp:TextBox>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gridVales" runat="server" CssClass="gvv table table-responsive table-bordered table-condensed table-hover" AutoGenerateColumns="False" OnRowDataBound="gridVales_RowDataBound" DataKeyNames="activo_nomina,bono">
                                            <Columns>
                                                <asp:BoundField DataField="idc_empleado" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="idc_vale" HeaderText="No. Vale"></asp:BoundField>
                                                <asp:BoundField DataField="fecha" HeaderText="Fecha"></asp:BoundField>
                                                <asp:BoundField DataField="concepto" HeaderText="Concepto"></asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="Importe" SortExpression="UnitPrice" DataFormatString="{0:c}"></asp:BoundField>
                                                <asp:BoundField DataField="pagado" HeaderText="Pagado" SortExpression="UnitPrice" DataFormatString="{0:c}"></asp:BoundField>
                                                <asp:BoundField DataField="saldo" HeaderText="Saldo" SortExpression="UnitPrice" DataFormatString="{0:c}"></asp:BoundField>
                                                <asp:BoundField DataField="descontar" HeaderText="Descontar" SortExpression="UnitPrice" DataFormatString="{0:c}"></asp:BoundField>
                                                <asp:BoundField DataField="activo_nomina" HeaderText="" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="bono" HeaderText="" Visible="false"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Nomina">
                                                    <ItemTemplate>
                                                        <asp:Image ID="nomina" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bono">
                                                    <ItemTemplate>
                                                        <asp:Image ID="bono" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-9 col-md-9 col-sm-6">
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-6">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">Saldo de vales</span>
                                            <asp:TextBox ID="txtTotal" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnAceptar" runat="server" Text="Revisar" CssClass="btn btn-primary btn-block" OnClick="btnAceptar_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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