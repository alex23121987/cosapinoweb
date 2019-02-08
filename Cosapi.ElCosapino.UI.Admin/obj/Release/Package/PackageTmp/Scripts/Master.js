function ObtenerPagina(paginaSolicitada, TotalPaginas, PaginaActual) {
    if (paginaSolicitada == 'A' && PaginaActual == 1)
        return 0;

    if (paginaSolicitada == 'P' && PaginaActual == 1)
        return 0;


    var PaginaResult = 0;

    switch (paginaSolicitada) {
        case "P":
            PaginaResult = 1;
            break;
        case "A":
            PaginaResult = PaginaActual - 1;
            break;
        case "S":
            if (PaginaActual == TotalPaginas) {
                return 0;
            } else PaginaResult = PaginaActual + 1;

            break;
        case "U":
            if (PaginaActual == TotalPaginas) {
                return 0;
            } else
                PaginaResult = TotalPaginas;
            break;
    }
    return PaginaResult;
}
/**
 * Función que valida que se ingresen sólo números en un componente de la página.
 * evento, Evento que tipo "onkeypress" que se esta validando.
 */
function validarSoloNumeros(evento) {
    /*Validar la existencia del objeto event*/
    evento = (evento) ? evento : event;

    /*Extraer el codigo del caracter de uno de los diferentes grupos de codigos*/
    var charCode = (evento.charCode) ? evento.charCode : ((evento.keyCode) ? evento.keyCode
			: ((evento.which) ? evento.which : 0));

    /*Predefinir como valido*/
    var respuesta = true;

    /*Validar si el codigo corresponde a los NO aceptables*/
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        /*Asignar FALSE a la respuesta si es de los NO aceptables*/
        respuesta = false;
    }

    /*Regresar la respuesta*/
    return respuesta;
}

/**
 * Función que valida que se ingresen sólo números, letras y el caracter punto en un componente de la página.
 * evento, Evento que tipo "onkeypress" que se esta validando.
 */
function validarSoloNumerosYPunto(evento) {
    /*Validar la existencia del objeto event*/
    evento = (evento) ? evento : event;

    /*Extraer el codigo del caracter de uno de los diferentes grupos de codigos*/
    var charCode = (evento.charCode) ? evento.charCode : ((evento.keyCode) ? evento.keyCode
			: ((evento.which) ? evento.which : 0));

    /*Predefinir como valido*/
    var respuesta = true;

    /*Validar si el codigo corresponde a los NO aceptables*/
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
        /*Asignar FALSE a la respuesta si es de los NO aceptables*/
        respuesta = false;
    }

    /*Regresar la respuesta*/
    return respuesta;
}

/**
 * Función que valida que se ingresen sólo números, letras en un componente de la página.
 * evento, Evento que tipo "onkeypress" que se esta validando. 
 */
function validarSoloNumerosLetras(evento) {
    tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 8 || tecla == 0)
        return true;
    if (tecla == 32)
        return false;
    patron = /^[A-Za-z0-9\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}


/**
 * Función que valida que se ingresen sólo números, letras, punto y guión bajo en un componente de la página.
 * evento, Evento que tipo "onkeypress" que se esta validando. 
 */
function validarSoloNumerosLetrasYGuionBajoYPunto(evento) {
    tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 8 || tecla == 0 || tecla == 46)
        return true;
    if (tecla == 32)
        return false;
    patron = /^[A-Za-z0-9_\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}

/**
 * Función que valida que se ingresen sólo números, letras, espacio en blanco y guión bajo en un componente de la página.
 * evento, Evento que tipo "onkeypress" que se esta validando. 
 */
function validarSoloNumerosLetrasEspaciosEnBlancoYGuionBajo(evento) {
    tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 8 || tecla == 0)
        return true;
    patron = /^[A-Za-z0-9_\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}

/**
 * Función que valida que se ingresen sólo números, letras, espacio en blanco, punto y guión bajo en un componente de la página.
 * evento, Evento que tipo "onkeypress" que se esta validando. 
 */
function validarSoloNumerosLetrasEspaciosEnBlancoYGuionBajoYPunto(evento) {
    tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 8 || tecla == 0 || tecla == 46)
        return true;
    patron = /^[A-Za-z0-9_\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}

/**
 * Función que valida que se ingresen sólo números, letras y espacio en blanco en un componente de la página.
 * evento, Evento que tipo "onkeypress" que se esta validando. 
 */
function validarSoloNumerosLetrasYEspaciosEnBlanco(evento) {
    tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 8 || tecla == 0)
        return true;
    patron = /^[A-Za-z0-9\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}

/**
 * Permite validar las teclas principales. 
 * tecla, tecla pulsada.
 */
