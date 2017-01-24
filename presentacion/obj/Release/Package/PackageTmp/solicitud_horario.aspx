<%@ Page Title="Solicitud de Horario" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="solicitud_horario.aspx.cs" Inherits="presentacion.solicitud_horario" %>

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

        $(document).ready(function () {
            Charge();

            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
        function Charge() {
            $('#<%= txthorasalidac.ClientID%>').mask("99:99", { placeholder: "HH:MM" });
            $('#<%= txthorasalida.ClientID%>').mask("99:99", { placeholder: "HH:MM" });
            $('#<%= txthoraentrada.ClientID%>').mask("99:99", { placeholder: "HH:MM" });
            $('#<%= txthoraentradac.ClientID%>').mask("99:99", { placeholder: "HH:MM" });
        }
        function ValidateRange2(Object, val_min, val_max, error_mess) {
            // It's a number

            if (Object.value != "") {
                numValue = parseFloat(Object.value);
                min = parseFloat(val_min);
                max = parseFloat(val_max);
                if (numValue < min || numValue > max) {
                    Object.value = "";
                    swal({
                        title: "Mensaje del Sistema",
                        text: error_mess,
                        type: 'error',
                        showCancelButton: false,
                        confirmButtonColor: "#428bca",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false, allowEscapeKey: false
                    });
                }
            }
        }
        //VALIDA EL COMBO DE SUCURSALES.
        function HoraEntrada() {
            if ($('#<%= txthoraentrada.ClientID%>').val() == "") {
                $('#<%= ddlsucursales.ClientID%>').prop('disabled', true);
            } else {
                $('#<%= ddlsucursales.ClientID%>').prop('disabled', false);
            }

            var hc1 = $('#<%= txthoraentradac.ClientID%>').val();
            var hc2 = $('#<%= txthorasalidac.ClientID%>').val();
            var he = $('#<%= txthoraentrada.ClientID%>').val();

            if (he != "" && hc2 != "" && hc1 != "") {
                if (hc2 < he || hc1 < he) {
                    $('#<%= txthoraentrada.ClientID%>').val('');
                    swal("Mensaje del Sistema", "Los horarios de Comida no deben ser mayores a el horario de entrada.", "info");
                }
            }
        }

        function HorasdeComida() {
            if ($('#<%= txthoraentradac.ClientID%>').val() == "" && $('#<%= txthorasalidac.ClientID%>').val() != "") {
                $('#<%= txthoraentradac.ClientID%>').focus();
                swal("Mensaje del Sistema", "Debe Cambiar ambos horarios de comida.", "info");
            }
            if ($('#<%= txthorasalidac.ClientID%>').val() == "" && $('#<%= txthoraentradac.ClientID%>').val() != "") {
                $('#<%= txthorasalidac.ClientID%>').focus();
                swal("Mensaje del Sistema", "Debe Cambiar ambos horarios de comida.", "info");
            }

            var hc1 = $('#<%= txthoraentradac.ClientID%>').val();
            var hc2 = $('#<%= txthorasalidac.ClientID%>').val();
            var he = $('#<%= txthoraentrada.ClientID%>').val();
            var hs = $('#<%= txthorasalida.ClientID%>').val();

            if (he != "" && hc2 != "" && hc1 != "") {
                if (hc2 < he || hc1 < he) {
                    $('#<%= txthorasalidac.ClientID%>').val('');
                    $('#<%= txthoraentradac.ClientID%>').val('');
                    swal("Mensaje del Sistema", "Los horarios de Comida no deben ser mayores a el horario de entrada.", "info");
                }
            }
            if (hs != "" && hc2 != "" && hc1 != "") {
                if (hc2 > hs || hc1 > hs) {
                    $('#<%= txthorasalidac.ClientID%>').val('');
                    $('#<%= txthoraentradac.ClientID%>').val('');
                    swal("Mensaje del Sistema", "Los horarios de Comida no deben ser menores a el horario de salida.", "info");
                }
            }
            if (hc2 != "" && hc1 != "") {
                if (hc2 > hc1) {
                    $('#<%= txthorasalidac.ClientID%>').val('');
                    $('#<%= txthoraentradac.ClientID%>').val('');
                    swal("Mensaje del Sistema", "Los horarios de Comida no tienen orden Logico.", "info");
                }
            }
        }
        function ValidarHoraLaboral(Object) {
            var string = Object.value;
            var hourstr = '';
            var minutesstr = '';
            for (var v of string) {
                if (v != ':') {
                    hourstr = hourstr + v;
                } else {
                    minutesstr = string.replace(hourstr + ":", "");
                    break;
                }
            }
            var hour = parseInt(hourstr);
            var minutes = parseInt(minutesstr) + (hour * 60);
            if (minutes > 1260 || minutes < 360) {
                Object.focus();
                swal("Mensaje del Sistema", "Los Horarios deben ser entre 6 de la mañana (6:00) y 9 de la noche(21:00) .", "info");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Solicitud de Cambio de Horario</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkedit" />
            <asp:PostBackTrigger ControlID="btnguardaredicio" />
            <asp:PostBackTrigger ControlID="btncancelaredicion" />
            <asp:PostBackTrigger ControlID="btnguardar" />
            <asp:PostBackTrigger ControlID="btnrechaza" />
            <asp:PostBackTrigger ControlID="btnautoriza" />
            <asp:PostBackTrigger ControlID="btncancelar" />
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
                        <h4 style="color:orangered;"><strong>
                            <asp:Label ID="lblobsr" Visible="false" runat="server" Text=""></asp:Label></strong>
                        </h4>
                    </div>
                    <div class="row" id="edicion" runat="server" visible="false">
                        <div class="col-lg-6 col-sm-6 col-xs-6">
                            <asp:Button ID="btnguardaredicio" CssClass="btn btn-primary btn-block" OnClick="btnguardaredicio_Click" runat="server" Text="Guardar Edición" />
                        </div>
                        <div class="col-lg-6 col-sm-6 col-xs-6">
                            <asp:Button ID="btncancelaredicion" CssClass="btn btn-danger btn-block" OnClick="btncancelaredicion_Click" runat="server" Text="Cancelar Edición" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12 col-xs-12">
                    <div class="panel panel-info fresh-color">
                        <div class="panel-heading">
                            <h4 style="text-align: center;"><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Detalles de Solicitud <span>
                                <asp:LinkButton ID="lnkedit" Visible="false" runat="server" CssClass="btn btn-default" OnClick="lnkedit_Click">Editar <i class="fa fa-pencil fa-fw"></i></asp:LinkButton></span> </h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-8 col-sm-10 col-xs-12">
                                    <h5><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;<strong>Fecha que aplicara</strong>
                                        <span>
                                            <asp:Button ID="btncancelarsol" Visible="false" CssClass="btn btn-danger" OnClick="btnrechaza2_Click" runat="server" Text="Cancelar Solicitud" /></span>
                                    </h5>
                                    <asp:TextBox ID="txtfecha" TextMode="Date" CssClass="form-control timepicker" AutoPostBack="true" OnTextChanged="txtfecha_TextChanged" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12 col-xs-12">
                                    <asp:Button ID="btntot" runat="server" Text="No Tendra Ninguna Checada en el Dia" CssClass="btn btn-default btn-block" OnClick="btntot_Click" />
                                </div>
                            </div>
                            <div id="cuerpo" runat="server">

                                <div class="row">
                                    <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                        <div class="form-inline">
                                            <h5><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;<strong>Hora de Entrada</strong> <small>Formato 24 Horas (0-24)</small>
                                                <asp:TextBox ID="txthoraentrada" OnTextChanged="txthoraentrada_TextChanged" AutoPostBack="true" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" CssClass="form-control" runat="server"></asp:TextBox>
                                            </h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <div class="form-inline">
                                            <h5><strong><i class="fa fa-university" aria-hidden="true"></i>&nbsp;Sucursal</strong> <small>Solo Hora de Entrada</small>
                                                <asp:DropDownList ID="ddlsucursales" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsucursales_SelectedIndexChanged" Enabled="false" runat="server"></asp:DropDownList></h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="form-inline">
                                            <h5><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;<strong>Hora de Salida a Comer</strong> <small>Formato 24 Horas (0-24)</small>
                                                <asp:TextBox onchange="HorasdeComida();" onblur="ValidarHoraLaboral(this);" ID="txthorasalidac" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" CssClass="form-control" runat="server"></asp:TextBox>
                                            </h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <asp:LinkButton ID="lnkno_horacomida" CssClass="btn btn-default btn-block" OnClick="lnkno_horacomida_Click" runat="server">No Marcar Incidencia en Comida</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="form-inline">
                                            <h5><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;<strong> Hora Entrada de Comer</strong> <small>Formato 24 Horas (0-24)</small>
                                                <asp:TextBox onchange="HorasdeComida();" onblur="ValidarHoraLaboral(this);" ID="txthoraentradac" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" CssClass="form-control" runat="server"></asp:TextBox>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="form-inline">
                                            <h5><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;<strong>Hora de Salida </strong><small>&nbsp;Formato 24 Horas (0-24)</small>
                                                &nbsp;
                                            <asp:TextBox ID="txthorasalida" onblur="ValidarHoraLaboral(this);" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" AutoPostBack="true" OnTextChanged="txthorasalida_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                            </h5>
                                        </div>

                                        <div class="alert fresh-color alert-danger alert-dismissible" role="alert" runat="server" id="divchecacomida" visible="false">
                                            <strong>NOTA IMPORTANTE!</strong> Verifica si la hora de comida va a cambiar o no checara comida.
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <asp:LinkButton ID="lnkno_Salida" CssClass="btn btn-default btn-block" OnClick="lnkno_Salida_Click" runat="server">No Marcar Incidencia en Salida</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12 col-xs-12">

                                    <h5><i class="fa fa-comment" aria-hidden="true"></i>&nbsp;<strong>Observaciones</strong></h5>
                                    <asp:TextBox ID="txtobservaciones" TextMode="MultiLine" Rows="3" onfocus="$(this).select();" CssClass="form-control" runat="server" placeholder="Observaciones"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="solicita" runat="server" visible="false">
                <div class="col-lg-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btnguardar" CssClass="btn btn-info btn-block" OnClick="btnguardar_Click" runat="server" Text="Guardar" />
                </div>
                <div class="col-lg-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btncancelar" CssClass="btn btn-danger btn-block" OnClick="btncancelar_Click" runat="server" OnClientClick="return confirm('¿DESEA REGRESAR?')" Text="Regresar" />
                </div>
            </div>
            <div class="row" id="autoriza" runat="server" visible="false">
                <div class="col-lg-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btnautoriza" CssClass="btn btn-success btn-block" OnClick="btnautoriza_Click" runat="server" Text="Autorizar" />
                </div>
                <div class="col-lg-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btnrechaza" CssClass="btn btn-danger btn-block" OnClick="btnrechaza_Click" runat="server" Text="Rechazar" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal -->
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
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>