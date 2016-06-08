<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="arbol_tareas.aspx.cs" Inherits="presentacion.arbol_tareas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link rel="stylesheet" href="primitive/js/jquery/ui-lightness/jquery-ui-1.10.2.custom.css" />
    <script type="text/javascript" src="primitive/js/jquery/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="primitive/js/jquery/jquery-ui-1.10.2.custom.min.js"></script>

    <script type="text/javascript" src="primitive/js/primitives.min.js?2110"></script>
    <link href="primitive/css/primitives.latest.css?2110" media="screen" rel="stylesheet" type="text/css" />
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" integrity="sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7" crossorigin="anonymous"/>
    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap-theme.min.css" integrity="sha384-fLW2N01lMqjakBkx3l/M9EahuwpSfeNvV63J5ezn3uZzapT0u7EYsXMjQV+0En5r" crossorigin="anonymous"/>
    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>
    <script type='text/javascript'>        //<![CDATA[
       
        $(window).load(function () {
            var width = $(window).width() - 30;
            var heigth = $(window).height() - 30;
            $("#basicdiagram").width(width);
            $("#basicdiagram").height(heigth);
            $.ajax({
                type: "POST",
                url: "arbol_tareas.aspx/GetTareas",
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
                            image: "demo/images/photos/a.png",
                            templateName: "contactTemplate",
                            itemTitleColor: val.color
                        });
                        items.push(x);

                    });
                    var options = new primitives.orgdiagram.Config();
                    options.items = items;
                    options.cursorItem = 0;
                    options.templates = [getContactTemplate(), getContactTemplate2()];
                    options.onItemRender = onTemplateRender;
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.hasButtons = primitives.common.Enabled.False;
                    //options.onMouseClick = function (e, data) {
                    //    alert("youre click me");
                    //}
                    jQuery("#basicdiagram").orgDiagram(options);

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

                            var fields = ["title", "description", "puesto", "fc"];
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

                            var fields = ["title", "description", "puesto", "fc"];
                            for (var index = 0; index < fields.length; index++) {
                                var field = fields[index];

                                var element = data.element.find("[name=" + field + "]");
                                if (element.text() != itemConfig[field]) {
                                    element.text(itemConfig[field]);
                                }
                            }
                        }
                    }

                    function getContactTemplate2() {
                        var result = new primitives.orgdiagram.TemplateConfig();
                        result.name = "contactTemplate2";
                        result.itemSize = new primitives.common.Size(220, 120);
                        result.minimizedItemSize = new primitives.common.Size(3, 3);
                        result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);

                        var itemTemplate = jQuery(
                          '<div class="bp-item bp-corner-all bt-item-frame">'
                            + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="top: 2px; left: 2px; width: 216px; height: 20px;">'
                                + '<div name="title" class="bp-item bp-title" style="top: 3px; left: 6px; width: 208px; height: 18px;">'
                                + '</div>'
                            + '</div>'
                            + '<div class="bp-item bp-photo-frame" style="top: 26px; left: 164px; width: 50px; height: 60px;">'
                                + '<img name="photo" style="height:60px; width:50px;" />'
                            + '</div>'
                            + '<div name="puesto" class="bp-item" style="top: 26px; left: 6px;font-size: 12px;"></div>'
                            + '<div name="fc" class="bp-item" style="top: 44px; left: 6px;font-size: 12px;"></div>'
                            + '<div name="description" class="bp-item" style="top: 62px; left: 6px; width: 162px; height: 36px; font-size: 10px;"></div>'
                        + '</div>'
                        ).css({
                            width: result.itemSize.width + "px",
                            height: result.itemSize.height + "px"
                        }).addClass("bp-item bp-corner-all bt-item-frame");
                        result.itemTemplate = itemTemplate.wrap('<div>').parent().html();

                        return result;
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="table table-responsive">
            <div id="basicdiagram" style="width: 1020px; height: 720px; border-style: dotted; border-width: 1px;" />
        </div>
    </form>
</body>
</html>