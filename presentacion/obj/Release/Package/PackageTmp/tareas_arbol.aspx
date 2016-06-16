<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_arbol.aspx.cs" Inherits="presentacion.tareas_arbol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="primitive/js/jquery/ui-lightness/jquery-ui-1.10.2.custom.css" />
    <script type="text/javascript" src="primitive/js/jquery/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="primitive/js/jquery/jquery-ui-1.10.2.custom.min.js"></script>

    <link rel="stylesheet" href="primitive/js/jquery/ui-lightness/jquery-ui-1.10.2.custom.css" />
    <script type="text/javascript" src="primitive/js/primitives.min.js?2110"></script>
    <link href="primitive/css/primitives.latest.css?2110" media="screen" rel="stylesheet" type="text/css" />
    <script type='text/javascript'>        //<![CDATA[

        $(window).load(function () {
            var width = $(window).width();
            var heigth = $(window).height();
            if ($(window).width() > 720) {
                width = $(window).width() - 110;
                heigth = $(window).height() - 160;
            }
            $("#basicdiagram").width(width);
            $("#basicdiagram").height(heigth);
            $.ajax({
                type: "POST",
                url: "tareas_arbol.aspx/GetTareas",
                contentType: "application/json; charset=utf-8",
                success: OnSuccess
            });

            function OnSuccess(response) {
                var items_post = response.d;
                if (items_post.length > 0) {
                    var options = new primitives.famdiagram.Config();
                    var items = [];
                    $.each(items_post, function (index, val) {
                        var x = new primitives.famdiagram.ItemConfig({
                            id: val.id,
                            parent: val.id_parent,
                            title: val.empleado,
                            description: val.descripcion,
                            image: "demo/images/photos/a.png",
                            puesto: val.puesto,
                            fc: val.f_com,
                            estado: val.estado,
                            image: "demo/images/photos/a.png",
                            templateName: "contactTemplate",
                            itemTitleColor: val.color,
                            label: val.idc_tarea_url,
                            groupTitle: val.tipo,
                            groupTitleColor: val.color_grupo,
                            redirect: val.redirect,
                        });
                        items.push(x);

                    });
                    options.items = items;
                    options.cursorItem = 0;
                    options.templates = [getContactTemplate()];
                    options.onItemRender = onTemplateRender;
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.hasButtons = primitives.common.Enabled.False;
                    options.onMouseClick = function (e, data) {
                        var redi = parseInt(data.context.redirect);
                        if (redi == 1) {
                            var url = document.getElementById('<%= HiddenField.ClientID%>').value;
                            url = url + "tareas_detalles.aspx?lectura=1&acepta=1&idc_tarea=" + data.context.label;
                            swal({
                                title: "¿Desea visualizar esta Tarea?",
                                text: "Por cuestiones de seguridad, \n si usted NO ESTA INVOLUCRADO EN ESTA TAREA, \n NO SE LE PERMITIRA modificar el contenido de la misma.",
                                type: "info",
                                showCancelButton: true,
                                confirmButtonColor: "#19B5FE",
                                confirmButtonText: "Ver la Tarea",
                                closeOnConfirm: false
                            },
                            function () {
                                window.location = url;
                            });
                        } else {
                            swal("Mensaje del Sistema", "Para ver los detalles, de clic sobre la tarea padre.", "info");
                        }

                    }
                    jQuery("#basicdiagram").orgDiagram(options);
                    jQuery("#Top").click(function (e) {
                        jQuery("#basicdiagram").orgDiagram({
                            orientationType: primitives.common.OrientationType.Top,
                            horizontalAlignment: primitives.common.HorizontalAlignmentType.Center
                        });
                        jQuery("#basicdiagram").orgDiagram("update", primitives.orgdiagram.UpdateMode.Refresh);
                    })

                    jQuery("#Left").click(function (e) {
                        jQuery("#basicdiagram").orgDiagram({
                            orientationType: primitives.common.OrientationType.Left,
                            horizontalAlignment: primitives.common.HorizontalAlignmentType.Left
                        });
                        jQuery("#basicdiagram").orgDiagram("update", primitives.orgdiagram.UpdateMode.Refresh);
                    })

                    jQuery("#Right").click(function (e) {
                        jQuery("#basicdiagram").orgDiagram({
                            orientationType: primitives.common.OrientationType.Right,
                            horizontalAlignment: primitives.common.HorizontalAlignmentType.Left
                        });
                        jQuery("#basicdiagram").orgDiagram("update", primitives.orgdiagram.UpdateMode.Refresh);
                    })

                    jQuery("#Bottom").click(function (e) {
                        jQuery("#basicdiagram").orgDiagram({
                            orientationType: primitives.common.OrientationType.Bottom,
                            horizontalAlignment: primitives.common.HorizontalAlignmentType.Center
                        });
                        jQuery("#basicdiagram").orgDiagram("update", primitives.orgdiagram.UpdateMode.Refresh);
                    })
                    function onTemplateRender(event, data) {
                        switch (data.renderingMode) {
                            case primitives.common.RenderingMode.Create:
                                /* Initialize widgets here */
                                break;
                            case primitives.common.RenderingMode.Update:
                                /* Update widgets here */
                                break;
                        }
                        var itemConfig = data.context;
                        if (data.templateName == "contactTemplate2") {
                            data.element.find("[name=photo]").attr({ "src": itemConfig.image, "alt": itemConfig.title });
                            data.element.find("[name=titleBackground]").css({ "background": itemConfig.itemTitleColor });
                            var fields = ["title", "description", "puesto", "fc", "estado"];
                            for (var index = 0; index < fields.length; index++) {
                                var field = fields[index];

                                var element = data.element.find("[name=" + field + "]");
                                if (element.text() != itemConfig[field]) {
                                    element.text(itemConfig[field]);
                                }
                            }
                        } else if (data.templateName == "contactTemplate") {
                            data.element.find("[name=photo]").attr({ "src": itemConfig.image, "alt": itemConfig.title });
                            data.element.find("[name=titleBackground]").css({ "background": itemConfig.itemTitleColor });

                            var fields = ["title", "description", "puesto", "fc", "estado"];
                            for (var index = 0; index < fields.length; index++) {
                                var field = fields[index];

                                var element = data.element.find("[name=" + field + "]");
                                if (element.text() != itemConfig[field]) {
                                    element.text(itemConfig[field]);
                                }
                            }
                        }
                    }

                    function getContactTemplate() {
                        var result = new primitives.orgdiagram.TemplateConfig();
                        result.name = "contactTemplate";
                        result.itemSize = new primitives.common.Size(220, 120);
                        result.minimizedItemSize = new primitives.common.Size(3, 3);
                        result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);
                        var itemTemplate = jQuery(
                          '<div class="bp-item bp-corner-all bt-item-frame">'
                            + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="top: 2px; left: 2px; width: 300px; height: 20px;">'
                                + '<div name="title" class="bp-item bp-title" style="top: 3px; left: 6px; width: 208px; height: 18px;">'
                                + '</div>'
                            + '</div>'
                            + ''
                            + '<div name="puesto" class="bp-item" style="top: 25px; left: 5px; width: 210px; height: 30px; font-size: 12px;"></div>'
                            + '<div name="fc" class="bp-item" style="top: 60px; left: 5px; width:208px; height: 25px; font-size: 12px;"></div>'
                            + '<div name="description" class="bp-item" style="top: 80px; left: 5px; width: 200px; height:25px; font-size: 10px;"></div>'
                            + '<div name="estado" class="bp-item" style="top: 100px; left: 5px; width: 200px; height:25px; font-size: 10px;"></div>'
                        + '</div>'
                        ).css({
                            width: result.itemSize.width + "px",
                            height: result.itemSize.height + "px"
                        }).addClass("bp-item bp-corner-all bt-item-frame");
                        result.itemTemplate = itemTemplate.wrap('<div>').parent().html();

                        return result;
                    }

                }

            }

        });//]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:HiddenField ID="HiddenField" runat="server" />
    <label style="background-color: #58ACFA; color: white; font-family: Trebuchet MS, Tahoma, Verdana, Arial, sans-serif;">Tareas Terminadas dentro del Tiempo</label>
    <label style="background-color: #FE9A2E; color: white; font-family: Trebuchet MS, Tahoma, Verdana, Arial, sans-serif;">Tareas Pendientes dentro del Tiempo</label>
    <label style="background-color: #D358F7; color: white; font-family: Trebuchet MS, Tahoma, Verdana, Arial, sans-serif;">Tareas Canceladas </label>
    <label style="background-color: #FA5858; color: white; font-family: Trebuchet MS, Tahoma, Verdana, Arial, sans-serif;">Tareas Fuera de Tiempo</label>

    <div class="">
        <div id="basicdiagram" style="border-style: dotted; border-width: 1px;" />
        <a class="btn btn-info" id="Top" href="#">Arriba <i class="fa fa-arrow-up"></i></a>
        <a class="btn btn-info" id="Left" href="#">Izquierda <i class="fa fa-arrow-left"></i></a>
        <a class="btn btn-info" id="Right" href="#">Derecha <i class="fa fa-arrow-right"></i></a>
        <a class="btn btn-info" id="Bottom" href="#">Abajo <i class="fa fa-arrow-down"></i></a>
    </div>
</asp:Content>