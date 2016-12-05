<%@ Page Title="Captura de Tickets" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="ticket_serv_captura.aspx.cs" Inherits="presentacion.ticket_serv_captura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#myModalinfo').modal('hide');
            $('#myModal').modal('hide');
        }
        function Modalinfo() {
            $('#myModalinfo').modal('show');
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
        function ModalConfirmEmpresa(cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalempresa').modal('show');
            $('#myModalempresa').removeClass('modal fade modal-info');
            $('#myModalempresa').addClass(ctype);
            $('#content_modalempresa').text(cContenido);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class=" page-header">Nuevo Ticket de Servicio</h2>
    <div class="row">
        <asp:TextBox ID="txtguid" Visible="false" runat="server"></asp:TextBox>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class=" col-lg-12">
                    <h4><strong><i class="fa fa-cog" aria-hidden="true"></i>&nbsp;Seleccione un Tipo de Servicio</strong></h4>
                    <asp:TextBox onfocus="this.select();" AutoPostBack="true" CssClass=" form-control2" Width="80%" placeholder="Buscar Servicio" ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                    <asp:LinkButton ID="lnkbuscar" runat="server" CssClass="btn btn-success" Width="18%" OnClick="lnkbuscar_Click">Buscar</asp:LinkButton>
                    <asp:DropDownList ID="ddltiposervicios" CssClass=" form-control2" Width="80%" runat="server"></asp:DropDownList>
                    <asp:LinkButton ID="lnkinfo" runat="server" CssClass="btn btn-info" Width="50px" OnClick="lnkinfo_Click" >
                        <i class="fa fa-info-circle" aria-hidden="true"></i></asp:LinkButton>

                </div>
                <div class="modal fade modal-info" id="myModalinfo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">

                        <div class="modal-content">
                            <div class="modal-header" style="text-align: center;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4><strong>Información del Servicio</strong></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row" style="text-align: center;">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                        <h5><strong>Descripción del Servicio</strong></h5>
                                        <asp:TextBox ID="txtdesc" ReadOnly="true" CssClass=" form-control" Style="font-size: 11px" TextMode="Multiline" Rows="3" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                        <h5><strong>Puestos que pueden dar este tipo de servicio</strong></h5>
                                        <asp:GridView style="text-align: left;" ID="grdipersona" CssClass="table table-responsive table-bordered table-condensed"  AutoGenerateColumns="false" runat="server">

                                            <Columns>
                                                <asp:BoundField DataField="puesto" HeaderText="Puesto | Empleado"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="col-lg-12">
                                    <input id="Nodd" class="btn btn-info btn-block" type="button" onclick="ModalClose();" value="Cerrar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class=" col-lg-12">
            <h4><strong><i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;Observaciones del Ticket</strong></h4>
            <asp:TextBox  onblur="return imposeMaxLength(this, 249);" CssClass=" form-control" TextMode="Multiline" Rows="4" placeholder="Observaciones" ID="txtobservaciones" runat="server"></asp:TextBox>
        </div>
    </div>
     <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Ticket" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                </div>
            </div>
    <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="Yes" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                </Triggers>
                <ContentTemplate>

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
                                <input id="No" class="btn btn-danger btn-block" type="button" onclick="ModalClose();" value="Cancelar" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
</asp:Content>
