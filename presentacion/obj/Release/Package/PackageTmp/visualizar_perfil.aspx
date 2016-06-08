<%@ Page Title="Perfiles Vista Previa" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="visualizar_perfil.aspx.cs" Inherits="presentacion.visualizar_perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="js/jquery.js"></script>
    <script src="js/BlockHumberto.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("textarea").click(function () {
                this.select();
            });
        });
    </script>
    <script type="text/javascript">
        function Return(URLBACK) {
            swal({
                title: "Alerta",
                text: "No hay ningun PERFIL relacionado con este PUESTO.",
                type: "error",
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                allowEscapeKey: false
            },
               function () {
                   location.href = URLBACK;
               });
        }
    </script>
    <style>
        #IrArriba {
            position: fixed;
            bottom: 30px; /* Distancia desde abajo */
            right: 30px; /* Distancia desde la derecha */
        }

            #IrArriba span {
                width: 60px; /* Ancho del botón */
                height: 60px; /* Alto del botón */
                display: block;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div id="page-wrapper">
        <div class="page-header">
            <div class="container">
                <h2>Perfil de <small>
                    <asp:Label ID="txttitulo" runat="server"></asp:Label></small></h2>
                <asp:ImageButton ID="ImageButton5" runat="server" Height="50px" ImageUrl="~/imagenes/btn/icono_organigrama.png" Width="50px" ToolTip="Ir al Organigrama" OnClick="ImageButton5_Click" />
                <asp:ImageButton ID="ImageButton4" runat="server" Height="50px" ImageUrl="~/imagenes/btn/return_default.png" Width="50px" ToolTip="Ver Perfil" OnClick="ImageButton4_Click" />
                <asp:ImageButton ID="ImageButton1" runat="server" Height="50px" ImageUrl="~/imagenes/btn/word_icon.png" Width="50px" ToolTip="Guarde este perfil en un documento de Word." OnClick="ImageButton1_Click" />
                <asp:ImageButton ID="ImageButton2" runat="server" Height="60px" ImageUrl="~/imagenes/btn/html_icon.png" ToolTip="Guarde este perfil en código HTML para poder utilizarlo posteriormente." OnClick="ImageButton2_Click" Width="60px" />
                <asp:ImageButton ID="ImageButton3" runat="server" Height="55px" ImageUrl="~/imagenes/btn/preview_icon.png" ToolTip="Mire una vista previa de como se vera el documento web con este código." Visible="False" Width="55px" OnClick="ImageButton3_Click" />
            </div>
            <br />

            <div class="container">
                <asp:Panel ID="PanelHTML" Visible="False" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b>Código HTML <i class="fa fa-file-code-o"></i>
                                        <small>De un CLICK en cualquier parte del código para que este se seleccione.</small></b>
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <textarea id="TextArea1" rows="20" class="form-control"> <asp:Label ID="txthtml" runat="server"></asp:Label>
                                                            </textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="VistaPrevia" Visible="False" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b>Vista previa del código <small>Esta es la manera en la que el código se visualizara en otras pagina WEB, por ejemplo: MyOCC.</small></b>
                                </div>
                                <div class="panel-body">

                                    <asp:Label ID="lbVistaPrevia" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Repeater ID="RepeatDataPuesto" runat="server" OnItemDataBound="RepeatDataPuesto_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-lg-10 col-md-10 col-sm-8 col-xs-12">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <h3 class="panel-title"><i class="fa fa-info-circle fa-fw"></i>
                                            <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Grupo") %>' /></h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="list-group">
                                            <asp:Repeater ID="RepeaterChild" runat="server" OnItemDataBound="RepeatChild_ItemDataBound">
                                                <ItemTemplate>
                                                    <a class="list-group-item">
                                                        <asp:Label ID="lblEtiqueta" runat="server" Text='<%#Eval("Etiqueta")%>'> <i class="fa fa-fw fa-check"></i></asp:Label><asp:Label ID="Label1" runat="server" Text='<%#Eval("Valor")%>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <!-- /.row -->
        </div>
    </div>
    <div id='IrArriba'>
        <a href='#Arriba'>
            <img src="imagenes/btn/return-up.png" /></a>
    </div>
</asp:Content>