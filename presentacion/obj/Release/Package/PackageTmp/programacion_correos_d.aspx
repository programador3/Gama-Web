<%@ Page Title="Correo" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="programacion_correos_d.aspx.cs" Inherits="presentacion.programacion_correos_d" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header" style="text-align: center;">Detalles del Correo</h1>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header">
                    <div class="card-title">
                        <div class="title"><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;<strong>Datos Principales</strong></div>
                    </div>
                </div>
                <div class="card-body">
                    <h4><strong><i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;Asunto:</strong>
                        <asp:Label ID="lblasunto" runat="server" Text=""></asp:Label></h4>
                    <h4><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha de Solicitud:</strong>
                        <asp:Label ID="lblfecha" runat="server" Text=""></asp:Label></h4>
                    <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Usuario que Solicito:</strong>
                        <asp:Label ID="lblusuario" runat="server" Text=""></asp:Label></h4>
                    <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre a Mostrar:</strong>
                        <asp:Label ID="lblnommostr" runat="server" Text=""></asp:Label></h4>
                    <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Correo que Enviara:</strong>
                        <asp:Label ID="lblcorreo" runat="server" Text=""></asp:Label></h4>
                    <h4 id="todos" runat="server"><strong><i class="fa fa-upload" aria-hidden="true"></i>&nbsp;El Correo se Enviara a TODOS LOS CORREOS</strong></h4>
                    <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Lista de Correo que Enviara:</strong>
                        <asp:Label ID="lbltitlecorreo" runat="server" Text=""></asp:Label></h4>
                    <br />
                    <h3><i class="fa fa-thumbs-o-up" aria-hidden="true"></i>&nbsp;<strong>Autorización</strong> </h3>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:TextBox onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" TextMode="MultiLine" Rows="2" ID="txtobsr" CssClass="form-control" placeholder="Observaciones" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton ID="lnkautorizar" OnClick="lnkautorizar_Click" CssClass="btn btn-success btn-block" runat="server">Autorizar <i class="fa fa-check-circle" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton ID="lnkrehaza" OnClick="lnkrehaza_Click" CssClass="btn btn-danger btn-block" runat="server">Rechazar <i class="fa fa-undo" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="correos" runat="server">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header">
                    <div class="card-title">
                        <div class="title"><i class="fa fa-cc" aria-hidden="true"></i>&nbsp;<strong> Correos a los que se Enviara</strong></div>
                    </div>
                </div>
                <div class="card-body">
                    <div>
                        <div style="width: 100%; height: 200px; overflow-y: auto">
                            <ul class="list-group">
                                <asp:Repeater ID="repeatcorreos" runat="server">
                                    <ItemTemplate>
                                        <li class="list-group-item col-lg-3 col-md-3 col-sm-12"><%#Eval("correo") %></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="fechas" runat="server">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header">
                    <div class="card-title">
                        <div class="title"><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;<strong>Fecha(s) de Envio</strong></div>
                    </div>
                </div>
                <div class="card-body">
                    <div>
                        <div style="width: 100%; height: 200px; overflow-y: auto">
                            <ul class="list-group">
                                <asp:Repeater ID="repeatfechas" runat="server">
                                    <ItemTemplate>
                                        <li class="list-group-item col-lg-3 col-md-3 col-sm-12"><%#Eval("fecha") %></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="archivos" runat="server">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header">
                    <div class="card-title">
                        <div class="title"><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;<strong>Archivos Adjuntos</strong></div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <asp:Repeater ID="repeat_archivos" runat="server">
                            <ItemTemplate>
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                    <asp:LinkButton ID="lnk" CssClass="btn btn-info btn-block" OnClick="lnk_Click" CommandName=' <%#Eval("url") %>' CommandArgument=' <%#Eval("name") %>' runat="server"> <%#Eval("name") %> <i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header">
                    <div class="card-title">
                        <div class="title"><strong>Vista Previa del Correo</strong></div>
                    </div>
                </div>
                <div class="card-body">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </div>
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
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>