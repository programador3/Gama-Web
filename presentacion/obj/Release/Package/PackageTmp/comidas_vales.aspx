<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="comidas_vales.aspx.cs" Inherits="presentacion.comidas_vales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Giftl(mensaje) {
            swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '6000', showConfirmButton: false });
            return true;
        }

        function ModalClose() {
            $('#myModalEmpleado').modal('hide');
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo,cContenido, ctype) {
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h2 class=" page-header"><strong>Generar Vales de Comida</strong></h2>
        </div>
        
        <div class="col-lg-12">
            <h4><strong>Seleccione un archivo de Excel, y genere los vales de comida de la fecha seleccionada</strong>
                <span>
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success" runat="server" OnClick="LinkButton1_Click">Descargar FORMATO DE EJEMPLO&nbsp;<i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton></span>
            </h4>

            <asp:FileUpload ID="fuparchivos" CssClass="form-control" runat="server" />
            <asp:LinkButton ID="lnk" runat="server" CssClass="btn btn-success btn btn-block" OnClick="lnk_Click" OnClientClick="return Giftl('ESTAMOS COMPROBANDO EL ARCHIVO DE EXCEL');">Subir Excel&nbsp;<i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
        </div>
        <div class="col-lg-12 col-sm-12" id="tabla_errores" runat="server" visible="false">
            <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success" runat="server" OnClick="LinkButton2_Click">Descargar ERRORES EN EXCEL&nbsp;<i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton>
            <div class="table table-responsive">
                <asp:GridView Style="text-align: center;" ID="grid_errores" CssClass="table table-responsive table-bordered table-condensed" runat="server" AutoGenerateColumns="False" >
                    <Columns>
                        <asp:BoundField DataField="num_nomina" HeaderText="NOMINA"></asp:BoundField>
                        <asp:BoundField DataField="descuento" HeaderText="DESCUENTO"></asp:BoundField>
                        <asp:BoundField DataField="error" HeaderText="ERROR"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-12" id="tabla" runat="server" visible="false">
            <asp:UpdatePanel ID="UpdatePanel1wswswsswwswsw" UpdateMode="Always" runat="server">
                <Triggers>
                    <asp:PostBackTrigger  ControlID="lnklimpirar"/>
                    <asp:PostBackTrigger  ControlID="yes"/>
                </Triggers>
                <ContentTemplate>
                    <h3 style="color: orangered;">Total de Descuento: <strong>
                        <asp:Label ID="lbltotal" runat="server" Text="" ForeColor="Black"></asp:Label></strong></h3>
                    <asp:LinkButton ID="lnksubir" CssClass="btn btn-primary" Width="49%" runat="server" OnClick="lnksubir_Click">Generar Vales</asp:LinkButton>
                    <asp:LinkButton ID="lnklimpirar" CssClass="btn btn-danger" Width="49%" runat="server" OnClick="lnklimpirar_Click">Limpiar Todo</asp:LinkButton>
                    <br />
                         <br />
                         <br />
                    <asp:TextBox ID="txtfiltro" style="float:right;"  onfocus="$(this).select();" AutoPostBack="true" CssClass="form-control2" Width="50%" placeholder="Buscar Nomina"
                         runat="server" OnTextChanged="txtfiltro_TextChanged"></asp:TextBox>
                         <br />
                     
                    <div class="table table-responsive">
                        <br />
                        <asp:GridView Style="text-align: center;" ID="grid_comidas" DataKeyNames="NOMINA" CssClass="table table-responsive table-bordered table-condensed" 
                            OnRowCommand="grid_errores_RowCommand" runat="server" AutoGenerateColumns="False" OnRowDataBound="grid_comidas_RowDataBound">
                            <HeaderStyle BackColor=" DimGray" ForeColor="White" HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="NOMINA" HeaderText="NUM. NOMINA"></asp:BoundField>                               
                                <asp:BoundField DataField="DESCUENTO" HeaderText="DESCUENTO" Visible="true"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
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
                                        <asp:Button ID="Button1" OnClientClick="ModalClose();" class="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="Button1_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade modal-info" id="myModalEmpleado" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">

                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="text-align: center;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4><strong>Mensaje del Sistema</strong></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row" style="text-align: center;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-lg-6 col-xs-6">
                                        <asp:Button ID="Button2" class="btn btn-info btn-block" runat="server" Text="Aceptar"/>
                                    </div>
                                    <div class="col-lg-6 col-xs-6">
                                        <asp:Button ID="Button3" OnClientClick="ModalClose();" class="btn btn-danger btn-block" runat="server" Text="Cancelar" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

  
</asp:Content>