function validarTeclasPrincipales(tecla) {
    var arr = new Array(8, 9, 35, 36, 37, 38, 39, 40);
    for (ctaArr = 0; ctaArr < arr.length; ctaArr++) {
        if (tecla == arr[ctaArr])
            return true;
    }
    return false;
}

/**
 * Valida si sólo se ha ingresado letras.
 * evento, Evento que tipo "onkeypress" que se esta validando.
 */
function validarSoloLetras(evento) {
    var key = evento.keyCode ? evento.keyCode : evento.which ? evento.which : evento.charCode; //Aceptado por FF y IE
    if (validarTeclasPrincipales(key))
        return true;
    if (evento && key == 13)/*enter*/
        return true;
    if (evento && key == 46) /*.*/
        return true;
    if (evento && key >= 65 && key <= 90)
        return true;
    if (evento && key >= 97 && key <= 122)
        return true;
    if (evento && key == 32)/*espace*/
        return true;
    if (evento && key >= 164 && key <= 165)
        return true;
    if (evento && key == 209)/*letra Ñ*/
        return true;
    if (evento && key == 241)/*letra ñ*/
        return true;
    if (evento && key == 193)/*letra Á*/
        return true;
    if (evento && key == 201)/*letra É*/
        return true;
    if (evento && key == 205)/*letra Í*/
        return true;
    if (evento && key == 211)/*letra Ó*/
        return true;
    if (evento && key == 218)/*letra Ú*/
        return true;
    if (evento && key == 225)/*letra á*/
        return true;
    if (evento && key == 233)/*letra é*/
        return true;
    if (evento && key == 237)/*letra í*/
        return true;
    if (evento && key == 243)/*letra ó*/
        return true;
    if (evento && key == 250)/*letra ú*/
        return true;
    return false;
}

function ValidarSiEsNumericoValido(monto) {
    if ($.trim(monto) == '') return false;

    return /^([0-9])*[.]?[0-9]*$/.test(monto);

}
function ValidarSiEsNumericoValidoNulo(monto) {
    //if ($.trim(monto) == '') return false;

    return /^([0-9])*[.]?[0-9]*$/.test(monto);

}

function formateafecha(fecha) {
    if (fecha.length >= 1) {
        var resultado = fecha.substr(0, fecha.length - 1);
        var ultimo = fecha.substr(fecha.length - 1);
        if (fecha.length <= 10) {
            if (fecha.length != 3 && fecha.length != 6) {
                if (fecha.length >= 3 && fecha.length < 6 &&
                        fecha.indexOf('/') != 2) {
                    resultado = fecha.substr(0, 2);
                } else {
                    if (fecha.length >= 6 && fecha.lastIndexOf('/') != 5) {
                        resultado = fecha.substr(0, 5);
                    } else {
                        if (ultimo.search(/\d/g) > -1) {
                            if (fecha.length <= 2) {
                                if (fecha <= 31) {
                                    resultado = resultado + ultimo;
                                }
                            } else {
                                if (fecha.length <= 5) {
                                    if (fecha.substr(3, fecha.length - 1) <= 12) {
                                        resultado = resultado + ultimo;
                                    }
                                } else {
                                    resultado = resultado + ultimo;
                                }
                            }
                        }
                    }
                }
            } else {
                if (ultimo == '/') {
                    resultado = resultado + ultimo;
                }
            }
        }
        return resultado;
    } else {
        return fecha;
    }
}
function formatearFechaBase(fecha) {
    var d = fecha.split("/")[0];
    var m = fecha.split("/")[1];
    var y = fecha.split("/")[2];
    if (fecha.indexOf('/') > 1) {
        return d + '/' + m + '/' + y;
    } else {
        var yf = fecha.split("-")[0];
        var mf = fecha.split("-")[1];
        var df = fecha.split("-")[2];
        return df + '/' + mf + '/' + yf;
    }

}
function validarFormatoFecha(campo, evento) {
    var fecha = campo.value;
    if (fecha.length < 10) {
        var ultimo = String.fromCharCode(evento.keyCode);
        if (validarSoloNumeros(evento)) {
            if (fecha.length < 2) {
                var dia = fecha + ultimo;
                if (dia <= 31) {
                    if (fecha.length == 1) dia = dia + '/';
                    fecha = dia;
                }
            } else {
                if (fecha.length < 5) {
                    var mes = fecha.substr(3, fecha.length - 1) + ultimo;
                    if (mes <= 12) {
                        if (fecha.length == 4) mes = mes + '/';
                        fecha = fecha.substr(0, 3) + mes;
                    }
                } else {
                    fecha = fecha + ultimo;
                }
            }
        }
    }
    campo.value = fecha;
    return false;
}
function AgregarComas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '.00';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
// Oculta/Muestra Paneles
$(function () {
    //$('.u-close-panel').each(function () {
    //    this.live('click', function (ev) {
    //        $this = $(this).toggleClass("active");
    //        $($this.data('panel')).toggleClass("panel-full");
    //    });
    //});

    var panelCollapse = function () {
        var $html = $('html');
        var $navmenu = $('#NavMenu');
        var $btn = $('.btn_slide');
        var $content = $('.content_slide');
        var $fixed_top = $('.fixed_top');
        var $ilogo = $('.i-logo');

        $btn.on('click', function (event) {
            var $this = $(this);
            event.preventDefault();
            if (!$html.hasClass('active-layout-slide')) {
                $html.addClass('active-layout-slide');
                $ilogo.css('background-image', 'url("/images/logocosapimovil.png")');
                $content.animate({
                    marginLeft: 60
                });

                $fixed_top.animate({
                    'left': "60px"
                })
                $navmenu.removeClass('scrollable-menu');
            } else {
                $html.removeClass('active-layout-slide');
                $ilogo.css('background-image', 'url("/images/logocosapi.png")');
                $content.animate({
                    marginLeft: 267
                });
                $fixed_top.animate({
                    'left': "267px"
                })
                $navmenu.addClass('scrollable-menu');
            }
        });
    };

    panelCollapse();

});

