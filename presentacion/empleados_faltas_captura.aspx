<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="empleados_faltas_captura.aspx.cs" Inherits="presentacion.empleados_faltas_captura" %>

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
        } ModalPreviewHeramienta
        function ModalPreviewHeramienta() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalPreview(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 1000);
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');

        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        <%--function ClickJustificar() {
            var a = $('#<%= btnjustifica.ClientID %>').attr('class');
            if (a == "btn btn-success btn-block") {
                $("#row_ar").css("visibility", "hidden");

            } else {

                $("#row_ar").css("visibility", "visible");
            }
        }--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Revisión de Falta </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-2" style="align-content: center;">
                    <a>
                        <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 120px; margin: 0 auto;" />
                    </a>
                </div>
                <div class="col-lg-10" style="text-align: left">
                    <h4>
                        <strong>Nombre Empleado: </strong>
                        <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                    </h4>
                    <h4><strong>Puesto: </strong>
                        <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                    </h4>
                    <h4><strong>Fecha de Falta: </strong>
                        <asp:Label ID="lblfecha" runat="server" Text=""></asp:Label>
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-xs-12">
                    <h4><i class="fa fa-comments-o" aria-hidden="true"></i>&nbsp;Observaciones</h4>
                    <asp:TextBox ID="txtobservaciones" Style="text-transform: uppercase;" CssClass="form-control" onblur="imposeMaxLength(this,1000);" placeholder="Observaciones" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                </div>
            </div>
            <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton ID="lbkasistencia" CommandName="lbkasistencia" CssClass="btn btn-default btn-block" runat="server" OnClick="lbkasistencia_Click">Click para Marcar Asistencia</asp:LinkButton>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton ID="lnkjustifica" CommandName="lnkjustifica" CssClass="btn btn-default btn-block" runat="server" OnClick="lbkasistencia_Click">Click para Marcar Justificación</asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row" id="row_ar" runat="server">
                <div class="col-lg-12">
                    <div class="panel panel-success fresh-color">
                        <div class="panel-heading" style="text-align: center;">
                            Anexar Justificante
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="form-control" placeholder="Descripcion del documento" onkeypress="return isNumber(event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                                            <span class="input-group-addon" style="color: #fff; background-color: #1ABC9C;">
                                                <asp:LinkButton ID="lnkGuardarPape" Style="color: #fff; background-color: ##1ABC9C;" OnClick="lnkGuardarPape_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="descripcion, nombre, ruta, extension">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="50" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Button" HeaderStyle-Width="50" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="extension" HeaderText="extension" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre Documento"></asp:BoundField>
                                                <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-sm-6 col-xs-6">
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary btn-block" runat="server" OnClick="LinkButton1_Click">Guardar</asp:LinkButton>
                </div>
                <div class="col-lg-6 col-sm-6 col-xs-6">

                    <asp:LinkButton ID="lnkcancelar" CssClass="btn btn-danger btn-block" runat="server" OnClick="lnkcancelar_Click">Cancelar</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <!-- /.CONFIRMA -->
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
</asp:Content>