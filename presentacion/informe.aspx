<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="informe.aspx.cs" Inherits="presentacion.informe" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function cerrar() {
            window.close();

        }
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:Button ID="btncerrar" runat="server" Text="Cerrar" OnClientClick="return cerrar();" CssClass="btn btn-default center-block" />
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <rsweb:ReportViewer ID="ReportViewer2" runat="server"  Height="100%" ProcessingMode="Remote" Width="100%">
            <ServerReport ReportServerUrl="http://192.168.0.4/ReportServer/Pages/ReportViewer.aspx?%2Finformes%2FAdministracion%2Fclientes_prueba&amp;rs:Command=Render&amp;pidc_cliente=0&amp;cadconexion=Data+Source%3D192%2E168%2E0%2E1%3BInitial+Catalog%3Dprueba&amp;rc:parameters=false&amp;rc:UniqueGuid=&lt;newguid&gt;" />
            <LocalReport ReportPath="">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
