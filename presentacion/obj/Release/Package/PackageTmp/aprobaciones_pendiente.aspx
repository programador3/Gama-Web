<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="aprobaciones_pendiente.aspx.cs" Inherits="presentacion.aprobaciones_pendiente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
    <script type="text/javascript">
        function Return(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ReturnGr(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalGr').modal('show');
            $('#confirmTituloGr').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function modal_detalle() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal_detalle').modal('show');
        }
        function modal_comentarios() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal_comentarios').modal('show');
        }
        function close_comentarios() {
            $('#myModal_comentarios').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">

            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Aprobaciones Pendientes</h1>
                </div>
            </div>
            <%--<div class="row">
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <asp:LinkButton ID="btnNew" runat="server" CssClass="btn btn-success btn-block" OnClick="btnNew_Click">Crear Nueva Aprobación <i class="fa fa-plus"></i></asp:LinkButton>
                </div>
            </div>--%>
            <br />
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title"><i class="fa fa-list"></i>&nbsp;Aprobaciones pendientes</h3>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <h1 style="text-align: center;">
                                    <asp:Label ID="lblTablaVacia" runat="server" Text="No hay Aprobaciones Pendientes<i class='fa fa-file-o'></i>" Visible="false"></asp:Label>
                                </h1>
                                <asp:GridView ID="gridaprobacionespendientes" runat="server" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [4]}" DataKeyNames="nombre,nombre_soli, des_puesto,comentarios, descorta, idc_aprobacion_reg, pagina, fecha_movimiento,idc_registro,idc_aprobacion" OnRowCommand="gridaprobacionespendientes_RowCommand" OnRowDataBound="gridaprobacionespendientes_RowDataBound" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Firma grupal" ShowHeader="False" HeaderStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:Button ID="btnfirmagrupal" runat="server" CausesValidation="false" CommandName="" Text="IR" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ver Detalle" ShowHeader="False" Visible="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="detalle" ImageUrl="~/imagenes/btn/icon_buscar.png" Text="ver detalle" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idc_aprobacion_soli" HeaderText="ID Solicitud" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:ButtonField DataTextField="nombre" HeaderText="Aprobación" CommandName="Vista" />
                                        <asp:BoundField DataField="des_puesto" HeaderText="Firma Requerida" Visible="true" HeaderStyle-Width="220px" />
                                        <%-- <asp:BoundField DataField="pagina" HeaderText="pagina" Visible="false" />--%>

                                        <asp:TemplateField ShowHeader="False" HeaderText="Aprobar" Visible="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnaprobar" runat="server" CausesValidation="false" CommandName="aprobar" ImageUrl="~/imagenes/btn/icon_autorizar.png" Text="Aprobar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="No Aprobar" Visible="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtn_noaprobar" runat="server" CausesValidation="false" CommandName="no_aprobar" ImageUrl="~/imagenes/btn/icon_borrar.png" Text="No aprobar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="comentarios" HeaderText="Comentarios" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descorta" HeaderText="Descripción" Visible="false" />
                                        <asp:BoundField DataField="pagina" HeaderText="pagina" Visible="false" />
                                        <asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Solicitud" Visible="true" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="fecha_movimiento" HeaderText="pagina" Visible="false" />
                                        <asp:TemplateField HeaderText="Estatus" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:Button ID="lblestatus" runat="server" CausesValidation="false" CommandName="Comentarios" Text="" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nombre_soli" HeaderText="pagina" Visible="false" />
                                        <asp:BoundField DataField="idc_registro" HeaderText="pagina" Visible="false" />
                                        <asp:BoundField DataField="idc_aprobacion" HeaderText="pagina" Visible="false" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.CONFIRMA -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style="text-align: center">
                    <div class="modal-header" style="background-color: #428bca; color: white">
                        <h4><strong id="confirmTitulo" class="modal-title">Autorizar</strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                <!--vacio -->
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                <label>Password</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtpass" runat="server" CssClass="form-control" type="password"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/btn/icon_llave.png" />
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                <!--vacio -->
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <label>Observaciones</label>
                                <asp:TextBox ID="txtobs" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Yes" class="btn btn-success" runat="server" Text="Aceptar" OnClick="btnFirmar_Click" />
                        <asp:Button ID="No" class="btn btn-danger" runat="server" Text="Cancelar" OnClick="No_Click" />

                        <!-- campos ocultos -->
                        <asp:HiddenField ID="modal_ocaprobado" runat="server" />
                        <asp:HiddenField ID="modal_ocidc_aprobacion_reg" runat="server" Value="0" />
                        <asp:HiddenField ID="oc_idc_aprobacion_soli" runat="server" Value="0" />
                    </div>
                </div>
            </div>
        </div>

        <!-- segundo modal -->
        <div id="myModal_detalle" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style="text-align: center">
                    <div class="modal-header" style="background-color: #428bca; color: white">
                        <h4><strong class="modal-title">Detalle</strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <label id="mensaje"></label>
                        </div>
                        <div class="row">
                            <asp:Literal ID="ltlmensaje" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btncerrar" class="btn btn-danger btn_" runat="server" Text="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
        <div id="myModal_comentarios" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style="text-align: center">
                    <div class="modal-header" style="background-color: #428bca; color: white">
                        <h4><strong class="modal-title">
                            <asp:Label ID="lblNombreComentarios" runat="server" Text="Label"></asp:Label>
                        </strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <h4>Puesto que Solicito:
                                    <asp:Label ID="lblPuestoSolicito" runat="server" Text="Label"></asp:Label></h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <h4>Puesto que debe Aprobar:
                                    <asp:Label ID="lblPuestoComentarios" runat="server" Text="Label"></asp:Label></h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <h4>Ultimo Movimiento:
                                    <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label></h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtDescripcionComentarios" CssClass="form-control" Rows="2" TextMode="MultiLine" ReadOnly="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtAprobacionComentarios" Text="Sin Comentarios" CssClass="form-control" Rows="3" TextMode="MultiLine" ReadOnly="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input id="ok" class="btn btn-danger btn-block" value="Cerrar" onclick="close_comentarios();" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.row -->

    <div id="myModalGr" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" style="text-align: center">
                <div class="modal-header" style="background-color: #428bca; color: white">
                    <h4><strong id="confirmTituloGr" class="modal-title"></strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4>
                                <label id="confirmContenido"></label>
                            </h4>
                        </div>
                    </div>
                    <div class="row" id="autori" runat="server" viis="false">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="gridaprobacionespendientes" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lnktodo" CssClass="btn btn-default btn-block" OnClick="lnktodo_Click" runat="server"></asp:LinkButton>
                                </div>
                                <asp:Repeater ID="Repeater1" runat="server" Visible="false">
                                    <ItemTemplate>
                                        <div class="col-lg-6 col-md-6 col-sm-12">
                                            <asp:LinkButton ID="lnkgrupo" CommandName='<%#Eval("idc_perfilgpo") %>' ToolTip='<%#Eval("TOOLTIP") %>' CssClass="btn btn-default btn-block" OnClick="lnkgrupo_Click" runat="server"><%#Eval("grupo") %></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <asp:Repeater ID="REPEATPERFILPUESTO" runat="server" Visible="false">
                                    <ItemTemplate>
                                        <div class="col-lg-12">
                                            <asp:LinkButton ID="lnkgrupo" CommandName='<%#Eval("idc_registro") %>' CssClass="btn btn-default btn-block" OnClick="lnkgrupo_Click" runat="server"><%#Eval("puesto") %></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <asp:Repeater ID="Repeater3" runat="server" Visible="false">
                                    <ItemTemplate>
                                        <div class="col-lg-6 col-md-6 col-sm-12">
                                            <asp:LinkButton ID="lnkgrupo" CommandName='<%#Eval("idc_subproceso_BORR") %>' ToolTip='<%#Eval("TOOLTIP") %>' CssClass="btn btn-default btn-block" OnClick="lnkgrupo_Click" runat="server"><%#Eval("descripcion") %></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="Button1" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="Button1_Click" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="Button2" class="btn btn-danger btn-block" runat="server" Text="Cancelar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>