function CreateDatePickerFromDiv(div_Contenedor) {
    $("#" + div_Contenedor + ' .dtpicker').datepicker({ dateFormat: 'dd/mm/yy' });

    $("#" + div_Contenedor + ' .dtpicker').bind({
        keypress: function (event) {
            return validarFormatoFecha((this), event);
        }
    });
}

/*JS FRONT END*/

//var collapse = function () {
//    var $btn = $('.btn-collapse');

//    $btn.on('click', function () {
//        var $el = $(this);
//        if ($btn.hasClass('active')) {
//            $el.closest('.collapse-gym').find('.bx-collapse').slideUp();
//            $el.removeClass('active');
//        }else{
//            $el.closest('.collapse-gym').find('.bx-collapse').slideDown();
//            $el.addClass('active');
//        }               
//    });
//}
//collapse();

//$(function () {

//    var menuLeftCollapse = function () {
//        var speed = 600;
//        $('body').on('click', '.btn_menu_collapse', function (ev) {

//            $el = $(this),
//            $elMenu = $el.next('.bx-collapse');
//            if (!$el.hasClass('active')) {
//                /*$elMenu.animate({
//                    top:46
//                }, speed);*/
//                $elMenu.slideDown();
//                $el.addClass('active');
//            } else {
//                //$elMenu.animate({
//                //    top:'-1000%'
//                //}, speed);
//                $elMenu.slideUp();
//                $el.removeClass('active');
//            }
//        });
//    };
//    menuLeftCollapse();
//});




$('body').tooltip({
    selector: "[data-toggle='tooltip']"
});
$('body').popover({
    selector: "[data-toggle='popover']"
});

//----------------------------
//MENU MULTINIVEL
//----------------------------
(function ($, window, document, undefined) {


    defaults = {
        type: '' || 'dropdown',
        speed: '' || 600,
        fx: ''
    };

    // The actual plugin constructor
    function menuMultinivel(element, options) {

        //SELECTORES
        $body = $('body'),
        $allElement = $('[data-menu]'),
        $element = $(element);
        $elementBtn = $element.children('a');
        $elementSubMenu = $allElement.find('.sub-menu');

        this.element = element;


        this.options = $.extend({}, defaults, options);
        this._defaults = defaults;

        this.init();
    }

    menuMultinivel.prototype = {
        init: function () {
            var that = this;
            that.constructor();
        },
        constructor: function () {
            var that = this;
            that.eventos();
        },
        eventos: function () {
            var that = this;
            $elementBtn.on('click', function (e) {
                if ($(this).closest('.menu-dropdown').data("menu") != "disabled" && !$body.hasClass('aside-closed'))
                    that.fx(that.options.type, $(this))
            })

        },
        fx: function (optionType) {

            var $el = arguments[1],
                $elLi = $el.closest('.menu-dropdown'),
                $elUl = $el.next('.sub-menu');

            switch (optionType) {
                case 'dropdown': {
                    if (!$elLi.hasClass('active')) {
                        $allElement.removeClass('active');
                        $elementSubMenu.slideUp();
                        $elLi.addClass('active');
                        $elUl.stop().slideDown(this.options.speed);
                    } else {
                        $elLi.removeClass('active');
                        $elUl.stop().slideUp(this.options.speed);
                    }
                }
            }
        }
    };


    $.fn.menuMultinivel = function (options) {
        return this.each(function () {
            if (!$.data(this, 'plugin_' + 'menuMultinivel')) {
                $.data(this, 'plugin_' + 'menuMultinivel',
                new menuMultinivel(this, options));
            }
        });
    }

})(jQuery, window, document);

