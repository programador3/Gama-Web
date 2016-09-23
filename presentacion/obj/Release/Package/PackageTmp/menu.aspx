<%@ Page Title="Menu Inicio" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="presentacion.menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
       
        function DeleteFocus(txt) {
            txt.value = "";
        }
    </script>
    <style type="text/css">
        a, a:active {
            text-decoration: none;
        }

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

        .caja a {
            color: white;
        }

        .caja h5 {
            font-size: 14px;
            text-align: -webkit-right;
        }
     
           
       .aa:link {
            color: white;
        }

        /* visited link */
        .aa:visited {
            color: white;
        }

        /* mouse over link */
        .aa:hover {
            color: white;
        }

        /* selected link */
        .aa:active {
            color: white;
        }
           
        .cardlk:link {
            color: yellow;
        }

        /* visited link */
        .cardlk:visited {
            color: yellow;
        }

        /* mouse over link */
        .cardlk:hover {
            color: yellow;
        }

        /* selected link */
        .cardlk:active {
            color: yellow;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Menu Inicio
            </h1>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="txtsearch" />
        </Triggers>
        <ContentTemplate>
            
            <div class="row">

                <div class="col-lg-4 col-md-6 col-sm-12">
                    <div class="form-group has-feedback">
                        <asp:TextBox ID="txtsearch" CssClass="form-control" runat="server" AutoPostBack="true" 
                            OnTextChanged="txtsearch_TextChanged" placeholder="Buscar Pagina"></asp:TextBox>
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
                <div class="row" id="linkv">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12 caja">
                                <a class="aa" href='<%#Eval("web_form") %>'>
                                    <div class="card green summary-inline">
                                        <div class="card-body">
                                            <i class="icon fa fa-chevron-circle-right fa-5x"></i>
                                            <div class="content">
                                                <h5><%# Eval("descripcion") %></h5>
                                                <h5>
                                                    <asp:LinkButton CommandName='<%#Eval("idc_opcion") %>' ID="LinkButton2" CssClass='<%#Convert.ToBoolean(Eval("favorita"))==false ? "aa":"cardlk" %>'
                                                        OnClick="Button1_Click" runat="server"><i class="icon fa fa-star fa-2x"></i></asp:LinkButton></h5>
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
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# DataBinder.Eval(Container.DataItem, "web_form").ToString() !="" ?  DataBinder.Eval(Container.DataItem, "web_form").ToString():string.Format("menu.aspx?menu={0}&nivel={1}", DataBinder.Eval(Container.DataItem, "menu").ToString().Trim(), DataBinder.Eval(Container.DataItem, "nivel"))  %>'>
                                    <div class='<%# DataBinder.Eval(Container.DataItem, "web_form").ToString() !="" ?  "card green summary-inline":"card blue summary-inline"  %>'>
                                        <div class="card-body">
                                            <i class="icon fa fa-chevron-circle-right fa-4x"></i>
                                            <div class="content">
                                                <h5>
                                                    <asp:Label ID="lbl" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "menu").ToString() %>'></asp:Label>
                                                    </h4>
                                                <h6 style="text-align:right;">
                                                    <asp:Label ID="Label1" Visible="true"
                                                         runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "web_form").ToString() !="" ?  "Pagina":"Menu"  %>'></asp:Label>
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
                            <a class="btn btn-primary btn-block" href="tareas_listado.aspx"><asp:Label ID="lblpendientes" runat="server" Text="Label"></asp:Label> <i class="fa fa-wrench" aria-hidden="true"></i></a>
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
                        <div class="card-header" style="background-color: #19B5FE; color: white;">
                            <div class="card-title" style="background-color: #19B5FE; color: white;">
                                <div class="title" style="background-color: #19B5FE; color: white;">
                                    <h3 style="background-color: #19B5FE; color: white;">Mis Tareas Asignadas <small style="background-color: #19B5FE; color: white;" id="Small1" runat="server">
                                        <asp:Label ID="lblasi" runat="server" Text=""></asp:Label></small></h3>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <a class="btn btn-info btn-block" href="tareas_asignadas_lista.aspx">
                                <asp:Label ID="lblasignadas" runat="server" Text="Label"></asp:Label> <i class="fa fa-wrench" aria-hidden="true"></i></a>
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