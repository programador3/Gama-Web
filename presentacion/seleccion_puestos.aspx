<%@ Page Title="Seleccion" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="seleccion_puestos.aspx.cs" Inherits="presentacion.seleccion_puestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/PanelsLTE.css" rel="stylesheet" />
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
                        <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Pendientes por Seleccionar</h1>
                </div>
            </div>
            <div class="row">
                <h2 id="NoPende" style="text-align: center;" runat="server">No hay Pendientes <i class="fa fa-exclamation-triangle"></i></h2>

                <asp:Panel ID="PanelPendientes" runat="server">
                    <asp:Repeater ID="repeatpendientes" runat="server" OnItemDataBound="repeatpendientes_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 col-sm-6">
                                <asp:Panel ID="PanelRevisionP" runat="server" class="small-box bg-green">
                                    <div class="inner">
                                        <h4>Reclutar Candidatos
                                        </h4>
                                        <p>
                                            <h6>
                                                <asp:Label ID="lbl" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                            </h6>
                                        </p>
                                    </div>
                                    <div class="icon">
                                        <asp:LinkButton ID="lnkGOdET" OnClick="lnkGO_Click" Style="color: white;" runat="server"><i class="ion ion-arrow-right-c"></i></asp:LinkButton>
                                    </div>
                                    <asp:LinkButton ID="lnkGO" OnClick="lnkGO_Click" runat="server" CssClass="small-box-footer">Comenzar Seelección <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                                </asp:Panel>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>