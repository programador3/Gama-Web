<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="html_editor.aspx.cs" Inherits="presentacion.html_editor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<title>CKEditor Sample</title>
    <script src="ckeditor/js/ckeditor.js"></script>
	<script src="ckeditor/js/sample.js"></script>
	<link rel="stylesheet" href="ckeditor/css/samples.css"/>
	<link rel="stylesheet" href="ckeditor/toolbarconfigurator/lib/codemirror/neo.css"/>


</head>
<body id="main">
    <form id="form1" runat="server">
    <div>
   <div class="adjoined-bottom">
		<div class="grid-container">
			<div class="grid-width-100">
				<div id="editor">
					<h1>Hello world!</h1>
					<p>I'm an instance of <a href="http://ckeditor.com">CKEditor</a>.</p>
				</div>
			</div>
		</div>
	</div>
    </div>
    </form>
    <script>
	initSample();
</script>
</body>
</html>
