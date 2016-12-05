<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="empleados_faltas.aspx.cs" Inherits="presentacion.empleados_faltas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
        function Gift(mensaje) {
            swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '800', showConfirmButton: false });
        }
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
                closeOnConfirm: false, allowEscapeKey: false
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h3 class="page-header">Empleados con Faltas Pendientes por Revisión</h3>
                </div>
            </div>
            <div class="row">
                <div class=" col-lg-12">
                    <div class="table table-responsive">
                        <asp:GridView Style="font-size: 11px;" ID="gridservicios" DataKeyNames="idc_empleado_falta,empleado,idc_empleado"
                            CssClass=" gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server" OnRowCommand="gridservicios_RowCommand" >
                            <Columns>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_buscar.png" HeaderText="Ver" CommandName="Editar">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="empleado" HeaderText="Empleado"></asp:BoundField>
                                <asp:BoundField DataField="puesto" HeaderText="Puesto" HeaderStyle-Width="180px"></asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" HeaderStyle-Width="150px"></asp:BoundField>
                                <asp:BoundField DataField="idc_empleado_falta" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                                <asp:BoundField DataField="idc_empleado" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <asp:Repeater ID="repeat_pendi" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <asp:LinkButton ID="lnkir" runat="server" CssClass="btn btn-info btn-block" CommandName='<%#Eval("idc_empleado_falta") %>' OnClick="lnkir_Click">
                                  <h5><%#Eval("puesto") %></h5>
                                  <h5><%#Eval("empleado") %></h5>
                                  <h5><%#Eval("fecha") %></h5>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>