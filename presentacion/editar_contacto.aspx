<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="editar_contacto.aspx.cs" Inherits="presentacion.editar_contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Ocultar {
            display:none;
        }
        .form-control3 {
            width: 100%;
            display: block;
            padding: 6px 12px;
            font-size: 11px;
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
       </style>
    <script type="text/javascript">

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
        function ScrollinEdit(div) {
            var posicion = $("#"+div+"").offset().top;
            $("html, body").animate({
                scrollTop: posicion
            }, 300);
            return true;
        }
          function ver_detalles() {
            var tabla = document.getElementById("<%=gridcontactos.ClientID%>");
            var cell;
            if (tabla == null) {
                return false;
            }
              var mybutton=document.getElementById ("<%=LinkButton6.ClientID %>");
            if (tabla.rows[0].cells[1].className == "Ocultar") {
                mybutton.innerHTML = "Ver Menos";
                tabla.rows[0].cells[1].className = "";
                tabla.rows[0].cells[2].className = "";
                tabla.rows[0].cells[6].className = "";
                tabla.rows[0].cells[7].className = "";
                tabla.rows[0].cells[8].className = "";
                tabla.rows[0].cells[9].className = "";
                tabla.rows[0].cells[10].className = "";
                for (var i = 1; i <= tabla.rows.length - 1; i++) {
                    tabla.rows[i].cells[1].className = "";
                    tabla.rows[i].cells[2].className = "";
                    tabla.rows[i].cells[6].className = "";
                    tabla.rows[i].cells[7].className = "";
                    tabla.rows[i].cells[8].className = "";
                    tabla.rows[i].cells[9].className = "";
                    tabla.rows[i].cells[10].className = "";
                }
            }
            else {
                mybutton.innerHTML = "Ver Mas";
                tabla.rows[0].cells[1].className = "Ocultar";
                tabla.rows[0].cells[2].className = "Ocultar";
                tabla.rows[0].cells[6].className = "Ocultar";
                tabla.rows[0].cells[7].className = "Ocultar";
                tabla.rows[0].cells[8].className = "Ocultar";
                tabla.rows[0].cells[9].className = "Ocultar";
                tabla.rows[0].cells[10].className = "Ocultar";
                for (var i = 1; i <= tabla.rows.length - 1; i++) {
                    tabla.rows[i].cells[1].className = "Ocultar";
                    tabla.rows[i].cells[2].className = "Ocultar";
                    tabla.rows[i].cells[6].className = "Ocultar";
                    tabla.rows[i].cells[7].className = "Ocultar";
                    tabla.rows[i].cells[8].className = "Ocultar";
                    tabla.rows[i].cells[9].className = "Ocultar";
                    tabla.rows[i].cells[10].className = "Ocultar";
                }
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class="page-header">
        Contactos
    </h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <div class="row">
                 <div class="col-lg-12">
                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Cliente</strong></h5>
                    <asp:TextBox  style="font-size:11px;" runat="server" ReadOnly="true" ID="txtcliente"
                        CssClass="form-control" onfocus="this.blur();"></asp:TextBox>
                    <label style="font-size:14px; width:49%"><strong>RFC</strong></label>
                      <label style="font-size:14px; width:49%"><strong>CVE</strong></label>
                    <asp:TextBox Width="49%" style="font-size:11px;" ReadOnly="true" runat="server" ID="txtrfc" onfocus="this.blur();"
                        CssClass="form-control2"></asp:TextBox>
                   
                    <asp:TextBox Width="49%"  style="font-size:11px;" ReadOnly="true" runat="server" ID="txtcve" onfocus="this.blur();"
                        CssClass="form-control2"></asp:TextBox>
                </div>
                
                <div class="col-lg-12" id="edicion" runat="server" visible="false">       
                    <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-primary" Width="100%" OnClientClick=" return ver_detalles();">Ver Mas&nbsp;</asp:LinkButton>
                                    
                    <div class="table table-responsive">
                        <asp:GridView ID="gridcontactos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="table table-responsive table-bordered table-condensed" Style="font-size: 11px;"
                            DataKeyNames="idc_solcambio" OnRowCommand="gridcontactos_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Editar" HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgeditar" runat="server" Height="30px"
                                            ImageUrl="imagenes/btn/more.png" Width="30px" CommandName="editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:BoundField DataField="tipo_contacto" HeaderText="Tipo de Contacto">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abreviatura" HeaderText="Titulo">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="nombre" HeaderText="Nombre" HeaderStyle-Width="180px"></asp:BoundField>
                                <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="celular" HeaderText="Celular" />
                                <asp:BoundField DataField="email" HeaderText="E-mail">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="funciones" HeaderText="Funciones">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_cumple" HeaderText="Cumpleaños">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="hobies" HeaderText="Hobbies">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="equipo" HeaderText="Equipo Favorito">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="idc_solcambio" HeaderText="idc">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="activo" HeaderText="activo">
                                    <HeaderStyle CssClass="Ocultar" />
                                    <ItemStyle CssClass="Ocultar" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <PagerSettings FirstPageText="" LastPageText="" />
                            <PagerStyle CssClass="PagerStyle" />
                            <RowStyle CssClass="AltRowStyle" ForeColor="Black" Font-Names="arial"
                                Font-Size="Small" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-lg-12" style="padding: 10px; font-size:10px;" id="nombrediv">
                    <div class="col-lg-12">
                        <h4 style="text-align: center;"><strong><i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;Datos Contacto</strong></h4>
                        <br />
                        <h5><strong>Nombre</strong></h5>
                        <asp:TextBox ID="txtnombre" runat="server" MaxLength="100"
                            CssClass="form-control"></asp:TextBox>
                        <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Tipo de Contacto</strong></h5>
                        <asp:DropDownList ID="cbotipo" runat="server" CssClass="form-control"
                            Width="100%">
                        </asp:DropDownList>

                        <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Titulo Profesional</strong></h5>
                        <asp:DropDownList ID="cbotitulo" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                        <h5><strong><i class="fa fa-phone" aria-hidden="true"></i>&nbsp;Telefono</strong></h5>
                        <asp:TextBox ID="txttelefono" MaxLength="20" TextMode="Number" runat="server" onkeypress="return isNumber(event);"
                            CssClass="form-control2" Height="30px" Width="73%"></asp:TextBox>
                        <asp:TextBox ID="txtext" runat="server" MaxLength="5" TextMode="Number" onkeypress="return isNumber(event);"
                            CssClass="form-control2" Height="30px" Width="25%"></asp:TextBox>

                        <h5><strong><i class="fa fa-mobile" aria-hidden="true"></i>&nbsp;Celular</strong></h5>
                        <asp:TextBox ID="txtcelular" MaxLength="20" runat="server" onkeypress="return isNumber(event);"
                            CssClass="form-control" TextMode="Number"></asp:TextBox>

                        <h5><strong><i class="fa fa-laptop" aria-hidden="true"></i>&nbsp;Email</strong></h5>
                        <asp:TextBox ID="txtemail" runat="server" TextMode="Email" onchange="return validarEmail(this);"
                            CssClass="form-control"></asp:TextBox>

                        <h5><strong>Funciones</strong></h5>
                        <asp:TextBox ID="txtfunciones" runat="server"
                            CssClass="form-control"></asp:TextBox>

                        <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Cumpleaños</strong></h5>
                        <asp:TextBox ID="txtcumple" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                        <asp:CheckBox ID="cbxsincumple" AutoPostBack="true" Text="Sin Cumpleaños" CssClass="radio3 radio-check radio-info radio-inline" OnCheckedChanged="cbxsincumple_CheckedChanged" runat="server" />

                        <h5><strong>Hobbies</strong></h5>
                        <asp:TextBox ID="txthobbies" runat="server" Style="resize: none;" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>


                        <h5><strong>Equipo Favorito</strong></h5>
                        <asp:TextBox ID="txtequipo" MaxLength="29" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:CheckBox ID="cbxactivo" AutoPostBack="true" Text="Activo" CssClass="radio3 radio-check radio-info radio-inline" OnCheckedChanged="cbxsincumple_CheckedChanged" runat="server" />

                    </div>
                </div>
               
                   <asp:TextBox ID="txttipo" runat ="server" Text ="" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtidc_telcli" runat="server" Text="" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtid" runat="server" Text="" Visible="false"></asp:TextBox>  
                     
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-block" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click"/>
        </div>
    </div>
            <!-- Modal -->
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
</asp:Content>
