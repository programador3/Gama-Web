<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page_error.aspx.cs" Inherits="presentacion.page_error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Error</title>
    <link rel="shortcut icon" href="imagenes/favicon.png" />
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Roboto:400,100,300,500" />
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/form-elements.css" />
    <link rel="stylesheet" href="assets/css/style.css" />
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/ico/apple-touch-icon-144-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/ico/apple-touch-icon-114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/ico/apple-touch-icon-72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" href="assets/ico/apple-touch-icon-57-precomposed.png" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="top-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12 form-box">
                        <div class="form-bottom">
                            <h1 style="text-align: center; color: white;"><strong>GAMA</strong> Materiales y Aceros</h1>
                            <div class="form-group">
                                <h2 style="text-align: center; color: white;">Oops!</h2>
                                <h4 style="text-align: center; color: white;">
                                    <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label></h4>
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/login.aspx" class="btn btn-block">Regresar al Inicio de Sesión</asp:LinkButton>
                            </div>
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="assets/js/jquery-1.11.1.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/js/jquery.backstretch.min.js"></script>
    <script src="assets/js/scripts.js"></script>
</body>
</html>