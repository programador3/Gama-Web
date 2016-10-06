<%@ Page title="puestos" MasterPageFile="~/Global.Master" Language="C#" AutoEventWireup="true" CodeBehind="puestos_equi.aspx.cs" Inherits="presentacion.puestos_equi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">
        function ModalClose() {
            $('#myModal').modal('hide');
            $('#myModalGrid').modal('hide');
            $('#myModalEditar').modal('hide');
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

        function ModalEditar(cTitulo,cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalEditar').modal('show');
            $('#myModalEditar').removeClass('modal fade modal-info');
            $('#myModalEditar').addClass(ctype);
            $('#ModalEditar_title').text(cTitulo);
            $('#content_ModalEditar').text(cContenido);
        }
        //Grid
        function ModalGrid(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            cContenido = cContenido.replace("----","");
            $('#myModalGrid').modal('show');
            $('#myModalGrid').removeClass('modal fade modal-info');
            $('#myModalGrid').addClass(ctype);
            $('#ModalGrid_title').text(cTitulo);
            $('#content_ModalGrid').text(cContenido);
        }
       

        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });

            
        });
    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
   
    <div id="page-wrapper">
        <div class="container-fluid">
        
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Puestos Equivalentes</h1>
                </div>
            </div>

                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <asp:Button runat="server" CssClass="btn btn-info btn-block" Text="Nuevo" ID="btnNuevo" OnClick="btnNuevo_Click" />
                    </div>
                   
                </div>
            

            <!------------------------- Grid y elemntos --------------------------------->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">            
                        <div class="panel-heading" style="text-align: center">
                            <h3 class="panel-title">LISTA <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <h3 id="NO_Hay" runat="server" visible="false" style="text-align: center;">NO HAY DATOS <i class="fa fa-exclamation-triangle"></i></h3>
                            <div class="table-responsive">
                                <asp:GridView ID="gridEquivalente" CssClass="table table-responsive table-condensed gvv {disableSortCols: [3]}"  AutoGenerateColumns="false" runat="server" DataKeyNames="pidc_puestoequi,pdescripcion" OnRowCommand="gridAsignacion_RowCommand" >
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image"  HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image"  HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Eliminar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>

                                        <asp:ButtonField ButtonType="Image"  HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_comparar.png" HeaderText="puestos relacionados" CommandName="puestos_relacionados">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>

                                        <%--<asp:TemplateField HeaderText="puestos relacionados" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <asp:LinkButton ID="lbtn" CommandName="puestos_relacionados" runat="server">puestos relacionados</asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:BoundField DataField="pidc_puestoequi" Visible="false"></asp:BoundField>
                                        
                                        <asp:BoundField DataField="pdescripcion"  HeaderText="Descripcion Puesto Equivalente"></asp:BoundField>                        
                                    
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!------------------------- FIN Grid y elemntos ----------------------------->


            <!------------------------- MODAL PRINCIPAL --------------------------------->
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
                                    <asp:TextBox ID="txtpbservaciones" placeholder="Observaciones" Visible="false" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-6 col-xs-6">
                                <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Guardar_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input  id="No" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!------------------------- FIN MODAL PRINCIPAL ------------------------------>

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
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <div style="text-align: center;">
                                    <h4>
                                        <label id="content_ModalEditar"></label>
                                    </h4>
                                    </div>
                                    <h4>
                                        <strong>Descripcion</strong>
                                    </h4>
                                    <asp:TextBox Style="text-transform: uppercase;resize:none;" onfocus="$(this).select();" 
                                            ID="txtDescripcion" runat="server" TextMode="Multiline" Rows="2" CssClass="form-control" 
                                            AutoPostBack="true" placeholder="Descripcion"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-6 col-xs-6">
                                <asp:Button ID="btnGuardar" class="btn btn-info btn-block" runat="server" Text="Guardar" OnClick="Guardar_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input  id="btnCancelar" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!------------------------- FIN MODAL EDICION -------------------------------->

            <!------------------------- MODAL GRID ------------------------------------>
            <div class="modal fade modal-info" id="myModalGrid" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 id="ModalGrid_title"><strong>Sistema de edicion</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" >
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <div style="text-align: center;">
                                    <h4>
                                        <label id="content_ModalGrid"></label>
                                    </h4>
                                    </div>
                                    <!-- -->
                                    <div class="panel panel-primary">            
                                        <div class="panel-heading" style="text-align: center">
                                            <h3 class="panel-title">PUESTOS RELACIONADOS<i class="fa fa-list"></i></h3>
                                        </div>
                                        <div class="panel-body">
                                            <h3 id="VacioGrid_modal" runat="server" visible="false" style="text-align: center;">NO HAY DATOS <i class="fa fa-exclamation-triangle"></i></h3>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridPuestoRelacionado" CssClass="table table-responsive table-condensed gvv "  AutoGenerateColumns="false" runat="server"   >
                                                    <Columns>
                                                           
                                                        <asp:BoundField DataField="idc_puesto" HeaderText="Puesto"></asp:BoundField>
                                        
                                                        <asp:BoundField DataField="descripcion"  HeaderText="Puesto Relacionado"></asp:BoundField>                        
                                    
                                        
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                    
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%--<div class="col-lg-6 col-xs-6">
                                <asp:Button ID="Button1" class="btn btn-info btn-block" runat="server" Text="Guardar" OnClick="Guardar_Click" />
                            </div>--%>
                            <div class="col-lg-12">
                                <input  id="btnCerrar" type="button" class="btn btn-danger btn-block" onclick="ModalClose(this);" value="Cerrar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!------------------------- FIN MODAL EDICION -------------------------------->
        </div>
    </div>
</asp:Content>
