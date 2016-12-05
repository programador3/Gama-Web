<%@ Page Title="Tareas Captura" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_captura.aspx.cs" Inherits="presentacion.tareas_captura" %>

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
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <h1 class="page-header">Captura Nueva Tarea</h1>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtfecha_solicompromiso" EventName="TextChanged" />
                </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12">
                            <h3 id="titleserv" runat="server" visible="false" style="color: orangered;"><strong>La Tarea fue solicitada a partir de un servicio con un tiempo de respuesta predefinido de 
                                <asp:Label Style="color: navy;" ID="lblhoras_tarea_serv" runat="server" Text="0"></asp:Label>
                                hora(s).
                            </strong></h3>
                            <h4><i class="fa fa-list-alt"></i>&nbsp;Descripción de la tarea
                            </h4>
                            <asp:TextBox ID="txtdescripcion" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 1000);" placeholder="Descripcion" CssClass="form-control"
                                TextMode="MultiLine" Rows="5" runat="server" Style="resize: none; text-transform: uppercase; font-size: 12px;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server" id="div_puestoasigna" visible="false" >
                         <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <label style="color: orangered; ">Puede Seleccionar un Empleado para dar Visto Bueno a la Tarea</label>
                            <asp:DropDownList ID="ddlpuestoasigna"  runat="server" CssClass="form-control" >
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-4 col-md-2 col-sm-12 col-xs-12">
                            <label>Escriba un Filtro</label>
                            <asp:TextBox  style="font-size:12px;" ID="txtpuesto_asigna" runat="server" TextMode="SingleLine" CssClass="form-control" 
                                AutoPostBack="true" OnTextChanged="LinkButton2_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                        </div>
                        <div class="col-lg-2 col-md-4 col-sm-12 col-xs-12">
                            <label style="width:100%"></label>
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-success" Width="100%" OnClick="LinkButton2_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                    
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <h4><i class="fa fa-calendar"></i>&nbsp;Fecha Solicitada de Compromiso <small>Es la fecha que usted solicita como compromiso</small></h4>
                            <asp:TextBox ID="txtfecha_solicompromiso" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-lg-12">
                            <h4><i class="fa fa-user"></i>&nbsp;Seleccione el puesto que realizara la tarea o servicio. <small>Tambien puede elegir varios empleados si quiere crear varias tareas iguales para diferentes puestos.</small></h4>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <label>Selecciona un Empleado <small>Si solo es un Empleado, NO ES NECESARIO que se agrege a la tabla.</small></label>
                            <asp:DropDownList ID="ddlPuesto" OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <label>Escriba un Filtro</label>
                            <asp:TextBox  style="font-size:12px;" ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" 
                                AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <label style="width:100%"></label>
                            <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success" Width="49%" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkadd" runat="server" CssClass="btn btn-info" OnClick="lnkadd_Click" Width="49%">Agregar <i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
                    
                        </div>
                    </div>
                    <div class="row" id="tareaservicios" runat="server" visible="false">
                        <div class=" col-lg-12">
                            <h4 style="color:orangered;text-align:center;"><strong>El Empleado Cuenta con la siguiente lista de Servicios Disponibles</strong></h4>
                            <asp:DropDownList style="font-size:12px;" Width="86%" ID="ddlservicios" runat="server" CssClass="form-control2" AutoPostBack="true" OnSelectedIndexChanged="ddlservicios_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:LinkButton ID="LinkButton1"  runat="server" CssClass="btn btn-success" Width="38px" OnClick="LinkButton1_Click" >
                                <i class="fa fa-info-circle" aria-hidden="true"></i>

                            </asp:LinkButton>
                       
                                <br />
                                <asp:Label ID="lblobservacionesser" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-xs-12">
                            <div class="table table-responsive"  style="text-align:center;">
                                <asp:GridView style="text-align:center;" ID="grid_puestpos" DataKeyNames="idc_puesto" CssClass="table table-responsive table-bordered table-hover" OnRowCommand="grid_puestpos_RowCommand" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" EditText="" HeaderText="Eliminar" SelectImageUrl="~/imagenes/btn/icon_delete.png" SelectText="" ShowSelectButton="True">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="idc_puesto" HeaderText="idc_puesto" Visible="false"></asp:BoundField>
                                        <asp:BoundField HeaderText="Empleado" DataField="descripcion_puesto_completa"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <h4><i class="fa fa-wrench"></i>&nbsp;Tareas Pendientes  <small>Este Puesto Tiene las Siguiente Tareas Pendientes</small></h4>
                            <h4 id="no_mias" runat="server" style="text-align: center;">No tiene Tareas Pendientes <i class="fa fa-thumbs-o-up"></i></h4>
                            <asp:Repeater ID="repeat_mis_tareas" runat="server">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnkmistarea" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                <asp:LinkButton Style="font-size: 11px;" ID="lnkmistarea" CssClass='<%#Eval("cssclass") %>' runat="server" CommandName='<%#Eval("idc_tarea") %>' OnClick="lnkmistarea_Click1" ToolTip='<%#Eval("desc_completa") %>'>
                                                     <h5 style="font-size:11px;"> <%#Eval("descripcion") %> </h5>
                                                     <h5 style="font-size:11px;">Fecha Compromiso: <%#Eval("fecha_compromiso") %> </h5>
                                                     <h5 style="font-size:11px;">Avance: <%#Eval("avance") %> %</h5>
                                                </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <asp:TextBox ID="txtidc_tareaser" Visible="false" runat="server"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <div class="row" style="padding: 5px; border-radius: 0px 0px 0px 0px; -moz-border-radius: 0px 0px 0px 0px; -webkit-border-radius: 0px 0px 0px 0px; border: 1px solid #000000;">
                <div class="col-lg-12">
                    <h4><i class="fa fa-file-archive-o"></i>&nbsp;Agregar Comentarios <small>Puede anexar la cantidad de archivos que usted requiera</small></h4>
                    <asp:TextBox ID="txtNombreArchivo" onfocus="$(this).select();" runat="server" CssClass="form-control" placeholder="Descripcion del documento" 
                        onkeypress="return isNumber(event);"></asp:TextBox>
                </div>
                <div class="col-lg-12">
                    <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                </div>
                <div class="col-lg-12 col-xs-12">
                    <asp:LinkButton ID="lnkGuardarPape" CssClass="btn btn-primary btn-block" OnClick="lnkGuardarPape_Click" runat="server">Agregar Comentario <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                </div>
                <div class="col-lg-12">
                    <div class="table table-responsive">
                        <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="id_archi,descripcion, ruta, extension">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" Visible="false" CommandName="Editar" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:ButtonField>
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
            <br />
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
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
        </div>
    </div>
</asp:Content>