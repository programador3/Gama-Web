<%@ Page Title="Preparaciones" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="preparaciones.aspx.cs" Inherits="presentacion.preparaciones" %>

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
                        Pendientes por Preparar</h1>
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
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label></h6>
                                        </p>
                                    </div>
                                    <div class="icon">
                                        <asp:LinkButton ID="lnkIcon" runat="server" Style="color: white;" OnClick="lnkIcon_Click"></asp:LinkButton>
                                    </div>
                                    <asp:LinkButton ID="lnkGO" runat="server" CssClass="small-box-footer" OnClick="lnkIcon_Click">Comenzar Preparación <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                                </asp:Panel>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>