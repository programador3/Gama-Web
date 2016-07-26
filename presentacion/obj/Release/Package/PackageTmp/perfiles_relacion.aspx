<%@ Page Title="Relacion" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="perfiles_relacion.aspx.cs" Inherits="presentacion.perfiles_relacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }

        function ModalClose() {
            $('#myModal').modal('hide');
            $('#ModalArchivos').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Solicitar Relacion de Perfil con Puestos</h1>
   
   <asp:UpdatePanel ID="AA" UpdateMode="Always" runat="server">
       <ContentTemplate>
           <div class="row">
               <div class="col-lg-6 col-md-8 col-sm-12 col-xs-12">

                   <h4><strong><i class="fa fa-tags" aria-hidden="true"></i>&nbsp;Seleccionar Perfil</strong></h4>
                   <asp:DropDownList ID="ddlperfil" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlperfil_SelectedIndexChanged">
                   </asp:DropDownList>
               </div>
               <div class="col-lg-4 col-md-4 col-sm-8 col-xs-8">
                   <h4>Escriba un Filtro</h4>
                   <asp:TextBox ID="txtperfil_filtro" OnTextChanged="lnkbuscarperfil_Click" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" placeholder="Escriba el Nombre del Perfil"></asp:TextBox>
               </div>
               <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                   <h4>.</h4>
                   <asp:LinkButton ID="lnkbuscarperfil" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarperfil_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
               </div>
           </div>
           <div class="row">
               <div class="col-lg-6 col-md-8 col-sm-12 col-xs-12" id="dd" runat="server" visible="false">
                   <asp:DropDownList ID="ddlPuesto" OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                   </asp:DropDownList>
               </div>
               <div class="col-lg-12" id="Div1" runat="server" visible="false">
                   <asp:LinkButton ID="lnkadd" runat="server" CssClass="btn btn-info btn-block" OnClick="lnkadd_Click">Agregar <i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
               </div>
               <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                   <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Seleccionar Puesto</strong></h4>
                   <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
               </div>
               <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                   <h4>.</h4>
                   <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
               </div>
               <div class="col-lg-12">
                   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                       <ContentTemplate>
                           <div style="width: 100%; height: 300px; overflow-y: auto">
                               <asp:Repeater ID="repeat_pues" runat="server">
                                   <ItemTemplate>
                                       <asp:UpdatePanel ID="UpdatePanel1RFRFR" UpdateMode="Always" runat="server">
                                           <Triggers>
                                               <asp:AsyncPostBackTrigger ControlID="lnkpuesto" EventName="Click" />
                                           </Triggers>
                                           <ContentTemplate>

                                               <asp:LinkButton ID="lnkpuesto" CssClass="btn btn-default btn-block" runat="server" CommandName='<%#Eval("idc_puesto") %>' OnClick="lnkpuesto_Click">
                                                    <h5><%#Eval("descripcion_puesto_completa") %></h5>
                                               </asp:LinkButton>
                                           </ContentTemplate>
                                       </asp:UpdatePanel>
                                   </ItemTemplate>
                               </asp:Repeater>
                           </div>
                       </ContentTemplate>
                   </asp:UpdatePanel>


               </div>
           </div>
           <div class="row">
               <div class="col-lg-12">
                   <h4><strong>Solicitudes Para Relación</strong></h4>
                   <div class="table table-responsive">
                       <asp:GridView ID="gridperfil" runat="server" DataKeyNames="idc_puestoperfil,idc_puesto" CssClass="table table-responsive table-bordered" OnRowCommand="gridperfil_RowCommand" AutoGenerateColumns="False">
                           <Columns>
                               <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar">
                                   <ItemStyle HorizontalAlign="Center" Width="30px" />
                               </asp:ButtonField>
                               <asp:BoundField DataField="puesto" HeaderText="Puesto"></asp:BoundField>
                               <asp:BoundField DataField="perfil" HeaderText="Perfil" Visible="true"></asp:BoundField>
                               <asp:BoundField DataField="idc_puestoperfil" HeaderText="idc_procesosarc" Visible="false"></asp:BoundField>
                               <asp:BoundField DataField="idc_puesto" HeaderText="idc_procesosarc" Visible="false"></asp:BoundField>
                           </Columns>
                       </asp:GridView>
                   </div>
               </div>
           </div>
       </ContentTemplate>
   </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
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
                        <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>