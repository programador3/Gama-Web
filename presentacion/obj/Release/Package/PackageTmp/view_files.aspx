<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="view_files.aspx.cs" Inherits="presentacion.view_files" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h2 class="header">Visor de Archivos
                        <asp:LinkButton ID="lnkCerrar" CssClass="btn btn-danger" runat="server" OnClick="lnkCerrar_Click">Cerrar Visor  <i class="fa fa-times-circle"></i></asp:LinkButton>
                    </h2>
                </div>
            </div>
            <asp:Panel ID="PanelVisor" runat="server" Visible="false">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="form-group">
                            <iframe id="ifrma" runat="server" width="1200" height="900"></iframe>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelHTML" runat="server" Visible="false">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="form-group">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>