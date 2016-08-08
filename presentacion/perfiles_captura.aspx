<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="perfiles_captura.aspx.cs" Inherits="presentacion.perfiles_captura2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#myModal').modal('hide');
            $('#ModalArchivos').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalArchi(value) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#ModalArchivos').modal('show');
            $('#confirmContenidoarchivos').text(value);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
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
        function GoSection(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 500);
        }
    </script>
    <style type="text/css">
        .img_btn {
            Width: 19px;
            Height: 18px;
        }

        .scroll {
            padding: 10px;
            background-color: #F2F2F2;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="page-header">
        <h1>
            <strong>
                <asp:Label ID="lblsession" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Literal ID="lit_titulo" runat="server"></asp:Literal><span>
                    <asp:Label ID="lblmensaje" runat="server" Text=""></asp:Label></span></strong></h1>
        <asp:HiddenField ID="oc_paginaprevia" runat="server" />
        <asp:CheckBox ID="check_borr_prod" runat="server" Text="borrador" Visible="False" />
    </div>
    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre del Perfil</strong></h4>
            <asp:TextBox onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" placeholder="Escriba el nombre del perfil" ID="txtnomperfil" onkeypress="return isNumber(event);" class="form-control" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
            <asp:HiddenField ID="oc_idc_puestoperfil" runat="server" Value="0" />
            <br />
            <div class="btn-group">
                <asp:HiddenField ID="ocgpoidmenu" runat="server" Value="0" />
                <asp:Repeater ID="repite_menu_grupos" runat="server" OnItemDataBound="repite_menu_grupos_ItemDataBound" OnItemCommand="repite_menu_grupos_ItemCommand">

                    <ItemTemplate>
                        <asp:Button ID="btnmenugpo" runat="server" Text="" />
                        <asp:HiddenField ID="oc_gpoid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "idc_perfilgpo").ToString() %>' />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <!-- titulo del grupo donde a dado clic -->

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-body">
                    <h2 style="text-align: center;"><strong>
                        <asp:Label ID="lblgrupotitulo" runat="server" Text=""></asp:Label></strong></h2>
                    <div class="alert fresh-color alert-danger alert-dismissible" role="alert" runat="server" id="inicio">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <strong>Para Comenzar!</strong> Seleccione un Grupo para empezar a llenar el formulario.
                    </div>
                    <!-- GRUPO DETALLE -->
                    <asp:Panel ID="panel_grupo_detalle" runat="server" Visible="False">

                        <div class="row">
                            <div class="col-lg-12">

                                <!-- inputs -->
                                <!--libre -->
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">

                                    <ContentTemplate>
                                        <asp:Panel ID="panel_gpo_libre" runat="server" CssClass="row" Visible="False">
                                            <div class="col-lg-12">
                                                <asp:HiddenField ID="oc_idcperfilgpo_lib" runat="server" />
                                                <h4><strong><i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;<asp:Label ID="lblgpolibre" runat="server" CssClass="etiqueta"></asp:Label></strong></h4>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtgpolibre" runat="server" CssClass="form-control" onkeypress="return isNumber(event);"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <asp:ImageButton ID="btnaddgpolib" CssClass="img_btn" runat="server" ImageUrl="~/imagenes/btn/icon_agregar.png" OnClick="btnaddgpolib_Click" ToolTip="Agregar" />
                                                        <asp:ImageButton ID="btneditgpolib" CssClass="img_btn" runat="server" ImageUrl="~/imagenes/btn/icon_actualizar.png" Visible="False" ToolTip="Actualizar" OnClick="btneditgpolib_Click" />
                                                        <asp:ImageButton ID="btncanceleditgpolib" CssClass="img_btn" runat="server" ImageUrl="~/imagenes/btn/icon_regresar.png" ToolTip="Cancelar" Visible="False" OnClick="btncanceleditgpolib_Click" />
                                                        <asp:HiddenField ID="oc_edit_idgpolib" runat="server" Value="0" />
                                                    </span>
                                                </div>
                                                <p class="help-block" style="color: grey;">
                                                    <asp:Literal ID="lit_mensaje_gpo_lib" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </asp:Panel>
                                        <asp:Label ID="lblmin" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblmax" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="table-responsive">
                                                    <!-- grid gpo libre -->
                                                    <asp:GridView ID="gridgpo_lib" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" DataKeyNames="idc_perfilgpod_dato_lib,orden" OnRowCommand="gridgpo_lib_RowCommand" OnRowDataBound="gridgpo_lib_RowDataBound1">
                                                        <Columns>
                                                            <asp:BoundField DataField="texto" HeaderText="Valor">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="up" ImageUrl="~/imagenes/up_btn.png" ShowHeader="False" Visible="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:ButtonField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="down" ImageUrl="~/imagenes/down_btn.png" ShowHeader="False" Visible="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:ButtonField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="editgpolibre" ImageUrl="~/imagenes/btn/icon_editar.png" Text="Editar" HeaderText="Editar" ShowHeader="True">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:ButtonField>

                                                            <asp:TemplateField HeaderText="Borrar">
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        Text="" ImageUrl="~/imagenes/btn/icon_delete.png" CommandName="deletegpolibre"></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="grupo" HeaderText="Grupo" Visible="False" />
                                                            <asp:BoundField DataField="orden" HeaderText="orden" Visible="false" ItemStyle-Width="40px" />
                                                            <asp:TemplateField HeaderText="Orden" Visible="true">
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderStyle Width="100px"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtOrderGrupo" CssClass="form-control" Text='<%#Eval("orden") %>' AutoPostBack="true" OnTextChanged="txtOrderGrupo_TextChanged" onkeypress="return validarEnteros(event);" runat="server" CommandName="change_order" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <!--opciones -->
                                        <asp:Panel ID="panel_gpo_opc" runat="server" Visible="False">
                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <h4><strong><i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;<asp:Label ID="lblgpoopc" runat="server" Text="" CssClass="etiqueta"></asp:Label></strong></h4>
                                                    <p class="help-block" style="color: grey;">
                                                        <asp:Literal ID="lit_mensaje_gpo_opc" runat="server"></asp:Literal>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="scroll">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <asp:CheckBoxList ID="check_gpo_opc" runat="server" CssClass="radio3 radio-check radio-info radio-inline" AutoPostBack='<%# (lblmax.Text).ToString() == "0" ?  false:true  %>' OnSelectedIndexChanged='<%# (lblmax.Text).ToString() == "0" ?  "":"check_gpo_opc_SelectedIndexChanged"  %>'></asp:CheckBoxList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- ETIQUETAS -->
                    <asp:Panel ID="panel_etiquetas" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:UpdatePanel ID="Panel_repet_el" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Repeater ID="repite_etiqueta_controles" runat="server" OnItemDataBound="repite_etiqueta_controles_ItemDataBound" OnItemCommand="repite_etiqueta_controles_ItemCommand">
                                            <ItemTemplate>
                                                <!--etiqueta libre -->
                                                <asp:Label ID="lblmin" runat="server" Text='<%#Eval("minimo_sel") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblmax" runat="server" Text='<%#Eval("maximo_sel") %>' Visible="false"></asp:Label>
                                                <div class='form-group <%# DataBinder.Eval(Container.DataItem, "libre").ToString() == "True" ?  "show":"hide"  %>'>
                                                    <asp:HiddenField ID="oc_idc_perfiletiqlibre" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "idc_perfiletiq").ToString()   %>' />
                                                    <h4><strong><i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;<asp:Label ID="lbletiquetalibre" runat="server" Text="lib" CssClass="etiqueta"></asp:Label></strong></h4>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtetiquetalibre" runat="server" CssClass="form-control" onkeypress="return isNumber(event);"></asp:TextBox>
                                                                <span class="input-group-addon">
                                                                    <asp:HiddenField ID="oc_edit_idcperfiletiq_opc_dato_lib" runat="server" Value="0" />
                                                                    <asp:ImageButton ID="btnadd_etiq_lib" runat="server" CssClass="img_btn" ImageUrl="~/imagenes/btn/icon_agregar.png" CommandName="add_valor_lib_etiq" />
                                                                    <asp:ImageButton ID="btnedit_etiq_lib" runat="server" CssClass="img_btn" ImageUrl="~/imagenes/btn/icon_actualizar_16.png" CommandName="edit_valor_lib_etiq" Visible="false" />
                                                                    <asp:ImageButton ID="btncancel_etiq_lib" runat="server" CssClass="img_btn" ImageUrl="~/imagenes/btn/icon_regresar_16.png" Visible="false" CommandName="cancel_valor_lib_etiq" />
                                                                </span>
                                                            </div>
                                                            <p class="help-block" style="color: grey">
                                                                <asp:Literal ID="lit_mensaje_etiq" runat="server"></asp:Literal>
                                                            </p>
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="grid_dinamico_etiqlibre" DataKeyNames="idc_perfiletiq,texto, orden" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" OnRowCommand="grid_dinamico_etiqlibre_RowCommand" OnRowDataBound="grid_dinamico_etiqlibre_RowDataBound">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="texto" HeaderText="Texto">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>

                                                                        <asp:ButtonField ButtonType="Image" CommandName="down" ImageUrl="~/imagenes/down_btn.png" ShowHeader="False" Visible="false">
                                                                            <ItemStyle HorizontalAlign="Center" Width="0px" />
                                                                        </asp:ButtonField>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="up" ImageUrl="~/imagenes/up_btn.png" ShowHeader="False" Visible="false">
                                                                            <ItemStyle HorizontalAlign="Center" Width="0px" />
                                                                        </asp:ButtonField>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="view_file" ImageUrl="~/imagenes/btn/icon_buscar.png" HeaderText="Ver" ShowHeader="False" Visible="true">
                                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                        </asp:ButtonField>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="add_file" ImageUrl="~/imagenes/anexar.png" HeaderText="Archivo" ShowHeader="False" Visible="true">
                                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                        </asp:ButtonField>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="updateetiqlibre" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar">
                                                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                        </asp:ButtonField>
                                                                        <asp:TemplateField HeaderText="Borrar">
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <HeaderStyle Width="30px"></HeaderStyle>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                                    Text="" ImageUrl="~/imagenes/btn/icon_delete.png" CommandName="deleteetiqlibre"></asp:ImageButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Orden">
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <HeaderStyle Width="100px"></HeaderStyle>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="UpId"
                                                                                    UpdateMode="Always" ChildrenAsTriggers="true">
                                                                                    <ContentTemplate>
                                                                                        <asp:TextBox ID="txtOrder" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtOrder_TextChanged" onkeypress="return validarEnteros(event);" runat="server" CommandName="change_order" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:TextBox>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="orden" HeaderText="orden" Visible="false" ItemStyle-Width="40px" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--etiqueta con opciones -->
                                                <div class='form-group <%# DataBinder.Eval(Container.DataItem, "libre").ToString() == "False" ?  "show":"hide"  %>'>
                                                    <asp:HiddenField ID="oc_idc_perfiletiqopc" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "idc_perfiletiq").ToString()   %>' />
                                                    <h4><strong><i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;<asp:Label ID="lbletiquetaopcion" runat="server" Text="opc" CssClass="etiqueta"> </asp:Label></strong></h4>
                                                    <p class="help-block" style="color: grey">
                                                        <asp:Literal ID="lit_mensaje_etiq_opc" runat="server"></asp:Literal>
                                                    </p>
                                                    <%-- <asp:DropDownList ID="cboxetiquetaopc" runat="server" CssClass="btn btn-default dropdown-toggle"></asp:DropDownList>--%>
                                                    <div id='check_<%# DataBinder.Eval(Container.DataItem, "idc_perfiletiq").ToString()  %>' class="cheks scroll">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:CheckBoxList ID="check_etiqopc" CssClass="radio3 radio-check radio-info radio-inline" runat="server" AutoPostBack="true" OnSelectedIndexChanged="check_etiqopc_SelectedIndexChanged"></asp:CheckBoxList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <!-- GRUPO DETALLE -->
    <%--<div class="row">
        <div class="col-lg-12">
            <asp:Panel ID="panel_grupo_detalle" runat="server" Visible="False">
                <div class="panel panel-primary fresh-color">
                    <div class="panel-heading" style="text-align: center;">Detalles</div>
                    <div class="panel-body">
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>--%>
    <!-- GRUPO DETALLE FIN -->
    <!-- ETIQUETAS -->
    <%-- <div class="row">
        <div class="col-lg-12">
            <asp:Panel ID="panel_etiquetas" runat="server" Visible="false">
                <div class="panel panel-primary fresh-color">
                    <div class="panel-heading" style="text-align: center;">Opciones</div>
                    <div class="panel-body">
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>--%>
    <!-- ETIQUETAS FIN -->
    <!--GRIDVIEW etiq ib -->
    <!-- DOCUMENTOS REQUERIDOS -->

    <div class="row">
        <div class="col-lg-12 col-xs-12">
            <h4><strong><i class="fa fa-clipboard" aria-hidden="true"></i>&nbsp;Documentos Requeridos</strong></h4>
            <div class="scroll">
                <asp:CheckBoxList ID="checklist_docs" CssClass="radio3 radio-check radio-info radio-inline" runat="server"></asp:CheckBoxList>
            </div>
        </div>
    </div>
    <!--horarios-->
    <div class="row" runat="server" id="hpor" visible="true">
        <div class="col-lg-12 col-xs-12">
            <h4><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Horarios Aplicables</strong></h4>
            <div class="scroll">
                <asp:CheckBoxList ID="cbxhorarios" CssClass="radio3 radio-check radio-info radio-inline" runat="server"></asp:CheckBoxList>
            </div>
        </div>
    </div>
    <!--EXAMENES-->
    <div class="row">
        <div class="col-lg-12 col-xs-12">
            <h4><strong><i class="fa fa-file-text-o" aria-hidden="true"></i>&nbsp;Examenes Disponibles</strong></h4>
            <div class="scroll">
                <asp:CheckBoxList ID="chxExamenes" CssClass="radio3 radio-check radio-info radio-inline" runat="server"></asp:CheckBoxList>
            </div>
        </div>
    </div>
    <!--ANEXOS-->
    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-files-o" aria-hidden="true"></i>&nbsp;Anexos</strong></h4>
            <h5>Agregar Archivo</h5>
            <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="form-control" placeholder="Descripcion del documento" onkeypress="return isNumber(event);"></asp:TextBox>
        </div>
        <div class="col-lg-12">
            <div class="input-group">
                <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                <span class="input-group-addon" style="color: #fff; background-color: #1ABC9C;">
                    <asp:LinkButton ID="lnkGuardarPape" Style="color: #fff; background-color: #1ABC9C;" OnClick="lnkGuardarPape_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                </span>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="id_archi,descripcion, nombre, ruta, extension">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="extension" HeaderText="extension" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre Documento"></asp:BoundField>
                        <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion"></asp:BoundField>
                        <asp:BoundField DataField="id_archi" HeaderText="id_archi" Visible="false"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- botones -->

    <div class="row">
        <div class="col-lg-6">
            <div class="form-group">
                <asp:Button ID="btnGuardarTodo" runat="server" Text="Guardar" CssClass="btn btn-primary  btn-block" OnClick="btnGuardarTodo_Click" />
            </div>
        </div>
        <div class="col-lg-6">
            <div class="form-group">
                <asp:Button ID="btnCancelarTodo" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelarTodo_Click" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">

            <!-- grid gpo opc -->
            <asp:GridView ID="gridgpo_opc" runat="server" CssClass="table table-bordered table-hover table-striped" Width="200px" Visible="False">
            </asp:GridView>
        </div>
    </div>
    <!--botones -->

    <!-- CAMPOS OCULTOS
                       se utilizan para saber que identificador usar si es con las tablas borrador o de producción
                       -->
    <div class="row">
        <div class="col-lg-12">
            <asp:HiddenField ID="llave_puestoperfil" runat="server" />
            <asp:HiddenField ID="llave_d_eti_lib" runat="server" />
            <asp:HiddenField ID="llave_d_eti_opc" runat="server" />
            <asp:HiddenField ID="llave_d_gpo_lib" runat="server" />
            <asp:HiddenField ID="llave_d_gpo_opc" runat="server" />
        </div>
    </div>
    <!-- /.CONFIRMA -->
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
    <!-- /.CONFIRMA -->
    <div id="ModalArchivos" class="modal fade bs-example-modal-lg" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content" style="text-align: center">
                <div class="modal-header" style="background-color: #428bca; color: white">
                    <h4><strong class="modal-title">Archivos Anexados </strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row">
                                <asp:Panel ID="PanelSubidadeArchiModal" Visible="false" runat="server">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <h4>Adjuntar archivo a <strong>
                                                <label id="confirmContenidoarchivos"></label>
                                            </strong></h4>
                                            <div class="input-group">
                                                <asp:FileUpload ID="filearchivoetiqueta" CssClass="form-control" runat="server" />
                                                <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                                    <asp:LinkButton ID="lnkaddfileetiqueta" Style="color: #fff; background-color: #3c8dbc;" OnClick="lnkaddfileetiqueta_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkEditar" CssClass="btn btn-warning btn-block" runat="server" OnClick="lnkEditar_Click">Editar Archivo <i class="fa fa-pencil"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkEliminar" CssClass="btn btn-danger btn-block" runat="server" OnClick="lnkEliminar_Click">Eliminar Archivo <i class="fa fa-times-circle"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkDescargarArchi" CssClass="btn btn-info btn-block" runat="server" OnClick="lnkDescargarArchi_Click">Descargar Archivo <i class="fa fa-cloud-download"></i></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-lg-6 col-md-6 col-sm-12">

                                    <asp:GridView Visible="false" ID="gridPapeleriaEtiquetas" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleriaEtiquetas_RowCommand" DataKeyNames="etiqueta, nombre, ruta">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="etiqueta" HeaderText="Etiqueta" Visible="true"></asp:BoundField>
                                            <asp:BoundField DataField="ruta" HeaderText="ruta" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre Documento"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-lg-12">
                            <input id="noarchi" class="btn btn-primary btn-block" onclick="ModalClose();" value="Cerrar" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>