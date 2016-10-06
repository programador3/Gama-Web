<%@ Page Title="Sucursales" MasterPageFile="~/Global.Master" Language="C#" AutoEventWireup="true" CodeBehind="Area_Trabajo.aspx.cs" Inherits="presentacion.Area_Trabajo" %>

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
        }
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });


        });
    </script>    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">ÁREAS DE TRABAJO</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlSucursal" />            
        </Triggers>
        <%-- <div class="dropdown">
  <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">Dropdown Example
  <span class="caret"></span></button>
  <ul class="dropdown-menu">
    <li><a href="#">HTML</a></li>
    <li><a href="#">CSS</a></li>
    <li><a href="#">JavaScript</a></li>
  </ul>
</div> --%>
        <ContentTemplate>
            <div class="row" runat="server" id="FILTRO">
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <h4><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Lista de Sucursales</strong></h4>
                    <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-info btn-block" OnClick="btnNuevo_Click" />
                </div>                
            </div>

           
        </ContentTemplate>
    </asp:UpdatePanel>
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
                       
                        <asp:GridView ID="gridRelacionAreaTrabajo" CssClass="table table-responsive table-condensed gvv {disableSortCols: [3]}" AutoGenerateColumns="false" runat="server" 
                            DataKeyNames="idc_area,nombre,activo" OnRowCommand="gridAsignacion_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" HeaderStyle-Width="40px" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:TemplateField HeaderText="Activo" HeaderStyle-Width="40px ">
                                    <ItemTemplate>
                                        <asp:Image ID="Image2" ImageUrl='<%#Convert.ToBoolean(Eval("activo"))==true?"~/imagenes/btn/checked.png":"~/imagenes/btn/inchecked.png" %>' runat="server" Width ="40px" Height="40px" ImageAlign="Middle"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField Visible="false"       DataField="idc_area">   </asp:BoundField>
                                <asp:BoundField HeaderText="Nombre"   DataField="nombre" >     </asp:BoundField>
                                <%--<asp:BoundField HeaderText="Activo"   DataField="activo" >     </asp:BoundField>--%>
                                
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
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <div style="text-align: center;">
                                <h4>
                                    <label id="content_ModalEditar"></label>
                                </h4>
                            </div>
                            <h4>
                                <strong>Descripcion</strong>
                            </h4>
                            <asp:TextBox Style="text-transform: uppercase; resize: none;" 
                                ID="txtDescripcion" runat="server" TextMode="Multiline" Rows="2"
                                CssClass="form-control" placeholder="Descripcion" MaxLength="250"
                                onblur="return imposeMaxLength(this, 250);"></asp:TextBox>
                            
                            <h4>
                                <asp:CheckBox ID="idActivo" runat="server"  Text ="Activo &nbsp;&nbsp;&nbsp;" TextAlign="left" Checked="true" Font-Size="18px" CssClass="checked" />
                            </h4>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="btnGuardar" class="btn btn-info btn-block" runat="server" Text="Guardar" OnClick="Guardar_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="btnCancelar" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!------------------------- FIN MODAL EDICION -------------------------------->
    
</asp:Content>
