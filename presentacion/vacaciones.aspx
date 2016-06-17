<%@ Page Title="Solicitud de Vacaciones" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="vacaciones.aspx.cs" Inherits="presentacion.vacaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
        function ValidateRange2(Object, val_min, val_max, error_mess) {
            // It's a number

            if (Object.value != "") {
                numValue = parseFloat(Object.value);
                min = parseFloat(val_min);
                max = parseFloat(val_max);
                if (numValue < min || numValue > max) {
                    Object.value = "";
                    swal({
                        title: "Mensaje del Sistema",
                        text: error_mess,
                        type: 'error',
                        showCancelButton: false,
                        confirmButtonColor: "#428bca",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false, allowEscapeKey: false
                    });
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Solicitud de Vacaciones</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkaddd" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2" style="align-content: center;">
                    <a>
                        <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 160px; margin: 0 auto;" />
                    </a>
                </div>
                <div class="col-lg-10" style="text-align: left">
                    <div class="form-group">
                        <h4>
                            <strong>Nombre Empleado: </strong>
                            <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                        </h4>
                        <h4><strong>Puesto: </strong>
                            <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                        </h4>
                        <h4><strong>Departamento: </strong>
                            <asp:Label ID="lbldepto" runat="server" Text=""></asp:Label>
                        </h4>
                        <h4><strong>Dias Disponibles para Vacaciones: </strong>
                            <asp:Label ID="lbldiasdisp" runat="server" Text=""></asp:Label>
                        </h4>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-success fresh-color">
                        <div class="panel-heading"></div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h4><strong><i class="fa fa-check" aria-hidden="true"></i>&nbsp;Tomadas</strong></h4>
                                    <asp:TextBox ID="txttomadas" AutoPostBack="true" OnTextChanged="txttomadas_TextChanged" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h4><strong><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Pagadas</strong></h4>
                                    <asp:TextBox ID="txtpagadas" AutoPostBack="true" OnTextChanged="txttomadas_TextChanged" onkeypress="return validarEnteros(event);" onfocus="$(this).select();" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h4><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Agregar Fechas Vacaciones Tomadas</strong></h4>
                                    <asp:TextBox ID="txtfecha" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lnkaddd" CssClass="btn btn-info btn-block" runat="server" OnClick="lnkaddd_Click">Agregar <i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>

                                    <div class="table table-responsive">
                                        <asp:GridView ID="gridfechas" DataKeyNames="fecha" CssClass="table table-responsive table-bordered" OnRowCommand="gridfechas_RowCommand" AutoGenerateColumns="false" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Borrar">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderStyle Width="30px"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text="" ImageUrl="~/imagenes/btn/icon_delete.png" CommandName="deletegpolibre"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="fecha_texto" HeaderText="Fecha"></asp:BoundField>
                                                <asp:BoundField DataField="fecha" HeaderText="fec" Visible="False"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
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
    <!-- Modal -->
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