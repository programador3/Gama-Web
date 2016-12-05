<%@ Page Title="Registro de Visitas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="registro_visitas.aspx.cs" Inherits="presentacion.registro_visitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Webcam_Plugin/jquery.webcam.js"></script>
    <script type="text/javascript">
        function ModalClose() {
            $('#myModalPersonas').modal('hide');
            $('#myModalempresa').modal('hide');
            $('#myModal').modal('hide');
        }
        function ModalConfirmPero(cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalPersonas').modal('show');
            $('#myModalPersonas').removeClass('modal fade modal-info');
            $('#myModalPersonas').addClass(ctype);
            $('#content_modals').text(cContenido);
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
        function ModalConfirmEmpresa(cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalempresa').modal('show');
            $('#myModalempresa').removeClass('modal fade modal-info');
            $('#myModalempresa').addClass(ctype);
            $('#content_modalempresa').text(cContenido);
        }

    </script>
    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/registro_visitas.aspx") %>';
        $(function () {
            jQuery("#webcam").webcam({
                width: 300,
                height: 300,
                mode: "save",
                swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                debug: function (type, status) {
                },
                onSave: function (data) {
                    $.ajax({
                        type: "POST",
                        url: pageUrl + "/GetCapturedImage",
                        data: '',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            $("[id*=imgCapture]").css("display", "block");
                            $("[id*=imgCapture]").attr("src", r.d);
                            //$("[id*=webcam]").css("display", "none");
                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                },
                onCapture: function () {
                    webcam.save(pageUrl);
                    
                    $("#<%= txtimgurl.ClientID %>").val("ddd");
                }
            });
        });
        function Capture() {            
            $("<%= txtimgurl.ClientID %>").val("");
            webcam.capture();
            return false;
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
    <h1 class="page-header">Registro de Visitas</h1>
    <asp:TextBox ID="txtimgurl" runat="server" Visible="false"></asp:TextBox>
    <div class="row">
        <div class="col-lg-4 col-md-4 col-sm-12">
            <div id="webcam">
            </div>
            <asp:Button ID="btnCapture" CssClass="btn btn-info" Width="300px" Text="Capturar Foto" runat="server" OnClientClick="return Capture();" />
        </div>
        <div class="col-lg-8 col-md-8 col-sm-12">
            <asp:Image ID="imgCapture" runat="server" Style="display: none; height: 300px;" />
        </div>
    </div>
    <asp:Button ID="Button2" Visible="false" runat="server" Text="Button" OnClick="Button2_Click" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkpersonas" EventName="Click" />
            <asp:PostBackTrigger ControlID="lnkempresa"/>
            <asp:AsyncPostBackTrigger ControlID="ddlPuestoAsigna" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="Yes" />
        </Triggers>
        <ContentTemplate>
            <div class="row">                
                <div class="col-lg-12">
                    <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre del Visitante</strong></h4>
                    <div class="input-group">
                        <asp:TextBox Style="text-transform: uppercase;" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 8000);" placeholder="Visitante" ID="txtnombre" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtnombre_TextChanged"></asp:TextBox>
                        <span class="input-group-addon" style="color: #fff; background-color: #1ABC9C;">
                            <asp:LinkButton ID="LinkButton2" runat="server" Style="color: #fff; background-color: #1ABC9C;" OnClick="txtnombre_TextChanged">Buscar Existente <i class="fa fa-search"></i></asp:LinkButton>
                        </span>
                    </div>
                </div>
                <div class="col-lg-12">
                    <h4><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Empresa</strong></h4>
                    <div class="input-group">
                        <asp:TextBox Style="text-transform: uppercase;" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" placeholder="Empresa" ID="txtempresa" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtempresa_TextChanged"></asp:TextBox>
                        <span class="input-group-addon" style="color: #fff; background-color: #1ABC9C;">
                            <asp:LinkButton ID="LinkButton1" runat="server" Style="color: #fff; background-color: #1ABC9C;" OnClick="txtnombre_TextChanged">Buscar Existente <i class="fa fa-search"></i></asp:LinkButton>
                        </span>
                    </div>
                </div>
            </div>
            <div class="row" runat="server" id="FILTRO">
                <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                    <h4><strong><i class="fa fa-child" aria-hidden="true"></i>&nbsp;Empleado de GAMA con quien tendra visita</strong></h4>
                    <asp:DropDownList ID="ddlPuestoAsigna" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPuestoAsigna_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <h4>Escriba un Filtro</h4>
                    <div class="input-group">
                        <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Empleado"></asp:TextBox>
                        <span class="input-group-addon" style="color: #fff; background-color: #1ABC9C;">
                            <asp:LinkButton ID="lnkbuscarpuestos" runat="server" Style="color: #fff; background-color: #1ABC9C;" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                        </span>
                    </div>
                </div>
                <div class="col-lg-12">
                    <asp:TextBox onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" placeholder="Motivo" TextMode="MultiLine" Rows="3" ID="txtmotivo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Visita" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:Button ID="btnCancelar" runat="server" Text="Limpiar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                </div>
            </div>

            <!--MODALES-->
            <div class="modal fade modal-info" id="myModalPersonas" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <h4 style="text-align: center;">
                                <label id="content_modals"></label>
                            </h4>
                            <div class="row" style="overflow-y: scroll; height: 150px;">
                                <asp:Repeater ID="repeatpersona" runat="server">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="upa" runat="server" UpdateMode="Always">
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkpersona"/>
                                            </Triggers>
                                            <ContentTemplate>
                                                <div class="col-lg-12 col-sm-12">
                                                    <asp:LinkButton ID="lnkpersona" OnClick="lnkpersona_Click" CssClass="btn btn-primary btn-block" runat="server" CommandName='<%#Eval("idc_visitap") %>' CommandArgument='<%#Eval("nombre") %>'>
                                                <%#Eval("nombre") %>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <br />
                            <asp:LinkButton ID="lnkpersonas" OnClientClick="ModalClose();" OnClick="lnkpersonas_Click" CssClass="btn btn-success btn-block" runat="server">
                                    Agregar Como Nuevo
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade modal-info" id="myModalempresa" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <h4 style="text-align: center;">
                                <label id="content_modalempresa"></label>
                            </h4>
                            <div class="row" style="overflow-y: scroll; height: 150px;">
                                <asp:Repeater ID="repeatempresa" runat="server">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="upaededededed" runat="server">
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkemp" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div class="col-lg-12 col-sm-12">
                                                    <asp:LinkButton ID="lnkemp" OnClick="lnkemp_Click" CssClass="btn btn-primary btn-block" runat="server" CommandName='<%#Eval("idc_visitaemp") %>' CommandArgument='<%#Eval("nombre") %>'>
                                                <%#Eval("nombre") %>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <br />
                            <asp:LinkButton ID="lnkempresa" OnClick="lnkempresa_Click" CssClass="btn btn-success btn-block" runat="server">
                                    Agregar Como Nueva
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            
    <div class="row">
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-indent" aria-hidden="true"></i>&nbsp;Visitas Actuales en curso
                        <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label></strong><span>

                            <asp:LinkButton Visible="true" ID="LNKUPDATE" CssClass="btn btn-info" OnClick="LNKUPDATE_Click" runat="server">Actualizar Registros <i class="fa fa-refresh" aria-hidden="true"></i></asp:LinkButton>
                            <asp:LinkButton Visible="true" ID="lnkurladicinal" CssClass="btn btn-success" OnClick="lnkurladicinal_Click" runat="server">Ver Visitas de Hoy <i class="fa fa-share" aria-hidden="true"></i></asp:LinkButton>
                        </span></h4>
            <div class="table-responsive">
                <asp:GridView ID="gridvisitas" CssClass="table table-responsive table-condensed" DataKeyNames="idc_visitareg" OnRowCommand="gridvisitas_RowCommand" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Visita" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Button ID="Button1" CssClass="btn btn-danger btn-block" runat="server" Text="Terminar" CommandName="Terminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="idc_visitareg" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="visitante" HeaderStyle-Width="110px" HeaderText="Visitante"></asp:BoundField>
                        <asp:BoundField DataField="empresa" HeaderStyle-Width="110px" HeaderText="Empresa"></asp:BoundField>
                        <asp:BoundField DataField="motivo" HeaderStyle-Width="200px" HeaderText="Motivo"></asp:BoundField>
                        <asp:BoundField DataField="empleado" HeaderStyle-Width="200px" HeaderText="Empleado"></asp:BoundField>
                        <asp:BoundField DataField="fecha_ingreso" HeaderStyle-Width="80px" HeaderText="Ingreso"></asp:BoundField>
                        <asp:BoundField DataField="fecha_salida" HeaderStyle-Width="80px" HeaderText="Salida"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
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
                            <asp:TextBox ID="txtpbservaciones" placeholder="Observaciones" Visible="false" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" type="button" onclick="ModalClose();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>