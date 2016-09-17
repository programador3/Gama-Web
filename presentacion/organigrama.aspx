<%@ Page Title="Organigrama GAMA" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="organigrama.aspx.cs" Inherits="presentacion.organigrama" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--Plugin Org--%>

    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="org/js/jquery/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="org/js/primitives.min.js"></script>
    <link href="org/css/primitives.latest.css" media="screen" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .bp-title {
            font-size: 7px;
            text-align: center;
        }

        .modal-header, .close {
            background-color: #428bca;
            color: white !important;
            text-align: center;
            font-size: 10px;
        }
    </style>
    <script type="text/javascript">
        $(window).resize(function () {
            var href = $(location).attr('href');
            window.location.replace(href);
        });

        function Abrir() {
            var id_puesto = jQuery('#ValueIDE').val();
            var id_descripcion = jQuery('#ValueDescripcion').val();
            var hex = '';
            for (var i = 0; i < id_descripcion.length; i++) {
                hex += '' + id_descripcion.charCodeAt(i).toString(16);
            }
            //id_descripcion = id_descripcion.replace(/ /gi, '_');
            if (id_puesto == 0) {
                location.href = "visualizar_perfil.aspx?id_descripcion=" + hex;
            } else {
                location.href = "perfiles_detalle.aspx?borrador=0&uidc_puestoperfil=" + id_puesto;
            }

        }
    </script>
    <script type="text/javascript">

        function organigrama(newHostURL) {
            var params = '@ptipo';
            var values = $('#<%= cbotipos.ClientID %>').val();
            var newURL = newHostURL;
            var sp = 'sp_datos_organigrama_todo';
            var req = $.ajax({
                url: 'w_s.aspx?tipo=1&sp=' + sp + '&pname=' + params + '&pvalue=' + values,
                async: false
            }).responseText;
            try {
                var data = JSON.parse(req);
                if (data.length > 0) {
                    if (data[0].length > 0) {
                        var options = new primitives.famdiagram.Config();
                        var items = [];
                        for (var i = 0; i < data[0].length; i++) {
                            var urlimage = newURL + data[0][i].idc_empleado + '.jpg';
                            if (newURL == 'http://localhost/empleados/') {
                                urlimage = 'imagenes/btn/default_employed.png';
                            }
                            var x = new primitives.famdiagram.ItemConfig({
                                id: data[0][i].idc_organigrama,
                                parents: [(data[0][i].padre == "0") ? null : data[0][i].padre],
                                title: data[0][i].descripcion,
                                label: data[0][i].idc_puestoperfil,
                                description: data[0][i].empleado,
                                phone: statuspuesto(data[0][i].idc_statuso),
                                image: urlimage,
                                itemTitleColor: data[0][i].bcolor,
                                groupTitle: data[0][i].status_name,
                                groupTitleColor: data[0][i].bcolor
                            });
                            items.push(x);
                        }
                        options.items = items;
                        options.cursorItem = 0;
                        options.hasSelectorCheckbox = primitives.common.Enabled.False;
                        options.pageFitMode = 5;
                        options.arrowsDirection = primitives.common.GroupByType.Children;
                        options.leavesPlacementType = 1;
                        options.onMouseClick = function (e, data) {
                            var colorvalue = data.context.phone;
                            if (colorvalue == 'yellow') {
                                $("#Puesto").css({ color: 'black' });
                                $("#btnVer").css({ color: 'black' });
                            } else {
                                $("#Puesto").css({ color: 'white' });
                                $("#btnVer").css({ color: 'white' });
                            }
                            $("#btnVer").css({ background: colorvalue });
                            $(".modal-header").css({ background: colorvalue });
                            $('#myModal').modal('show');
                            $('#ValueIDE').val(data.context.label);
                            var Nombre = data.context.description;
                            if (Nombre == '' | Nombre == null | Nombre == 'undefined') {
                                if (colorvalue == 'red') {
                                    $('#Personal').text('Vacante, CONTRATAR.');
                                }
                                if (colorvalue == 'gray') {
                                    $('#Personal').text('Vacante, NO CONTRATAR por ahora.');
                                }

                            } else {
                                $('#Personal').text(data.context.description);
                            }
                            $('#Puesto').text(data.context.title);
                            $('#ValueDescripcion').val(data.context.title);
                            var urlimage = newURL + data.context.label + '.jpg';
                            if (newURL == 'http://localhost/empleados/') {
                                urlimage = 'imagenes/btn/default_employed.png';
                            }
                            $('#myImage').attr('src', urlimage);
                        }
                        jQuery("#basicdiagram").famDiagram(options);
                    }
                }
                else {
                    return false;
                }
            }
            catch (e) {
                return false;
            }
        }

        //funcion que regresa un color en base a un numero
        function statuspuesto(id) {
            var color = "";
            switch (id) {
                case "1": //contratado
                    color = "green";
                    break;
                case "2": //capacitacion
                    color = "yellow";
                    break;
                case "3": //vacante
                    color = "red";
                    break;
                case "4": //vacante no contratar por ahora
                    color = "gray";
                    break;
                case "5": //incapacitado
                    color = "purple";
                    break;
                default: //indefinido
                    color = "black";
                    break;
            }

            return color;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div id="page-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-2">
                    <h1>Organigrama</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-8 COL-SM-10 col-sm-12">
                    <input id="ValueIDE" type="hidden" />
                    <input id="ValueDescripcion" type="hidden" />
                    <br />
                    <asp:DropDownList ID="cbotipos" runat="server" AutoPostBack="true" CssClass="form-control">
                        <asp:ListItem Text="Gama Materiales" Value="1">
                        </asp:ListItem>
                        <asp:ListItem Text="Gama Elite" Value="2">
                        </asp:ListItem>
                        <asp:ListItem Text="Todos" Selected="True" Value="0">
                        </asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <%-- <div class="table-responsive" id="mytbl">--%>
                    <div id="basicdiagram" style="height: 650px; border-style: dotted;">
                    </div>
                    <%-- </div>--%>
                </div>
            </div>
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h3><strong id="Puesto" class="modal-title"></strong></h3>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-5 col-sm-5 col-xs-12 portfolio-item" style="text-align: center;">
                                    <a style="text-align: center;">
                                        <img id="myImage" class="img-responsive" alt="" width="256px" height="256px" style="text-align: center;">
                                    </a>
                                </div>
                                <div class="col-lg-9 col-md-7 col-sm-7 col-xs-12 ">
                                    <strong>Empleado: </strong>
                                    <h4 id="Personal"></h4>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <button id="btnVer" type="button" class="btn btn-primary btn-block" data-dismiss="modal" onclick="Abrir();">
                                            Ver Perfil
                                        </button>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <button type="button" class="btn btn-danger btn-block" data-dismiss="modal">
                                            Cerrar
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>