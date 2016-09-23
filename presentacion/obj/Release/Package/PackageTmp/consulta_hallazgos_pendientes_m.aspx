<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="consulta_hallazgos_pendientes_m.aspx.cs" Inherits="presentacion.consulta_hallazgos_pendientes_m" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
        }
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class="page-header">Consulta de Hallazgos</h2>
    <div class="row">
        <div class="col-lg-12">
            <asp:LinkButton ID="lnk" CssClass="btn btn-danger btn-block" runat="server" OnClick="lnk_Click">Cerrar Ventana</asp:LinkButton>
        </div>
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridhallazgos" Style="font-size: 11px; text-align: center;" OnRowCommand="gridhallazgos_RowCommand" DataKeyNames="idc,observaciones" CssClass="gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Imagen" CommandName="Ver" Text="Ver">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="sucursal" HeaderText="Sucursal">
                            <HeaderStyle Width="90px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha">
                            <HeaderStyle Width="90px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_solucion" HeaderText="Solucion">
                            <HeaderStyle Width="90px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="usuario_sol" HeaderText="Usuario Solu.">
                            <HeaderStyle Width="90px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="num_economico" HeaderText="No. Eco.">
                            <HeaderStyle Width="40px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="vehiculo" HeaderText="Vehiculo">
                            <HeaderStyle Width="130px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="observaciones" HeaderText="Hallazgo" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="hallazgo_corto" HeaderText="Hallazgo"></asp:BoundField>

                        <asp:BoundField DataField="idc" Visible="false" HeaderText="Hallazgo"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
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
                            <asp:TextBox ReadOnly="true" ID="txthallazgo" CssClass="form-control" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-12" style="display: table-cell; vertical-align: middle; text-align: center;">

                            <asp:Image AlternateText="El Sistema No Encontro la Imagen en la Ruta, Puede Deberse a que sea un proyecto de pruebas." ID="img" Style="margin-left: auto; margin-right: auto;"
                                runat="server" CssClass="image img-responsive" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-12">
                        <input id="No" class="btn btn-info btn-block" onclick="ModalClose();" value="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>