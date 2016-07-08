<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="lugares_captura.aspx.cs" Inherits="presentacion.lugares_captura" %>

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
    <asp:UpdatePanel ID="u" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkguardar" />
            <asp:PostBackTrigger ControlID="gridlugares" />
        </Triggers>
        <ContentTemplate>
            <h1 class="page-header">
                <asp:Label ID="lbltitle" runat="server" Text="lbltitle"></asp:Label></h1>
            <div class="row">
                <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: center;">
                    <h4><i class="fa fa-user" aria-hidden="true"></i>&nbsp;<asp:Label ID="lblareaname" runat="server" Text=""></asp:Label></h4>
                    <asp:Image ID="imgarea" CssClass="image img-responsive" runat="server" />
                </div>
                <div class="col-lg-6 col-md-12 col-sm-12">
                    <h4><i class="fa fa-plus-circle" aria-hidden="true"></i>&nbsp;Agregar Lugar</h4>
                    <h5><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre</h5>
                    <asp:TextBox ID="txtnombre" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" CssClass="form-control" placeholder="Nombre del Lugar" runat="server"></asp:TextBox>
                    <h5><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Alias</h5>
                    <asp:TextBox ID="txtalias" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 10);" CssClass="form-control" placeholder="Alias del Lugar" runat="server"></asp:TextBox>
                    <h5><i class="fa fa-file-image-o" aria-hidden="true"></i>&nbsp;Imagen del Lugar</h5>
                    <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                    <br />
                    <asp:LinkButton ID="lnkguardar" CssClass="btn btn-info btn-block" runat="server" OnClick="lnkguardar_Click">Guardar Lugar <i class="fa fa-floppy-o" aria-hidden="true"></i></asp:LinkButton>
                    <br />
                    <div class="alert alert-danger fade in">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>NOTA:</strong>&nbsp;Puede descargar la IMAGEN original del AREA y marcar el area con un editor de imagenes, para usarla como imagen representativa del lugar de trabajo.
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="table table-responsive">
                        <asp:GridView ID="gridlugares" DataKeyNames="idc_lugart,nombre,lugar,alias, ruta" CssClass="table table-responsive table-bordered" runat="server" AutoGenerateColumns="False" OnRowDataBound="gridlugares_RowDataBound" OnRowCommand="gridlugares_RowCommand">

                            <Columns>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/more.png" HeaderText="Ver" CommandName="Ver">
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:TemplateField HeaderText="Ocupado" HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:Image ID="ocupado" runat="server" Width="30px" ImageUrl="~/imagenes/btn/inchecked.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="idc_lugart" HeaderText="idc_lugart" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="Alias" HeaderText="Alias" HeaderStyle-Width="90px"></asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre"></asp:BoundField>
                                <asp:BoundField DataField="ruta" HeaderText="ruta" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="lugar" HeaderText="lugar" Visible="false"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="modal fade modal-info" id="myModalImg" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="text-align: center;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="content_modali"></label>
                                    </h4>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <asp:Image ID="imgmodal" CssClass="image img-responsive" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-12">
                                <input id="Nwwwwwwo" class="btn btn-info btn-block" onclick="ModalClose();" value="Cerrar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
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
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>