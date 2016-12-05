<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reproductor_llamadas.aspx.cs" Inherits="presentacion.reproductor_llamadasaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gama</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" href="imagenes/favicon.png" />
    <link href='http://fonts.googleapis.com/css?family=Roboto+Condensed:300,400' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="css/animate.min.css" />
    <link rel="stylesheet" type="text/css" href="css/bootstrap-switch.min.css" />
    <link rel="stylesheet" type="text/css" href="css/checkbox3.min.css" />
    <link href="css/PanelsLTE.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/ionicons.css" rel="stylesheet" />
    <link href="css/ionicons.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/themes/flat-blue.css" />
    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/pnotify.custom.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="js/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script src="js/jquery10.js"></script>
    <script src="js/pnotify.custom.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="js/app.js"></script>
     <script>
        function pista(R_Audio, Audio) {

            $("#download_Rep").attr("href", R_Audio);
            $("#download_Rep").attr("download", Audio);
            $("source").attr('src', R_Audio);
            $("#Reproductor").attr('src', R_Audio);

        }

        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_Modal').text(cContenido);


        }

        function progreso(tim) {
            var $pb = $('.progress .progress-bar');
            $pb.attr('data-transitiongoal', tim).progressbar({ display_text: 'center' });
        }
    </script>
    <style>
        #Reproductor {
            -webkit-box-shadow: 1px 1px 8px rgba(0, 0, 0, 0.3);
            -moz-box-shadow: 1px 1px 8px rgba(0, 0, 0, 0.3);
            -o-box-shadow: 1px 1px 8px rgba(0, 0, 0, 0.3);
            border: none;
        }

        .tcenter {
            padding: 10px;
            text-align: center;
        }

        a:link {
            text-decoration: none;
        }

        .green, .green a {
            color: #339900;
        }
    </style>
</head>
<body  class="flat-blue">
    <form id="form1" runat="server">
        <div class="app-container">
            <div class=" row content-container">
                <div class=" row">
                    <div class=" col-lg-12" style="padding: 20px;">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <h2 class="page-header"><i class="fa fa-phone" aria-hidden="true">&nbsp;</i>Reproductor de Llamadas</h2>
                        <!-- tabla temporal-->
                        <div class="row" id="div_reproductor" runat="server">
                            <h4 style="text-align: left;">&nbsp;&nbsp;          
            <asp:Label ID="lblPista" runat="server"></asp:Label>
                            </h4>
                            <div class="col-lg-5 col-md-5 col-sm-8 col-xs-12 ">
                                <audio controls="controls" id="Reproductor" preload="auto" class="form-control">
                                    <source type="audio/mpeg" />
                                    <source type="audio/ogg" />
                                    <source type="audio/wav" />
                                    <code>audio</code> element.
                                </audio>

                            </div>

                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 ">
                                <a class="btn btn-success btn-block" id="download_Rep">
                                    <i class="fa fa-download" aria-hidden="true"></i>
                                    &nbsp;Descargar
                                </a>
                            </div>
                        </div>




                        <div class="row" id="div_Observaciones" runat="server">
                            <div class="col-lg-5 col-md-5 col-sm-8 col-xs-12 ">

                                <h4 style="text-align: left;">
                                    <strong>Observaciones</strong>
                                </h4>
                                <asp:TextBox Style="text-transform: uppercase; resize: none;" onfocus="$(this).select();"
                                    ID="txtObservaciones" runat="server" TextMode="Multiline" Rows="3" CssClass="form-control"
                                    AutoPostBack="false" placeholder="Observaciones"></asp:TextBox>
                            </div>

                            <div id="div_tipo" runat="server">
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                    <h4 style="text-align: left;">
                                        <strong>Calidad de Lamada</strong>
                                    </h4>
                                    <asp:UpdatePanel ID="u" runat="server" UpdateMode="Always">
                                        <Triggers>

                                            <asp:AsyncPostBackTrigger ControlID="chkBueno" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkMalo" EventName="CheckedChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:CheckBox ID="chkBueno" CssClass="radio  radio3" runat="server" Text="&nbsp;Bueno" OnCheckedChanged="chkBueno_Click" AutoPostBack="true" />

                                            <asp:CheckBox ID="chkMalo" CssClass="radio  radio3" runat="server" Text="&nbsp;Malo" OnCheckedChanged="chkMalo_Click" AutoPostBack="true" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div id="div_marcar" runat="server">
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                    <h4 style="text-align: left;">
                                        <strong>&nbsp;
                                        </strong>
                                    </h4>
                                    <asp:Button ID="btnMarcar" CssClass="btn btn-info btn-block" runat="server" Text="Marcar" OnClick="Marcar_Click" />


                                </div>
                            </div>
                        </div>

                        <!-- modal -->
                        <div class="modal fade modal-info" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content" style="text-align: center;">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h4 id="Modal_title"><strong>Sistema de edicion</strong></h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                                <div>
                                                    <h4>
                                                        <label id="content_Modal"></label>
                                                    </h4>
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <div class="col-lg-6 col-xs-6">
                                            <%--<asp:Button ID="btnGuardar" class="btn btn-info btn-block" runat="server" Text="Guardar" OnClick="Guardar_Click" />--%>
                                            <asp:Button ID="btnGuardar" class="btn btn-info btn-block" runat="server" Text="Guardar" OnClick="Guardar_Click" />
                                        </div>
                                        <div class="col-lg-6 col-xs-6">
                                            <input id="btnCancelar" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>




                        <input type="hidden" runat="server" id="h_idc_llamada" value="" />
                        <input type="hidden" runat="server" id="h_idc_Usuario" value="" />
                        <input type="hidden" runat="server" id="h_idc_llamadamarcar" value="" />
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
