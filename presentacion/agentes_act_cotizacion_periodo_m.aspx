<%@ Page Language="VB" MasterPageFile="~/Global.Master" AutoEventWireup="false" CodeFile="agentes_act_cotizacion_periodo_m.aspx.vb" Inherits="agentes_act_cotizacion_periodo_m" title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" Runat="Server">

    <script type="text/javascript" >
        var myvar_guardando;
        function mostrar_procesar_guard()
        {
            myvar_guardando = setTimeout(function(){document.getElementById('div_guardando').style.display =''}, 0);
        }
        
        function myStopFunction_guard()
        {
            clearTimeout(myvar_busq);
            document.getElementById('div_guardando').style.display ='none';
        }
    
        function cargar_grid()
        {
            var finicial = $("#<%=txtfinicial.ClientID%>").val();
            var ffinal =  $("#<%=txtffinal.ClientID%>").val();
            var cboagentes = document.getElementById('<%=cboagentes.ClientID%>');
            var btng = document.getElementById('<%=btng.ClientID%>');
                      
            if (finicial == '')
            {
                swal('Mensaje del Sistema','Seleccione la Fecha Inicial.','info');
                return false;
            }
            else if (ffinal == '') {
                swal('Mensaje del Sistema', 'Seleccione la Fecha Final.', 'info');
                return false;
            }
            else if (cboagentes.options.length <= 0) {
                swal('Mensaje del Sistema', 'Seleccione un Agente.', 'info');
                return false;
            } else {

                mostrar_procesar_guard();
            }            
        }
        
        function read_grid()
        {
            var cboclientes = document.getElementById('<%=cboclientes.ClientID%>');
            var griddatos = document.getElementById('<%=griddatos.ClientID%>');
            if(griddatos!=null)
            {
                var idc_cliente = cboclientes.options[cboclientes.selectedIndex].value;
                for(var i= 1;i<=griddatos.rows.length-1;i++)
                {
                    if(idc_cliente==griddatos.rows[i].cells[12].textContent)
                    {
                        griddatos.rows[i].className="";
                    }
                    else
                    {
                        griddatos.rows[i].className="Ocultar";
                    }                
                }
            }        
        }
        
        function detalles(index)
        {
            var txtindex = document.getElementById('<%=txtindex.ClientID%>');
            txtindex.value = index;
            marcar_rows();
            cargar_obs(index);
            window.open('detalles_agente_cot.aspx');
            return false;            
        }
        
        function marcar_rows()
        {
            var txtindex = document.getElementById('<%=txtindex.ClientID%>');
            var griddatos = document.getElementById('<%=griddatos.ClientID%>');
            if(griddatos!=null)            
            {
                for(var i = 1;i<=griddatos.rows.length-1;i++)
                {
                    if(i==txtindex.value)
                    {
                        griddatos.rows[i].style.backgroundColor="rgb(141, 141, 141)";
                    }
                    else
                    {
                        griddatos.rows[i].style.backgroundColor="white";
                    }                
                }
            }
        }
        function cargar_obs(index)
        {
            var griddatos = document.getElementById('<%=griddatos.ClientID%>');
            var txtobs = document.getElementById('<%=txtobs.ClientID%>');
            var txtindex = document.getElementById('<%=txtindex.ClientID%>').value = index;
            if(griddatos!=null)  
            {
                for(var i = 1;i<=griddatos.rows.length-1;i++)
                {
                    if(i==index)
                    {
                        txtobs.value=griddatos.rows[i].cells[11].textContent;
                        marcar_rows();
                    }              
                }
            }            
        }
    </script>


    <div style="width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px;">
        <h3><strong>Negociaciones de Vendedores</strong></h3>
        <table align="left" style="width: 100%">
            <tr>
                <td align="left" style="width: 96%">
                     <i class="fa fa-user" aria-hidden="true"></i>&nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" ForeColor="Black" Text="Agente:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <div class="styled-select" style="width: 100%;">
                        <asp:DropDownList ID="cboagentes" runat="server" AutoPostBack="false"
                            CssClass="form-control" Width="100%">
                        </asp:DropDownList>
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                   <i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" ForeColor="Black" Text="Fecha Inicial:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 100%">
                    <asp:TextBox ID="txtfinicial" CssClass=" form-control" TextMode="Date" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                     <i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" ForeColor="Black" Text="Fecha Final:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 100%">
                    <asp:TextBox ID="txtffinal" CssClass=" form-control" TextMode="Date" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <asp:Button ID="btnejecutar" runat="server"
                        CssClass="btn btn-danger btn-block" OnClientClick="return cargar_grid();"
                         Text="Ver" Width="100%" />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <span id="div_guardando" style="display: none; text-align: center; width: 100%;">
                        <table style="width: 100%">
                            <tr>
                                <td align="center">
                                    <img src="imagenes/loading.gif" alt="" id="Img3" align="middle" height="40px" width="40px" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" align="center" style="font-family: Arial; font-weight: bold; font-size: small; color: steelblue;">Cargando Información...</td>
                            </tr>
                        </table>
                    </span>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <div id="div_cboclientes" runat="server" class="styled-select" style="width: 100%;">
                        <asp:DropDownList ID="cboclientes" runat="server" CssClass="form-control" Width="100%">
                        </asp:DropDownList>
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 5%">
                                <asp:TextBox ID="TextBox2" Style="background-color: Blue; border-radius: 5px; border: none;" onfocus="this.blur();" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="Small" ForeColor="Black" Text="Articulo de Grupo"></asp:Label>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 5%">
                                <asp:TextBox ID="TextBox3" Style="background-color: Red; border-radius: 5px; border: none;" onfocus="this.blur();" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="Small" ForeColor="Black" Text="Precio &lt; Precio Minimo"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <asp:DataGrid ID="griddatos" runat="server" Width="100%" style="font-size:12px; background-color:white" CssClass="table table-responsive table-bordered table-condensed"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgdet" runat="server" Height="20px"
                                        ImageUrl="~/imagenes/btn/File_info48.png" Width="20px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="desart" HeaderText="Descripcion">
                                <ItemStyle Font-Size="7pt" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="unimed" HeaderText="UM">
                                <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="precio" HeaderText="Precio Autorizar" DataFormatString="{0:N4}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="status" HeaderText="Status">
                                <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="precio_cliente" HeaderText="Precio Acual" DataFormatString="{0:N4}">
                                <ItemStyle CssClass="Ocultar" />
                                <HeaderStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="premin" HeaderText="Precio Minimo" DataFormatString="{0:N4}">
                                <ItemStyle CssClass="Ocultar" />
                                <HeaderStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="uti_deseada" HeaderText="Ulti Des">
                                <ItemStyle CssClass="Ocultar" />
                                <HeaderStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ult_precio" HeaderText="Ulti Precio">
                                <ItemStyle CssClass="Ocultar" />
                                <HeaderStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fecha_ult_precio" HeaderText="Fecha">
                                <ItemStyle CssClass="Ocultar" />
                                <HeaderStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="idc_gpoart" HeaderText="gpo">
                                <ItemStyle CssClass="Ocultar" />
                                <HeaderStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="obs" HeaderText="obs">
                                <ItemStyle CssClass="Ocultar" />
                                <HeaderStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="idc_cliente" HeaderText="idc_cliente">
                                <ItemStyle CssClass="Ocultar" />
                                <HeaderStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" ForeColor="Black" Text="Observaciones:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <asp:TextBox ID="txtobs" runat="server" Width="100%" onfocus="this.blur();" Style="resize: none;"
                        CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini" Height="70px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%">
                    <asp:Button ID="btng" runat="server" CssClass="Ocultar" />
                    <asp:TextBox ID="txtindex" runat="server" CssClass="Ocultar"></asp:TextBox>
                </td>
            </tr>
        </table>


    </div>
</asp:Content>

