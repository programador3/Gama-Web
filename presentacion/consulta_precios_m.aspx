<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="consulta_precios_m.aspx.cs" Inherits="presentacion.consulta_precios_m" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <h2 class="page-header" style="text-align: center;">Consulta de Precios</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID ="ddlAgente"            EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID ="ddlClientes"          EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID ="ddlProductos_Master"  EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID ="ddlTipo_Producto"     EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID ="txtsearch"            EventName="TextChanged" />
            
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <h4><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Agente </h4>
                    <asp:DropDownList ID="ddlAgente" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAgente_Changed" AutoPostBack="true">
                    </asp:DropDownList>
                    <h4><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Clientes </h4>            
                    <asp:DropDownList ID="ddlClientes" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlClientes_Changed" AutoPostBack="true">
                    </asp:DropDownList>
                    

                    <h4><i class="fa fa-archive" aria-hidden="true"></i>&nbsp;Tipo de Producto</h4>
                    <asp:DropDownList ID="ddlTipo_Producto" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTipo_Producto_Changed"  AutoPostBack="true">
                        <asp:listitem value ="0"> Master </asp:listitem>
                        <asp:listitem value ="1"> General </asp:listitem>
                    </asp:DropDownList>

                    <div runat="server" id="divBuscar" style="display:block" >
                        <h4 ><i class="fa fa-search" aria-hidden="true"></i>&nbsp;Buscar </h4>                     
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="txtsearch" CssClass="form-control" runat="server" AutoPostBack="true"
                                 placeholder="Buscar Productos" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                            <span class="glyphicon glyphicon-search form-control-feedback"></span>
                        </div>
                    </div>

                    <h4 runat="server" id="h4Producto_Master" style="display:block"><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp; <asp:Label runat="server" ID="lblProducto"> Productos  Master</asp:Label> </h4>
                    <asp:DropDownList ID="ddlProductos_Master" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProductos_Master_Changed"  AutoPostBack="true">
                    </asp:DropDownList> 
                                        
                </div>
            </div>    
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:LinkButton ID="lnkcomentario" Visible="true" CssClass="btn btn-primary btn-block" runat="server"  OnClick="btnAceptar_Click">Seleccionar</asp:LinkButton>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:LinkButton ID="lnkcerrar" CssClass="btn btn-danger btn-block" runat="server" OnClick="btnCancelarTodo_Click">Salir</asp:LinkButton>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" >                                          
                    <h5><i class="fa fa-info-circle" aria-hidden="true"> </i>&nbsp;Descripcion</h5>
                    <asp:TextBox ID="txtDescripcion" onfocus="$(this).select();" onblur="return imposeMaxLength(this, 250);" CssClass="form-control" placeholder="Descripcion" runat="server"
                        Style="text-transform: uppercase;resize:none;" TextMode="Multiline" Rows="2" MaxLength ="250"  Enabled="false">
                    </asp:TextBox >                                       
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" >
                    <h5><i class="fa fa-barcode" aria-hidden="true"></i>&nbsp;Codigo Articulo</h5>
                    <asp:TextBox ID="txtCodigo_Articulo" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>                                
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" >
                    <h5><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Unidad de Medida</h5>
                    <asp:TextBox ID="txtUM" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" >
                    <h5><i class="fa fa-usd" aria-hidden="true"></i>&nbsp; Precio</h5>
                    <asp:TextBox ID="txtPrecio" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" >
                    <h5><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;<i class="fa fa-arrow-down" aria-hidden="true"></i>&nbsp;Precio Minimo</h5>
                    <asp:TextBox ID="txtPrecio_Minimo" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" >
                    <h5><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;<i class="fa fa-list-ul" aria-hidden="true"></i>&nbsp;Precio Lista</h5>
                    <asp:TextBox ID="txtPrecio_Lista" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" >
                    <h5><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;<i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Precio Real</h5>
                    <asp:TextBox ID="txtPrecio_Real" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
        
            </div>
            <hr />
            <div class="row" style="border: solid 1px rgb(255, 166, 166);border-radius: 5px;">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" >
                    <h5><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;<i class="fa fa-file-text" aria-hidden="true"></i>&nbsp;Ult. Precio Fac</h5>
                    <asp:TextBox ID="txtUlt_Precio_Fac" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" >
                    <h5><i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;Nota de Credito</h5>
                    <asp:TextBox ID="txtNota_Credito" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" >
                    <h5><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</h5>
                    <asp:TextBox ID="txtFecha" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
            </div>

            <%--<div class="row">
                <div class="col-lg-12">
                    <div class="table table-responsive">
                        <asp:TextBox ID="txtidc_Articulo" runat="server" ></asp:TextBox>
                        <asp:GridView ID="gridclientes" CssClass="gvv table table-responsive table-bordered" runat="server" DataKeyNames="idc_cliente,rfccliente,cveadi,nombre,num_gpo,grupo" AutoGenerateColumns="false" >
                            <Columns>                                
                                <asp:BoundField DataField="idc_cliente" HeaderText="idc_cliente" ></asp:BoundField>
                                <asp:BoundField DataField="rfccliente"  HeaderText="rfccliente" ></asp:BoundField>
                                <asp:BoundField DataField="cveadi"      HeaderText="cveadi" ></asp:BoundField>
                                <asp:BoundField DataField="nombre"      HeaderText="nombre" ></asp:BoundField>
                                <asp:BoundField DataField="num_gpo"     HeaderText="num_gpo" ></asp:BoundField>
                                <asp:BoundField DataField="grupo"       HeaderText="grupo" ></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
