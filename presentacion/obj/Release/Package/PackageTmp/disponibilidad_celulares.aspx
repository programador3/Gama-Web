<%@ Page Title="Disponibilidad" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="disponibilidad_celulares.aspx.cs" Inherits="presentacion.disponibilidad_celulares" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function getImage(path) {
            $("#myImage").attr("src", path);
            //alert(path);
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            },
               function () {
                   location.href = URL;
               });
        }
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
        function AsignaProgress(value) {
            $('#pavance').width(value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header" style="text-align: center;">Reporte de Celulares</h1>
    <div class="row">
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridcelulares" CssClass="gvv table table-responsive table-bordered" runat="server" AutoGenerateColumns="false" OnRowDataBound="gridcelulares_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Disponible" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Image ID="check" runat="server" ImageUrl='<%#Eval("img") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="descripcion" HeaderText="Modelo" HeaderStyle-Width="150px"></asp:BoundField>
                        <asp:BoundField DataField="imei" HeaderText="IMEI" HeaderStyle-Width="80px"></asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Telefono" HeaderStyle-Width="80px"></asp:BoundField>

                        <asp:BoundField DataField="estado" HeaderText="Estado"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>