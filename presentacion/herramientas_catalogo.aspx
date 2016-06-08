<%@ Page Title="Herramientas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="herramientas_catalogo.aspx.cs" Inherits="presentacion.herramientas_catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function getImage(path) {
            $("#myImage").attr("src", path);
        }

        function ModalConfirm(cTitulo, cContenido) {
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalPreview() {
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
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
            <div class="page-header">
                <h1>
                    <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                    Herramientas de Trabajo</h1>
            </div>
            <div class="row">
                <div class="col-lg-8" style="margin: 0 auto;">
                    <h3><strong>Datos del Empleado Responsable</strong></h3>
                    <asp:Panel ID="Panel" runat="server">
                        <div class="row">
                            <div class="col-lg-2" style="align-content: center;">
                                <a>
                                    <img id="myImage" class="img-responsive" alt="Gama System" style="width: 100px; margin: 0 auto;" />
                                </a>
                            </div>
                            <div class="col-lg-10">
                                <div class="form-group">
                                    <h4><strong>Puesto:</strong>
                                        <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                                    </h4>
                                </div>
                                <div class="form-group">
                                    <h4><strong>Empleado Actual:</strong>
                                        <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></h4>
                                </div>

                                <div class="form-group">
                                    <h4><strong>Departamento:</strong>
                                        <asp:Label ID="lblDepto" runat="server" Text=""></asp:Label>
                                        <strong>Sucursal:</strong>
                                        <asp:Label ID="lblucursal" runat="server" Text=""></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="col-lg-2">
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-lg-5">
                            <div class="btn-group">

                                <asp:LinkButton ID="lnkActivo" runat="server" class="btn btn-primary active" OnClick="btnActivo_Click">Herramientas <i class="fa fa-cubes"></i></asp:LinkButton>

                                <asp:LinkButton ID="lnkAuto" runat="server" class="btn btn-link" OnClick="btnVehiculo_Click">Vehiculos <i class="fa fa-car"></i></asp:LinkButton>

                                <asp:LinkButton ID="lnkCelular" runat="server" class="btn btn-link" OnClick="btnCelular_Click">Celulares <i class="fa fa-mobile"></i></asp:LinkButton>
                            </div>
                            <div class="btn-group">
                                <asp:LinkButton runat="server" class="btn btn-primary" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                           Guardar Todo <i class="fa fa-floppy-o"></i>
                                </asp:LinkButton>
                                <ul class="dropdown-menu">
                                    <li><a href="#">
                                        <asp:LinkButton ID="lnkGuardarTodo" runat="server" OnClick="lnkGuardarTodo_Click">
                                            <asp:Image ID="Image2" runat="server" Height="25" Width="25" ImageUrl="~/imagenes/btn/excel.png" />
                                            Excel 2007
                                        </asp:LinkButton></a></li>
                                    <li><a href="#">
                                        <asp:LinkButton ID="lnkPDF" runat="server" OnClick="lnkPDF_Click">
                                            <asp:Image ID="Image3" runat="server" Height="25" Width="25" ImageUrl="~/imagenes/btn/pdf.png" />
                                            PDF
                                        </asp:LinkButton></a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-lg-7">
                            <h2 style="text-align: left;">
                                <asp:Label ID="lblSelectedHerramientas" runat="server" Text="Herramientas <i class='fa fa-cubes'></i>"></asp:Label>
                            </h2>
                        </div>
                    </div>

                    <%--   PANEL ACTIVOS--%>
                    <asp:Panel ID="PanelActivos" runat="server" class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Listado de Herramientas <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="PanelSinHerramientas" runat="server" Visible="false">
                                <h2 style="text-align: center;">Este Puesto no tiene Herramientas asignadas <i class="fa fa-exclamation-triangle"></i></h2>
                            </asp:Panel>
                            <asp:Panel ID="PanelConHerramientas" runat="server">
                                <div class="form-group">
                                    <!-- Single button -->
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-primary" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <i class="fa fa-floppy-o"></i>Guardar Tabla <span class="caret"></span>
                                        </button>
                                        <ul class="dropdown-menu">
                                            <li><a href="#">
                                                <asp:LinkButton ID="lnkHeraamientasExport" runat="server" OnClick="lnkHeraamientasExport_Click">
                                                    <asp:Image ID="Image1" runat="server" Height="25" Width="25" ImageUrl="~/imagenes/btn/excel.png" />
                                                    Excel 2007
                                                </asp:LinkButton></a></li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="table table-responsive">
                                    <asp:GridView ID="gridHerramientas" runat="server" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [6]}" AutoGenerateColumns="False" DataKeyNames="idc_actscategoria,folio,subcat" OnRowCommand="gridHerramientas_RowCommand" OnRowDataBound="gridHerramientas_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditText="" HeaderText="Editar" SelectImageUrl="~/imagenes/btn/icon_editar.png" SelectText="" ShowSelectButton="True">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Eliminar">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="idc_activo" HeaderText="idc_activo" Visible="False" />
                                            <asp:ButtonField DataTextField="folio" HeaderText="Folio" CommandName="Ver Activo" />
                                            <asp:ButtonField DataTextField="subcat" HeaderText="Herramienta" CommandName="Ver Activo" />
                                            <asp:BoundField DataField="idc_actscategoria" HeaderText="idc_actscategoria" Visible="False" />
                                            <asp:BoundField DataField="cat" HeaderText="Categoria" />
                                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                                            <asp:BoundField DataField="idc_areaact" HeaderText="idc_areaact" Visible="False" />
                                            <asp:TemplateField HeaderText="Area Comun">
                                                <ItemTemplate>
                                                    <asp:Image ID="area_comun" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <%--   PANEL VEHICULOS--%>
            <div class="row">
                <div class="col-lg-12">
                    <asp:Panel ID="PanelVehiculos" runat="server" CssClass="panel panel-primary" Visible="false">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Listado de Vehiculos <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="PanelSInVehiculo" runat="server" Visible="false">
                                <h2 style="text-align: center;">Este Puesto no tiene Vehiculos asignados <i class="fa fa-exclamation-triangle"></i></h2>
                            </asp:Panel>
                            <asp:Panel ID="PanelConVehiculo" runat="server">
                                <asp:Repeater ID="RepeatVehiculos" runat="server" OnItemDataBound="RepeatVehiculos_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-lg-12" style="margin: 0 auto;">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class="row">
                                                        <div class="col-lg-4" style="align-content: center;">
                                                            <a>
                                                                <asp:Image ID="imgVehiculos" runat="server" class="img-responsive" alt="Gama" />
                                                            </a>
                                                        </div>
                                                        <div class="col-lg-8">

                                                            <asp:Panel ID="PanelTemp" runat="server">
                                                                <div class="form-group">
                                                                    <h4 style="text-align: center;"><strong>Detalles del Vehiculo</strong></h4>
                                                                </div>
                                                                <div class="form-group">
                                                                    <h4><strong><i class="fa fa-user fa-fw"></i>Puesto Asignado: </strong>
                                                                        <asp:Label ID="lblPuestoTEMP" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                                    </h4>
                                                                </div>
                                                                <div class="form-group">
                                                                    <h4><strong><i class="fa fa-list-ol fa-fw"></i>Numero Economico: </strong>
                                                                        <asp:Label ID="lblNumeroEc" runat="server" Text='<%#Eval("num_economico") %>'></asp:Label>
                                                                    </h4>
                                                                </div>
                                                                <div class="form-group">
                                                                    <h4><strong><i class="fa fa-car fa-fw"></i>Descripción del Vehiculo: </strong>
                                                                        <asp:Label ID="lblDescripcionVehoculo" runat="server" Text='<%#Eval("descripcion_vehiculo") %>'></asp:Label>
                                                                    </h4>
                                                                </div>
                                                                <div class="form-group">
                                                                    <h4><strong><i class="fa fa-lastfm-square fa-fw"></i>Placas: </strong>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("placas") %>'></asp:Label>
                                                                    </h4>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-4"></div>
                                                                    <div class="col-lg-4">
                                                                        <div class="form-group">

                                                                            <asp:LinkButton ID="lnkVerHVehiculos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkVerHVehiculos_Click">Ver Herramientas <i class="fa fa-arrow-circle-o-right"></i></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <div class="form-group">
                                                                            <asp:LinkButton ID="lnkEditar" runat="server" CssClass="btn btn-success btn-block">Editar Información <i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-10">
                    <asp:Panel ID="PanelHerramientasVehiculo" class="panel panel-green" runat="server" Visible="false">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Herramientas de Vehiculo <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="PanelsinHVehiculo" runat="server" Visible="false">
                                <h2 style="text-align: center;">Este Vehiculo no tiene Herramientas <i class="fa fa-exclamation-triangle"></i></h2>
                            </asp:Panel>
                            <asp:Panel ID="PanelconHVehiculo" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gridHerramientasVehiculo" runat="server" AutoGenerateColumns="false" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [5]}" OnRowCommand="gridHerramientasVehiculo_RowCommand">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditText="" HeaderText="Editar" SelectImageUrl="~/imagenes/btn/icon_editar.png" SelectText="" ShowSelectButton="True">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Eliminar">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="idc_gpo_herramientasd" HeaderText="idc_gpo_herramientasd" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="idc_tipoherramienta" HeaderText="idc_tipoherramienta" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad"></asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Herramienta"></asp:BoundField>
                                            <asp:BoundField DataField="activo" HeaderText="activo" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="tipo" HeaderText="tipo" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="idc_vehiculo" HeaderText="idc_vehiculo" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="costo" HeaderText="Costo"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <%--   PANEL CELULARES--%>
                    <asp:Panel ID="PanelCelulares" runat="server" CssClass="panel panel-primary" Visible="false">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Listado de Celulares y Lineas Asigandas <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="PanelsinCel" runat="server" Visible="false">
                                <h2 style="text-align: center;">Este Puesto no tiene Celulares asigandos <i class="fa fa-exclamation-triangle"></i></h2>
                            </asp:Panel>
                            <asp:Panel ID="PanelConCel" runat="server" Visible="true">
                                <asp:Repeater ID="repeatCelulares" runat="server" OnItemDataBound="repeatCelulares_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-lg-12" style="margin: 0 auto;">
                                                <asp:Panel ID="PanelT1" runat="server">
                                                    <div class="row">
                                                        <div class="col-lg-2" style="align-content: center;">

                                                            <a style="align-content: center;">
                                                                <asp:Image ID="imgCel" runat="server" class="img-responsive" alt="Gama" Style="width: 100px;" />
                                                            </a>
                                                        </div>
                                                        <div class="col-lg-5">
                                                            <div class="form-group">
                                                                <div class="form-group">
                                                                    <h5 style="text-align: center;"><strong>Detalles del Celular o Linea</strong></h5>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <h5><i class="fa fa-phone-square fa-fw"></i><strong>Linea: </strong>
                                                                    <asp:Label ID="linea" runat="server" Text='<%#Eval("telefono") %>'></asp:Label>
                                                                    &nbsp; &nbsp; &nbsp;&nbsp;   <i class="fa fa-phone fa-fw"></i><strong>Marcación Corta:</strong>
                                                                    <asp:Label ID="marca" runat="server" Text='<%#Eval("mar_corta") %>'></asp:Label>
                                                                </h5>
                                                            </div>
                                                            <div class="form-group">
                                                                <h5><strong><i class="fa fa-mobile fa-fw"></i>Descripción del Equipo:</strong>
                                                                    <asp:Label ID="descri" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                                </h5>
                                                            </div>
                                                            <div class="form-group">
                                                                <h5><strong><i class="fa fa-usd fa-fw"></i>Costo del Equipo:  $</strong>
                                                                    <asp:Label ID="costo" runat="server" Text='<%#Eval("costo") %>'></asp:Label>
                                                                </h5>
                                                            </div>

                                                            <div class="form-group">
                                                                <asp:LinkButton ID="lnkEditarCel" runat="server" CssClass="btn btn-success">Editar Información <i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-5">
                                                            <div class="form-group">
                                                                <h5 style="text-align: center;"><strong>Accesorios del equipo</strong>
                                                                </h5>
                                                            </div>
                                                            <asp:Panel ID="PanelconAccesorios" runat="server">
                                                                <asp:Repeater ID="repeatAccesorios" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="form-group">
                                                                            <h5><strong><i class="fa fa-info-circle fa-fw"></i>Descripcion: </strong>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                                                                &nbsp;&nbsp;<strong> Precio:  $</strong>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("costo") %>'></asp:Label>
                                                                            </h5>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                            <asp:Panel ID="PanelsinAccesorios" runat="server">
                                                                <h3 style="text-align: center;">Esta linea no cuenta con Equipo Celular <i class="fa fa-exclamation-triangle"></i></h3>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
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
                                <div class="col-lg-6 col-md-4"></div>
                                <div class="col-lg-3 col-md-4 col-sm-4 col-xs-6">
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" />
                                </div>
                                <div class="col-lg-3 col-md-4 col-sm-4 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalPreviewView" class="modal fade bs-example-modal-md" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                            <h4>Detalles de Herramienta/Activo con Folio:
                          <asp:Label ID="lblMDetalles" runat="server" Text=""></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <h4 style="text-align: center;"><strong>
                                        <asp:Label ID="lblMSubcat" runat="server" Text=""></asp:Label></strong></h4>
                                    <asp:Repeater ID="gridHerramientasDetalles" runat="server">
                                        <ItemTemplate>
                                            <asp:Panel ID="Panel" runat="server">
                                                <h5><i class="fa fa-caret-right"></i><strong>
                                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label></strong>
                                                    &nbsp;<asp:Label ID="lblValor" runat="server" Text='<%#Eval("valor") %>'></asp:Label>
                                                    &nbsp;<asp:Label ID="lblObs" runat="server" Text='<%#Eval("observaciones") %>'></asp:Label>
                                                </h5>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-5">
                                </div>
                                <div class="col-lg-3">
                                    <input id="btnModalAcept" class="btn btn-primary btn-block" value="Aceptar" onclick="ModalClose();" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>