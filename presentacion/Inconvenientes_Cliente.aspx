<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="Inconvenientes_Cliente.aspx.cs" Inherits="presentacion.Inconvenientes_Cliente" %>
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
    <h2 class="page-header" style="text-align: center;">Inconvenientes del Cliente</h2>
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
                        <asp:GridView ID="gridinconv" CssClass="table table-responsive table-bordered gvv {disableSortCols: [3]}"  AutoGenerateColumns="false"
                            runat="server">
                            <Columns>                        
                                <asp:BoundField DataField="fecha"               HeaderText="Fecha"          ></asp:BoundField>
                                <asp:BoundField DataField="usuario"             HeaderText="Usuario"        ></asp:BoundField>
                                <asp:BoundField DataField="Inconveniente"       HeaderText="Inconveniente"  ></asp:BoundField>                                
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
                                    <div runat="server" id="Contenido_NuevoInconveniente" visible ="true">                                        
                                        <h4><i class="fa fa-times-circle-o" aria-hidden="true"></i>&nbsp;Inconveniente </h4>
                                        <asp:TextBox Style="text-transform: uppercase;resize:none;" onfocus="$(this).select();" 
                                                ID="txtInconveniente" runat="server" TextMode="Multiline" Rows="2" 
                                            CssClass="form-control"  placeholder="Inconveniente" MaxLength ="250"
                                            onblur="return imposeMaxLength(this, 250);" >
                                        </asp:TextBox>
                                    </div>                            
                                    <!-- fin contenido => nuevo compromiso --> 
                                                                       
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
