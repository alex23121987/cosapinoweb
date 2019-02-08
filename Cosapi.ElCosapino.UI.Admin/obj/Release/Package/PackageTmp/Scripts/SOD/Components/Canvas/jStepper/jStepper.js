ns('Pe.Canvas.UI.Web.Components');
Pe.Canvas.UI.Web.Components.jStepper = function (opts) {
    this.init(opts);
};

Pe.Canvas.UI.Web.Components.jStepper.prototype = {
    init: function (opts) {
        //var me = this;
        opts.inputFrom.jStepper(opts);
    }
}