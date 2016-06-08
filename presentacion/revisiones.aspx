<%@ Page Title="Pendientes" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="revisiones.aspx.cs" Inherits="presentacion.revisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/PanelsLTE.css" rel="stylesheet" />
    <link href="css/ionicons.css" rel="stylesheet" />
    <link href="css/ionicons.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Pendientes por Revisión</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div class="form-group">
                        <div class="btn-group">
                            <asp:LinkButton runat="server" class="btn btn-primary" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                           Guardar Reporte de Pendientes <i class="fa fa-floppy-o"></i>
                            </asp:LinkButton>
                            <ul class="dropdown-menu">
                                <li><a href="#">
                                    <asp:LinkButton ID="lnkGuardarTodo" runat="server" OnClick="lnkGuardarTodo_Click">
                                        <asp:Image ID="Image2" runat="server" Height="25" Width="25" ImageUrl="~/imagenes/btn/excel.png" />
                                        Excel 2007
                                    </asp:LinkButton></a></li>
                                <li><a href="#">
                                    <asp:LinkButton ID="lnkPDF" runat="server" OnClick="lnkPDF_Click">
                                        <asp:Image ID="Image1" runat="server" Height="25" Width="25" ImageUrl="~/imagenes/btn/pdf.png" />
                                        PDF
                                    </asp:LinkButton></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5 col-md-6">
                    <div class="btn-group">
                        <div class="form-group">
                            <asp:LinkButton ID="lnkW8" runat="server" class="btn btn-primary active" OnClick="lnkW8_Click">Modo Paneles <i class="fa fa-th"></i></asp:LinkButton>

                            <asp:LinkButton ID="lnklista" runat="server" class="btn btn-link" OnClick="lnklista_Click">Modo Lista <i class="fa fa-list"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <h2 id="NoPende" style="text-align: center;" runat="server" visible="false">No hay Revisiones Pendientes <i class="fa fa-exclamation-triangle"></i></h2>

                <asp:Panel ID="PanelModoW8" runat="server">
                    <asp:Repeater ID="repeatpendientes" runat="server" OnItemDataBound="repeatpendientes_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 col-sm-6">
                                <asp:Panel ID="PanelRevisionP" runat="server" class="small-box bg-green">
                                    <div class="inner">
                                        <h4>
                                            <asp:Label ID="lblTipoRev" runat="server" Text=""></asp:Label>
                                        </h4>
                                        <p>
                                            <h6>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("empleado") %>'></asp:Label></h6>
                                        </p>
                                    </div>
                                    <div class="icon">
                                        <asp:LinkButton ID="lnkIcon" runat="server" Style="color: white;" OnClick="lnkIcon_Click"></asp:LinkButton>
                                    </div>
                                    <asp:LinkButton ID="lnkCelulares" runat="server" CssClass="small-box-footer " OnClick="LnkGOCelulare_Click" Visible="false">Ir a Revisión <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkVehiculos" runat="server" CssClass="small-box-footer" OnClick="lnkVehiculos_Click" Visible="false">Ir a Revisión <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkGoServicios" runat="server" CssClass="small-box-footer" OnClick="lnkGoServicios_Click" Visible="false">Ir a Revisión <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkFinal" runat="server" CssClass="small-box-footer" OnClick="lnkFinal_Click" Visible="false">Ir a Revisión <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>

                                    <asp:LinkButton ID="lnkGO" runat="server" CssClass="small-box-footer" OnClick="lnkGO_Click">Ir a Revisión <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                                </asp:Panel>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
            <asp:Panel ID="PanelModoLista" runat="server" Visible="false">
                <div class="row">
                    <div class="col-lg-8">
                        <div class="form-group">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa-clock-o fa-fw"></i>Revisiones Pendientes</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="list-group">
                                        <asp:Repeater ID="RepeatMOdoLista" runat="server" OnItemDataBound="RepeatMOdoLista_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkGORev" runat="server" class="list-group-item" OnClick="lnkGORev_Click">
                                                    <span class="badge"><%#Eval("tipo_revision") %></span>
                                                    <asp:Label ID="lblTipeIconRev" runat="server" Text="<i class='fa fa-user'></i> "></asp:Label>
                                                    <strong><%#Eval("empleado") %></strong>
                                                    &nbsp; &nbsp;<%#Eval("descripcion") %>< />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>