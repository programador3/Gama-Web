<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="seleccion_candidato.aspx.cs" Inherits="presentacion.seleccion_candidato" %>

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
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Seleccionar Candidatos para
                        <asp:Label ID="lblNombrePuesto" runat="server"></asp:Label></h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="text-align: left">
                    <div class="form-group">
                        <h4><strong>
                            <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label></strong>
                            <asp:Label ID="lblfechasoli" runat="server" Text="" Visible="false"></asp:Label>
                        </h4>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PanelCatalogo" runat="server">
                <h4 id="ExisteProce" runat="server" visible="false">Ya hay un Proceso de Pre Alta o Alta activo. No podra hacer ningun movimiento hasta que este termine o se cancele. <i class="fa fa-info-circle"></i></h4>

                <br />
                           <h3 id="nohay" runat="server" visible="false" style="text-align: center;">No hay pendientes <i class="fa fa-exclamation-triangle"></i></h3>
                                <asp:Repeater ID="repeat_candidatos" runat="server" OnItemDataBound="repeat_candidatos_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbxSelected" EventName="CheckedChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlorden" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtObservaciones" EventName="TextChanged" />
                                                <asp:PostBackTrigger ControlID="lnkInfoOrden" />
                                                <asp:PostBackTrigger ControlID="lnkver" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div style="border: 1px solid #000000;padding:10px;">
                                                    <asp:Label ID="lblidc_pre_empleado" runat="server" Text='<%#Eval("idc_pre_empleado") %>' Visible="false" CssClass="label label-danger"></asp:Label>

                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">
                                                                    <asp:LinkButton ID="lnkver" OnClick="lnkver_Click" Style="color: #fff; background-color: #19B5FE;" runat="server">Nombre <i class="fa fa-user"></i> </asp:LinkButton></span>
                                                                <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("nombre") %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg4 col-md-4 col-sm-12 col-xs-12">

                                                            <h4>
                                                                <asp:Image ID="imgYes" ImageUrl="~/imagenes/btn/checked.png" Visible="false" runat="server" />
                                                                <asp:Image ID="imgNo" ImageUrl="~/imagenes/btn/inchecked.png" runat="server" /><asp:Label ID="lblacepted" runat="server" Text="Rechazado" CssClass="label label-default"></asp:Label></h4>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                            <h4><strong>Seleccione para Aceptar, deje desmarcado para Rechazar</strong></h4>
                                                            <asp:CheckBox ID="cbxSelected" runat="server" Text="Aceptado" AutoPostBack="true" OnCheckedChanged="cbxSelected_CheckedChanged" CssClass="radio3 radio-check radio-info radio-inline" />
                                                        </div>
                                                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                                            <asp:Panel ID="panelorden" runat="server" Visible="false">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">
                                                                        <asp:LinkButton ID="lnkInfoOrden" OnClick="lnkInfoOrden_Click" Style="color: #fff; background-color: #19B5FE;" runat="server">Numero de Prioridad <i class="fa fa-sort-numeric-asc"></i></asp:LinkButton>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlorden" AutoPostBack="true" OnSelectedIndexChanged="ddlorden_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <asp:Label ID="lblerrororden" runat="server" Text="" Visible="false" CssClass="label label-danger"></asp:Label>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Panel ID="panelobsr" runat="server">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">
                                                                        <i class="fa fa-comment-o"></i></span>
                                                                    <asp:TextBox ID="txtObservaciones" AutoPostBack="true" OnTextChanged="txtObservaciones_TextChanged" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Observaciones"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="lblerrorobs" runat="server" Text="Debe Colocar una Observacion" Visible="true" CssClass="label label-danger"></asp:Label>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:Repeater>
            </asp:Panel>

            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary btn-block" OnClick="btnGuardar_Click1" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-danger btn-block" OnClick="btnCancelar_Click1" />
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
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" CausesValidation="false" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalPreviewView" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                            <h4>Información del Candidato <i class="fa fa-info-circle"></i>                               
                            </h4>
                        </div>
                        <div class="modal-body">
                               <h4 style="text-align: center;"><strong>Datos Personales <i class="fa fa-user"></i></strong></h4>
                                    <asp:Repeater ID="gridDetalles" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">Nombre <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("nombre") %>'></asp:TextBox>
                                                        </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                   <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">Genero <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="TextBox1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("sexo") %>'></asp:TextBox>
                                                        </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                   <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">Estado Civil <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="TextBox4" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("edo_civil") %>'></asp:TextBox>
                                                        </div>
                                                </div>

                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">Fecha de Nac <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="TextBox2" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("fec_nac") %>'></asp:TextBox>
                                                        </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-12">
                                                   <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">Dirección <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="TextBox5" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("direccion") %>'></asp:TextBox>
                                                        </div>
                                                </div>
                                            </div>
                                            <h4 style="text-align: center;"><strong>Contacto <i class="fa fa-mobile"></i></strong></h4>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                   <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">Correo <i class="fa fa-laptop"></i></span>
                                                            <asp:TextBox ID="TextBox3" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("correo_personal") %>'></asp:TextBox>
                                                        </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <asp:Repeater ID="repeat_telefonos" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                   <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;"><i class="fa fa-mobile"></i></span>
                                                            <asp:TextBox ID="TextBox3" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("telefono") %>'></asp:TextBox>
                                                        </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <div class="row" id="datos2" runat="server" visible="true">
                                        <h4 style="text-align: center;"><strong>Papeleria <i class="fa fa-archive"></i></strong></h4>
                                        <asp:Repeater ID="repeat_papeleria" runat="server" OnItemDataBound="repeat_papeleria_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="col-lg-6 col-md-6 col-sm-12">
                                                    <div class="input-group">
                                                            <asp:TextBox ID="TextBox3" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>

                                                            <span class="input-group-addon" style="color: #fff; background-color: #19B5FE;">
                                                                <asp:LinkButton ID="lnkdownload" Style="color: #fff; background-color: #19B5FE;" runat="server" OnClick="lnkdownload_Click">Descargar <i class="fa fa-download"></i></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:TextBox ID="txtobservaciones2" TextMode="MultiLine" Rows="3" ReadOnly="true" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-lg-12">
                                            <input id="btnModalAcept" class="btn btn-primary btn-block" value="Cerrar" onclick="ModalClose();" />
                                        </div>
                                    </div>
                        </div>
                    </div>
                    <!-- /.CONFIRMA -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>