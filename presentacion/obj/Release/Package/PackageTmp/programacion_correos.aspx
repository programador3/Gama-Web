<%@ Page Title="Programacion Correos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="programacion_correos.aspx.cs" Inherits="presentacion.programacion_correos" %>

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
    <h1 class="page-header" style="text-align: center;">Programaciones de Correos Pendientes por Autorizar</h1>
    <div class="row">
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridtareas" DataKeyNames="idc_progracorreo,asunto" OnRowCommand="gridtareas_RowCommand" CssClass="gvv table table-responsive table-bordered table-condensed" runat="server" AutoGenerateColumns="False" ShowHeader="true">
                    <RowStyle HorizontalAlign="Center"></RowStyle>
                    <Columns>
                        <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-block" ItemStyle-Width="50px" Text="Ver Detalles" CommandName="Detalles" ButtonType="Button"></asp:ButtonField>
                        <asp:BoundField DataField="idc_progracorreo" HeaderText="idc_progracorreo" HeaderStyle-Width="300" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="asunto" HeaderText="Asunto"></asp:BoundField>
                        <asp:BoundField DataField="fecha_solicitud" HeaderText="Fecha Solicitud" HeaderStyle-Width="200"></asp:BoundField>
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" HeaderStyle-Width="60"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="modal_title"><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4>
                                <label id="content_modal"></label>
                            </h4>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>