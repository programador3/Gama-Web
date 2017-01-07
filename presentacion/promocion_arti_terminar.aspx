<%@ Page Language="VB" MasterPageFile="~/Global.Master" AutoEventWireup="false" CodeFile="promocion_arti_terminar_m.aspx.vb" Inherits="promocion_arti_terminar_m" title="" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" Runat="Server">
<script type="text/javascript" >
    
    function swipe() {
        var largeImage =  document.getElementById('<%=imgprincipal.ClientID%>'); 
        var url=largeImage.getAttribute('src');
        window.open(url);
        return false
    }

    function Go(dir)
    {
        window.open(dir);
        return false;
    }

    function datos(dir)
    {
        var griddatos = document.getElementById('<%=griddatos.ClientID%>');
        var txtindex = document.getElementById('<%=txtindex.ClientID%>');
        if(dir==0)
        {
            txtindex.value=parseInt(txtindex.value) - 1;
        }        
        else if(dir==1)
        {
            txtindex.value=parseInt(txtindex.value) + 1;
        }
        else
        {
            txtindex.value =1;
        }
        
        var txtfec = document.getElementById('<%=txtfec.ClientID%>');
        var txtdias = document.getElementById('<%=txtdias.ClientID%>');
        var txtmul = document.getElementById('<%=txtmul.ClientID%>');
        var txtum = document.getElementById('<%=txtum.ClientID%>');
        var txtcodigo = document.getElementById('<%=txtcodigo.ClientID%>');
        var txtart = document.getElementById('<%=txtart.ClientID%>');     
        var txtlen1 = document.getElementById('<%=txtlen1.ClientID%>');
        var txtlen = document.getElementById('<%=txtlen.ClientID%>');   
        var imagen =  document.getElementById('<%=imgprincipal.ClientID%>');  

        var idc_promocion;
        if(griddatos!=null)
        {
            if(txtindex.value<=0 && dir==0)
            {
                txtindex.value=1;
                return false;
            }
            if(txtindex.value>griddatos.rows.length-1)
            {
                txtindex.value=griddatos.rows.length-1;
                return false;
            }
            else
            {
                txtlen1.value =String(txtindex.value) + ' de ' + String(griddatos.rows.length-1);
                txtlen.value = String(txtindex.value) + ' de ' + String(griddatos.rows.length-1);
                scrollTo(0,100);
            }
            for(var i = 1;i<=griddatos.rows.length-1;i++)
            {
                if(i==txtindex.value)
                {
                    idc_promocion= griddatos.rows[i].cells[0].textContent;
                    txtfec.value = griddatos.rows[i].cells[3].textContent;
                    txtdias.value = griddatos.rows[i].cells[8].textContent;
                    txtmul.value = griddatos.rows[i].cells[4].textContent;
                    txtum.value = griddatos.rows[i].cells[7].textContent;
                    txtcodigo.value = griddatos.rows[i].cells[5].textContent;
                    txtart.value = griddatos.rows[i].cells[6].textContent;
                    imagen.setAttribute('src', griddatos.rows[i].cells[12].textContent)
                    procesar_grids(idc_promocion);                  
                }
            }
        
        }   
        return false;   
    }
    
    function procesar_grids(idc_promocion)
    {
    
       
        var DataGrid1 = document.getElementById('<%=DataGrid1.ClientID%>');
        var DataGrid2 = document.getElementById('<%=DataGrid2.ClientID%>');
        var DataGrid3 = document.getElementById('<%=DataGrid3.ClientID%>');
        
        if(DataGrid1!=null)
        {
            for(var i = 1;i<=DataGrid1.rows.length-1;i++)
            {
                if(idc_promocion==DataGrid1.rows[i].cells[5].textContent)
                {
                    DataGrid1.rows[i].className="";
                }
                else
                {
                    DataGrid1.rows[i].className="Ocultar";
                }
            }
        }
        
        if(DataGrid2!=null)
        {
            for(var i = 1;i<=DataGrid2.rows.length-1;i++)
            {
                if(idc_promocion==DataGrid2.rows[i].cells[2].textContent)
                {
                    DataGrid2.rows[i].className="";
                }
                else
                {
                    DataGrid2.rows[i].className="Ocultar";
                }
            }
            
        }
        
        if(DataGrid3!=null)
        {
            for(var i = 1;i<=DataGrid3.rows.length-1;i++)
            {
                if(idc_promocion==DataGrid3.rows[i].cells[3].textContent)
                {
                    DataGrid3.rows[i].className="";
                }
                else
                {
                    DataGrid3.rows[i].className="Ocultar";
                }
            }
        
        }
        
    
    }

      function imgError(me) {
            var host = $(location).attr('href');
            var values;
            values = host.split('?')[1];
            var imagen;
            imagen = "/imagenes/btn/logo-gama.png";
            var AlterNativeImg = host.replace("promocion_arti_terminar.aspx#no-back-button", "").replace(values, "").replace("?", "") + imagen;

            // to avoid the case that even the alternative fails        
            if (AlterNativeImg != me.src)
                me.src = AlterNativeImg;
        }


