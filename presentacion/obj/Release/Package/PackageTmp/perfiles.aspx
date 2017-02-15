<%@ Page Title="Perfiles" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="perfiles.aspx.cs" Inherits="presentacion.perfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/highlight.css" rel="stylesheet" />
    <link href="css/bootstrap-switch.css" rel="stylesheet" />
    <link href="css/main.css" rel="stylesheet" />

    <script type="text/javascript">
        function Return(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalClose() {
            $('#myModal').modal('hide');
            $("[id='<%= btnSinLigar.ClientID%>']").hide();
            $("[id='<%= btnGuardarSinLigar.ClientID%>']").hide();
            $('#Yes').text('Si');
        }
        $(window).resize(function () {
            var href = $(location).attr('href');
            if ($(window).width() < 800) {
                window.location.replace(href);
            } else {
                window.location.replace(href);
            }
        });
    </script>

    <style>
        .p-produccion {
            color: #FFF;
            background-color: #1abc9c;
            border-color: #1abc9c;
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .p-borrador {
            color: #fff;
            background-color: #353d47;
            border-color: #353d47;
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        A:hover {
            text-decoration: none;
            color: #000000;
        }

        .produccion {
            color: #FFF;
            background-color: #1abc9c;
            border-color: #1abc9c;
            padding: 6px 12px;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.42857;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            -moz-user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            display: block;
            width: 100%;
        }

        .borrador {
            color: #fff;
            background-color: #353d47;
            border-color: #353d47;
            padding: 6px 12px;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.42857;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            -moz-user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            display: block;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <asp:Literal ID="ltlBORR" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="ltlPRO" runat="server" Visible="false"></asp:Literal>
    <asp:HiddenField ID="id_res" runat="server" />
    <!-- Page Heading -->
    <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">Perfiles
            </h2>
            <h4>Esta Viendo Perfiles 
                <asp:Label ID="lblmensaje" runat="server" Text=" en Proceso" CssClass="btn btn-primary"></asp:Label></h4>
        </div>
    </div>
    <!-- /.row -->

    <div class="row">
        <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4">
            <asp:LinkButton ID="lnknuevo" CssClass="btn btn-primary btn-block" OnClick="lnknuevo_Click" runat="server">Nuevo Perfil <i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
        </div>
        <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4">
            <asp:LinkButton ID="lnktodos" CssClass="btn btn-default btn-block" runat="server" OnClick="lnktodos_Click">Perfiles de Todos los Usuarios</asp:LinkButton>
        </div>
        <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4">
            <asp:LinkButton ID="lnkproduccion" CssClass="btn btn-success btn-block" OnClick="lnkproduccion_Click" runat="server">Ver Perfiles Autorizados</asp:LinkButton>
            <asp:LinkButton ID="lnkborrador" Visible="false" CssClass="btn btn-primary btn-block" OnClick="lnkborrador_Click" runat="server">Ver Perfiles en Proceso</asp:LinkButton>
            <asp:CheckBox ID="cbxTipo" AutoPostBack="true" runat="server" BorderStyle="None" Visible="false" />
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4">
        </div>
        <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4">
        </div>
        <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4">
            <h6>Filtrar por Depto
                <span>
                    
             <asp:LinkButton ID="LinkButton1" CssClass="btn btn-danger" runat="server" OnClick="LinkButton1_Click">Reporte de Relacion Puestos vs Perfiles</asp:LinkButton>
                </span>
            </h6>
            <asp:DropDownList AutoPostBack="True" ID="ddldeptos" CssClass =" form-control" runat="server" OnSelectedIndexChanged="ddldeptos_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="table table-responsive">
                <asp:GridView ID="gridperfiles" runat="server" CssClass="gvv table table-bordered table-condensed table-responsive" style="font-size:12px; text-align:center;"
                     AutoGenerateColumns="False" DataKeyNames="id_perfilproduccion,id_perfilborrador,descripcion" OnSelectedIndexChanged="gridperfiles_SelectedIndexChanged"
                      OnRowDataBound="gridperfiles_RowDataBound" OnRowCommand="gridperfiles_RowCommand">

                    <Columns>
                        <asp:CommandField ButtonType="Image" EditText="" HeaderText="Editar" SelectImageUrl="~/imagenes/btn/icon_editar.png" SelectText="" ShowSelectButton="True">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        </asp:CommandField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>                        
                        <asp:ButtonField ButtonType="Button" Text="Crear" ControlStyle-CssClass="btn btn-info btn-block"  HeaderText="Crear Borrador" CommandName="CrearBorrador">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="id_perfilproduccion" HeaderText="Id" Visible="False">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="id_perfilborrador" HeaderText="Id_b" Visible="False" />
                        <asp:ButtonField DataTextField="descripcion" HeaderText="Perfil" CommandName="Vista" />                        
                        <asp:BoundField DataField="DEPTO" HeaderText="Depto" />
                        <asp:BoundField HeaderText="Autor" DataField="usuario"></asp:BoundField>
                        <asp:TemplateField HeaderText="Perfil Autorizado" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <asp:Image ID="produccion" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="En Proceso" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <asp:Image ID="borrador" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Button" Text="Solicitar Autorización" ControlStyle-CssClass="btn btn-info btn-block"  CommandName="Solicitar">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_autorizar.png" HeaderText="Desbloquear" CommandName="Desbloquear">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row" style="text-align: center; display:none;">
                <div class="col-lg-2" style="background-color: #1de9b6; text-align: center;">
                    <div class="form-group" style="background-color:#1de9b6; text-align: center;">
                        Indica que es un Nuevo Registro
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div></div>
    <!-- /.row -->
    <!-- /.CONFIRMA -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="Yes" />
                    <asp:AsyncPostBackTrigger ControlID="gridperfiles" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardarSinLigar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSinLigar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content" style="text-align: center">
                        <div class="modal-header" style="background-color: #428bca; color: white">
                            <h4><strong id="confirmTitulo" class="modal-title">Mensaje del Sistema</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h4>
                                        <label id="confirmContenido"></label>
                                    </h4>
                                    <asp:Panel ID="PanelNuevoBorrador" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:TextBox ID="txtNombrePerfil" placeholder="Escriba el Nombre del Perfil" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:Label ID="lblerror" CssClass="label label-danger" runat="server" Text="ESCRIBA EL NOMBRE DEL PERFIL" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                   <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="btnConfirm_Click" />
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                    <asp:Button Visible="false" ID="btnGuardarSinLigar" runat="server" Text="Guardar" class="btn btn-warning btn-block" OnClick="btnGuardarSinLigar_Click" />
                                        <asp:Button Visible="false" ID="btnSinLigar" class="btn btn-warning btn-block" runat="server" Text="Si, sin ligar a perfil" OnClick="btnSinLigar_Click" />
                                </div>

                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                  <asp:Button ID="btnNo" runat="server" class="btn btn-danger btn-block" Text="No" OnClientClick="ModalClose();" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script src="js/bootstrap-switch.js"></script>
    <script>
        $(function (argument) {
            $("[id='<%= cbxTipo.ClientID%>']").bootstrapSwitch();
        })
    </script>
</asp:Content>