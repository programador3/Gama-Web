<%@ Page Title="Herramientas Pendientes" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="herramientas_revision.aspx.cs" Inherits="presentacion.herramientas_revision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function GoSection(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 500);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: "success",
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                allowEscapeKey: false
            },
               function () {
                   location.href = URL;
               });
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
        }

        function ModalConfirm(cTitulo, cContenido) {
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        } ModalPreviewHeramienta
        function ModalPreviewHeramienta() {
            $('#modalPreviewView').modal('show');
        }
        function ModalPreview(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 1000);
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
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Revisión de Herramientas Pendientes</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8" style="margin: 0 auto;">
                    <h4><strong>Datos del Empleado en Proceso de Pre-Baja</strong></h4>
                    <asp:Panel ID="Panel" runat="server">
                        <div class="row">
                            <div class="col-lg-2" style="align-content: center;">
                                <a>
                                    <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 190px; margin: 0 auto;" />
                                </a>
                            </div>
                            <div class="col-lg-10">
                                <div class="form-group">
                                    <h5><strong>Puesto: </strong>
                                        <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                                        &nbsp;&nbsp;<strong>Numero de Nomina:</strong>
                                        <asp:Label ID="lblnomina" runat="server" Text=""></asp:Label>
                                    </h5>
                                </div>
                                <div class="form-group">
                                    <h5><strong>Empleado Actual: </strong>
                                        <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></h5>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <strong>Motivo: </strong>
                                        <asp:Label ID="lblmotivo" runat="server" Text=""></asp:Label>
                                    </h5>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="col-lg-2">
                </div>
            </div>
            <div class="row" runat="server" id="Selecte" visible="false">
                <div class="col-lg-4">
                    <div class="btn-group">
                        <asp:LinkButton ID="lnkDetalles" runat="server" class="btn btn-primary active" OnClick="lnkDetalles_Click">Detalles <i class="fa fa-list-alt"></i></asp:LinkButton>

                        <asp:LinkButton ID="lnkRevision" runat="server" class="btn btn-link" OnClick="lnkRevision_Click">Revisión <i class="fa fa-check-square-o"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="col-lg-8">
                    <h2 style="text-align: left;">
                        <asp:Label ID="lblSelectedHerramientas" runat="server" Text="Detalles de Herramientas <i class='fa fa-list-alt'></i>"></asp:Label>
                    </h2>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:Panel ID="PanelActivos" runat="server" class="panel panel-primary" Visible="false">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Listado de Herramientas <i class="fa fa-list"></i></h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="PanelConHerramientas" runat="server">

                                <div class="table table-responsive">
                                    <asp:GridView ID="gridHerramientas" runat="server" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [6]}" AutoGenerateColumns="False" DataKeyNames="idc_actscategoria,folio,subcat" OnRowCommand="gridHerramientas_RowCommand">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/more.png" HeaderText="Ver" CommandName="Ver">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="idc_activo" HeaderText="idc_activo" Visible="False" />
                                            <asp:BoundField DataField="folio" HeaderText="Folio" />
                                            <asp:BoundField DataField="subcat" HeaderText="Herramienta" />
                                            <asp:BoundField DataField="idc_actscategoria" HeaderText="idc_actscategoria" Visible="False" />
                                            <asp:BoundField DataField="cat" HeaderText="Categoria" />
                                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                                            <asp:BoundField DataField="area" HeaderText="Area" />
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

                    <asp:Panel ID="PanelRevisionActivos" runat="server" class="panel panel-info fresh-color" Visible="true">
                        <div class="panel-heading" style="text-align: center;">
                            <h3 class="panel-title">Revision de Herramientas <i class="fa fa-check-square-o"></i></h3>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-7 col-xs-12" style="text-align: center;">
                                            <h4><strong>Herramienta <i class="fa fa-wrench"></i></strong></h4>
                                        </div>
                                        <div class="col-lg-3 col-xs-12" style="text-align: center;">
                                            <h5><strong>Entrego </strong>
                                                <asp:LinkButton ID="lbkseltodo" OnClick="lbkseltodo_Click" runat="server" CssClass="btn btn-primary btn-xs">Seleccionar Todo  <i class="fa fa-check-square-o"></i></asp:LinkButton>

                                                <asp:LinkButton ID="lnkDes" OnClick="lbkdestodo_Click" runat="server" CssClass="btn btn-primary btn-xs" Visible="false">Deseleccionar Todo  <i class="fa fa-check-square-o"></i></asp:LinkButton>
                                            </h5>
                                        </div>
                                        <div class="col-lg-2 col-xs-12" style="text-align: center;">
                                            <h4><strong>Costo <i class="fa fa-usd"></i></strong></h4>
                                        </div>
                                    </div>

                                    <asp:Repeater ID="repeatRevision" runat="server" OnItemDataBound="repeatRevision_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtMoney" EventName="TextChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <div class="row" style="border: 1px solid gray; padding-top: 7px">
                                                        <div class="col-lg-6 col-md-4 col-sm-12 col-xs-12">

                                                            <asp:Label ID="lblactivo" runat="server" Text="Label" Visible="false"></asp:Label>
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                    <asp:LinkButton ID="lnkDetallesHerrRevision" Style="color: #fff; background-color: #337ab7;" runat="server"
                                                                         OnClientClick="return false;"
                                                                         OnClick="lnkDetallesHerrRevision_Click"><i class="fa fa-wrench"></i></asp:LinkButton></span>
                                                                <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("subcat") %>'></asp:TextBox>
                                                                <asp:Label ID="LBLFECHA_REV" runat="server" Text='<%#Eval("fecha") %>' Visible="false"></asp:Label>
                                                            </div>

                                                        </div>
                                                        <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">

                                                            <asp:Label ID="lblerrorfolio" runat="server" CssClass="label label-danger" Text="" Visible="false"></asp:Label></strong>
                                                             <asp:Label ID="lblfolioactv" runat="server" Visible="false" Text='<%#Eval("folio") %>'></asp:Label></strong>
                                                         
                                                                <asp:TextBox ID="txtFolio" AutoPostBack="true" runat="server" MaxLength="10" TextMode="Number" OnTextChanged="txtFolio_TextChanged" CssClass="form-control input-group-sm " placeholder="Folio"></asp:TextBox>

                                                            <asp:Label ID="lblfoliocorrecto" runat="server" Text="0" Style="color: red; font: bold;" Visible="false"></asp:Label></strong>
                                                           
                                                         
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">

                                                            <asp:CheckBox ID="cbx" runat="server" CssClass="radio3 radio-check radio-info radio-inline" Text="Entrego" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="cbx_CheckedChanged" />


                                                        </div>
                                                        <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">

                                                            <strong>
                                                                <asp:Label ID="lblerror" runat="server" Text="" Style="color: red; font: bold;" Visible="false"></asp:Label></strong>

                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="color: #fff; background-color: #5bc0de;"><i class="fa fa-usd"></i></span>
                                                                <asp:TextBox ID="txtMoney" runat="server" AutoPostBack="true" class="form-control input-sm" Text="0.00" TextMode="Number" OnTextChanged="txtMoney_TextChanged"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-6">
                                        </div>
                                        <div class="col-lg-3">
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" Style="color: white;" OnClick="LinkButton1_Click">Generar Total <i class="fa fa-usd"></i></asp:LinkButton></span>
                                                            <asp:TextBox ID="txtTotal" runat="server" class="form-control input-sm" Text="0.00" AutoPostBack="true" ReadOnly="true" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="row">
                                <div class="col-lg-6 col-xs-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-6 col-xs-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
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
                                <div class="col-lg-12">
                                    <input id="btnModalAcept" class="btn btn-primary btn-block" value="Aceptar" onclick="ModalClose();" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.CONFIRMA -->
                </div>
            </div>
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
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>