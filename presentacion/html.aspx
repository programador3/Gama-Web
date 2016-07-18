<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="html.aspx.cs" Inherits="presentacion.html" %>

<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="" />
    <meta name="author" content="" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/sweetalert.min.js"></script>
    <script type="text/javascript" src="js/sweetalert-dev.js"></script>
    <link href="js/sweetalert.css" rel="stylesheet" />
    <title>HTML</title>
    <link href="css/html_page.css" rel="stylesheet" />
    <script type="text/javascript">
        function Confirm(value_data) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(value_data)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function PlaySound(path) {
            var audio = new Audio(path);
            audio.play();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="HiddenField_value" runat="server" />
        <asp:Label ID="lblsession" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblsession_h" runat="server" Text="" Visible="false"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="Content">
            <asp:Image ID="Image1" runat="server" Style="position: fixed; top: 0px; left: 0px;" ImageUrl="~/imagenes/logo.png" Visible="false" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDescarga" />
                </Triggers>
                <ContentTemplate>

                    <table id="DemoTable" border="0" cellpadding="0" cellspacing="0">

                        <tr>

                            <td id="EditorCell">

                                <div id="EditorPanel">

                                    <asp:Panel ID="PanelTitulo" runat="server">
                                        <h3 style="font-family: Verdana"><strong>Titulo del Archivo</strong>
                                            <asp:TextBox ID="txtTitulo" placeholder="Titulo del Archivo" runat="server" CssClass="form-control" Width="500px"></asp:TextBox>
                                        </h3>
                                    </asp:Panel>
                                    <div class="form-group">
                                        <asp:Button ID="Button1" runat="server" Text="Salvar Cambios" OnClick="Button1_Click" CssClass="btn btn-primary" ToolTip="Salva los cambios temporales actuales" />
                                        <asp:Button ID="btnGuardarEdicionLive" OnClientClick="Confirm('Desea Guardar la Edicion del Documento?')" runat="server" Text="Guardar Edición" OnClick="btnGuardarEdicionLive_Click" CssClass="btn btn-info" ToolTip="Guarda la edicion del archivo" />
                                        <asp:Button ID="SaveButton" OnClientClick="Confirm('Desea Guardar el Archivo en la Base de Datos')" runat="server" Text="Guardar en BDD" OnClick="SaveButton_Click" CssClass="btn btn-info" ToolTip="Guardar el archivo en la base de datos" />
                                        <asp:Button ID="btnsave_detalles" Visible="false" OnClientClick="Confirm('Desea Guardar el Archivo')" runat="server" Text="Guardar Documento" OnClick="btnsave_detalles_Click" CssClass="btn btn-info" ToolTip="Guardar el archivo en la base de datos" />
                                        <asp:Button ID="btnDescarga" runat="server" Text="Descargar" OnClick="btnDescarga_Click" CssClass="btn btn-success" ToolTip="Descargar" />
                                        <asp:Button ID="ClearButton" runat="server" Text="Limpiar" OnClick="ClearButton_Click" CssClass="btn btn-warning" ToolTip="Limpiar" />
                                        <asp:Button ID="close" runat="server" Text="Cerrar Ventana" OnClick="close_Click" CssClass="btn btn-danger" ToolTip="Limpiar" />
                                        <asp:DropDownList ID="ddlhistorial" runat="server" CssClass="btn btn-default" AutoPostBack="true" OnTextChanged="ddlhistorial_TextChanged"></asp:DropDownList>
                                        <asp:Button ID="btnEliminarrachivo" OnClientClick="Confirm('Desea Eliminar el Archivo')" runat="server" Text="Eliminar Archivo" OnClick="btnEliminarrachivo_Click" CssClass="btn btn-danger" ToolTip="Eliminar Archivo Seleccionado" Visible="false" />
                                    </div>
                                    <cc:HtmlEditor ID="Editor" runat="server" Height="720px" Width="1200" />
                                    <div id="DemoControls">
                                        <asp:BulletedList ID="lista_imagenes" runat="server">
                                        </asp:BulletedList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="panel panel-primary" style="width: auto;">
            <div class="panel-heading" style="text-align: center;">
                Subida de Imagenes
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <asp:FileUpload ID="fileimg" runat="server" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnUploadIMG" CssClass="btn btn-primary" runat="server" Text="Subir Imagen" OnClick="btnUploadIMG_Click" />
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">

        function GetHtmlEditor() {
            return $find('<%= Editor.ClientID %>');
        }
    </script>
</body>
</html>