<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="puestos_alta_autorizar.aspx.cs" Inherits="presentacion.puestos_alta_autorizar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
        function ModalClose() {
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">Solicitudes Alta de Puestos</h3>
        </div>
        <div class="col-lg-12">

        </div>
        <div class="col-lg-12">
            <div class="table table-responsive" style="text-align: center;">
                <asp:GridView ID="grid_prepuestos" DataKeyNames="descripcion,idc_pre_puesto" CssClass="table table-responsive table-bordered table-condensed gvv" runat="server" AutoGenerateColumns="False" OnRowCommand="grid_prepuestos_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="idc_pre_puesto" HeaderText="IDC_PRE_PUESTO" Visible="False"></asp:BoundField>
                        <asp:ButtonField DataTextField="descripcion" HeaderText="Puesto" CommandName="Vista" />  
                        <asp:BoundField DataField="usuario" HeaderText="Usuario Solicito" HeaderStyle-Width="250px"></asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Status" HeaderStyle-Width="100px"></asp:BoundField>
                        <asp:TemplateField HeaderText="Acciones" HeaderStyle-Width="150px">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnksolicitar"  CommandName="Responder"
                                    CssClass="btn btn-success" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">
                                    Responder Solicitud&nbsp;<i class="fa fa-check-circle" aria-hidden="true"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:Label ID="lblid" Visible="false" runat="server" Text="0"></asp:Label>
    <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grid_prepuestos" EventName="RowCommand" />
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
                                <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" OnClientClick="ModalClose();" Text="Aceptar" OnClick="Yes_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" type="button" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>