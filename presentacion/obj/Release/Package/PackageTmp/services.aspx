<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="services.aspx.cs" Inherits="presentacion.services" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript">

        function SendSlack(URL) {
            $.ajax({
                type: "POST",
                //url: "https://slack.com/api/chat.postMessage?token=xoxp-32823140596-32969794503-32967042148-3f7c3e4f62&channel=%23gamaelite&text=Eded&pretty=1&username=admin"
                url: URL
            });
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>