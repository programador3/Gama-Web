<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="recordatorios_pendientes.aspx.cs" Inherits="presentacion.recordatorios_pendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModalrec').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalrec').modal('show');
            $('#myModalrec').removeClass('modal fade modal-info');
            $('#myModalrec').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
    </script>
    <style type="text/css">
        .form-control2 {
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Recordatorios Pendientes <span>
                <asp:LinkButton ID="lnksolucionar" CssClass="btn btn-info" runat="server" PostBackUrl="recordatorios.aspx">
                                    Nuevo Recordatorio <i class="fa fa-plus-circle" aria-hidden="true"></i>
                </asp:LinkButton>
            </span></h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:LinkButton ID="lnktodas" runat="server" CssClass="btn btn-danger btn-block" OnClick="lnktodas_Click">Descartar Todos <i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-success btn-block" OnClick="pospomulti_Click">Posponer Todos <i class="fa fa-calendar" aria-hidden="true"></i></asp:LinkButton>
        </div>
        <asp:Repeater ID="repeat" runat="server">
            <ItemTemplate>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <div class="thumbnail no-margin-bottom">
                        <div class="caption">
                            <h5 id="thumbnail-label"><strong><%#Eval("asunto") %></strong></h5>
                            <p style="font-size: 12px; height: 15px"><strong>Fecha de Aviso:</strong>&nbsp;<%#Eval("fecha_aviso") %></p>
                            <p style="font-size: 12px; height: 70px"><%#Eval("descripcion_resumido") %></p>
                            <p>
                                <asp:LinkButton ID="lnksolucionar" CommandName="sol" OnClick="btnver_Click" CommandArgument='<%#Eval("idc_avisogen") %>' CssClass="btn btn-info" runat="server">
                                    Visualizar <i class="fa fa-eye" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkdescartar" CommandName="sol" OnClick="btndesc_Click" CommandArgument='<%#Eval("idc_avisogen") %>' CssClass="btn btn-danger" runat="server">
                                    Descartar <i class="fa fa-times" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="LinkButton1" CommandName="sol" OnClick="pospo_Click" CommandArgument='<%#Eval("idc_avisogen") %>' CssClass="btn btn-success" runat="server">
                                    Posponer <i class="fa fa-calendar" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:LinkButton Visible='<%#Convert.ToInt32(Eval("pospuesto"))==0?false:true %>' ID="lnkpospuesta" CommandName="sol" OnClick="historial_Click" CommandArgument='<%#Eval("idc_avisogen") %>' CssClass="btn btn-primary" runat="server">
                                    Historial <i class="fa fa-database" aria-hidden="true"></i>
                                </asp:LinkButton>
                                   <asp:LinkButton Visible='<%#Request.QueryString["all"]==null?false:true %>' ID="LinkButton3" CommandArgument='<%#Eval("idc_avisogen") %>' OnClick="editar_Click" CommandName="editar"  CssClass="btn btn-default" runat="server">
                                    Editar <i class="fa fa-pencil" aria-hidden="true"></i>
                                </asp:LinkButton>
                            </p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="modal fade modal-info" id="myModalrec" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="modal_title"><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <h4>
                            <label id="content_modal"></label>
                        </h4>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: left;" id="recor" runat="server" visible="false">
                            <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha de Aviso:</strong>
                                <asp:Label ID="lblfecha" Visible="true" runat="server" Text=""></asp:Label></h5>
                            <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Descripcion:</strong>
                                <asp:Label ID="lbldescripcion" Visible="true" runat="server" Text=""></asp:Label></h5>
                            <h5>
                                <strong>
                                    <i class="fa fa-envelope-o" aria-hidden="true"></i>&nbsp;Correo Relacionado: </strong>
                                <a href="" id="correoto" runat="server">
                                    <asp:Label ID="lnlcorreo" Visible="true" runat="server" Text=""></asp:Label></a>
                            </h5>
                        </div>
                    </div>

                    <div class="row" style="text-align: center; padding: 5px;" id="cambio" runat="server" visible="false">
                        <h5><strong><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;Posponer Recordatorio</strong></h5>
                        <asp:TextBox CssClass="form-control2" Width="45%" ID="txtnum" TextMode="Number" autofocus onkeypress="return validarEnteros(event);" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="ddltipo" CssClass="form-control2" runat="server" Width="45%">
                            <asp:ListItem Text="MINUTOS" Value="1"></asp:ListItem>
                            <asp:ListItem Text="HORAS" Value="2"></asp:ListItem>
                            <asp:ListItem Text="DIAS" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                    </div>
                    <div style="padding: 5px;">
                        <asp:TextBox ID="txtobsr" Width="100%" TextMode="Multiline" Rows="3" placeholder="Ingrese Observaciones" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="row" id="historial" runat="server" visible="false">
                        <div class="col-12 col-md-12 col-sm-12 col-xs-12">
                            <h5 style="text-align: center;"><strong>Historial de Movimientos</strong></h5>
                            <asp:GridView Style="font-size: 9px;" ID="gridgistorial" CssClass="table table-responsive table-bordered table-condensed" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="tipo" HeaderText="Movimiento">
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="motivo" HeaderText="Motivo">
                                        <HeaderStyle Width="250px"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_anterior" HeaderText="Fecha Anterior">
                                        <HeaderStyle Width="70px"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Movimiento">
                                        <HeaderStyle Width="70px"></HeaderStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>