<%@ Page Title="Pendientes" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="permisos_pendientes.aspx.cs" Inherits="presentacion.permisos_pendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Giftp(mensaje) {
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
        function AsignaProgress(value) {
            $('#pavance').width(value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header" style="text-align: center;">Permisos de Cambio de Horarios Pendientes</h1>
    <asp:UpdatePanel ID="upda" runat="server" UpdateMode="Always">
        <Triggers>
            
            <asp:PostBackTrigger ControlID="Yes" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class=" col-lg-12">    
                    <asp:LinkButton ID="lnkauto" CssClass="btn btn-success" Width="49%" runat="server" OnClick="lnkauto_Click">Autorizar Solicitud</asp:LinkButton>
                    <asp:LinkButton ID="lnkcanclar" CssClass="btn btn-danger" Width="49%" runat="server" OnClick="lnkcanclar_Click">Cancelar Solicitud</asp:LinkButton>  
                    <br />
                    <asp:CheckBox Style="font-size: 20px; font-weight: 800;" ID="cbxselecttodos" Text="Seleccionar Todos"
                        CssClass="radio3 radio-check radio-info" AutoPostBack="true" runat="server" OnCheckedChanged="cbxselecttodos_CheckedChanged" />
                   
                </div>
                <div class="col-lg-12">
                     <h5><strong>Filtrar</strong></h5>
                    <asp:TextBox ID="txtfiltrar" onfocus="this.select();" CssClass=" form-control2" Width="80%" placeholder="Escriba un valor para filtrar" AutoPostBack="true" runat="server" OnTextChanged="txtfiltrar_TextChanged"></asp:TextBox>
                    <asp:LinkButton ID="lbkbuscar" CssClass="btn btn-info" Width="18%" runat="server" OnClick="txtfiltrar_TextChanged">Buscar</asp:LinkButton>
                    <h4><strong style="color: orangered; text-align: right;">Total de Solicitudes:</strong>&nbsp;
                <strong>
                    <asp:Label ID="lbltotal" runat="server" Text="0"></asp:Label></strong>
                        </h4>
                    <div class="table table-responsive">
                        <asp:GridView ID="gridcelulares" DataKeyNames="idc_horario_perm,idc_puesto" CssClass="table table-responsive table-bordered table-condensed table-responsive" runat="server"
                            AutoGenerateColumns="false" OnRowCommand="gridcelulares_RowCommand" OnRowDataBound="gridcelulares_RowDataBound">
                            <Columns>
                                <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-block" ItemStyle-Width="50px" Text="Detalles" CommandName="Detalles" ButtonType="Button"></asp:ButtonField>
                                <asp:TemplateField HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:CheckBox AutoPostBack="true" OnCheckedChanged="cbx_CheckedChanged" ID="cbxselected" Text="Seleccionar"
                                            CssClass="radio3 radio-check radio-info" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="idc_horario_perm" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="idc_puesto" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="empleado" HeaderText="Empleado" HeaderStyle-Width="150px"></asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha para aplicar" HeaderStyle-Width="150px"></asp:BoundField>
                                <asp:BoundField DataField="empleado_solicito" HeaderText="Solicito" HeaderStyle-Width="150px"></asp:BoundField>
                                <asp:BoundField DataField="observaciones" HeaderText="Motivo" HeaderStyle-Width="200px"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>