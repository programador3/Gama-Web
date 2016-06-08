<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="empleados_faltas.aspx.cs" Inherits="presentacion.empleados_faltas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Empleados con Faltas Pendientes por Revisión</h1>
                </div>
            </div>
            <div class="row">
                <asp:Repeater ID="repeat_pendi" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <asp:LinkButton ID="lnkir" runat="server" CssClass="btn btn-info btn-block" CommandName='<%#Eval("idc_empleado_falta") %>' OnClick="lnkir_Click">
                                  <h5><%#Eval("puesto") %></h5>
                                  <h5><%#Eval("empleado") %></h5>
                                  <h5><%#Eval("fecha") %></h5>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>