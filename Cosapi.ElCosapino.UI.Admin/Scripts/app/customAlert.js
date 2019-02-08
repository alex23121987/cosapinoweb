window.alert = function (title, mensaje, callbackok) {
    mensaje = mensaje.toString().split('\n').join('<br/>');
    MostrarAlerta(title, mensaje, 'ALERT', callbackok);
};

window.confirm = function (title, mensaje, callbackok, callbackCancel) {
    mensaje = mensaje.split('\n').join('<br/>');
    MostrarAlerta(title, mensaje, 'CONFIRM', callbackok, callbackCancel);
};

function MostrarAlerta(title, mensaje, tipo, callbackOk, callbackCancel) {
    if (tipo == "ALERT") {
        if (title == "") {
            title = "Mensaje de Alerta";
        }
        var x = BootstrapDialog.alert({
            title: title,
            message: function (dialog) {
                //var $message = $('<div class="BoxAlert"></div>');
                //var $message1 = $('<div class="alert alert-success"></div>');
                //$message1.html(mensaje);
                //$message.append($message1);
                //return $message;
                return mensaje;
            },
            type: BootstrapDialog.TYPE_SUCCESS,
            closable: true,
            buttonLabel: 'Aceptar',
            callback: function (result) {

                if (typeof callbackOk == 'function') { callbackOk(); }
            }
        });

    } else {
        var buttondialog = [{
            label: 'Aceptar',
            icon: 'glyphicon glyphicon-ok',
            action: function (dialog) {
                dialog.close();
                if (typeof callbackOk == 'function') {
                    callbackOk();
                }
            }
        }, {
            label: 'Cancelar',
            icon: 'glyphicon glyphicon-remove',
            action: function (dialog) {
                dialog.close();
                if (typeof callbackCancel == 'function') {
                    callbackCancel();
                }
            }
        }];
        BootstrapDialog.show({
            closable: true,
            closeByBackdrop: false,
            onhide: function (dialogRef) {
                //UnloadLoading();
            },
            title: 'Mensaje de Confirmación',
            message: function (dialog) {
                //var $message = $('<div class="BoxAlert"></div>');
                //var $message1 = $('<div class="alert alert-success"></div>');
                //$message1.html(mensaje);
                //$message.append($message1);
                //return $message;
                return mensaje;
            },
            buttons: buttondialog
        }, true);
    }
}

function MostrarPopUp(idModal, title, idmodalBody, size, clsdialog, buttonss) {
    if (idmodalBody != undefined && idmodalBody != "") {
        var $divModalBody = $("<div id=\'" + idmodalBody + "\'></div>");
    } else {
        var $divModalBody = $("<div id='divModalBody'></div>");
    }
    if (clsdialog == undefined) {
        var dialogInstance1 = new BootstrapDialog({
            id: idModal,
            closable: true,
            title: $("<h3>" + title + "</h3>"),
            message: $divModalBody,
            draggable: true,
            closeByBackdrop: false,
            buttons: buttonss
        });
    } else {
        var dialogInstance1 = new BootstrapDialog({
            cssClass: clsdialog,
            id: idModal,
            closable: true,
            title: $("<h3>" + title + "</h3>"),
            message: $divModalBody,
            draggable: true,
            closeByBackdrop: false,
            buttons: buttonss
        });
    } 
    dialogInstance1.open();
    if (size == 'SIZE_WIDE') {
        BootstrapDialog.dialogs[idModal].setSize(BootstrapDialog.SIZE_WIDE);
    }
}

