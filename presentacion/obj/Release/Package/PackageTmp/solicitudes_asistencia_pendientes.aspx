<%@ Page Title="Autorizar" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="solicitudes_asistencia_pendientes.aspx.cs" Inherits="presentacion.solicitudes_asistencia_pendientes" %>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <h2 class=" page-header">Solicitudes de Asistencia Pendientes</h2>
    <asp:UpdatePanel ID="sss" runat="server" UpdateMode="always">

        <Triggers>
            <asp:PostBackTrigger ControlID="Yes" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <asp:TextBox ID="txtfiltrar" onfocus="this.select();" CssClass=" form-control2" placeholder="Buscar" Width="80%" AutoPostBack="true" runat="server" OnTextChanged="txtfiltrar_TextChanged"></asp:TextBox>
                    <asp:LinkButton ID="lbkbuscar" CssClass="btn btn-info" Width="18%" runat="server" OnClick="lbkbuscar_Click">Buscar</asp:LinkButton>
                    <br />
                    <asp:CheckBox Style="font-size: 20px; font-weight: 800;" ID="cbxselecttodos" Text="Seleccionar Todos"
                        CssClass="radio3 radio-check radio-info" AutoPostBack="true" runat="server" OnCheckedChanged="cbxselecttodos_CheckedChanged" />
                    <br />
                    <asp:LinkButton ID="lnkauto" CssClass="btn btn-success" Width="49%" runat="server" OnClick="lnkauto_Click">Autorizar Solicitud</asp:LinkButton>
                    <asp:LinkButton ID="lnkcanclar" CssClass="btn btn-danger" Width="49%" runat="server" OnClick="lnkcanclar_Click">Cancelar Solicitud</asp:LinkButton>
                    <br />
                    <br />
                    <div class="table table-responsive">

                        <asp:GridView Style="font-size: 10px; text-align: center;" ID="gridservicios"
                            DataKeyNames="idc_solicitudasi, fecha,idc_empleado, num_nomina,empleado,temprano"
                            CssClass=" gvv table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server" OnRowDataBound="gridservicios_RowDataBound" OnRowCommand="gridservicios_RowCommand">
                            <Columns>
                               
                                <asp:BoundField DataField="observaciones" HeaderText="Observaciones"></asp:BoundField>
                                 <asp:TemplateField HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:CheckBox AutoPostBack="true" OnCheckedChanged="cbx_CheckedChanged" ID="cbxselected" Text="Seleccionar" CssClass="radio3 radio-check radio-info" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="num_nomina" HeaderText="Nomina" HeaderStyle-Width="50px"></asp:BoundField>
                                 <asp:ButtonField ControlStyle-Font-Size="11px" ControlStyle-CssClass="btn btn-default btn-block" DataTextField="empleado" HeaderStyle-Width="280px" 
                                     CommandName="empleado" ButtonType="Button" HeaderText="Detalles Asistencia"></asp:ButtonField>

                                <asp:BoundField DataField="puesto" HeaderText="Puesto" HeaderStyle-Width="180px"></asp:BoundField>
                                <asp:BoundField DataField="depto" HeaderText="Depto" HeaderStyle-Width="180px"></asp:BoundField>
                                <asp:BoundField DataField="horario" HeaderText="Horario" HeaderStyle-Width="100px"></asp:BoundField>
                                <asp:BoundField DataField="hora_checo_real" HeaderText="Hora Checada" HeaderStyle-Width="100px"></asp:BoundField>
                                <asp:BoundField DataField="HORA_CHECK" HeaderText="Fecha Solicitud" HeaderStyle-Width="190px"></asp:BoundField>
                                <asp:BoundField DataField="usuario" HeaderText="Usuario" HeaderStyle-Width="100px"></asp:BoundField>
                                <asp:CheckBoxField DataField="temprano" ItemStyle-CssClass="radio3 radio-check radio-info" Text="Temprano" HeaderText="Temprano"
                                    HeaderStyle-Width="60px"></asp:CheckBoxField>
                                <asp:BoundField DataField="IDC_EMPLEADO" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                                <asp:BoundField DataField="idc_solicitudasi" HeaderText="idc_tiporep" Visible="FALSE"></asp:BoundField>
                                <asp:BoundField DataField="fecha" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="temprano" Visible="false"></asp:BoundField>

                            </Columns>
                        </asp:GridView>

                        <h4><strong style="color: orangered; text-align: right;">Total de Solicitudes:</strong>&nbsp;
                <strong>
                    <asp:Label ID="lbltotal" runat="server" Text="0"></asp:Label></strong>
                        </h4>

                    </div>
                </div>
            </div>
            <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
                        <ContentTemplate>

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
                                            <asp:TextBox ID="txtobservaciones" placeholder="Observaciones" CssClass=" form-control" TextMode="Multiline"
                                                Rows="4" Style="resize: none;" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server" id="error_modal" visible="false">
                                            <div class="alert fresh-color alert-danger alert-dismissible" role="alert">
                                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                <strong>ERROR&nbsp;</strong><asp:Label ID="lblerror" runat="server" Text="" Visible="true"></asp:Label>
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
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>

            </div>
            
                <div class="modal fade modal-info" id="myModalinfo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">


                        <div class="modal-content">
                            <div class="modal-header" style="text-align: center;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4><strong>Mensaje del Sistema</strong></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                        <label style="text-align: right; width: 18%; font-size: 20px"><strong>Empleado</strong></label>
                                        <asp:TextBox Style="font-size: 12px" ID="txtempleado" Width="80%" ReadOnly="true" CssClass="form-control2" runat="server"></asp:TextBox>
                                        <br />
                                        <label style="text-align: right; width: 18%; font-size: 20px"><strong>Status</strong></label>
                                        <asp:TextBox Style="font-size: 20px;text-align:center;" ID="txtstaus" Width="80%" ReadOnly="true" CssClass="form-control2" runat="server"></asp:TextBox>                                                                       
                                        <asp:TextBox Style="font-size: 20px;text-align:center;" ID="txtincid" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
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

                                        <asp:TextBox ID="txtidc_empleado" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtnumeronomina" runat="server" Visible="false"></asp:TextBox>

                                        <asp:LinkButton ID="LinkButton2" OnClientClick="ModalClose();" CssClass="btn btn-success btn-block" runat="server" OnClick="LinkButton2_Click">Ver Reporte de Asistencia</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="col-lg-12">
                                    <input id="Nop" type="button" class="btn btn-info btn-block" onclick="ModalClose();" value="Cerrar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
