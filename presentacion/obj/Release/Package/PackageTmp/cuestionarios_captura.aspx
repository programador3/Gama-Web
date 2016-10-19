<%@ Page Title="Cuestionarios Captura" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cuestionarios_captura.aspx.cs" Inherits="presentacion.cuestionarios_captura" %>

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
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                allowEscapeKey:false
            },
               function () {
                   location.href = URL;
               });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Captura de Cuestionarios</h1>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="Yes" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">

                    <div class="panel panel-info fresh-color">
                        <div class="panel-heading" style="text-align: center">
                            Datos del Cuestionario
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12 col-sm-12">
                                    <label>Nombre de la Plantilla</label>
                                    <asp:TextBox ID="txtnombrecuestiuonario" CssClass="form-control" placeholder="Plantilla" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12"  id="ISID" runat="server" visible="false">
                                    <label>Tipo de Cuestionario</label>
                                    <asp:DropDownList ID="ddltipo_cuestionario" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltipo_cuestionario_SelectedIndexChanged">
                                            </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <label>Pregunta</label>
                                    <asp:TextBox ID="txtpregunta" CssClass="form-control" placeholder="Pregunta" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-5 col-md-4 col-sm-7 col-xs-7">
                                    <label>Tipo de Pregunta</label>
                                   <asp:DropDownList ID="ddlTipoPregunta" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoPregunta_SelectedIndexChanged"></asp:DropDownList>
                                        
                                </div>
                                <div class="col-lg-1 col-md-2 col-sm-5 col-xs-5">
                                    <label>Orden</label>
                                     <asp:TextBox ID="txtorden" CssClass="form-control" placeholder="Orden" TextMode="Number" MaxLength="5" runat="server"></asp:TextBox>
                                   
                                </div>
                                  <asp:Panel ID="panel_entradalibre" runat="server" Visible="false" CssClass="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <label>Seleccione el Tipo de entrada de datos</label>
                                        <asp:DropDownList ID="ddltipoentrada" CssClass="form-control" runat="server">
                                                    <asp:ListItem Selected="True" Text="Seleccione uno por favor" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Fecha" Value="Date"></asp:ListItem>
                                                    <asp:ListItem Text="Solo numeros" Value="Number"></asp:ListItem>
                                                    <asp:ListItem Text="Entrada de cualquier dato (texto)" Value="SingleLine"></asp:ListItem>
                                                </asp:DropDownList>
                                   
                                </asp:Panel>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <label style="visibility: hidden;">Seleccione el Tipo</label>
                                    <asp:Button ID="btnAgregarPregunta" CssClass="btn btn-info btn-block" OnClick="btnAgregarPregunta_Click" runat="server" Text="Agregar Pregunta" />
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <label style="visibility: hidden;">Seleccione el Tipo</label>
                                    <asp:Button ID="btnCancelarPreg" CssClass="btn btn-danger btn-block" OnClick="BTNCancelar_Click1" runat="server" Text="Cancelar" Visible="false" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <asp:GridView style="text-align:center;font-size:12px;" ID="gridPreguntas" AutoGenerateColumns="false" OnRowDataBound="gridPreguntas_RowDataBound" CssClass="table table-responsive table-bordered table-condensed" runat="server" OnRowCommand="gridPreguntas_RowCommand" DataKeyNames="idc_pregunta,nuevo,orden,tipo_entrada,descripcion,idc_tipopregunta,primer_visita">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Eliminar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="idc_pregunta" HeaderText="idc_pregunta" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion"></asp:BoundField>
                                                <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                                                <asp:BoundField DataField="idc_tipopregunta" HeaderText="Tipo" Visible="false" />
                                                <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-block" Text="Modificar" CommandName="Respuestas" ButtonType="Button" 
                                                    HeaderText="Respuestas"></asp:ButtonField>

                                                <asp:BoundField DataField="orden" HeaderText="Orden" />
                                                <asp:BoundField DataField="tipo_entrada" HeaderText="Tipo de Entrada" Visible="false" />
                                                <asp:BoundField DataField="nuevo" HeaderText="nuevo" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="primer_visita" HeaderText="nuevo" Visible="false"></asp:BoundField>
                                            </Columns>
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="Panel_Respuestas" runat="server" CssClass="panel panel-success fresh-color" Visible="false">
                <div class="panel-heading" style="text-align: center;">
                    Ingrese las Respuestas Posibles
                </div>
                <div class="panel-body">
                    <label>Ingrese sus respuestas para la pregunta : "<asp:Label ID="lblpregunta" runat="server" Text=""></asp:Label>"</label>
                    <div class="row">
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                            <asp:TextBox ID="txtrespuesta" CssClass="form-control" placeholder="Ingrese una Respuesta" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <asp:TextBox ID="txtgrupo" CssClass="form-control" placeholder="Grupo" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                            <div class="btn-group">
                                <asp:CheckBox ID="cbxRespuestaEntrada" Text="La Respuesta Requiere una especificación" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                            <asp:TextBox ID="txtnombre_entrada" CssClass="form-control" placeholder="Ingrese un Nombre para la Entrada" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">

                            <div class="btn-group">
                                <asp:DropDownList ID="ddlTipoEntradRespuesta" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Text="Seleccione el Tipo de entrada de la Respuesta" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Fecha" Value="Date"></asp:ListItem>
                                    <asp:ListItem Text="Solo numeros" Value="Number"></asp:ListItem>
                                    <asp:ListItem Text="Entrada de cualquier dato (texto)" Value="SingleLine"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <asp:Button ID="btnAgreagrRespuesta" CssClass="btn btn-success btn-block" runat="server" Text="Agregar" OnClick="btnAgreagrRespuesta_Click" />
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <asp:Button ID="btnCancelar_respuesta" CssClass="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="btnCancelar_respuesta_Click" Visible="false" />
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="table table-responsive">
                            <asp:GridView ID="gridrespuestas" AutoGenerateColumns="false" CssClass="table table-responsive table-bordered table-condensed" runat="server" OnRowCommand="gridrespuestas_RowCommand" DataKeyNames="idc_pregunta,idc_respuesta,grupo, descripcion,nuevo,tipo_entrada,nombre_entrada,entrada">
                                <Columns>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar" CommandName="Editar">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Borrar" CommandName="Eliminar">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="idc_pregunta" HeaderText="idc_pregunta" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="idc_respuesta" HeaderText="idc_respuesta" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion"></asp:BoundField>
                                    <asp:BoundField DataField="grupo" HeaderText="Grupo"></asp:BoundField>
                                    <asp:BoundField DataField="nuevo" HeaderText="nuevo" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="entrada" HeaderText="Entrada" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="tipo_entrada" HeaderText="Entrada" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="nombre_entrada" HeaderText="Entrada" Visible="false"></asp:BoundField>
                                </Columns>
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Button ID="btncerrarrespuestas" runat="server" Text="Cerrar Respuestas" CssClass="btn btn-info btn-block" OnClick="btncerrarrespuestas_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:Button ID="btnGuardar" CssClass="btn btn-primary btn-block" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:Button ID="btnCancelar" CssClass="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
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
                                <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="Yes_Click" />
                            </div>
                            <div class="col-lg-6 col-xs-6">
                                <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="No" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>