<%@ Page Title="Areas Captura" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="areas_captura.aspx.cs" Inherits="presentacion.areas_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <br />
                    <br />
                    <h1 class="page-header">Captura de Areas</h1>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading" style="text-align: center;">
                                    <label>Datos Principales del Area</label>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <label>Nombre</label>
                                                <asp:TextBox ID="txtdescripcion" CssClass="form-control" placeholder="Nombre del Area" MaxLength="250" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-8 col-sm-12">
                                            <div class="form-group">
                                                <label>Eliga una sucursal para el Area</label>
                                                <asp:DropDownList ID="ddlsucursales" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsucursales_SelectedIndexChanged"></asp:DropDownList>
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
                <div class="col-lg-12">
                    <div class="panel panel-red">
                        <div class="panel-heading" style="text-align: center;">
                            Imagen del Area
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                                            <span class="input-group-addon" style="color: #fff; background-color: #d9534f;">
                                                <asp:LinkButton ID="lnkGuardarPape" Style="color: #fff; background-color: #d9534f;" OnClick="lnkGuardarPape_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                            </span>
                                        </div>
                                        <asp:RegularExpressionValidator ID="REV" runat="server" CssClass="label label-danger"
                                            ErrorMessage="Tipo de archivo no permitido. Debe ser JPG" ControlToValidate="fupPapeleria"
                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG)$">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="nombre, ruta">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-info" HeaderText="Descargar" CommandName="Descargar" Text="Descargar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre Documento"></asp:BoundField>
                                                <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnGuardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" runat="server" Text="Guardar" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Button ID="btnCancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click" runat="server" Text="Cancelar" />
                    </div>
                </div>
            </div>
            <!-- /.CONFIRMA -->
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