<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="recordatorios.aspx.cs" Inherits="presentacion.recordatorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModalrec').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalrec').modal('show');
            $('#myModalrec').removeClass('modal fade modal-info');
            $('#myModalrec').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Nuevo Recordatorio</h1>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Asunto</strong></h4>
            <asp:TextBox onfocus="$(this).select();" onblur="return imposeMaxLength(this, 50);" TextMode="Multiline" Rows="2" ID="txtasunto_rec" CssClass="form-control" placeholder="Asunto" runat="server" autofocus></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-file-text" aria-hidden="true"></i>&nbsp;Descripción</strong></h4>
            <asp:TextBox onfocus="$(this).select();" onblur="return imposeMaxLength(this, 1000);" TextMode="Multiline" Rows="5" ID="txtdesc_rec" CssClass="form-control" placeholder="Descripción" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</strong></h4>
            <asp:TextBox ID="txtfecha_rec" CssClass="form-control" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-envelope" aria-hidden="true"></i>&nbsp;Correo Relacionado</strong></h4>
            <asp:TextBox ID="txtcorreo_rec" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
        </div>
    </div>

    <div class="modal fade modal-info" id="myModalrec" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="modal_title"><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4>
                                <label id="content_modal"></label>
                            </h4>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>