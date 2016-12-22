<%@ Page Language="VB" AutoEventWireup="false" CodeFile="especificaciones.aspx.vb" Inherits="especificaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Editar</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href='http://fonts.googleapis.com/css?family=Roboto+Condensed:300,400' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900' rel='stylesheet' type='text/css' />
    <link href="css/gridestilo.css" rel="stylesheet" />
    <link href="css/jquery.mobile-1.2.0.min.css" rel="stylesheet" />
    <link href="js/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js" type="text/javascript"></script>
    <link href="TinyBox/style.css" rel="stylesheet" />
    <script src="TinyBox/tinybox.js" type="text/javascript"></script>
    <link href="js/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js" type="text/javascript"></script>
    <script src="js/sweetalert.min.js" type="text/javascript"></script>
    <link rel="shortcut icon" href="imagenes/favicon.png" />
    <%----/-----------/----%>
    <style type="text/css">
        body {
            font-family: Arial;
        }

        input[type="text"] {
            padding: 0px !important;
            height: 32px;
        }

        label {
            color: Black;
            font-size: small;
            font-family: Arial;
            font-weight: bold;
        }

        #txtcodigoarticulo {
            border-radius: 10px 10px 10px 10px;
            padding: 0px 0px 0px 4px !important;
            border-left-style: none;
            width: 100%;
        }

        #Label {
            padding: 0px;
            margin: 0px;
            border-style: solid;
            border-width: 2px;
        }

        #text {
            color: Black;
            text-align: center;
            font-size: 10pt;
            font-family: Arial;
            line-height: 0px;
        }

        .Ocultar {
            display: none;
        }

        #lblnc {
            font-size: 12px;
            font-family: Arial;
        }

        #img_principal {
            text-align: center;
            padding: 5px;
        }

        #div_real_size {
            position: fixed;
            top: 0;
            right: 0;
            left: 0;
            bottom: 0;
            background: none;
            text-align: center;
            padding-top: 50px;
            z-index: 7;
            overflow: auto;
        }

            #div_real_size ul {
                padding: 0;
                margin: 0;
                text-align: center;
            }

            #div_real_size li {
                display: inline-table;
                list-style: none;
                margin: 5px 10px;
                overflow: hidden;
                cursor: pointer;
                cursor: hand;
                border: solid 1px gainsboro;
                position: relative;
                text-align: center;
                background: white;
            }

                #div_real_size li a {
                    display: inline-table;
                    width: 250px !important;
                    height: 250px !important;
                    vertical-align: middle;
                    cursor: pointer;
                    cursor: hand;
                    text-align: center;
                    padding: 5px;
                }

            #div_real_size img {
                max-width: 250px;
                max-height: 250px;
            }

        #div_mask {
            position: fixed;
            top: 0;
            right: 0;
            left: 0;
            bottom: 0;
            background: gray;
            text-align: center;
            padding-top: 20px;
            z-index: 6;
            opacity: 0.8;
        }

        @media all and (max-width: 400px) {
            #img_principal img {
                max-height: 340px;
                max-width: 200px;
            }
        }

        @media all and (min-width: 400px) {
            #img_principal img {
                max-height: 400px;
                max-width: 400px;
            }
        }

        @media all and (min-width: 500px) {
            #img_principal img {
                max-height: 400px;
                max-width: 500px;
            }
        }

        #div_acabados {
            /*border-top:solid 1px gray;
            border-top: solid 17px gray;
            margin-top: 10px;*/
        }

            #div_acabados ul {
                text-align: left;
                margin: 0;
                padding: 0;
            }

            #div_acabados li {
                list-style: none;
                display: inline-table;
                margin: 5px 10px;
                cursor: pointer;
                cursor: hand;
                border: solid 1px gainsboro;
                position: relative;
                text-align: center;
            }

                #div_acabados li a {
                    display: inline-block;
                    width: 62px;
                    height: 62px;
                    vertical-align: middle;
                    cursor: pointer;
                    cursor: hand;
                    text-align: center;
                    padding: 5px;
                }

            #div_acabados img {
                width: 60px;
                max-height: 60px;
                border: solid 1px gray;
            }

        #div_desart {
            color: black;
            padding: 5px 3px;
        }

        #div_title {
            text-align: center;
            text-transform: capitalize !important;
            margin-top: 10px;
            font-weight: bold;
            color: blue;
            text-align: left;
            box-shadow: INSET blue 0px 0px 8px;
        }

        #div_zoom {
            width: 200px;
            height: 200px;
            position: absolute;
            border: solid 1px gainsboro;
            z-index: 5;
        }

            #div_zoom div {
                text-align: center;
                display: table-cell;
                vertical-align: middle;
                width: 200px;
                height: 200px;
                background: white;
            }

            #div_zoom img {
                max-width: 200px;
                max-height: 200px;
            }

            #div_zoom span {
                display: block;
                position: absolute;
                bottom: 0px;
                font-size: 12px;
                background: gray;
                color: White;
                left: 0;
                right: 0;
            }

        #div_adicional {
            text-align: left;
        }

            #div_adicional span {
                text-transform: capitalize !important;
                font-family: AmbleRegular;
                font-weight: bold;
                color: gray;
                display: block;
                margin-bottom: 5px;
            }

        .selected {
            border: solid 1px red !important;
        }

            .selected span {
                position: absolute;
                /*top: 42px;
            left: 43px;*/
                float: right;
                width: 16px;
                height: 16px;
                display: inline-block;
                background: url(images/ok2.png);
                background-repeat: no-repeat;
                background-size: 16px;
                right: 2px;
                bottom: 2px;
            }

            .selected img {
                -webkit-transition: opacity 0.5s ease-in-out;
                -moz-transition: opacity 0.5s ease-in;
                -o-transition: opacity 0.5s ease-in-out;
                transition: opacity 0.5s ease-in-out;
                opacity: 0.4;
            }

            .selected div {
                -webkit-transition: opacity 0.5s ease-in-out;
                -moz-transition: opacity 0.5s ease-in;
                -o-transition: opacity 0.5s ease-in-out;
                transition: opacity 0.5s ease-in-out;
                opacity: 0.4;
            }

        table {
            width: 100%;
            margin: 0;
            padding: 0;
        }

            table td {
                width: 50%;
                padding: 0;
                margin: 0;
            }

        .rp {
            text-align: center;
            font-weight: bold;
            text-decoration: overline;
        }

        .rd {
            text-indent: 1px;
            position: absolute;
            top: 5px;
            left: 5px;
            bottom: 5px;
            text-shadow: blue,5 5 5;
        }

        #gallery {
            padding: 5px;
            /*background: #e1eef5;*/
            background: white;
            /*Perso*/
            width: 100%;
            height: 365px;
            box-sizing: border-box;
            overflow: hidden;
        }

        #descriptions {
            position: relative;
            /*height: 50px;*/
            background: White;
            margin-top: 10px;
            /* width: 640px;*/
            /*padding: 10px;*/
            overflow: hidden;
            font-family: AmbleRegular;
            font-size: 13px;
        }

            #descriptions .ad-image-description {
                position: absolute;
            }

                #descriptions .ad-image-description .ad-description-title {
                    display: block;
                }

        .div_details_r {
            width: 25%;
            float: right;
            color: white;
            text-align: center;
            background: url(images/add.gif);
            padding: 6px;
            margin: 0;
            /*padding-bottom: 100%;
            margin-bottom: -100%;*/
            cursor: pointer;
            cursor: hand;
            border-radius: 3px;
        }

            .div_details_r img {
                max-width: 100%;
            }

        .table-cell-wrapper {
            position: fixed;
        }

        .table-cell {
            height: 200px;
            width: 200px;
            vertical-align: middle;
            background: #eee;
            display: table-cell;
            text-align: center;
        }


        .span {
            position: absolute;
            top: 5px;
            right: 20px;
            padding: 10px;
            border-radius: 5px;
            border: solid gainsboro;
            cursor: pointer;
            background: white url(images/cerrar.png) no-repeat center center;
            width: 20px;
            height: 20px;
        }

        .span_ul {
            color: #60BAEC;
            display: table;
            padding: 0 25px 0 10px;
            width: auto;
            border: solid 1px gainsboro;
            background: white URL(IMAGES/expand.png) no-repeat 96%;
        }

        .span_cve {
            display: table-row;
            font-size: small;
            color: White;
            background: gray;
        }

        .span_title {
            font-size: 10px;
            color: blue;
            opacity: .7;
            margin-left: 30px;
        }

        .txttotal, .txttotal_n {
            padding-left: 5px;
            color: blue;
            font-weight: bold;
        }

        #div_total {
            padding: 5px;
        }

        select {
            padding: 5px;
            background: #3093c7;
            color: white;
            border-radius: 5px;
            font-weight: bold;
            width: 100%;
            margin: 3px 0;
        }

        .adi {
            background-image: url(images/clip_image002.png);
            background-repeat: no-repeat;
            background-size: 19px;
            background-position: right bottom;
        }

        .info {
            background-color: gray;
            color: white;
            padding-left: 25px;
            background-image: url(images/clip_image002.png);
            background-repeat: no-repeat;
        }

        .f_l {
            display: block;
            color: Gray;
        }

        .f_r {
            float: right;
            color: rgb(248, 86, 86);
        }

        .item_opt {
            padding: 5px 10px 5px 20px;
            border: solid gainsboro 1px;
            margin: 1px;
            border-radius: 3px;
            overflow: hidden;
            margin-bottom: 3px;
            cursor: hand;
            cursor: pointer;
        }

        .options {
            padding: 3px;
        }

        .selected_opt {
            /*border:gray solid 2px;
            border: gray solid 2px;*/
            background: url(images/ok2.png);
            background-repeat: no-repeat;
            background-size: 18px;
            background-position: 5px center;
            padding-left: 30px;
            box-shadow: 0px 0px 2px red;
            font-weight: bold;
        }

        .zoom {
            display: block !important;
            width: 20px;
            height: 20px;
        }

        .desc-opt-new {
            font-size: small;
            font-family: Arial;
            color: rgb(15, 188, 243);
            width: 250px;
            height: 16px;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
        }

        .info-price {
            width: 25px;
            height: 25px;
            vertical-align: bottom;
        }

        input[type=submit] {
            height: 40px;
            padding: 5px;
            width: 100%;
        }

        .fotorama {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        //        $(function() {
        //            $('img.image1').data('ad-desc', 'Whoa! This description is set through elm.data("ad-desc") instead of using the longdesc attribute.<br>And it contains <strong>H</strong>ow <strong>T</strong>o <strong>M</strong>eet <strong>L</strong>adies... <em>What?</em> That aint what HTML stands for? Man...');
        //            $('img.image1').data('ad-title', 'Title through $.data');
        //            $('img.image4').data('ad-desc', 'This image is wider than the wrapper, so it has been scaled down');
        //            $('img.image5').data('ad-desc', 'This image is higher than the wrapper, so it has been scaled down');
        //            var galleries = $('.ad-gallery').adGallery();
        //            //galleries[0].settings.effect = 'slide-hori';
        //            galleries[0].settings.effect = 'fade';
        //            //        setTimeout(function() {
        //            //          galleries[0].addImage("images/thumbs/t7.jpg", "images/7.jpg");
        //            //        }, 1000);
        //            //        setTimeout(function() {
        //            //          galleries[0].addImage("images/thumbs/t8.jpg", "images/8.jpg");
        //            //        }, 2000);
        //            //        setTimeout(function() {
        //            //          galleries[0].addImage("images/thumbs/t9.jpg", "images/9.jpg");
        //            //        }, 3000);
        //            //        setTimeout(function() {
        //            //          galleries[0].removeImage(1);
        //            //        }, 4000);

        //            $('#toggle-slideshow').click(
        //                  function() {
        //                      galleries[0].slideshow.toggle();
        //                      return false;
        //                  }
        //                );
        //            $('#toggle-description').click(
        //              function() {
        //                  if (!galleries[0].settings.description_wrapper) {
        //                      galleries[0].settings.description_wrapper = $('#descriptions');
        //                  } else {
        //                      galleries[0].settings.description_wrapper = false;
        //                  }
        //                  return false;
        //              }
        //            );
        //        });

        function zoom_image(idc, obj, visible) {
            if (visible != undefined) {
                var li = obj.parentElement;
                var ul = li.parentElement;
                if (ul.attributes.length > 0) {
                    visible = (ul.attributes[0].value == '1') ? visible : undefined;
                }
                else {
                    visible = undefined;
                }
                var lis = ul.getElementsByTagName('li');
                if (lis.length > 0) {
                    for (var i = 0; i <= lis.length - 1; i++) {
                        if (lis[i].id == li.id) {
                            //lis[i].childNodes[0].childNodes[0].className='selected';
                            lis[i].className = 'selected';
                            var elem = lis[i].getElementsByTagName('span');
                            if (elem.length <= 0) {
                                var span = document.createElement('span');
                                lis[i].appendChild(span);
                            }
                        }
                        else {
                            if ($(lis[i]).hasClass('zoom') == false) {
                                lis[i].className = '';
                            }

                            //lis[i].childNodes[0].childNodes[0].className='';
                            var elem = lis[i].getElementsByTagName('span');
                            if (elem.length > 0) {
                                elem[0].parentNode.removeChild(elem[0]);
                                //lis[i].className='';
                            }
                        }
                    }
                }
            }
            var img = obj.childNodes[0];
            //alert();
            var left = $(img).offset().left; //img.offsetLeft;
            var topp = $(img).offset().top; //img.offsetTop;
            var w_scrn = $(document).width();
            //alert(w_scrn);
            var div_zoom = document.getElementById('div_zoom');
            var spa_info = div_zoom.getElementsByTagName('span');
            spa_info[0].textContent = obj.getAttribute('data-info');
            var imgzoom = document.getElementById('imgzoom');
            if (div_zoom != null && imgzoom != null) {
                topp = topp - 200;
                if ((left + 200) >= w_scrn) {
                    left = (left + 62) - 200;
                }

                div_zoom.style.left = left + 'px';
                div_zoom.style.top = topp + 'px';
                //div_zoom.className='';
                imgzoom.src = 'finishes/' + idc + '.jpg';
                imgzoom.alt = spa_info[0].textContent;
            }
            if (visible != undefined) {
                adi(visible);
            }
            finishes_selected();
        }

        function zoom_image2(idc, obj, visible) {
            var a;
            $(obj).find('a').each(function () {
                a = this;
            });
            return zoom_image(idc, a, visible);
        }

        function zoom_image_out() {
            var div_zoom = document.getElementById('div_zoom');
            if (div_zoom != null) {
                div_zoom.className = 'Ocultar';
            }
        }

        function adi(visible) {
            var div_adicional = document.getElementById('div_adicional');
            if (div_adicional != null) {
                if (visible == 1) {
                    //div_adicional.className = '';
                    $('#div_adicional').removeClass('Ocultar');
                    location.href = '#div_adicional';
                }
                else if (visible == 0) {
                    //div_adicional.className = 'Ocultar';
                    $('#div_adicional').addClass('Ocultar');
                }
            }

        }
        function finishes_selected() {
            var txtselected = document.getElementById('txtselected');
            var txtvcade = document.getElementById('txtvcade');
            var txtvcade2 = document.getElementById('txtvcade2');
            txtselected.value = '';
            txtvcade.value = '';
            txtvcade2.value = '';
            var div_acabados = document.getElementById('div_acabados');
            var uls;
            var lis;
            var li;
            var c_name;
            var data;
            var cboadi = document.getElementById('cboadi');
            var cos_adi = 0;
            if (div_acabados != null) {
                $('ul').each(function () {
                    var l = $(this).attr('l_v_p');
                    if (l != '' && l != null) {
                        var spl_lv = l.split(',');
                        for (var i = 0; i <= spl_lv.length - 2; i++) {
                            //$('.type_' + spl_lv[i]).show('fast');
                            $('.type_' + spl_lv[i]).removeClass('is_hide Ocultar');
                            $('.tit_' + spl_lv[i]).removeClass('Ocultar');
                        }
                    }
                });
                $('.selected').each(function () {
                    var l = $(this).attr('l');
                    if (l == '1') {
                        var l_v = $(this).attr('l_v');
                        var spl_lv = l_v.split(',');
                        for (var i = 0; i <= spl_lv.length - 2; i++) {
                            //$('.type_' + spl_lv[i]).hide(0);
                            //$('.type_' + spl_lv[i]).css('display','none');
                            $('.type_' + spl_lv[i]).addClass('is_hide Ocultar');
                            $('.tit_' + spl_lv[i]).addClass('Ocultar');
                            $('.type_' + spl_lv[i]).find('.selected').removeClass('selected');
                        }
                    }
                });
                $('.options').each(function () {
                    var l = $(this).attr('l_v_p');
                    if (l != '' && l != null) {
                        var spl_lv = l.split(',');
                        for (var i = 0; i <= spl_lv.length - 2; i++) {
                            //$('.type_' + spl_lv[i]).show('fast');
                            $('.type_' + spl_lv[i]).removeClass('is_hide Ocultar');
                            $('.tit_' + spl_lv[i]).removeClass('Ocultar');
                        }
                    }
                });
                $('.selected_opt').each(function () {
                    var l = $(this).attr('l');
                    if (l == '1') {
                        var l_v = $(this).attr('l_v');
                        var spl_lv = l_v.split(',');
                        for (var i = 0; i <= spl_lv.length - 2; i++) {
                            //$('.type_' + spl_lv[i]).hide(0);
                            //$('.type_' + spl_lv[i]).css('display','none');
                            $('.type_' + spl_lv[i]).addClass('is_hide Ocultar');
                            $('.tit_' + spl_lv[i]).addClass('Ocultar');
                            $('.type_' + spl_lv[i]).find('.selected_opt').removeClass('selected_opt');
                        }
                    }
                });
                uls = div_acabados.getElementsByTagName('ul');
                if (uls.length > 0) {
                    for (var i = 0; i <= uls.length - 1; i++) {
                        lis = uls[i].getElementsByTagName('li');
                        var c_name1 = $(uls[i]).hasClass('is_hide');
                        if (lis.length > 0 && c_name1 == false) {
                            for (var x = 0; x <= lis.length - 1; x++) {
                                li = lis[x];
                                c_name = li.className;
                                data = li.attributes['data-adi'].value;

                                if (c_name == 'selected') {

                                    var title_desc = '';
                                    var grp = '';
                                    $(li).find('a').each(function () {
                                        grp = $(li).attr('grp');
                                        var info = $(this).attr('data-info');
                                        title_desc = info;
                                    });
                                    $('.tit_' + grp).each(function () {
                                        $(this).find('.span_title').remove();
                                        var span_desc = document.createElement('span');
                                        span_desc.innerHTML = title_desc;
                                        $(span_desc).addClass('span_title');
                                        $(this).append(span_desc);
                                    });

                                    cos_adi = cos_adi + parseFloat(li.getAttribute('c-adi'));
                                    txtselected.value = txtselected.value + lis[x].id + ';' + ((data == 1) ? cboadi.options[cboadi.selectedIndex].value : '') + ';';
                                    var grp = $(lis[x]).attr('grp');
                                    txtvcade.value = txtvcade.value + grp + ';' + lis[x].id + ';' + ((data == 1) ? cboadi.options[cboadi.selectedIndex].value : '') + ';';
                                    txtvcade2.value = txtvcade2.value + lis[x].id + ';';
                                    if (data == 1) {
                                        if (cboadi.options.length > 0) {
                                            if (cboadi.selectedIndex == 0) {
                                                txtselected.value = '';
                                                txtvcade.value = '';
                                                txtvcade2.value = '';
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                var selects = div_acabados.getElementsByClassName('drop');
                if (selects != null) {
                    for (var x = 0; x <= selects.length - 1; x++) {
                        var select = selects[x];
                        if (select.selectedIndex > 0) {
                            var grp = $(select).attr('grp');
                            var opc = select.options[select.selectedIndex];
                            txtselected.value = txtselected.value + opc.value + ';;';
                            cos_adi = cos_adi + parseFloat(opc.getAttribute('c-adi'));
                            txtvcade2.value = txtvcade2.value + opc.value + ';';
                            txtvcade.value = txtvcade.value + grp + ';' + opc.value + ';;';
                        }

                    }
                }
                //x = ROUND((thisformset.pcosto + vsuma) * (1 + thisformset.pporcentaje / 100.000000000) * (1 + thisformset.piva / 100.0000000000), 4)
                //thisformset.form1.txtcostot.Value = x

                var divs_options = $('.options');
                if (divs_options.length > 0) {
                    for (var x = 0; x <= divs_options.length - 1; x++) {
                        var items = divs_options[x].getElementsByClassName('selected_opt');
                        var c_name1 = $(divs_options[x]).hasClass('is_hide');
                        if (items.length > 0 && c_name1 == false) {
                            txtselected.value = txtselected.value + items[0].id + ';;';
                            var grp = $(items[0]).attr('grp');
                            txtvcade.value = txtvcade.value + grp + ';' + items[0].id + ';;';
                            txtvcade2.value = txtvcade2.value + items[0].id + ';';
                            cos_adi = cos_adi + parseFloat(items[0].getAttribute('c-adi'));

                            $('.tit_' + grp).each(function () {
                                var title_desc_opc = $(items[0]).attr('data-info');
                                $(this).find('.span_title').remove();
                                var span_desc = document.createElement('span');
                                span_desc.innerHTML = title_desc_opc;
                                $(span_desc).addClass('span_title');
                                $(this).append(span_desc);
                            });
                        }
                    }
                }

                var txtiva = document.getElementById('txtiva');
                var txtcosto = document.getElementById('txtcosto');
                var txtp = document.getElementById('txtp');
                var y = (1 + parseFloat(txtp.value) / 100.0000000000)
                var z = (1)
                var x = (parseFloat(txtcosto.value) + parseFloat(cos_adi)) * (1 + parseFloat(txtp.value) / 100.0000000000) * (1);
                var txttotal = document.getElementById('txttotal');
                txttotal.value = (parseFloat(x)).toFixed(4);



                var txttot = document.getElementById('txttot');
                if (cos_adi <= 0) {
                    txttotal.value = parseFloat(txttot.value).toFixed(4);
                }
                //Costo Final del Articulo
                var costo_final = parseFloat($('#txtcosto').val()) + parseFloat(cos_adi);
                $('#txtcosto_final').val(costo_final)

                //Precio Minimo final del Articulo
                var vpre = parseFloat($('#txt_pm').val());
                var premin_final = parseFloat(txttotal.value) / vpre;
                $('#txtpremin_final').val(parseFloat(premin_final).toFixed(4));

                if ($('#txtparametros_precioneto').val() == '1') {
                    var dec = $('#txtparametros_decimales').val();
                    var iva = 1 + parseFloat($('#txtiva').val()) / 100;
                    var precio_neto = parseFloat(txttotal.value).toFixed(4);
                    precio_neto = parseFloat(precio_neto) * iva;
                    precio_neto = parseFloat(precio_neto).toFixedDown(dec);
                    precio_neto = String(precio_neto).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");

                    $('.span-price,.td-price').each(function () {
                        $(this).remove();
                    });

                    $('#tbltotal tr').each(function (index, e) {
                        if (index == 1) {
                            $(this).append('<td class="td-price"><span class="span-price"><b>P. Neto </b>$' + precio_neto + '</span></td>');
                        }
                        else {
                            $(this).append('<td class="td-price"></td>');
                        }
                    });
                    $('#txttotal_n').trigger('change');
                }

                txttotal.value = parseFloat(txttotal.value).toFixed(4).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
                mark_title();
            }
        }
        function mark_title() {
            $('.title').each(function () {
                $(this).css('color', 'blue');
                $(this).css('box-shadow', 'INSET blue 0px 0px 8px');
            });
            $('.selected').each(function () {
                var grp = $(this).attr('grp');
                $('.tit_' + grp).css('color', 'gray');
                $('.tit_' + grp).css('box-shadow', 'INSET gray 0px 0px 8px');
            });
            $('.selected_opt').each(function () {
                var grp = $(this).attr('grp');
                $('.tit_' + grp).css('color', 'gray');
                $('.tit_' + grp).css('box-shadow', 'INSET gray 0px 0px 8px');
            });
            $('.drop').each(function () {
                if ($(this)[0].selectedIndex > 0) {
                    var grp = $(this).attr('grp');
                    $('.tit_' + grp).css('color', 'gray');
                    $('.tit_' + grp).css('box-shadow', 'INSET gray 0px 0px 8px');
                }
            });
        }

        function guardar() {
            var txtselected = document.getElementById('txtselected');
            var div_acabados = document.getElementById('div_acabados');
            var unselected = 0;
            var uls;
            var lis;
            var li;
            if (txtselected != null) {
                if (txtselected.value == '') {
                    alert('Es Necesario Seleccionar Cada Una de las Opciones Disponibles.');
                    return false;
                }
                else {
                    if (div_acabados != null) {
                        uls = div_acabados.getElementsByTagName('ul');
                        if (uls.length > 0) {
                            for (var i = 0; i <= uls.length - 1; i++) {
                                unselected++;
                                lis = uls[i].getElementsByTagName('li');
                                var c_name1 = $(uls[i]).hasClass('is_hide');
                                if (lis.length > 0 && c_name1 == false) {
                                    for (var x = 0; x <= lis.length - 1; x++) {
                                        li = lis[x];
                                        c_name = li.className;
                                        data = li.attributes['data-adi'].value;
                                        if (c_name == 'selected') {
                                            //txtselected.value = txtselected.value + lis[x].id + ';' + ((data == 1) ? cboadi.options[cboadi.selectedIndex].value : '') + ';';

                                            unselected = unselected - 1;
                                            if (data == 1) {
                                                if (cboadi.options.length > 0) {
                                                    if (cboadi.selectedIndex == 0) {
                                                        //txtselected.value = '';
                                                        unselected++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else {
                                    unselected = unselected - 1;
                                }
                            }
                        }
                        var selects = div_acabados.getElementsByClassName('drop');
                        if (selects != null) {
                            for (var x = 0; x <= selects.length - 1; x++) {
                                var select = selects[x];
                                if (select.selectedIndex <= 0) {
                                    unselected = unselected + 1;
                                }

                            }
                        }
                        var divs_options = $('.options');
                        if (divs_options.length > 0) {
                            for (var x = 0; x <= divs_options.length - 1; x++) {
                                var items = divs_options[x].getElementsByClassName('selected_opt');
                                var c_name1_opt = $(divs_options[x]).hasClass('is_hide');
                                if (items.length <= 0 && c_name1_opt == false) {
                                    unselected = unselected + 1;
                                }
                            }
                        }

                    }
                    if (unselected > 0) {
                        alert('Es Necesario Seleccionar Cada Una de las Opciones Disponibles.');
                        return false;
                    }
                    if ($('.qty').val() == '') {
                        alert('Ingrese Cantidad.');
                        $('.qty').focus();
                        return false;
                    }
                    return valid_total(0);
                }
            }
        }
        function cerrar() {
            var type = $('#txttype').val();
            var txtred = $('#txtred').val(); //Redireccionar
            if (txtred == 1) {
                if (type == 2013) {
                    window.location.href = 'buscar_c.aspx';

                }
                else {
                    window.location.href = 'buscar3_m.aspx';
                }
            }
            else {
                try {
                    //window.opener.cargar_cotizados();
                    var win_o = window.opener;
                    var win_p = window.parent;
                    if (win_o != null) {
                        window.opener.cargar_cotizados();
                    }
                    if (win_p != null) {
                        window.parent.close_parent();
                    }
                }
                catch (e) {
                    if ($('#txtidc_cot').val() != '') {
                        window.opener.location.reload();
                    }
                    window.close();
                }
                window.close();
            }
        }

        function add_cliente(idc_articulo) {
            alert('Es Necesario Seleccionar Cliente');
            var txtsearch = (window.opener == null ? null : window.opener.document.getElementById('txtsearch'));
            var txttype = document.getElementById('txttype');
            if (txtsearch != null) {
                txtsearch.value = '%23' + idc_articulo;
                window.opener.add_cliente();
                window.close();
            }
            var ruta;
            if (txttype.value == '') {
                ruta = 'selecciona_cliente_m.aspx?b=%23' + idc_articulo;
                //ruta = 'alta_c.aspx?b=|' + '%23' + idc_articulo;                
            }
            else {
                ruta = 'alta_c.aspx?b=|' + idc_articulo;
            }
            //window.opener.add_cliente();
            window.location.href = ruta;
            return false;
        }


        $(document).ready(function () {
            $(".span").click(function () {
                $("#div_real_size").toggle('slow');
                $("#div_mask").toggle('fast');
                //                $('html, body').css({
                //                    'overflow': 'auto',
                //                    'height': 'auto'
                //                });
            });
            $('#div_real_size').click(function () {
                $("#div_real_size").hide('slow');
                $("#div_mask").hide('fast');
                //                $('html, body').css({
                //                    'overflow': 'auto',
                //                    'height': 'auto'
                //                });
            });

            $(".zoom").click(function () { zoom_selected(this) });
            $(".zoom").find("img").addClass("zoom_img");

            $(".zoom").each(function () {
                $(this).parent().prepend(this);
            });
            $('#div_real_size').removeClass('Ocultar');
            $('#div_mask').removeClass('Ocultar');
            $("#div_real_size").toggle();
            $("#div_mask").toggle();
            //$("#div_real_size").click(function() { $(this).toggle('slow'); });
            var txtidc_cot = $('#txtidc_cot').val();

            select_automatic();

            var txtidc_cotarti = $('#txtidc_cotarti').val();

            alts();
            $('.qty').prop('type', 'number');
            $('.qty').on('keypress', function (e) { return numeros(e, 0, this); });
            $('#txttotal_n').on('keypress', function (e) { return numeros(e, 4, this); });
            $('#txttotal_n').on('blur', function () { return valid_total(); });
            $('#txttotal_n').each(function () {
                $(this).prop('type', 'number');
                $(this).on('focus', function (e) {
                    change_to_number(this);
                });
                $(this).on('blur', function (e) {
                    change_to_text(this);
                });
            });
            $('#txttotal_n').on('change', function () {
                if ($('#txtparametros_precioneto').val() == '1') {
                    var dec = $('#txtparametros_decimales').val();
                    var iva = 1 + parseFloat($('#txtiva').val()) / 100;
                    $(this).parent().parent().find('.td-price').remove();

                    var precio_neto = $(this).val();
                    if (precio_neto != '') {
                        precio_neto = String(precio_neto).replace(",", "");
                        precio_neto = parseFloat(precio_neto).toFixed(4);
                        precio_neto = parseFloat(precio_neto) * iva;
                        precio_neto = parseFloat(precio_neto).toFixed(4);
                        console.log(precio_neto);
                        if (precio_neto > 0) {
                            precio_neto = parseFloat(precio_neto).toFixedDown(dec);
                            precio_neto = String(precio_neto).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
                            $(this).parent().parent().append('<td class="td-price"><span class="span-price"><b>P. Neto </b>$' + precio_neto + '</span></td>');
                        }
                    }
                }
            });
            finishes_selected();

            //Precio Minimo Imagen alert
            $('#imgprice').click(function () {
                var premin = $('#txtpremin_final').val();
                premin = parseFloat(premin).toFixed(4).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
                alert('Precio Minimo:\n \u000b \n$' + premin);
                return false;
            });

            //Select Change
            $('.drop').each(function () {
                $(this).on('change', function () {
                    finishes_selected();
                })
            });
            $('#btncancelar').click(function () {
                window.close();
                return false;
            });
        });
        function valid_total(change) {
            var premin = parseFloat($('#txtpremin_final').val());
            var pre_total_n = $('#txttotal_n').val();
            if (pre_total_n != '') {
                pre_total_n = parseFloat(pre_total_n.replace(",", ""));
            }
            else {
                pre_total_n = parseFloat(0);
            }

            if (pre_total_n > 0) {
                if (premin > pre_total_n) {
                    alert('El Precio Nuevo no Puede Ser Menor al Precio Minimo:\n \u000b \n$' + parseFloat(premin).toFixed(4).replace(/(\d)(?=(\d{3})+\.)/g, "$1,"));
                    if (change != 0) {
                        $('#txttotal_n').val(premin);
                    }
                    else {
                        return false;
                    }

                }
            }



            //var pre_min_fact = $('#txt_pm').val();
            //var pre_total = $('#txttotal').val();
            //pre_total = pre_total.replace(',', '');
            //pre_total = parseFloat(pre_total);

            //pre_min_fact = parseFloat(pre_min_fact) * pre_total;

            //var pre_total_n = $('#txttotal_n').val();
            //if (pre_total_n != '' && pre_total_n > 0) {
            //    pre_total_n = parseFloat(pre_total_n);
            //    if (pre_total_n < (pre_total - pre_min_fact)) {
            //        alert('El Precio Nuevo no Puede Ser Menor al Precio Minimo');
            //        $('#txttotal_n').val('');
            //        $('#txttotal_n').focus();
            //    } 
            //}
            // else {
            //    $('#txttotal_n').val('');
            //}
        }
        function select_automatic() {
            var txtselected = $('#txtselected').val();
            if (txtselected != '') {
                var opc = txtselected.split(';');
                for (var i = 0; i <= opc.length - 2; i++) {
                    var id = opc[i];
                    i++;
                    $("option[value='" + id + "']").each(function () {
                        $(this).parent().val(id);
                    });
                }
                finishes_selected();
            }
        }
        function select_automatic_arti() {
            var txtselected = $('#txtselected').val();
            if (txtselected != '') {
                var opc = txtselected.split(';');
                for (var i = 0; i <= opc.length - 2; i++) {
                    i++;
                    var id = '#' + opc[i];
                    i++;
                    var adi = opc[i];
                    $(id).each(function () {
                        var tag = $(this).prop('tagName');
                        if (tag == 'DIV') {
                            $(this).addClass('selected_opt');
                        }
                        else {
                            $(this).addClass('selected');
                            var especif = $(this).attr('especif');
                            if (especif != undefined) {
                                $('.' + especif + '.Ocultar').each(function () {
                                    $(this).removeClass('Ocultar');
                                    $(this).find('select').each(function () {
                                        $(this).val(adi);
                                    });
                                });
                            }
                        }
                    });
                }
                finishes_selected();
            }
        }

        function zoom_selected(li) {
            //            if (li != null) 
            //            {
            //                var ul = li.parentElement;
            //                var li_array;
            //                if (ul != null) 
            //                {
            //                    li_array = ul.getElementsByTagName('li');
            //                    for (var i = 0; i <= li_array.length - 1; i++) 
            //                    {
            //                        if (li_array[i].className == 'selected') 
            //                        {
            //                            var imgzoom_selected = document.getElementById('imgzoom_selected');
            //                            var div_real_size = document.getElementById('div_real_size');
            //                            if (imgzoom_selected != null) {

            //                                var info = li_array[i].childNodes[0].attributes['data-info'].value;
            //                                imgzoom_selected.src = 'finishes/' + li_array[i].id + '.jpg';
            //                                imgzoom_selected.alt = info;
            //                                div_real_size.className = '';
            //                                $('#' + div_real_size.id).toggle('slow');
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            $('#div_mask').css('display', '');
            var parent = $(li).parent();
            var copy_parent = $(parent).clone();
            $(copy_parent).find('.zoom').remove();
            $(copy_parent).addClass('copy');
            $(copy_parent).find('.zoom_img').parent().remove();
            $(copy_parent).find('img').each(function () {
                var src = $(this).attr('src');
                src = src.replace('_chica', '');
                $(this).attr('src', src);
            });
            $(copy_parent).find('li').each(function () {
                $(this).click(function () {
                    click_on_zoom($(this).attr('id'), $(this).attr('grp'));
                });
                var desc = $(this).find('a').attr('data-info2');
                $(this).find('a').after('<div class="desc-opt-new">' + desc + '</div>');
            });
            $('#div_real_size').find('.copy').remove();
            $('#div_real_size').css('display', '');
            $('#div_real_size').append(copy_parent);
            //            $('html, body').css({
            //                'overflow': 'hidden',
            //                'height': '100%'
            //            });
        }
        function click_on_zoom(id, grp) {
            $('.type_' + grp).find('.selected').removeClass('selected');
            $('#' + id).addClass('selected').append('<span></span>');
            $('#div_real_size').toggle('fast');
            $('#div_mask').toggle('fast');
            finishes_selected();
            //            $('html, body').css({
            //                'overflow': 'auto',
            //                'height': 'auto'
            //            });
            $('html, body').animate({
                scrollTop: $('#' + id).offset().top
            }, 500);
        }
        function alts() {
            $('ul li').each(function (indice, valor) {
                $(valor).find('a').each(function (j, li) {
                    $(li).find('img').each(function (i, img) {
                        var src = img.attributes['image-src'].value;
                        if (src != undefined) {
                            var a = img.parentElement;
                            if (a != null) {
                                a.setAttribute('style', 'width:auto;');
                            }
                        }
                    });
                });
            });
            return false;
        }
        function opts(div_option) {
            var div_options = $(div_option).parent();
            var items = div_options[0].getElementsByClassName('item_opt');
            if (items.length > 0) {
                for (var i = 0; i <= items.length - 1; i++) {
                    items[i].className = "item_opt";
                }
                $(div_option).addClass('item_opt selected_opt');
                finishes_selected();
            }
        }
        function close_no_session() {
            window.close();
            var txttype = $('#txttype').val();
            if (txttype == '') {
                window.opener.location.href = "login_m.aspx";
            }
            else {
                window.opener.location.href = "alta_c.aspx";
            }
        }
        function refrescar() {
            window.opener.mostrar_procesar_grid();
            window.opener.document.getElementById('Contenido_btnref_especif').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%">
    <div style="width:100%;">
            <div align="center" class="ui-header ui-bar-a" style="width:100%;margin-bottom:4px !important; top:0px; left:0px;">
                <asp:Label ID="lblpedidos" runat="server" Text="Especificaciones" ForeColor="White" Font-Bold="true">
                </asp:Label>
            </div>   
            <div id="div_desart" runat="server">
            
            </div>
            <table>
                <tr>
                    <td>
                        <label id="Label1" for="txttotal">Cantidad:</label> 
                    </td>
                    <td>
                        <label id="lbltot" for="txttotal">Precio:</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtcantidad" runat="server" placeholder ="Cantidad" 
                            Enabled="false" Text = "1" class="qty" 
                            CssClass="ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini txt_search"></asp:TextBox> 
                    </td>
                    <td>
                        <asp:TextBox ID="txttotal" 
                            CssClass="txttotal ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini txt_search" 
                            runat="server" onfocus="this.blur();"></asp:TextBox>  
                    </td>
                </tr>
            </table>
            <div id="div_content">
                <div id="gallery" runat="server" class="ad-gallery" style="display:none;">
                      <div class="ad-image-wrapper" id="big_image">
                      </div>
                      <div class="ad-controls">
                      </div>
                      <div class="ad-nav">
                        <div class="ad-thumbs">
                          <ul id="ul" class="ad-thumb-list" runat="server">
                            <%--<li>
                              <a href="images/1.jpg">
                                <img src="images/1.jpg" alt="" class="image0">
                              </a>
                            </li>
                            <li>
                              <a href="images/2.jpg">
                                <img src="images/2.jpg" alt="" class="image1">
                              </a>
                            </li>--%>
                          </ul>
                        </div>
                      </div>
                </div>
                <%--data-transition="dissolve"--%>
                <div class="fotorama" data-nav="thumbs" data-width="100%" data-height="100%" data-fit="contain" runat="server" id="div_fotorama" >
            
                </div>
                <div id="img_principal" class="Ocultar">
                    <asp:Image ID="imgart" runat="server" />
                </div>
            </div>  
            <div id="div_total" runat="server" >
            </div>
            <div id="div_info_acabados" class="info">
                Costo Adicional
            </div>
            <div id="div_acabados" runat="server">            
            
            </div>  
            <div id="div_zoom" class="Ocultar" onclick="zoom_image_out();">
                <div>
                    <img runat="server" id="imgzoom" onerror="this.src='';" src="" alt=""/>
                    <span> </span>
                </div>                
            </div>
            <div id="div_mask" class="Ocultar">
            
            </div>
            
            <div id="div_real_size" class="Ocultar">
                <%--<div>
                    <img runat="server" id="imgzoom_selected" onerror="this.src='';" src="finishes/156.jpg" alt=""/>
                </div> --%>
                <div id="div_close" class="span">
                
                </div>            
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnguardar" runat="server" Text="Guardar" CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" />
                        </td>
                        <td>
                            <asp:Button ID="btncancelar" runat="server" Text="Cancelar" 
                                CssClass="ui-btn ui-shadow ui-btn-corner-all ui-submit ui-btn-up-c" />
                        </td>
                    </tr>                
                </table>
            </div>
            <div id="div_hide">
                <input type="hidden" runat="server" id="txtselected" />
                <input type="hidden" runat="server" id="txttype" />
                <input type="hidden" runat="server" id="txttot" />
                <input type="hidden" runat="server" id="txtiva"/>
                <input type="hidden" runat="server" id="txtp" />
                <input type="hidden" runat="server" id="txtcosto" />
                <input type="hidden" runat="server" id="txtidc_cot" />
                <input type="hidden" runat="server" id="txtidc_cotarti" />
                <input type="hidden" runat="server" id="txtvcade" />        
                <input type="hidden" runat="server" id="txtvcade2" />              
                <input type="hidden" runat="server" id="txtfila" />
                <input type="hidden" runat="server" id="txtred" />
                <input type="hidden" runat="server" id="txtcd" />
                <input type="hidden" runat="server" id="txt_pm" />
                <input type="hidden" runat="server" id="txtpre_min" />
                <input type="hidden" runat="server" id="txtprecio" />
                <input type="hidden" runat="server" id="txtcosto_final" />
                <input type="hidden" runat="server" id="txtpremin_final" />
                <input id="txtparametros_decimales" runat="server" type="hidden" />
                <input id="txtparametros_precioneto" runat="server" type="hidden" />
                
                <table id="tbltotal" class="Ocultar">
                    <tr>
                        <td style="width:50%">                            
                            &nbsp;</td>
                        <td>                        
                            <asp:ImageButton ID="imgprice" ImageUrl="images/info.png" CssClass="info-price" runat="server" />                                      
                        </td>
                    </tr>
                    <tr>
                        <td>                            
                            Cantidad Nueva:
                        </td>
                        <td>    
                            Precio Nuevo
                        </td>
                    </tr>
                    <tr>
                        <td>                            
                            <asp:TextBox ID="txtcantidad_nueva" runat="server" placeholder ="Cantidad Nueva" class="qty"></asp:TextBox>                            
                        </td>
                        <td>                            
                            <asp:TextBox ID="txttotal_n" CssClass="txttotal_n" runat="server"></asp:TextBox>                            
                        </td>
                    </tr>
                </table>
            </div>             
    </div>
    </form>
</body>
</html>
