<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="areas.aspx.cs" Inherits="presentacion.areas" %>

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
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
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
    <h1 class="page-header" style="text-align: center;">Catalogo de Areas</h1>
    <div class="row">
        <div class="col-lg-12">
            <asp:LinkButton ID="lnk" CssClass="btn btn-info btn-block" OnClick="lnk_Click" runat="server"><i class="fa fa-plus-circle" aria-hidden="true"></i>&nbsp;Agregar Nueva Area </asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridareas" CssClass="gvv table table-responsive table-bordered" runat="server" DataKeyNames="idc_area,nombre,idc_sucursal,ruta" AutoGenerateColumns="false" OnRowCommand="gridareas_RowCommand">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/more.png" HeaderText="Ver" CommandName="Ver">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="idc_area" HeaderText="Area" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Area"></asp:BoundField>
                        <asp:BoundField DataField="sucursal" HeaderText="Sucursal"></asp:BoundField>
                        <asp:BoundField DataField="idc_sucursal" HeaderText="Sucursal" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="ruta" HeaderText="Sucursal" Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Numero de Lugares" HeaderStyle-Width="90px">
                            <ItemTemplate>
                                <asp:Button ID="Button1" CssClass="btn btn-info btn-block" runat="server" Text='<%#Eval("lugares") %>' CommandName="Lugares" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="lugares_coupado" ItemStyle-CssClass="btn btn-danger btn-block" HeaderText="Lugares Ocupados" Visible="false"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>    
    <div class="modal fade modal-info" id="myModalImg" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gridareas" EventName="RowCommand" />
                </Triggers>
                <ContentTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
           
        </div>
    </div>
    <div id="myModalArea" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="aceptar" />
                    <asp:PostBackTrigger ControlID="Button3" />
                    <asp:AsyncPostBackTrigger ControlID="gridareas" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="lnk" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header" style="text-align:center ;background-color: #428bca; color: white">
                            <h4><strong id="confirmTituloa" class="modal-title"></strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="text-align: left;">
                                <div class="col-lg-12">
                                    <h5><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre</h5>
                                    <asp:TextBox ID="txtnombre" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" CssClass="form-control" placeholder="Nombre del Area" runat="server"></asp:TextBox>
                                    <h5><i class="fa fa-university" aria-hidden="true"></i>&nbsp;Seleccione una Sucursal</h5>
                                    <asp:DropDownList ID="ddldeptos" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <h5><i class="fa fa-file-image-o" aria-hidden="true"></i>&nbsp;Imagen del Area</h5>
                                    <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                                    <br />
                                    <div class="alert alert-info fade in">
                                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                        <strong>NOTA:</strong>&nbsp;Imagen debe ser JPG
                                    </div>
                                    <div class="alert alert-danger fade in" id="error" runat="server" visible="false">
                                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                        <strong>Error:</strong>&nbsp;<asp:Label ID="lblmensajeerror" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="aceptar" class="btn btn-primary btn-block" runat="server" Text="Guardar" OnClick="aceptar_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">

                                    <asp:Button ID="Button3" CssClass="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="Button3_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="text-align: center">
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
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-lg-6 col-xs-6">
                            <asp:Button ID="Button2" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                        </div>
                        <div class="col-lg-6 col-xs-6">
                            <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>