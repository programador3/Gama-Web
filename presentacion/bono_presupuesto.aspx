<%@ Page Title="" Language="C#" MasterPageFile="~/Adicional.Master" AutoEventWireup="true" CodeBehind="bono_presupuesto.aspx.cs" Inherits="presentacion.bono_presupuesto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header"><i class="fa fa-info-circle" aria-hidden="true"></i>&nbsp;Información del Bono de Presupuesto</h3>
        </div>
        <div class="col-lg-12">
            <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Agente</strong></h5>
            <asp:TextBox ID="txtnumagente" runat="server"
                CssClass="form-control2" style="text-align:right;"
                Height="30px" onfocus="this.blur();" Width="99%"
                BackColor="White"></asp:TextBox>

        </div>
        <div class="col-lg-12">
            <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Presupuesto</strong></h5>
            <asp:TextBox ID="txtpresupuesto" runat="server"
                CssClass="form-control2" style="text-align:right;"
                Height="30px" onfocus="this.blur();" Width="99%"
                BackColor="White"></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Venta</strong></h5>
            <asp:TextBox ID="txtventa_modal" runat="server"
                CssClass="form-control2" style="text-align:right;"
                Height="30px" onfocus="this.blur();" Width="99%"
                BackColor="White"></asp:TextBox>
        </div>

        <div class="col-lg-12">
            <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Bono del Presupuesto</strong></h5>
            <asp:TextBox ID="txtbono_presupuesto" runat="server"
                CssClass="form-control2" style="text-align:right;"
                Height="30px" onfocus="this.blur();" Width="99%"
                BackColor="White"></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <asp:LinkButton ID="LinkButton1" OnClientClick="window.close();" CssClass="btn btn-danger btn-block" runat="server">Cerrar Ventana&nbsp;<i class="fa fa-window-close" aria-hidden="true"></i></asp:LinkButton>
        </div>
    </div>
</asp:Content>
