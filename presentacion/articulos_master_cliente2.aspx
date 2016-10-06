<%@ Page Title="Articulos Master" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="articulos_master_cliente2.aspx.cs" Inherits="presentacion.articulos_master_cliente2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Ocultar {
            display: none;
        }

        .linea {
            background-color: #1ABC9C;
            color: white;
        }

        .negociados {
            background: yellow;
            color: Blue;
        }

        .propuestos {
            background: orange;
            color: Blue;
        }

        .bloqueados {
            background-color: #FA2A00;
            color: #FFF;
            border-color: #FA2A00;
        }

        .grupo {
            text-decoration: underline;
        }

        .text-align-left {
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class="page-header">Articulos Master</h2>
   <asp:UpdatePanel ID="sss" runat="server" UpdateMode="always">
       <Triggers>
           <asp:PostBackTrigger ControlID="LinkButton5" />
           <asp:PostBackTrigger ControlID="Yes" />
           <asp:PostBackTrigger ControlID="LinkButton1" />
       </Triggers>
       <ContentTemplate>
           <div class="row">
               <div class="col-lg-12">
                   <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente</strong></h4>
                   <asp:TextBox ID="txtcliente" runat="server"
                       CssClass="form-control" Style="resize: none; margin: 0 0 !important;"
                       onfocus="this.blur()"></asp:TextBox>
               </div>
               <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                   <h4><strong>RFC</strong></h4>
                   <asp:TextBox ID="txtrfc" runat="server"
                       CssClass="form-control" Style="resize: none; margin: 0 0 !important;"
                       onfocus="this.blur()"></asp:TextBox>
               </div>
               <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                   <h4><strong>CVE</strong></h4>
                   <asp:TextBox ID="txtcve" runat="server"
                       CssClass="form-control" Style="resize: none; margin: 0 0 !important;"
                       onfocus="this.blur()"></asp:TextBox>
                   <asp:Label ID="lblrazonsocial" runat="server" Font-Bold="True" ForeColor="Gray"
                       Font-Names="arial" Font-Size="Small"></asp:Label>
               </div>
           </div>
           <div class="row">
               <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                   <asp:LinkButton ID="LinkButton2" CssClass="btn btn-primary" Width="100%" runat="server" OnClick="LinkButton2_Click">Nueva Tarea</asp:LinkButton>
               </div>
               <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                   <asp:LinkButton ID="LinkButton1" CssClass="btn btn-danger" Width="100%" runat="server" OnClick="LinkButton1_Click">Cerrar</asp:LinkButton>
               </div>
           </div>
           <div class="row">

               <div class="col-lg-12">
                   <asp:LinkButton ID="LinkButton3" CssClass="btn btn-success" Width="100%" runat="server" OnClick="LinkButton3_Click">
                Agregar Articulo&nbsp;<i class="fa fa-cubes" aria-hidden="true"></i></asp:LinkButton>
               </div>
               <div class="col-lg-12" id="tablediv" runat="server" style="font-size: 10px;">
                   <div class="table table-responsive">
                       <asp:GridView ID="gridproductosmaster" runat="server" CssClass="gvv table table-responsive table-bordered table-condensed" OnRowDataBound="gridproductosmaster_RowDataBound">
                           <Columns>
                           </Columns>
                       </asp:GridView>
                   </div>
                   <table style="width: 100%;">
                       <tr>
                           <td style="width: 2%">
                               <asp:TextBox ID="TextBox4" runat="server" BackColor="Blue" Height="16px"
                                   Width="30px"></asp:TextBox>
                           </td>
                           <td align="left" style="width: 49%">
                               <asp:Label ID="Label5" runat="server" Font-Size="X-Small"
                                   Text="Grupo de Articulos" Font-Bold="True" Font-Names="arial"></asp:Label>
                           </td>
                           <td align="right" style="width: 49%">
                               <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="True"
                                   Font-Size="X-Small" ForeColor="Red"
                                   Text="&quot;Precios no incluyen I.V.A.&quot;" Font-Names="arial"></asp:Label>
                           </td>
                       </tr>
                   </table>
                   <div class="table table-responsive">
                       <asp:DataGrid ID="grdmeses" CssClass="Ocultar" runat="server" AutoGenerateColumns="false">
                           <Columns>
                               <asp:BoundColumn DataField="fechai" HeaderText="fechai"></asp:BoundColumn>
                               <asp:BoundColumn DataField="fechaf" HeaderText="fechaf"></asp:BoundColumn>
                               <asp:BoundColumn DataField="mes" HeaderText="mes"></asp:BoundColumn>
                           </Columns>
                       </asp:DataGrid>
                       <input type="hidden" id="txtidc_cliente" runat="server" />
                   </div>
               </div>
               <div class="col-lg-12 col-xs-12" id="divadd" runat="server" visible="false">
                   <div style="border: 2px solid #757575; background-color: white; width: 100%; padding: 10px;">
                       <h4 style="text-align: center;"><strong><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Agregar Articulo</strong></h4>
                       <asp:LinkButton ID="LinkButton4" CssClass="btn btn-primary btn-block" runat="server" OnClick="LinkButton4_Click">
                Cancelar</asp:LinkButton>
                       <asp:TextBox placeholder="Buscar Articulo" runat="server" ID="txtbuscararticulo" AutoPostBack="true" onfocus="this.select();"
                           CssClass="form-control2" Width="60%" OnTextChanged="txtbuscararticulo_TextChanged"></asp:TextBox>

                       <asp:Button ID="btnbuscarm" runat="server" Style="width: 38%;" Text="Buscar Articulo"
                           CssClass="btn btn-primary" OnClick="txtbuscararticulo_TextChanged" />
                       <div runat="server" id="aded" visible="false">
                           <h5><strong>Articulo</strong></h5>
                           <asp:DropDownList ID="cbarticulos" Style="width: 70%"
                               runat="server" CssClass=" form-control2" AutoPostBack="true" OnSelectedIndexChanged="btnaddarticulo_Click">
                           </asp:DropDownList>

                           <asp:Button ID="btnaddarticulo" runat="server" Style="width: 28%;" Text="Seleccionar"
                               CssClass="btn btn-primary" OnClick="btnaddarticulo_Click" />
                           <h5><strong>Codigo</strong></h5>
                           <asp:TextBox ID="txtcodigo" CssClass=" form-control" ReadOnly="true" runat="server"></asp:TextBox>
                           <h5><strong>UM</strong></h5>
                           <asp:TextBox ID="txtum" CssClass=" form-control" ReadOnly="true" runat="server"></asp:TextBox>
                           <h5><strong>Tipo</strong></h5>
                           <asp:DropDownList ID="cbotipos" runat="server" CssClass=" form-control">
                           </asp:DropDownList>
                           <h5><strong>Fecha Compromiso</strong></h5>
                           <asp:TextBox ID="txtfechacompromiso" CssClass=" form-control" ReadOnly="false" TextMode="Date" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="LinkButton5" CssClass="btn btn-primary btn-block " runat="server" OnClick="LinkButton5_Click">Guardar Articulo</asp:LinkButton>
                       </div>
                   </div>
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
                               <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                           </div>
                           <div class="col-lg-6 col-xs-6">
                               <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                           </div>
                       </div>
                   </div>
               </div>
           </div>
       </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
