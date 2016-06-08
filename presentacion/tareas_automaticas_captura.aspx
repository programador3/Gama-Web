<%@ Page Title="Captura" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_automaticas_captura.aspx.cs" Inherits="presentacion.tareas_automaticas_captura" %>

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
            $('#modalPreviewView').modal('hide');
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
        function ValidateRange(Object, val_min, val_max, error_mess) {
            // It's a number

            if (Object.value != "") {
                numValue = parseFloat(Object.value);
                min = parseFloat(val_min);
                max = parseFloat(val_max);
                if (numValue < min || numValue > max) {
                    Object.value = "1";
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
    </script>
    <style type="text/css">
        .txtred {
            border: 2px solid #456879;
            border-radius: 5px;
            height: 22px;
            width: 230px;
        }

        select.custom-dropdown {
            -webkit-appearance: none; /*REMOVES DEFAULT CHROME & SAFARI STYLE*/
            -moz-appearance: none; /*REMOVES DEFAULT FIREFOX STYLE*/
            border: 0 !important; /*REMOVES BORDER*/
            color: #000;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            font-size: 14px;
            padding: 10px;
            width: 35%;
            cursor: pointer;
            background: url(drop-down-arrow.png) no-repeat right center;
            background-size: 40px 37px; /*TO ACCOUNT FOR @2X IMAGE FOR RETINA */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <h1 class="page-header">Captura Nueva Tarea Automatica</h1>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-7 col-md-7 col-sm-6 col-xs-12">
                            <label>Selecciona el puesto que revisara la tarea (Visto Bueno)</label>
                            <asp:DropDownList ID="ddlPuestoAsigna" OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label>Escriba un Filtro</label>
                            <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <label></label>
                            <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-7 col-md-7 col-sm-6 col-xs-12">
                            <label>Selecciona el puesto que realizara la tarea</label>
                            <asp:DropDownList ID="ddlpuestorealiza" OnSelectedIndexChanged="ddlpuestorealiza_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label>Escriba un Filtro</label>
                            <asp:TextBox ID="txtfiltro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="LinkButton1_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <label></label>
                            <asp:LinkButton ID="lnkpuesto" runat="server" CssClass="btn btn-success btn-block" OnClick="LinkButton1_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <h4><i class="fa fa-list-alt"></i>&nbsp;Descripcion de la tarea</h4>
                            <asp:TextBox ID="txtdescripcion" onfocus="$(this).select();" placeholder="Descripcion" CssClass="form-control" TextMode="MultiLine" Rows="4" onblur="return imposeMaxLength(this, 1000);" runat="server" Style="resize: none; text-transform: uppercase;"></asp:TextBox>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <h4><i class="fa fa-calendar-plus-o" aria-hidden="true"></i>&nbsp;Fecha de inicio <small>A partir de cuando comenzara la tarea automatica</small></h4>
                            <asp:TextBox ID="txtfecha_inicio" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <h4><i class="fa fa-calendar-times-o" aria-hidden="true"></i>&nbsp;Fecha de fin <small>Fecha que dejarar de ejecutarse la tarea (en blanco sin fecha)</small></h4>
                            <asp:TextBox ID="txtfecha_fin" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                            <h4><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;Tiempo de duracion</h4>
                            <h4>Tiempo aproximado de duración en horas (este tiempo ayuda a calcular la fecha compromiso)
                        <asp:TextBox ID="txthoras_terminar" onblur="ValidateRange(this,1,360,'El valor de duracion debe ser de 1 a 360 horas (1-15 dias)');" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" CssClass="txtred" Width="100px" TextMode="Number" MaxLength="1" runat="server" AutoPostBack="true"></asp:TextBox>
                            </h4>
                        </div>
                        <div class="col-lg-4  col-md-4 col-sm-6 col-xs-12">
                            <h4><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;Tipo de Tarea Automatica</h4>
                            <asp:DropDownList ID="ddltipo" CssClass="btn btn-default btn-block" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltipo_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="Seleccione una opción" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Diaria" Value="D"></asp:ListItem>
                                <asp:ListItem Text="Semanal" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Mensual" Value="M"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:Panel ID="panel_d" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <h4><i class="fa fa-refresh" aria-hidden="true"></i>&nbsp;Frecuencia de repetición</h4>
                                <h4>La tarea se repetira cada
                        <asp:TextBox ID="txtfrecuencia_d" onblur="ValidateRange(this,1,8,'El valor de frecuencia  debe ser de 1 a 8 dias');" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" CssClass="txtred" Width="100px" TextMode="Number" MaxLength="1" runat="server" AutoPostBack="true" OnTextChanged="txtfrecuencia_d_TextChanged"></asp:TextBox>
                                    dia(s).</h4>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panel_s" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <h4><i class="fa fa-refresh" aria-hidden="true"></i>&nbsp;Frecuencia de repetición</h4>
                                <h4>La tarea se repetira cada
                        <asp:TextBox ID="txtfrecuencia_s" onblur="ValidateRange(this,1,52,'El valor de frecuencia  debe ser de 1 a 52 semanas');" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" CssClass="txtred" Width="100px" TextMode="Number" MaxLength="2" runat="server" AutoPostBack="true" OnTextChanged="txtfrecuencia_s_TextChanged"></asp:TextBox>
                                    semana(s).</h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <h4><i class="fa fa-check-square-o" aria-hidden="true"></i>&nbsp;Seleccione los dias de la semana de repetición</h4>
                            </div>
                            <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6">
                                <asp:LinkButton ID="lnklunes" runat="server" CssClass="btn btn-default btn-block" OnClick="lnklunes_Click">Lunes</asp:LinkButton>
                            </div>
                            <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6">
                                <asp:LinkButton ID="lnkmartes" runat="server" CssClass="btn btn-default btn-block" OnClick="lnklunes_Click">Martes</asp:LinkButton>
                            </div>

                            <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6">
                                <asp:LinkButton ID="lnkmiercoles" runat="server" CssClass="btn btn-default btn-block" OnClick="lnklunes_Click">Miercoles</asp:LinkButton>
                            </div>

                            <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6">
                                <asp:LinkButton ID="lnkjueves" runat="server" CssClass="btn btn-default btn-block" OnClick="lnklunes_Click">Jueves</asp:LinkButton>
                            </div>

                            <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6">
                                <asp:LinkButton ID="lnkviernes" runat="server" CssClass="btn btn-default btn-block" OnClick="lnklunes_Click">Viernes</asp:LinkButton>
                            </div>

                            <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6">
                                <asp:LinkButton ID="lnksabado" runat="server" CssClass="btn btn-default btn-block" OnClick="lnklunes_Click">Sabado</asp:LinkButton>
                            </div>

                            <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6">
                                <asp:LinkButton ID="lnkdomingo" runat="server" CssClass="btn btn-default btn-block" OnClick="lnklunes_Click">Domingo</asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panel_m" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <h4><i class="fa fa-refresh" aria-hidden="true"></i>&nbsp;Frecuencia de repetición</h4>
                                <h4>La tarea se repetira cada
                        <asp:TextBox ID="txtfrecuencia_m" onblur="ValidateRange(this,1,24,'El valor de frecuencia  debe ser de 1 a 24 meses');" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" CssClass="txtred" Width="100px" TextMode="Number" MaxLength="2" runat="server" AutoPostBack="true" OnTextChanged="txtfrecuencia_m_TextChanged"></asp:TextBox>
                                    mes(es). </h4>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <h4><i class="fa fa-refresh" aria-hidden="true"></i>&nbsp;Dia de Repeticion</h4>
                                <h4>El dia
                        <asp:TextBox ID="txtdiames" onblur="ValidateRange(this,1,24,'El valor de frecuencia  debe ser de 1 a 30 dias');" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" CssClass="txtred" Width="100px" TextMode="Number" MaxLength="2" runat="server" AutoPostBack="true" OnTextChanged="txtfrecuencia_m_TextChanged"></asp:TextBox>
                                    de cada mes. </h4>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-lg-12">
                            <h4><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;Horario de Repeticion</h4>
                            <asp:DropDownList ID="ddlhorario" CssClass="btn btn-default btn-block" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlhorario_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="Seleccione una opción" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Hora Especifica(Asigne una hora del dia)" Value="E"></asp:ListItem>
                                <asp:ListItem Text="Rango de Horarios(Asigne un rango)" Value="R"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:Panel ID="panel_hora_Esp" runat="server" Visible="false">
                        <div class="row">

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <h4><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;Hora especifica</h4>
                                <h4>La tarea se repetira a las
                         <asp:DropDownList ID="ddl_hora_esp" CssClass="btn btn-default" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_hora_esp_SelectedIndexChanged">
                             <asp:ListItem Selected="True" Text="Seleccione una hora" Value="S"></asp:ListItem>
                             <asp:ListItem Text="12 am" Value="0"></asp:ListItem>
                             <asp:ListItem Text="1 am" Value="1"></asp:ListItem>
                             <asp:ListItem Text="2 am" Value="2"></asp:ListItem>
                             <asp:ListItem Text="3 am" Value="3"></asp:ListItem>
                             <asp:ListItem Text="4 am" Value="4"></asp:ListItem>
                             <asp:ListItem Text="5 am" Value="5"></asp:ListItem>
                             <asp:ListItem Text="6 am" Value="6"></asp:ListItem>
                             <asp:ListItem Text="7 am" Value="7"></asp:ListItem>
                             <asp:ListItem Text="8 am" Value="8"></asp:ListItem>
                             <asp:ListItem Text="9 am" Value="9"></asp:ListItem>
                             <asp:ListItem Text="10 am" Value="10"></asp:ListItem>
                             <asp:ListItem Text="11 am" Value="11"></asp:ListItem>
                             <asp:ListItem Text="12 pm" Value="12"></asp:ListItem>
                             <asp:ListItem Text="1 pm" Value="13"></asp:ListItem>
                             <asp:ListItem Text="2 pm" Value="14"></asp:ListItem>
                             <asp:ListItem Text="3 pm" Value="15"></asp:ListItem>
                             <asp:ListItem Text="4 pm" Value="16"></asp:ListItem>
                             <asp:ListItem Text="5 pm" Value="17"></asp:ListItem>
                             <asp:ListItem Text="6 pm" Value="18"></asp:ListItem>
                             <asp:ListItem Text="7 pm" Value="19"></asp:ListItem>
                             <asp:ListItem Text="8 pm" Value="20"></asp:ListItem>
                             <asp:ListItem Text="9 pm" Value="21"></asp:ListItem>
                             <asp:ListItem Text="10 pm" Value="22"></asp:ListItem>
                             <asp:ListItem Text="11 pm" Value="23"></asp:ListItem>
                         </asp:DropDownList>
                                </h4>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panel_rango" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-lg-12">
                                <h4><i class="fa fa-arrows-h" aria-hidden="true"></i>&nbsp;Rango de Horario</h4>
                                <h4>La tarea se repetira de
                         <asp:DropDownList ID="ddl_rango_inicio" CssClass="btn btn-default" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_hora_esp_SelectedIndexChanged">
                             <asp:ListItem Selected="True" Text="Seleccione una hora" Value="S"></asp:ListItem>
                             <asp:ListItem Text="12 am" Value="0"></asp:ListItem>
                             <asp:ListItem Text="1 am" Value="1"></asp:ListItem>
                             <asp:ListItem Text="2 am" Value="2"></asp:ListItem>
                             <asp:ListItem Text="3 am" Value="3"></asp:ListItem>
                             <asp:ListItem Text="4 am" Value="4"></asp:ListItem>
                             <asp:ListItem Text="5 am" Value="5"></asp:ListItem>
                             <asp:ListItem Text="6 am" Value="6"></asp:ListItem>
                             <asp:ListItem Text="7 am" Value="7"></asp:ListItem>
                             <asp:ListItem Text="8 am" Value="8"></asp:ListItem>
                             <asp:ListItem Text="9 am" Value="9"></asp:ListItem>
                             <asp:ListItem Text="10 am" Value="10"></asp:ListItem>
                             <asp:ListItem Text="11 am" Value="11"></asp:ListItem>
                             <asp:ListItem Text="12 pm" Value="12"></asp:ListItem>
                             <asp:ListItem Text="1 pm" Value="13"></asp:ListItem>
                             <asp:ListItem Text="2 pm" Value="14"></asp:ListItem>
                             <asp:ListItem Text="3 pm" Value="15"></asp:ListItem>
                             <asp:ListItem Text="4 pm" Value="16"></asp:ListItem>
                             <asp:ListItem Text="5 pm" Value="17"></asp:ListItem>
                             <asp:ListItem Text="6 pm" Value="18"></asp:ListItem>
                             <asp:ListItem Text="7 pm" Value="19"></asp:ListItem>
                             <asp:ListItem Text="8 pm" Value="20"></asp:ListItem>
                             <asp:ListItem Text="9 pm" Value="21"></asp:ListItem>
                             <asp:ListItem Text="10 pm" Value="22"></asp:ListItem>
                             <asp:ListItem Text="11 pm" Value="23"></asp:ListItem>
                         </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                            a&nbsp;&nbsp;&nbsp;
                         <asp:DropDownList ID="ddl_rango_fin" CssClass="btn btn-default" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_hora_esp_SelectedIndexChanged">
                             <asp:ListItem Selected="True" Text="Seleccione una hora" Value="S"></asp:ListItem>
                             <asp:ListItem Text="12 am" Value="0"></asp:ListItem>
                             <asp:ListItem Text="1 am" Value="1"></asp:ListItem>
                             <asp:ListItem Text="2 am" Value="2"></asp:ListItem>
                             <asp:ListItem Text="3 am" Value="3"></asp:ListItem>
                             <asp:ListItem Text="4 am" Value="4"></asp:ListItem>
                             <asp:ListItem Text="5 am" Value="5"></asp:ListItem>
                             <asp:ListItem Text="6 am" Value="6"></asp:ListItem>
                             <asp:ListItem Text="7 am" Value="7"></asp:ListItem>
                             <asp:ListItem Text="8 am" Value="8"></asp:ListItem>
                             <asp:ListItem Text="9 am" Value="9"></asp:ListItem>
                             <asp:ListItem Text="10 am" Value="10"></asp:ListItem>
                             <asp:ListItem Text="11 am" Value="11"></asp:ListItem>
                             <asp:ListItem Text="12 pm" Value="12"></asp:ListItem>
                             <asp:ListItem Text="1 pm" Value="13"></asp:ListItem>
                             <asp:ListItem Text="2 pm" Value="14"></asp:ListItem>
                             <asp:ListItem Text="3 pm" Value="15"></asp:ListItem>
                             <asp:ListItem Text="4 pm" Value="16"></asp:ListItem>
                             <asp:ListItem Text="5 pm" Value="17"></asp:ListItem>
                             <asp:ListItem Text="6 pm" Value="18"></asp:ListItem>
                             <asp:ListItem Text="7 pm" Value="19"></asp:ListItem>
                             <asp:ListItem Text="8 pm" Value="20"></asp:ListItem>
                             <asp:ListItem Text="9 pm" Value="21"></asp:ListItem>
                             <asp:ListItem Text="10 pm" Value="22"></asp:ListItem>
                             <asp:ListItem Text="11 pm" Value="23"></asp:ListItem>
                         </asp:DropDownList>
                                </h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <h4>La tarea se repetira cada
                        <asp:TextBox ID="txtnumhora" onblur="ValidateRange(this,1,24,'El valor de frecuencia  debe ser de 1 a 24 horas');" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" CssClass="txtred" Width="100px" TextMode="Number" MaxLength="2" runat="server" AutoPostBack="true"></asp:TextBox>
                                    hora(s). </h4>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btnguardar" OnClick="btnguardar_Click" CssClass="btn btn-primary btn-block" runat="server" Text="Guardar" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:Button ID="btncancelar" CssClass="btn btn-danger btn-block" runat="server" Text="Cancelar" />
                </div>
            </div>
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
                                    <h4 runat="server" id="valores">
                                        <asp:Label ID="lblvar" runat="server" Text=""></asp:Label>
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
        </div>
    </div>
</asp:Content>