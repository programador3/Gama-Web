<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="ejemplo_menu.aspx.cs" Inherits="presentacion.ejemplo_menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () { // Script del Navegador

            $(".subnavegador").hide();
            $(".sub-subnavegador").hide();
            $(".desplegable").click(
                function () {
                    if ($(this).parent().find(".subnavegador").is(":visible")) {
                        $(this).parent().find(".subnavegador").slideUp('fast');
                    } else {
                        $(this).parent().find(".subnavegador").slideDown('slow');
                    }
                }
            );
            $(".sub-desplegable").click(
                function () {
                    if ($(this).parent().find(".sub-subnavegador").is(":visible")) {
                        $(this).parent().find(".sub-subnavegador").slideUp('fast');
                    } else {
                        $(this).parent().find(".sub-subnavegador").slideDown('slow');
                    }

                }
            );
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-3">
                    <ul class="navegador nav nav-pills" style="font-size: smaller;">
                        <asp:Repeater ID="MenuPadres" runat="server" OnItemDataBound="MenuPadres_ItemDataBound">
                            <ItemTemplate>
                                <li><a href="#" style="text-decoration: none;" class="desplegable dropdown-toggle btn-link" title='<%#Eval("nombre") %>'><%#Eval("nombre") %></a>
                                    <ul class="subnavegador nav">
                                        <asp:Repeater ID="MenuHijo" runat="server">
                                            <ItemTemplate>
                                                <li><a href="#" class="sub-desplegable dropdown-toggle btn-link" title='<%#Eval("hijo") %>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-plus-square"></i> <%#Eval("hijo") %></a>
                                                    <ul class="sub-subnavegador nav">
                                                        <li><a href='ejemplo_menu.aspx?art=<%#Eval("hijo") %>' class="dropdown-toggle" title='<%#Eval("hijo") %>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-circle"></i> <%#Eval("hijo") %></a>
                                                        </li>
                                                        <li></i> <a href='ejemplo_menu.aspx?art=<%#Eval("hijo") %>' class="dropdown-toggle" title='<%#Eval("hijo") %>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-circle"></i> <%#Eval("hijo") %></a>
                                                        </li>
                                                        <li><a href='ejemplo_menu.aspx?art=<%#Eval("hijo") %>' class="dropdown-toggle" title='<%#Eval("hijo") %>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-circle"></i> <%#Eval("hijo") %></a>
                                                        </li>
                                                    </ul>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="col-lg-8">
                    <h3>
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label></h3>
                    <asp:Button ID="Button1" runat="server" Text="Clean User" CssClass="btn btn-success" OnClick="Button1_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>