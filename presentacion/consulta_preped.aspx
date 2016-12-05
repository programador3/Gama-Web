<%@ Page Title="" Language="C#" MasterPageFile="~/Adicional.Master" AutoEventWireup="true" CodeBehind="consulta_preped.aspx.cs" Inherits="presentacion.consulta_preped1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
   <div class="container">
       <h3 class=" text-center"><strong>Pre Pedido</strong></h3>
       <asp:Button ID="btncerrar" runat="server" Text="Cerrar" CssClass="btn btn-danger btn-block" onclientclick="window.close();" />
         <table style="width:100%;">
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td style="width: 34px">
                            <asp:Label ID="Label2" runat="server" Text="Pre-Pedido:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" Width="75px"></asp:Label>
                        </td>
                        <td align="left" width="170">
                            <asp:TextBox ID="txtprepedido" runat="server" onfocus="this.blur();" 
                                ForeColor="White" Font-Names="Arial" BackColor="#3399FF" 
                                CssClass=" form-control2" Font-Bold="True" Font-Size="12pt" Width="100px"></asp:TextBox>
                        </td>
                        <td align="right" style="width: 120px">
                            <asp:RadioButton ID="rde" runat="server" Font-Names="arial" 
                                Font-Size="10pt" Text="Espera" onclick="return false;" GroupName="tipo" />
                        </td>
                        <td style="width: 136px">
                            <asp:RadioButton ID="rda" runat="server" Font-Names="arial" 
                                Font-Size="10pt" Text="Autorizado" onclick="return false;" Checked="True" 
                                GroupName="tipo" />
                        </td>
                        <td align="left">
                            <asp:RadioButton ID="rdno" runat="server" Font-Names="arial" 
                                Font-Size="10pt" Text="No Autorizado" onclick="return false;" 
                                GroupName="tipo" />
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="Id. Cliente:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" Width="70px"></asp:Label>
                        </td>
                        <td style="width: 101px">
                            <asp:TextBox ID="txtidc_cli" runat="server" onfocus="this.blur();" CssClass="form-control2" 
                                ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td style="width: 14px">
                            <asp:Label ID="Label4" runat="server" Text="RFC:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt"></asp:Label>
                        </td>
                        <td style="width: 110px">
                            <asp:TextBox ID="txtrfc" runat="server" onfocus="this.blur();" ForeColor="Black" CssClass="form-control2"  Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td style="width: 5px">
                            <asp:Label ID="Label5" runat="server" Text="Cve. Adi:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" Width="60px"></asp:Label>
                        </td>
                        <td style="width: 74px">
                            <asp:TextBox ID="txtcve" CssClass="form-control2"  runat="server" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="Nombre:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" Width="53px"></asp:Label>
                        </td>
                        <td style="width: 187px">
                            <asp:TextBox ID="txtnombre" CssClass="form-control2" runat="server" Width="400px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Agente:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" Width="48px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtagente" CssClass="form-control2" runat="server" Width="80px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtagenten" CssClass="form-control2" runat="server" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <table style="width:81%;">
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="Fecha:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt"></asp:Label>
                        </td>
                        <td style="width: 187px">
                            <asp:TextBox CssClass="form-control2" ID="txtfeha2" runat="server" Width="140px" onfocus="this.blur();" 
                                ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Fecha de Entrega:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" Width="117px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control2" ID="txtfechae" runat="server" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Usuario:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control2" ID="txtusu" runat="server" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkoc" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt" Font-Underline="True" ForeColor="Blue" Text="Ver OC" 
                                Width="75px" Checked="true" onclick="return false;"/>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <table>
                    <tr>
                        <td style="width: 65px" align="right">
                            <asp:Label ID="Label12" runat="server" Text="Sucursal:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt"></asp:Label>
                        </td>
                        <td style="width: 60px">
                            <asp:TextBox CssClass="form-control2"  ID="txtsuci" runat="server" Width="55px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td style="width: 341px">
                            <asp:TextBox CssClass="form-control2" ID="txtsucn" runat="server" Width="350px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Monto:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt"></asp:Label>
                        </td>
                        <td style="width: 111px">
                            <asp:TextBox CssClass="form-control2" ID="txtmonto" runat="server" Width="117px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkcroquis" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt" Font-Underline="True" ForeColor="Blue" Text="Ver Croquis" 
                                Width="95px" onclick="return false;" Checked="True"/>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <div class=" table table-responsive">
                    <asp:DataGrid ID="gridprod" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive table-bordered table-condensed"
                    Width="100%">
                    <Columns>
                        <asp:BoundColumn HeaderText="Codigo" DataField="codigo"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Descripcion" DataField="desart"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="U.M." DataField="unimed"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Cantidad" DataField="cantidad" DataFormatString="{0:N3}">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Precio" HeaderStyle-HorizontalAlign="Right"  DataField="precio" DataFormatString="{0:N4}">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Importe" HeaderStyle-HorizontalAlign="Right"  DataField="Importe" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Precio Real" HeaderStyle-HorizontalAlign="Right"  DataField="precioreal" DataFormatString="{0:N4}">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Descuento" HeaderStyle-HorizontalAlign="Right"  DataField="descuento" DataFormatString="{0:N4}">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle CssClass="RowStyle" />
                </asp:DataGrid>
                </div>
                <br />
            </td>
        </tr>
        <tr>
            <td>
            
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 600px">
                            <table style="width: 100%; height: 103px;" width="100%">
                                <tr>
                                    <td style="width: 29px" align="left" valign="top">
                                        <asp:Panel ID="Panel3" runat="server" Width="205px">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblmotivo" runat="server" Font-Bold="True" 
                                                            Text="Motivo Cancelacion:" Width="131px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtmotivo" runat="server" Height="56px" style="resize:none;" 
                                                            TextMode="MultiLine" Width="197px" onfocus="this.blur();"   CssClass="form-control2"
                                                            ForeColor="Black" Font-Names="Arial"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 84px">
                                        <asp:Panel ID="Panel2" runat="server" Width="240px">
                                            <table>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblpedido" runat="server" Text="Pedido:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" Font-Underline="True" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox  CssClass="form-control2" ID="txtpedido" runat="server" Width="69px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblfecha" runat="server" Text="Fecha:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox  CssClass="form-control2" ID="txtfecha" runat="server" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <asp:Label ID="lblobs" runat="server" Font-Bold="True" Font-Names="arial" 
                                                            Font-Size="10pt" Text="Obs:"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox  CssClass="form-control2" ID="txtobs" runat="server" Font-Names="Arial" ForeColor="Black" 
                                                            Height="50px" onfocus="this.blur();" style="resize: none;" TextMode="MultiLine" 
                                                            Width="178px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                            <asp:Label ID="lblusuario" runat="server" Text="Usuario:" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt"></asp:Label>
                                    </td>
                                    <td style="width: 84px" align="left">
                            <asp:TextBox ID="txtusuario"  CssClass="form-control2" runat="server" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial"></asp:TextBox>
                                    </td>
                                </tr>
                                </table>
                        </td>
                        <td align="right" valign="top">
            
                <table style="width:100%;" align="right">
                    <tr>
                        <td width="500">
                            &nbsp;</td>
                        <td bgcolor="#3399FF" style="background-color: #3399FF" align="center">
                            <asp:Label ID="Label16" runat="server" Text="Subtotal" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" Width="75px" ForeColor="White"></asp:Label>
                        </td>
                        <td style="background-color: #3399FF" align="center">
                            <asp:Label ID="lbliva" runat="server" Text="I.V.A." Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" ForeColor="White"></asp:Label>
                        </td>
                        <td style="background-color: #3399FF" align="center">
                            <asp:Label ID="Label14" runat="server" Text="Total" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" Width="75px" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtsub1" runat="server" CssClass="form-control2"  
                                Width="110px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" 
                                Font-Bold="True"></asp:TextBox>
                        </td>
                        <td> 
                            <asp:TextBox ID="txtiva1" runat="server" CssClass="form-control2" 
                                Width="110px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" 
                                Font-Bold="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txttotal1" runat="server" CssClass="form-control2" 
                                Width="110px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" 
                                Font-Bold="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label24" runat="server" Text="Nota de Credito" Font-Bold="True" 
                                Font-Names="arial" Font-Size="10pt" ForeColor="Red" Width="100px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtnota1" runat="server" CssClass="form-control2" 
                                Width="110px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" 
                                Font-Bold="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtnota2" runat="server" CssClass="form-control2" 
                                Width="110px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" 
                                Font-Bold="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtnota3" runat="server" CssClass="form-control2" 
                                Width="110px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" 
                                Font-Bold="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtsub2" runat="server" CssClass="form-control2" 
                                Width="110px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" 
                                Font-Bold="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtiva2" runat="server" CssClass="form-control2" 
                                Width="110px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" 
                                Font-Bold="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txttotal2" runat="server" CssClass="form-control2" 
                                Width="110px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" 
                                Font-Bold="True"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td style="width: 35px" align="right" valign="top">
                            &nbsp;</td>
                        <td style="width: 108px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td width="50%">
                            <asp:Panel ID="Panel1" runat="server" GroupingText="Cambio de Tipo" style="border:1px  solid gray; padding:5px; background-color:white;"
                                HorizontalAlign="Left">

                                <table style="width:100%;">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="arial" 
                                                Font-Size="10pt" Text="Observaciones:" Width="98px"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="arial" 
                                                Font-Size="10pt" Text="Fecha:" Width="52px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox CssClass="form-control2"  ID="txtfechapanel" runat="server" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:TextBox CssClass="form-control2"  ID="txtobservaciones" runat="server" Height="50px" TextMode="MultiLine" style="resize:none;"
                                                Width="500px" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial"></asp:TextBox>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="arial" 
                                                Font-Size="10pt" Text="Usuario:" Width="50px"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox CssClass="form-control2"  ID="txtusuariopanel" runat="server" onfocus="this.blur();" ForeColor="Black" Font-Names="Arial" Height="20px" Font-Size="11pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   </div>
</asp:Content>
