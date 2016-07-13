<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="procesos_autorizar.aspx.cs" Inherits="presentacion.procesos_autorizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[10, 25, -1], [10, 25, "All"]] //value:item pair
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

        //21-10-2015
        $(function () {
            //codigo aquí
            //valor del campo oculto
            var panel_coments = "<%= panel_comentarios.Visible%>";
            if (panel_coments == "True") {
                //no valides observaciones
                $('#<%= Yes.ClientID%>').prop('disabled', true);
            }
        })
        function campoVacio() {
            var panel_coments = "<%= panel_comentarios.Visible%>";
            if (panel_coments == "True") { //esta habilitado el panel
                var coments = $('#<%= txtcoments.ClientID%>').val().replace(/\s+/g, '');
                if (coments.length < 1) {
                    //inhabilita el boton aceptar
                    $('#<%= Yes.ClientID%>').prop('disabled', true);

                } else {
                    $('#<%= Yes.ClientID%>').prop('disabled', false);
                }
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Manuales de Procesos pendientes por aprobar
            </h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading" style="text-align: center">
                    <h3 class="panel-title">Listado de perfiles por aprobar<i class="fa fa-list"></i></h3>
                </div>
                <div class="panel-body">
                    <div class="table table-responsive">
                        <asp:GridView ID="grid_perfilpendiente" runat="server" CssClass="gvv table table-bordered table-condensed table-hover grid sortable" AutoGenerateColumns="False" DataKeyNames="idc_registro" OnRowCommand="grid_perfilpendiente_RowCommand" OnRowDataBound="grid_perfilpendiente_RowDataBound">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" HeaderText="Comparar" ImageUrl="~/imagenes/btn/icon_comparar.png" Text="Comparar" CommandName="comparar" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:TemplateField HeaderText="Aprobación" ShowHeader="False" HeaderStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Button ID="btnaprobar" CssClass="btn btn-default btn-block" runat="server" CausesValidation="false" CommandName="aprobar" Text="Aprobación" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Regresar" ShowHeader="False" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtn_regresar" runat="server" CausesValidation="false" CommandName="regresar" ImageUrl="~/imagenes/btn/icon_return_borr.png" Text="Regresar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cancelar" ShowHeader="False" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtncancelar" runat="server" CausesValidation="false" CommandName="cancelar" ImageUrl="~/imagenes/btn/icon_borrar.png" Text="" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proceso">
                                    <ItemTemplate>
                                        <asp:Button ID="btnproce" CommandName="preview" CssClass="btn btn-default btn-block" runat="server" Text='<%#Eval("descripcion") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="puesto" HeaderStyle-Width="220px" HeaderText="Solicitante">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
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
                    <!-- comentarios cuando vaya a cancelar -->
                    <asp:Panel ID="panel_comentarios" runat="server" class="row">

                        <div class="col-lg-12">
                            <label>Comentarios</label>
                            <asp:TextBox ID="txtcoments" runat="server" CssClass="form-control" TextMode="MultiLine" onblur="campoVacio();"></asp:TextBox>
                        </div>
                    </asp:Panel>
                    <div class="row" id="autori" runat="server" viis="false">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lnktodo" CssClass="btn btn-default btn-block" OnClick="lnktodo_Click" runat="server">Mostrar Toda la Información</asp:LinkButton>
                                </div>
                                <asp:Repeater ID="Repeater1" runat="server">
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
                                <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="No" class="btn btn-danger btn-block" runat="server" Text="Cancelar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- modal numero 2 -->
    <div id="Div1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" style="text-align: center">
                <div class="modal-header" style="background-color: #428bca; color: white">
                    <h4><strong id="confirmTitulo_2" class="modal-title"></strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4>
                                <label id="Label1"></label>
                            </h4>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button1" class="btn btn-success" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                    <asp:Button ID="Button2" class="btn btn-danger" runat="server" Text="Cancelar" />
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>