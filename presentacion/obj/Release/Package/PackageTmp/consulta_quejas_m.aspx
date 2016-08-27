<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="consulta_quejas_m.aspx.cs" Inherits="presentacion.consulta_quejas_m" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .sombreado {
                background-color: rgb(186, 192, 194);
                padding:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Consulta de Quejas
            </h1>
        </div>
    </div>
    <div style="font-size:11px;">
        
        <div class="row">
            <div class="col-lg-12">
                <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;No. Queja</strong></h5>
                <asp:TextBox AutoPostBack="true" style="color:cornflowerblue;" ReadOnly="true" ID="txtnoqueja" CssClass="form-control" runat="server" OnTextChanged="txtnoqueja_TextChanged" TextMode="Number" autofocus></asp:TextBox>
            </div>
            <div class="col-lg-12">
                <h5><strong><i class="fa fa-book" aria-hidden="true"></i>&nbsp;Factura</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtfactura" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtfecha" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <h5><strong><i class="fa fa-laptop" aria-hidden="true"></i>&nbsp;Capturo</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtcaptura" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <h5><strong><i class="fa fa-key" aria-hidden="true"></i>&nbsp;RFC</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtfrc" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <h5><strong><i class="fa fa-key" aria-hidden="true"></i>&nbsp;Cveadi</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtcveadi" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-12">
                <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtcliente" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-12">
                <h5><strong><i class="fa fa-comment-o" aria-hidden="true"></i>&nbsp;Problema o Queja</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtqueja" TextMode="Multiline" Rows="5" style="font-size:11px;" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-12">
                <div class="table table-responsive">
                    <asp:DataGrid ID="griddatos" runat="server" AutoGenerateColumns="False"
                         CssClass="table table-responsive table-condensed table-bordered">
                        <Columns>
                            <asp:BoundColumn DataField="codigo" HeaderText="Codigo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="desart" HeaderText="Descripcion"></asp:BoundColumn>
                            <asp:BoundColumn DataField="unimed" HeaderText="UM"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cantidad" HeaderText="Cantidad" DataFormatString="{0:N4}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="devo" HeaderText="Queja" DataFormatString="{0:N4}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
            <div style="padding:15px;">
                <div class=" sombreado col-lg-8 col-md-8 col-sm-8 col-xs-12">
                    <h5><strong>Agente</strong></h5>
                    <asp:TextBox ReadOnly="true" ID="txtagente" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div  class="sombreado col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <h5><strong>Tel</strong></h5>
                    <asp:TextBox ReadOnly="true" ID="txttelefonoagente" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="sombreado col-lg-8 col-md-8 col-sm-8 col-xs-12">
                    <h5><strong>TMK</strong></h5>
                    <asp:TextBox ReadOnly="true" ID="txttkm" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="sombreado col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <h5><strong>Tel</strong></h5>
                    <asp:TextBox ReadOnly="true" ID="txtteñtkm" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="sombreado col-lg-8 col-md-8 col-sm-8 col-xs-12">
                    <h5><strong>Comprador</strong></h5>
                    <asp:TextBox ReadOnly="true" ID="txtcomprador" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="sombreado col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <h5><strong>Tel</strong></h5>
                    <asp:TextBox ReadOnly="true" ID="txttelcomprador" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="sombreado col-lg-8 col-md-8 col-sm-8 col-xs-12">
                    <h5><strong>Contacto(Obra)</strong></h5>
                    <asp:TextBox ReadOnly="true" ID="txtcontacto" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="sombreado col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <h5><strong>Tel</strong></h5>
                    <asp:TextBox ReadOnly="true" ID="txttelcontacto" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                
                <br />
            </div>
            <div class="col-lg-12">
                <h5><strong><i class="fa fa-comment" aria-hidden="true"></i>&nbsp;Observaciones</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtobservaciones" TextMode="Multiline"  Rows="5" style="font-size:11px;" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <h5><strong>Colonia</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtcolonia" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <h5><strong>Hora de Visita:</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txthoravisita" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <h5><strong>Calle</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtCalle" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <h5><strong>Num</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtnumero" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-12">
                <h5><strong>Municipio</strong></h5>
                <asp:TextBox ReadOnly="true" ID="txtmunicipio" TextMode="Multiline" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-lg-12" id="cancelada" runat="server" visible="false">
                <asp:Label ID="lblcance" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="15px" ForeColor="Red" Visible="False"></asp:Label>
            </div>
             <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                 <asp:LinkButton ID="LinkButton2" CssClass="btn btn-primary btn-block"  runat="server" OnClick="LinkButton2_Click">Ver Otra Queja</asp:LinkButton>
            </div>
             <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                 <asp:LinkButton ID="LinkButton1" CssClass="btn btn-danger btn-block" PostBackUrl="quejas_espera_m.aspx" runat="server">Cerrar</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
