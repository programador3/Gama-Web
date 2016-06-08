<%@ Page Title="Bajas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="pre_bajas_bajas.aspx.cs" Inherits="presentacion.pre_bajas_bajas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var downloadURL = function downloadURL(url) {
            var hiddenIFrameID = 'hiddenDownloader',
                iframe = document.getElementById(hiddenIFrameID);
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                iframe.style.display = 'none';
                document.body.appendChild(iframe);
            }
            iframe.src = url;
        };
        function GoSection(URL) {
            $('html,body').animate({
                scrollTop: $(URL).offset().top
            }, 500);
        }
        function AlertGO(TextMess, URL, typealert) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: typealert,
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
        function ModalConfirm(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
        function ModalPreBaja() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');
        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
            $('#myModal').modal('hide');
        }
        function getImage(path) {
            $("#myImage").attr("src", path);
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
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" Visible="false" runat="server" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Bajas Pendientes</h1>
                </div>
            </div>
            <div class="row" runat="server" id="row_grid">
                <div class="col-lg-12">
                    <div class="form-group">
                        <div class="panel panel-primary">
                            <div class="panel-heading" style="text-align: center;">
                                <h4>Bajas Pendientes <i class="fa fa-list"></i></h4>
                            </div>
                            <div class="panel-body">
                                <div class="table table-responsive">
                                    <h2 id="Noempleados" runat="server" visible="false" style="text-align: center;">No hay bajas pendientes<i class="fa fa-exclamation-triangle"></i></h2>

                                    <asp:GridView ID="gridEmpleados" runat="server" CssClass="gvv table table-bordered table-hover table-condensed grid sortable {disableSortCols: [3]}" AutoGenerateColumns="False" DataKeyNames="num_nomina,empleado,descripcion,motivo,apto_reingreso,honesto,drogas,trabajador,alcohol,especificar,carta_recomendacion,contratar,fecha_baja, idc_empleado, idc_puesto, idc_prebaja, idc_cap,renuncia" OnRowCommand="gridEmpleados_RowCommand">
                                        <Columns>

                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/btn_solicitar_equipos.png" HeaderText="Terminar Baja" CommandName="Solicitar">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="num_nomina" HeaderText="idc_empleado" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="empleado" HeaderText="Empleado"></asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Puesto"></asp:BoundField>
                                            <asp:BoundField DataField="motivo" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="apto_reingreso" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="honesto" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="drogas" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="trabajador" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="alcohol" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="especificar" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="carta_recomendacion" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="contratar" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="fecha_baja" HeaderText="no" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="renuncia" HeaderText="no" Visible="false"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-2"></div>
                <div class="col-lg-8 col-md-12">
                    <asp:Panel ID="PanelPreBaja" runat="server" CssClass="form-group" Visible="false"
                        Style="background-color: #f5f5f5; border-radius: 24px 24px 24px 24px; -moz-border-radius: 24px 24px 24px 24px; -webkit-border-radius: 24px 24px 24px 24px; border: 1px inset #000000; -webkit-box-shadow: 10px 10px 49px -6px rgba(0,0,0,0.75); -moz-box-shadow: 10px 10px 49px -6px rgba(0,0,0,0.75); box-shadow: 10px 10px 49px -6px rgba(0,0,0,0.75);">
                        <h3 style="text-align: center;">Información de la Baja Pendiente <i class="fa fa-users"></i>
                        </h3>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkAptoReingresoSI" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkAptoReingresoNO" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkVacanteC" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkVacanteNO" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="txtcheque" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlCheque" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h5><strong><i class="fa fa-user"></i>Empleado: </strong>
                                                <asp:Label ID="lblEmpleadoName" runat="server" Text=" Empleado"></asp:Label></h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h5><strong><i class="fa fa-suitcase"></i>Puesto: </strong>
                                                <asp:Label ID="lblPuesto" runat="server" Text="Puesto"></asp:Label>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <h5><strong><i class="fa fa-calendar-o"></i>Fecha de Baja: </strong>
                                                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12">
                                        <asp:Label ID="lblErrorMotivo" runat="server" Text="Debe indicar el Motivo" Style="color: red; font: bold;" Visible="false"></asp:Label>

                                        <div class="form-group">
                                            <h5>
                                                <asp:TextBox ID="txtMotivo" runat="server" ReadOnly="true" CssClass="form-control" TextMode="MultiLine" MaxLength="250" Rows="3" Style="resize: none;" placeholder="Motivo de Baja"></asp:TextBox>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblAptoReingreso" runat="server" Text="0" Visible="false"></asp:Label>
                                            <h5><strong><i class="fa fa-arrow-left"></i>¿Apto para Reingreso? </strong>
                                                <div class="btn-group" style="background-color: white;">
                                                    <asp:LinkButton ID="lnkAptoReingresoSI" runat="server" class="btn btn-primary active" OnClick="lnkAptoReingresoSI_Click" Style="text-decoration: none; border: 1px solid #000000;">SI </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkAptoReingresoNO" runat="server" class="btn btn-link" OnClick="lnkAptoReingresoNO_Click" Style="text-decoration: none; border: 1px solid #000000;">NO  </asp:LinkButton>
                                                </div>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-10">
                                        <div class="form-group">
                                            <h5 style="background-color: white;">

                                                <asp:CheckBox ID="cbxHonesto" runat="server" Text="Honesto" Style="" Enabled="false" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:CheckBox ID="cbxTrabajador" runat="server" Text="Trabajador" Enabled="false" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:CheckBox ID="cbxDrogas" runat="server" Text="Drogas" Enabled="false" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:CheckBox ID="cbxAlcol" runat="server" Text="Alcohol" Enabled="false" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:CheckBox ID="cbxRobo" runat="server" Text="Robo" Enabled="false" /></h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtEspecificar" ReadOnly="true" runat="server" CssClass="form-control" placeholder="Especificar" TextMode="MultiLine" Rows="3" MaxLength="250" Style="resize: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="form-group">
                                            <h5>
                                                <div class="input-group">
                                                    <span class="input-group-addon" style="color: #fff; background-color: #367fa9;">
                                                        <i class="fa fa-file-text-o"></i></span>

                                                    <asp:CheckBox ID="cbxCartaRec" Enabled="false" runat="server" Text="Autorizo Carta de Recomendación" />
                                                </div>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <h5><strong><i class="fa fa-thumbs-o-up"></i>Status del Puesto: Vacante </strong>
                                                <div class="btn-group" style="background-color: white;">
                                                    <asp:LinkButton ID="lnkVacanteC" runat="server" class="btn btn-primary active" Style="text-decoration: none; border: 1px solid #000000;" OnClick="lnkVacanteC_Click">Contratar </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkVacanteNO" runat="server" class="btn btn-link" Style="text-decoration: none; border: 1px solid #000000;" OnClick="lnkVacanteNO_Click">No Contratar </asp:LinkButton>
                                                </div>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label><strong>Liquidar</strong></label>
                                            <h5>
                                                <asp:DropDownList ID="ddlCheque" runat="server" OnSelectedIndexChanged="rdbCheque_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                    <asp:ListItem Text="SI" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="NO" Value="0" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </h5>
                                        </div>
                                    </div>
                                    <asp:Panel ID="panel_cheque" runat="server" Visible="false">
                                        <div class="col-lg-8 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label><strong>.</strong></label>
                                                <h5>
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="color: #fff; background-color: #367fa9;">Cheque</span>
                                                        <asp:TextBox ID="txtcheque" TextMode="Number" MaxLength="15" runat="server" CssClass="form-control" placeholder="Numero de Cheque" AutoPostBack="true" OnTextChanged="txtcheque_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblerrorcheque" runat="server" Text="FOLIO DE CHEQUE INCORRECTO" Visible="false" CssClass="label label-danger"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                                        <span class="input-group-addon" style="color: #fff; background-color: #3c8dbc;">
                                            <asp:LinkButton ID="lnkGuardarPape" Style="color: #fff; background-color: #3c8dbc;" OnClick="lnkGuardarPape_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="nombre, ruta, extension">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="extension" HeaderText="Extension" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre Documento"></asp:BoundField>
                                            <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <h5><strong><i class="fa fa-thumbs-o-up"></i>Tipo de Baja </strong>
                                        <div class="btn-group" style="background-color: white;">
                                            <asp:DropDownList ID="ddlTipoBaja" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="SIN" Text="Selecciona uno por favor" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Renuncia"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Despido"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </h5>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-xs-12">
                                <div class="form-group">
                                    <asp:Button ID="btnACeptarPrebaja" runat="server" Text="Baja" CssClass="btn btn-primary btn-block" OnClick="btnACeptarPrebaja_Click" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-xs-12">
                                <div class="form-group">
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
                                </div>
                            </div>

                            <div class="col-lg-6"></div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="col-lg-2"></div>
            </div>

            <%--   MODALES--%>
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