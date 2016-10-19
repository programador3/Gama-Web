<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IMG_Croquis.aspx.cs" Inherits="presentacion.IMG_Croquis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Croquis</title>
    <style type="text/css">
        body {
            font-family: 'Roboto Condensed', sans-serif;
        }
        .Ocultar {
            display:none;
        }
        .form-control2 {
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
        </style>
    <script type="text/javascript">
        function imgError(me) {
            //console.log('entro');
            // place here the alternative image
            var AlterNativeImg = "http://hdimagesnew.com/wp-content/uploads/2016/09/image-not-found.png";

            // to avoid the case that even the alternative fails        
            if (AlterNativeImg != me.src)
                me.src = AlterNativeImg;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 898px">
        <input value="Cerrar Ventana" class="form-control2" type="button" onclick="window.close();" style="width:100%; background-color:red;color:white;" />
        <br />
        <br />
        <asp:Image ID="imgcroquis" runat="server" Height="900px" Width="1000px" onerror="imgError(this);" />
    
    </div>
    </form>
</body>
</html>
