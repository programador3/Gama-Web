<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="pre_empleados_pendientes_alta.aspx.cs" Inherits="presentacion.pre_empleados_pendientes_alta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class=" col-lg-12">
        <h3 class=" page-header">Pre Empleados Pendientes de Alta</h3>
            </div>
        <div class=" col-lg-12">
            <asp:GridView ID="gridCatalogo" runat="server" OnRowCommand="gridCatalogo_RowCommand" AutoGenerateColumns="false" CssClass="gvv table table-bordered table-hover table-condensed" 
                DataKeyNames="idc_prepara ,idc_pre_empleado,CORREO_PERSONAL,SE_ENVIARA_CORREO,nombre">
                <Columns>
                    <asp:TemplateField HeaderText="Rechazar">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle Width="60px"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkrege" CssClass="btn btn-primary btn-block" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                CommandName="RegenerarGUID" runat="server">Regenerar Link</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="idc_pre_empleado" HeaderText="idc_pre_empleado" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="CORREO_PERSONAL" HeaderText="idc_pre_empleado" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="SE_ENVIARA_CORREO" HeaderText="idc_pre_empleado" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="idc_prepara" HeaderText="idc_prepara" Visible="false"></asp:BoundField>
                    <asp:ButtonField DataTextField="nombre" ControlStyle-CssClass="btn btn-default btn-block" HeaderText="Candidato" CommandName="Puesto" />
                    <asp:BoundField DataField="puesto" HeaderText="Puesto"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="text-align: center">
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gridCatalogo" EventName="RowCommand" />
                            </Triggers>
                            <ContentTemplate>
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
                                    <div class="row" id="cambiar_fecha" runat="server" visible="false">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">                                           
                                                <asp:Label Style="font-weight: 700; color: red" ID="lbltextocorreo" Visible="false" runat="server" Text=""></asp:Label>
                                            <asp:TextBox ID="txtcorreo" CssClass="form-control" Style="text-transform: uppercase;" onblur="return imposeMaxLength(this, 250);" 
                                                TextMode="Email" placeholder="Ingrese un Correo" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtmotivo" CssClass="form-control" Style="text-transform: uppercase;" onblur="return imposeMaxLength(this, 250);" TextMode="MultiLine" placeholder="Ingrese un Motivo" 
                                                Rows="3" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="row">
                                        <div class="col-lg-6 col-xs-6">
                                            <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClientClick="ModalClose();" OnClick="Yes_Click" CausesValidation="false" />
                                        </div>
                                        <div class="col-lg-6 col-xs-6">
                                            <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
</asp:Content>
