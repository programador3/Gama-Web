<%@ Page Title="Captura" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="puestos_alta_captura.aspx.cs" Inherits="presentacion.puestos_alta_captura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .scroll {
            padding: 10px;
            background-color: #F2F2F2;
        }
    </style>
    <script type="text/javascript">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <h3 class=" page-header">Solicitud Nuevo Puesto
                        <span>
                            <asp:LinkButton ID="lnkeditar" Visible="false" runat="server" CssClass="btn btn-info" OnClick="lnkeditar_Click">Editar Solicitud&nbsp;<i class="fa fa-pencil-square" aria-hidden="true"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkcancelaredicion" Visible="false" runat="server" CssClass="btn btn-danger" OnClick="lnkcancelaredicion_Click">Cancelar Edición&nbsp;<i class="fa fa-times-circle" aria-hidden="true"></i></asp:LinkButton>
                        </span>
                    </h3>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre del Puesto</strong></h5>
                    <asp:TextBox ID="txtnombre" Style="text-transform: uppercase;" CssClass=" form-control" placeholder="Nombre del Puesto" MaxLength="50" runat="server"></asp:TextBox>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <h5><strong><i class="fa fa-key" aria-hidden="true"></i>&nbsp;Clave</strong></h5>
                    <asp:TextBox ID="txtClave" CssClass=" form-control" placeholder="Clave" MaxLength="20" runat="server"></asp:TextBox>
                </div>

                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <h5><strong><i class="fa fa-coffee" aria-hidden="true"></i>&nbsp;Depto</strong></h5>
                    <asp:DropDownList ID="ddldepto" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>

                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <h5><strong><i class="fa fa-university" aria-hidden="true"></i>&nbsp;Sucursal</strong></h5>
                    <asp:DropDownList ID="ddlsucursal" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <h5><strong><i class="fa fa-child" aria-hidden="true"></i>&nbsp;Tipo Uniforme</strong></h5>
                    <asp:DropDownList ID="ddluniforme" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <h5>
                        <strong>
                        <i class="fa fa-vcard-o"></i>&nbsp;Jefe del Puesto </strong>
                    </h5>
                    <asp:DropDownList ID="ddlPuesto" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="div_buscar_puesto" runat="server">
                    <h5>
                        <strong><i class="fa fa-search"></i>&nbsp;Buscar Jefe</strong>
                    </h5>
                    <asp:TextBox Style="font-size: 12px;" ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control2" Width="80%"
                        AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                    <span> <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-info" OnClick="lnkbuscarpuestos_Click"><i class="fa fa-search"></i></asp:LinkButton></span>

                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-id-badge" aria-hidden="true"></i>&nbsp;Perfil</strong></h5>
                    <asp:DropDownList ID="ddlperfil" CssClass="form-control2" Width="80%" runat="server"></asp:DropDownList>
                    <span>
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info" OnClick="LinkButton2_Click1"><i class="fa fa-info-circle"></i></asp:LinkButton>
                    </span>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Horario Laboral</strong></h5>
                    <asp:DropDownList ID="ddlhorarios" CssClass=" form-control2 " Width="80%" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlhorarios_SelectedIndexChanged"></asp:DropDownList>
                    <span>
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info" OnClick="LinkButton1_Click1"><i class="fa fa-info-circle"></i></asp:LinkButton>
                    </span>
                    <br />
                    <div class="table table-responsive" id="div_details_horario" runat="server" visible="false">
                        <asp:GridView ID="grid_Detalles" CssClass="table table-responsive table-bordered" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:BoundField DataField="nombre_dia" HeaderText="Dia"></asp:BoundField>
                                <asp:BoundField DataField="horario" HeaderText="Horario"></asp:BoundField>
                                <asp:BoundField DataField="horario_comida" HeaderText="Comida"></asp:BoundField>
                                <asp:CheckBoxField DataField="laborable" Text="Laborable"></asp:CheckBoxField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Salario</strong></h5>
                    <asp:DropDownList ID="ddlsueldo" CssClass="form-control" Width="100%" runat="server"></asp:DropDownList>
                  
                </div>
                <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Dias para reclutar</strong></h5>
                    <asp:TextBox ID="txtdias_reclu" Text="5"  onblur="ValidateRange(this,1,90,'El valor debe ser de 1 - 90 dias');" onkeypress="return validarEnteros(event);" 
                        onfocus="$(this).select();" TextMode="Number" CssClass="form-control2" Width="200px" runat="server"></asp:TextBox>
                  
                </div>

            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Clasificaciones</strong></h5>
                    <div class="scroll">

                        <asp:CheckBoxList ID="cbxClasificacion" CssClass="radio3 radio-check radio-info radio-inline scroll" runat="server"></asp:CheckBoxList>
                    </div>
                </div>
            </div>
            <div class="row" runat="server" id="click">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-block" runat="server" OnClick="LinkButton1_Click">Guardar</asp:LinkButton>
                    <asp:LinkButton ID="lnkautorizar" Visible="false" CssClass="btn btn-success btn-block" runat="server" OnClick="lnkautorizar_Click">Autorizar Solicitud</asp:LinkButton>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <asp:LinkButton ID="lnkrechazarsolicitud" Visible="false" CssClass="btn btn-danger btn-block" runat="server" OnClick="lnkrechazarsolicitud_Click">Eliminar Solicitud</asp:LinkButton>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <asp:LinkButton ID="lnkcancelarsolicitud" Visible="false" CssClass="btn btn-info btn-block" runat="server" OnClick="lnkrechazarsolicitud_Click1">Regresar Solicitud</asp:LinkButton>
                    <asp:LinkButton ID="lnkguardaEdicion" Visible="false" CssClass="btn btn-primary btn-block" runat="server" OnClick="lnkguardaEdicion_Click">Guardar Edición</asp:LinkButton>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <asp:LinkButton ID="lnkcancelar" CssClass="btn btn-danger btn-block" runat="server" OnClick="LinkButton2_Click">Cancelar</asp:LinkButton>
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
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 " id="div_observaciones" runat="server">
                                    <h5>Ingrese Observaciones
                                    </h5>
                                    <asp:TextBox ID="txtobservaciones" placeholder="Ingrese Observaciones" TextMode="MultiLine" CssClass=" form-control" Rows="3" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-6 col-xs-6">
                                <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" OnClientClick="ModalClose();" Text="Aceptar" OnClick="Yes_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" type="button" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
