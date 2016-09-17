<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="retiro_ahorro.aspx.cs" Inherits="presentacion.retiro_ahorro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="js/wickedpicker.min.css" rel="stylesheet" />
    <script src="js/wickedpicker.min.js"></script>
    <script src="js/mascara.js"></script>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Solicitud de Retiro de Ahorro</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2" style="align-content: center;">
                    <a>
                        <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 160px; margin: 0 auto;" />
                    </a>
                </div>
                <div class="col-lg-10" style="text-align: left">
                    <div class="form-group">
                        <h4>
                            <strong>Nombre Empleado: </strong>
                            <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                        </h4>
                        <h4><strong>Puesto: </strong>
                            <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                        </h4>
                        <h4><strong>Departamento: </strong>
                            <asp:Label ID="lbldepto" runat="server" Text=""></asp:Label>
                        </h4>
                        <h4><strong>
                            <asp:Label ID="lblobsr" Visible="false" runat="server" Text=""></asp:Label></strong>
                        </h4>
                    </div>
                </div>
            </div>
            <div id="solicitud" runat="server" visible="true">
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <h5><strong>Saldo de Ahorro</strong></h5>
                        <asp:TextBox ID="txtsaldo" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                        <h5><strong># Retiros</strong></h5>
                        <asp:TextBox ID="txtnumretiroa" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                        <div class="alert fresh-color alert-danger alert-dismissible" role="alert" runat="server" id="numret" visible="false">
                            <strong>NOTA IMPORTANTE!</strong> Ya ha realizado 2 retiros. Si se realiza el retiro quedara fuera del ahorro
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <h5><strong>Retiro Maximo Sugerido</strong></h5>
                        <asp:TextBox ID="txtsugerido" Style="color: white; background-color: #04B404" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Monto del Retiro</strong></h5>
                        <asp:TextBox ID="txtretiro" onkeypress="return validarEnteros(event);" AutoPostBack="true" OnTextChanged="txtretiro_TextChanged" ReadOnly="false" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                        <div class="alert fresh-color alert-danger alert-dismissible" role="alert" runat="server" id="retirod" visible="false">
                            <strong>NOTA IMPORTANTE!</strong> Con este importe quedara fuera del ahorro.
                        </div>
                    </div>
                </div>
            </div>
            <div id="tiene" runat="server" visible="false">
                <div class="alert fresh-color alert-danger alert-dismissible" role="alert" runat="server" id="mensaje" visible="true">
                    <strong>NOTA IMPORTANTE!</strong> Este empleado ya tiene una solicitud pendiente
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Monto del Retiro</strong></h5>
                        <asp:TextBox ID="txtmonto_existe" ReadOnly="true" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha de Solicitud</strong></h5>
                        <asp:TextBox ID="txtfechasolicitud" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <h5><strong><i class="fa fa-use" aria-hidden="true"></i>&nbsp;Usuario que Solicito</strong></h5>
                        <asp:TextBox ID="txtusuario" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row" id="solicita" runat="server" visible="true">
        <div class="col-lg-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnguardar" CssClass="btn btn-info btn-block" OnClick="btnguardar_Click" runat="server" Text="Guardar" />
        </div>
        <div class="col-lg-6 col-sm-6 col-xs-6">
            <asp:Button ID="btncancelar" CssClass="btn btn-danger btn-block" OnClick="btncancelar_Click" runat="server" Text="Cancelar" />
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
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>