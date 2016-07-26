<%@ Page Title="Asignacion" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="asignacion_lugares.aspx.cs" Inherits="presentacion.asignacion_lugares" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <h1 class="page-header">Aisgnacion de Lugares de Trabajo</h1>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlareas" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-2" style="align-content: center;">
                            <a>
                                <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 160px; margin: 0 auto;" />
                            </a>
                        </div>
                        <div class="col-lg-10" style="text-align: left">
                            <div class="form-group">
                                <h4>
                                    <strong>Nombre Empleado: </strong>
                                    <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                                </h4>
                                <h4><strong>Puesto: </strong>
                                    <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                                </h4>
                                <h4><strong>Departamento: </strong>
                                    <asp:Label ID="lbldepto" runat="server" Text=""></asp:Label>
                                </h4>
                                <h4><strong>
                                    <asp:Label ID="lblobsr" Visible="false" runat="server" Text=""></asp:Label></strong>
                                </h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <h5><i class="fa fa-university" aria-hidden="true"></i>&nbsp;Seleccione una Sucursal</h5>
                            <asp:DropDownList ID="ddldeptos" OnSelectedIndexChanged="ddldeptos_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <h5><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Seleccione un Area</h5>
                            <asp:DropDownList ID="ddlareas" OnSelectedIndexChanged="ddlareas_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divimgarea" visible="false">
                        <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: center;">
                            <h4><i class="fa fa-user" aria-hidden="true"></i>&nbsp;<asp:Label ID="lblareaname" runat="server" Text=""></asp:Label></h4>
                            <a id="example2" runat="server">
                                <asp:Image ID="imgarea" CssClass="image img-responsive" runat="server" />
                            </a>
                            <asp:LinkButton ID="lnkagrgar" CssClass="btn btn-info btn-block" OnClick="lnkagrgar_Click" runat="server">Agregar Relación con este Lugar <i class="fa fa-floppy-o" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: center; background-color: white;">
                            <h4><i class="ion ion-briefcase"></i>&nbsp;Lugares</h4>
                            <div class="row">
                                <asp:Repeater ID="repeater_puestos" runat="server">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="up_repeat" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="btnLugar" ToolTip='<%#Eval("nombre")%>' Text='<%#Eval("ALIAS")%>' CssClass="btn btn-default btn-block" OnClick="btnLugar_Click" runat="server" CommandName='<%#Eval("idc_lugart")%>' CommandArgument='<%#Eval("lugar")%>'>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="label label-warning">LUGAR OCUPADO</label>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="label label-default">LUGAR DISPONIBLE</label>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="label label-success">LUGAR SELECCIONADO</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <h4><strong><i class="fa fa-refresh" aria-hidden="true"></i>&nbsp;Relacion de este Puesto con Otros Lugares</strong></h4>
                            <div class="table table-responsive">
                                <asp:GridView ID="grid_puestpos" DataKeyNames="idc_lugart,nombre,area,idc_area,idc_sucursal" CssClass="table table-responsive table-bordered table-hover" OnRowCommand="gridlugares_RowCommand" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/more.png" HeaderText="Ver" CommandName="Ver">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="idc_lugart" HeaderText="idc" Visible="false"></asp:BoundField>
                                        <asp:BoundField DataField="idc_area" HeaderText="idc" Visible="false"></asp:BoundField>
                                        <asp:BoundField DataField="idc_sucursal" HeaderText="idc" Visible="false"></asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Lugar De Trabajo"></asp:BoundField>
                                        <asp:BoundField DataField="alias" HeaderText="Alias"></asp:BoundField>
                                        <asp:BoundField DataField="area" HeaderText="Area"></asp:BoundField>
                                        <asp:BoundField DataField="Sucursal" HeaderText="Sucursal"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
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
            <!-- Modal -->
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
        </div>
    </div>
</asp:Content>