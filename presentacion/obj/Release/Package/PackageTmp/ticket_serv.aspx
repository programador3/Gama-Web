<%@ Page Title="Ticket de Servicios" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="ticket_serv.aspx.cs" Inherits="presentacion.ticket_serv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .form-control1 {
            float: right;
            text-align: right;
            font-weight: bold;
            color: blue;
        }

        .form-control + .form-control-feedback {
            left: 0;
        }

        .input-group {
            display: block;
        }
    </style>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <h1 class="page-header"><i class="fa fa-bookmark-o" aria-hidden="true"></i>&nbsp; Ticket de Servicios</h1>
   

    <div class="row">
        <div id="grids" runat="server" visible="true">
                    <div class="col-lg-12 col-md-12 ">
            <div class="panel panel-info fresh-color">

                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-list"></i>&nbsp;Ticket de Servicio En Espera. </h3>
                </div>
                <div class="panel-body">
                    <h3 id="NO_Hay_E" runat="server" visible="false" style="text-align: center;">NO HAY DATOS <i class="fa fa-exclamation-triangle"></i></h3>
                    <div class="table-responsive">
                        <asp:GridView Style="font-size: 12px; text-align:center;" ID="grid_E" CssClass="gvv table table-responsive table-condensed table-bordered" AutoGenerateColumns="false" runat="server"
                            DataKeyNames="idc_ticketserv,idc_tareaser,descripcion,fecha,observaciones,EMPLEADO_ATIENDE,ARCHIVO,empleado,DEPTO" OnRowCommand="grid_E_RowCommand" OnRowDataBound="grid_E_RowDataBound">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_autorizar.png" HeaderText="Atender" CommandName="Atender">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_borrar.png" HeaderText="Cancelar" CommandName="Cancelar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                   <asp:ButtonField ButtonType="Image" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_download.png" HeaderText="Archivo" CommandName="Descargar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="idc_tareaser" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="idc_ticketserv" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="descripcion" Visible='false'></asp:BoundField>                        
                                <asp:ButtonField  ButtonType="Button" ControlStyle-CssClass="btn btn-default btn-block"  HeaderText="Descripcion" DataTextField="des_corta" CommandName="preview">
                                    <HeaderStyle HorizontalAlign="Center"  />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha Solicitud"></asp:BoundField>
                                <asp:BoundField DataField="observaciones" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="obs_corta" HeaderText="Observaciones"></asp:BoundField>
                                <asp:BoundField DataField="empleado" HeaderText="Empleado"></asp:BoundField>
                                <asp:BoundField DataField="DEPTO" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="ARCHIVO" Visible='false'></asp:BoundField>
                                <asp:boundfield DataField="EMPLEADO_ATIENDE" Visible="false" HeaderText="idc_puesto_rep" />

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12 col-md-12">
            <div class="panel panel-primary">

                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-list"></i>&nbsp;Ticket de Servicio Atendido. </h3>
                </div>
                <div class="panel-body">
                    <h3 id="NO_Hay_A" runat="server" visible="false" style="text-align: center;">NO HAY DATOS <i class="fa fa-exclamation-triangle"></i></h3>
                    <div class="table-responsive">
                        <asp:GridView Style="font-size: 12px; text-align:center;" ID="grid_A" CssClass="table table-responsive table-condensed table-bordered" AutoGenerateColumns="false" runat="server"
                            DataKeyNames="idc_ticketserv,idc_ticketserva,idc_tareaser,descripcion,fecha,EMPLEADO_ATIENDE,ARCHIVO,empleado,DEPTO,observaciones,idc_usuario_aten,idc_usuario,idc_puesto" 
                            OnRowCommand="grid_A_RowCommand" OnRowDataBound="grid_A_RowDataBound">
           
                            <Columns>
                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_autorizar.png" HeaderText="Terminar" CommandName="Terminar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:ButtonField  ButtonType="Image" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_borrar.png" HeaderText="Cancelar" CommandName="Aten_Cancelar">
                                    <HeaderStyle HorizontalAlign="Center"  />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                   <asp:ButtonField ButtonType="Image" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_download.png" HeaderText="Archivo" CommandName="Descargar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>

                                <asp:BoundField DataField="idc_tareaser" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="idc_ticketserv" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="idc_ticketserva" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="descripcion" Visible='false'></asp:BoundField>                                
                                <asp:ButtonField  ButtonType="Button" ControlStyle-CssClass="btn btn-default btn-block"  HeaderText="Descripcion" DataTextField="des_corta" CommandName="preview">
                                    <HeaderStyle HorizontalAlign="Center"  />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha Atendiendo"></asp:BoundField>
                                <asp:BoundField DataField="observaciones" Visible='false'></asp:BoundField>
                                <asp:BoundField DataField="obs_corta" HeaderText="Observaciones"></asp:BoundField>
                                <asp:BoundField DataField="EMPLEADO_ATIENDE" HeaderText="Empleado Atendiendo"></asp:BoundField>
                                <asp:BoundField DataField="DEPTO" Visible='false' ></asp:BoundField>
                                <asp:BoundField DataField="idc_usuario_aten" Visible="false" ></asp:BoundField>
                                <asp:BoundField DataField="idc_usuario" Visible="false" HeaderText="idc_usuario_rep" />
                                <asp:boundfield DataField="idc_puesto" Visible="false" HeaderText="idc_puesto_rep" />
                                <asp:BoundField DataField="ARCHIVO" Visible='false'></asp:BoundField>
                                <asp:boundfield DataField="EMPLEADO" Visible="false" HeaderText="idc_puesto_rep" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
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
                                        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
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
                                    <asp:Button ID="yes" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" onclick="CerrarModal();" value="Cancelar" />
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
</asp:Content>
