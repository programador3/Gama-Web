<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="quejas_acciones.aspx.cs" Inherits="presentacion.quejas_acciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
        function ModalClose() {
            $('#myModal').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label>
            </h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Numero de Queja</strong>&nbsp;<span><asp:LinkButton ID="lnkagregarcomentario" CssClass="btn btn-info" runat="server" OnClick="lnkagregarcomentario_Click">Otra Queja <i class="fa fa-pencil" aria-hidden="true"></i></asp:LinkButton></span></h4>
            <asp:TextBox ID="txtnoqueja" autofocus AutoPostBack="true" Style="color: cornflowerblue;" CssClass="form-control" ReadOnly="true" runat="server" OnTextChanged="txtnoqueja_TextChanged" TextMode="Number"></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente</strong></h4>
            <asp:TextBox ID="txtcliente" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-thumbs-o-down" aria-hidden="true"></i>&nbsp;Problema</strong></h4>
            <asp:TextBox ID="txtproblema" CssClass="form-control" TextMode="MultiLine" Rows="5" ReadOnly="true" Style="font-size: 11px;" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row" id="solucionar" runat="server" visible="false">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-cogs" aria-hidden="true"></i>&nbsp;Solución</strong></h4>
            <asp:TextBox ID="txtsolucion" CssClass="form-control" TextMode="MultiLine" Rows="5" Style="font-size: 11px;" ReadOnly="false" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Encargado</strong></h4>
            <asp:TextBox ID="txtencargado" onblur="return imposeMaxLength(this, 99);" CssClass="form-control" ReadOnly="false" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <asp:UpdatePanel ID="upda" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:LinkButton ID="lnksatisf" CssClass="btn btn-info btn-block" runat="server" OnClick="lnksatisf_Click">Cliente Satisfecho</asp:LinkButton>
                    <asp:TextBox ID="txtdescripcionsatis" placeholder="Describe aqui el motivo por el cual el cliente no quedo satisfecho" Visible="false" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-file-image-o" aria-hidden="true"></i>&nbsp;Imagen</strong></h4>
            <asp:FileUpload CssClass="form-control" ID="fupimagen" runat="server" />
            <asp:RegularExpressionValidator ID="REV" runat="server" CssClass="label label-danger"
                ErrorMessage="Tipo de archivo no permitido. Debe ser JPG, JPEG" ControlToValidate="fupimagen"
                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.jpeg|.JPEG)$">
            </asp:RegularExpressionValidator>
        </div>

        <div class="col-lg-12">
            <h4><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha de Solución</strong></h4>
            <asp:TextBox ID="txtfecha" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row" id="cancelar" runat="server" visible="false">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-comment" aria-hidden="true"></i>&nbsp;Observaciones</strong></h4>
            <asp:TextBox ID="txtobservaciones_can" onblur="return imposeMaxLength(this, 253);" placeholder="Describe aqui el motivo de la cancelacion" Visible="true" CssClass="form-control" TextMode="MultiLine" Rows="5" Style="font-size: 11px;" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row" id="comentario" runat="server" visible="false">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-comment" aria-hidden="true"></i>&nbsp;Comentario</strong></h4>
            <asp:TextBox ID="txtcomentario" placeholder="Comentario" Visible="true" CssClass="form-control" TextMode="MultiLine" Rows="5" Style="font-size: 11px;" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">

            <asp:LinkButton ID="lnkcomentario" Visible="false" CssClass="btn btn-primary btn-block" runat="server" OnClick="lnkcomentario_Click">Agregar Comentario</asp:LinkButton>
            <asp:LinkButton ID="lnksoli" Visible="false" CssClass="btn btn-primary btn-block" runat="server" OnClick="lnksoli_Click">Solucionar</asp:LinkButton>
            <asp:LinkButton ID="lnkcance" Visible="false" CssClass="btn btn-primary btn-block" runat="server" OnClick="lnkcance_Click">Cancelar Queja</asp:LinkButton>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:LinkButton ID="lnkcerrar" CssClass="btn btn-danger btn-block" runat="server" OnClick="lnkcerrar_Click">Cerrar</asp:LinkButton>
        </div>
    </div>
    <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
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
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="TesClick_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>