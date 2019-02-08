ns('Pe.Canvas.UI.Web.Common.Resources')
Pe.Canvas.UI.Web.Common.Resources.LabelInformation = 'Información';
Pe.Canvas.UI.Web.Common.Resources.LabelAceptConfirmation = 'Aceptar';
Pe.Canvas.UI.Web.Common.Resources.LabelCancelConfirmation = 'Cancelar';
Pe.Canvas.UI.Web.Common.Resources.LabelWarning = 'Advertencia';

/// Copyright (c) 2015.
/// All rights reserved.
/// <summary>
/// Controlador de Mensajes
/// </summary>
/// <remarks>
/// Creacion: 	Canvas(CERS) 20150107 <br />
/// </remarks>
ns('Pe.Canvas.UI.Web.Components.Message');
Pe.Canvas.UI.Web.Components.Message = function (opts) {
    this.init(opts);
};

Pe.Canvas.UI.Web.Components.Message.prototype = {
    init: function (opts) {
        this._privateFunction.createMessage.apply(this, [opts]);
        ResultConfirm = false;
    },

    Information: function (opts) {
        opts.dialogClass = 'message-dialog-information';
        opts.position = { my: "right top", at: "right top", of: window };
        opts.title = opts.title ? opts.title : Pe.Canvas.UI.Web.Common.Resources.LabelInformation;
        opts.title = '<strong>' + opts.title + ' : </strong>'
        opts.message = opts.title + opts.message;
        opts.message = '<div class="alert alert-success">' + opts.message + '</div>';
        opts.modal = false;
        opts.minWidth = 550;
        opts.minHeight = 60;
        this._privateFunction.show.apply(this, [opts]);
        if (this.divDialog) {
            var me = this;
            this.informationTimeOut = setTimeout(function () {
                me.divDialog.close();
            }, 2500);
        }
    },

    InformationCustom: function (opts) {
        var me = this;
        opts.dialogClass = 'message-dialog-informationCustom';
        opts.position = { my: "right top", at: "right top", of: window };
        //opts.title = opts.title ? opts.title : Pe.Canvas.UI.Web.Common.Resources.LabelInformation;
        //opts.title = '<strong>' + opts.title + ' : </strong>'
        //opts.message = opts.title + opts.message;
        //opts.message = '<div class="alert alert-success">' + opts.message + '</div>';
        opts.title = opts.title ? opts.title : Pe.Canvas.UI.Web.Common.Resources.LabelInformation;
        opts.message = '<div class="cont-critico"><span class="' + opts.classimage + '"></span><span class="texto-item">' + opts.message + '</span></div>';
        opts.modal = false;
        //opts.minWidth = 550;
        //opts.minHeight = 60;
        this._privateFunction.show.apply(this, [opts]);
        if (this.divDialog) {
            var me = this;
            this.informationTimeOut = setTimeout(function () {
                me.divDialog.close();
            }, 2500);
        }
    },

    ResultConfirm: false,

    Confirmation: function (opts) {
        var me = this;
        opts.dialogClass = 'message-dialog-confirmation';
        opts.title = opts.title ? opts.title : Pe.Canvas.UI.Web.Common.Resources.LabelConfirmation;
        opts.buttons = [
                            {
                text: opts.textConfirmar ? opts.textConfirmar : Pe.Canvas.UI.Web.Common.Resources.LabelAceptConfirmation,
                                'class': 'btn btn-primary',
                                click: function () {
                                    ResultConfirm = true;
                                    if (me.divDialog) {
                                        me.divDialog.close();
                                    }
                                    setTimeout(function () {
                                        if (opts.onConfirm) {
                                            opts.onConfirm(true);
                                        }
                                        if (opts.onAccept) {
                                            opts.onAccept();
                                        }
                                    }, 100);
                                }
                            },
                            {
                                text: opts.textCancelar ? opts.textCancelar : Pe.Canvas.UI.Web.Common.Resources.LabelCancelConfirmation,
                                'class': 'btn btn-info',
                                click: function () {
                                    if (me.divDialog) {
                                        me.divDialog.close();
                                    }
                                    setTimeout(function () {
                                        if (opts.onConfirm) {
                                            opts.onConfirm(false);
                                        }
                                        if (opts.onCancel) {
                                            opts.onCancel();
                                        }
                                    }, 100);

                                }
                            }
        ];
        this._privateFunction.show.apply(this, [opts]);
    },

    ConfirmationCustom: function (opts) {
        var me = this;
        opts.dialogClass = 'message-dialog-confirmation';
        opts.title = opts.title ? opts.title : Pe.Canvas.UI.Web.Common.Resources.LabelConfirmation;
        opts.buttons = [
                            {
                text: opts.textConfirmar ? opts.textConfirmar : Pe.Canvas.UI.Web.Common.Resources.LabelAceptConfirmation,
                                'class': 'btn btn-primary',
                                click: function () {
                                    ResultConfirm = true;
                                    if (me.divDialog) {
                                        me.divDialog.close();
                                    }
                                    setTimeout(function () {
                                        if (opts.onConfirm) {
                                            opts.onConfirm(true);
                                        }
                                        if (opts.onAccept) {
                                            opts.onAccept();
                                        }
                                    }, 100);
                                }
                            },
                            {
                                text: opts.textCancelar ? opts.textCancelar : Pe.Canvas.UI.Web.Common.Resources.LabelCancelConfirmation,
                                'class': 'btn btn-info',
                                click: function () {
                                    if (me.divDialog) {
                                        me.divDialog.close();
                                    }
                                    setTimeout(function () {
                                        if (opts.onConfirm) {
                                            opts.onConfirm(false);
                                        }
                                        if (opts.onCancel) {
                                            opts.onCancel();
                                        }
                                    }, 100);

                                }
                            }
        ];
        opts.message = '<div class="cont-critico"><span class="' + opts.classimage + '"></span><span class="texto-item">' + opts.message + '</span></div>';
        this._privateFunction.show.apply(this, [opts]);
    },

    Warning: function (opts) {
        opts.dialogClass = 'message-dialog-warning';
        opts.title = opts.title ? opts.title : Pe.Canvas.UI.Web.Common.Resources.LabelWarning;
        opts.message = '<div class="alert alert-warning">' + opts.message + '</div>';
        this._privateFunction.show.apply(this, [opts]);
    },

    Error: function (opts) {
        opts.dialogClass = 'message-dialog-error';
        opts.title = opts.title ? opts.title : 'Error';
        opts.message = '<div class="alert alert-danger">' + opts.message + '</div>';
        this._privateFunction.show.apply(this, [opts]);
    },

    Basic: function (opts) {
        var me = this;
        opts.dialogClass = 'message-dialog';
        opts.title = opts.title ? opts.title : '';
        opts.message = '<div>' + opts.message + '</div>';
        this._privateFunction.show.apply(this, [opts]);
        return me;
    },

    setMessage: function (message) {
        this.divDialog.setContent(message);
    },

    onClose: null,

    destroy: function () {
        if (this.divDialog) {
            this.divDialog.destroy();
        }
    },
    _privateFunction: {
        createMessage: function (opts) {
            var me = this;
            this.divDialog = new Pe.Canvas.UI.Web.Components.Dialog({
                closeOnEscape: false,
                close: function (event, ui) { if (me.onClose && me.onClose != null) { me.onClose(event, ui); } },
                resizable: false,
                closeText: "Close",
                width: 300
            });
            this.divDialog.setClass('message-dialog-confirmation');
        },
        show: function (opts) {
            if (this.divDialog) {
                if (this.informationTimeOut) {
                    clearTimeout(this.informationTimeOut);
                }
                //opts.position = opts.position ? opts.position : { my: "center", at: "center", of: window };
                opts.modal = opts.modal == false ? opts.modal : true;
                //this.divDialog.setPosition(opts.position);
                this.divDialog.close();
                this.onClose = opts.onClose ? opts.onClose : null;
                this.divDialog.setTitle(opts.title);
                this.setMessage(opts.message);
                this.divDialog.setButtons(opts.buttons);
                this.divDialog.setClass(opts.dialogClass);
                this.divDialog.setModal(opts.modal);
                this.divDialog.setMinWidth(opts.minWidth);
                this.divDialog.setMinHeight(opts.minHeight);
                this.divDialog.open();
            }
        }
    }
};
