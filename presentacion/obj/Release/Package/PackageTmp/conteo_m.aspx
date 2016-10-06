<%@ Page Title="Validar Inventario" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="conteo_m.aspx.cs" Inherits="presentacion.validar_conteo_inventario" %>

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
            $('#myModalArea').modal('hide');
            $('#myModal').modal('hide');
            $('#myModalImg').modal('hide');
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
        function ModalImgc(cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalImg').modal('show');
            $('#myModalImg').removeClass('modal fade modal-info');
            $('#myModalImg').addClass(ctype);
            $('#content_modali').text(cContenido);
        }
        function ModalA(cTitulo) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalArea').modal('show');
            $('#confirmTituloa').text(cTitulo);
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

        function ReturnValue() {
            var txt;
            var r = confirm("Se Validara el Conteo. Desea Continuar?");
            if (r == true) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:UpdatePanel runat="server" ID="upa" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlmodulo" />
            <asp:PostBackTrigger ControlID="lbkbuscar" />
        </Triggers>
        <ContentTemplate>
            <h1 class="page-header">Conteo de Inventario</h1>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <h4><strong>Sucursal</strong></h4>
                    <asp:DropDownList ID="ddlsucursal" AutoPostBack="true" OnSelectedIndexChanged="ddlsucursal_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <h4><strong>Almacen</strong></h4>
                    <asp:TextBox ReadOnly="true" ID="txtalmacen" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h4><strong>Modulo</strong></h4>
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlmodulo_SelectedIndexChanged" ID="ddlmodulo" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-10 col-md-9 col-sm-8 col-xs-12">
                    <asp:TextBox ID="txtbuscar" CssClass="form-control" placeholder="Buscar Modulo" runat="server"></asp:TextBox>
                    <asp:CheckBox ID="cbxtodo" CssClass="radio3 radio-check radio-info radio-inline" Text="Buscar en Todos los Modulos" runat="server" />
                </div>
                <div class="col-lg-2 col-md-3 col-sm-4 col-xs-12">
                    <asp:LinkButton ID="lbkbuscar" runat="server" CssClass="btn btn-info btn-block" OnClick="lbkbuscar_Click">
                            Buscar <i class="fa fa-search"></i></asp:LinkButton>
                </div>
                <div class="col-lg-12">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-12">
            <div class="table table-responsive" style="font-size:11px;">
                <asp:GridView ID="gridprocesos" CssClass="gvv table table-responsive table-bordered" runat="server" DataKeyNames="idc_artimod,estado,conteo_total,idc_articulo,idc_artimodprog " OnRowDataBound="gridprocesos_RowDataBound" AutoGenerateColumns="false" OnRowCommand="gridprocesos_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="idc_artimod" HeaderStyle-Width="30px" HeaderText="Codigo" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="idc_articulo" HeaderStyle-Width="30px" HeaderText="Codigo" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="idc_artimodprog " HeaderStyle-Width="30px" HeaderText="Codigo" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="codigo" HeaderStyle-Width="70px" HeaderText="Codigo" Visible="true"></asp:BoundField>
                        <asp:BoundField DataField="desart" HeaderText="Descripcion" Visible="true"></asp:BoundField>
                        <asp:BoundField DataField="um" HeaderText="UM" Visible="true" HeaderStyle-Width="20px"></asp:BoundField>
                        <asp:TemplateField HeaderText="Conteo" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:TextBox Width="70%"  ReadOnly="false" ID="txtconteo" TextMode="Number" CssClass="form-control2" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="lnkir" Width="28%" CssClass="btn btn-primary" OnClientClick="return ReturnValue();" OnClick="txtconteo_TextChanged" runat="server"><i class="fa fa-check-circle" aria-hidden="true"></i></asp:LinkButton>
                                <asp:RegularExpressionValidator ID="YourRegularExpressionValidator"
                                    ControlToValidate="txtconteo"
                                    runat="server"
                                    ErrorMessage="Please enter an amount in the correct format"
                                    EnableClientScript="false"
                                    ValidationExpression="^\d+([,\.]\d{1,2})?$">
                                </asp:RegularExpressionValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="conteo_total" HeaderText="Total Conteo" Visible="true" HeaderStyle-Width="30px"></asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado" Visible="true" HeaderStyle-Width="50px"></asp:BoundField>
                        <asp:BoundField DataField="decimales" HeaderText="Estado" Visible="false"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>