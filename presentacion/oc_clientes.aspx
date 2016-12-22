<%@ Page Title="Oc Digitales" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="oc_clientes.aspx.cs" Inherits="presentacion.oc_clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
        function getImage(path) {
            $("#myImage").attr("src", path);
            //alert(path);
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false, allowEscapeKey: false
            },
               function () {
                   location.href = URL;
               });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class=" page-header">Ordenes de Compra</h2>
    <div class="row">
        <div class="col-lg-12">
            <asp:FileUpload ID="fuparchivo" CssClass="form-control" runat="server" />
            <label style="color:red;"><strong>Solo se permiten los formato de IMAGEN: .bmp, .gif, .jpg, .dib</strong></label>
            <asp:LinkButton ID="lnksubir"  CssClass="btn btn-info btn-block" runat="server" OnClick="lnksubir_Click">Subir Archivo</asp:LinkButton>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <asp:Image ID="img"  CssClass="img-responsive " style="max-height:600px" runat="server" />
        </div>        
        <div class="col-lg-8 col-md-8 col-sm-6 col-xs-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <h4><strong>Cliente</strong></h4>
                    <asp:DropDownList ID="ddlcliente" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcliente_SelectedIndexChanged"></asp:DropDownList>
                    <asp:TextBox ID="txtcliente" OnTextChanged="lnkbuscar_Click" AutoPostBack="true" placeholder="Buscar Cliente" CssClass="form-control2" Width="80%" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="lnkbuscar" CssClass="btn btn-info" Width="18%" runat="server" OnClick="lnkbuscar_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                    <h4><strong>RFC</strong></h4>
                    <asp:TextBox ID="txtfrc" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    <h4><strong>CVE</strong></h4>
                    <asp:TextBox ID="txtcve" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <h4><strong>OC</strong></h4>
                    <asp:TextBox ID="txtox" ReadOnly="false" placeholder="Orden De Compra" CssClass="form-control" MaxLength="30" runat="server"></asp:TextBox>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <h4><strong>Cantidad</strong></h4>
                    <asp:TextBox ID="txtxantidad" AutoPostBack="True" ReadOnly="false" TextMode="Number" CssClass="form-control" 
                        onkeypress="return validarEnteros(event);" onfocus="this.select();" runat="server" OnTextChanged="txtxantidad_TextChanged"></asp:TextBox>
                </div>

                <div class="col-lg-12 col-xs-12" runat="server" id="enviar">
                    <asp:CheckBox ID="cbxenviar" Text="ENVIAR AVISO A USUARIO DE TMK" CssClass="radio3 radio-check radio-info radio-inline" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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
                                <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
