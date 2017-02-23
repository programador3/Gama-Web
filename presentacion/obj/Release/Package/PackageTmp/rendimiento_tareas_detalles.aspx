<%@ Page Title="Rendimiento" Language="C#" MasterPageFile="~/Adicional.Master" AutoEventWireup="true" CodeBehind="rendimiento_tareas_detalles.aspx.cs" Inherits="presentacion.rendimiento_tareas_detalles" %>

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
    <asp:Label ID="lblsession" runat="server" Text="" Visible="false"></asp:Label>
    <h3 class="page-header" style="text-align: center;">
        <strong><asp:Label ID="lblhead" runat="server" Text=""></asp:Label></strong></h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkexcel" />
            <asp:PostBackTrigger ControlID="lnkpdf" />
            <asp:PostBackTrigger ControlID="lnkmostrar" />
            <asp:PostBackTrigger ControlID="gridPapeleria" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="panel_repeat" runat="server">
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <asp:LinkButton Visible="true" ID="lnkexcel" CssClass="btn btn-success btn-block" runat="server" OnClick="lnkexcel_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i> &nbsp;Exportar a Excel</asp:LinkButton>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <asp:LinkButton Visible="false" ID="lnkpdf" CssClass="btn btn-danger btn-block" runat="server" OnClick="lnkpdf_Click"><i class="fa fa-file-pdf-o" aria-hidden="true"></i> &nbsp;Exportar a PDF</asp:LinkButton>
                    </div>
                    <div class="col-lg-12">
                        <div class="table table-responsive">
                            <asp:GridView style="font-size:11px;" ID="gridtareas" DataKeyNames="idc_tarea,desc_completa,descripcion" OnRowCommand="gridtareas_RowCommand" 
                                CssClass="gvv table table-responsive table-bordered table-condensed" OnRowDataBound="gridtareas_RowDataBound" 
                                runat="server" AutoGenerateColumns="False" ShowHeader="true">
                                <RowStyle HorizontalAlign="Center"></RowStyle>
                                <Columns>
                                    <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-block" ItemStyle-Width="50px" Text="Detalles" CommandName="Detalles" ButtonType="Button"></asp:ButtonField>
                                    <asp:ButtonField ControlStyle-CssClass="btn btn-success btn-block" Visible="true" ItemStyle-Width="50px" Text="Arbol" CommandName="Arbol" ButtonType="Button"></asp:ButtonField>
                                    <asp:BoundField DataField="idc_tarea" Visible="false"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Tarea">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Text='<%#Eval("descripcion") %>' ToolTip='<%#Eval("desc_completa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="desc_completa" HeaderText="Tarea" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="puesto_realizo" HeaderText="Realizo"></asp:BoundField>
                                    <asp:BoundField DataField="puesto_solicito" HeaderText="Solicito"></asp:BoundField>
                                    <asp:BoundField DataField="fecha_registro" HeaderText="F. Registro" HeaderStyle-Width="150px"></asp:BoundField>
                                    <asp:BoundField DataField="fecha_compromiso" HeaderText="F. Compromiso" HeaderStyle-Width="150px"></asp:BoundField>
                                    <asp:BoundField DataField="fecha_terminado" HeaderText="F. Terminado" HeaderStyle-Width="150px"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Externo" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Image ID="externo" runat="server" Width="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="avance" HeaderText="% Avance" HeaderStyle-Width="50px"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Estado" HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Image ID="edo" runat="server" Width="20px" ImageUrl="~/imagenes/btn/inchecked.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R. Solicitante" HeaderStyle-Width="60px" Visible="False">
                                        <ItemTemplate>
                                            <asp:Image ID="r_usuarios" runat="server" Width="20px" ImageUrl="~/imagenes/btn/inchecked.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resultado Sistema" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Image ID="r_sistema" runat="server" Width="20px" ImageUrl="~/imagenes/btn/inchecked.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="col-lg-12">
                                <table class="table table-responsive table-bordered">
                                    <tr>
                                        <td style="width: 30%">
                                            <asp:Image Width="30px" ID="externo" runat="server" ImageUrl="~/imagenes/ok.png" />
                                            <label style="font-size:18px;"><strong>Terminada</strong></label>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Image Width="30px" ID="Image1" runat="server" ImageUrl="~/imagenes/cogs.png" /> 
                                            <label style="font-size:16px;"><strong>En Proceso</strong></label>                                       </td>
                                        <td style="width: 30%; color:black;background-color:#ef9a9a;">
                                            <asp:Image Width="30px" ID="Image2" runat="server" ImageUrl="~/imagenes/btn/cancel.png" />
                                            <label style="font-size:18px;"><strong>Cancelada</strong></label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="panel_detalles" runat="server" Visible="false">
                <h2 style="text-align: center;">Detalles de la Tarea "<asp:Label ID="lblheaddet" runat="server" Text="" Style="font-style: italic;"></asp:Label>"</h2>
                <div class="row">
                    <div class="col-lg-12">
                        <h4><i class="fa fa-random" aria-hidden="true"></i>&nbsp;Avance de la Tarea </h4>
                        <asp:Label ID="lblprogress" runat="server" Text=""></asp:Label>
                        <div class="progress">
                            <div id="pavance" class="progress-bar progress-bar-success" role="progressbar" aria-valuemin="0" aria-valuemax="100">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="table table-responsive">
                            <asp:GridView ID="grid_detalles" CssClass="table table-responsive table-bordered table-condensed" runat="server" AutoGenerateColumns="False" ShowHeader="true">
                                <Columns>
                                    <asp:BoundField DataField="tipo" HeaderText="Movimiento"></asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Comentarios"></asp:BoundField>
                                    <asp:BoundField DataField="fecha_compromiso" HeaderText="Fecha Compromiso"></asp:BoundField>
                                    <asp:BoundField DataField="fecha_terminado" HeaderText="Fecha Terminado"></asp:BoundField>
                                    <asp:BoundField DataField="fecha_movimiento" HeaderText="Fecha Movimiento"></asp:BoundField>
                                    <asp:BoundField DataField="puesto_movimiento" HeaderText="Puesto Movimiento"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-lg-12" id="proveedores" runat="server">
                        <div class="table table-responsive">
                            <h3 style="text-align: center;"><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Proveedores Relacionados</h3>
                            <asp:GridView ID="gridproveedires" CssClass="table table-responsive table-bordered table-condensed" runat="server" AutoGenerateColumns="False" ShowHeader="true">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Proveedor"></asp:BoundField>
                                    <asp:BoundField DataField="observaciones" HeaderText="Comentarios"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-lg-12" id="comentarios" runat="server">
                        <div class="table-responsive">
                            <h3 style="text-align: center;"><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Archivos o Comentarios</h3>
                            <asp:GridView ID="gridPapeleria" OnRowDataBound="gridPapeleria_RowDataBound" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="archivo,idc_tarea_archivo,descripcion, ruta, extension">
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="extension" HeaderText="extension" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Comentario"></asp:BoundField>
                                    <asp:BoundField DataField="idc_tarea_archivo" HeaderText="id_archi" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="archivo" HeaderText="id_archi" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="empleado" HeaderText="Empleado" Visible="true"></asp:BoundField>
                                    <asp:BoundField DataField="puesto" HeaderText="Puesto" Visible="true"></asp:BoundField>
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" Visible="true"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-lg-12 col-xs-12">
                        <asp:LinkButton ID="lnkmostrar" CssClass="btn btn-danger btn-block" runat="server" OnClick="lnkmostrar_Click">Mostrar Todas las Tareas</asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <input runat="server" type="hidden" id="H_casoFiltor" />
</asp:Content>