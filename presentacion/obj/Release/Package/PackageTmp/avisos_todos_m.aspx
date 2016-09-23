<%@ Page Title="Avisos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="avisos_todos_m.aspx.cs" Inherits="presentacion.avisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/PanelsLTE.css" rel="stylesheet" />
    <link href="css/ionicons.css" rel="stylesheet" />
    <link href="css/ionicons.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function ModalPreviewHeramienta() {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#modalPreviewView').modal('show');

        }
        function ModalClose() {
            $('#modalPreviewView').modal('hide');
        }
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
    <style type="text/css">
        #row_modal {            
            height:auto;
            overflow: scroll;
        }
        @media (min-width: 768px) {
            .modal-dialog {
            width: 95%;
            margin: 30px auto;
        }
        }
         @media (max-width: 768px) {
            .modal-dialog {
            width:  95%;
            margin: 30px auto;
        }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:LinkButton ID="lnkReturn" runat="server" Visible="false" OnClick="lnkReturn_Click" CausesValidation="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                        Avisos sin Leer</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card card-info">
                        <div class="card-header">
                            <div class="card-title" style="text-align: center;">
                                <div class="title">
                                    <h3><i class="fa fa-comments-o"></i>&nbsp;Avisos <small style="color: white;">Click sobre el mensaje para ver el aviso completo</small></h3>
                                </div>
                            </div>
                            <div class="clear-both"></div>
                        </div>
                        <div class="card-body no-padding">
                            <ul class="message-list" style="height: 560px; overflow: scroll;">
                                <asp:Repeater ID="repeat_avisos" runat="server" OnItemDataBound="repeat_avisos_ItemDataBound">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName='<%#Eval("idc_aviso") %>' OnClick="LinkButton1_Click">
                                            <li>
                                                <asp:Image Visible="false" ID="img_profile" runat="server" CssClass="profile-img pull-left" />
                                                <%--<img src="../img/profile/profile-1.jpg" class="">--%>
                                                <div class="message-block">
                                                    <div>
                                                        <span class="username"><%#Eval("usuenv") %></span> <span class="message-datetime"><%#Eval("fecha_form") %></span>
                                                    </div>
                                                    <div class="message"><%#Eval("descripcion_corta") %></div>
                                                </div>
                                            </li>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalPreviewView" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #428bca; color: white; text-align: center;">
                             <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 style="text-align: center;"><strong>
                                <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></strong>
                                <span><input id="btnModalAcept" type="button" class="btn btn-primary" value="Aceptar" onclick="ModalClose();" /></span>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                              <div class="col-lg-12">
                                    <label style="text-align: center;"><strong>Aviso emitido desde
                                        <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label></strong>                                              
                                    </label>
                           
                              </div>
                                <div class="col-lg-12" id="row_modal">
                                    <asp:PlaceHolder ID="plccontenido" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.CONFIRMA -->
                </div>
            </div>
</asp:Content>