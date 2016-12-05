<%@ Page Title="Reporte" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="empleados_incidencias_reporte.aspx.cs" Inherits="presentacion.empleados_incidencias_reporte" %>
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
     <h2 class=" page-header">Reportes(INCIDENCIAS) a Empleados</h2>
    <div class="row">
        <div class="col-lg-12">
            <label style="width:49%;font-size:18px;";><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicio</strong></label>
            <label style="width:49%;font-size:18px;"><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Fin</strong></label>
            <asp:TextBox Width="49%" TextMode="Date" CssClass=" form-control2" ID="txtfechainicio" runat="server"></asp:TextBox>
            <asp:TextBox Width="49%" TextMode="Date" CssClass=" form-control2" ID="txtfechafin" runat="server"></asp:TextBox>
            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-info btn-block"  runat="server" OnClick="LinkButton1_Click">Ver Reportes(Incidencias)</asp:LinkButton>
            <asp:LinkButton ID="lnkcerrar" Visible="false" CssClass="btn btn-danger btn-block"  runat="server"  OnClick="lnkcerrar_Click">Cerrar Ventana</asp:LinkButton>
            <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success"  runat="server" OnClick="LinkButton2_Click">Exportar a Excel&nbsp;<i class="fa fa-file-excel-o" aria-hidden="true"></i></asp:LinkButton>


            <div class="table table-responsive">
                <asp:GridView Style="font-size: 11px;" ID="gridservicios" DataKeyNames="idc_empleadorep,empleado,idc_empleado" 
                    CssClass=" gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server" OnRowCommand="gridservicios_RowCommand">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_buscar.png" HeaderText="Ver" CommandName="Editar">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="empleado" HeaderText="Empleado" HeaderStyle-Width="180px"></asp:BoundField>
                        <asp:BoundField DataField="puesto" HeaderText="Puesto" HeaderStyle-Width="90px"></asp:BoundField>
                        <asp:BoundField DataField="reporte" HeaderText="Reporte"></asp:BoundField>
                        <asp:BoundField DataField="observaciones" HeaderText="Descripcion"></asp:BoundField>
                        <asp:BoundField DataField="fecha_reporte" HeaderText="Fecha Reporte" HeaderStyle-Width="110px"></asp:BoundField>
                        <asp:BoundField DataField="empleado_vobo" HeaderText="Empleado VoBo" HeaderStyle-Width="110px"></asp:BoundField>
                        <asp:BoundField DataField="idc_empleadorep" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                        <asp:BoundField DataField="idc_empleado" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-lg-12" runat="server" id="concentrado" visible="false">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
                <ContentTemplate>

                    <asp:LinkButton ID="lnkconcentrado" Visible="false" CssClass="btn btn-info btn-block" runat="server" OnClick="lnkconcentrado_Click">Ver Reportes(Incidencias)</asp:LinkButton>
                    <asp:TextBox ID="txtbuscar" CssClass=" form-control" AutoPostBack="true"
                        placeholder="Buscar Empleado" runat="server" OnTextChanged="txtbuscar_TextChanged"></asp:TextBox>
                    <br />
                    <div class="list-group">
                        <a href="#" class="list-group-item active"><strong>Empleado</strong><span class="badge">Total Reportes</span></a>
                        <asp:Repeater ID="repeat_principal" runat="server" OnItemCommand="repeat_principal_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkview" CssClass="list-group-item" CommandName="View" CommandArgument='<%#Eval("idc_empleado") %>' runat="server">
                                    <%#Eval("empleado") %>
                                    <span class="badge"><%#Eval("total_reportes") %></span>                                  
                                </asp:LinkButton>
                                  <div class="table table-responsive" style="padding:10px;" runat="server" id="detalles" visible="false">
                                        <asp:GridView ID="griddet" DataKeyNames="idc_empleado,idc_tiporep" AutoGenerateColumns="false" CssClass="table table-responsive table-condensed table-bordered" 
                                            runat="server"  OnRowCommand="griddet_RowCommand">
                                            <HeaderStyle ForeColor="White" BackColor="Gray" />
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_buscar.png" HeaderText="Ver" CommandName="Editar">
                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="descripcion" HeaderText="Reporte"></asp:BoundField>
                                                <asp:BoundField DataField="total" HeaderText="Total" HeaderStyle-Width="50px"></asp:BoundField>
                                                <asp:BoundField DataField="idc_empleado" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="idc_tiporep" Visible="false"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
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
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
