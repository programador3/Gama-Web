//<![CDATA[
// Botón para Ir Arriba
jQuery.noConflict();
jQuery(document).ready(function () {
    jQuery("#IrArriba").hide();
    jQuery(function () {
        jQuery(window).scroll(function () {
            if (jQuery(this).scrollTop() > 200) {
                jQuery('#IrArriba').fadeIn();
            } else {
                jQuery('#IrArriba').fadeOut();
            }
        });
        jQuery('#IrArriba a').click(function () {
            jQuery('body,html').animate({
                scrollTop: 0
            }, 800);
            return false;
        });
    });
    $("#btn_p").on("click", function () {
        if ($('#panel_produccion').is(":visible")) {
            $('#panel_produccion').hide("slow"); //oculto mediante id
            $("#btn_p").html('<span class="glyphicon glyphicon-chevron-down"></span> Ver');
        } else {
            $('#panel_produccion').show("slow"); //oculto mediante id
            $("#btn_p").html('<span class="glyphicon glyphicon-chevron-up"></span> Ocultar');
        }
    });
    $("#btn_b").on("click", function () {
        if ($('#panel_borrador').is(":visible")) {
            $('#panel_borrador').hide("slow"); //oculto mediante id
            $("#btn_b").html('<span class="glyphicon glyphicon-chevron-down"></span> Ver');
        } else {
            $('#panel_borrador').show("slow"); //oculto mediante id
            $("#btn_b").html('<span class="glyphicon glyphicon-chevron-up"></span> Ocultar');
        }
    });
    $(window).resize(function () {
        //var href = $(location).attr('href');
        if ($(window).width() < 800) {
            $('#panel_borrador').hide("slow"); //oculto mediante id
            $('#panel_produccion').hide("slow"); //oculto mediante id
            $("#btn_p").html('<span class="glyphicon glyphicon-chevron-down"></span> Ver');
            $("#btn_b").html('<span class="glyphicon glyphicon-chevron-down"></span> Ver');
            // window.location.replace(href);
        } else {
            $('#panel_borrador').show("slow"); //oculto mediante id
            $('#panel_produccion').show("slow"); //oculto mediante id
            $("#btn_p").html('<span class="glyphicon glyphicon-chevron-up"></span> Ocultar');
            $("#btn_b").html('<span class="glyphicon glyphicon-chevron-up"></span> Ocultar');
            // window.location.replace(href);
        }
    });
    $("#TextArea1").click(function () {
        $("#TextArea1").select();
        return false;
    });
});