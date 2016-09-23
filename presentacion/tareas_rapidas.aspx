<%@ Page Title="Nueva Tarea" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_rapidas.aspx.cs" Inherits="presentacion.tareas_rapidas" %>
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
                closeOnConfirm: false, allowEscapeKey: false
            },
               function () {
                   location.href = URL;
               });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <h1 class="page-header">Captura Nueva Tarea</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="Yes" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <h4><i class="fa fa-list-alt"></i>&nbsp;Descripción de la tarea</h4>
                    <asp:TextBox ID="txtdescripcion" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 1000);" placeholder="Descripcion" CssClass="form-control"
                        TextMode="MultiLine" Rows="4" runat="server" Style="resize: none; text-transform: uppercase;"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h4><i class="fa fa-calendar"></i>&nbsp;Fecha Solicitada de Compromiso <small>Es la fecha que usted solicita como compromiso</small></h4>
                    <asp:TextBox ID="txtfecha_solicompromiso" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h4><i class="fa fa-user"></i>&nbsp;Seleccione el puesto que realizara la tarea o servicio.</h4>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-8 col-sm-12 col-xs-12">
                    <label>Selecciona un Empleado </label>
                    <asp:DropDownList ID="ddlPuesto" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-4 col-md-2 col-sm-8 col-xs-8">
                    <label>Escriba un Filtro</label>
                    <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                    <label></label>
                    <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                </div>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-12">
            <h4><i class="fa fa-file-archive-o"></i>&nbsp;Agregar Imagen</h4>
            <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:LinkButton ID="lnkGuardarPape" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" runat="server">Guardar </asp:LinkButton>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
        </div>
    </div>
            <!-- Modal -->
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
                                <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
