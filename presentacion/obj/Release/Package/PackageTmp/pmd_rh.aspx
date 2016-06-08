<%@ Page Title="Catalogo" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="pmd_rh.aspx.cs" Inherits="presentacion.pmd_rh" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">Pendientes de PMD
                           <input id="inp_gr" style="visibility: hidden;" value="Ver Grafica Global" class="btn btn-primary" onclick="ModalGraph();" />
                </h1>
            </div>
        </div>
        <asp:Panel ID="PanelPrebajas" runat="server">
            <h2 id="Noempleados" runat="server" visible="false" style="text-align: center;">No hay Preparaciones Activas <i class="fa fa-exclamation-triangle"></i></h2>
            <asp:Repeater ID="repeatpendientes" runat="server">
                <ItemTemplate>
                    <div class="col-lg-4 col-md-6 col-sm-6">
                        <asp:Panel ID="PanelRevisionP" runat="server" class='<%#Eval("css_class") %>'>
                            <div class="inner">
                                <h4><%#Eval("tipo") %>
                                </h4>
                                <p>
                                    <h6>
                                        <asp:Label ID="lbl" runat="server" Text='<%#Eval("empleado") %>'></asp:Label></h6>
                                </p>
                            </div>
                            <div class="icon">
                                <asp:LinkButton ID="lnkGOdET" Style="color: white;" runat="server" CommandName='<%#Eval("idc_empleado_pmd") %>' OnClick="lnkGO_Click"><i class="ion ion-arrow-right-c"></i></asp:LinkButton>
                            </div>
                            <asp:LinkButton ID="lnkGO" runat="server" CssClass="small-box-footer" OnClick="lnkGO_Click" CommandName='<%#Eval("idc_empleado_pmd") %>'>Ver Detalles <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                        </asp:Panel>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
</asp:Content>