/*--------------------------------------------------------------------------------------------------------- 
BODY HEIGHT BROWSER RESIZE
**//*---------------------------------------------------------------------------------------------------- */

$(function (bodyResize) {
    $(window).resize(function () {
        $('.main-content-inner').css("height", $(window).height() - 53);
    });
    $(window).trigger('resize');
});

//----------------------------
//ASIDE SLIDE
//----------------------------
; (function ($, window, document, undefined) {


    defaults = {
        speed: '' || 400,
        fx: ''
    };

    // The actual plugin constructor
    function asideSlide(element, options) {

        //SELECTORES
        $body = $('body');
        $element = $(element);
        $btn = $('[data-asideslide="button"]'),
        $slide = $('[data-asideslide="slide"]');

        this.element = element;


        this.options = $.extend({}, defaults, options);
        this._defaults = defaults;

        this.init();
    }

    asideSlide.prototype = {
        init: function () {
            var that = this;
            that.constructor();
        },
        constructor: function () {
            var that = this;
            if ($body.hasClass('aside-closed')) {
                $('.wrap-text, .bl-slide').css({
                    left: -237
                }, that.options.speed)
            }
            that.eventos();
        },
        eventos: function () {
            var that = this;
            $btn.on('click', function () {
                var $el = $(this),
                    $open = $el.hasClass('active');

                if (!$open) {
                    $body.addClass('aside-closed');
                    $body.removeClass('aside-open aside-open-complete');

                    $el.addClass('active');


                    $('.sub-menu').slideUp(function () {
                        $("[data-navajax='true']").each(function (key, val) {
                            var $el = $(this);

                            if ($el.hasClass('active')) {
                                $('.menu-dropdown').removeClass('active');
                                $el.closest('.menu-dropdown').addClass('active');
                            }
                        })
                    });
                    //setTimeout(function () {
                    $('.wrap-text').parent().stop().animate({
                        width: '60px'
                    });

                    $('.wrap-text, .bl-slide').stop().animate({
                        left: -237
                    }, that.options.speed, function () {
                        $body.addClass('aside-closed-complete');
                    });
                    //}, 500)


                } else {
                    $el.removeClass('active');
                    $body.addClass('aside-open');
                    $body.removeClass('aside-closed aside-closed-complete');

                    $('.wrap-text').parent().stop().animate({
                        width: '267px'
                    });
                    $('.wrap-text, .bl-slide').stop().animate({
                        left: 0
                    }, that.options.speed, function () {
                        $body.addClass('aside-open-complete');
                        if ($body.hasClass('aside-open-complete')) {

                            $('[data-menu]').each(function (key, val) {
                                if ($(this).hasClass('active')) {
                                    $(this).find('.sub-menu').slideDown();
                                }
                            });
                        }
                    });

                }
            });
        }
    };
    $.fn.asideSlide = function (options) {
        if (!$.data(this, 'plugin_' + 'asideSlide')) {
            $.data(this, 'plugin_' + 'asideSlide',
            new asideSlide(this, options));
        }
    }

})(jQuery, window, document);


var selectTheme = function () {
    var $body = $('body'),
        $wrapTheme = $('.select-ui'),
        $btn = $wrapTheme.find('li');

    $btn.on('click', function () {
        var $el = $(this);

        if ($el.hasClass('skin')) {
            $el.closest('ul').find('li').removeClass('active');
            $el.addClass('active');
            $body.removeClass(function (index, css) {
                return (css.match(/\bskin-\S+/g) || []).join(' ');
            }).addClass($el.attr('class'))
        }
        if ($el.hasClass('color')) {
            $el.closest('ul').find('li').removeClass('active');
            $el.addClass('active');
            $body.removeClass(function (index, css) {
                return (css.match(/\bbar-\S+/g) || []).join(' ');
            }).addClass($el.attr('class'))
        }
    });

    $('.close-ui').on('click', function () {
        $body.removeClass('show-ui');
        $('.select-ui').hide();
    })
};

selectTheme();

$(function () {
    $('[data-menu]').menuMultinivel({
        type: "dropdown"
    });
    $.fn.asideSlide();
});


function validateEmail(control) {

    var validEmail = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (validEmail.test($(control).val())) {
        return true;
    }
    else {
        return false;
    }
}