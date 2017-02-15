<%@ Page Title="Tickets" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="ticket_serv.aspx.cs" Inherits="presentacion.ticket_serv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CerrarModal() {
            $('#myModal').modal('hide');
            return true;
        }


        function ModalConfirm(cTitulo, cContenido) {
            var tipo = "";
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
            var texto = $('#lblDescripcion').val();
            $('#Contenido_txtDescripcion').attr('placeholder', tipo);
        }
        function ShowGiftMessage() {

            swal({
                title: '" + Title + "',
                text: '" + Mensaje + "',
                allowEscapeKey: false, imageUrl: '" + IMG + "',
                timer: " + Timer + ",
                showConfirmButton: false
            },
                function () {
                    swal({
                        title: 'Mensaje del Sistema',
                        allowEscapeKey: false,
                        text: '" + MensajeOK + "',
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#428bca',
                        confirmButtonText: 'Aceptar',
                        closeOnConfirm: false
                    },
                        function () {
                            location.href = '" + URL + "';
                        });
                });

        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'warning',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            },
               function () {
                   location.href = URL;
               });
        }
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
    <style>
        .div_espera {
            margin-top: 5px;
            margin-bottom: 5px;
            border-radius: 1px;
            border-width: 1px;
            font-family: 'Roboto Condensed', sans-serif;
            background-color: #F0F0F0;
            border-color: #EAEAEA;
            padding: 10px;
            text-align: center;
        }

        .div_aten {
            margin-top: 5px;
            margin-bottom: 5px;
            border-radius: 1px;
            border-width: 1px;
            font-family: 'Roboto Condensed', sans-serif;
            background-color: #353d47;
            color: #FFF;
            border-color: #353d47;
            padding: 10px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
      <%--  <Triggers>
            <asp:AsyncPostBackTrigger ControlID="repeat_en_espera" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="repeat_atendidos" EventName="ItemCommand" />
        </Triggers>--%>
        <ContentTemplate>
            <h2 class="page-header"><i class="fa fa-bookmark-o" aria-hidden="true"></i>&nbsp; Ticket de Servicios
                <span>
                    <asp:LinkButton ID="lnksolomios" CssClass="btn btn-info" Text="Ver Solo Tickets Mios" runat="server" OnClick="lnksolomios_Click"></asp:LinkButton>
                </span>
            </h2>
            <div class="row">
                <div class="col-lg-12">
                    <h4><i class="fa fa-list"></i>&nbsp;Tickets de Servicio En Espera. 
                        <span>
                            <asp:TextBox ID="txtbuscarespera" CssClass=" form-control2" placeholder="Buscar en Espera" Width="200px" runat="server" autocomplete="off"></asp:TextBox>
                            <asp:LinkButton ID="lnkbuscarespera" runat="server" CssClass="btn btn-info" OnClick="lnkbuscarespera_Click">
                                <i class="fa fa-search" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </span>
                    </h4>
                    <h4 id="NO_Hay_E" runat="server" visible="false" style="text-align: center;">No Hay Registros <i class="fa fa-exclamation-triangle"></i></h4>
                    <asp:Repeater ID="repeat_en_espera" runat="server" OnItemCommand="repeat_en_espera_ItemCommand">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnktickete" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="LinkButton3" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="LinkButton2" EventName="Click" />
                                    <asp:PostBackTrigger ControlID="LNKARCHIVO2" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12"  data-toggle="tooltip" data-placement="top" title='<%# Eval("observaciones").ToString() %>'>
                                        <div class=" div_espera">

                                            <h6><strong><%# Eval("des_corta").ToString() %></strong></h6>
                                            <h6><i class="fa fa-user-circle" aria-hidden="true"></i>&nbsp;<strong><%# Eval("empleado").ToString() %></strong></h6>
                                            <h6><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;<%# Eval("fecha").ToString() %></h6>
                                            <h6><%# Eval("obs_corta").ToString() %></h6>
                                            <asp:LinkButton Visible='<%# Convert.ToBoolean(Eval("aplica")) && Convert.ToInt32(Session["sidc_puesto_login"]) != Convert.ToInt32(Eval("idc_puesto"))  %>' 
                                                ID="LinkButton2" CommandName="Tomar" CommandArgument='<%# Eval("idc_ticketserv")%>' runat="server"
                                                CssClass="btn btn-primary" ToolTip='<%# Eval("observaciones").ToString() %>'>
                                            Tomar&nbsp;<i class="fa fa-check-circle" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton3" Visible='<%# Convert.ToBoolean(Eval("aplica")) %>' CommandName="Cancelar" CommandArgument='<%# Eval("idc_ticketserv")%>' runat="server"
                                                CssClass="btn btn-danger" ToolTip='<%# Eval("observaciones").ToString() %>'>
                                            Cancelar&nbsp;<i class="fa fa-times-circle" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnktickete" CommandName="Ver" CommandArgument='<%# Eval("idc_ticketserv")%>' runat="server"
                                                CssClass="btn btn-info" ToolTip='<%# Eval("observaciones").ToString() %>'>
                                            Info&nbsp;<i class="fa fa-info-circle" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton Visible='<%# Eval("archivo").ToString() != "" %>' ID="LNKARCHIVO2" CommandName="Descargar" CommandArgument='<%# Eval("idc_ticketserv")%>' runat="server"
                                                CssClass="btn btn-success" ToolTip='<%# Eval("observaciones").ToString() %>'>                                  
                                                 Archivo&nbsp;<i class="fa fa-download" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h4><i class="fa fa-list"></i>&nbsp;Tickets de Servicio Atendidos.                         
                        <span>
                            <asp:TextBox ID="txtbuscaraten" placeholder="Buscar en Atendidos" Width="200px" CssClass=" form-control2" runat="server" autocomplete="off"></asp:TextBox>
                            <asp:TextBox ID="TextBox1" placeholder="Buscar en AtendidosJ" Width="200px" CssClass=" form-control2" runat="server" Style="display: none;" autocomplete="off"></asp:TextBox>
                            <asp:LinkButton ID="lnkbuscaraten" runat="server" CssClass="btn btn-primary" OnClick="lnkbuscaraten_Click">
                                <i class="fa fa-search" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </span>
                    </h4>
                    <h4 id="NO_Hay_A" runat="server" visible="false" style="text-align: center;">No Hay Registros <i class="fa fa-exclamation-triangle"></i></h4>
                    <asp:Repeater ID="repeat_atendidos" runat="server" OnItemCommand="repeat_atendidos_ItemCommand">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnkticketat" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="LinkButton32" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="LinkButton22" EventName="Click" />
                                    <asp:PostBackTrigger ControlID="LNKARCHIVO" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12"  data-toggle="tooltip" data-placement="top" title='<%# Eval("observaciones").ToString() %>'>
                                        <div class=" div_aten">
                                            <h6><i class="fa fa-user-circle" aria-hidden="true"></i>&nbsp;<%# Eval("empleado_atiende").ToString() %></h6>
                                            <h6><%# Eval("des_corta").ToString() %></h6>
                                            <h6><i class="fa fa-user-circle" aria-hidden="true"></i>&nbsp;<%# Eval("empleado").ToString() %></h6>
                                            <h6><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;<%# Eval("fecha").ToString() %></h6>
                                            <h6><asp:Label ID="lbldes" runat="server" Text='<%# Eval("obs_corta").ToString() %>' ToolTip='<%# Eval("observaciones").ToString() %>'></asp:Label></h6>
                                            <asp:LinkButton Visible='<%# Convert.ToBoolean(Eval("aplica")) %>' ID="LinkButton22" CommandName="Terminar" CommandArgument='<%# Eval("idc_ticketserv")%>' runat="server"
                                                CssClass="btn btn-default" ToolTip='<%# Eval("observaciones").ToString() %>'>
                                                    Terminar&nbsp;<i class="fa fa-check-circle" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton Visible='<%# Convert.ToBoolean(Eval("aplica")) %>' ID="LinkButton32" CommandName="Aten_Cancelar" CommandArgument='<%# Eval("idc_ticketserv")%>' runat="server"
                                                CssClass="btn btn-danger" ToolTip='<%# Eval("observaciones").ToString() %>'>
                                                 Cancelar&nbsp;<i class="fa fa-times-circle" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkticketat" CommandName="Ver" CommandArgument='<%# Eval("idc_ticketserv")%>' runat="server"
                                                CssClass="btn btn-info" ToolTip='<%# Eval("observaciones").ToString() %>'>                                  
                                                 Info&nbsp;<i class="fa fa-info-circle" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton Visible='<%# Eval("archivo").ToString() != ""%>' ID="LNKARCHIVO" CommandName="Descargar" CommandArgument='<%# Eval("idc_ticketserv")%>' runat="server"
                                                CssClass="btn btn-success" ToolTip='<%# Eval("observaciones").ToString() %>'>                                  
                                                 Archivo&nbsp;<i class="fa fa-download" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->

                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" runat="server" id="confir">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="confirmContenido"></label>
                                    </h4>
                                </div>
                            </div>
                            <div class="row" id="div_detalles" runat="server" style="text-align: left;" visible="true">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                        <b>Descripcion:</b>
                                        <asp:Label runat="server" ID="lblDescr"> texto</asp:Label>
                                        <br />
                                        <b>Observacion:</b>
                                        <asp:Label runat="server" ID="lblObser"> texto</asp:Label><br />
                                        <b>Empleado Solicito<asp:Label runat="server" ID="lblAten">&nbsp;(Atendiendo)</asp:Label>:</b>
                                        <asp:Label runat="server" ID="lblEmple"> texto</asp:Label><br />
                                        <b>Empleado Atiende<asp:Label runat="server" ID="lblat">&nbsp;</asp:Label>:</b>
                                        <asp:Label runat="server" ID="lblempleaten"> texto</asp:Label><br />
                                        <b>Fecha:</b>
                                        <asp:Label runat="server" ID="lblFecha"> texto</asp:Label><br />
                                        <b>Departamento:</b>
                                        <asp:Label runat="server" ID="lblDepto"> texto</asp:Label><br />
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="div2" runat="server" style="text-align: left;">
                                <div class="col-sm-12 col-xs-12 ">
                                    <div class="col-sm-12 col-xs-12 ">
                                        <h5><i class="fa fa-newspaper-o" aria-hidden="true"></i>&nbsp; 
                                    <asp:Label runat="server" ID="lblDescripcion">Observaciones</asp:Label></h5>
                                        <asp:TextBox Style="text-transform: uppercase; resize: none;" onfocus="$(this).select();"
                                            ID="txtDescripcion" runat="server" TextMode="Multiline" Rows="2" CssClass="form-control"
                                            AutoPostBack="false" placeholder="Descripcion"></asp:TextBox>
                                        <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                                        <div id="div_pass" runat="server">
                                            <!--   -->
                                            <h5><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Usuario</h5>
                                            <asp:DropDownList ID="ddlUsuarios" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <div id="div_pass_1" runat="server">
                                                <h5><i class="fa fa-key" aria-hidden="true"></i>&nbsp;Contraseña</h5>
                                                <asp:TextBox ID="txtContraseña" MaxLength="250" TextMode="Password" onfocus="$(this).select();" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="yes" class="btn btn-info btn-block" OnClientClick="CerrarModal();" runat="server" OnClick="Yes_Click" Text="Aceptar" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" type="button" class="btn btn-danger btn-block" onclick="CerrarModal();" value="Cancelar" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <input type="hidden" runat="server" id="descripcion_h" />
                    <input type="hidden" runat="server" id="idc_ticketserv_h" />
                    <input type="hidden" runat="server" id="idc_ticketserva_h" />
                    <input type="hidden" runat="server" id="idc_tareaser_h" />
                    <input type="hidden" runat="server" id="sidc_puesto_h" />
                    <input type="hidden" runat="server" id="idc_usuario_aten_h" />
                    <input type="hidden" runat="server" id="idc_usuario_rep_h" />
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
