<%@ Page Title="Captura Candidatos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="candidatos_preparar_captura.aspx.cs" Inherits="presentacion.candidatos_preparar_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function ProgressBar(value, valuein) {
            $('#progressincorrect').css('width', valuein);
            $('#progrescorrect').css('width', value);
            $('#pavanze').css('width', value);
            $('#lblya').text(value);
            $('#lblno').text(valuein);
        }
        var downloadURL = function downloadURL(url) {
            var hiddenIFrameID = 'hiddenDownloader',
                iframe = document.getElementById(hiddenIFrameID);
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                iframe.style.display = 'none';
                document.body.appendChild(iframe);
            }
            iframe.src = url;
        };
        function GoSection(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 500);
        }
        function AlertGO(TextMess, URL, typealert) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: typealert,
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                allowEscapeKey: false
            },
               function () {
                   location.href = URL;
               });
        }
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalPreBaja() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <br />
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Reclutar Candidatos al Puesto</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="text-align: left">
                    <div class="form-group">
                        <h4><strong>Puesto:
                            <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label></strong>
                            <asp:Label ID="lblfechasoli" runat="server" Text="" Visible="false"></asp:Label>
                        </h4>
                        <h4>
                            <asp:Label ID="lblfecha_compro" runat="server" Text="" Visible="true"></asp:Label>
                            <span>
                                <asp:LinkButton ID="lnkcambiarfecha" runat="server" CssClass="btn btn-success" OnClick="lnkcambiarfecha_Click">Cambiar Fecha</asp:LinkButton></span>
                        </h4>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                    <asp:LinkButton ID="lnknuevocandidato" runat="server" CssClass="btn btn-success btn-block" OnClick="lnknuevocandidato_Click">Agregar Candidato <i class="fa fa-plus-circle"></i></asp:LinkButton>
                </div>
            </div>
            <asp:Panel ID="PanelCatalogo" runat="server">

                <h4 id="ExisteProce" runat="server" visible="false">Ya hay un Proceso de Pre Alta o Alta activo. No podra hacer ningun movimiento hasta que este termine o se cancele. <i class="fa fa-info-circle"></i></h4>

                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 xol-sm-12">
                        <div class="panel panel-success fresh-color">
                            <div class="panel-heading" style="text-align: center;">
                                <h4 class="panel-title">Candidatos al Puesto <i class="fa fa-users"></i></h4>
                            </div>
                            <div class="panel-body">
                                <div class="table table-responsive">
                                    <h2 id="nocandidatos" runat="server" visible="false" style="text-align: center;"><strong>Aun no hay ningun candidato relacionado <i class="fa fa-exclamation-triangle"></i></strong></h2>
                                    <asp:GridView ID="gridCatalogo" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowDataBound="gridCatalogo_RowDataBound" OnRowCommand="gridCatalogo_RowCommand" DataKeyNames="idc_puesto, idc_prepara ,idc_pre_empleado">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_autorizar.png" HeaderText="Capacitar" CommandName="Capacitacion" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar" CausesValidation="false">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <%-- <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar" CausesValidation="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>--%>
                                            <asp:BoundField DataField="idc_pre_empleado" HeaderText="idc_pre_empleado" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="idc_puesto" HeaderText="idc_tipoherramienta" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="idc_prepara" HeaderText="idc_prepara" Visible="false"></asp:BoundField>
                                            <asp:ButtonField DataTextField="nombre" ControlStyle-CssClass="btn btn-default btn-block" HeaderText="Candidato" CommandName="Puesto" />
                                            <asp:BoundField DataField="fec_ingreso" HeaderText="Fecha de Alta"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="view" runat="server" visible="false">
                    <div class="panel panel-info fresh-color">
                        <div class="panel-heading" style="text-align: center;">
                            Información del Candidato
                            <span>
                                <asp:Button ID="btnocul" CssClass="btn btn-default" OnClick="btnocul_Click" runat="server" Text="Ocultar Información" /></span>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h4 style="text-align: center;"><strong>Datos Personales <i class="fa fa-user"></i></strong></h4>
                                    <asp:Repeater ID="gridDetalles" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Nombre <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("nombre") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Genero <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="TextBox1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("sexo") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Estado Civil <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="TextBox4" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("edo_civil") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Fecha de Nac <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="TextBox2" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("fec_nac") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Dirección <i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="TextBox5" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("direccion") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <h4 style="text-align: center;"><strong>Contacto <i class="fa fa-mobile"></i></strong></h4>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Correo <i class="fa fa-laptop"></i></span>
                                                            <asp:TextBox ID="TextBox3" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("correo_personal") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <asp:Repeater ID="repeat_telefonos" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;"><i class="fa fa-mobile"></i></span>
                                                            <asp:TextBox ID="TextBox3" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("telefono") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <h4 style="text-align: center;"><strong>Papeleria <i class="fa fa-archive"></i></strong></h4>

                                    <div class="row">
                                        <asp:Repeater ID="repeat_papeleria" runat="server" OnItemDataBound="repeat_papeleria_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="col-lg-6 col-md-6 col-sm-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="TextBox3" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>

                                                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">
                                                                <asp:LinkButton ID="lnkdownload" Style="color: #fff; background-color: #22A7F0;" runat="server" OnClick="lnkdownload_Click">Descargar <i class="fa fa-download"></i></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:TextBox ID="txtobservaciones2" ReadOnly="true" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelCaptura" runat="server" Visible="false">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading" style="text-align: center;">
                                <h4 class="panel-title">Datos del Candidato <i class="fa fa-user"></i></h4>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                                    <i class="fa fa-user"></i></span>
                                                <asp:TextBox ID="txtNombres" runat="server" class="form-control" placeholder="Nombre del Candidato" Style="resize: none;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-12">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                                    <i class="fa fa-user"></i></span>
                                                <asp:TextBox ID="txtPaterno" runat="server" class="form-control" placeholder="Apellido Paterno" Style="resize: none;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                                    <i class="fa fa-user"></i></span>
                                                <asp:TextBox ID="txtMaterno" runat="server" class="form-control" placeholder="Apellido Materno" Style="resize: none;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="PanelArchivos" class="panel panel-green" runat="server">
                            <div class="panel-heading" style="text-align: center;">
                                <h4 class="panel-title">Documentos del candidato <i class="fa fa-file-pdf-o"></i></h4>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <p>Puede subir varios documentos relacionados al candidato, sin embargo, el Curriculum Vitae es obligatorio.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:FileUpload ID="FileUpload" runat="server" />
                                            <strong>
                                                <asp:RequiredFieldValidator ID="RFV" runat="server" ControlToValidate="fileupload" ErrorMessage="Debe seleccionar un archivo" Style="color: red;"></asp:RequiredFieldValidator>
                                            </strong>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                    <i class="fa fa-check-square-o"></i></span>
                                                <asp:CheckBox ID="cbxCurriculum" runat="server" class="input-sm" Text="Es el Curriculum" TextAlign="Right" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-12">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;">
                                                    <i class="fa fa-comments-o"></i></span>
                                                <asp:TextBox ID="txtObservaciones" runat="server" class="form-control" Text="" placeholder="Observaciones" Style="resize: none;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkGuardarArchivo" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="gridArchivos" EventName="RowCommand" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-lg-3 col-md-6 col-sm-12">
                                                <asp:LinkButton ID="lnkGuardarArchivo" CssClass="btn btn-success btn-block" runat="server" OnClick="lnkGuardarArchivo_Click">Guardar Archivo <i class="fa fa-floppy-o"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 Col-sm-12">
                                                <div class="table table-responsive">
                                                    <asp:GridView ID="gridArchivos" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridArchivos_RowCommand" DataKeyNames="ruta">
                                                        <Columns>

                                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar" CausesValidation="false">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="ruta" HeaderText="idc_tipoherramienta" Visible="false"></asp:BoundField>
                                                            <asp:BoundField DataField="descripcion" HeaderText="Nombre"></asp:BoundField>
                                                            <asp:BoundField DataField="tipo_archi" HeaderText="Tipo_Archi" Visible="false"></asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-4">
                        <div class="form-group">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary btn-block" OnClick="btnGuardar_Click" CausesValidation="false" />
                            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" class="btn btn-primary btn-block" OnClick="btnActualizar_Click" Visible="false" CausesValidation="false" />
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="form-group">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-danger btn-block" OnClick="btnCancelar_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="confirmContenido"></label>
                                    </h4>
                                </div>
                            </div>
                            <div class="row" id="cambiar_fecha" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <asp:TextBox ID="txtnueva_fecha" CssClass="form-control" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <asp:TextBox ID="txtmotivo" CssClass="form-control" Style="text-transform: uppercase;" onblur="return imposeMaxLength(this, 250);" TextMode="MultiLine" placeholder="Ingrese un Motivo" Rows="3" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" CausesValidation="false" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalPreviewView" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                    <h4>Información del Candidato <i class="fa fa-info-circle"></i>
                    </h4>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-lg-12">
                            <input id="btnModalAcept" class="btn btn-primary btn-block" value="Cerrar" onclick="ModalClose();" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.CONFIRMA -->
        </div>
    </div>
</asp:Content>