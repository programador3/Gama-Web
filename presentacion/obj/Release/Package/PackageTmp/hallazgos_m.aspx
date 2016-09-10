<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="hallazgos_m.aspx.cs" Inherits="presentacion.hallazgos_m" %>
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
        function OnClickG() {
            ModalConfirm('Mensaje del Sistema', '¿ Desea Guardar este Hallazgo ?', 'modal fade modal-info');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
      <h2 class="page-header">Captura Hallazgos</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <h5><strong><i class="fa fa-institution" aria-hidden="true"></i>&nbsp;Sucursal</strong> <span>
                        <asp:LinkButton ID="blksucursal" CssClass="btn btn-success" runat="server" OnClick="blksucursal_Click">Ver Pendientes de Sucursal&nbsp;<i class="fa fa-share" aria-hidden="true"></i></asp:LinkButton></span></h5>
                    <asp:DropDownList ID="ddlsucursal" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>
                <div class="col-lg-12">
                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Usuario Solución</strong></h5>
                    <asp:DropDownList ID="ddlusu" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlusu_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </div>
                <div class="col-lg-12" id="vehi" runat="server" visible="false">
                    <h5><strong><i class="fa fa-car" aria-hidden="true"></i>&nbsp;Vehiculo</strong></h5>
                    <asp:DropDownList ID="ddlvehiculo" CssClass="form-control2" Width="100%" runat="server"></asp:DropDownList>                    
                    <asp:TextBox ID="txtbiscar" CssClass="form-control2" Width="50%" placeholder="Buscar Vehiculo" runat="server"></asp:TextBox>
                     <asp:LinkButton ID="lnkbuscar" CssClass="btn btn-info" Width="48%" OnClick="lnkbuscar_Click" runat="server">Buscar</asp:LinkButton>
                </div>
                <div class="col-lg-12">
                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Detalles</strong></h5>
                    <asp:TextBox ID="txtdetalles" CssClass="form-control" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">

        <div class="col-lg-12">
            <h5><strong><i class="fa fa-image" aria-hidden="true"></i>&nbsp;Imagen</strong></h5>
            <asp:FileUpload ID="fuparchivos" CssClass="form-control" runat="server" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:UpdatePanel ID="dd" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClientClick="OnClickG();" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" />
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
                        <asp:Button ID="Yes" class="btn btn-info btn-block"  OnClientClick="ModalClose(); Gift('Estamos Guardando el Hallazgo y Enviando los Correos');" runat="server" Text="Si" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
