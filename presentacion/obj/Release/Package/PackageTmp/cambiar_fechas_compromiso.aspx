<%@ Page Title="Cambiar Fechas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cambiar_fechas_compromiso.aspx.cs" Inherits="presentacion.cambiar_fechas_compromiso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <h1 class="page-header">Cambiar Fechas Compromiso</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-success fresh-color">
                        <div class="panel-heading">
                            Seleccione la fecha compromiso para cada puesto
                        </div>
                        <div class="panel-body">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row" id="cambiar_fecha" runat="server" style="border: 1px solid #000000;">
                                        <div class="col-lg-6 col-md-6  col-sm-12 col-xs-12 ">
                                            <h4><i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;<strong><%#Eval("descripcion") %></strong></h4>
                                        </div>
                                        <div class="col-lg-12 col-md-12  col-sm-12 col-xs-12">
                                            <h4><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Compromiso</h4>
                                            <asp:TextBox ID="txtnueva_fecha" CssClass="form-control" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                            <asp:TextBox ID="txtmotivo" CssClass="form-control" Style="text-transform: uppercase;" onblur="return imposeMaxLength(this, 250);" TextMode="MultiLine" placeholder="Ingrese un Motivo" Rows="2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("idc_prepara") %>' Visible="false"></asp:Label>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" CausesValidation="false" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>