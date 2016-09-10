<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="comisiones_especiales.aspx.cs" Inherits="presentacion.comisiones_especiales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">      
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
   <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header">Comisiones Especiales</h3>
            </div>
        </div>
        <!-- filtro -->
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                De:
                <asp:TextBox ID="txtfecha1" runat="server" type="date"  CssClass="form-control" ></asp:TextBox>
                
            </div>
           
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                A: 
                <asp:TextBox ID="txtfecha2" runat="server" type="date"  CssClass="form-control" ></asp:TextBox>
            </div>
            <div class="col-lg-12 col-sm-12">
                <asp:Button ID="btnfiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-block" OnClick="btnfiltrar_Click"  />
                
            </div>
        </div>
        <br />

        <!-- grid primario -->
        <div class="row">
            <div class="col-lg-12">

                <div class="table table-responsive">
                    <asp:GridView ID="grid_primario" CssClass="table table-bordered table-condensed gvv" runat="server" AutoGenerateColumns="False" DataKeyNames="idc_usuario" OnRowCommand="grid_primario_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar" HeaderStyle-Width="20px"> 
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnseleccionar" runat="server" ImageUrl="~/imagenes/btn/icon_buscar.png" CommandName="select_registro" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="idc_usuario" HeaderText="Idc_usuario" Visible="False">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bono" HeaderText="Bono" DataFormatString="{0:C}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-7 col-md-6 col-sm-12"></div>
            <div class="col-lg-5 col-md-6 col-sm-12">
                <h5 style=" width:100%"><strong>Comision Total: </strong></h5>
                 <asp:Label ID="lblcomisiontotal" runat="server"  CssClass="form-control" Text=""></asp:Label>
            </div>

        </div>
        <!-- grid secundario -->
        <div class="row" id="gridse" runat="server" visible="false">
            <div class="col-lg-12">                
                <h2 style="text-align:center;"><strong>Detalle</strong></h2>
                <div class="table table-responsive">
                    <asp:GridView ID="grid_detalle" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" OnRowDataBound="grid_detalle_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="idc_prospecto" HeaderText="ID">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_razon_social" HeaderText="Razon Social">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion" HeaderText="Direccion">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Revisado">
                                <ItemTemplate>
                                    <asp:Label ID="lblrevisado" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="bono" HeaderText="Bono" DataFormatString="{0:C}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Registro">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
</asp:Content>
