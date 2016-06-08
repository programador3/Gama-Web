<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="presentacion.login" %>

<asp:Content ID="Contentheadlog" ContentPlaceHolderID="head" runat="server">
    <script src="js/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="js/sweetalert.css" />
    <script>
        // convertimos en minusculas la cadena devuelta por navigator.userAgent
        var nav = navigator.userAgent.toLowerCase();
        //buscamos dentro de la cadena mediante indexOf() el identificador del navegador
        if (nav.indexOf("msie") != -1) {
            alert("Su navegador Internet Explorer no es 100 % compatible con este sistema. Utilize Google Chrome");
        } else if (nav.indexOf("firefox") != -1) {
            alert("Su navegador Firefox no es 100 % compatible con este sistema. Utilize Google Chrome");
        } else if (nav.indexOf("opera") != -1) {
            alert("Su navegador Opera no es 100 % compatible con este sistema. Utilize Google Chrome");
        }
    </script>
    <style type="text/css">
        #footer-zone {
            height: 50px;
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
            <asp:AsyncPostBackTrigger ControlID="lnkVerContraseña" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <!-- Top content -->
            <div class="top-content">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-12 col-md-2 col-lg-2 form-box"></div>
                        <div class="col-sm-12 col-md-8 col-lg-8 form-box">

                            <div class="form-bottom">
                                <div class="form-group" style="text-align: center;">
                                    <img style="display: block; margin: 0 auto;"
                                        class="img-responsive" src="imagenes/logo_black.png" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtuser" onfocus="this.select()" Style="text-transform: uppercase" runat="server" CssClass="form-username form-control" placeholder="Usuario" required="Indique su Usuario" autofocus></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtpass" onfocus="this.select()" Style="text-transform: uppercase" runat="server" CssClass="form-password form-control" placeholder="Contraseña" required="Escriba su contraseña" TextMode="Password"></asp:TextBox>

                                        <span class="input-group-addon" style="background-color: white;">
                                            <asp:LinkButton ID="lnkVerContraseña" Text="Mostrar" OnClick="lnkVerContraseña_Click" runat="server"></asp:LinkButton></span>
                                    </div>
                                </div>
                                <asp:Button ID="btnaceptar" runat="server" Text="Iniciar Sesión" CssClass="btn btn-block" OnClick="btnaceptar_Click" />
                                <asp:CheckBox ID="cbxRecordar" runat="server" Text="No Cerrar Sesión" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="footer-zone">
        <h5> Versión <asp:Label ID="lblfooter" runat="server" Text="Footer"></asp:Label></h5>
    </div>
    <!-- /#page-wrapper -->
</asp:Content>