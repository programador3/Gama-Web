<%@ Page Title="Captura PreEmpleados" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="pre_empleados_captura.aspx.cs" Inherits="presentacion.pre_empleados_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/PanelsLTE.css" rel="stylesheet" />
    <link href="css/ionicons.css" rel="stylesheet" />
    <link href="css/ionicons.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function GoSection(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 500);
        }
        function validarNum(e) {//valida solo numero
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /\d/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function isNumberKey(evt) {//valida solo caracteres
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return true;
            return false;
        }
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
        function ModalPreview() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalPreview').modal('show');
        }
        function ModalClose() {
            $('#myModalPreview').modal('hide');
            $('#myModal').modal('hide');
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Captura de Pre Empleados</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <asp:Panel ID="Panel_Personal" CssClass="panel panel-primary" runat="server">
                        <div class="panel-heading" style="text-align: center;">
                            <h4 class="panel-title">Datos Personales <i class="fa fa-users"></i></h4>
                        </div>
                        <div class="panel-body">
                            <h4><strong><i class="fa fa-chevron-right"></i>Datos Personales</strong></h4>

                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlTitulo" runat="server" CssClass="form-control">
                                            <asp:ListItem Selected="True" Text="Titulo" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Sin Titulo" Value=""></asp:ListItem>
                                            <asp:ListItem Text="LIC" Value="LIC"></asp:ListItem>
                                            <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                            <asp:ListItem Text="ING" Value="ING"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lnkAgregarFotoPerfil" />
                                        <asp:AsyncPostBackTrigger ControlID="imgdeletefoto" EventName="Click" />
                                        <asp:PostBackTrigger ControlID="btnVer" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-8">
                                            <div class="form-group" id="partupload" runat="server" visible="true">
                                                <div class="input-group">
                                                    <asp:FileUpload CssClass="form-control" ID="fupFotoPerfil" runat="server" />

                                                    <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                                        <asp:LinkButton ID="lnkAgregarFotoPerfil" Style="color: #fff; background-color: #3c8dbc;" runat="server" OnClick="lnkAgregarFotoPerfil_Click">Agregar Foto <i class="fa fa-plus-circle"></i></asp:LinkButton>
                                                    </span>
                                                </div>

                                                <asp:RegularExpressionValidator ID="REV" runat="server" CssClass="label label-danger"
                                                    ErrorMessage="Tipo de archivo no permitido. Debe ser JPG, JPEG" ControlToValidate="fupFotoPerfil"
                                                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.jpeg|.JPEG)$">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-1">
                                            <asp:Button ID="btnVer" runat="server" Text="Ver Imagen" CssClass="btn btn-info btn-block" Visible="false" OnClick="btnVer_Click" />
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <asp:ImageButton ID="imgdeletefoto" ImageUrl="~/imagenes/btn/icon_delete.png" OnClick="imgdeletefoto_Click" Visible="false" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
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

                                <%--           </ContentTemplate>
                               </asp:UpdatePanel>--%>
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
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                                <i class="fa fa-user"></i></span>
                                            <asp:Label ID="lblidc_puesto" runat="server" Text="Label" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPuesto" ReadOnly="true" runat="server" class="form-control" placeholder="Nombre del Candidato" Style="resize: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-12">
                                    <div class="form-group">
                                        <h5><strong>Fecha de Nacimiento</strong></h5>
                                        <div class="input-group">
                                            <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                                <i class="fa fa-user"></i></span>
                                            <asp:TextBox ID="txtFecNac" runat="server" class="form-control input-sm" TextMode="Date"></asp:TextBox>
                                        </div>

                                        <asp:Label ID="lblFecNac" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-12">
                                    <div class="form-group">
                                        <h5><strong>Sexo</strong></h5>
                                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                                            <asp:ListItem Selected="True" Text="Seleccione uno" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Hombre" Value="H"></asp:ListItem>
                                            <asp:ListItem Text="Mujer" Value="M"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblSexo" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-12">
                                    <div class="form-group">
                                        <h5><strong>Estado Civil</strong></h5>
                                        <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlpais" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlestado" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txtCorreo" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txtFiltroColonias" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txtCalle" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlColonia" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-12">
                                            <div class="form-group">
                                                <h4><strong><i class="fa fa-chevron-right"></i>Originario de: Pais</strong></h4>
                                                <asp:DropDownList ID="ddlpais" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlpais_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-12">
                                            <div class="form-group">
                                                <h5><strong>Estado </strong></h5>
                                                <asp:DropDownList ID="ddlestado" AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <h4><strong><i class="fa fa-chevron-right"></i>Domicilio Actual </strong></h4>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                                        <i class="fa fa-home"></i></span>
                                                    <asp:TextBox ID="txtCalle" AutoPostBack="true" OnTextChanged="txtCalle_TextChanged" runat="server" class="form-control" placeholder="Calle, # Interior o Exterior" Style="resize: none;"></asp:TextBox>
                                                </div>

                                                <asp:Label ID="lblCalle" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                                        <i class="fa fa-home"></i></span>
                                                    <asp:TextBox ID="txtMunicipio" ReadOnly="true" runat="server" class="form-control" placeholder="Colonia, Municipio, Estado, Pais, #Postal(Seleccione una Colonia)" Style="resize: none;"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="lblColonia" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;"><i class="fa fa-search"></i></span>
                                                    <asp:TextBox ID="txtFiltroColonias" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" runat="server" class="form-control" placeholder="Colonia (Escriba una palabra clave y presione Enter)" Style="resize: none;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <h4 style="text-align: center;" id="resultados" runat="server" visible="false">No hay resultados. Puede intentarlo nuevamente.</h4>
                                                <asp:DropDownList ID="ddlColonia" Visible="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlColonia_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <h4><strong><i class="fa fa-chevron-right"></i>Contacto </strong></h4>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;"><i class="fa fa-mobile"></i></span>
                                                    <asp:TextBox ID="txtCorreo" TextMode="Email" OnTextChanged="txtCorreo_TextChanged" AutoPostBack="true" runat="server" class="form-control" placeholder="Correo Electronico Personal" Style="resize: none;"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="lblCorreo" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;"><i class="fa fa-mobile"></i></span>
                                                    <asp:TextBox ID="txtTelefono" onkeypress="return validarNum(event)" MaxLength="10" AutoPostBack="true" runat="server" class="form-control" placeholder="Telefonos" Style="resize: none;"></asp:TextBox>
                                                </div>

                                                <asp:Label ID="lblTelefono" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-8 col-xs-8">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlTipoTelefono" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoTelefono_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="Celular" Value="celular"></asp:ListItem>
                                                    <asp:ListItem Text="Casa" Value="casa"></asp:ListItem>
                                                    <asp:ListItem Text="Oficina" Value="oficina"></asp:ListItem>
                                                    <asp:ListItem Text="Otro" Value="otro"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:Label ID="lblTipoCel" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:ImageButton ID="imgAddTelefono" ImageUrl="~/imagenes/btn/icon_agregar.png" OnClick="imgAddTelefono_Click" runat="server" />
                                            Click para Agregar
                                        </div>
                                        <div class="col-lg-3">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-8 col-sm-12">
                                            <div class="table table-responsive">
                                                <asp:GridView ID="gridTelefonos" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridTelefonos_RowCommand" DataKeyNames="telefono">
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar" CausesValidation="false">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="telefono" HeaderText="Numeros de Telefono"></asp:BoundField>
                                                        <asp:BoundField DataField="tipo" HeaderText="Descripcion"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <ul class="pager">
                                <li class="next">
                                    <asp:LinkButton ID="lnkSiguientePDatosP" Style="color: #fff; background-color: #3c8dbc;" CssClass="next" OnClick="lnkSiguientePDatosP_Click" runat="server">Continuar <i class="fa fa-arrow-right"></i></asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="PanelDatosLaborales" Visible="false" CssClass="panel panel-green" runat="server">
                        <div class="panel-heading" style="text-align: center;">
                            <h4 class="panel-title">Datos Laborales <i class="fa fa-cogs"></i></h4>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtSueldo" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txtComplementos" EventName="TextChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <h4><strong><i class="fa fa-chevron-right"></i>Afiliaciones </strong></h4>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #00a65a;"><i class="fa fa-cogs"></i></span>
                                                    <asp:TextBox ID="txtIMSS" MaxLength="11" AutoPostBack="true" OnTextChanged="txtIMSS_TextChanged" runat="server" class="form-control" placeholder="Numero de Seguro Social" Style="resize: none;"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="LBLIMSS" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #00a65a;"><i class="fa fa-cogs"></i></span>
                                                    <asp:TextBox ID="TXTCURP" runat="server" AutoPostBack="true" class="form-control" placeholder="CURP" Style="resize: none;" MaxLength="18" OnTextChanged="TXTCURP_TextChanged"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="lblcurp" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #00a65a;"><i class="fa fa-cogs"></i></span>
                                                    <asp:TextBox ID="txtRFC" MaxLength="13" AutoPostBack="true" OnTextChanged="txtRFC_TextChanged" runat="server" class="form-control" placeholder="RFC" Style="resize: none;"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="lblRFC" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <h4><strong><i class="fa fa-chevron-right"></i>Sueldos y Prestaciones </strong></h4>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #00a65a;">Sueldo <i class="fa fa-usd"></i></span>
                                                    <asp:TextBox ID="txtSueldo" TextMode="Number" OnTextChanged="txtSueldo_TextChanged" AutoPostBack="true" runat="server" class="form-control" placeholder="Sueldo" Style="resize: none;"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="lblSueldo" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #00a65a;">Complementos <i class="fa fa-usd"></i></span>
                                                    <asp:TextBox ID="txtComplementos" TextMode="Number" OnTextChanged="txtComplementos_TextChanged" runat="server" AutoPostBack="true" class="form-control" placeholder="Complementos" Style="resize: none;" MaxLength="18"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="lblComplemento" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #00a65a;"><i class="fa fa-cogs"></i></span>
                                                    <asp:CheckBox ID="cbxPremioTransporte" Text="Premio Transporte" Checked="true" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <h4><strong><i class="fa fa-chevron-right"></i>Horarios de Trabajo </strong></h4>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:Repeater ID="repeatdias" runat="server" OnItemDataBound="repeatdias_ItemDataBound">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlhorariodia" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cbxLaborables" EventName="CheckedChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-4 col-sm-3">
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-5">
                                                    <h4>Dia Laborable</h4>
                                                </div>
                                                <div class="col-lg-2 col-md-4 col-sm-2 col-xs-12">
                                                    <h4>Horario</h4>
                                                </div>
                                                <div class="col-lg-2 col-md-4 col-sm-2 col-xs-12">
                                                    <h4>Comida</h4>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-4 col-sm-12">
                                                    <strong><i class="fa fa-chevron-right"></i><%#Eval("dia") %></strong>
                                                </div>
                                                <div class="col-lg-2 col-md-4 col-sm-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #00a65a;"><i class="fa fa-cogs"></i></span>
                                                            <asp:CheckBox ID="cbxLaborables" AutoPostBack="true" Text="Si" OnCheckedChanged="cbxLaborables_CheckedChanged" Checked="true" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2 col-md-4 col-sm-12">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #00a65a;"><i class="fa fa-cogs"></i></span>
                                                            <asp:CheckBox ID="cbxDescanso" AutoPostBack="true" Text="Descanso" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2 col-md-4 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlhorariodia" OnSelectedIndexChanged="ddlLunes_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2 col-md-4 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlhorariocomida" Visible="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:Label ID="lblNoComida" runat="server" Text="No aplica comida"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblErrorHorario" runat="server" CssClass="label label-danger"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:Repeater>
                            <ul class="pager">
                                <li class="previous">
                                    <asp:LinkButton ID="lnkAnteriorPDatosL" Style="color: #fff; background-color: #00a65a;" CssClass="previous" OnClick="lnkAnteriorPDatosL_Click" runat="server"> <i class="fa fa-arrow-left"></i> Anterior</asp:LinkButton>
                                </li>
                                <li class="next">
                                    <asp:LinkButton ID="lnkSiguientePDatosL" Style="color: #fff; background-color: #00a65a;" CssClass="next" runat="server" OnClick="lnkSiguientePDatosL_Click">Continuar <i class="fa fa-arrow-right"></i></asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelDFamiliares" CssClass="panel panel-red" Visible="false" runat="server">
                        <div class="panel-heading" style="text-align: center;">
                            <h4 class="panel-title">Datos Familiares <i class="fa fa-users"></i></h4>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtPadre" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txtMadre" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txtEsposo" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="imgAddHijos" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="gridHijos" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlSexoHijos" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txtFechaNacHijos" EventName="TextChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #d9534f;"><i class="fa fa-users"></i></span>
                                                    <asp:TextBox ID="txtPadre" onkeypress="return isNumberKey(event)" OnTextChanged="txtPadre_TextChanged" AutoPostBack="true" runat="server" class="form-control" placeholder="Nombre Completo del Padre" Style="resize: none;"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="lblPadre" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #d9534f;"><i class="fa fa-users"></i></span>
                                                    <asp:TextBox ID="txtMadre" onkeypress="return isNumberKey(event)" OnTextChanged="txtMadre_TextChanged" AutoPostBack="true" runat="server" class="form-control" placeholder="Nombre Completo de la Madre" Style="resize: none;"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="lblMadre" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #d9534f;"><i class="fa fa-users"></i></span>
                                                    <asp:TextBox ID="txtEsposo" onkeypress="return isNumberKey(event)" OnTextChanged="txtEsposo_TextChanged" AutoPostBack="true" runat="server" class="form-control" placeholder="Nombre Completo del(a) Esposo(a)" Style="resize: none;"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="lblEsposo" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-10 col-xs-10">
                                            <h4><strong><i class="fa fa-chevron-right"></i>Hijos </strong></h4>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #d9534f;"><i class="fa fa-users"></i></span>
                                                    <asp:TextBox ID="txtHijos" onkeypress="return isNumberKey(event)" OnTextChanged="txtEsposo_TextChanged" AutoPostBack="true" runat="server" class="form-control" placeholder="Nombre Completo del(os) Hijo(s)" Style="resize: none;"></asp:TextBox>
                                                </div>

                                                <asp:Label ID="lblHijos" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-10 col-xs-10">
                                            <h4>Fecha de Nacimiento</h4>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #d9534f;"><i class="fa fa-users"></i></span>
                                                    <asp:TextBox ID="txtFechaNacHijos" OnTextChanged="txtFechaNacHijos_TextChanged" TextMode="Date" AutoPostBack="true" runat="server" class="form-control input-sm" Style="resize: none;"></asp:TextBox>
                                                </div>

                                                <asp:Label ID="lblFcehaNacHijos" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-sm-10 col-xs-10">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlSexoHijos" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSexoHijos_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="Seleccione un Genero" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Hombre" Value="H"></asp:ListItem>
                                                    <asp:ListItem Text="Mujer" Value="M"></asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:Label ID="lblSexoHijos" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:ImageButton ID="imgAddHijos" ImageUrl="~/imagenes/btn/icon_agregar.png" OnClick="imgAddHijos_Click" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                            <div class="table table-responsive">
                                                <asp:GridView ID="gridHijos" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridHijos_RowCommand" DataKeyNames="nombre">
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar" CausesValidation="false">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="nombre" HeaderText="Nombre Hijo(a)"></asp:BoundField>
                                                        <asp:BoundField DataField="sexo" HeaderText="Genero"></asp:BoundField>
                                                        <asp:BoundField DataField="fec_nac" HeaderText="Fecha de Nacimiento"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <ul class="pager">
                                <li class="previous">
                                    <asp:LinkButton ID="lnkReturnDFam" Style="color: #fff; background-color: #d9534f;" CssClass="previous" OnClick="lnkReturnDFam_Click" runat="server"> <i class="fa fa-arrow-left"></i> Anterior</asp:LinkButton>
                                </li>
                                <li class="next">
                                    <asp:LinkButton ID="lnkAdlenateDFAM" Style="color: #fff; background-color: #d9534f;" CssClass="next" runat="server" OnClick="lnkAdlenateDFAM_Click">Continuar <i class="fa fa-arrow-right"></i></asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelDConact" CssClass="panel panel-yellow" runat="server" Visible="false">
                        <div class="panel-heading" style="text-align: center;">
                            <h4 class="panel-title">Documentos y Papeleria <i class="fa fa-file-archive-o"></i></h4>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="PanelElector" runat="server">
                                <h4><strong><i class="fa fa-chevron-right"></i>Credencial de Elector </strong>
                                    <asp:Image ID="imgElector" ImageUrl="~/imagenes/btn/checked.png" runat="server" Visible="false" /></h4>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-12">
                                        <div class="form-group">
                                            Parte de Adelante
                                           <asp:FileUpload ID="fupFrenteElector" CssClass="form-control" runat="server" />
                                            <asp:Label ID="lblfrenteelec" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>

                                            <asp:RegularExpressionValidator ID="revFrenteElect" runat="server" CssClass="label label-danger"
                                                ErrorMessage="Tipo de archivo no permitido. Debe ser JPG" ControlToValidate="fupFrenteElector"
                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG)$">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12">
                                        <div class="form-group">
                                            Parte de Atras
                                           <asp:FileUpload ID="fupAtrasElector" CssClass="form-control" runat="server" />
                                            <asp:Label ID="lblatraselect" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>

                                            <asp:RegularExpressionValidator ID="revAtrasElect" runat="server" CssClass="label label-danger"
                                                ErrorMessage="Tipo de archivo no permitido. Debe ser JPG" ControlToValidate="fupAtrasElector"
                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG)$">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <%--<asp:AsyncPostBackTrigger ControlID="lnkElector" EventName="Click" />--%>
                                            <asp:AsyncPostBackTrigger ControlID="txtFechaVencElect" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtFolioElector" EventName="TextChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="col-lg-4 col-md-4 col-sm-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #f39c12;"><i class="fa fa-file-archive-o"></i># Elector</span>
                                                        <asp:TextBox ID="txtFolioElector" onkeypress="return validarNum(event)" MaxLength="15" placeholder="Folio Elector" OnTextChanged="txtFolioElector_TextChanged" AutoPostBack="true" runat="server" class="form-control input-sm" Style="resize: none;"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblFolioEelec" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #f39c12;"><i class="fa fa-file-archive-o"></i>Vencimiento</span>
                                                        <asp:TextBox ID="txtFechaVencElect" TextMode="Date" placeholder="Año Vencimiento" MaxLength="4" OnTextChanged="txtFechaVencElect_TextChanged" AutoPostBack="true" runat="server" class="form-control input-sm" Style="resize: none;"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblFecVenElect" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <div class="col-lg-2 col-md-2 col-sm-12">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkElector" OnClick="lnkElector_Click" CssClass="btn btn-warning btn-block" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panelLicencia" runat="server">
                                <h4><strong><i class="fa fa-chevron-right"></i>Licencia de Manejo
                                    <asp:Image ID="imgLicencia" ImageUrl="~/imagenes/btn/checked.png" runat="server" Visible="false" />
                                </strong></h4>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-12">
                                        <div class="form-group">
                                            Parte de Adelante
                                           <asp:FileUpload ID="fupFrenteLic" CssClass="form-control" runat="server" />
                                            <asp:RegularExpressionValidator ID="revFrenteLic" runat="server" CssClass="label label-danger"
                                                ErrorMessage="Tipo de archivo no permitido. Debe ser JPG" ControlToValidate="fupFrenteLic"
                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG)$">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12">
                                        <div class="form-group">
                                            Parte de Atras
                                           <asp:FileUpload ID="fupAtrasLic" CssClass="form-control" runat="server" />
                                            <asp:RegularExpressionValidator ID="revAtrasLic" runat="server" CssClass="label label-danger"
                                                ErrorMessage="Tipo de archivo no permitido. Debe ser JPG" ControlToValidate="fupAtrasLic"
                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG)$">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <%--         <asp:AsyncPostBackTrigger ControlID="lnkElector" EventName="Click" />--%>
                                            <asp:AsyncPostBackTrigger ControlID="txtNumLicencia" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtFecVenLic" EventName="TextChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="col-lg-4 col-md-6 col-sm-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #f39c12;"><i class="fa fa-file-archive-o"></i># Licencia</span>
                                                        <asp:TextBox ID="txtNumLicencia" onkeypress="return validarNum(event)" MaxLength="10" placeholder="Numero Licencia" OnTextChanged="txtNumLicencia_TextChanged" AutoPostBack="true" runat="server" class="form-control input-sm" Style="resize: none;"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblNumLic" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-sm-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #f39c12;"><i class="fa fa-file-archive-o"></i>Vencimiento</span>
                                                        <asp:TextBox ID="txtFecVenLic" TextMode="Date" placeholder="Año Vencimiento" MaxLength="4" OnTextChanged="txtFecVenLic_TextChanged" AutoPostBack="true" runat="server" class="form-control input-sm" Style="resize: none;"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblfecvenlic" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-6 col-sm-12">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlTipoLic" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="col-lg-2 col-md-6 col-sm-12">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkGuardarLic" OnClick="lnkGuardarLic_Click" CssClass="btn btn-warning btn-block" runat="server" Visible="true">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <h4><strong><i class="fa fa-chevron-right"></i>Papeleria </strong>
                            </h4>
                            <div class="row">
                                <asp:Repeater ID="repeatPapeleria" runat="server" OnItemDataBound="repeatPapeleria_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkGuardarPape" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div class="col-lg-6 col-md-6 col-sm-12">
                                                    <asp:Panel ID="PanelPApeleriaIndv" runat="server">
                                                        <h6>
                                                            <asp:Label ID="lblDescr" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                            (Extension <%#Eval("tipo_archivo") %>)
                                                           <asp:Image ID="imgPape" ImageUrl="~/imagenes/btn/checked.png" runat="server" Visible="false" />
                                                        </h6>
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />

                                                                <span class="input-group-addon" style="color: #fff; background-color: #f39c12;">
                                                                    <asp:LinkButton ID="lnkGuardarPape" Style="color: #fff; background-color: #f39c12;" OnClick="lnkGuardarPape_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                                                </span>
                                                            </div>
                                                            <asp:Label ID="lblerrorpapedinamico" CssClass="label label-danger" runat="server" Text="" Visible="false"></asp:Label>

                                                            <asp:RegularExpressionValidator ID="revpapeleria" runat="server" CssClass="label label-danger"
                                                                ErrorMessage="Tipo de archivo no permitido." ControlToValidate="fupPapeleria"
                                                                ValidationExpression="">
                                                            </asp:RegularExpressionValidator>
                                                            <asp:Label ID="lblidc_tipodocarc" runat="server" Text='<%#Eval("idc_tipodocarc") %>' Visible="false"></asp:Label>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="row">
                                <div class="col-lg-10 col-md-10 col-sm-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="tipo, nombre, ruta, idc_tipodoc,idc_tipodocarc, extension">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar" CausesValidation="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar" CausesValidation="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="idc_tipodoc" HeaderText="tipo" Visible="false"></asp:BoundField>

                                                <asp:BoundField DataField="idc_tipodocarc" HeaderText="idc_tipodocarc" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="extension" HeaderText="extension" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre Documento"></asp:BoundField>
                                                <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <ul class="pager">
                                <li class="previous">
                                    <asp:LinkButton ID="lnkReturnDConc" Style="color: #fff; background-color: #f0ad4e;" CssClass="previous" OnClick="lnkReturnDConc_Click" runat="server"> <i class="fa fa-arrow-left"></i> Anterior</asp:LinkButton>
                                </li>
                            </ul>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary btn-block" OnClick="btnGuardar_Click" CausesValidation="false" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-danger btn-block" OnClick="btnCancelar_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4>Observaciones Generales <i class="fa fa-comment" aria-hidden="true"></i></h4>
                                    <asp:TextBox ID="txtobservaciones" CssClass="form-control" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 8000);" placeholder="Observaciones" Rows="5" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Button OnClick="Button1_Click" ID="Button1" runat="server" Visible="false" Text="Cadena Papeleria Detalles" />
                    <asp:Button OnClick="Button2_Click" ID="Button2" runat="server" Visible="false" Text="Cadena Papeleria" />
                    <asp:Button OnClick="Button3_Click" ID="Button3" runat="server" Visible="false" Text="Cadena ELECTOR/LICENCIA" />
                </div>
            </div>

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
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="myModalPreview" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong class="modal-title"></strong></h4>
                        </div>
                        <div class="modal-body">
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-9 col-md-8"></div>

                                <div class="col-lg-3 col-md-4 col-sm-4 col-xs-6">
                                    <input class="btn btn-primary btn-block" onclick="ModalClose();" value="Aceptar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>