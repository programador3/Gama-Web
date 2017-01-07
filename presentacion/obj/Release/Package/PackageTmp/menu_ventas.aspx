<%@ Page Title="Menu Ventas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="menu_ventas.aspx.cs" Inherits="presentacion.menu_ventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">Menu Ventas
                <span>
                    <asp:LinkButton ID="lnkmenuventas" PostBackUrl="menu.aspx" CssClass="btn btn-info" runat="server">Menu Administrativo</asp:LinkButton></span>
            </h2>
        </div>
       </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:TextBox ID="txtbuscar" placeholder="Buscar Opcion" CssClass=" form-control"
                            AutoPostBack="true" runat="server" OnTextChanged="txtbuscar_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <asp:Repeater ID="repeat_menu" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                <a href='<%# Eval("web_form") %>'>
                                    <div class="card blue summary-inline">
                                        <div class="card-body">
                                            <i class="icon fa fa-chevron-circle-right fa-3x"></i>
                                            <div class="content">
                                                <p>
                                                    <%# Eval("descripcion") %>
                                                </p>
                                            </div>
                                            <div class="clear-both"></div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
</asp:Content>
