<%@ Page Title="SolicitarAsistencias" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="solicitar_asistencia.aspx.cs" Inherits="presentacion.solicitar_asistencia" %>
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
        function ModalConfirm(cTitulo, ctype) {
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
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
        function TablaGVV() {
            $(".gvv").DataTable();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <h2 class=" page-header">Solicitar Asistencia</h2>
    <div class="row">
        <div class="col-lg-12">
            <asp:TextBox ID="txtfecha"  TextMode="Date" CssClass=" form-control2" Width="73%" runat="server"></asp:TextBox>
            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-danger" Width="25%" runat="server" OnClick="LinkButton1_Click">Ejecutar</asp:LinkButton>
                <label id="lblsolohoy" runat="server" visible="false" style="color:red"><strong>Solo puede solicitar asistencia de hoy</strong></label>
            <div class=" table table-responsive">
                <asp:GridView Style="font-size: 11px;text-align:center;" ID="gridservicios" 
                    DataKeyNames="IDC_EMPLEADO,IDC_PUESTO,num_nomina,empleado,puesto,depto,horario,hora_asistencia,asistencia,hora_checo_real"
                    CssClass=" gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server" OnRowCommand="gridservicios_RowCommand">
                 
                    <Columns>
                        <asp:TemplateField HeaderText="Solicitar" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="TablaGVV();" Visible='<%#Eval("btn").ToString() == "" ? false : true %>'
                                    Width="30px" ImageUrl='<%#Eval("btn") %>' CommandName="Puestos" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:ButtonField ControlStyle-Font-Size="11px" ControlStyle-CssClass="btn btn-default btn-block" Text="Reporte Asist." HeaderStyle-Width="100px"
                            CommandName="asistencia" ButtonType="Button"></asp:ButtonField>
                        <asp:BoundField DataField="num_nomina" HeaderText="Nomina" HeaderStyle-Width="30px"></asp:BoundField>
                        <asp:BoundField DataField="empleado" HeaderText="Empleado"></asp:BoundField>
                        <asp:BoundField DataField="puesto" HeaderText="Puesto" HeaderStyle-Width="180px"></asp:BoundField>
                        <asp:BoundField DataField="depto" HeaderText="Depto" HeaderStyle-Width="180px"></asp:BoundField>
                        <asp:BoundField DataField="horario" HeaderText="Horario" HeaderStyle-Width="50px"></asp:BoundField>
                        <asp:BoundField DataField="hora_checo_real" HeaderText="Hora" HeaderStyle-Width="50px"></asp:BoundField>
                        <asp:CheckBoxField DataField="bien" ItemStyle-CssClass="radio3 radio-check radio-info" Text="Asistencia" HeaderText="Asistencia" HeaderStyle-Width="60px"></asp:CheckBoxField>
                        <asp:CheckBoxField DataField="amanual" ItemStyle-CssClass="radio3 radio-check radio-info" Text="Manual" HeaderText="Manual" HeaderStyle-Width="40px"></asp:CheckBoxField>
                        <asp:CheckBoxField DataField="aviso" ItemStyle-CssClass="radio3 radio-check radio-info" Text="Aviso" HeaderText="Aviso" HeaderStyle-Width="40px"></asp:CheckBoxField>
                        <asp:CheckBoxField DataField="solicitud" ItemStyle-CssClass="radio3 radio-check radio-info" Text="Solicitud" HeaderText="Solicitud" HeaderStyle-Width="50px"></asp:CheckBoxField>
                        <asp:BoundField DataField="IDC_EMPLEADO" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                        <asp:BoundField DataField="IDC_PUESTO" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                        <asp:BoundField DataField="hora_asistencia" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                        <asp:BoundField DataField="asistencia" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>

                    </Columns>
                </asp:GridView>
            </div>
            <h4><strong style="color:orangered;">Total de Empleados</strong>&nbsp;
                <strong><asp:Label ID="lbltotal" runat="server" Text="0"></asp:Label></strong>
            </h4>
        </div>
    </div>
    <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">

        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gridservicios" EventName="RowCommand" />
                </Triggers>
                <ContentTemplate>
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="modal_title"><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 " style="font-size: 11px;">
                            <label style="width: 20%; text-align: right; font-size: 11px;"><strong>Nomina</strong></label>
                            <asp:TextBox Style="font-size: 11px;" ID="txtnomina" CssClass=" form-control2" Width="40%" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:TextBox Style="font-size: 11px;" ID="txtfechaview" CssClass=" form-control2" Width="38%" ReadOnly="true" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <label style="width: 20%; text-align: right;"><strong>Empleado</strong></label>
                            <asp:TextBox Style="font-size: 11px;" ID="txtempleado" CssClass=" form-control2" Width="78%" ReadOnly="true" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:TextBox Style="font-size: 11px;" ID="txtmotivo" CssClass=" form-control2" Width="98%" Height="60px" placeholder="Motivo" TextMode="Multiline" Rows="3"
                                ReadOnly="false" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <label id="lblhora" runat="Server" style="width: 20%; text-align: right;"><strong>Hora de Checada del Empleado.</strong></label>
                            <asp:TextBox Style="font-size: 11px;" ID="txthora" CssClass=" form-control2" Width="78%" ReadOnly="false" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
                            <asp:CheckBox ID="cbxllegadatemprano" CssClass="radio3 radio-check radio-info radio-inline" Text="LLEGADA TEMPRANO" runat="server" />

                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server" id="error_modal" visible="false">
                            <div class="alert fresh-color alert-danger alert-dismissible" role="alert">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <strong>ERROR&nbsp;</strong><asp:Label ID="lblerror" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        </div>
                        
                        <asp:TextBox ID="txtidc_empleado" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txthoracheco" runat="server" Visible="false"></asp:TextBox>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Button1" OnClientClick="ModalClose();" class="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="Button1_Click" />
                    </div>
                </div>
            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
