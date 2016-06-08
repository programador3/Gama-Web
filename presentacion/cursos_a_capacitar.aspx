<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos_a_capacitar.aspx.cs" Inherits="presentacion.cursos_a_capacitar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--   DataTable--%>

    <script src="js/bootbox.min.js"></script>
    <style>
        .p-borrador {
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
    <script type="text/javascript">
        function Return(cTitulo) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                allowEscapeKey: false
            },
               function () {
                   location.href = URL;
               });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">

            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Contactar Empleado</h1>
                </div>
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <h4>Contacte al Pre Emplado para confirmar la fecha de ingreso. Si el empleado aplica a un curso este se asignara en automatico, en caso contrario el proceso de ingreso continuara.</h4>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="table-responsive">
                        <div class="panel panel-primary ">
                            <div id="panel-head1" class="p-borrador" style="text-align: center">
                                <h3 class="panel-title">Cursos <i class="fa fa-list"></i></h3>
                            </div>
                            <div class="panel panel-body">
                                <div class="table table-responsive">
                                    <asp:GridView ID="grid_cursos_pre_alta_pendientes" CssClass="gvv table table-bordered table-hover grid sortable {disableSortCols: [2]}" runat="server" AutoGenerateColumns="False" DataKeyNames="idc_pre_empleado" OnRowCommand="grid_cursos_pre_alta_pendientes_RowCommand" OnRowDataBound="grid_cursos_pre_alta_pendientes_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Programar" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnprogramar" runat="server" CausesValidation="false" CommandName="clic_programar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/imagenes/btn/icon_autorizar.png" Text="Checklist" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="idc_pre_empleado" HeaderText="Idc_pre_empleado" Visible="False">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="pre_empleado" HeaderText="Candidato (pre empleado)">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="total" HeaderText="Candidatos Restantes">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!--mensaje oculto -->
                                <asp:Panel ID="panel_mensaje" runat="server" Visible="False" CssClass="row">
                                    <div class="col-lg-12">
                                        <h2>No tiene pendientes</h2>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
            <!-- /.CONFIRMA -->
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbtnlist_programar_rechazar" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row" style="text-align: left;">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <label>Pre Emplado:</label>
                                                <asp:Label ID="modal_pre_empleado" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <label>Puesto:</label>
                                                <asp:Label ID="modal_lblpuesto" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <label>Cursos:</label>

                                                <asp:BulletedList ID="lista_cursos" CssClass="table-bordered" runat="server"></asp:BulletedList>
                                            </div>
                                            <div class="form-group">
                                                <label>Correo:</label>
                                                <asp:Label ID="modal_lblcorreo" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <label>Telefonos:</label>
                                                <asp:BulletedList ID="lista_tels" runat="server" CssClass="table-bordered"></asp:BulletedList>
                                            </div>
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="rbtnlist_programar_rechazar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnlist_programar_rechazar_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Programar Candidato</asp:ListItem>
                                                    <asp:ListItem Value="0">Rechazar Candidato</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="form-group" id="fecha" runat="server">
                                                <label><strong>Fecha Acordada de Ingreso:</strong></label>
                                                <asp:TextBox ID="txtfecha_tentativa" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group" id="comentarios" runat="server" visible="false">
                                                Comentarios:
                                          <asp:TextBox ID="txtComentarios" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Button ID="modal_btnaceptar" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="modal_btnaceptar_Click" />
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Button ID="modal_btncancelar" class="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="modal_btncancelar_Click" />
                                </div>
                            </div>

                            <!--campos ocultos -->
                            <asp:HiddenField ID="oc_modal_idc_curso" runat="server" />
                            <asp:HiddenField ID="oc_modal_idc_puesto" runat="server" />
                            <asp:HiddenField ID="oc_modal_idc_pre_empleado" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="js/bootstrap-switch-original.js"></script>
    </div>
</asp:Content>