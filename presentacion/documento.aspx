<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="documento.aspx.cs" Inherits="presentacion.documento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link rel="shortcut icon" href="imagenes/favicon.png" />
    <link href='http://fonts.googleapis.com/css?family=Roboto+Condensed:300,400' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900' rel='stylesheet' type='text/css' />
    <!-- CSS Libs -->
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="css/animate.min.css" />
    <link rel="stylesheet" type="text/css" href="css/bootstrap-switch.min.css" />
    <link rel="stylesheet" type="text/css" href="css/checkbox3.min.css" />
    <link href="css/PanelsLTE.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="js/sweetalert.css" rel="stylesheet" />
    <link href="css/ionicons.css" rel="stylesheet" />
    <link href="css/ionicons.min.css" rel="stylesheet" />
    <!-- CSS App -->
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/themes/flat-blue.css" />
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <link href="animate.css" rel="stylesheet" />
    <link href="pnotify.custom.min.css" rel="stylesheet" />
    <script src="pnotify.custom.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <link href="css/jquery.dataTables.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <style type="text/css">
        .a4 {
            border: 2px;
            width: 2480px;
            height: 3508px;
            background: black;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div style="padding: 30px;">
            <div class="row">
                <div class="col-lg-12" style="text-align: left; border: solid 1px;">
                    <div class="form-group">
                        <h5><strong>Empleado
                        </strong></h5>
                        <h5><strong>Nombre: </strong>
                            <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                            <strong>Puesto: </strong>
                            <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                        </h5>
                        <h5><strong>Direccion: </strong>
                            <asp:Label ID="lbldireccion" runat="server" Text=""></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
            <div class="row" style="text-align: left; border: solid 1px;">
                <asp:Repeater ID="RepeatVehiculos" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-12">
                            <h5><strong>Vehiculo</strong></h5>
                            <h5><strong>Descripción del Vehiculo: </strong>
                                <asp:Label ID="lblDescripcionVehoculo" runat="server" Text='<%#Eval("descripcion_vehiculo") %>'></asp:Label>
                            </h5>
                            <h5><strong>Numero Economico: </strong>
                                <asp:Label ID="lblNumeroEc" runat="server" Text='<%#Eval("num_economico") %>'></asp:Label>
                                <strong>Placas: </strong>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("placas") %>'></asp:Label>
                                <strong>Modelo: </strong>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("modelo") %>'></asp:Label>
                            </h5>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-8 col-sm-8" style="text-align: center;">
                    <asp:GridView ID="gridHerramientasVehiculo" runat="server" AutoGenerateColumns="false" ShowHeader="false" Font-Size="Smaller">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="40px" DataField="cantidad" HeaderText="Cantidad"></asp:BoundField>
                            <asp:BoundField ItemStyle-Width="500px" DataField="descripcion" HeaderText="Herramienta"></asp:BoundField>
                            <asp:BoundField ItemStyle-Width="80px" DataField="ok"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-lg-4 col-sm-4" style="text-align: center;">
                    <img id="imav" runat="server" class="img-responsive" alt="Gama" style="width: 400px; margin: 0 auto;" />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h5>Observaciones
                        <textarea rows="3" class="form-control"></textarea>
                    </h5>
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-lg-3 col-sm-5">
                    <h6>______________________________
                        <br />
                        <br />
                        &nbsp; &nbsp;Firma de Usuario
                    </h6>
                </div>
                <div class="col-lg-3 col-sm-6">
                    <h6>______________________________
                        <br />
                        <br />
                        &nbsp; &nbsp;Firma de Evaluador
                    </h6>
                </div>
            </div>
        </div>
    </form>
</body>
</html>