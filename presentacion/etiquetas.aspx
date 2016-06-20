<%@ Page Title="Agregar Nueva Etiqueta" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="etiquetas.aspx.cs" Inherits="presentacion.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function LimpiarTexto(txt) {
            txt.value = "";
        }
        function validarNum(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /\d/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
    </script>
    <%--    script para evitar que el usuario ingrese con forward--%>
    <%-- <script type="text/javascript">
        window.history.forward(1);
    </script>--%>
    <script type="text/javascript">
        function Confirm() {
            var confirma_valor = document.createElement("INPUT");
            confirma_valor.type = "hidden";
            confirma_valor.name = "confirma_valor";
            if (confirm("¿Estas Seguro de realizar esta operación?, Recuerda que vualquier cambio a una etiqueta, cambiara la referencia de esta etiqueta.")) {
                confirma_valor.value = "Yes";
            } else {
                confirma_valor.value = "No";
            }
            document.forms[0].appendChild(confirma_valor);
        }
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalClose() {
            $('#myModal').modal('hide');
        }
    </script>
    <script src="js/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="js/sweetalert.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div id="page-wrapper">
        <div class="container-fluid">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="page-header">
                        <h1>Etiquetas</h1>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-xs-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Añadir Etiquetas</h3>
                                </div>
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <h4>Grupo:</h4>
                                            <asp:TextBox ID="txtNombreGrupo" onkeypress="return isNumber(event);" runat="server" class="form-control" type="text" ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <h4>Nombre de la Etiqueta</h4>
                                            <asp:TextBox ID="txtNombreEtiqueta" runat="server" class="form-control" type="text" placeholder="Nombre de la Etiqueta" ToolTip="Escriba el Nombre de la Nueva Etiqueta"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <br />
                                            <h4>Tipo de Entrada de Datos
                                                             <span>
                                                                 <asp:Button ID="btnInfoTipoDato" class="btn btn-info btn-xs" runat="server" Text="Info" OnClick="btnInfoTipoDato_Click" ToolTip="Click Para obtener Información Acerca de esto" />
                                                             </span>
                                            </h4>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <%-- <asp:CheckBox ID="cbxLibre" runat="server" Text="Libre" AutoPostBack="true" OnCheckedChanged="Click"  ToolTip="Opcion de Entrada de Texto Libre" BorderStyle="None" CssClass="btn-md" />
                                                         <br />
                                                         <asp:CheckBox ID="cbxOpciones" runat="server" Text="Con Opciones" AutoPostBack="true" OnCheckedChanged="cbxOpciones_CheckedChanged"  ToolTip="Etiqueta con Opciones predefinidas" />
                                            --%>
                                            <asp:RadioButtonList ID="rbtnTipoEntrada" runat="server" OnSelectedIndexChanged="rbtnTipoEntrada_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Selected="True" Value="1">Libre</asp:ListItem>
                                                <asp:ListItem Value="0">Con Opciones</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h4>Asigne un Orden para la Etiqueta</h4>
                                            <div class="row">
                                                <div class="col-lg-1">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtOrden" CssClass="form-control" onkeypress="return validarNum(event)" runat="server" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-xs-12">
                            <asp:Panel ID="Etiquetalibre" runat="server" Visible="false">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Añadir Etiqueta Libre</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <h4>¿Manejar Limite?
                                                         <span>
                                                             <asp:Button ID="Button1" class="btn btn-info btn-xs" runat="server" Text="Info" OnClick="Button1_Click" ToolTip="Click Para obtener Información Acerca de esto" />
                                                         </span></h4>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:RadioButtonList ID="rblLibre" runat="server" OnSelectedIndexChanged="rblLibre_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Selected="True">Si</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                            </div>

                                            <asp:Panel ID="PanelEtiquetaLibre" runat="server">
                                                <div class="col-md-1">
                                                    Minimo<br />
                                                    <asp:TextBox ID="txtMinimoLibre" runat="server" onkeypress="return validarNum(event)" class="form-control" type="text" Text="0" Width="72px" MaxLength="2"></asp:TextBox>
                                                </div>

                                                <div class="col-md-1">
                                                    Maximo<br />
                                                    <asp:TextBox ID="txtMaximoLibre" runat="server" onkeypress="return validarNum(event)" class="form-control" type="text" Text="0" Width="72px" MaxLength="2" AutoPostBack="True" OnTextChanged="txtMaximoLibre_TextChanged"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-xs-12">
                            <asp:Panel ID="EtiquetaOpciones" runat="server" Visible="False">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Añadir Etiqueta con Opciones</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <h4>¿Manejar Limite?
                                                                  <span>
                                                                      <asp:Button ID="Button2" class="btn btn-info btn-xs" runat="server" Text="Info" OnClick="Button2_Click" ToolTip="Click Para obtener Información Acerca de esto" />
                                                                  </span>
                                                </h4>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:RadioButtonList ID="rblOpciones" runat="server" OnSelectedIndexChanged="rblOpciones_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Selected="True">Si</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <asp:Panel ID="PanelOpciones" runat="server">
                                                <div class="col-md-1">
                                                    Minimo<br />
                                                    <asp:TextBox ID="txtMinimoOpciones" Text="0" onkeypress="return validarNum(event)" runat="server" class="form-control" type="text" Width="72px" MaxLength="2"></asp:TextBox>
                                                </div>

                                                <div class="col-md-1">
                                                    Maximo<asp:TextBox ID="txtMaximoOpciones" Text="0" runat="server" onkeypress="return validarNum(event)" class="form-control" type="text" Width="72px" AutoPostBack="True" OnTextChanged="txtMaximoOpciones_TextChanged"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </div>

                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gridOpciones" EventName="RowCommand" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <h4>Nombre de la Opción</h4>
                                                        <asp:TextBox ID="txtNombreOpcion" onkeypress="return isNumber(event);" runat="server" class="form-control" type="text"></asp:TextBox>
                                                        <asp:Button ID="btnAgregarOpcion" runat="server" class="btn btn-info btn-block" Text="Agregar" OnClick="btnAgregarOpcion_Click" />
                                                    </div>
                                                    <div class="col-md-1"></div>
                                                    <div class="col-md-8">
                                                        <br />
                                                        <br />
                                                        <h4 hidden="hidden">La Opción
                                                               <asp:DropDownList ID="dlEtiqueta" runat="server" class="btn btn-default dropdown-toggle">
                                                                   <asp:ListItem Value="select">Seleccione</asp:ListItem>
                                                               </asp:DropDownList>
                                                            bloquea a la Etiqueta
                                                                <asp:DropDownList ID="dlEtiquetaBloqueada" runat="server" AutoPostBack="True" class="btn btn-default dropdown-toggle" OnSelectedIndexChanged="dlEtiquetaBloqueada_SelectedIndexChanged" Width="133px">
                                                                    <asp:ListItem Value="select">Seleccione</asp:ListItem>
                                                                </asp:DropDownList>
                                                            <span>
                                                                <asp:Button ID="Button3" class="btn btn-info btn-xs" runat="server" Text="Info" OnClick="Button3_Click" ToolTip="Click Para obtener Información Acerca de esto" />
                                                            </span>
                                                        </h4>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gridOpciones" DataKeyNames="idc_perfiletiq_opc,descripcion,nombre" runat="server" class="table table-condensed table-bordered table-hover table table-striped" OnRowCommand="gridOpciones_RowCommand" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="idc_perfiletiq_opc" HeaderText="ID" SortExpression="idc_perfiletiq_opc" Visible="False" />
                                                                    <asp:ButtonField ButtonType="Image" HeaderStyle-Width="50px" CommandName="Editar" HeaderText="Editar" ShowHeader="True" Text="Editar" ImageUrl="~/imagenes/btn/icon_editar.png" />
                                                                    <asp:ButtonField ButtonType="Image" HeaderStyle-Width="50px" CommandName="Eliminar" HeaderText="Borrar" ShowHeader="True" Text="Borrar" ImageUrl="~/imagenes/btn/icon_delete.png" />
                                                                    <asp:BoundField DataField="descripcion" HeaderText="Opcion" SortExpression="descripcion" />
                                                                    <asp:BoundField DataField="nombre" HeaderText="Etiqueta" SortExpression="nombre" />
                                                                    <asp:BoundField DataField="borrado" HeaderText="Borrado" Visible="false" />
                                                                    <asp:BoundField DataField="nuevo" HeaderText="Nuevo" Visible="false" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <div class="table-responsive">

                                                            <asp:GridView ID="gridBloqueos" Enabled="false" runat="server" class="table table-condensed table-bordered table-hover table table-striped" OnRowDeleting="gridBloqueos_RowDeleting" AutoGenerateColumns="False" Visible="False">

                                                                <Columns>
                                                                    <asp:BoundField DataField="idc_perfiletiq_bloq" HeaderText="ID" SortExpression="idc_perfiletiq_bloq" Visible="False" />
                                                                    <asp:ButtonField ButtonType="Image" HeaderStyle-Width="50px" CommandName="Delete" HeaderText="Borrar" ShowHeader="True" Text="Borrar" ImageUrl="~/imagenes/btn/icon_delete.png" />
                                                                    <asp:BoundField DataField="nombre" HeaderText="La Etiqueta" SortExpression="nombre" />
                                                                    <asp:BoundField DataField="descripcion" HeaderText="Esta Bloqueada por la Opción" SortExpression="descripcion" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-lg-6 col-sm-6 col-xs-12">
                            <div class="form-group">

                                <asp:Button ID="btnActualizarFormulario" class="btn btn-info btn-block" runat="server" Text="Actualizar" Visible="false" OnClick="btnActualizarFormulario_Click" />
                                <asp:Button ID="btnGuardarFormulario" class="btn btn-primary btn-block" runat="server" Text="Guardar" OnClick="btnGuardarFormulario_Click" />
                            </div>
                        </div>
                        <div class="col-lg-6 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <asp:Button ID="btnCancelarFormulario" class="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="btnCancelarFormulario_Click" />
                            </div>
                        </div>

                        <div class="col-md-5">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading" style="text-align: center;">
                                    Etiquetas
                                </div>
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-lg-12">
                                            <div class=" table table-responsive">

                                                <asp:GridView ID="TotalEtiquetas" runat="server" HeaderStyle-HorizontalAlign="Center" DataKeyNames="idc_perfiletiq, nombre, grupo" class="table table-condensed table-bordered table-hover table table-striped" OnRowCommand="TotalEtiquetas_RowCommand" OnSelectedIndexChanged="TotalEtiquetas_SelectedIndexChanged1" AutoGenerateColumns="False">
                                                    <Columns>

                                                        <asp:CommandField HeaderStyle-Width="50px" ButtonType="Image" EditText="" HeaderText="Editar" SelectImageUrl="~/imagenes/btn/icon_editar.png" SelectText="" ShowSelectButton="True">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                        <asp:ButtonField HeaderStyle-Width="50px" ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:ButtonField>

                                                        <asp:BoundField DataField="idc_perfiletiq" HeaderText="ID" SortExpression="idc_perfiletiq" Visible="False" />
                                                        <asp:BoundField DataField="grupo" HeaderText="Grupo" SortExpression="grupo">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre" HeaderText="Etiqueta" SortExpression="nombre" />
                                                        <asp:CheckBoxField DataField="libre" HeaderText="¿Es de Tipo Libre?" />
                                                        <asp:BoundField DataField="minimo_sel" HeaderText="Minimo" SortExpression="minimo_sel" />
                                                        <asp:BoundField DataField="maximo_sel" HeaderText="Maximo" SortExpression="maximo_sel" />
                                                        <asp:HyperLinkField Visible="false" DataTextField="idc_perfiletiq" DataNavigateUrlFields="idc_perfiletiq" DataNavigateUrlFormatString="etiquetas_bloq.aspx?idc_perfiletiq={0}" HeaderText="Bloqueo" />
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- -->
                    <div class="row">
                        <div class="col-lg-12">
                            <!-- campos ocultos -->
                            <asp:HiddenField ID="ocidcperfilgpo" runat="server" />
                            <asp:HiddenField ID="ocidcusuario" runat="server" />
                            <asp:HiddenField ID="ocidcgpoetiqueta" runat="server" Value="0" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

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
        </div>
    </div>
</asp:Content>