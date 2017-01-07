<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="html_editor.aspx.cs" Inherits="presentacion.html_editor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<title>CKEditor Sample</title>
    <script src="js/ckeditor.js"></script>
    <script src="js/sample.js"></script>

    <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
    
</head>
<body id="main">
    <form id="form1" runat="server">
        <div id="editor">
        </div>
        <button id="dd" onclick="GetHtml();">CLICK</button>
    </form>
    <script type="text/JavaScript">
	    initSample();
	    function GetHtml(){
		    var val = $("#editor" ).html();
		    alert(val);
	    }
</script>
</body>
</html>
