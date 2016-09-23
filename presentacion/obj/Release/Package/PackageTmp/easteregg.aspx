<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="easteregg.aspx.cs" Inherits="presentacion.easteregg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="js/jquery10.js"></script>
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            alert("Esto es solo un EasterEgg creado para probar el rendimiento de la pagina WEB.\n\n\n\nUsaremos sus Cookies, los datos de su equipo y navegador.")
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <iframe width="1920px" height="1020px" src="http://zty.pe/"></iframe>
    </form>
</body>
</html>