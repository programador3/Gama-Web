<%@ Page Title="Informacion" Language="C#" MasterPageFile="~/Adicional.Master" AutoEventWireup="true" CodeBehind="pre_empleados_info.aspx.cs" Inherits="presentacion.pre_empleados_info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .form-control {
            width: 100%;
            display: block;
            padding: 6px 12px;
            font-size: 10px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        }

        .input-group-addon {
            padding: 6px 12px;
            font-size: 11px;
            font-weight: 400;
            line-height: 1;
            color: #555;
            text-align: center;
            background-color: #eee;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        div.container4 {
            height: 20em;
            position: relative }
        div.container4 img {
            margin: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-right: -50%;
            transform: translate(-50%, -50%) }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class =" header">
        Información del Pre-Empleado&nbsp;<span>
          <asp:Button ID="close" runat="server" Text="Cerrar Ventana" OnClientClick="window.close();" CssClass="btn btn-danger" ToolTip="Limpiar" />
                                                                              </span>
    </h2>    
    <div class="row">
        <div class="container4">
            <asp:Image CssClass=" image img-responsive" style="max-width:250px; max-height:250px" ID="imgempleado" ImageUrl="~/imagenes/acount.png" runat="server" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <h4 style="text-align: center;"><strong>Datos Personales <i class="fa fa-user"></i></strong></h4>               
            <asp:Repeater ID="gridDetalles" runat="server">
                <ItemTemplate>
                    <div class="row">
                    <div class="col-lg-12 ">
                        
                            <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Nombre <i class="fa fa-user"></i></span>
                            <asp:TextBox ID="txt" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("nombre") %>'></asp:TextBox>
                        </div>
                                </div>
                        
                            <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Genero <i class="fa fa-user"></i></span>
                            <asp:TextBox ID="TextBox1" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("sexo") %>'></asp:TextBox>
                        </div>
                                </div>
                    </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Estado Civil <i class="fa fa-user"></i></span>
                                    <asp:TextBox ID="TextBox4" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("edo_civil") %>'></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Fecha de Nac <i class="fa fa-user"></i></span>
                                    <asp:TextBox ID="TextBox2" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%# Convert.ToDateTime(Eval("fec_nac")).ToString("dddd dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")).ToUpper() %>'></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Dirección <i class="fa fa-user"></i></span>
                                    <asp:TextBox ID="TextBox5" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("direccion") %>'></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h4 style="text-align: center;"><strong>Contacto <i class="fa fa-mobile"></i></strong></h4>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">Correo <i class="fa fa-laptop"></i></span>
                                    <asp:TextBox ID="TextBox3" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("correo_personal") %>'></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Repeater ID="repeat_telefonos" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;"><i class="fa fa-mobile"></i></span>
                                    <asp:TextBox ID="TextBox3" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("telefono") %>'></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            
            <h4 style="text-align: center;"><strong>Referencias Laborales <i class="fa fa-archive"></i></strong></h4>
            <div class=" row">
                <div class=" col-lg-12">
                    <div class="table table-responsive">
                        <asp:GridView ID="gridreferencias" runat="server" AutoGenerateColumns="false" CssClass=" table table-responsive table-bordered table-condensed"
                            DataKeyNames="telefono, contacto">
                            <Columns>
                                <asp:BoundField DataField="empresa" HeaderText="Empresa"></asp:BoundField>
                                <asp:BoundField DataField="contacto" HeaderText="Contacto"></asp:BoundField>
                                <asp:BoundField DataField="telefono" HeaderText="Numero de Telefono"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <asp:Repeater ID="repeater_referencias" Visible="false" runat="server">
                    <ItemTemplate>

                        <div class=" col-lg-12">
                            <h5><strong>Empresa</strong></h5>
                            <asp:TextBox ReadOnly="true" CssClass=" form-control" ID="TextBox6" runat="server" Text='<%#Eval("empresa") %>'></asp:TextBox>
                            <h5><strong>Contacto</strong></h5>
                            <asp:TextBox ReadOnly="true" CssClass=" form-control" ID="TextBox7" runat="server" Text='<%#Eval("contacto") %>'></asp:TextBox>
                            <h5><strong>Telefono</strong></h5>
                            <asp:TextBox ReadOnly="true" CssClass=" form-control" ID="TextBox8" runat="server" Text='<%#Eval("telefono") %>'></asp:TextBox>
                            <%--<h5><strong>Llamada</strong></h5>--%>
                            <%--     <asp:LinkButton ID="LinkButton1" CommandName='<%#Eval("audiopath") %>' CommandArgument='<%#Eval("audio") %>'  OnClick="lnkdownload_Click" CssClass="btn btn-info btn-block" runat="server">Ver Archivo</asp:LinkButton>--%>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <h4 style="text-align: center;"><strong>Papeleria <i class="fa fa-archive"></i></strong></h4>

            <div class="row">
                <asp:Repeater ID="repeat_papeleria" runat="server" OnItemDataBound="repeat_papeleria_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-lg-6 col-md-6 col-sm-12" id="div_details" runat="server">
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:TextBox ID="TextBox3" ReadOnly="true" runat="server" CssClass="form-control input-group-sm " Text='<%#Eval("descripcion") %>'></asp:TextBox>

                                    <span class="input-group-addon" style="color: #fff; background-color: #22A7F0;">
                                        <asp:LinkButton ID="lnkdownload" Style="color: #fff; background-color: #22A7F0;" runat="server" OnClick="lnkdownload_Click">Ver Archivo <i class="fa fa-download"></i></asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h4 style="text-align: center;"><strong>Observaciones del Reclutador <i class="fa fa-archive"></i></strong></h4>
                    <asp:TextBox ID="txtobservaciones2" ReadOnly="true" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" Text=""></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
