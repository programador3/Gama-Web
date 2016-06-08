<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="etiquetas_bloq.aspx.cs" Inherits="presentacion.etiquetas_bloq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">

        <div class="container-fluid">

            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Etiquetas bloqueo
                    </h1>
                </div>
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-md-8">

                    <h4>La Opción
                                    <asp:DropDownList ID="cboxopcionesetiq" runat="server" class="btn btn-default dropdown-toggle">
                                        <asp:ListItem Value="select">Seleccione</asp:ListItem>
                                    </asp:DropDownList>
                        bloquea a la Etiqueta
                                    <asp:DropDownList ID="cboxetiq" runat="server" AutoPostBack="false" class="btn btn-default dropdown-toggle" Width="133px">
                                        <asp:ListItem Value="select">Seleccione</asp:ListItem>
                                    </asp:DropDownList>
                        <asp:Button ID="btnaddbolqueo" runat="server" Text="Agregar" CssClass="btn btn-info" OnClick="btnaddbolqueo_Click" />
                        <%--<span>       <asp:Button ID="Button3" class="btn btn-info btn-xs" runat="server" Text="Info" OnClick="Button3_Click"  ToolTip="Click Para obtener Información Acerca de esto" />
                                </span>--%>
                    </h4>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                    <div class="table-responsive">

                        <asp:GridView ID="grid_etiquetas_bloq" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="False" DataKeyNames="idc_perfiletiq_bloq" OnRowCommand="grid_etiquetas_bloq_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle Width="40px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            Text="" ImageUrl="~/imagenes/btn/icon_delete.png" CommandName="eliminar" OnClientClick="return confirm('¿Desea eliminar este bloqueo?')"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="idc_perfiletiq_bloq" HeaderText="Id" />
                                <asp:BoundField DataField="opcion" HeaderText="Bloquea a" />
                                <asp:BoundField DataField="etiqueta" HeaderText="Etiqueta" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <asp:HiddenField ID="ocidc_perfiletiq" runat="server" />
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </div>
    <!-- /#page-wrapper -->
</asp:Content>