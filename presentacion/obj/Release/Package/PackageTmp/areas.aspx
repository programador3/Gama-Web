<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="areas.aspx.cs" Inherits="presentacion.areas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header"></h1>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="form-group">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>