<%@ Page Title="Respaldo de Manuales" Language="C#" MasterPageFile="~/Adicional.Master" AutoEventWireup="true" CodeBehind="respaldo_manuales.aspx.cs" Inherits="presentacion.respaldo_manuales" %>
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
    <div class="row">
        <div class=" col-lg-12">
            <h3 class=" page-header">Respaldo de Manuales</h3>
        </div>
        <div class=" col-lg-12">
            <div class="alert fresh-color alert-info alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <strong>Nota&nbsp;</strong>Cada dos Meses, EL SISTEMA ELIMINA EL RESPALDO.
            </div>
            <div class="alert fresh-color alert-info alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <strong>Nota&nbsp;</strong>Los Archivos se Guarda con el siguiente formato: NUMERO AL AZAR+USUARIO+FECHA DE CREACION+.HTML
            </div>
        </div>
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="grid_archivos" DataKeyNames="ruta,archivo" CssClass="gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server" OnRowCommand="grid_archivos_RowCommand">

                    <Columns>
                          <asp:ButtonField ButtonType="Button" Text="Ver Archivo" ControlStyle-CssClass="btn btn-info btn-block"  HeaderText="Ver" 
                              CommandName="Solicitar">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                          <asp:ButtonField ButtonType="Button" Text="Descargar" ControlStyle-CssClass="btn btn-success btn-block"  HeaderText="Descargar" 
                              CommandName="Descargar">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="archivo" HeaderText="Archivo"></asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha Creacion" HeaderStyle-Width="30%"></asp:BoundField>
                        <asp:BoundField DataField="ruta" HeaderText="ruta" Visible="false"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
