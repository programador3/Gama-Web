<%@ Page Title="Informacion Adicional" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_informacion_adicional.aspx.cs" Inherits="presentacion.tareas_informacion_adicional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="//cdn.datatables.net/1.10.7/css/jquery.dataTables.css" rel="stylesheet" />
    <script src="//cdn.datatables.net/1.10.7/js/jquery.dataTables.js"></script>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    $(".dt").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
        //        "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
        //    });
        //});

        function DataTa1(value) {
            $(value).DataTable();
        }
        function DataTa2(value) {
            $(value).DataTable();
        }
        function DataTa3(value) {
            $(value).DataTable();
        }
        function DataTa4(value) {
            $(value).DataTable();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header" style="text-align: center;">Información Adicional <span>
          <asp:Button ID="close" runat="server" Text="Cerrar Reporte" OnClick="close_Click" CssClass="btn btn-danger" ToolTip="Limpiar" />
                                                                              </span></h1>
    <asp:Repeater ID="repeat" runat="server" OnItemDataBound="repeat_ItemDataBound">
        <ItemTemplate>
            <asp:UpdatePanel ID="aa" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkexport" />
                </Triggers>
                <ContentTemplate>
                    <div>
                        <asp:HiddenField ID="cursornumber" runat="server" />
                        <asp:Panel ID="textbox" runat="server" Visible="false" CssClass="row">
                            <div class="col-lg-12">
                                <h4><strong>
                                    <asp:Label ID="lbltextname" runat="server" Text=""></asp:Label></strong></h4>
                                <asp:TextBox ID="txtdesc" ReadOnly="true" TextMode="MultiLine" Rows="4" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="gridview" Visible="false" CssClass="row" runat="server">
                            <div class="col-lg-12">
                                <h4><strong>
                                    <asp:Label ID="lblgridname" runat="server" Text=""></asp:Label></strong><span>
                                        <asp:LinkButton ID="lnkexport" runat="server" CssClass="btn btn-success" OnClick="lnkexport_Click">Exportar a Excel <i class="fa fa-file-excel-o" aria-hidden="true"></i></asp:LinkButton></span></h4>
                                <div class="table table-responsive">
                                    <asp:GridView ID="grid" CssClass="gvv table table-responsive table-bordered table-condensed" runat="server" Visible="false">
                                    </asp:GridView>
                                    <asp:PlaceHolder ID="PlaceHolder" runat="server" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>