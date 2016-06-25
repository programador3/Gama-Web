<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="asignacion_lugares_puestos.aspx.cs" Inherits="presentacion.asignacion_lugares_puestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/ionicons.css" rel="stylesheet" />
    <link href="css/ionicons.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script type="text/javascript" src="/fancybox/jquery.easing-1.4.pack.js"></script>
    <link rel="stylesheet" href="/fancybox/jquery.fancybox-1.3.4.css" type="text/css" media="screen" />
    <script type="text/javascript">
        function ModalClose() {
            $('#myModal').modal('hide');
        }
        function ModalConfirm() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
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
    </script>

    <style type="text/css">
        .fancybox-custom .fancybox-skin {
            box-shadow: 0 0 50px #222;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Asiganción de Lugares </h1>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <div class="form-group">
                                <h3>Seleccione un Area de la Sucursal
                                    <asp:Label ID="lblsucursal" runat="server" Text=""></asp:Label></h3>
                                <asp:DropDownList ID="ddlAreas" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAreas_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="panel_sinlugares" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <h3>Esta Area no contiene Lugares Disponibles </h3>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panel_lugares" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-lg-12" style="text-align: center;">
                                <h3>Seleccione un lugar para el puesto
                                    <asp:Label ID="lblpuesto" runat="server" Text=""></asp:Label></h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <div class="form-group">
                                    <a id="a_img" runat="server" class="fancybox-effects-d">
                                        <img id="Areaim" runat="server" class="image img-responsive" alt="" />
                                    </a>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <div class="row">
                                    <asp:Repeater ID="repeater_puestos" runat="server" OnItemDataBound="repeater_puestos_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="up_repeat" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="btnLugar" CssClass="btn btn-default btn-block" OnClick="btnLugar_Click" runat="server" CommandName='<%#Eval("idc_lugar")%>' CommandArgument='<%#Eval("lugar")%>'>
                                                        <%#Eval("Folio")%> <i class="ion ion-briefcase"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                        <div class="form-group">
                                            <label class="label label-warning">LUGAR OCUPADO</label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                        <div class="form-group">
                                            <label class="label label-default">LUGAR DISPONIBLE</label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                        <div class="form-group">
                                            <label class="label label-success">LUGAR SELECCIONADO</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- /.CONFIRMA -->
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnAceptar" CssClass="btn btn-primary btn-block" OnClick="btnAceptar_Click" runat="server" Text="Guardar" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnCancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" runat="server" Text="Cancelar" />
                    </div>
                </div>
            </div>
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong>Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <asp:Label ID="content_modal" runat="server" Text="Label"></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
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
            </div>
        </div>
    </div>
</asp:Content>