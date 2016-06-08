<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inventario.aspx.cs" Inherits="presentacion.inventario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inventarios</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <link href='http://fonts.googleapis.com/css?family=Roboto+Condensed:300,400' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900' rel='stylesheet' type='text/css' />
    <!-- CSS Libs -->
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" integrity="sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7" crossorigin="anonymous" />
    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap-theme.min.css" integrity="sha384-fLW2N01lMqjakBkx3l/M9EahuwpSfeNvV63J5ezn3uZzapT0u7EYsXMjQV+0En5r" crossorigin="anonymous" />
    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>

    <link href="css/PanelsLTE.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="js/sweetalert.css" rel="stylesheet" />
    <link href="css/ionicons.css" rel="stylesheet" />
    <link href="css/ionicons.min.css" rel="stylesheet" />
    <link href="Not/animate.css" rel="stylesheet" />
    <link href="Not/pnotify.custom.min.css" rel="stylesheet" />
    <script src="Not/pnotify.custom.min.js"></script>
    <link href="css/jquery.dataTables.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script type="text/javascript">

        function ConfirmCancel(value_data) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value_can";
            if (confirm(value_data)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
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
        function imposeMaxLength(Object, MaxLen) {
            if (Object.value.length > MaxLen) {
                Object.focus();
                Object.select();
                alert("Sobrepaso el numero de caracteres permitidos(" + MaxLen + "). Corriga este error.");
            }
        }
        //VALIDA QUE SOLO SEAN NUMEROS ENTEROS REALES
        function validarEnteros(e) {
            k = (document.all) ? e.keyCode : e.which;
            if (k == 8 || k == 0) return true;
            patron = /[0-9\s\t]/;
            n = String.fromCharCode(k);
            return patron.test(n);
        }
        function Go() {
            var value = document.getElementById('<%= HiddenField.ClientID%>').value;
            if (value == "") {
                alert("Seleccione un Empleado");
            } else {
                window.open('http://192.168.0.4/ReportServer/Pages/ReportViewer.aspx?%2FInformes%2FInventario%2Factivos&rs:Command=Render&cadconexion=Data+Source%3D192%2E168%2E0%2E4%3BInitial+Catalog%3Dgm&pidc_puesto=' + value + '&rc:parameters=false&rc:UniqueGuid=<newguid>');

            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="HiddenField" runat="server" />
        <div class="container">

            <div class="page-header">
                <h1>Captura</h1>
            </div>
            <div class="row">
                <div class="col-lg-10 col-md-8 col-sm-8 col-xs-12">
                    <div class="form-group">
                        <h4>Puesto </h4>
                        <asp:DropDownList ID="ddlPuesto" OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-4 col-sm-4 col-xs-12">
                    <div class="form-group">
                        <h4>. </h4>
                        <button class="btn btn-primary btn-block" onclick="Go();">Ver Articulos</button>
                    </div>
                </div>
                <div class="col-lg-8 col-xs-8">
                    <div class="form-group">
                        <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtpuesto_filtro_TextChanged" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4 col-xs-4">
                    <div class="form-group">
                        <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12 col-xs-12">
                            <div class="form-group">
                                <h4>Categorias </h4>
                                <asp:DropDownList ID="ddlcategorias" OnSelectedIndexChanged="ddlcategorias_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-8 col-xs-8">
                            <div class="form-group">
                                <asp:TextBox ID="txtfiltrocat" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtfiltrocat_TextChanged" placeholder="Escriba el Nombre de la Categoria"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4 col-xs-4">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbuscarcat" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarcat_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-4 col-xs-12">
                            <div class="form-group">
                                <h4>Folio </h4>
                                <asp:TextBox ID="txtfolio" onblur="return imposeMaxLength(this, 10);" onkeypress="return validarEnteros(event);" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4 col-xs-6">
                            <div class="form-group">
                                <h4>Area Comun </h4>
                                <asp:LinkButton ID="lnkarea" runat="server" CssClass="btn btn-default btn-block" OnClick="lnkarea_Click"> Es Area Comun</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-lg-4 col-xs-6">
                            <div class="form-group">
                                <h4>Area Comun </h4>
                                <asp:DropDownList ID="ddlarea" OnSelectedIndexChanged="ddlarea_SelectedIndexChanged" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <h4>Observaciones </h4>
                                <asp:TextBox ID="txtObservaciones" onblur="return imposeMaxLength(this, 245);" TextMode="MultiLine" Rows="3" placeholder="Observaciones" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:Repeater ID="repeatreractivos" runat="server">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="row" style="border: 1px solid #000000;">
                                        <div class="col-lg-1 col-md-1 col-sm-2 col-xs-3">
                                            <div class="form-group">
                                                <h4>No </h4>
                                                <asp:TextBox ID="txtNumero" Text='<%#Eval("idc_actespec") %>' ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-11 col-sm-10 col-xs-9">
                                            <div class="form-group">
                                                <h4>Especificacion </h4>
                                                <asp:TextBox ID="txtespec" Text='<%#Eval("desesp") %>' ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-9 col-sm-9 col-xs-9">
                                            <div class="form-group">
                                                <h4>Valor </h4>
                                                <asp:TextBox ID="txtvaloract" onblur="return imposeMaxLength(this, 245);" placeholdeR="Valor" ReadOnly="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-3">
                                            <div class="form-group">
                                                <h4>. </h4>
                                                <asp:LinkButton ID="lnkaplica" runat="server" CommandName='<%#Eval("idc_actespec") %>' CssClass="btn btn-default btn-block" OnClick="lnkaplica_Click">N/A</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:Button ID="btngiuardar" CssClass="btn btn-primary btn-block" OnClientClick="Confirm('Desea Guardar el Formulario?')" OnClick="btngiuardar_Click" runat="server" Text="Guardar" />
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:Button ID="btncancelar" CssClass="btn btn-danger btn-block" OnClientClick="ConfirmCancel('Desea Cancelar el Formulario?')" OnClick="btncancelar_Click" runat="server" Text="Cancelar (Limpiar)" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>