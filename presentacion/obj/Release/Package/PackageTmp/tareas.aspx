<%@ Page Title="Mis Tareas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas.aspx.cs" Inherits="presentacion.tareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#mis_pendientes').is(':hidden')) {
                $("#mis_pendientes").hide(1000);
            } else {
                $("#mis_pendientes").show(1000);
            }
            if ($('#mis_asignadas').is(':hidden')) {
                $("#mis_asignadas").hide(1000);
            } else {
                $("#mis_asignadas").show(1000);
            }
        });
        function MostrarMisP() {
            if ($('#mis_pendientes').is(':hidden')) {
                $("#mis_pendientes").show(1000);
            } else {
                $("#mis_pendientes").hide(1000);
            }
        }
        function MostrarMisA() {
            if ($('#mis_asignadas').is(':hidden')) {
                $("#mis_asignadas").show(1000);
            } else {
                $("#mis_asignadas").hide(1000);
            }
        }
    </script>
     <style type="text/css">
         .h5, h5 {
                font-size: 11px;
            }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Mis Tareas</h1>
    <div class="row">
        <div class="col-lg-12">
            <asp:LinkButton ID="lnkGO" runat="server" CssClass="btn btn-info btn-block" PostBackUrl="~/tareas_captura.aspx">Crear Nueva Tarea  <i class="fa fa-plus-circle"></i></asp:LinkButton>
        </div>
    </div>
    <asp:UpdatePanel ID="jeje" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkreturn_mias" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkregresa_mias" EventName="Click" />
            <asp:PostBackTrigger ControlID="LinkButton1" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12">
                    <div class="card">
                        <div class="card-header" style="background-color: #1ABC9C; color: white;">
                            <div class="card-title" style="background-color: #1ABC9C; color: white;">
                                <div class="title" style="text-align: center; background-color: #1ABC9C; color: white;">
                                    <h4 style="text-align: center;"><i class="fa fa-wrench"></i>&nbsp;Mis Tareas Pendientes
                                        <span>
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server" OnClick="LinkButton1_Click">Excel</asp:LinkButton>
                                        </span>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="card-body" id="mis_pendientes">
                            <asp:Panel ID="panel_deptos_mias" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h4 id="H4" runat="server" style="text-align: center;">Departamentos</h4>
                                        <asp:Repeater ID="repeat_deptos_mias" runat="server">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkdeptos_mias" EventName="Click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                            <asp:LinkButton ID="lnkdeptos_mias" CssClass="btn btn-primary btn-block" runat="server" CommandName='<%#Eval("idc_depto") %>' OnClick="lnkdeptos_mias_Click">
                                                 <h5><%#Eval("nombre") %></h5>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panel_puestos_mias" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:LinkButton ID="lnkreturn_mias" runat="server" CssClass="btn btn-info btn-block" OnClick="lnkreturn_mias_Click"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i> &nbsp;Regresar a Departamentos</asp:LinkButton>
                                        <h4 id="H5" runat="server" style="text-align: center;">Empleados</h4>
                                        <asp:Repeater ID="repeat_puestos_mias" runat="server">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel212" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkpuestos_mias" EventName="Click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                            <asp:LinkButton ID="lnkpuestos_mias" CssClass="btn btn-primary btn-block" runat="server" CommandName='<%#Eval("idc_puesto") %>' OnClick="lnkpuestos_mias_Click">
                                                <h5><%#Eval("puesto") %></h5>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panel_detalles_mias" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:LinkButton ID="lnkregresa_mias" runat="server" CssClass="btn btn-info btn-block" OnClick="lnkregresa_mias_Click"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i>&nbsp;Regresar a Empleados</asp:LinkButton>

                                        <asp:Repeater ID="repeat_pendientes" OnItemDataBound="repeat_pendientes_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <h5 id="des" runat="server" style="text-align: left;"><%#Eval("descripcion") %></h5>
                                                        <asp:Repeater ID="repeat_mis_tareas" runat="server">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkmistarea" EventName="Click" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                                            <asp:LinkButton ID="lnkmistarea" CssClass='<%#Eval("cssclass") %>' runat="server" CommandName='<%#Eval("idc_tarea") %>' OnClick="lnkmistarea_Click1" ToolTip='<%#Eval("desc_completa") %>'>

                                                            <h5>"<%#Eval("descripcion") %>"</h5>
                                                            <h5>FI: <%#Eval("fecha_inicio") %> </h5>
                                                            <h5>FC: <%#Eval("fecha_compromiso") %> </h5>
                                                             <h5><%#Eval("puesto") %></h5>
                                                             <h5>Avance: <%#Eval("avance") %> %</h5>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="e" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="nlkreturn" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkregresa" EventName="Click" />
            <asp:PostBackTrigger ControlID="LinkButton2" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12">
                    <div class="card">
                        <div class="card-header" style="background-color: #1ABC9C; color: white;">
                            <div class="card-title" style="background-color: #1ABC9C; color: white;">
                                <div class="title" style="background-color: #1ABC9C; color: white;">
                                    <h4 style="text-align: center;"><i class="fa fa-wrench"></i>&nbsp;Tareas que yo Asigne  <span>
                                            <asp:LinkButton ID="LinkButton2" CssClass="btn btn-primary" runat="server" OnClick="LinkButton2_Click">Excel</asp:LinkButton>
                                        </span></h4>
                                </div>
                            </div>
                        </div>
                        <div class="card-body" id="mis_asignadas">
                            <asp:Panel ID="panel_deptos" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h4 id="H1" runat="server" style="text-align: center;">Departamentos</h4>
                                        <asp:Repeater ID="repeat_deptos" runat="server">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkdeptos" EventName="Click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                            <asp:LinkButton ID="lnkdeptos" CssClass="btn btn-primary btn-block" runat="server" CommandName='<%#Eval("idc_depto") %>' OnClick="lnkdeptos_Click">
                                    <h5><%#Eval("nombre") %></h5>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panel_puestos" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:LinkButton ID="nlkreturn" runat="server" CssClass="btn btn-info btn-block" OnClick="nlkreturn_Click"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i> &nbsp;Regresar a Departamentos</asp:LinkButton>
                                        <h4 id="H2" runat="server" style="text-align: center;">Empleados</h4>
                                        <asp:Repeater ID="repeat_puestos" runat="server">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkpuestos" EventName="Click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                            <asp:LinkButton ID="lnkpuestos" CssClass="btn btn-primary btn-block" runat="server" CommandName='<%#Eval("idc_puesto") %>' OnClick="lnkpuestos_Click">
                                    <h5><%#Eval("puesto") %></h5>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panel_detalles" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:LinkButton ID="lnkregresa" runat="server" CssClass="btn btn-info btn-block" OnClick="lnkregresa_Click"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i>&nbsp;Regresar a Empleados</asp:LinkButton>
                                        <asp:Repeater ID="repeat_asignadas" OnItemDataBound="repeat_asignadas_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <h5 id="descri" runat="server" style="text-align: left;"><%#Eval("descripcion") %></h5>
                                                        <asp:Repeater ID="repeat_mis_tareas_asignadas" runat="server">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkmistarea_asig" EventName="Click" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                                            <asp:LinkButton ID="lnkmistarea_asig" CssClass='<%#Eval("cssclass") %>' runat="server" CommandName='<%#Eval("idc_tarea") %>' OnClick="lnkmistarea_Click" ToolTip='<%#Eval("desc_completa") %>'>
                                    <h5>"<%#Eval("descripcion") %>"</h5>
                                                            <h5>FI: <%#Eval("fecha_inicio") %> </h5>
                                     <h5>FC: <%#Eval("fecha_compromiso") %></h5>
                                     <h5><%#Eval("puesto") %></h5>
                                     <h5>Avance: <%#Eval("avance") %> %</h5>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

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
                        <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>