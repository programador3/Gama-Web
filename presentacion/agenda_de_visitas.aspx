<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="agenda_de_visitas.aspx.cs" Inherits="presentacion.agenda_de_visitas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css" >
        select
        {
            text-align:center !important;
            text-transform:uppercase !important;	    	
        }
    </style>
    <script type="text/javascript"></script>
    <script type="text/javascript" >
        
        function Giftq(mensaje) {
            swal({ title: 'Espere un Momento...', text: mensaje, allowEscapeKey: false, imageUrl: 'imagenes/loading.gif', timer: '3000', showConfirmButton: false });
        }
        var myvar_guardando;
        function mostrar_procesar_guard()
        {
            myvar_guardando = setTimeout(function(){document.getElementById('div_guardando').style.display =''}, 0);
        }
        
        function myStopFunction_guard()
        {
            clearTimeout(myvar_guardando);
            document.getElementById('div_guardando').style.display ='none';
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <div style="text-align: center;">
             <h2 class=" page-header">
                 Agenda de Visitas
             </h2>
              <table style="width: 100%">
                  <tr>
                    <td>
                    <div id="Div2" class="styled-select" style="width:100%;">
                        <asp:DropDownList ID="cboagentes" runat="server" AutoPostBack="True" CssClass="form-control"
                                Font-Bold="True" ForeColor="Black" Height="35px" style="text-align:center" 
                                Width="100%" OnSelectedIndexChanged="cboagentes_SelectedIndexChanged">
                        </asp:DropDownList>     
                        <br />               
                    </div>
                        
                      </td>              
                  </tr>
                  <tr>
                    <td style="text-align:center;">
                    <div id="Div1" class="styled-select" style="width:100%;">
                        <asp:DropDownList ID="drpDays" runat="server" AutoPostBack="True" CssClass="form-control"
                            Font-Bold="True" ForeColor="Black" Height="35px" style="text-align:center !important;" 
                            Width="100%" OnSelectedIndexChanged="drpDays_SelectedIndexChanged">
                            <asp:ListItem Value="0">Todos los Días</asp:ListItem>
                            <asp:ListItem Value="1">Lunes</asp:ListItem>
                            <asp:ListItem Value="2">Martes</asp:ListItem>
                            <asp:ListItem Value="3">Miercoles</asp:ListItem>
                            <asp:ListItem Value="4">Jueves</asp:ListItem>
                            <asp:ListItem Value="5">Viernes</asp:ListItem>
                            <asp:ListItem Value="6">Sábado</asp:ListItem>
                            <asp:ListItem Value="7">Domingo</asp:ListItem>
                        </asp:DropDownList>   
                        <br />                 
                    </div>
                        
                      </td>              
                  </tr>
                  <tr>
                      <td>
                            <span id="div_guardando" style="display:none;text-align:center;width:100%;"> 
                                <table style="width:100%">
                                    <tr>
                                        <td  style="width:100%" align="center" >
                                            <img src="imagenes/loading.gif" alt="" id="Img3" align="middle" height="50px" width="50px"/>
                                        </td>                                    
                                    </tr>
                                    <tr>
                                        <td valign="bottom" style="font-family:Arial;font-weight:bold;font-size:small;color:steelblue;width:100%;height:40px;" align="center">
                                                Filtrando Clientes...
                                        </td>                                    
                                    </tr>
                                </table>
                            </span>
                      
                      </td>
                  </tr>
                  <tr>
                      <td>
                        <div class="table table-responsive">
                              <asp:GridView ID="grdClientes" runat="server" CssClass="table table-responsive table-bordered table-condensed "  AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" PageSize="50" 
                              Width="100%">                             
                              <HeaderStyle Font-Names="arial" ForeColor="White" BackColor="Gray"
                              Font-Size="Small" />
                              <Columns>
                                  <asp:BoundField DataField="DIA" HeaderText="Dia" SortExpression="DIA">
                                      <HeaderStyle CssClass="HeaderStyle" Width="100px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="RFCCLIENTE" HeaderText="R.F.C." 
                                      SortExpression="RFCCLIENTE">
                                      <HeaderStyle Width="150px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="CVEADI" HeaderText="Cve. Adi" 
                                      SortExpression="CVEADI">
                                      <HeaderStyle Width="300px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE">
                                      <HeaderStyle Width="400px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="CALLE_NO" HeaderText="Calle y No." 
                                      SortExpression="CALLE_NO">
                                      <HeaderStyle Width="400px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="COLONIA" HeaderText="Colonia" 
                                      SortExpression="COLONIA">
                                      <HeaderStyle Width="400px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="MPIO" HeaderText="Municipio" SortExpression="MPIO">
                                      <HeaderStyle Width="300px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="EDO" HeaderText="Estado" SortExpression="EDO">
                                      <HeaderStyle Width="150px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="TELEFONO" HeaderText="Telefono" 
                                      SortExpression="TELEFONO">
                                      <HeaderStyle Width="100px" />
                                  </asp:BoundField>
                              </Columns>
                            
                          </asp:GridView>
                        </div>
                      </td>
                  </tr>
                  <tr>
                      <td>
                          <asp:Button ID="btncerrar" runat="server" 
                              CssClass="btn btn-danger"  Text="Salir" ToolTip="Cerrar" 
                              Width="100%" OnClick="btncerrar_Click" />
                      </td>
                  </tr>
              </table>
          </div>
</asp:Content>
