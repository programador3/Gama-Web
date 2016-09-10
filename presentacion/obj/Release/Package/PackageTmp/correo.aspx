<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="correo.aspx.cs" Inherits="presentacion.correo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#lismail").height($(window).height() - 220);
        });
        function ModalClose() {
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
    <div class="row">
        <div class="col-lg-12">
            <div class="card card-success">
                <div class="card-header">
                    <div class="card-title">
                        <div class="title"><i class="fa fa-comments-o"></i>Bandeja de Entrada</div>
                    </div>
                    <div class="clear-both"></div>
                </div>
                <div class="card-body no-padding">
                    <ul id="lismail" class="message-list" style="overflow: auto; height: 720px;">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk" runat="server" CommandArgument='<%#Eval("mail") %>' CommandName='<%#Eval("date") %>' OnClick="lnk_Click">
                                      <li>
                                        <img src="../img/profile/user.png" class="profile-img pull-left">
                                        <div class="message-block">
                                            <div>
                                                <h5><strong><%#Eval("mail") %></strong><br /><small><%#Eval("date") %></small>
                                                   <br /> <%#Eval("title") %>
                                                </h5>
                                            </div>
                                        </div>
                                    </li>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" CssClass="btn btn-default btn-block" runat="server"> 
                                <i class="fa fa-refresh"></i>Refrescar
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
     <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 id="modal_title"><strong></strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="text-align: left;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <h5><strong>
                                        <label id="content_modal"></label>
                                    </strong>
                                    </h5>
                                    <h6>
                                        <asp:Label ID="lbldate" runat="server" Text=""></asp:Label></h6>
                                    <asp:Repeater ID="repeat_file" runat="server">
                                        <ItemTemplate>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                <asp:LinkButton ID="lnk" runat="server" OnClick="LinkButton2_Click" CommandArgument='<%#Eval("id") %>' CommandName='<%#Eval("path") %>'
                                                    CssClass="btn btn-info btn-block"><%#Eval("name") %>&nbsp;<i class="fa fa-file-archive-o" aria-hidden="true"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"  style="overflow: auto; height: auto; border:solid 1px;">
                                        <h6 id="bmail">
                                        <asp:Label ID="lblbody" runat="server" Text=""></asp:Label></h6>
                                    </div>
                                </div>
                            </div> 
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-12">
                                <input id="No" class="btn btn-info btn-block" onclick="ModalClose();" value="Cerrar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
