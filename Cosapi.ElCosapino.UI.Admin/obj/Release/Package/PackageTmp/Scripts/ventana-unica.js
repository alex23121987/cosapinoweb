//Spiner
var $loading = $('#loadingDiv').hide();

$(document).ajaxStart(function () {$loading.show();}).ajaxStop(function () {$loading.hide();});
//FIn spiner

function onChangeFloatInput(inputElement) {
    var value = $(inputElement).val().replace(',', '.');
    if (!isNumber(value)) {
        value = 0;
        $(inputElement).val('');
    }
    value = parseFloat(value);
    $(inputElement).val(value.toFixed(2));
    return value;
}

function onKeyDecimal(e, thix) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if (document.getElementById(thix.id).value.indexOf('.') != -1 && keynum == 46)
        return false;
    if ((keynum == 8 || keynum == 48 || keynum == 46))
        return true;
    if (keynum <= 47 || keynum >= 58) return false;
    return /\d/.test(String.fromCharCode(keynum));
}

function getElementsByClassName(node, classname) {
    var a = [];
    var re = new RegExp('(^| )' + classname + '( |$)');
    var els = node.getElementsByTagName("*");
    for (var i = 0, j = els.length; i < j; i++)
        if (re.test(els[i].className)) a.push(els[i]);
    return a;
}


function isNullOrEmpty(stringValue) {
    return stringValue == null || stringValue.trim().length == 0;
}

/**
 * Valida si un string es una fecha valida segun el formato del parametro format, si format es null o vacio se toma por defecto DD/MM/YYYY
 * @param {string} dateString
 */
function isDateStringValid(dateString, format) {
    if (isNullOrEmpty(format))
        format = 'DD/MM/YYYY';
    return moment(dateString, format, true).isValid();
}

function dateToShortString(date) {
    return moment(date).format('DD/MM/YYYY');
}

function decimalToString(number) {
    return parseFloat(Math.round(number * 100) / 100).toFixed(2);
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function mostrarError(mensaje, callback) {
    //var msg = alertify.error('Default message');
    //msg.delay(8).setContent(mensaje);
    if (callback)
        alertify.alert('Error', mensaje, callback);
    else
        alertify.alert('Error', mensaje);
}
function mostrarMensajeConTitulo(titulo, detalles, callback) {
    if (callback)
        alertify.alert(titulo, detalles, callback);
    else
        alertify.alert(titulo, detalles);
}
function mostrarMensaje(mensaje, callback) {
    if (callback)
        alertify.alert('Mensaje', mensaje, callback);
    else
        alertify.alert('Mensaje', mensaje);
}
//crea una lista UL con los mensaje del arreglo
//limite por defecto es 5, y es usado para limitar mostrar solo cierta cantidad de mensajes.
function crearListaMensajes(mensajesArray, limite) {
    if (!limite || limite == 0)
        limite = 5;

    var ul = [];
    ul.push('<ul>');
    mensajesArray.forEach(function (item, index) {
        if (index + 1 > limite)
            return false;
        ul.push('<li>' + item + '</li>');        
    });
    ul.push('</ul>');
    return ul.join('');
}

function bloquearBotonesProceso(bloquear) {
    $('.btn-proceso').prop('disabled', bloquear);

    if (bloquear === true)
        $('label.btn-proceso').addClass('label-disabled');
    else
        $('label.btn-proceso').removeClass('label-disabled');
}

//usada para asignar el valor de la propiedad "language" de los datatables
// por ejemplo "language": getDatatableLanguagueConfig()
function getDatatableLanguagueConfig() {
    var config = {
        "lengthMenu": "Ver _MENU_ filas por pág.",
        "zeroRecords": "Ningún registro encontrado",
        "info": "Mostrando pág. _PAGE_ de _PAGES_",
        "infoEmpty": "Sin registros disponibles",
        "infoFiltered": "(filtrados de _MAX_ registros)",
        "paginate": {
            "previous": "Anterior",
            "next": "Sig.",
            "last": "Ultima",
            "first": "Primera"
        },
        "search": "Buscar"
    };
    return config;
}

function setActiveMenu(menuItem) {
    $('#navMenuLateral .nav .active').removeClass('active');
    $('#navMenuLateral ul > li#' + menuItem).addClass('active');
}

function stringToDate(_date, _delimiter) {    
    var formatItems = _date.split(_delimiter);   
    var dia = formatItems[0];
    var mes = formatItems[1];
    var anio = formatItems[2];

    var formatedDate = mes + _delimiter + dia + _delimiter+ anio;
    return formatedDate;
}

function SoloNumeros(evt) {
    var nav4 = window.Event ? true : false;
    var key = nav4 ? evt.which : evt.keyCode;
    return (key <= 13 || (key >= 48 && key <= 57) || key == 08);
}
function SoloLetras(evt) {
    var nav4 = window.Event ? true : false;
    var key = nav4 ? evt.which : evt.keyCode;
    return (key > 31 && (key < 48 || key > 57) || key == 08);
}
function SoloLetrasNros(evt) {
    

    var nav4 = window.Event ? true : false;
    var key = nav4 ? evt.which : evt.keyCode;
    if ((key.charCode < 97 || key.charCode > 122)//letras mayusculas
        && (key.charCode < 65 || key.charCode > 90) //letras minusculas
        && (key.charCode != 45) //retroceso
        && (key.charCode != 241) //ñ
        && (key.charCode != 209) //Ñ
        && (key.charCode != 32) //espacio
        && (key.charCode != 225) //á
        && (key.charCode != 233) //é
        && (key.charCode != 237) //í
        && (key.charCode != 243) //ó
        && (key.charCode != 250) //ú
        && (key.charCode != 193) //Á
        && (key.charCode != 201) //É
        && (key.charCode != 205) //Í
        && (key.charCode != 211) //Ó
        && (key.charCode != 218) //Ú

    )
        return false;
}

function MostrarOcultarDiv(id) {
    if ($('#' + id).css('display') == 'none') {
        $('#' + id).show();
    } else {
        $('#' + id).hide();
    }
}