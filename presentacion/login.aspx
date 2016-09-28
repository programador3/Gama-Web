<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="presentacion.login" %>

<asp:Content ID="Contentheadlog" ContentPlaceHolderID="head" runat="server">   
   
    <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>
    <style type="text/css">
         body {
            font-size: 16px;
            font-weight: 500;
            background-color: #353d47;
            line-height: 30px;
            text-align: center;
        }

        #footer-zone {
            width: 100%;
            position: absolute;
            bottom: 0;
            left: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Contentlog" ContentPlaceHolderID="ContentLogin" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnaceptar" EventName="Click" />
        </Triggers>
        <ContentTemplate>
          
            <div class="login-page">
                <div class="form">
                    <div class="login-form">

                        <asp:TextBox ID="txtuser" onfocus="this.select()" style="text-align:center;"
                            runat="server" CssClass="form-control" placeholder="Usuario" required="Indique su Usuario" autofocus></asp:TextBox>

                        <asp:TextBox ID="txtpass" onfocus="this.select()"  style="text-align:center;"
                            runat="server" CssClass="form-password form-control" placeholder="Contraseña" required="Escriba su contraseña" TextMode="Password"></asp:TextBox>

                        <asp:Button ID="btnaceptar" runat="server"
                                    Text="Iniciar Sesión" CssClass="bt" OnClick="btnaceptar_Click" />
                    </div>
                </div>
            </div>
            <!-- Top content -->
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="footer-zone" style="text-align: center; color: white;">
          <div class="form-group" style="text-align: center;">
                <img onclick="ClicImg();" style="display: block; margin: 0 auto;" width="150"
                    class="img-responsive" src="imagenes/logo_black.png" />
            </div>
        <h5>Sistema de Tareas <small>v.<asp:Label ID="lblfooter" runat="server" Text="Footer"></asp:Label></small></h5>
    </div>
    <!-- /#page-wrapper -->
</asp:Content>