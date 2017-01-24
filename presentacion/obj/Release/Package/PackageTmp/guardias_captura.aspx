<%@ Page Title="Captura" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="guardias_captura.aspx.cs" Inherits="presentacion.guardias_captura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">Captura Descansos Sabados</h2>
        </div>
        <div class="col-lg-12">
            <h4 style="color: red;"><strong>NOTAS IMPORTANTE: EL EXCEL DEBE CONTENER EL FORMATO CORRECTO</strong></h4>
            <h4 style="color: red;"><strong>LA FECHA DEBE SER EN EL FORMATO YYYY-MM-DD. EJEMPLO: 2017-01-14(14 DE ENERO DEL 2017)</strong></h4>
            <h4 style="color: red;"><strong>LA FECHA DEBE SER SOLO SABADOS Y MAYORES A HOY</strong></h4>
             <h4 style="color: red;"><strong>NO PUEDE EXISTIR DOS REGISTROS DE UN MISMO EMPLEADO EN UNA MISMA FECHA</strong></h4>
            <h4><strong>Puede descargar el formato de Excel </strong>
                <span>
                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success" runat="server" OnClick="Linkdown_Click">
                   Descargar FORMATO DE EJEMPLO&nbsp;<i class="fa fa-download" aria-hidden="true"></i>

                    </asp:LinkButton></span>
            </h4>
        </div>
        <div class="col-lg-12">
            <h5><strong>Seleccione o arrastre un archivo de Excel para capturar los descansos de los sabados</strong>
            </h5>
            <asp:FileUpload ID="fuparchivo" CssClass="form-control2" Width="100%" runat="server" />
            <asp:LinkButton ID="lnksubirarchivo" CssClass="btn btn-info" Width="100%" runat="server" OnClick="lnksubirarchivo_Click">
                 Subir&nbsp;<i class="fa fa-upload" aria-hidden="true"></i>
            </asp:LinkButton>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:LinkButton ID="lnksubir" CssClass="btn btn-primary btn-block" OnClick="lnksubir_Click" runat="server">
                Guardar Listado
            </asp:LinkButton>
        </div>

        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
        </div>
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="grid_descansos" CssClass="table table-responsive table-bordered table-condensed" runat="server" AutoGenerateColumns="False">

                    <Columns>
                        <asp:BoundField DataField="nomina" HeaderText="Numero de Nomina"></asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha Aplicación"></asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-lg-12" id="errores" runat="server" visible="false">
            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success" runat="server" OnClick="LinkButton1_Click">Descargar En Excel</asp:LinkButton>
            <div class="table table-responsive">
                <asp:GridView ID="griderrores" CssClass="table table-responsive table-bordered table-condensed" runat="server" AutoGenerateColumns="False">

                    <Columns>
                        <asp:BoundField DataField="nomina" HeaderText="Numero de Nomina"></asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha Aplicación"></asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
                        <asp:BoundField DataField="error" HeaderText="Error"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
     <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
         <div class="modal-dialog">
             <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="always">
                 <Triggers>
                     <asp:PostBackTrigger ControlID="Yes" />
                     <asp:AsyncPostBackTrigger ControlID="lnksubir" EventName="Click" />
                 </Triggers>
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
                                 </div>
                             </div>
                         </div>
                         <div class="modal-footer">
                             <div class="col-lg-6 col-xs-6">
                                 <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                             </div>
                             <div class="col-lg-6 col-xs-6">
                                 <input id="No" class="btn btn-danger btn-block" type="button" onclick="ModalClose();" value="Cancelar" />
                             </div>
                         </div>
                     </div>
                 </ContentTemplate>
             </asp:UpdatePanel>
         </div>
    </div>
</asp:Content>
