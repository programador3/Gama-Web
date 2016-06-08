<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_asignadas_lista.aspx.cs" Inherits="presentacion.tareas_asignadas_lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-xs-12">
                    <div class="card">
                        <div class="card-header" style="background-color: #22A7F0; color: white;">
                            <div class="card-title" style="background-color: #22A7F0; color: white;">
                                <div class="title" style="background-color: #22A7F0; color: white;">
                                    <h3 style="background-color: #22A7F0; color: white;">Tareas Asignadas Pendientes <small style="background-color: #22A7F0; color: white;" id="total_tareas" runat="server">
                                        <asp:Label ID="lbltotaltt" runat="server" Text=""></asp:Label></small></h3>
                                </div>
                            </div>
                        </div>
                        <div class="card-body" id="TAREAS_IND" runat="server" visible="false">
                            <div class="row">
                                <div class="col-lg-10 col-xs-8">
                                    <h4>Escriba un Filtro<small> Ingrese "/" para Buscar Departamento, "+" para Buscar Empleados y "*" para Buscar Estado</small></h4>
                                    <asp:TextBox ID="TextBox1" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtpuesto_filtro_TextChanged" placeholder="Buscar"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 col-xs-4">
                                    <h4>.</h4>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                            </div>
                            <h3 style="text-align: center" id="notareas" runat="server" visible="false">No tiene Tareas Asignadas Pendientes <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></h3>
                            <asp:LinkButton ID="lnkexcel" CssClass="btn btn-success btn-block" OnClick="lnkexcel_Click" runat="server">Exportar a Excel <i class="fa fa-file-excel-o" aria-hidden="true"></i></asp:LinkButton>
                            <div class="list-group">
                                <asp:Repeater ID="repeat_tareas" runat="server">
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
                        <div class="card-body" id="tareas_todas" runat="server" visible="false">
                            <h3 style="text-align: center" id="H1" runat="server" visible="false">No tiene Tareas Pendientes <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></h3>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="LinkButton1" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-10 col-xs-8">
                                            <h4>Escriba un Filtro <small>Ingrese "/" para Buscar Departamento, "+" para Buscar Empleados y "*" para Buscar Estado</small></h4>
                                            <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtpuesto_filtro_TextChanged" placeholder="Buscar"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-xs-4">
                                            <h4>.</h4>
                                            <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <h5>
                                        <asp:Label ID="lbtot_global" runat="server" Text=""></asp:Label></h5>
                                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success btn-block" OnClick="lnkexcel_Click" runat="server">Exportar a Excel <i class="fa fa-file-excel-o" aria-hidden="true"></i></asp:LinkButton>
                                    <div class="list-group">
                                        <asp:Repeater ID="repeatglobal" runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnktarea" PostBackUrl='<%#Eval("url")%>' runat="server" CssClass='<%#Eval("css_class")%>' ToolTip='<%#Eval("desc_completa")%>'>
                                            <span class="badge btn btn-default btn-xs"><%#Eval("icono")%></span> <h5 class="list-group-item-heading"><strong><%#Eval("fecha_compromiso")%></strong></h5>
                                                <p class="list-group-item-text"><%#Eval("descripcion")%></p>
                                                <p class="list-group-item-text"><strong>Estado</strong>: <%#Eval("empleado")%></p>
                                                <p class="list-group-item-text"><strong>Depto</strong>: <%#Eval("depto")%></p>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>