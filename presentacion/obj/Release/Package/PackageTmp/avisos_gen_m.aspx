<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="avisos_gen_m.aspx.cs" Inherits="presentacion.avisos_gen_m" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">
           function DataTa1() {
               $('#<%= gridperfiles.ClientID%>').DataTable();
           }
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
        .read {        
             border: 1px solid #ccc;
            background-color: #eee;
            opacity: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <h3 class="page-header">Enviar Aviso
     </h3>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div class="row">
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong>Asunto </strong></h5>
                     <asp:TextBox ID="txtasunto" CssClass="form-control" placeholder="Asunto" runat="server" MaxLength="100"></asp:TextBox>
                </div>
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong>Aviso </strong></h5>
                     <asp:TextBox ID="txtaviso" CssClass="form-control" placeholder="Aviso" TextMode="Multiline" Rows="5" runat="server"></asp:TextBox>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">                    
                    <h5><strong>Seleccionar Usuario </strong></h5>
                     <asp:TextBox  AutoPostBack="True" ID="txtbuscar" CssClass="form-control2" Width="70%" placeholder="Buscar" runat="server" MaxLength="100" OnTextChanged="lnkbuscar_Click"></asp:TextBox>
                    <asp:LinkButton ID="lnkbuscar" CssClass="btn btn-success" Width="29%" runat="server" OnClick="lnkbuscar_Click">Buscar</asp:LinkButton>
                    <asp:DropDownList ID="ddlusuarios" CssClass="form-control2" Width="70%" runat="server"></asp:DropDownList>
                    <asp:LinkButton ID="lnkagregar" CssClass="btn btn-info" Width="29%" runat="server" OnClick="lnkagregar_Click">Agregar</asp:LinkButton>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:LinkButton ID="lnkagregarTodo" CssClass="btn btn-default btn-block" runat="server" OnClick="lnkagregarTodo_Click">Agregar Todos los Usuarios</asp:LinkButton>
                </div>
                
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="table table-responsive" style=" overflow: auto; height:300px; background-color:gainsboro;">
                        <asp:GridView ID="gridperfiles" runat="server" CssClass="table table-bordered table-responsive table-condensed" AutoGenerateColumns="False" DataKeyNames="idc_usuario" OnRowCommand="gridperfiles_RowCommand" >
                            <Columns>
                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="30px" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Borrar">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="idc_usuario" HeaderText="Id_b" Visible="False" />
                                <asp:BoundField DataField="usuario_nombre" HeaderText="Usuario" Visible="true" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
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
