<%@ Page Title="Catalogo" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_procesos.aspx.cs" Inherits="presentacion.catalogo_procesos" %>

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
            $('#myModalArea').modal('hide');
            $('#myModal').modal('hide');
            $('#myModalImg').modal('hide');
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
        function ModalImgc(cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalImg').modal('show');
            $('#myModalImg').removeClass('modal fade modal-info');
            $('#myModalImg').addClass(ctype);
            $('#content_modali').text(cContenido);
        }
        function ModalA(cTitulo) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalArea').modal('show');
            $('#confirmTituloa').text(cTitulo);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
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
        function AsignaProgress(value) {
            $('#pavance').width(value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Catalogo de Procesos <span>
        <asp:Label ID="lnltipo" runat="server" Text=" Tipo Borrador" CssClass="btn btn-primary"></asp:Label></span></h1>
    <div class="row">
        <div class="col-lg-4 col-md-5 col-sm-5 col-xs-12">
            <asp:LinkButton ID="lbknuevoprocesos" CssClass="btn btn-info btn-block" runat="server" OnClick="lbknuevoprocesos_Click">Nuevo Proceso <i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
        </div>
        <div class="col-lg-4 col-md-2 col-sm-2">
        </div>
        <div class="col-lg-4 col-md-5 col-sm-5 col-xs-12">
            <asp:LinkButton ID="lnkborrador" OnClick="lnkborrador_Click" CssClass="btn btn-primary btn-block" Visible="false" runat="server">Ver Borrador</asp:LinkButton>
            <asp:LinkButton ID="lnkproduccion" OnClick="lnkproduccion_Click" CssClass="btn btn-success btn-block" Visible="true" runat="server">Ver Producción</asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridprocesos" CssClass="gvv table table-responsive table-bordered" runat="server" DataKeyNames="idc_proceso,idc_proceso_borr,descripcion" OnRowDataBound="gridprocesos_RowDataBound" AutoGenerateColumns="false" OnRowCommand="gridprocesos_RowCommand">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Eliminar">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="idc_proceso" HeaderText="Area" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="idc_proceso_BORR" HeaderText="Area" Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Proceso">
                            <ItemTemplate>
                                <asp:Button ID="btnproce" CommandName="preview" CssClass="btn btn-default btn-block" runat="server" Text='<%#Eval("descripcion") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-CssClass="btn btn-default btn-block" DataField="descripcion" HeaderText="Proceso" Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Sub-Procesos" HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Button ID="Button1" CssClass="btn btn-info btn-block" runat="server" Text='<%#Eval("total_subprocesos") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="usuario" HeaderText="Autor" HeaderStyle-Width="80px"></asp:BoundField>
                        <asp:TemplateField HeaderText="Produccion" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <asp:Image ID="Image1" ImageUrl='<%#Eval("produccion") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Borrador" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <asp:Image ID="Image2" ImageUrl='<%#Eval("borrador") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/btn_solicitar_equipos.png" HeaderText="Autorizacion" CommandName="Solicitar">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="Yes" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardarSinLigar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSinLigar" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btncrearborrador" />
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
                            <asp:Panel ID="PanelNuevoBorrador" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h5 style="text-align: center;"><strong>Escriba el Nombre del Nuevo Manual</strong></h5>
                                        <asp:TextBox ID="txtNombrePerfil" placeholder="Escriba el Nombre del Perfil" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblerror" CssClass="label label-danger" runat="server" Text="ESCRIBA EL NOMBRE DEL MANUAL" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-md-6 col-xs-12">
                                    <asp:Button Visible="false" ID="btnGuardarSinLigar" runat="server" Text="Guardar" class="btn btn-success btn-block" OnClick="btnGuardarSinLigar_Click" />
                                    <asp:Button Visible="false" ID="btnSinLigar" class="btn btn-success btn-block" runat="server" Text="Si, Borrador sin ligar a Manual" OnClick="btnSinLigar_Click" />
                                </div>
                                <div class="col-lg-6 col-md-6 col-md-6 col-xs-12">
                                    <asp:Button ID="btncrearborrador" class="btn btn-primary btn-block" runat="server" Text="Si, Borrador Ligado a este Manual" OnClick="btncrearborrador_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-md-6 col-xs-12">
                                    <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                                </div>
                                <div class="col-lg-6 col-md-6 col-md-6 col-xs-12">
                                    <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>