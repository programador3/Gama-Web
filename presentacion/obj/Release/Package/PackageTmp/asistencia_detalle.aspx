<%@ Page Title="Asistencia" Language="C#" MasterPageFile="~/Adicional.Master" AutoEventWireup="true" CodeBehind="asistencia_detalle.aspx.cs" Inherits="presentacion.asistencia_detalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
        function Gift(mensaje) {
            swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '800', showConfirmButton: false });
        }
        function ModalClose() {
            $('#myModal').modal('hide');
            $('#myModalinfo').modal('hide');
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
        function ModalConfirms(ctype) {
            $('#myModalinfo').removeClass();
            $('#myModalinfo').addClass(ctype);
            $('#myModalinfo').modal('show');
        }
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class="header text-center"><strong>Asistencia</strong></h2>
    <div class=" row">
        <div class=" col-lg-12">
             <div class="row">
                <div class="col-lg-2" style="align-content: center;">
                    <a>
                        <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 120px; margin: 0 auto;" />
                    </a>
                </div>
                <div class="col-lg-10" style="text-align: left">
                    <h4>
                        <strong>Nombre Empleado: </strong>
                        <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                    </h4>
                    <h4><strong>Puesto: </strong>
                        <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                    </h4>
                    <h4><strong>Depto: </strong>
                        <asp:Label ID="lbldepto" runat="server" Text=""></asp:Label>
                    </h4>
                </div>
            </div>
        </div>
        <div class=" col-lg-12">
            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-danger btn-block" OnClientClick="window.close();" runat="server">Cerrar Reporte</asp:LinkButton>
            <br />
            <div class="table table-responsive">
                <asp:GridView Style="font-size: 11px; text-align: center;" ID="gridservicios"
                    DataKeyNames="fecha"
                    CssClass=" gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server" OnRowCommand="gridservicios_RowCommand">
                    <Columns>
                        <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-block" Text="Detalle" HeaderStyle-Width="50px"
                            CommandName="empleado" ButtonType="Button"></asp:ButtonField>
                        <asp:BoundField DataField="fecha_str" HeaderText="Dia | Fecha"></asp:BoundField>   
                   
                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Asis. Manual">
                            <ItemTemplate>
                                <asp:CheckBox Checked='<%#Convert.ToInt32(Eval("asis_manual")) == 0 ? false : true %>' Enabled="false" ID="cbxselected" Text="Asis. Manual" 
                                    CssClass="radio3 radio-check radio-info" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="llegadatemprano" ItemStyle-CssClass="radio3 radio-check radio-info" Text="Temprano" HeaderText="Temprano"
                            HeaderStyle-Width="80px"></asp:CheckBoxField>
                        <asp:BoundField DataField="sucursal" HeaderText="Sucursal" HeaderStyle-Width="250px"></asp:BoundField>  
                        <asp:BoundField DataField="fecha" Visible="false"></asp:BoundField> 
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="modal fade modal-info" id="myModalinfo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">


            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gridservicios" EventName="RowCommand" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">                            
                            <label style="text-align: right; width: 18%; font-size: 20px"><strong>Status</strong></label>
                            <asp:TextBox Style="font-size: 20px;text-align:center;" ID="txtstaus" Width="80%" ReadOnly="true" CssClass="form-control2" runat="server"></asp:TextBox>
                            <h4 id="nohay" runat="server" style="text-align: center;" visible="false"><strong>No hay Detalles de Asistencia</strong></h4>
                            <br />
                            <div class=" table table-responsive">
                                <asp:GridView ID="grid" CssClass="table table-responsive table-bordered table-condensed"
                                    AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="fecha_str" HeaderText="Dia | Hora" HeaderStyle-Width="50%"></asp:BoundField>
                                        <asp:BoundField DataField="softkey" HeaderText="Tipo" HeaderStyle-Width="10%"></asp:BoundField>
                                        <asp:BoundField DataField="sucursal" HeaderText="Sucursal" HeaderStyle-Width="30%"></asp:BoundField>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo" HeaderStyle-Width="10%"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-12">
                        <input id="Nop" type="button" class="btn btn-info btn-block" onclick="ModalClose();" value="Cerrar" />
                    </div>
                </div>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
