<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="quejas_espera_m.aspx.cs" Inherits="presentacion.quejas_espera_m" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
        function ModalConfirm() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
        }
        function ModalClose() {
            $('#myModal').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Quejas Pendientes
            </h1>
        </div>
    </div>

    <asp:UpdatePanel ID="Aa" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="input-group">
                        <asp:TextBox ID="txtbuscar" CssClass="form-control" placeholder="Buscar" AutoPostBack="true" runat="server" OnTextChanged="txtbuscar_TextChanged"></asp:TextBox>
                        <span class="input-group-addon" style="color: #fff; background-color: #1ABC9C;">
                            <asp:LinkButton ID="lnkGuardarPape" Style="color: #fff; background-color: #1ABC9C;" runat="server" OnClick="txtbuscar_TextChanged">Buscar <i class="fa fa-search"></i> </asp:LinkButton>
                        </span>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Repeater ID="repeat" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="thumbnail no-margin-bottom">
                                <div class="caption">
                                    <h4 id="thumbnail-label"><strong>No.</strong><%#Eval("idc_queja") %> <strong>&nbsp;&nbsp;Cliente:&nbsp;</strong><%#Eval("cliente") %></h4>
                                    <p style="font-size: 12px; height: 70px"><%#Eval("problema_resumido") %></p>
                                    <p>
                                        <asp:LinkButton ID="lnksolucionar" CommandName="sol" CommandArgument='<%#Eval("idc_queja") %>' OnClick="lnksolu_Click" CssClass="btn btn-success" runat="server">Solucionar <i class="fa fa-check" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:LinkButton  ID="lnkcancelar" CommandName="can" CommandArgument='<%#Eval("idc_queja") %>' OnClick="lnksolu_Click" CssClass="btn btn-danger" runat="server">Cancelar <i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:LinkButton  ID="lnkagregarcomentario" CommandName="add" CommandArgument='<%#Eval("idc_queja") %>' OnClick="lnksolu_Click" CssClass="btn btn-primary" runat="server">Comentario <i class="fa fa-pencil" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:LinkButton  ID="lnkinformacion" CommandName="info" CommandArgument='<%#Eval("idc_queja") %>' OnClick="lnkinfo_Click" CssClass="btn btn-info" runat="server">Información <i class="fa fa-info" aria-hidden="true"></i></asp:LinkButton>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="modal_title"><strong>Información sobre Queja</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-12">
                        <input id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>