</script>
    <style type="text/css">
        .imcenter {
	        margin-left: auto;
	        margin-right: auto;
	        display: block;
        }
    </style>

    <div style="position: relative; top: 0px; right: 0px; left: 0px; bottom: 0px;">
        <h2 class=" page-header">
            <asp:Label ID="lblheader" runat="server" Text="Promociones Por Terminar"></asp:Label>
        </h2>

        <table style="width: 100%;">
            <tr>
                <td>

                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 33.3%">

                                <asp:Button ID="btnanterior1" runat="server"
                                    CssClass="btn btn-default"
                                    Font-Bold="true" Font-Names="arial" Font-Size="Small" ForeColor="Black"
                                    Height="35px" Text="&lt;&lt;" Width="100%" />
                            </td>
                            <td style="width: 33.4%">

                                <asp:TextBox ID="txtlen1" runat="server"
                                    CssClass="form-control2" onfocus="this.blur();"
                                    Font-Bold="True" Font-Size="Small" ForeColor="Blue" Style="display: inline !important; text-align: center"
                                    ReadOnly="True" Width="100%"></asp:TextBox>

                            </td>
                            <td style="width: 33.3%">
                                <asp:Button ID="btnsiguiente1" runat="server"
                                    CssClass="btn btn-default"
                                    Font-Bold="true" Font-Names="arial" Font-Size="Small" ForeColor="Black"
                                    Height="35px" Text="&gt;&gt;" Width="100%" />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td>

                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 4%" align="right">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Vigencia:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfec" runat="server"
                                    CssClass="form-control2" onfocus="this.blur();"
                                    Font-Bold="True" Font-Size="Small" ForeColor="Black" Style="display: inline !important;"
                                    ReadOnly="True" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="Small" Text="Dias:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdias" runat="server"
                                    CssClass="form-control2" onfocus="this.blur();"
                                    Font-Bold="True" Font-Size="Small" ForeColor="Black" Style="display: inline !important;"
                                    ReadOnly="True" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="padding-left: 4px">

                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" Text="En La Compra De:"></asp:Label>

                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:ImageButton ID="imgprincipal" onerror="imgError(this);"
                        OnClientClick="return swipe();" 
                        CssClass=" img-responsive imcenter" style="max-height:250px;border:1px solid gray;" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>

                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%">
                                <asp:TextBox ID="txtmul" runat="server"
                                    CssClass="form-control2" onfocus="this.blur();"
                                    Font-Bold="True" Font-Size="Small" ForeColor="Black" Style="display: inline !important;"
                                    ReadOnly="True" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtum" runat="server"
                                    CssClass="form-control2" onfocus="this.blur();"
                                    Font-Bold="True" Font-Size="Small" ForeColor="Black" Style="display: inline !important;"
                                    ReadOnly="True" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtcodigo" runat="server"
                                    CssClass="form-control2" onfocus="this.blur();"
                                    Font-Bold="True" Font-Size="Small" ForeColor="Black" Style="display: inline !important;"
                                    ReadOnly="True" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtart" runat="server"
                                    CssClass="form-control2" onfocus="this.blur();"
                                    Font-Bold="True" Font-Size="Small" ForeColor="Black" Style="display: inline !important;"
                                    ReadOnly="True" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="padding-left: 4px;">

                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="Small" Text="Se Regala:"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="DataGrid1" runat="server" BackColor="White" CssClass="table table-responsive table-bordered table-condensed" Width="100%" Style="font-size:11px"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="img" CssClass=" img-responsive imcenter" 
                                        style="max-width:50px;" runat="server"  onerror="imgError(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigo" HeaderText="Codigo"></asp:BoundField>
                            <asp:BoundField DataField="desart" HeaderText="Descripcion"></asp:BoundField>
                            <asp:BoundField DataField="um" HeaderText="UM"></asp:BoundField>
                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad"></asp:BoundField>
                            <asp:BoundField DataField="idc_promocion">
                                <HeaderStyle CssClass="Ocultar" />
                                <ItemStyle CssClass="Ocultar" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="RowStyle" />
                    </asp:GridView>

                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DataGrid2" runat="server" BackColor="White"  CssClass="table table-responsive table-bordered table-condensed"  Width="100%" Style="margin-bottom: 5px;"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundColumn DataField="idc_listap"></asp:BoundColumn>
                            <asp:BoundColumn DataField="descripcion" HeaderText="Lista"></asp:BoundColumn>
                            <asp:BoundColumn DataField="idc_promocion">
                                <HeaderStyle CssClass="Ocultar" />
                                <ItemStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="RowStyle" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DataGrid3" runat="server" BackColor="White"  CssClass="table table-responsive table-bordered table-condensed"  Width="100%" Style="margin-bottom: 5px;"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundColumn DataField="rfccliente" HeaderText="RFC">
                                <ItemStyle Font-Size="Small" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="cveadi" HeaderText="CVE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="nombre" HeaderText="Cliente">
                                <ItemStyle Font-Size="Small" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="idc_promocion">
                                <HeaderStyle CssClass="Ocultar" />
                                <ItemStyle CssClass="Ocultar" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="RowStyle" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td>

                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 33.3%">

                                <asp:Button ID="btnanterior" runat="server"
                                    CssClass="btn btn-default"
                                    Font-Bold="true" Font-Names="arial" Font-Size="Small" ForeColor="Black"
                                    Height="35px" Text="&lt;&lt;" Width="100%" />
                            </td>
                            <td style="width: 33.4%">

                                <asp:TextBox ID="txtlen" runat="server"
                                    CssClass="form-control2" onfocus="this.blur();"
                                    Font-Bold="True" Font-Size="Small" ForeColor="Blue" Style="display: inline !important; text-align: center"
                                    ReadOnly="True" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 33.3%">
                                <asp:Button ID="btnsiguiente" runat="server"
                                    CssClass="btn btn-default"
                                    Font-Bold="true" Font-Names="arial" Font-Size="Small" ForeColor="Black"
                                    Height="35px" Text="&gt;&gt;" Width="100%" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:TextBox ID="txtindex" runat="server" CssClass="Ocultar"></asp:TextBox>
                    <asp:DataGrid ID="griddatos" runat="server" CssClass="Ocultar">
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>




