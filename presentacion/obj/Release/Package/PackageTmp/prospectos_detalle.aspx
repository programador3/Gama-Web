<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prospectos_detalle.aspx.cs" MasterPageFile="~/Global.Master"  Inherits="presentacion.prospectos_detalle" %>



<asp:Content ID="pdetalle_head" ContentPlaceHolderID="head" runat="server">

    <title>Prospecto Detalle</title>
<script type="text/javascript" src="js/jquery.rotate.min.js"></script>

<script type="text/javascript">

    var rotate_angle1 = 0;
    var rotate_angle2 = 0;

    $(document).ready(function () {
        $('#ContentDetalle_imgbtnrotar1').click(function () {
           
            rotate_angle1 = (rotate_angle1 == 360) ? 0 : rotate_angle1 + 90;
            $('#ContentDetalle_img1').rotate({ angle: rotate_angle1 });
            $('#id-content').rotate({ angle: rotate_angle1 });
            $('#id-body').rotate({ angle: rotate_angle1 });
           
        });
        //img 2 
        $('#ContentDetalle_imgbtnrotar2').click(function () {

            rotate_angle2 = (rotate_angle2 == 360) ? 0 : rotate_angle2 + 90;
            $('#ContentDetalle_img2').rotate({ angle: rotate_angle2 });
            $('#id-content2').rotate({ angle: rotate_angle2 });
            $('#id-body2').rotate({ angle: rotate_angle2 });
        });



    });
    
    
</script>


    <script type="text/javascript">
        function centerModal() {
            $(this).css('display', 'block');
            var $dialog = $(this).find(".modal-dialog");
            var offset = ($(window).height() - $dialog.height()) / 2;

            // Center modal vertically in window
            $dialog.css("margin-top", offset);
            
        }

        $('.modal').on('show.bs.modal', centerModal);
        $(window).on("resize", function () {
            $('.modal:visible').each(centerModal);
        });
    </script>
     <style type="text/css" >
  
    .btn_transparente
    { 
    background:transparent; 
    }
        
    </style> 

