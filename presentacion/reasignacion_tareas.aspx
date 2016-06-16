<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reasignacion_tareas.aspx.cs" Inherits="presentacion.reasignacion_tareas" %>

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
            $('#myModalOpciones').modal('hide');
            $('#myModalcss').modal('hide');
        }
        function ModalConfirm(cTitulo, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
        }
        function ModalConfirmC() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalcss').modal('show');
        }
        function ModalConfirmOpciones(cTitulo) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalOpciones').modal('show');
            $('#title_opciones').text(cTitulo);
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
    <h1 class="page-header">Tareas sin Empleado Activo</h1>
    <div class="row">
        <asp:Repeater ID="Repeater3" runat="server">
            <ItemTemplate>
                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                    <asp:LinkButton ID="lnkmistarea" CssClass='<%#Eval("css_class") %>' runat="server" CommandName='<%#Eval("pagina") %>' CommandArgument='<%#Eval("idc_tarea") %>' OnClick="lnkmistarea_Click" ToolTip='<%#Eval("desc_completa") %>'>

                                                          <h5> <%#Eval("faltantes") %></h5>
                        <h5>"<%#Eval("descripcion") %>"</h5>
                                                            <h5>FC: <%#Eval("fecha_compromiso") %> </h5>
                                                          <h6> Asigno: <%#Eval("puesto_asigna") %></h6>
                                                          <h6> Realiza: <%#Eval("puesto") %></h6>
                                                            <h6>Estado: <%#Eval("estado") %></h6>
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <!-- Modal -->
    <div class="modal fade modal-info" id="myModalOpciones" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4 id="title_opciones"></h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-xs-12">
                            <asp:Button ID="btnir" class="btn btn-success btn-block" runat="server" Text="Ver Tarea" OnClick="btnir_Click" />
                        </div>
                        <div class="col-lg-4 col-xs-12">
                            <asp:Button ID="btnReasignar" class="btn btn-primary btn-block" runat="server" Text="Reasignar Tarea" OnClick="btnReasignar_Click" />
                        </div>
                        <div class="col-lg-4 col-xs-12">
                            <asp:Button ID="btncancelartarea" class="btn btn-danger btn-block" runat="server" Text="Cancelar Tarea" OnClick="btncancelartarea_Click" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-12">
                        <input id="Nodddp" type="button" class="btn btn-info btn-block" onclick="ModalClose();" value="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;" runat="server" id="cancelar">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4><strong>Cancelación de la Tarea</strong></h4>
                            <h5>Ingrese un Motivo para la Cancelacion</h5>
                            <asp:TextBox ID="txtcance" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Observaciones" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="text-align: center;" runat="server" id="reasignar">
                        <h4><strong>Reasignación de la Tarea</strong> <small>Seleccione las opciones indicadas</small></h4>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 " runat="server" id="asigna">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnkbuscarpuestos" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="txtpuesto_filtro" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlPuesto" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <label>Empleado que Revisara la Tarea</label>
                                        <asp:DropDownList ID="ddlPuesto" OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                        <label></label>
                                        <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                        <label></label>
                                        <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 " runat="server" id="realiza">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnkbusrealiza" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="txtfiltrorealiza" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlpuestorealiza" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <label>Empleado que Realizara la Tarea</label>
                                        <asp:DropDownList ID="ddlpuestorealiza" OnSelectedIndexChanged="ddlpuestorealiza_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                        <label></label>
                                        <asp:TextBox ID="txtfiltrorealiza" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtfiltrorealiza_TextChanged" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                        <label></label>
                                        <asp:LinkButton ID="lnkbusrealiza" runat="server" CssClass="btn btn-success btn-block" OnClick="txtfiltrorealiza_TextChanged">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="YesSE" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="YesSE_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="Nop" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade modal-info" id="myModalcss" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4>¿Confirma esta operación?
                            </h4>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="Noa" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>