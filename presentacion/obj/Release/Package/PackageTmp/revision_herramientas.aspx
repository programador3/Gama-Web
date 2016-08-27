<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="revision_herramientas.aspx.cs" Inherits="presentacion.revision_herramientas"  MaintainScrollPositionOnPostback="true" %>
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
            $('#myModalh').modal('hide');
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
        function ModalConfirmh(cTitulo) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalh').modal('show');
            $('#modal_titlef').text(cTitulo);
        }
        function AlertGO(TextMess, URL) {
            swal({
                title: "Mensaje del Sistema",
                text: TextMess,
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false, allowEscapeKey: false
            },
               function () {
                   location.href = URL;
               });
        }
    </script>
    <style type="text/css">
        .form-control2 {
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
        .read {        
             border: 1px solid #ccc;
            background-color: #eee;
            opacity: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h3 class="page-header">Revisión de Herramientas</h3>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="always" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkGuardarPape" />
            <asp:PostBackTrigger ControlID="gridPapeleria" />
            <asp:PostBackTrigger ControlID="gridherramientas" />
            <asp:PostBackTrigger ControlID="blkcalibracion" />
            <asp:AsyncPostBackTrigger ControlID="txtentrego" EventName="TextChanged" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-car" aria-hidden="true"></i>&nbsp;Vehiculo</strong></h5>
                    <asp:DropDownList AutoPostBack="true" ID="ddlvehiculo" CssClass="form-control2" Width="100%" runat="server" OnSelectedIndexChanged="ddlvehiculo_SelectedIndexChanged"></asp:DropDownList>
                    <asp:TextBox OnTextChanged="lnkbuscarvehiculo_Click" AutoPostBack="true" placeholder="Buscar" ID="txtbuscarvehiculo" CssClass="form-control2" Width="68%" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="lnkbuscarvehiculo" CssClass="btn btn-default" Width="30%" runat="server" OnClick="lnkbuscarvehiculo_Click">Buscar</asp:LinkButton>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Empleado</strong></h5>
                    <asp:TextBox placeholder="Empleado" ID="txtempleado" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                    <asp:Label ID="idc_empleado" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                    <h4 style="text-align:center;"><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Herramientas a Revisar</strong>
                        <span>
                            <asp:LinkButton ID="lnkver" CssClass="btn btn-success" runat="server" Visible="false" OnClick="lnkver_Click">Ver Herramientas <i class="fa fa-chevron-circle-down" aria-hidden="true"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkocultar" CssClass="btn btn-danger" runat="server" Visible="false" OnClick="lnkocultar_Click">Ocultar Herramientas <i class="fa fa-chevron-circle-up" aria-hidden="true"></i></asp:LinkButton>
                        </span>
                    </h4>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="table table-responsive">
                        <asp:GridView Style="text-align: center; font-size: 11px" ID="gridherramientas" runat="server" 
                            AutoGenerateColumns="false" CssClass="table table-bordered table-condensed" 
                            DataKeyNames="descripcion, idc_tipoherramienta,cantidad,revision,costo,observaciones" OnRowCommand="gridherramientas_RowCommand" OnRowDataBound="gridherramientas_RowDataBound">
                            <HeaderStyle HorizontalAlign="center" />
                            <Columns>
                                <asp:BoundField DataField="gpo" HeaderText="Grupo" HeaderStyle-Width="60px"></asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Herramientas" HeaderStyle-Width="90px"></asp:BoundField>
                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" HeaderStyle-Width="10px"></asp:BoundField>
                                <asp:BoundField DataField="revision" HeaderText="Revision" HeaderStyle-Width="10px"></asp:BoundField>
                                <asp:BoundField DataField="observaciones" HeaderText="Observaciones"></asp:BoundField>
                                <asp:BoundField DataField="costo" HeaderText="Costo" HeaderStyle-Width="40px"></asp:BoundField>
                                <asp:BoundField DataField="idc_tipoherramienta" HeaderText="Costo" Visible="false" HeaderStyle-Width="50px"></asp:BoundField>
                                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Editar" CommandName="Editar" Text="Editar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                    <asp:Repeater ID="repeat_herramientas" runat="server" Visible="true">
                        <ItemTemplate>
                           <%-- <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12" style="font-size:11px">
                                <asp:UpdatePanel ID="aa" UpdateMode="always" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblidc_tipo" runat="server" Text='<%#Eval("idc_tipoherramienta") %>' Visible="false"></asp:Label>
                                        <div class="thumbnail no-margin-bottom">
                                            <div class="caption">
                                                <h5 id="thumbnail-label"><strong><i class="fa fa-cog" aria-hidden="true"></i>&nbsp;<%#Eval("descripcion") %></strong></h5>
                                                <strong>
                                                    <asp:Label style="text-align:left;"  Height="25px" ID="Label1" runat="server" Text="Cantidad: " Width="18%"></asp:Label></strong>&nbsp;
                                                    <asp:Label style="text-align:left;" Height="25px" ID="lblcantidad" runat="server" Text='<%#Eval("cantidad") %>' Width="25%"></asp:Label>
                                                <strong>
                                                    <asp:Label style="text-align:left;" Height="25px" ID="Label2" runat="server" Text="Costo Uni:" Width="22%"></asp:Label></strong>
                                                    <i class="fa fa-usd" aria-hidden="true"></i>&nbsp;<asp:Label style="text-align:left;" Height="25px" ID="lblcosto" runat="server" Text='<%#Eval("costo") %>' Width="25%"></asp:Label>
                                               
                                                <br />
                                                <strong>
                                                    <asp:Label Height="25px" ID="idc" runat="server" Text="Entrego: " Width="18%"></asp:Label></strong>
                                                <asp:TextBox Height="25px" onfocus="$(this).select();" Width="75%" OnTextChanged="texcosto_Click" AutoPostBack="true" ID="txtentrego" CssClass="form-control2" Text='<%#Eval("cantidad") %>' TextMode="Number" runat="server"></asp:TextBox>
                                                <br />
                                                <strong>
                                                 <asp:Label Height="25px" ID="Label3" runat="server" Text="Vale:" Width="18%"></asp:Label></strong>
                                                <asp:TextBox Height="25px" Width="75%" ID="txtcostovale" style="color:red;" ReadOnly="true" CssClass="form-control2 read" TextMode="Number" runat="server"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:TextBox Style="resize: none;font-size:11px" Width="100%" ID="txtobservaciones" CssClass="form-control" placeholder="Observaciones" TextMode="Multiline" Rows="2" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>--%>
                        </ItemTemplate>
                </asp:Repeater>
                
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Total Vale</strong></h5>
                    <asp:TextBox placeholder="0" ID="txttotalvale" Text="0" Style="color: red;" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Descontar por Semana</strong></h5>
                    <asp:TextBox placeholder="0"  onkeypress="return validarEnteros(event);" ID="txtporsemana" Text="0" AutoPostBack="true" CssClass="form-control" ReadOnly="false" runat="server" OnTextChanged="txtporsemana_TextChanged"></asp:TextBox>

                </div>
                
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-exclamation-circle" aria-hidden="true"></i>&nbsp;Capturar Hallazgo o Golpe</strong></h5>
                    <asp:TextBox TextMode="Multiline" Rows="2"  placeholder="Ingrese Comentarios acerca del hallazgo" ID="txtcomenthallazgo" CssClass="form-control" ReadOnly="false" runat="server"></asp:TextBox>
                    <br />
                    <asp:FileUpload ID="fuparchivo" CssClass="form-control" runat="server" />
                    <br /> 
                    <asp:LinkButton ID="lnkGuardarPape" CssClass="btn btn-primary btn-block" OnClick="lnkGuardarPape_Click" runat="server">Agregar Golpe <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                </div>
                <div class="col-lg-12 col-xs-12">
                    <div class="table table-responsive">
                        <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" DataKeyNames="descripcion, ruta, extension" OnRowCommand="gridPapeleria_RowCommand">
                            <Columns>                                
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:ButtonField>     
                                 <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:ButtonField>                           
                                <asp:BoundField DataField="extension" HeaderText="extension" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5><strong><i class="fa fa-car" aria-hidden="true"></i>&nbsp;Revisar Calibracion de Llantas</strong></h5>
                    <asp:LinkButton ID="blkcalibracion" CssClass="btn btn-default btn-block" runat="server" OnClick="blkcalibracion_Click">Revisado</asp:LinkButton>

                </div>
                 
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
            <br />
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-info btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" />
        </div>
    </div>
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
    <div class="modal fade modal-info" id="myModalh" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="modal_titlef"><strong></strong></h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="sss" runat="server" UpdateMode="always">
                        <ContentTemplate>
                            <div class="row" style="text-align: center;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <strong style="text-align: right;">
                                        <asp:Label ID="Label1w" runat="server" Text="Cantidad:" Width="20%"></asp:Label></strong>
                                    <asp:TextBox ID="txtcantidad" ReadOnly="true" Width="75%" CssClass="form-control2 read" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <strong style="text-align: right;">
                                        <asp:Label Style="text-align: right;" ID="Label4" runat="server" Text="Costo:" Width="20%"></asp:Label></strong>
                                    <asp:TextBox ID="txtcosto" ReadOnly="true" Width="75%" CssClass="form-control2 read" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <strong style="text-align: right;">
                                        <asp:Label Style="text-align: right;" ID="Label5" runat="server" Text="Entrego: " Width="20%"></asp:Label></strong>
                                    <asp:TextBox ID="txtentrego" OnTextChanged="texcosto_Click"  onkeypress="return validarEnteros(event);" AutoPostBack="true" onfocus="$(this).select();" ReadOnly="false" TextMode="Number" Width="75%" CssClass="form-control2" runat="server" autofocus></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <strong style="text-align: right;">
                                        <asp:Label Style="text-align: right;" ID="Label3" runat="server" Text="Vale:" Width="20%"></asp:Label></strong>
                                    <asp:TextBox Width="75%" ID="txtcostovale" Style="color: red;" ReadOnly="true"
                                        CssClass="form-control2 read" TextMode="Number" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:TextBox Style="resize: none; font-size: 11px" Width="100%" ID="txtobservaciones" CssClass="form-control" placeholder="Observaciones" TextMode="Multiline" Rows="5" runat="server"></asp:TextBox>
                                </div>
                                <asp:Label Visible="false" ID="lblidc_herramienta" runat="server" Text=""></asp:Label>
                                 <asp:Label Visible="false" style="font-size:20px; color:red; font:bold;" ID="lblerror" runat="server" Text=""></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="Button1f" class="btn btn-info btn-block" runat="server" Text="Editar" OnClick="Yesh_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                         <asp:Button ID="Button1" class="btn btn-danger btn-block" runat="server" Text="Cerrar" OnClick="cerr_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
