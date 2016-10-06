<%@ Page Title="Actividades" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="captura_actividades_agentes2.aspx.cs" Inherits="presentacion.captura_actividades_agentes2_m" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Ocultar {
            display:none;
        }
        #div_mayor, #div_mayor1 {
            padding: 0px 3px 3px 3px;
            background-color: Gray;
            border-radius: 3px;
            overflow: hidden;
        }

        #div_title {
            font-size: small;
            font-family: Arial;
            font-weight: bold;
            color: Black;
            background-color: Gray;
            text-align: center;
        }

        .div_title1 {
            font-size: small;
            font-family: Arial;
            font-weight: bold;
            color: Black;
            background-color: Gray;
            text-align: center;
        }

        #div_1 {
            font-size: small;
            font-family: Arial;
            font-weight: normal;
            color: Black;
            background-color: White;
            text-align: center;
            width: 50%;
            float: left;
        }

        #div_g1 {
            font-size: small;
            font-family: Arial;
            font-weight: normal;
            color: Black;
            background-color: White;
            text-align: center;
            width: 50%;
            float: left;
        }

        #div_2 {
            font-size: small;
            font-family: Arial;
            font-weight: normal;
            color: white;
            background-color: Red;
            text-align: center;
            width: 50%;
            float: right;
        }

        #div_g2 {
            font-size: small;
            font-family: Arial;
            font-weight: normal;
            color: Black;
            background-color: rgb(0, 204, 255);
            text-align: center;
            width: 50%;
            float: left;
        }

        #div_3 {
            font-size: small;
            font-family: Arial;
            font-weight: normal;
            color: Black;
            background-color: Yellow;
            text-align: center;
            width: 50%;
            float: left;
        }

        #div_g3 {
            font-size: small;
            font-family: Arial;
            font-weight: normal;
            color: Black;
            background-color: rgb(243, 156, 75);
            text-align: center;
            width: 50%;
            float: left;
        }

        #div_4 {
            font-size: small;
            font-family: Arial;
            font-weight: normal;
            color: Black;
            background-color: gainsboro;
            text-align: center;
            width: 50%;
            float: right;
        }

        #div_g4 {
            font-size: small;
            font-family: Arial;
            font-weight: normal;
            color: Black;
            background-color: rgb(133,181,32);
            text-align: center;
            width: 50%;
            float: left;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class=" page-header">
        Captura de Actividades
    </h2>
    <div class="row">
        <div class="col-lg-12" id="clientes" runat="server">
            <h4><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Clientes de Agentes</strong></h4>
            <asp:DropDownList ID="cboagente" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="cboagente_SelectedIndexChanged" ></asp:DropDownList>
        </div>
        <div class="col-lg-12">
            <h4><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Visitas</strong></h4>
            <asp:DropDownList ID="cbodias" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="cbodias_SelectedIndexChanged" >
                <asp:ListItem>Hoy</asp:ListItem>
                <asp:ListItem>Otros Dias</asp:ListItem>
                <asp:ListItem>Lunes</asp:ListItem>
                <asp:ListItem>Martes</asp:ListItem>
                <asp:ListItem>Miercoles</asp:ListItem>
                <asp:ListItem>Jueves</asp:ListItem>
                <asp:ListItem>Viernes</asp:ListItem>
                <asp:ListItem>Sabado</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-lg-12">
           <div class="table table-responsive">
                <asp:GridView ID="grdclientes" runat="server" CssClass="gvv table table-responsive table-bordered table-condensed"
                AutoGenerateColumns="False" DataKeyNames="IDC_CLIENTE, p,t,dia,ult_compra" RowStyle-Font-Size="11px"
                HeaderStyle-Font-Names="arial" HeaderStyle-Font-Size="10px"
                PageSize="5" Width="100%" OnRowCommand="grdclientes_RowCommand" OnRowDataBound="grdclientes_RowDataBound">
                <RowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <HeaderStyle CssClass="sorttable_nosort" Width="30px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="btnSeleccionar" runat="server" CommandArgument="Update"
                                CommandName="Update" Height="30px" ImageUrl="~/imagenes/128.png"
                                OnClientClick="rowcommand" ToolTip="Seleccionar" Width="30px" />
                        </ItemTemplate>
                        <ItemStyle Font-Size="0.7em" HorizontalAlign="Center" />
                        <HeaderStyle Font-Size="0.7em" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Idc_cliente" HeaderStyle-CssClass="Ocultar"
                        HeaderText="ID" ItemStyle-CssClass="Ocultar" ItemStyle-Font-Names="arial">
                        <HeaderStyle CssClass="Ocultar" />
                        <ItemStyle CssClass="Ocultar" Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombre" HeaderText="Cliente"
                        ItemStyle-Font-Names="arial" SortExpression="nombre">
                        <ItemStyle Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DIAv" HeaderText="Dia Visita"
                        ItemStyle-Font-Names="arial" SortExpression="DIAv">
                        <ItemStyle Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rfccliente" HeaderText="RFC"
                        ItemStyle-Font-Names="arial" SortExpression="rfccliente">
                        <ItemStyle Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cveadi" HeaderText="Cve"
                        ItemStyle-Font-Names="arial" SortExpression="cveadi">
                        <ItemStyle Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="idc_gpocli" HeaderStyle-CssClass="Ocultar"
                        HeaderText="No. Grupo" ItemStyle-CssClass="Ocultar"
                        ItemStyle-Font-Names="arial" SortExpression="idc_gpocli">
                        <HeaderStyle CssClass="Ocultar" />
                        <ItemStyle CssClass="Ocultar" Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="idc_gpocli" HeaderStyle-CssClass="Ocultar"
                        HeaderText="Grupo" ItemStyle-CssClass="Ocultar"
                        ItemStyle-Font-Names="arial">
                        <HeaderStyle CssClass="Ocultar" />
                        <ItemStyle CssClass="Ocultar" Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ult_compra" HeaderStyle-CssClass="Ocultar"
                        HeaderText="ultventa" ItemStyle-CssClass="Ocultar"
                        ItemStyle-Font-Names="arial">
                        <HeaderStyle CssClass="Ocultar" />
                        <ItemStyle CssClass="Ocultar" Font-Names="arial" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Visita HOY">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle CssClass="sorttable_nosort" />
                        <ItemTemplate>
                            <asp:CheckBox  CssClass="radio3 radio-check radio-info radio-inline" ID="chkact" runat="server" Checked="true" onclick="return false;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="" HeaderText="GPS" HeaderStyle-CssClass="sorttable_nosort" ItemStyle-Font-Names="arial">
                        <ItemStyle Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="dia" HeaderStyle-CssClass="Ocultar"
                        HeaderText="dia" ItemStyle-CssClass="Ocultar"
                        ItemStyle-Font-Names="arial">
                        <HeaderStyle CssClass="Ocultar" />
                        <ItemStyle CssClass="Ocultar" Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="p" HeaderStyle-CssClass="Ocultar"
                        HeaderText="p" ItemStyle-CssClass="Ocultar" ItemStyle-Font-Names="arial">
                        <HeaderStyle CssClass="Ocultar" />
                        <ItemStyle CssClass="Ocultar" Font-Names="arial" />
                    </asp:BoundField>
                    <asp:BoundField DataField="t" HeaderStyle-CssClass="Ocultar" Visible="false"
                        HeaderText="t" ItemStyle-CssClass="Ocultar" ItemStyle-Font-Names="arial">
                        <HeaderStyle CssClass="Ocultar" />
                        <ItemStyle CssClass="Ocultar" Font-Names="arial" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="HeaderStyle" />
            </asp:GridView>
           </div>
        </div>
        <div class="col-lg-12">
            <table style="width: 100%;">
                   <tr>
                    <td>
                        <div id="div_mayor">
                            <div id="div_title">
                                Ultima Venta
                            </div>
                            <div id="div_c1">
                                <div id="div_1">
                                    0 a 30 Dias
                                </div>
                                
                                <div id="div_2">
                                    31 a 60 Dias
                                </div>
                            </div>
                            
                            <div id="div_c2">
                                <div id="div_3">
                                    Mas de 60 Dias
                                </div>
                                
                                <div id="div_4">
                                    No Ventas
                                </div>                            
                            </div>
                        </div>
                    
                    </td>
                
                </tr>
                <tr>
                    <td>
                        <div id="div_mayor1">
                            <div id="#div_title1" class="div_title1" >
                                Visitas(GPS)
                            </div>
                            <div id="div_gps1">
                                <div id="div_g1">
                                    0 min.
                                </div>
                                
                                <div id="div_g2">
                                    6 - 14 min.
                                </div>
                            </div>
                            
                            <div id="div_gps2">
                                <div id="div_g3">
                                    1 - 5 min.
                                </div>
                                
                                <div id="div_g4">
                                    Mas de 15 min.
                                </div>                            
                            </div>
                        </div>
                   </td>
                </tr>
            </table>
        </div>
    </div>
     <asp:Label ID="lblusuario" runat="server" Visible="False" Font-Names="arial" 
                Font-Size="Small"></asp:Label>
</asp:Content>
