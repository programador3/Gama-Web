<%@ Page Title="Menu Principal" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="presentacion.menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/PanelsLTE.css" rel="stylesheet" />
    <link href="css/ionicons.css" rel="stylesheet" />
    <link href="css/ionicons.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function Search() {
            var value = document.getElementById('<%=txtsearch.ClientID%>').value;
            var panel_menus_repeat_js = document.getElementById("<%=panel_menus_repeat.ClientID%>");
            var panel_search_js = document.getElementById("<%=panel_search.ClientID%>");
            if (value = "") {
                panel_menus_repeat_js.style.visibility = "visible";
                panel_search_js.style.visibility = "hidden";

            } else {
                panel_menus_repeat_js.style.visibility = "hidden";
                panel_search_js.style.visibility = "visible";
            }
        }
        function DeleteFocus(txt) {
            txt.value = "";
        }
    </script>
    <style type="text/css">
        .dropdown-submenu {
            position: relative;
        }

            .dropdown-submenu > .dropdown-menu {
                top: 0;
                left: 100%;
                margin-top: -6px;
                margin-left: -1px;
                -webkit-border-radius: 0 6px 6px 6px;
                -moz-border-radius: 0 6px 6px;
                border-radius: 0 6px 6px 6px;
            }

            .dropdown-submenu:hover > .dropdown-menu {
                display: block;
            }

            .dropdown-submenu > a:after {
                display: block;
                content: " ";
                float: right;
                width: 0;
                height: 0;
                border-color: transparent;
                border-style: solid;
                border-width: 5px 0 5px 5px;
                border-left-color: #ccc;
                margin-top: 5px;
                margin-right: -10px;
            }

            .dropdown-submenu:hover > a:after {
                border-left-color: #fff;
            }

            .dropdown-submenu.pull-left {
                float: none;
            }

                .dropdown-submenu.pull-left > .dropdown-menu {
                    left: -100%;
                    margin-left: 10px;
                    -webkit-border-radius: 6px 0 6px 6px;
                    -moz-border-radius: 6px 0 6px 6px;
                    border-radius: 6px 0 6px 6px;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Menu Principal
            </h1>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-6">
                    <div class="form-group has-feedback">
                        <asp:TextBox ID="txtsearch" onkeypress="Search()" onfocus="DeleteFocus(this);" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtsearch_TextChanged" placeholder="Buscar Pagina"></asp:TextBox>
                        <span class="glyphicon glyphicon-search form-control-feedback"></span>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <div class="form-group">
                        <h4 id="Encontramos" runat="server" visible="false">Encontramos un Total de
                            <asp:Label ID="lblenc" runat="server" Text=""></asp:Label>
                            coincidencia(s) <i class="fa fa-search"></i></h4>
                    </div>
                </div>
            </div>

            <!-- /.row -->

            <asp:Panel ID="panel_search" runat="server">
                <div class="row">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                                <a href='<%# DataBinder.Eval(Container.DataItem, "web_form").ToString() !="" ?  DataBinder.Eval(Container.DataItem, "web_form").ToString():string.Format("menu.aspx?menu={0}&nivel={1}", DataBinder.Eval(Container.DataItem, "menu").ToString().Trim(), DataBinder.Eval(Container.DataItem, "nivel"))  %>'>
                                    <div class="card green summary-inline">
                                        <div class="card-body">
                                            <i class="icon fa fa-chevron-circle-right fa-4x"></i>
                                            <div class="content">
                                                <h5>
                                                    <asp:Label ID="lbl" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "descripcion").ToString() %>'></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="clear-both"></div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <h2 style="text-align: center;" id="NoResultados" runat="server" visible="false">La Busqueda no encontro resultados. <i class="fa fa-thumbs-o-down"></i>
                        <br />
                        Puede Intentarlo Nuevamente.</h2>
                </div>
            </asp:Panel>
            <asp:Panel ID="panel_menus_repeat" runat="server">
                <div class="row">
                    <asp:Repeater ID="Repeater3" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# DataBinder.Eval(Container.DataItem, "web_form").ToString() !="" ?  DataBinder.Eval(Container.DataItem, "web_form").ToString():string.Format("menu.aspx?menu={0}&nivel={1}", DataBinder.Eval(Container.DataItem, "menu").ToString().Trim(), DataBinder.Eval(Container.DataItem, "nivel"))  %>'>
                                    <div class='<%# DataBinder.Eval(Container.DataItem, "web_form").ToString() !="" ?  "card green summary-inline":"card blue summary-inline"  %>'>
                                        <div class="card-body">
                                            <i class="icon fa fa-chevron-circle-right fa-4x"></i>
                                            <div class="content">
                                                <h5>
                                                    <asp:Label ID="lbl" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "menu").ToString() %>'></asp:Label>
                                                    </h4>
                                                <h6><%# DataBinder.Eval(Container.DataItem, "web_form").ToString() !="" ?  "Pagina":"Menu"  %>
                                                </h6>
                                            </div>
                                            <div class="clear-both"></div>
                                        </div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <div class="card">
                        <div class="card-header" style="background-color: #353d47; color: white;">
                            <div class="card-title" style="background-color: #353d47; color: white;">
                                <div class="title" style="background-color: #353d47; color: white;">
                                    <h3 style="background-color: #353d47; color: white;">Mis Tareas Pendientes para hoy <small style="background-color: #353d47; color: white;" id="total_tareas" runat="server">
                                        <asp:Label ID="lbltotaltt" runat="server" Text=""></asp:Label></small></h3>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <a class="btn btn-primary btn-block" href="tareas_listado.aspx">Ver Todas Mis Tareas <i class="fa fa-wrench" aria-hidden="true"></i></a>
                            <h3 style="text-align: center" id="notareas" runat="server" visible="false">No tiene Tareas Pendientes para HOY <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></h3>
                            <div class="list-group">
                                <asp:Repeater ID="repeat_tareas" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnktarea" PostBackUrl='<%#Eval("url")%>' runat="server" CssClass='<%#Eval("css_class")%>' ToolTip='<%#Eval("desc_completa")%>'>
                                            <span class="badge btn btn-default btn-xs"><%#Eval("icono")%></span> <h5 class="list-group-item-heading"><strong><%#Eval("fecha_compromiso")%></strong></h5>
                                                <p class="list-group-item-text"><%#Eval("descripcion")%></p>
                                                <p class="list-group-item-text"><strong>Asigno</strong>: <%#Eval("empleado_asigna")%></p>
                                                <p class="list-group-item-text"><strong>Depto</strong>: <%#Eval("depto_asigna")%></p>
                                                <p class="list-group-item-text"><strong>Estado</strong>: <%#Eval("tipo")%></p>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <div class="card">
                        <div class="card-header" style="background-color: #1ABC9C; color: white;">
                            <div class="card-title" style="background-color: #1ABC9C; color: white;">
                                <div class="title" style="background-color: #1ABC9C; color: white;">
                                    <h3 style="background-color: #1ABC9C; color: white;">Mis Tareas Asignadas <small style="background-color: #1ABC9C; color: white;" id="Small1" runat="server">
                                        <asp:Label ID="lblasi" runat="server" Text=""></asp:Label></small></h3>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <a class="btn btn-success btn-block" href="tareas_asignadas_lista.aspx">Ver Todas Mis Tareas <i class="fa fa-wrench" aria-hidden="true"></i></a>
                            <h3 style="text-align: center" id="tareasasig" runat="server" visible="false">No tiene Tareas Pendientes para HOY <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></h3>
                            <div class="list-group">
                                <asp:Repeater ID="repeatasignadas" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnktarea" PostBackUrl='<%#Eval("url")%>' runat="server" CssClass='<%#Eval("css_class")%>' ToolTip='<%#Eval("desc_completa")%>'>
                                            <span class="badge btn btn-default btn-xs"><%#Eval("icono")%></span> <h5 class="list-group-item-heading"><strong><%#Eval("fecha_compromiso")%></strong></h5>
                                                <p class="list-group-item-text"><%#Eval("descripcion")%></p>
                                                <p class="list-group-item-text"><strong>Realiza</strong>: <%#Eval("empleado")%></p>
                                                <p class="list-group-item-text"><strong>Depto</strong>: <%#Eval("depto")%></p>
                                                <p class="list-group-item-text"><strong>Estado</strong>: <%#Eval("tipo")%></p>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>

            <!--campos ocultos -->
            <asp:HiddenField ID="ocmenu1" runat="server" />
            <asp:HiddenField ID="ocmenu2" runat="server" />
            <asp:HiddenField ID="ocmenu3" runat="server" />
            <asp:HiddenField ID="ocmenu4" runat="server" />
            <asp:HiddenField ID="ocmenu5" runat="server" />
            <asp:HiddenField ID="ocmenu6" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <footer>
        <asp:Panel ID="panel" runat="server" Visible="false">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">

                    <ol class="breadcrumb">
                        <li>
                            <a href="menu.aspx">Menu Principal</a>
                        </li>
                        <!-- 1 -->
                        <li class="<%= ocmenu1.Value!=""?"show-line":"hide" %>">

                            <asp:LinkButton ID="link1" runat="server"><%= ocmenu1.Value.ToString()  %></asp:LinkButton>
                        </li>
                        <!-- 2 -->
                        <li class="<%= ocmenu2.Value!=""?"show-line":"hide" %>">
                            <asp:LinkButton ID="link2" runat="server">
                                    <%= ocmenu2.Value.ToString()  %>
                            </asp:LinkButton>
                        </li>
                        <!-- 3 -->
                        <li class="<%= ocmenu3.Value!=""?"show-line":"hide" %>">
                            <asp:LinkButton ID="link3" runat="server">
                                <%= ocmenu3.Value.ToString()  %>
                            </asp:LinkButton>
                        </li>
                        <!-- 4 -->
                        <li class="<%= ocmenu4.Value!=""?"show-line":"hide" %>">
                            <asp:LinkButton ID="link4" runat="server">
                                    <%= ocmenu4.Value.ToString()  %>
                            </asp:LinkButton>
                        </li>
                        <!-- 5 -->
                        <li class="<%= ocmenu5.Value!=""?"show-line":"hide" %>">
                            <asp:LinkButton ID="link5" runat="server">
                                    <%= ocmenu5.Value.ToString()  %>
                            </asp:LinkButton>
                        </li>
                        <!-- 6 -->
                        <li class="<%= ocmenu6.Value!=""?"show-line":"hide" %>">
                            <asp:LinkButton ID="link6" runat="server">
                                  <%= ocmenu6.Value.ToString()  %>
                            </asp:LinkButton>
                        </li>
                    </ol>
                </div>
            </div>
        </asp:Panel>
    </footer>
</asp:Content>