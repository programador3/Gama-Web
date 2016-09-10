<%@ Page Title="Terminar" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="hallazgos_pendientes_terminar_m.aspx.cs" Inherits="presentacion.hallazgos_pendientes_terminar_m" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
        function ModalClose() {
            $('#myModal').modal('hide');
        }
        function ModalConfirm() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
        }
        function ModalConfirmcamb() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalcamb').modal('show');
        }
        function ModalClose() {
            $('#myModal').modal('hide');
            $('#myModalimg').modal('hide');
            $('#myModalcamb').modal('hide');
        }
        function ModalConfirmimg(cTitulo, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalimg').modal('show');
            $('#myModalimg').removeClass('modal fade modal-info');
            $('#myModalimg').addClass(ctype);
            $('#modal_titleimg').text(cTitulo);
        }
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
      <h2 class="page-header">Hallazgos Pendientes por Terminar</h2>
    <div class="row">
        <div class="col-lg-12">
            <h4><strong>Sucursal</strong></h4>
            <asp:DropDownList ID="ddlsucursal" AutoPostBack="true" OnSelectedIndexChanged="ddlsucursal_SelectedIndexChanged" CssClass="form-control" 
                runat="server"></asp:DropDownList>
        </div>
        <div class="col-lg-12">
            <div class="table table-responsive" style="font-size:12px;">
                <asp:GridView style="text-align:center;" ID="gridhallazgos" DataKeyNames="idc,observaciones, sucursal,tipoh, tipo,veh,reviso, usuario_sol,correo_capturo,idc_revsuccheck " AutoGenerateColumns="false" CssClass="gvv table table-responsive table-bordered table-condensed" runat="server" OnRowCommand="gridhallazgos_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 32%; border: none; background: none;">
                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="25px" ImageUrl="imagenes/btn/icon_add.png"
                                                ToolTip="Revisar" CommandName="Revisar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </td>
                                        <td style="width: 32%; border: none; background: none;">
                                            <asp:ImageButton ID="imgimagen" runat="server" Height="25px" ImageUrl="imagenes/btn/image-x-generic.png"
                                                ToolTip="Imagen" Width="25px" CommandName="Ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="hallazgo_corto" HeaderText="Hallazgo"></asp:BoundField>
                        <asp:BoundField DataField="observaciones" Visible="false" HeaderText="Hallazgo"></asp:BoundField>
                        <asp:BoundField DataField="fecha_string_solucion" HeaderText="Fecha Solución" HeaderStyle-Width="160px"></asp:BoundField>
                        <asp:BoundField DataField="usuario_sol" HeaderText="Usuario Solucion" HeaderStyle-Width="80px"></asp:BoundField>
                        <asp:BoundField DataField="sucursal" HeaderText="Sucursal" HeaderStyle-Width="90px"></asp:BoundField>
                        <asp:BoundField DataField="idc" HeaderText="idc" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="idc" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="tipoh" HeaderText="idc" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="VEH" HeaderText="idc" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="correo_capturo" Visible="false" HeaderText="Hallazgo"></asp:BoundField>                        
                        <asp:BoundField DataField="reviso" Visible="false" HeaderText="Hallazgo"></asp:BoundField>
                        <asp:BoundField DataField="idc_revsuccheck" HeaderText="idc" Visible="False"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <asp:Label ID="idc_halla" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbltipoh" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbltipo" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblsucursal" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="LBLVEH" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="reviso" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="usuario_sol" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="correo_capturo" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="idc_principal" Visible="false" runat="server" Text=""></asp:Label>
    </div>
    <div class="modal fade modal-info" id="myModalimg" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="modal_titleimg"><strong>Mensaje del Sistema</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <asp:TextBox ReadOnly="true" ID="txthallazgo" CssClass="form-control" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-12" style="display: table-cell; vertical-align: middle; text-align: center;">

                            <asp:Image AlternateText="El Sistema No Encontro la Imagen en la Ruta, Puede Deberse a que sea un proyecto de pruebas." ID="img" Style="margin-left: auto; margin-right: auto;"
                                runat="server" CssClass="image img-responsive" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-12">
                        <input id="No" class="btn btn-info btn-block" onclick="ModalClose();" value="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div class="modal fade modal-success" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4><strong>Revisar Hallazgo</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                           <h5> <strong >Sucursal</strong></h5>
                            <asp:TextBox ReadOnly="true" ID="txtsucursal" CssClass="form-control" runat="server"></asp:TextBox>
                            <h5><strong>Hallazgo</strong></h5>
                            <asp:TextBox style="font-size:11px; resize:none;" ReadOnly="true" ID="txthallazgo_revi" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <asp:TextBox style="font-size:11px; resize:none;" ReadOnly="false" ID="txtcomentarios" placeholder="Comentarios" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                        </div>                        
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <asp:FileUpload ID="fuparchivo" runat="server" CssClass="form-control" />
                        </div>
                       <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 " runat="server" id="diverror" visible="false">
                           <div class="alert fresh-color alert-danger alert-dismissible" role="alert">
                               <strong>ERROR</strong>
                               <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                           </div>
                       </div>
                    </div>
                </div>
                <div class="modal-footer"> 
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <asp:LinkButton ID="LinkButton1" OnClick="RevisarHallazgo" OnClientClick="ModalClose(); Gift('Estamos Terminando el Hallazgo y Enviando los Correos');" CssClass="btn btn-success btn-block" runat="server">Revisar</asp:LinkButton>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <input id="No1" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
  
</asp:Content>
