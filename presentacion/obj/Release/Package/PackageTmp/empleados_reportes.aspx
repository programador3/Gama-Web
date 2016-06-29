<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="empleados_reportes.aspx.cs" Inherits="presentacion.empleados_reportes" %>

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
    <h1 class="page-header">Reporte a Empleados</h1>
    <div class="row">
        <div class="col-lg-2" style="align-content: center;">
            <a>
                <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Seleccione un Empleado" Style="width: 160px; margin: 0 auto;" />
            </a>
        </div>
        <div class="col-lg-10" style="text-align: left">
            <div class="form-group">
                <h4>
                    <strong>Nombre Empleado: </strong>
                    <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                </h4>
                <h4><strong>Puesto: </strong>
                    <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                </h4>
                <h4><strong>Departamento: </strong>
                    <asp:Label ID="lbldepto" runat="server" Text=""></asp:Label>
                </h4>
                <h4><strong>
                    <asp:Label ID="lblfecha" Visible="false" runat="server" Text=""></asp:Label>
                </strong>
                </h4>
            </div>
        </div>
    </div>

    <div class="row" runat="server" id="FILTRO">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h4><strong>Selecciona el Empleado</strong></h4>
            <asp:DropDownList ID="ddlPuestoAsigna" OnSelectedIndexChanged="ddlPuestoAsigna_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-8 col-xs-8">
            <h4>Escriba un Filtro</h4>
            <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Empleado"></asp:TextBox>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
            <h4>.</h4>
            <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12">
            <h4><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;<strong>Seleccione un Tipo de Reporte</strong></h4>
            <asp:DropDownList ID="ddltiporeporte" CssClass="form-control" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="row" id="obsrva" runat="server" visible="false">
        <div class="col-lg-12 col-sm-12">
            <h4><i class="fa fa-comments-o" aria-hidden="true"></i>&nbsp;<strong>Observaciones de quien Realizo el Reporte</strong></h4>
            <asp:TextBox ID="txtobservaciones_auto" CssClass="form-control" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 8000);" TextMode="MultiLine" placeholder="Ingrese Observaciones" Rows="4" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 col-sm-12">
            <h4><i class="fa fa-comments-o" aria-hidden="true"></i>&nbsp;<strong>Sus Observaciones</strong></h4>
            <asp:TextBox ID="txtcomentarios" CssClass="form-control" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 8000);" TextMode="MultiLine" placeholder="Ingrese Observaciones" Rows="4" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row" runat="server" id="archi" visible="false">
        <div class="col-lg-12">
            <h4><i class="fa fa-file-archive-o"></i>&nbsp;Agregar Archivos <small>Puede anexar la cantidad de archivos que usted requiera</small></h4>
            <asp:TextBox ID="txtNombreArchivo" onfocus="$(this).select();" runat="server" CssClass="form-control" placeholder="Descripcion del documento" onkeypress="return isNumber(event);"></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
        </div>
        <div class="col-lg-12 col-xs-12">
            <asp:LinkButton ID="lnkGuardarPape" CssClass="btn btn-info btn-block" OnClick="lnkGuardarPape_Click" runat="server">Agregar Archivo <i class="fa fa-plus-circle"></i> </asp:LinkButton>
        </div>
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="id_archi,descripcion, ruta, extension">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="extension" HeaderText="extension" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion"></asp:BoundField>
                        <asp:BoundField DataField="id_archi" HeaderText="id_archi" Visible="false"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="btnGuardar" Visible="false" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnvistobueno" Visible="false" runat="server" Text="Revisar" CssClass="btn btn-primary btn-block" OnClick="btnvistobueno_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>