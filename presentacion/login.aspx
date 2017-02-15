<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="presentacion.login" %>

<asp:Content ID="Contentheadlog" ContentPlaceHolderID="head" runat="server">   
   
    <link href='http://fonts.googleapis.com/css?family=Roboto+Condensed:300,400' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900' rel='stylesheet' type='text/css' />  
    <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>
    <style type="text/css">
         body {
            text-align: center;
            background:#1976d2 ;
        }

        #footer-zone {
            width: 100%;
            position: absolute;
            bottom: 0;
            left: 0;
        }
        .login-page {
            margin: auto;
            padding: 10% 0 0;
            max-width: 340px;
        }
        .form input {
          outline: 0;
          background:#1565c0  ;
          color:white;
          width: 100%;
          border: 0;
          margin: 0 0 15px;
          padding: 13px;
          box-sizing: border-box;
          font-size: 14px;
        }
        .form .bt {
          background:#0d47a1;
          width: 100%;
        }
        .form .bt:hover,.form .bt:active,.form .bt:focus {
          background: #1565c0   ;
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
                        <div class="form-group" style="text-align: center; height: 50%">
                            <img onclick="ClicImg();" style="display: block; margin: 0 auto; max-width:150px"
                                class="img-responsive" src="imagenes/acount.png?p=9" />
                        </div>
                        <br />
                       
                        <asp:TextBox ID="txtuser" onfocus="this.select()" Style="text-align: center;"
                            runat="server" CssClass="form-control" placeholder="Usuario" required="Indique su Usuario" autofocus></asp:TextBox>
                        <h5 id="tit1" runat="server" visible="false" style="color:white;">Bienvenido de Nuevo </h5>
                        <h6 id="tit2" runat="server" visible="false" style="color:white;">
                        <asp:Label style="color:white;" ID="lblnamecooki" runat="server" Text="Label"></asp:Label></h6>
                        <asp:TextBox ID="txtpass" onfocus="this.select()" Style="text-align: center;"
                            runat="server" CssClass="form-password form-control" placeholder="Contraseña" required="Escriba su contraseña" TextMode="Password"></asp:TextBox>

                        <asp:Button ID="btnaceptar" runat="server"
                            Text="Iniciar Sesión" CssClass="waves-effect waves-light btn-large bt " OnClick="btnaceptar_Click" />
                        <h6>   <asp:LinkButton CssClass="waves-effect waves-light btn" Style="background: #37474f  ; height:30px; font-size:12px"
            Visible="false" ID="lnlotracuenta" runat="server" OnClick="lnlotracuenta_Click">Iniciar Sesion con otra Cuenta</asp:LinkButton></h6>
                        <br />
                        <br />
                        <br />
                        <asp:LinkButton ID="lbklimpiarsession" CssClass="waves-effect waves-light red darken-1 btn" runat="server" OnClick="lbklimpiarsession_Click">Cerrar Sesión Iniciada</asp:LinkButton>
                           </div>
                </div>
            </div>
            <!-- Top content -->
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="footer-zone" style="text-align: center; color: white;">
      
        <h6>Sistema GAMA Web <small>v.<asp:Label ID="lblfooter" runat="server" Text="Footer"></asp:Label></small></h6>
    </div>
    <!-- /#page-wrapper -->
</asp:Content>