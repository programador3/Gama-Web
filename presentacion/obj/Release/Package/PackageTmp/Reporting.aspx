<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reporting.aspx.vb" Inherits="Reporting" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informes</title>
    
       <%--Jquery--%>
    
   <link rel="Stylesheet" type="text/css" href="JQuery/jquery-ui-1.8.19.custom.css" />
    <%--<script  type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>--%>
    
    <%--<script type="text/javascript" src="http://demos.esasp.net/rar/jQuery/ui.datepicker-es.js"></script>--%>
    <%--<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>--%>
    
   <%-- <script type="text/javascript" src="JQuery/JQTimePicker.js"></script>--%>
   <%-- <link rel="stylesheet" href="JQuery/jquery.ui.all.css" />--%>
    
    
    <script type="text/javascript" src="JQuery/jquery-1.7.2.min.js"></script>
    <link rel="Stylesheet" type="text/css" href="JQuery/jquery-ui-1.8.19.custom.min.js" />
	<script type="text/javascript" src="JQuery/jquery.ui.datepicker.js"></script>
	<%--<script type="text/javascript" src="http://jquery-ui.googlecode.com/svn/trunk/ui/i18n/jquery.ui.datepicker-es.js"></script>--%>
	<script type="text/javascript" src="JQuery/jquery.ui.datepicker-es.js"></script>
   <%-- <script type="text/javascript" src="JQuery/jquery-1.7.2.js"></script>--%>	  
    <script type="text/javascript" src="JQuery/jquery.ui.core.js"></script>
	<script type="text/javascript" src="JQuery/jquery.ui.widget.js"></script>
 
		
    
    
    
  

    
    <script type="text/javascript">


      window.onload = function() {
    var reportViewer = window.frames['ReportFrameReportViewer1'];
    if (reportViewer != null) {
        reportViewer.window.frames['report'].document.getElementById('oReportCell').style.width = '100%';
        reportViewer.window.frames['report'].document.getElementById('oReportCell').style.height = '100%';
    }
    document.title = document.getElementById('lbltitulo').textContent;
    }
    
    function reporte()
    {
        try{
        var reportViewer = window.frames['ReportFrameReportViewer1'];
        if (reportViewer != null) 
        {
            reportViewer.window.frames['report'].document.getElementById('oReportCell').style.width = '100%';
            reportViewer.window.frames['report'].document.getElementById('oReportCell').style.height = '100%';
        }
         else
         {
            setTimeout("rezoom()",1000);
         }
          return false;  
         }
         catch(ex)
         {setTimeout("rezoom()",1000);}
         
    }
    
    
    
    function rezoom() 
    {
        var reportViewer = window.frames['ReportFrameReportViewer1'];
//        if (reportViewer.readyState null) {
//            reportViewer.window.frames['report'].document.getElementById('oReportCell').style.width = '100%';
//            reportViewer.window.frames['report'].document.getElementById('oReportCell').style.height = '100%';
//        }
        setTimeout("reporte()",1000)
        return false;  
   }    
      </script> 



</head>
<body>

    <form id="form1" runat="server" 
    style="position: absolute; top: 0px; left: 0px; width: 100%;height:100%;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
            Width="100%" Height="100%" 
            Style="display:table!important; margin:0px; " 
            DocumentMapWidth="" SizeToReportContent="True">
        </rsweb:ReportViewer>
        <asp:Label ID="lbltitulo" runat="server" Text="" style="display:none"></asp:Label>
        <asp:ImageButton ID="imcerrar" runat="server" 
        style="position:absolute;top:5px;right:5px;width:25px;height:25px;" 
        ImageUrl="Iconos/cerrar.png"  />
    
    </form>

    </body>
</html>
