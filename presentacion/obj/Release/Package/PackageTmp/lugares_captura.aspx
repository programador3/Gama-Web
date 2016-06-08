<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="lugares_captura.aspx.cs" Inherits="presentacion.lugares_captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .fancybox-custom .fancybox-skin {
            box-shadow: 0 0 50px #222;
        }
    </style>
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
                    <h1 class="page-header">Captura de Lugares</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                            <label>Datos Principales del Lugar de Trabajo</label>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label>Folio</label>
                                        <asp:TextBox ID="txtfolio" CssClass="form-control" placeholder="Folio del Lugar" MaxLength="250" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-8 col-md-8 col-sm-6">
                                    <div class="form-group">
                                        <label>Descripcion</label>
                                        <asp:TextBox ID="txtdescripcion" CssClass="form-control" placeholder="Descripcion del Lugar" MaxLength="250" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label>Debe Ingresar una imagen marcando el lugar, puede tomar por base <a id="img_area" runat="server" class="fancybox-effects-d">la imagen del Area</a></label>
                                        <div class="input-group">
                                            <asp:FileUpload ID="fupPapeleria" CssClass="form-control" runat="server" />
                                            <span class="input-group-addon" style="color: #fff; background-color: #337ab7;">
                                                <asp:LinkButton ID="lnkGuardarPape" Style="color: #fff; background-color: #337ab7;" OnClick="lnkGuardarPape_Click" runat="server">Agregar <i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                            </span>
                                        </div>
                                        <asp:RegularExpressionValidator ID="REV" runat="server" CssClass="label label-danger"
                                            ErrorMessage="Tipo de archivo no permitido. Debe ser JPG" ControlToValidate="fupPapeleria"
                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG)$">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="gridPapeleria" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="col-lg-12">
                                            <div class="table table-responsive" style="text-align: center">
                                                <label style="text-align: center">Lugares Anexados</label>
                                                <asp:GridView ID="gridPapeleria" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed" OnRowDataBound="gridPapeleria_RowDataBound" OnRowCommand="gridPapeleria_RowCommand" DataKeyNames="nombre, ruta,folio, descripcion">
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/imagenes/btn/icon_editar.png" HeaderText="Editar">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:ButtonField>
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/btn/icon_delete.png" HeaderText="Eliminar" CommandName="Eliminar">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField HeaderText="Imagen">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <HeaderStyle Width="90px"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <a id="a_img" runat="server" class="fancybox-effects-d">Ver Imagen
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="folio" HeaderText="Folio"></asp:BoundField>
                                                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion"></asp:BoundField>
                                                        <asp:BoundField DataField="nombre" HeaderText="Imagen"></asp:BoundField>
                                                        <asp:BoundField DataField="ruta" HeaderText="Ruta Fisica Web" Visible="false"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>