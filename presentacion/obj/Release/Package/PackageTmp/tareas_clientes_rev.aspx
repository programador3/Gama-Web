<%@ Page Title="Revisar Tareas de Agente" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_clientes_rev.aspx.cs" Inherits="presentacion.tareas_clientes_rev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        .span-padding {
        }

        .div-separator {
            background: gainsboro;
            height: 1px;
            margin: 5px 0;
        }

        .span-title {
            color: Blue;
            font-weight: bold;
        }

        .button-input {
            height: 30px;
            width: 100px;
        }

        .div-descripion {
            overflow: hidden;
            min-width: 79px;
            text-align: center;
            display: initial;
            padding: 0;
            margin: 0;
        }

        .div-V, .div-v {
            background: lime;
            color: Black;
            text-align: center;
            min-width: 20px;
        }

        .div-N, .div-n {
            background: yellow;
            color: Black;
            text-align: center;
            min-width: 20px;
        }

        .div-P, .div-p {
            background: orange;
            color: Black;
            text-align: center;
            min-width: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class=" page-header">
        Revisar Tareas de Agente
    </h2>
     
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
         <Triggers>
             <asp:PostBackTrigger ControlID="LinkButton2" />
         </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <h4><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</strong></h4>
                    <asp:TextBox runat="server" ID="txtfecha" CssClass="form-control" TextMode="Date">
                    </asp:TextBox>
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary btn-block" runat="server" OnClick="LinkButton1_Click">Ver Tareas</asp:LinkButton>
                </div>
                <div class="col-lg-12">
                    <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente</strong></h4>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtcliente"
                        CssClass="form-control" onfocus="this.blur();"></asp:TextBox>
                </div>
                <div class="col-lg-6">
                    <h4><strong>RFC</strong></h4>
                    <asp:TextBox ReadOnly="true" runat="server" ID="txtrfc" onfocus="this.blur();"
                        CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-lg-6">
                    <h4><strong>CVE</strong></h4>
                    <asp:TextBox ReadOnly="true" runat="server" ID="txtcve" onfocus="this.blur();"
                        CssClass="form-control"></asp:TextBox>
                </div>
                <div class=" col-lg-12">
                    <h4><strong>Observaciones</strong></h4>
                    <asp:TextBox runat="server" ReadOnly="true" TextMode="MultiLine" Rows="3" ID="txtobservaciones" onfocus="this.blur();"
                        CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-12">
                    <div id="iv_articulos">
                        <div class=" table table-responsive">
                            <asp:DataGrid ID="grdarts" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive table-bordered table-condensed"
                                Width="100%" OnItemCommand="grdarts_ItemCommand">
                                <HeaderStyle BackColor="Gray" ForeColor="white" />
                                <ItemStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundColumn DataField="idc_clientetareadet" HeaderText="idc_clientetareadet" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idc_articulo" HeaderText="idc_articulo" Visible="false"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Descripción">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <span class="span-title">Articulo:</span>
                                            <asp:Label ID="lbldesart" runat="server" Text='<%#Eval("desart") %>'>
                                            </asp:Label>
                                            <div class="div-separator"></div>

                                            <span class="span-title">Codigo:</span>
                                            <asp:Label ID="lblcodigo" runat="server" Text='<%#Eval("codigo") %>'>
                                            </asp:Label>
                                            <div class="div-separator"></div>

                                            <span class="span-title">UM:</span>
                                            <asp:Label ID="lblum" runat="server" Text='<%#Eval("nom_corto") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Detalle">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <span class="span-title">Tipo:</span>
                                            <div class='<%#"div-" + Eval("tipo") %>' style="display: -webkit-inline-box !important;">
                                                <asp:Label ID="lbltipo" runat="server" Text='<%#Eval("tipo") %>'>
                                                </asp:Label>
                                            </div>
                                            <div class="div-separator"></div>

                                            <span class="span-title">Cantidad:</span>
                                            <asp:Label ID="lblcantidad" runat="server" Text='<%#Eval("cantidad") %>'>
                                            </asp:Label>
                                            <div class="div-separator"></div>

                                            <span class="span-title">Meta:</span>
                                            <asp:Label ID="lblmeta" runat="server" Text='<%#Eval("meta") %>'>
                                            </asp:Label>
                                            <asp:Button ID="btnnegociar" runat="server" Text="Negociar" CommandName="negociar" 
                                                CssClass="btn btn-default" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="codigo" HeaderText="Codigo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="desart" HeaderText="Articulo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="nom_corto" HeaderText="UM" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="tipo" HeaderText="Tipo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="cantidad" HeaderText="Cantidad" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="meta" HeaderText="Meta" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="observ" HeaderText="Observ"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkcumplida"
                                                            Checked='<%#Eval("cumplida") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Obs">
                                        <ItemStyle VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <div>
                                                <asp:TextBox ID="txtobs" runat="server" Text='<%#Eval("obs_cumplida") %>' 
                                                    Style="min-height: 50px;" TextMode="MultiLine" Width="100%" CssClass="form-control">
                                                </asp:TextBox>
                                            </div>
                                            <div>
                                                <asp:Button ID="btnguardar" runat="server" Text="Guardar" CommandName="Guardar" CssClass="btn btn-default" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <div style="text-align: right;">
                        <div class="div-descripion div-V">
                            Venta
                        </div>
                        <div class="div-descripion div-N">
                            Negociar
                        </div>
                        <div class="div-descripion div-P">
                            Propuesta
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-danger btn-block" OnClick="LinkButton2_Click">Regresar</asp:LinkButton>
                </div>
            </div>
            </ContentTemplate>
         </asp:UpdatePanel>

</asp:Content>
