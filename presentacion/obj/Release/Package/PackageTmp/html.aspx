<%@ Page Title="HTML" Language="C#" ValidateRequest="false" MasterPageFile="~/Adicional.Master" AutoEventWireup="true" CodeBehind="html.aspx.cs" Inherits="presentacion.html" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:HiddenField ID="HiddenField_value" runat="server" />
    <asp:Label ID="lblsession" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblsession_h" runat="server" Text="" Visible="false"></asp:Label>
    <asp:HiddenField ID="HiddenField" runat="server" />
    <div class="row">

        <div class="col-lg-12">
            <asp:Panel ID="PanelTitulo" runat="server">
                <h3><strong>Titulo del Archivo</strong>
                    <asp:TextBox ID="txtTitulo" placeholder="Titulo del Archivo" runat="server" CssClass="form-control" Width="500px"></asp:TextBox>
                </h3>
            </asp:Panel>
            <br />
            <asp:LinkButton ID="LinkButton1" OnClick="Button1_Click" CssClass="btn btn-primary" ToolTip="Salva los cambios temporales actuales" runat="server">BackUP&nbsp;<i class="fa fa-save" aria-hidden="true"></i></asp:LinkButton>
            <asp:LinkButton ID="lnkGuardarEdicionLive" OnClientClick="Confirm('Desea Guardar la Edicion del Documento?')" runat="server"  OnClick="lnkGuardarEdicionLive_Click" CssClass="btn btn-info" 
                ToolTip="Guarda la edicion del archivo" >Guardar Edición en Perfil&nbsp;<i class="fa fa-save" aria-hidden="true"></i></asp:LinkButton> 
            <asp:LinkButton ID="lnkSaveButton" OnClientClick="Confirm('Desea Guardar el Archivo en la Base de Datos')" runat="server"  OnClick="lnkSaveButton_Click" CssClass="btn btn-info" 
                ToolTip="Guardar el archivo en la base de datos">Guardar en BDD&nbsp;<i class="fa fa-database" aria-hidden="true"></i></asp:LinkButton> 
            <asp:LinkButton ID="lnksave_detalles" Visible="false" runat="server" OnClientClick="Confirm('Desea Guardar el Archivo')"  OnClick="lnksave_detalles_Click" CssClass="btn btn-info" 
                ToolTip="Guardar el archivo">Guardar Documento&nbsp;<i class="fa fa-save" aria-hidden="true"></i></asp:LinkButton>   
            <asp:LinkButton ID="lnkDescarga" runat="server" OnClick="btnDescarga_Click" CssClass="btn btn-success">Descargar&nbsp;<i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton>            
            <asp:LinkButton ID="lnkClearButton" runat="server" OnClick="ClearButton_Click" CssClass="btn btn-warning">Limpiar&nbsp;<i class="fa fa-eraser" aria-hidden="true"></i></asp:LinkButton>
            <asp:LinkButton ID="lnkclose" runat="server" OnClick="close_Click" CssClass="btn btn-danger">Cerrar Ventana&nbsp;<i class="fa fa-window-close" aria-hidden="true"></i></asp:LinkButton>
            <asp:DropDownList ID="ddlhistorial" runat="server" CssClass="btn btn-default" AutoPostBack="true" OnTextChanged="ddlhistorial_TextChanged"></asp:DropDownList>
            <asp:LinkButton ID="lnkEliminarrachivo" OnClientClick="Confirm('Desea Eliminar el Archivo')" runat="server"
                OnClick="btnEliminarrachivo_Click" CssClass="btn btn-danger" ToolTip="Eliminar Archivo Seleccionado" Visible="false">Eliminar&nbsp;<i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>


        </div>
        <div class="col-lg-12">
            <FTB:FreeTextBox ID="Editor"
                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell"
                Language="es-ES" runat="server" Width="100%" Height="720px" />
        </div>

        <div class="col-lg-12">
            <div class="panel panel-primary" style="width: auto;" id="panel">
                <div class="panel-heading" style="text-align: center;">
                    Subida de Imagenes
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <asp:FileUpload ID="fileimg" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnUploadIMG" CssClass="btn btn-primary" runat="server" Text="Subir Archivo" OnClick="btnUploadIMG_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
