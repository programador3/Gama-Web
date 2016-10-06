<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="Compromisos_Cliente.aspx.cs" Inherits="presentacion.Compromisos_Cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">        
        function ModalClose() {
            $('#myModalEditar').modal('hide');           
        }
        
        function ModalEditar(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalEditar').modal('show');
            $('#myModalEditar').removeClass('modal fade modal-info');
            $('#myModalEditar').addClass(ctype);
            $('#ModalEditar_title').text(cTitulo);
            $('#content_ModalEditar').text(cContenido);
            //$('#contenido_RevisarCompromiso').css()
        }
        
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
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
        function AsignaProgress(value) {
            $('#pavance').width(value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    
    <h2 class="page-header" style="text-align: center;">Compromisos con el Cliente</h2>
    <div class="row">
        <div class="col-lg-12">
            <h4><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente </h4>
            <asp:TextBox ID="txtcliente" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" CssClass="form-control" placeholder="Cliente" runat="server" Enabled ="false"></asp:TextBox>
            <h4><i class="fa fa-credit-card" aria-hidden="true"></i>&nbsp;R.F.C. </h4>            
            <asp:TextBox ID="txtrfc" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" CssClass="form-control" placeholder="R.F.C." runat="server" Enabled ="false"></asp:TextBox>
            <h4><i class="fa fa-check-square-o" aria-hidden="true"></i>&nbsp;Clave </h4>
            <asp:TextBox ID="txtcve" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" CssClass="form-control" placeholder="Clave" runat="server" Enabled="false"></asp:TextBox>
              
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:LinkButton ID="lnkcomentario" Visible="true" CssClass="btn btn-primary btn-block" runat="server"  OnClick="lnkNuevo_Click">Nuevo</asp:LinkButton>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:LinkButton ID="lnkcerrar" CssClass="btn btn-danger btn-block" runat="server" OnClick="btnCancelarTodo_Click">Cerrar</asp:LinkButton>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">            
                <div class="panel-heading" style="text-align: center">
                    <h3 class="panel-title">LISTA <i class="fa fa-list"></i></h3>
                </div>
                <div class="panel-body" >
                    <h3 id="NO_Hay" runat="server" visible="false" style="text-align: center;">NO HAY DATOS <i class="fa fa-exclamation-triangle"></i></h3>
                    <div class="table table-responsive" >
                        <asp:GridView ID="gridcompromisos" CssClass="table table-responsive table-bordered gvv {disableSortCols: [3]}" runat="server" DataKeyNames="idc_clicompromiso,fecha,descripcion,usuario,observaciones" AutoGenerateColumns="false" OnRowCommand="gridcompromisos_RowCommand">
                            <Columns>                        
                                <asp:BoundField DataField="idc_clicompromiso"   HeaderText="idc_clicompromiso" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="fecha"               HeaderText="Fecha"          ></asp:BoundField>
                                <asp:BoundField DataField="descripcion"         HeaderText="Descripcion"    ></asp:BoundField>
                                <asp:BoundField DataField="usuario"             HeaderText="Usuario"        ></asp:BoundField>
                                <asp:BoundField DataField="observaciones"       HeaderText="Observaciones"  ></asp:BoundField>
                                <asp:TemplateField HeaderText="Revisar" HeaderStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:Button ID="btnrevisar" CssClass="btn btn-info btn-block" runat="server" Text='Revisar' CommandName="Revisar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!------------------------- MODAL EDICION ------------------------------------>
    <div class="modal fade modal-info" id="myModalEditar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 id="ModalEditar_title"><strong>Sistema de edicion</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" >
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 " >
                                    <div style="text-align: center;">
                                    <h4>
                                        <label id="content_ModalEditar"></label>
                                    </h4>
                                    </div>
                                    <!-- contenido => nuevo compromiso -->
                                    <div runat="server" id="Contenido_NuevoCompromiso" visible ="false">
                                        <h4><i class="fa fa-university" aria-hidden="true"></i>&nbsp;Tipo de Compromiso</h4>
                                        <asp:DropDownList ID="ddlT_Compromisos" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <h4><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Compromiso </h4>
                                        <asp:TextBox Style="text-transform: uppercase;resize:none;" onfocus="$(this).select();" 
                                                ID="txtcompromiso" runat="server" TextMode="Multiline" Rows="2" 
                                            CssClass="form-control"  placeholder="Descripcion" MaxLength ="250"
                                            onblur="return imposeMaxLength(this, 250);" >
                                        </asp:TextBox>
                                    </div>                            
                                    <!-- fin contenido => nuevo compromiso --> 
                                    <!-- contenido => Revisar compromiso -->
                                    <div runat="server" id="contenido_RevisarCompromiso" visible ="false">
                                        <h5><i class="fa fa-server" aria-hidden="true"></i>&nbsp;Descripcion</h5>
                                        <asp:TextBox ID="txtDescripcion" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                
                                        <h5><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</h5>
                                        <asp:TextBox ID="txtFecha" onfocus="$(this).select();"  CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                
                                        <h5><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Usuario</h5>
                                        <asp:TextBox ID="txtUsuario" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                
                                        <h5><i class="fa fa-check-square-o" aria-hidden="true"></i>&nbsp;Cumplido</h5>                                
                                        <asp:DropDownList ID="ddlCumplido" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">&nbsp;No </asp:ListItem>
                                            <asp:ListItem Value="1">&nbsp;Si</asp:ListItem>
                                        </asp:DropDownList>

                                        <h5><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Observaciones</h5>
                                        <asp:TextBox ID="txtObservaciones" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" CssClass="form-control" placeholder="Observaciones" runat="server"
                                            Style="text-transform: uppercase;resize:none;" TextMode="Multiline" Rows="2" MaxLength ="250" >
                                        </asp:TextBox>
                                    </div>
                                    <!-- fin contenido => Revisar compromiso -->                                   
                                </div>
                            </div>
                        </div>

                        <!-- BOTONES -->
                        <div class="modal-footer">
                            <div class="col-lg-6 col-xs-6">
                                <asp:Button ID="btnGuardar" class="btn btn-info btn-block" runat="server" Text="Guardar" OnClick ="guardar_click"/>
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input  id="btnCancelar" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                            </div>
                        </div>
                    </div>
                
        </div>
    </div>
    <!------------------------- FIN MODAL EDICION -------------------------------->

</asp:Content>
