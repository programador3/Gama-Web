<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="grupos_backend_captura.aspx.cs" Inherits="presentacion.grupos_backend_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=txtminlib.ClientID %>').on('keypress', function (e) {
                return numeros(e, 0, this);
            });
            $('#<%=txtmaxlib.ClientID %>').on('keypress', function (e) {
                return numeros(e, 0, this);
            });

            $('#<%=txtminopcs.ClientID %>').on('keypress', function (e) {
                return numeros(e, 0, this);
            });
            $('#<%=txtmaxopcs.ClientID %>').on('keypress', function (e) {
                return numeros(e, 0, this);
            });

        });

        function ModalClose() {
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
    </script>
    <script type="text/javascript">
        function AlertOK(URL) {
            swal({
                title: "Mensaje del sistema",
                text: "Grupo Guardado correctamente",
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div id="page-wrapper">
        <div class="container-fluid">

            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1>
                        <asp:Label ID="txttitulo" runat="server"></asp:Label></h1>
                </div>
            </div>
            <!-- /.row -->
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h4 style="text-align: center;"><strong>Datos Principales</strong></h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Nombre</label>
                                <asp:TextBox ID="txtnombregpo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-2">

                                    <div class="form-group">
                                        <label>Orden</label>
                                        <asp:TextBox ID="txtorden" type="number" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Publicar en Occ</label>
                                        <asp:RadioButtonList ID="rbtnexterno" AutoPostBack="true" runat="server">
                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!-- /.row -->
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h4 style="text-align: center;"><strong>Entrada de datos</strong></h4>
                </div>
                <div class="panel-body">
                    <!-- /.row -->

                    <!-- /.row -->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rbtnlimitopciones" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnadd_etiq_lib" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="GridOpciones" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="checkopciones" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="checklibre" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="rbtnlimitelib" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-12">

                                    <asp:CheckBox ID="checklibre" runat="server" Text="Libre" AutoPostBack="True" OnCheckedChanged="checklibre_CheckedChanged" />
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Panel ID="panel_libre" runat="server">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Libre</h3>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-xs-12 col-sm-12 col-md-12  col-lg-6">

                                                        <div class="form-group">
                                                            <label>Manejar limite</label><asp:RadioButtonList ID="rbtnlimitelib" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtnlimitelib_SelectedIndexChanged">
                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-12 col-md-12  col-lg-6">
                                                        <asp:Panel ID="gpominmaxlibre" runat="server" CssClass="form-group">

                                                            <label>Min</label><asp:TextBox ID="txtminlib" runat="server" type="number"></asp:TextBox>
                                                            <label>Max</label><asp:TextBox ID="txtmaxlib" runat="server" type="number"></asp:TextBox>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">

                                    <asp:CheckBox ID="checkopciones" runat="server" Text="Opciones" AutoPostBack="True" OnCheckedChanged="checkopciones_CheckedChanged" />
                                </div>
                            </div>
                            <!-- /.row -->

                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Panel ID="panel_opciones" runat="server">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Opciones</h3>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-xs-12 col-sm-12 col-md-12  col-lg-6">

                                                        <div class="form-group">
                                                            <label>Manejar limite</label><asp:RadioButtonList ID="rbtnlimitopciones" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtnlimitopciones_SelectedIndexChanged">
                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-12 col-md-12  col-lg-6">
                                                        <asp:Panel ID="gpominmaxopciones" runat="server" CssClass="form-group">

                                                            <label>Min</label><asp:TextBox ID="txtminopcs" runat="server" type="number"></asp:TextBox>
                                                            <label>Max</label><asp:TextBox ID="txtmaxopcs" runat="server" type="number"></asp:TextBox>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-lg-6 col-md-8 col-sm-10 col-xs-12">
                                                            <label>Opcion</label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtopcsvalor" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <span class="input-group-addon">
                                                                    <asp:ImageButton ID="btnadd_etiq_lib" runat="server" ImageUrl="~/imagenes/btn/icon_add.png" OnClick="btnaceptaropcs_Click" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-8 col-sm-12">
                                                        <div class="table-responsive form-group">
                                                            <asp:GridView ID="GridOpciones" runat="server" CssClass="table table-condensed table-bordered table-hover table-responsive table-striped" DataKeyNames="idc_perfilgpod,descripcion" OnRowCommand="GridOpciones_RowCommand" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:ButtonField ButtonType="Image" CommandName="editar" HeaderText="Editar" ImageUrl="~/imagenes/btn/icon_editar.png" />
                                                                    <asp:ButtonField ButtonType="Image" CommandName="eliminarOpcion" HeaderText="Borrar" ImageUrl="~/imagenes/btn/icon_delete.png" />
                                                                    <asp:BoundField DataField="idc_perfilgpod" HeaderText="opcion_id" Visible="False" />
                                                                    <asp:BoundField DataField="descripcion" HeaderText="Opcion" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="row">

                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:Button ID="btnguardar" class="btn btn-primary btn-block" runat="server" Text="Guardar" OnClick="btnguardar_Click" />
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:Button ID="btncancelar" class="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="btncancelar_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <!-- campos ocultos -->
                    <asp:HiddenField ID="ocidc_perfilgpo" runat="server" />
                </div>
            </div>
            <!-- /.CONFIRMA -->
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
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
        <!-- /.container-fluid -->
    </div>
    <!-- /#page-wrapper -->
</asp:Content>