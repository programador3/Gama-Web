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
        function getImage2(path) {
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
    <asp:TextBox ID="txtidoriginal" Visible="false" runat="server"></asp:TextBox>
    <h1 class="page-header">Reporte a Empleados
        <small>
            <asp:Label style="color: orangered;" ID="lblreportereasignado" Visible="false" runat="server" Text="Este Reporte fue Reasignado de Otro Reporte"></asp:Label>
            <span>
                <asp:LinkButton ID="lnkoriginal" Visible="false" CssClass="btn btn-info" runat="server" OnClick="lnkoriginal_Click">Ver Reporte Original</asp:LinkButton></span>
        </small>
    </h1>
    <asp:UpdatePanel ID="ddedd" runat="server" UpdateMode="always">
        <ContentTemplate>
            <div style="padding: 20px">
                <div class="row" id="div_empleadoalta" runat="server" visible="false" style="background-color: white; border: 1px solid gray; padding: 10px;">
                    <asp:TextBox ID="txtidc_empleado" Visible="false" runat="server"></asp:TextBox>
                    <div class="col-lg-12" id="div_busqueda" runat="server">
                        <h3 style="color: orangered; text-align: center;"><strong>Para realizar esta solicitud, indique quien es usted.</strong></h3>
                        <h4><strong>Puede Buscar el Empleado por Nombre o Numero de Nomina.</strong></h4>
                        <asp:TextBox onfocus="$(this).select();" ID="txtbuscar" CssClass="form-control2" placegolder="Puede Buscar por Numero de Nomina o Nombres y Apellidos" AutoPostBack="true" runat="server" OnTextChanged="txtbuscar_TextChanged" Width="73%"></asp:TextBox>
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info" Width="25%" OnClick="LinkButton1_Click">Buscar</asp:LinkButton>
                        <h5><strong>Seleccione un Empleado.</strong></h5>
                        <asp:DropDownList ID="ddlseleccionar" CssClass=" form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlseleccionar_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class=" col-lg-12">
                        <h4 style="text-align: center;"><strong><i class="fa fa-info-circle" aria-hidden="true"></i>&nbsp;Datos del Empleado que Reporta</strong><span>
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-success" OnClick="LinkButton2_Click">Cambiar de Emplado&nbsp;<i class="fa fa-exchange" aria-hidden="true"></i></asp:LinkButton>
                        </span></h4>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-4" style="align-content: center;">
                        <a>
                            <asp:Image ID="imgempleadoalta" runat="server" class="img-responsive" alt="Seleccione un Empleado" Style="width: 160px; margin: 0 auto;" />
                        </a>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-8" style="text-align: left">
                        <h4>
                            <strong>Nombre Empleado: </strong>
                            <asp:Label ID="lblnombremepleadoalta" runat="server" Text=""></asp:Label>
                        </h4>
                        <h4>
                            <strong>Numero de Nomina: </strong>
                            <asp:Label ID="lblnomina" runat="server" Text=""></asp:Label>
                        </h4>
                    </div>
                </div>
            </div>

            <br />
            <div style="padding: 20px;">

                <div class="row" style="background-color: white; border: 1px solid gray; padding: 10px;">
                    <div class=" col-lg-12">
                        <h4 style="text-align: center;"><strong><i class="fa fa-info-circle" aria-hidden="true"></i>&nbsp;Datos del Empleado a Reportar</strong></h4>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-4" style="align-content: center;">
                        <a>
                            <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Seleccione un Empleado" Style="width: 160px; margin: 0 auto;" />
                        </a>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-8" style="text-align: left">
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
            <div class=" row">
                <div class="col-lg-12">
                    <asp:LinkButton ID="lnlvalido" Visible="False" CssClass="btn btn-success btn-block" runat="server" OnClick="lnlvalido_Click">Seleccionar si el Reporte contara como valido</asp:LinkButton>

                </div>
            </div>
            <div class="row" runat="server" id="FILTRO">
               
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">                     
                    <h4><strong>
                        <asp:Label ID="lbltitle" runat="server" Text="Selecciona el Empleado"></asp:Label></strong></h4>
                    <asp:DropDownList ID="ddlPuestoAsigna" OnSelectedIndexChanged="ddlPuestoAsigna_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-8">
                    <h4>Escriba un Filtro</h4>
                    <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Empleado"></asp:TextBox>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                    <h4>.</h4>
                    <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <h4><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;<strong>Seleccione un Tipo de Reporte</strong>&nbsp;<small style="color: orangered;">Para seleccionar un reporte, seleccione un empleado</small></h4>
                    <asp:DropDownList ID="ddltiporeporte" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="row" id="obsrva" runat="server" visible="false">
                <div class="col-lg-12 col-sm-12">
                    <h4><i class="fa fa-comments-o" aria-hidden="true"></i>&nbsp;<strong>Observaciones de quien Realizo el Reporte</strong></h4>
                    <asp:TextBox ID="txtobservaciones_auto" CssClass="form-control" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 8000);" TextMode="MultiLine" placeholder="Ingrese Observaciones" Rows="4" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row" runat="server" id="obsrvobo" visible="false">
                <div class="col-lg-12 col-sm-12">
                    <h4><i class="fa fa-comments-o" aria-hidden="true"></i>&nbsp;<strong>Observaciones Visto Bueno</strong></h4>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="btnGuardar" Visible="false" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnvistobueno" Visible="false" runat="server" Text="Revisar" CssClass="btn btn-primary btn-block" OnClick="btnvistobueno_Click" />
            <asp:Button ID="btnterminar" Visible="false" runat="server" Text="Terminar" CssClass="btn btn-primary btn-block" OnClick="btnterminar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
        </div>
        <div class="col-lg-12">
            <asp:Button ID="BTNCERRAR" Visible="false" runat="server" Text="Cerrar Esta Ventana" CssClass="btn btn-danger btn-block" OnClientClick="window.close();" />
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