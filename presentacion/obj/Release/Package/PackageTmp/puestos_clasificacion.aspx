<%@ Page Title="Alta" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="puestos_clasificacion.aspx.cs" Inherits="presentacion.relacion_reclu_puestos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
        function Gifted(mensaje) {
            swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '5000', showConfirmButton: false });
            return true;
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
    <h2 class=" page-header">Clasificacion de Puestos</h2>
    <div class=" row">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <Triggers>
                  <asp:PostBackTrigger ControlID="LinkButton1" />
              </Triggers>
              <ContentTemplate>
                  <div class="col-lg-12">
                      <h4><strong>Se requiere un Excel con SOLO los siguientes campos</strong></h4>                      
                          <label style="width:32%;background-color:white; border:1px solid gray; text-align:center;padding:5px;font-size:18px;">idc_puesto</label>
                          <label style="width:32%;background-color:white; border:1px solid gray;text-align:center;padding:5px;font-size:18px;">descripcion</label>
                          <label style="width:32%;background-color:white; border:1px solid gray;text-align:center;padding:5px;font-size:18px;">clasificacion</label>
                      <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success btn-block" runat="server" OnClick="LinkButton1_Click1">Descargar Ejemplo de Formato&nbsp;<i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton>
                      <br />
                      <h4><strong>Seleccione una Accion.</strong></h4>
                      <asp:DropDownList ID="ddlaccion" AutoPostBack="True" CssClass=" form-control" runat="server" OnSelectedIndexChanged="ddlaccion_SelectedIndexChanged">
                          <asp:ListItem Selected="true" Text="-- Seleccione una Opcion" Value="0"></asp:ListItem>
                          <asp:ListItem Text="Borrar Clasificaciones de la Lista" Value="B"></asp:ListItem>
                          <asp:ListItem Text="Agregar|Actualizar Clasificaciones de la Lista" Value="A"></asp:ListItem>
                      </asp:DropDownList>
                  </div>
                  <div class="col-lg-12" id="borrartodo" runat="server" visible="false">
                      <br />
                      <asp:CheckBox ID="cbxbtodo" AutoPostBack="true" CssClass="radio3 radio-check radio-info radio-inline" Text="Borrar Todo y dejar solo Excel" runat="server" OnCheckedChanged="cbxbtodo_CheckedChanged" />
                        <br />
                       <asp:CheckBox ID="cbxactuali" AutoPostBack="true" CssClass="radio3 radio-check radio-info radio-inline" Text="Conservar Todo y Agregar Excel" runat="server" OnCheckedChanged="cbxactuali_CheckedChanged" />
                  </div>
              </ContentTemplate>
            </asp:UpdatePanel>
        
        <div class="col-lg-12">
            <h4><strong>Seleccione o Arrastre un archivo de Excel con las Relaciones de Reclutadores y Puestos.</strong></h4>
            <asp:FileUpload ID="fuparchivo" CssClass=" form-control" runat="server" />
            <br />
            <asp:LinkButton OnClientClick="return Gifted('Estamos Procesando el Archivo en la Base De Datos');" CssClass="btn btn-info btn-block" ID="lnksubir"
                 runat="server" OnClick="lnksubir_Click">Subir Archivo</asp:LinkButton>
            <br />
            <h3 style="color:orangered;" id="error" runat="server" visible="false"><strong>
                <asp:Label ID="lblerror" runat="server" Text=""></asp:Label></strong></h3>
            <asp:LinkButton ID="lnkexport" Visible="false" CssClass="btn btn-success"  runat="server" OnClick="LinkButton1_Click">Exportar Tabla a Excel</asp:LinkButton>
            <div class="table table-responsive">
                <asp:GridView ID="grid" CssClass="gvv table table-responsive table-condensed table-bordered"
                     AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="idc_puesto" HeaderText="idc_puesto" HeaderStyle-Width="50px"></asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Puesto"></asp:BoundField>
                        <asp:BoundField DataField="clasificacion" HeaderText="Clasificacion"></asp:BoundField>
                        <asp:BoundField DataField="error" HeaderText="Error"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
