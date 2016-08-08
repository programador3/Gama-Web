<%@ Page Title="Candidatos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="candidatos_preparar.aspx.cs" Inherits="presentacion.candidatos_preparar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
        }

        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <br />
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Puestos por Reclutar&nbsp;<asp:Label Visible="false" ID="lblto" runat="server" Text=""></asp:Label>
                    </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:LinkButton ID="lnkexcel" CssClass="btn btn-success" runat="server" OnClick="lnkexcel_Click">Exportar Listado a Excel <i class="fa fa-file-excel-o" aria-hidden="true"></i></asp:LinkButton>
                    <div class="table table-responsive">
                        <asp:GridView AutoGenerateColumns="false" ID="gridreclu" DataKeyNames="idc_puesto,idc_prepara" OnRowCommand="gridreclu_RowCommand" CssClass="table table-responsive table-bordered table-condensed gvv" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="Proceso" HeaderStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:Button ID="btnproce" CommandName="preview" CssClass="btn btn-info btn-block" runat="server" Text="Detalles" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="fecha_registro" HeaderStyle-Width="100px" HeaderText="Fecha de Solicitud"></asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Puesto" HeaderStyle-Width="120px"></asp:BoundField>
                                <asp:BoundField DataField="fecha_compromiso_reclutamiento" HeaderStyle-Width="100px" HeaderText="Fecha Compromiso"></asp:BoundField>
                                <asp:BoundField DataField="total_candidatos" HeaderStyle-Width="10px" HeaderText="Candidatos Reclutados"></asp:BoundField>
                                <asp:BoundField DataField="reclutador" HeaderStyle-Width="150px" HeaderText="Reclutador"></asp:BoundField>
                                <asp:BoundField DataField="idc_puesto" HeaderText="Area" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="idc_prepara" HeaderText="Area" Visible="false"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PanelPrincipal" runat="server" Visible="false">
                <h2 id="Noempleados" runat="server" visible="false" style="text-align: center;">No hay Pendientes Activos <i class="fa fa-exclamation-triangle"></i></h2>
                <h2 id="H1" runat="server" visible="true" style="text-align: center;"><a class="btn btn-success" href="cambiar_fechas_compromiso.aspx">Cambiar Fechas Compromiso <i class="fa fa-calendar" aria-hidden="true"></i></a></h2>
                <asp:Repeater ID="repeatpendientes" runat="server" OnItemDataBound="repeatpendientes_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <asp:Panel ID="PanelRevisionP" runat="server" class='<%#Eval("css_class") %>'>
                                <div class="inner">
                                    <h4>Reclutar Candidatos
                                    </h4>
                                    <h6>
                                        <asp:Label ID="lbl" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                    </h6>
                                    <h6>FC:
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("fecha_compromiso_reclutamiento") %>'></asp:Label>
                                    </h6>
                                    <h6>Candidatos Reclutados:&nbsp;<strong><%#Eval("total_candidatos") %></strong>
                                    </h6>
                                    <h6>Reclutador:&nbsp;<strong><%#Eval("reclutador") %></strong>
                                    </h6>
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
        </div>
    </div>
</asp:Content>