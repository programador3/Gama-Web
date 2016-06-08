<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos_detalle.aspx.cs" Inherits="presentacion.cursos_detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="page-header">

            <div class="container">

                <h1>
                    <asp:LinkButton ID="lnkReturn" runat="server" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>

                    Detalle</h1>
                <asp:HyperLink ID="linkpendientes" NavigateUrl="~/cursos_pendiente.aspx" runat="server">(Regresar a pendientes)</asp:HyperLink>
                <asp:HiddenField ID="oc_paginaprevia" runat="server" />
            </div>
        </div>
        <div class="container">
            <div class="row">
                <asp:Panel ID="panel_prod_encabezado" CssClass="col-lg-6" runat="server">
                    <!-- produccion encabezado -->
                    <h3>
                        <asp:Label ID="lblmensaje" CssClass="label label-success" runat="server" Text="Producción"></asp:Label></h3>
                </asp:Panel>
                <asp:Panel ID="panel_borr_encabezado" CssClass="col-lg-6" runat="server">
                    <!-- borrador encabezado -->
                    <h3>
                        <asp:Label ID="lblmensaje_borr" CssClass=" label label-info" runat="server" Text="Borrador"></asp:Label></h3>
                    <!-- solo debe verlo aquel que tiene permisos -->
                    <asp:ImageButton ID="btneditarborrador" runat="server" ImageUrl="~/imagenes/btn/icon_editar.png" OnClick="btneditarborrador_Click" />
                </asp:Panel>
            </div>
            <div class="row">
                <asp:Panel ID="panel_prod_cursos" CssClass="col-lg-6" runat="server">
                    <!-- produccion cursos -->
                    <div class="row">
                        <div class="panel panel-success">
                            <div class="panel-heading">Cursos</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="etiqueta">Descripción</label>
                                            <asp:Label ID="lbldesc" runat="server" CssClass="form-control" Text=""></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label class="etiqueta">Tipo Curso</label>
                                            <asp:Label ID="lbltipocurso" runat="server" CssClass="form-control" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--panel body -->
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_borr_cursos" CssClass="col-lg-6" runat="server">
                    <!-- borrador cursos -->
                    <div class="row">
                        <div class="panel panel-info">
                            <div class="panel-heading">Cursos</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="etiqueta">Descripción</label>
                                            <asp:Label ID="lbldesc_borr" runat="server" CssClass="form-control" Text=""></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label class="etiqueta">Tipo Curso</label>
                                            <asp:Label ID="lbltipocurso_borr" CssClass="form-control" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--panel body -->
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="row">
                <asp:Panel ID="panel_prod_perfiles" CssClass="col-lg-6" runat="server">
                    <!-- produccion perfiles -->
                    <div class="row">
                        <div class="panel panel-success">
                            <div class="panel-heading">Perfiles</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:BulletedList ID="listaperfiles" runat="server"></asp:BulletedList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--panel body -->
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="panel_borr_perfiles" CssClass="col-lg-6" runat="server">
                    <!-- borrador perfiles -->
                    <div class="row">
                        <div class="panel panel-info">
                            <div class="panel-heading">Perfiles</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:BulletedList ID="listaperfiles_borr" runat="server"></asp:BulletedList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--panel body -->
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="row">
                <asp:Panel ID="panel_prod_archivos" CssClass="col-lg-6" runat="server">
                    <!-- produccion archivos -->
                    <div class="row">
                        <div class="panel panel-success">
                            <div class="panel-heading">Archivos</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Repeater ID="repit_archivos" runat="server" OnItemDataBound="repit_archivos_ItemDataBound" OnItemCommand="repit_archivos_ItemCommand">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <asp:Label ID="lbldesc_archivo" runat="server" Text=""></asp:Label>
                                                    <asp:ImageButton ID="imgbtn_linkdescarga" runat="server" ImageUrl="~/imagenes/btn/icon_descargar.png" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                            <!--panel body -->
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="panel_borr_archivos" CssClass="col-lg-6" runat="server">
                    <!-- borrador archivos -->
                    <div class="row">
                        <div class="panel panel-info">
                            <div class="panel-heading">Archivos</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Repeater ID="repit_archivos_borr" runat="server" OnItemCommand="repit_archivos_borr_ItemCommand" OnItemDataBound="repit_archivos_borr_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <asp:Label ID="lbldesc_archivo_borr" runat="server" Text=""></asp:Label>
                                                    <asp:ImageButton ID="imgbtn_linkdescarga_borr" runat="server" ImageUrl="~/imagenes/btn/icon_descargar.png" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                            <!--panel body -->
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="row">
                <asp:Panel ID="panel_prod_obs" CssClass="col-lg-6" runat="server">
                    <!--produccion -->
                </asp:Panel>

                <asp:Panel ID="panel_borr_obs" CssClass="col-lg-6" runat="server">
                    <!--borrador -->
                    <div class="row">
                        <div class="panel panel-info">
                            <div class="panel-heading">Observaciones</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="etiqueta">Observaciones</label>
                                            <asp:Label ID="lblobservaciones_borr" runat="server" CssClass="form-control" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--panel body -->
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>