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
        .btr {
            position:relative;
            float:right;
        }
        .h5n {
            font-size: 17px;
            text-align:center;
        }
        .flat-blue .btn.btn-info {
            background-color: #22A7F0;
            color: #FFF;
            border-color: white;
        }
        .flat-blue .btn.btn-primary {
            background-color: #353d47;
            color: #FFF;
            border-color: white;
        }
    </style>
    <script type="text/javascript">
        function Press(id, url, estado, desc)
        {
            return true;
        }
        function PressRev(id, url, estado, desc) {
            return true;
        }
        $(document).ready(function(){
            $("#Listado").empty();
            $("#Listado").hide();
        });
        function ModalConfirm(cTitulo) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#modal_title').text(cTitulo);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">Menu Inicio
                <span>
                    <asp:LinkButton ID="lnkmenuventas" PostBackUrl="menu_ventas.aspx" CssClass="btn btn-info" runat="server" OnClick="lnkmenuventas_Click">Menu Ventas&nbsp;
                        <i class="fa fa-shopping-cart" aria-hidden="true"></i></asp:LinkButton></span>
                <span>
                    <asp:LinkButton ID="lnkverpromo" PostBackUrl="promocion_arti_terminar.aspx" CssClass="btn btn-danger" runat="server" OnClick="lnkverpromo_Click">Ver Promociones</asp:LinkButton></span>
            </h2>
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
            <asp:Panel ID="panel_search" runat="server">
                <div class="row" id="linkv">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 caja">
                                <a class="aa" href='<%#Eval("web_form") %>'>
                                    <div class="card green summary-inline">
                                        <div class="card-body">
                                            <i class="icon fa fa-chevron-circle-right fa-4x"></i>
                                            <div class="content">
                                                <h6><%# Eval("descripcion") %></h6>
                                                <h6>
                                                    <asp:LinkButton CommandName='<%#Eval("idc_opcion") %>' ID="LinkButton2" CssClass='<%#Convert.ToBoolean(Eval("favorita"))==false ? "aa":"cardlk" %>'
                                                        OnClick="Button1_Click" runat="server"><i class="icon fa fa-star fa-2x"></i></asp:LinkButton></h6>
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
            <asp:Panel ID="panel_menus_repeat" runat="server" Visible="true">
                <div class="row">
                    <asp:Repeater ID="Repeater3" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# DataBinder.Eval(Container.DataItem, "web_form").ToString() !="" ?  DataBinder.Eval(Container.DataItem, "web_form").ToString():string.Format("menu.aspx?menu={0}&nivel={1}", DataBinder.Eval(Container.DataItem, "menu").ToString().Trim(), DataBinder.Eval(Container.DataItem, "nivel"))  %>'>
                                    <div class='<%# DataBinder.Eval(Container.DataItem, "web_form").ToString() !="" ?  "card green summary-inline":"card dark summary-inline"  %>'>
                                        <div class="card-body">
                                            <i class="icon fa fa-chevron-circle-right fa-4x"></i>
                                            <div class="content">
                                                <h6>
                                                    <asp:Label ID="lbl" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "menu").ToString() %>'></asp:Label>
                                                </h6>
                                                <h6 style="text-align: right;">
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
                                    <h5 class="h5n" style="background-color: #353d47; color: white;">Tareas Pendientes para hoy<small style="background-color: #353d47; color: white;" id="total_tareas" runat="server">
                                        <asp:Label ID="lbltotaltt" runat="server" Text=""></asp:Label></small></h5>

                                </div>
                            </div>
                            <div class="pull-right card-action">
                                <div class="btn-group" role="group">
                                    <asp:LinkButton ID="LinkButton3" CssClass="btn btn-primary" runat="server" OnClick="LinkButton3_Click"><i class="fa fa-chevron-down" aria-hidden="true"></i>

                                    </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                        <div class="card-body" id="cardmias" runat="server">
                            <a class="btn btn-primary btn-block" href="tareas_listado.aspx">
                                <asp:Label ID="lblpendientes" runat="server" Text="Ver Todas Mis Tareas"></asp:Label>
                                <i class="fa fa-wrench" aria-hidden="true"></i></a>
                            <h5 style="text-align: center" id="notareas" runat="server" visible="false">No tiene Tareas Pendientes para HOY <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></h5>

                            <div class="list-group">
                                <asp:Repeater ID="repeat_tareas" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnktarea" PostBackUrl='<%# Eval("url").ToString()+presentacion.funciones.deTextoa64(Eval("idc_tarea").ToString())%>'
                                            runat="server" CssClass='<%#Eval("css_class")%>' ToolTip='<%#Eval("desc_completa")%>'>
                                            <span onclick="return Press('<%#Eval("idc_tarea")%>','<%#Eval("url").ToString()+presentacion.funciones.deTextoa64(Eval("idc_tarea").ToString())%>','<%#Eval("tipo")%>','<%#Eval("descripcion").ToString().Replace(System.Environment.NewLine,"")%>')" 
                                                class="badge btn btn-default btn-xs"><%#Eval("icono")%></span> 
                                             
                                            <h5 class="list-group-item-heading"><strong><%#Eval("fecha_compromiso")%></strong></h5>                                             
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
                        <div class="card-header" style="background-color: #1e88e5; color: white;">
                            <div class="card-title" style="background-color: #1e88e5; color: white;">
                                <div class="title" style="background-color: #1e88e5; color: white;">
                                    <h5 class="h5n" style="background-color: #1e88e5; color: white;">Tareas Asignadas para hoy<small style="background-color: #1e88e5; color: white;" id="Small1" runat="server">
                                        <asp:Label ID="lblasi" runat="server" Text=""></asp:Label></small></h5>
                                </div>
                            </div>
                            <div class="pull-right card-action">
                                <div class="btn-group" role="group">
                                    <asp:LinkButton ID="LinkButton4" OnClick="LinkButton4_Click" CssClass="btn btn-info" runat="server"><i class="fa fa-chevron-down" aria-hidden="true"></i>

                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body" id="cardasignadas" runat="server">
                            <a class="btn btn-info btn-block" href="tareas_asignadas_lista.aspx">
                                <asp:Label ID="lblasignadas" runat="server" Text="Ver Todas Mis Tareas"></asp:Label>
                                <i class="fa fa-wrench" aria-hidden="true"></i></a>
                            <h5 style="text-align: center" id="tareasasig" runat="server" visible="false">No tiene Tareas Pendientes para HOY <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></h5>
                            <div class="list-group">
                                <asp:Repeater ID="repeatasignadas" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnktarea" PostBackUrl='<%#Eval("url").ToString()+presentacion.funciones.deTextoa64(Eval("idc_tarea").ToString())%>' runat="server" CssClass='<%#Eval("css_class")%>' ToolTip='<%#Eval("desc_completa")%>'>
                                            <span onclick="return PressRev('<%#Eval("idc_tarea")%>','<%#Eval("url").ToString()+presentacion.funciones.deTextoa64(Eval("idc_tarea").ToString())%>','<%#Eval("tipo")%>','<%#Eval("descripcion").ToString().Replace(System.Environment.NewLine," ")%>')" 
                                                class="badge btn btn-default btn-xs"><%#Eval("icono")%></span> <h5 class="list-group-item-heading"><strong><%#Eval("fecha_compromiso")%></strong></h5>
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
                                                <div id="Listado" class="ListadoR">
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