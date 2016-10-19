<%@ Page Title="Catalgo de Reportes" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_reportes.aspx.cs" Inherits="presentacion.catalogo_reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Gift(mensaje) {
            swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '800', showConfirmButton: false });
        }
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
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class=" page-header">Catalogo de Reportes</h2>
    <div class="row">
        <div class="col-lg-12">
            <asp:LinkButton ID="lnkservicio" CssClass="btn btn-info btn-block" PostBackUrl="reportes_tipo_captura.aspx" runat="server">Agregar Nuevo Reporte&nbsp;<i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
            <div class="table table-responsive">
                <asp:GridView Style="font-size: 12px;" ID="gridservicios" DataKeyNames="idc_tiporep,descripcion" 
                    CssClass=" gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server" OnRowCommand="gridservicios_RowCommand">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>

                        <asp:TemplateField HeaderText="Borrar" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <asp:ImageButton CommandName="Borrar" Visible='<%#Convert.ToInt32(Eval("TOTAL_PUESTOS"))>0?false:true %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    ID="ImageButton1" runat="server" Width="30px" Height="30px" ImageUrl="~/imagenes/btn/icon_delete.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n"></asp:BoundField>
                        <asp:TemplateField HeaderText="Puestos Rel." HeaderStyle-Width="90px">
                            <ItemTemplate>
                                <asp:Button ID="Button1" CssClass="btn btn-info btn-block" runat="server" Text='<%#Eval("puestos_rel") %>' CommandName="Puestos" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Aplica Todos" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton13e" runat="server" Width="40px" Height="40px" ImageUrl='<%#Eval("todos_img") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="idc_tiporep" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                    </Columns>
                </asp:GridView>
                <div style="text-align: left; font-size: 11px; color: orangered;">
                    <label style="font: italic;"><strong>LOS REPORTES CON PUESTOS RELACIONADOS, Y QUE NO HAN SIDO REVISADO NO PUEDEN ELIMINARSE</strong></label>
                </div>
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
                        <input id="No" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
