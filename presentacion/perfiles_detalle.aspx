<%@ Page Title="Perfiles Vista Previa" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="perfiles_detalle.aspx.cs" Inherits="presentacion.perfiles_detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="js/jquery.js"></script>
    <script src="js/BlockHumberto.js"></script>
    <script type="text/javascript">
        function Ocultar(name) {
            $('#' + name).hide("slow"); //oculto mediante id
        }
        function Ver(name) {
            $('#' + name).show("slow"); //muestro mediante id
        }
    </script>

    <script type="text/javascript">

        function Return(TextMess, URLBACK) {
            swal({
                title: "Alerta",
                text: TextMess,
                type: "error",
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                allowEscapeKey: false
            },
               function () {
                   location.href = URLBACK;
               });
        }
     
    </script>
    <style>
        #IrArriba {
            position: fixed;
            bottom: 30px; /* Distancia desde abajo */
            right: 30px; /* Distancia desde la derecha */
        }

            #IrArriba span {
                width: 60px; /* Ancho del botón */
                height: 60px; /* Alto del botón */
                display: block;
            }
    </style>
    <script src="js/bootbox.min.js"></script>
    <style>
        .p-success {
            color: #FFF;
            background-color: #449D44;
            border-color: #398439;
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        .p-success-p {
            color: #FFF;
            background-color: #449D44;
            border-color: #398439;
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        .p-info {
            color: #FFF;
            background-color: #5BC0DE;
            border-color: #46B8DA;
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        .p-danger {
            color: #FFF;
            background-color: #C9302C;
            border-color: #AC2925;
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="row">
            <div class="col-lg-1 col-md-2 col-sm-12">
            </div>
            <div class="col-lg-10 col-md-8 col-sm-12">
                <div class="page-header">
                    <h1>
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click1" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Perfil de <small>
                            <asp:Label ID="txttitulo" runat="server"></asp:Label>
                        </small>
                        <asp:Label ID="lbltipo" runat="server" Text="" Visible="false"></asp:Label></h1>
                    <%--<asp:LinkButton ID="lnkReturn" runat="server" Onclick="lnkReturn_Click"><i class="fa fa-arrow-circle-left fa-3x"></i></asp:LinkButton>--%>
                    <asp:ImageButton ID="ImageButton5" runat="server" Height="50px" Width="50px" ToolTip="Regresar" OnClick="ImageButton5_Click" ImageUrl="~/imagenes/btn/icon_list_perfil.png" />
                    <asp:ImageButton ID="ImageButton4" runat="server" Height="50px" ImageUrl="~/imagenes/sign-up.png" Width="50px" ToolTip="Ver Perfil" OnClick="ImageButton4_Click" />
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="50px" ImageUrl="~/imagenes/word.png" Width="50px" ToolTip="Guarde este perfil en un documento de Word." OnClick="ImageButton1_Click" />
                    <asp:ImageButton ID="ImageButton2" runat="server" Height="60px" ImageUrl="~/imagenes/office_html2.png" ToolTip="Guarde este perfil en código HTML para poder utilizarlo posteriormente." OnClick="ImageButton2_Click" Width="60px" />
                    <asp:ImageButton ID="ImageButton3" runat="server" Height="55px" ImageUrl="~/imagenes/actions_document_preview.png" ToolTip="Mire una vista previa de como se vera el documento web con este código." Visible="False" Width="55px" OnClick="ImageButton3_Click" />
                    <asp:ImageButton ID="btnPerfilesP" runat="server" Height="50px" Width="50px" ToolTip="Perfiles Pendientes" OnClick="btnPerfilesP_Click" ImageUrl="~/imagenes/btn/home.png" Visible="False" />
                    <asp:DropDownList ID="ddlTipo" runat="server">
                        <asp:ListItem Selected="True" Value="todo" Text="Toda la información del perfil"></asp:ListItem>
                        <asp:ListItem Value="occ" Text="Solo la información externa(PARA PUBLICAR EN OCC)"></asp:ListItem>
                        <asp:ListItem Value="excp" Text="Toda la informacion menos la externa"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="lnkdescargarmanual" CssClass="btn btn-info" Width="50%" runat="server" OnClick="lnkdescargarmanual_Click">Descargar Manual Completo</asp:LinkButton>
                    <h3 style="text-align: center"><strong>
                        <asp:Label ID="lblComparacion" runat="server" Text="Comparación de Perfiles" Visible="false"></asp:Label></strong></h3>
                    <a style="display: none" id="btnGO" class="btn btn-primary btn-block" href="#PanelB">Ver Perfil Borrador <span class='glyphicon glyphicon-new-window'></span></a>
                    <br />
                </div>
                <asp:Panel ID="PanelHTML" Visible="False" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-primary fresh-color">
                                <div class="panel-heading">
                                    <b>Código HTML <i class="fa fa-file-code-o"></i>
                                        <small>De un CLICK en cualquier parte del código para que este se seleccione.</small></b>
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <textarea id="TextArea1" class="form-control" rows="25" readonly="readonly"> <asp:Label ID="txthtml" runat="server"></asp:Label>
                                                            </textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="VistaPrevia" Visible="False" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-primary fresh-color">
                                <div class="panel-heading">
                                    <b>Vista previa del código <small>Esta es la manera en la que el código se visualizara en otras pagina WEB, por ejemplo: MyOCC.</small></b>
                                </div>
                                <div class="panel-body">

                                    <asp:Label ID="lbVistaPrevia" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row" id="direccion" runat="server" visible="false">
                    <div class="col-lg-12">
                        <div class="panel panel-primary fresh-color">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-info-circle fa-fw"></i>Dirección(Lugar) de Trabajo</h3>
                            </div>
                            <div class="panel-body">
                                <div class="list-group">
                                    <a class="list-group-item">
                                        <asp:Label ID="lbldire" runat="server" Text="Dirección:"> <i class="fa fa-fw fa-check"></i></asp:Label><asp:Label ID="lbldir" runat="server" Text="No Hay Direccion Registrada"></asp:Label>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Repeater ID="RepeatDataPuesto" runat="server" OnItemDataBound="RepeatDataPuesto_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-primary fresh-color">
                                    <div class="panel-heading">
                                        <h3 class="panel-title"><i class="fa fa-info-circle fa-fw"></i>
                                            <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Grupo") %>' /></h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="list-group">
                                            <asp:Repeater ID="RepeaterChild" runat="server" OnItemDataBound="RepeatChild_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="up_rdp" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btneditar" />
                                                            <asp:AsyncPostBackTrigger ControlID="BtnVers" EventName="Click" />
                                                            <asp:PostBackTrigger ControlID="btnIRSw" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <a class="list-group-item">
                                                                <asp:Label ID="lblEtiqueta" runat="server" Text='<%#Eval("Etiqueta")%>'> <i class="fa fa-fw fa-check"></i></asp:Label><asp:Label ID="Label1" runat="server" Text='<%#Eval("Valor")%>'></asp:Label>

                                                                <asp:Button ID="btnIRSw" runat="server" Text="Descargar" Visible="false" CssClass="btn btn-default btn-sm" OnClick="btnIR_Click" />
                                                                <asp:Button ID="BtnVers" runat="server" Text="Ver" Visible="false" CssClass="btn btn-default btn-sm" OnClick="btnIR_Click" />
                                                                <asp:Button ID="btneditar" runat="server" Text="Editar" Visible="false" CssClass="btn btn-info btn-sm" OnClick="btneditar_Click" />
                                                            </a>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="Panelbtns" runat="server" Visible="false">

                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Button ID="btnVerPanelesProduccion" runat="server" Text="Ver Todos Publicados" OnClick="btnVerPanelesProduccion_Click" class="btn btn-success btn-block" />
                                    <%-- <input id="Sin" type="button"  value="Sin cambios" />--%>
                                    <br />
                                </div>
                                <div class="col-lg-6">
                                    <%--  <asp:Button ID="btnVerPanelesBorrador" runat="server" Text="Ver Todos de Borrador" OnClick="btnVerPanelesBorrador_Click" class="btn btn-primary btn-block" />
                                    --%>
                                </div>
                            </div>
                            <asp:Panel ID="PanelProduccion" runat="server" Visible="false">
                                <div class="panel panel-success fresh-color">
                                    <div class="panel-heading" style="text-align: center; color: #FFF; background-color: #449D44;">
                                        <h3><i class="glyphicon glyphicon-check"></i>Perfil Actual en Producción
                                               <span>
                                                   <button type="button" id="btn_p" class="btn btn-success btn-xs">
                                                       <span class="glyphicon glyphicon-chevron-down"></span>Ver
                                                   </button>
                                               </span>
                                        </h3>
                                    </div>
                                    <div id="panel_produccion" class="panel-body">

                                        <asp:Repeater ID="rpCompara" runat="server" OnItemDataBound="RepeatProduccion_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnOcultar_BodyPanel" EventName="Click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                                                <div class="panel">
                                                                    <div class="panel-heading p-success fresh-color">
                                                                        <h3 class="panel-title"><i class="fa fa-info-circle fa-fw"></i>
                                                                            <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Grupo") %>' /><span>
                                                                                <asp:LinkButton ID="btnOcultar_BodyPanel" runat="server" Text="Ocultar" OnClick="btnDinamico_Click" class="btn btn-success btn-xs" />
                                                                            </span></h3>
                                                                    </div>
                                                                    <asp:Panel ID="Produccion_Panel_Individual" runat="server" class="panel panel-body">
                                                                        <div id="lista_produccion" class="list-group">
                                                                            <asp:Repeater ID="RepeaterChild" runat="server" OnItemDataBound="RepeatChildProduccion_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <a class="list-group-item">
                                                                                        <asp:Label ID="lblEtiqueta" runat="server" Text='<%#Eval("Etiqueta")%>'> <i class="fa fa-fw fa-check"></i></asp:Label><asp:Label ID="Label1" runat="server" Text='<%#Eval("Valor")%>'></asp:Label>
                                                                                        <asp:Button ID="btnIRSe" runat="server" Text="Descargar" Visible="false" CssClass="btn btn-default btn-sm" OnClick="btnIR_Click" />

                                                                                        <asp:Button ID="BtnVere" runat="server" Text="Ver" Visible="false" CssClass="btn btn-default btn-sm" OnClick="btnIR_Click" />
                                                                                    </a>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                            <br />
                                                                            <div class="text-right">
                                                                                <a id="btnNew" class="btn btn-success">Publicado Actualmente <span class='fa fa-fw fa-check'></span></a>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="PanelB" class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Button ID="btnSinCambios" runat="server" Text="Ver Sin Cambios" OnClick="btnSinCambios_Click" class="btn btn-success btn-block" />
                                    <%-- <input id="Sin" type="button"  value="Sin cambios" />--%>
                                    <br />
                                </div>
                                <div class="col-lg-4">
                                    <asp:Button ID="btnEditado" runat="server" Text="Ver Grupo Editado" OnClick="btnEditado_Click" class="btn btn-info btn-block" />

                                    <br />
                                </div>
                                <div class="col-lg-4">
                                    <asp:Button ID="btnNuevoGrupo" runat="server" Text="Ver Nuevo Grupo" OnClick="btnNuevoGrupo_Click" class="btn btn-danger btn-block" />

                                    <br />
                                </div>
                            </div>
                            <asp:Panel ID="PanelBorrador" runat="server" Visible="false">
                                <div class="panel panel-primary fresh-color">
                                    <div class="panel-heading" style="text-align: center">
                                        <h3><i class="glyphicon glyphicon-cloud"></i>Perfil de Borrador seleccionado
                                               <span>
                                                   <button type="button" id="btn_b" class="btn btn-info btn-xs">
                                                       <span class="glyphicon glyphicon-chevron-down"></span>Ver
                                                   </button>
                                               </span>
                                        </h3>
                                    </div>
                                    <div id="panel_borrador" class="panel-body">

                                        <asp:Repeater ID="rpBorrador" runat="server" OnItemDataBound="rpBorrador_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnOcultar_BodyPanel_Borrador" EventName="Click" />
                                                        <asp:PostBackTrigger ControlID="lnkEdit" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <asp:Panel ID="Borrador_Panel_Individual" runat="server">
                                                                    <div class="panel">

                                                                        <asp:Panel ID="panel_comprobar_diferencia" runat="server">
                                                                            <h3 class="panel-title"><i class="fa fa-info-circle fa-fw"></i>
                                                                                <asp:Label ID="lblComment2" runat="server" Text='<%#Eval("Grupo") %>' />
                                                                                <asp:Label ID="lblTipoDiferencia" runat="server" Text="" Font-Size="Small"></asp:Label>
                                                                                <asp:LinkButton ID="btnOcultar_BodyPanel_Borrador" runat="server" Text="Ocultar" OnClick="btnDinamicoBorrador_Click" class="btn btn-success btn-xs" />
                                                                            </h3>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="Borrador_Panel_Indiv" runat="server" CssClass="panel-body">
                                                                            <div class="list-group">
                                                                                <asp:Repeater ID="RepeaterChild_B" runat="server" OnItemDataBound="RepeatChildB_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                            <Triggers>
                                                                                                <asp:PostBackTrigger ControlID="btnIR" />
                                                                                                <asp:AsyncPostBackTrigger ControlID="BtnVerW" EventName="Click" />
                                                                                            </Triggers>
                                                                                            <ContentTemplate>
                                                                                                <a class="list-group-item">
                                                                                                    <asp:Label ID="lblEtiquetaB" runat="server" Text='<%#Eval("Etiqueta")%>'> <i class="fa fa-fw fa-check"></i></asp:Label>
                                                                                                    <asp:Label ID="Label1B" runat="server" Text='<%#Eval("Valor")%>'></asp:Label>
                                                                                                    <span>
                                                                                                        <asp:Button ID="btnIRd" runat="server" Text="Descargar" Visible="false" CssClass="btn btn-default btn-sm" OnClick="btnIR_Click" />
                                                                                                        <asp:Button ID="BtnVerWd" runat="server" Text="Ver" Visible="false" CssClass="btn btn-default btn-sm" OnClick="btnIR_Click" />
                                                                                                        <asp:HyperLink ID="hplnkGO" runat="server" NavigateUrl='<%#Eval("Valor")%>' Visible="false">Descargar</asp:HyperLink></span>
                                                                                                </a>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <br />
                                                                                <div class="text-right">
                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Editar <span class='fa fa-arrow-circle-right'></span></asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater ID="rpBorrador_sinProduccion" runat="server" OnItemDataBound="rpBorradorNuevo_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <div class="panel">
                                                            <asp:Panel ID="panel_comprobar_diferencia" class="p-danger" runat="server">

                                                                <h3 class="panel-title"><i class="fa fa-info-circle fa-fw"></i>
                                                                    <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Grupo") %>' />
                                                                    <asp:Label ID="lblTipoDiferencia" runat="server" Text="" Font-Size="Small"></asp:Label></h3>
                                                            </asp:Panel>
                                                            <div class="panel panel-body">
                                                                <div class="list-group">
                                                                    <asp:Repeater ID="RepeaterChild_B_Nuevo" runat="server" OnItemDataBound="RepeatChildB_Nuevo_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel ID="UpdatePanel113" runat="server" UpdateMode="Conditional">
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="btnIRS" />
                                                                                    <asp:AsyncPostBackTrigger ControlID="BtnVerC" EventName="Click" />
                                                                                </Triggers>
                                                                                <ContentTemplate>
                                                                                    <a class="list-group-item">
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Etiqueta")%>'> <i class="fa fa-fw fa-check"></i></asp:Label>
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Valor")%>'></asp:Label>
                                                                                        <asp:Button ID="btnIRS" runat="server" Text="Descargar" Visible="false" CssClass="btn btn-default" OnClick="btnIR_Click" />
                                                                                        <asp:Button ID="BtnVerC" runat="server" Text="Ver" Visible="false" CssClass="btn btn-default btn-sm" OnClick="btnIR_Click" />
                                                                                    </a>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <br />
                                                                    <div class="text-right">
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Editar <span class='fa fa-arrow-circle-right'></span></asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <!-- /.row -->
    </div>
    <div id='IrArriba'>
        <a href='Arriba'>
            <img src="imagenes/btn/return-up.png" /></a>
    </div>
</asp:Content>