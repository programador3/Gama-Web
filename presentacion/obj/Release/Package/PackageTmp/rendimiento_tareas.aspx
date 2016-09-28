<%@ Page Title="Rendimiento Tareas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="rendimiento_tareas.aspx.cs" Inherits="presentacion.rendimiento_tareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Reportes de rendimiento por Puesto</h1>
    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" id="DEPTO" runat="server">
            <h4><strong>Departamento</strong></h4>
            <asp:DropDownList ID="ddldeptos" OnSelectedIndexChanged="ddldeptos_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <h4><strong>Puesto que realiza la Tarea</strong></h4>
            <asp:DropDownList ID="ddlPuestoAsigna" OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-8 col-xs-12">
            <h4>Escriba un Filtro</h4>
            <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12">
            <h4>.</h4>
            <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h4><i class="fa fa-calendar"></i>&nbsp;<strong>Seleccione una fecha de inicio</strong></h4>
            <asp:TextBox ID="txtfechainicio" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h4><i class="fa fa-calendar"></i>&nbsp;<strong>Seleccione una fecha de fin</strong></h4>
            <asp:TextBox ID="txtfechafin" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-12 col-xs-12">
            <asp:LinkButton ID="lnkir" OnClick="lnkir_Click" CssClass="btn btn-success btn-block" runat="server">Generar Graficas <i class="fa fa-repeat" aria-hidden="true"></i></asp:LinkButton>
        </div>
        <div class="col-lg-12 col-xs-12">
            <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" CssClass="btn btn-info btn-block" runat="server">Ver Detalles Desglosados <i class="fa fa-repeat" aria-hidden="true"></i></asp:LinkButton>
        </div>
    </div>
</asp:Content>