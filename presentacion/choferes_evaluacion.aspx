<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="choferes_evaluacion.aspx.cs" Inherits="presentacion.choferes_evaluacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h3 class=" page-header">Evaluación de Choferes</h3>
            <div class="row ">
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <h5>Seleccione un Mes</h5>
                    <asp:DropDownList CssClass="form-control" ID="ddlmeses" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <h5>Seleccione un Año</h5>
                    <asp:DropDownList CssClass="form-control" ID="ddlaño" runat="server">
                    </asp:DropDownList>
                </div>

                <div class="col-lg-12">
                    <asp:LinkButton ID="LinkButton1" OnClick="lnkbuscar_Click" CssClass="btn btn-danger" Width="100%" runat="server">Ejecutar Reporte</asp:LinkButton>
                </div>
                <div class="col-lg-12">
                    <asp:LinkButton ID="LinkButton2" Visible="false" CssClass="btn btn-success" runat="server" OnClick="LinkButton2_Click">Exporta a Excel</asp:LinkButton>
                    <div class="table table-responsive">
                        <asp:GridView DataKeyNames="EMPLEADO" ID="gridchoferes" style="text-align:center;" runat="server" CssClass="gvv table table-responsive table-bordered table-condensed" OnRowCommand="gridchoferes_RowCommand">
                            <HeaderStyle ForeColor="White" BackColor="DimGray" />
                            <Columns>
                                <asp:TemplateField HeaderText="" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" CssClass="btn btn-info btn-block" runat="server" Text="Detalles"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
    <asp:TextBox ID="txtguid" Visible="false" runat="server"></asp:TextBox>
</asp:Content>
