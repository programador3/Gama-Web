<%@ Page Title="Registro de Visitas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="registro_visitas.aspx.cs" Inherits="presentacion.registro_visitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#myModalPersonas').modal('hide');
            $('#myModalempresa').modal('hide');
        }
        function ModalConfirmPero(cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalPersonas').modal('show');
            $('#myModalPersonas').removeClass('modal fade modal-info');
            $('#myModalPersonas').addClass(ctype);
            $('#content_modals').text(cContenido);
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

        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Registro de Visitas</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkpersonas" />
            <asp:PostBackTrigger ControlID="lnkempresa" />
            <asp:PostBackTrigger ControlID="ddlPuestoAsigna" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre del Visitante</strong></h4>
                    <asp:TextBox onfocus="$(this).select();" onblur="return imposeMaxLength(this, 8000);" placeholder="Visitante" ID="txtnombre" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtnombre_TextChanged"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <h4><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Empresa</strong></h4>
                    <asp:TextBox onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" placeholder="Empresa" ID="txtempresa" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtempresa_TextChanged"></asp:TextBox>
                </div>
            </div>
            <div class="row" runat="server" id="FILTRO">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <h4><strong><i class="fa fa-child" aria-hidden="true"></i>&nbsp;Empleado de GAMA con quien tendra visita</strong></h4>
                    <asp:DropDownList ID="ddlPuestoAsigna" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPuestoAsigna_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-8 col-xs-8">
                    <h4>Escriba un Filtro</h4>
                    <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Empleado"></asp:TextBox>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                    <h4>.</h4>
                    <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                </div>
                <div class="col-lg-12">
                    <asp:TextBox onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" placeholder="Motivo" TextMode="MultiLine" Rows="3" ID="txtmotivo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Visita" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:Button ID="btnCancelar" runat="server" Text="Limpiar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                </div>
            </div>

            <!--MODALES-->
            <div class="modal fade modal-info" id="myModalPersonas" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <h4 style="text-align: center;">
                                <label id="content_modals"></label>
                            </h4>
                            <div class="row" style="overflow-y: scroll; height: 150px;">
                                <asp:Repeater ID="repeatpersona" runat="server">
                                    <ItemTemplate>
                                        <div class="col-lg-12 col-sm-12">
                                            <asp:LinkButton ID="lnkpersona" OnClick="lnkpersona_Click" CssClass="btn btn-primary btn-block" runat="server" CommandName='<%#Eval("idc_visitap") %>' CommandArgument='<%#Eval("nombre") %>'>
                                                <%#Eval("nombre") %>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <br />
                            <asp:LinkButton ID="lnkpersonas" OnClick="lnkpersonas_Click" CssClass="btn btn-success btn-block" runat="server">
                                    Agregar Como Nuevo
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade modal-info" id="myModalempresa" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <h4 style="text-align: center;">
                                <label id="content_modalempresa"></label>
                            </h4>
                            <div class="row" style="overflow-y: scroll; height: 150px;">
                                <asp:Repeater ID="repeatempresa" runat="server">
                                    <ItemTemplate>
                                        <div class="col-lg-12 col-sm-12">
                                            <asp:LinkButton ID="lnkemp" OnClick="lnkemp_Click" CssClass="btn btn-primary btn-block" runat="server" CommandName='<%#Eval("idc_visitaemp") %>' CommandArgument='<%#Eval("nombre") %>'>
                                                <%#Eval("nombre") %>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <br />
                            <asp:LinkButton ID="lnkempresa" OnClick="lnkempresa_Click" CssClass="btn btn-success btn-block" runat="server">
                                    Agregar Como Nueva
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-indent" aria-hidden="true"></i>&nbsp;Visitas Actuales en curso
                        <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label></strong></h4>
            <div class="table-responsive">
                <asp:GridView ID="gridvisitas" CssClass="table table-responsive table-condensed gvv" DataKeyNames="idc_visitareg" OnRowCommand="gridvisitas_RowCommand" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Visita" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Button ID="Button1" CssClass="btn btn-danger btn-block" runat="server" Text="Terminar" CommandName="Terminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="idc_visitareg" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="visitante" HeaderStyle-Width="110px" HeaderText="Visitante"></asp:BoundField>
                        <asp:BoundField DataField="empresa" HeaderStyle-Width="110px" HeaderText="Empresa"></asp:BoundField>
                        <asp:BoundField DataField="motivo" HeaderStyle-Width="200px" HeaderText="Motivo"></asp:BoundField>
                        <asp:BoundField DataField="empleado" HeaderStyle-Width="200px" HeaderText="Empleado"></asp:BoundField>
                        <asp:BoundField DataField="fecha_ingreso" HeaderStyle-Width="80px" HeaderText="Ingreso"></asp:BoundField>
                        <asp:BoundField DataField="fecha_salida" HeaderStyle-Width="80px" HeaderText="Salida"></asp:BoundField>
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