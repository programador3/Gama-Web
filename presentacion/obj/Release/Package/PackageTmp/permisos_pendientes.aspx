<%@ Page Title="Pendientes" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="permisos_pendientes.aspx.cs" Inherits="presentacion.permisos_pendientes" %>

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
    <h1 class="page-header" style="text-align: center;">Permisos de Cambio de Horarios Pendientes</h1>
    <div class="row">
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridcelulares" DataKeyNames="idc_horario_perm,idc_puesto" CssClass="gvv table table-responsive table-bordered" runat="server" AutoGenerateColumns="false" OnRowCommand="gridcelulares_RowCommand">
                    <Columns>
                        <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-block" ItemStyle-Width="50px" Text="Detalles" CommandName="Detalles" ButtonType="Button"></asp:ButtonField>
                        <asp:BoundField DataField="idc_horario_perm" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="idc_puesto" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="empleado" HeaderText="Empleado" HeaderStyle-Width="150px"></asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha para aplicar" HeaderStyle-Width="150px"></asp:BoundField>
                        <asp:BoundField DataField="empleado_solicito" HeaderText="Solicito" HeaderStyle-Width="150px"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>