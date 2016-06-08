<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="aprobaciones_firma.aspx.cs" Inherits="presentacion.autorizaciones_firma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Return(cTitulo, aprobado, error) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#mensaje_error').text(error);
            if (aprobado == "1") {
                $('#header_color').css('background-color', '#449D44');
                $('#header_color').css('color', 'white');
            } else {
                $('#header_color').css('background-color', '#D9534F');
                $('#header_color').css('color', 'white');
            }

            //$('#confirmContenido').text(cContenido);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: "success",
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
        //
        $(function () {
            //codigo aquí
            //valor del campo oculto
            var aprobar = $('#<%=modal_ocaprobado.ClientID%>').val();
            if (aprobar == "True") {
                //no valides observaciones
                $('#<%= Yes.ClientID%>').prop('disabled', false);
            } else {
                //no valides observaciones
                $('#<%= Yes.ClientID%>').prop('disabled', true);
            }
        })
        function campoVacio() {
            var aprobar = $('#<%=modal_ocaprobado.ClientID%>').val();
            if (aprobar == "False") {
                var obs = $('#<%= txtobs.ClientID%>').val().replace(/\s+/g, '');
                if (obs.length < 1) {
                    //inhabilita el boton aceptar
                    $('#<%= Yes.ClientID%>').prop('disabled', true);

                } else {
                    $('#<%= Yes.ClientID%>').prop('disabled', false);
                }
            }

        }
        function ModalClose() {
            $('#myModalc').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido) {
            $('#myModalc').modal('show');
            $('#confirmTituloc').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <div class="page-header">
                <h1>
                    <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                    Aprobaciones firmas</h1>
            </div>
            <!-- Page Heading -->
            <asp:Panel ID="panel" runat="server" class="row">

                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">Firma</div>
                        <div class="panel-body">
                            <!--campo oculto IDC_APROBACION_SOLI -->
                            <asp:HiddenField ID="oc_idc_aprobacion_soli" runat="server" Value="0" />
                            <asp:Repeater ID="repit_usuarios" runat="server" OnItemDataBound="repit_usuarios_ItemDataBound" OnItemCommand="repit_usuarios_ItemCommand">
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <!-- campo oculto idc_usuario -->
                                                <asp:HiddenField ID="oc_idc_usuario" runat="server" />
                                                <asp:HiddenField ID="oc_idc_aprobacion_reg" runat="server" />
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtusuario" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <asp:Image ID="imguser" runat="server" ImageUrl="~/imagenes/btn/icon_user.png" />
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <asp:Panel ID="panelbtn" runat="server">
                                                    <div class="btn-group">
                                                        <asp:Button ID="btnautorizar" runat="server" CssClass="btn btn-success" Text="Aprobar" CommandName="aprobar" />
                                                        <asp:Button ID="btnautdenegada" runat="server" CssClass="btn btn-danger" Text="No Aprobar" CommandName="noaprobar" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <asp:Panel ID="panelmensaje" runat="server">
                                                    <asp:Label ID="lblmensaje" runat="server" Text=""></asp:Label>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Panel ID="PanelCancelar" runat="server">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtcoments" placeholder="Comentarios" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h3><i class="fa fa-times-circle"></i>&nbsp; Cancelar Aprobación</h3>
                                            <asp:Button ID="btnCancelarAprob" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelarAprob_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!-- /.row -->
            <!-- campos ocultos -->
            <div class="row">
                <div class="col-lg-12">
                    <asp:HiddenField ID="oc_paginaprevia" runat="server" />
                </div>
            </div>
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content" style="text-align: center">
                        <div id="header_color" class="modal-header">
                            <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
                            <br />
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                    <!--vacio -->
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                    <label>Password</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtpass" runat="server" CssClass="form-control" type="password"></asp:TextBox>
                                        <span class="input-group-addon">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/btn/icon_llave.png" />
                                        </span>
                                    </div>
                                    <h4>
                                        <label id="mensaje_error" class="label label-danger"></label>
                                    </h4>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                    <!--vacio -->
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <label>Observaciones</label>
                                    <asp:TextBox ID="txtobs" runat="server" CssClass="form-control" TextMode="MultiLine" onblur="campoVacio();" Style="resize: none;" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="btnFirmar_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Button ID="No" class="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="No_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- campos ocultos -->
                        <asp:HiddenField ID="modal_ocusuario" runat="server" />
                        <asp:HiddenField ID="modal_ocaprobado" runat="server" />
                        <asp:HiddenField ID="modal_ocidc_aprobacion_reg" runat="server" />
                    </div>
                </div>
            </div>

            <div id="myModalc" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong id="confirmTituloc" class="modal-title"></strong></h4>
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
                                    <asp:Button ID="btnYesCo" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="btnYesCo_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="Noc" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </div>
    <!-- /#page-wrapper -->
</asp:Content>