</asp:Content>
<asp:Content ID="pdetalle_content" ContentPlaceHolderID="Contenido" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <h1>
                Detalles Prospecto
            </h1>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive table">

            <table class="table table-bordered table-responsive table-condensed">
                <tr>
                    <th colspan="2">Detalle
                    </th>
                </tr>
                <tr>
                    <td>ID
                    </td>
                    <td>
                        <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Dirección</td>
                    <td>
                        <asp:Label ID="lbldir" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">Razón Social</td>
                    <td class="auto-style1">
                        <asp:Label ID="lblrsocial" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Tipo de obra</td>
                    <td>
                        <asp:Label ID="lbltipobra" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Tamaño de la Obra (m2)</td>
                    <td>
                        <asp:Label ID="lbltamobra" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Etapa de la obra</td>
                    <td>
                        <asp:Label ID="lbletapaob" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Observaciones</td>
                    <td>
                        <asp:Label ID="lblobs" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Fecha de Registro</td>
                    <td>
                        <asp:Label ID="lblregistro" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Usuario</td>
                    <td>
                        <asp:Label ID="lblusuario" runat="server" Text="Label"></asp:Label></td>

                </tr>

            </table>

        </div>
        <div class="table-responsive table">
            <asp:GridView ID="GridContacto" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-responsive table-condensed" DataKeyNames="contacto_id" OnRowCommand="GridContacto_RowCommand">
                <Columns>
                    <asp:BoundField DataField="contacto_id" HeaderText="Id" Visible="False" />
                    <asp:BoundField DataField="contacto" HeaderText="Contacto" />
                    <asp:ButtonField ButtonType="Image" CommandName="verTelefono" ImageUrl="~/imagenes/phone.png">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridTelefono" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridCorreo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="correo" HeaderText="Correo" />
                </Columns>
            </asp:GridView>
        </div>
        <!--tablas de familia de articulos -->
        <div class="table-responsive table">
            <asp:GridView ID="grid_famart_det" runat="server" CssClass="table table-bordered table-responsive table-condensed" AutoGenerateColumns="False" DataKeyNames="idc_prospecto_famartd" OnRowCommand="grid_famart_det_RowCommand">
                <Columns>
                    <asp:BoundField DataField="idc_prospecto_famartd" HeaderText="Idc_prospecto_famartd" Visible="False">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="idc_prospecto_famart" HeaderText="Idc_prospecto_famart" Visible="False">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:ButtonField ButtonType="Button" CommandName="clic_famartdet" DataTextField="descripcion" Text="Descripcion" HeaderText="Articulo">
                        <ControlStyle BorderStyle="None" CssClass="btn_transparente" />
                        <ItemStyle CssClass="btn_transparente" HorizontalAlign="Center" />
                    </asp:ButtonField>
                </Columns>
            </asp:GridView>
            <asp:HiddenField ID="oc_gridfamartdet" runat="server" Value="-1" />
        </div>
        <div class="table-responsive table">
            <asp:GridView ID="grid_famart_detmar" runat="server" CssClass="table table-bordered table-responsive table-condensed" AutoGenerateColumns="False" DataKeyNames="idc_prospecto_famartdm">
                <Columns>
                    <asp:BoundField DataField="idc_prospecto_famartdm" HeaderText="Idc_prospecto_famartdm" Visible="False">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="idc_prospecto_famartd" HeaderText="Idc_prospecto_famartd" Visible="False" />
                    <asp:BoundField DataField="marca" HeaderText="Marca">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="distribuidor" HeaderText="Distribuidor">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="precio" DataFormatString="{0:C2}" HeaderText="Precio">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                </Columns>
            </asp:GridView>
        </div>
        </div>
        <!-- fin 13-10-2015 -->
    </div>
    <div class="row">
        <div class="col-xs-4">
            <asp:Label ID="lbllatitud" runat="server" Text="Latitud:" Visible="False"></asp:Label>
            <asp:Label ID="lbllatitudval" runat="server" Text="Label" Visible="False"></asp:Label><br />
            <asp:Label ID="lbllongitud" runat="server" Text="Longitud: " Visible="False"></asp:Label>
            <asp:Label ID="lbllongitudval" runat="server" Text="Label" Visible="False"></asp:Label>
        </div>
        <div class="col-xs-2">
            <asp:ImageButton ID="btnimgmap" runat="server" ImageUrl="~/imagenes/mapa.png" Visible="False" OnClick="btnimgmap_Click1" />
        </div>
        <div class="col-xs-6"></div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <asp:GridView ID="GridObras" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="descripcion" HeaderText="Mas obras" />
                </Columns>
            </asp:GridView>

        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-6">
            <asp:Image ID="img1small" runat="server" Height="200px" Width="200px" data-toggle="modal" data-target="#myModal" CssClass="img-responsive center-block" />
        </div>
        <div class="col-xs-6">
            <asp:Image ID="img2small" runat="server" Height="200px" Width="200px" data-toggle="modal" data-target="#myModal2" CssClass="img-responsive center-block" />


        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <%-- divs con imagenes originales ocultas --%>
            <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content" id="id-content">
                        <div class="modal-body" id="id-body">

                            <asp:Image ID="img1" runat="server" CssClass="img-responsive center-block" />

                        </div>

                    </div>
                    <asp:ImageButton ID="imgbtnrotar1" runat="server" OnClientClick="return false;" ImageUrl="~/imagenes/rotar.png" />
                </div>
            </div>
            <div id="myModal2" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content" id="id-content2">
                        <div class="modal-body" id="id-body2">

                            <asp:Image ID="img2" runat="server" CssClass="img-responsive center-block" />

                        </div>
                    </div>
                    <asp:ImageButton ID="imgbtnrotar2" runat="server" OnClientClick="return false;" ImageUrl="~/imagenes/rotar.png" />
                </div>
            </div>
            <%-- divs con imagenes originales ocultas fin  --%>
        </div>
    </div>


</asp:Content>
