<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="presentacion.login" %>

<asp:Content ID="Contentheadlog" ContentPlaceHolderID="head" runat="server">
    <script>

        $(document).ready(function () {
            if (!navigator.onLine) {
                swal("Mensaje del Sistema", "Actualmente NO SE DETECTA una conexion a Internet.\nEsto podria hacer que la carga de la pagina sea lenta.\nPor Favor, Sea paciente!!", "info");
            } 
        });
        var value = 0;
        function ClicImg() {
            value++;
            if (value > 10 && value < 15) {
                var tot = 15 - value
                alert("Esta a " + tot + " clicks de ser un programador.")
            } else if (value > 14) {

                window.open("easteregg.aspx");
            }
        }

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
        body {
            font-size: 16px;
            font-weight: 300;
            background-color: #353d47;
            line-height: 30px;
            text-align: center;
        }

        #footer-zone {
            height: 50px;
            width: 100%;
            position: absolute;
            bottom: 0;
            left: 0;
        }

        .form-bottom {
            padding: 25px 25px 30px 25px;
            background: #444;
            background: rgba(0, 0, 0, 0.3);
            -moz-border-radius: 0 0 4px 4px;
            -webkit-border-radius: 0 0 4px 4px;
            border-radius: 0 0 4px 4px;
            text-align: left;
        }

            .form-bottom form textarea {
                height: 100px;
            }

            .form-bottom form .btn {
                width: 100%;
            }

            .form-bottom form .input-error {
                border-color: #00c0ef;
            }

        .top-content .text {
            color: #fff;
        }

            .top-content .text h1 {
                color: #fff;
            }

        .top-content .description {
            margin: 20px 0 10px 0;
        }

            .top-content .description p {
                opacity: 0.8;
            }

            .top-content .description a {
                color: #fff;
            }

                .top-content .description a:hover,
                .top-content .description a:focus {
                    border-bottom: 1px dotted #fff;
                }

        .form-box {
            margin-top: 35px;
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
            <!-- Top content -->
            <div class="top-content">
                <div class="container">
                    <div class="row" style="text-align: center;">
                        <div class="col-sm-3 col-md-4 col-lg-4"></div>
                        <div class="col-sm-12 col-md-4 col-lg-4 form-box">

                            <div class="form-bottom">
                                <div class="form-group" style="text-align: center;">
                                    <img onclick="ClicImg();" style="display: block; margin: 0 auto;" width="180"
                                        class="img-responsive" src="imagenes/logo_black.png" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtuser" onfocus="this.select()" Style="text-transform: uppercase; text-align: center;" runat="server" CssClass="form-control" placeholder="Usuario" required="Indique su Usuario" autofocus></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtpass" onfocus="this.select()" Style="text-transform: uppercase; text-align: center;" runat="server" CssClass="form-password form-control" placeholder="Contraseña" required="Escriba su contraseña" TextMode="Password"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnaceptar" runat="server" Style="font-size: 17px; font-weight: 400;"
                                    Text="Iniciar Sesión" CssClass="btn btn-info btn-block" OnClick="btnaceptar_Click" />
                                <asp:CheckBox ID="cbxRecordar" runat="server" Text="No Cerrar Sesión" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="footer-zone" style="text-align: center; color: white;">
        <h5>Versión
            <asp:Label ID="lblfooter" runat="server" Text="Footer"></asp:Label></h5>
    </div>
    <!-- /#page-wrapper -->
</asp:Content>