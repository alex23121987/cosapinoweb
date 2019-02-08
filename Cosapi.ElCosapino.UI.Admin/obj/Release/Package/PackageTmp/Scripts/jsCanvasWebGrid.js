(function ($) {

    /*
    Función para extender un objeto
    target [Object]: Objeto donde se adicionarán propiedades
    source [Object]: Objeto de donde se obtendrán las funcionalidades nuevas
    */
    function extend(target, source) {
        var prop;
        if (!target)
            target = {};
        for (prop in source) {
            target[prop] = source[prop];
        }
        return target;
    }

    function merge() {
        var i,
            len = arguments.length,
            ret = {},
            doCopy = function (copy, original) {
                var value, key;

                for (key in original) {
                    if (original.hasOwnProperty(key)) {
                        value = original[key];
                        if (typeof copy !== 'object') {
                            copy = {};
                        }
                        if (value && typeof value === 'object' && Object.prototype.toString.call(value) !== '[object Array]'
                                && typeof value.nodeType !== 'number') {
                            copy[key] = doCopy(copy[key] || {}, value);
                        } else {
                            copy[key] = original[key];
                        }
                    }
                }
                return copy;
            };
        for (i = 0; i < len; i++) {
            ret = doCopy(ret, arguments[i]);
        }

        return ret;
    }

    function IsNumberKeyPress(e) {
        var valid = "0123456789";
        var key = String.fromCharCode(event.keyCode);
        if (valid.indexOf("" + key) == "-1") {
            event.stopPropagation();
            window.event.keyCode = 0;
        }
    }

    $.fn.extend({
        CanvasWebGrid: function (args) {

            var DefaultOptions = {
                DirectionOrder: "",
                OrderBy: "",
                Filters: [],
                CurrentPageIndex: 1,
                RowsPerPage: 0,
                TotalPage: 1
            };
            var argsMerge = {};
            this.each(function () {
                var TABLE = this;
                argsMerge = merge(DefaultOptions, args);
                $(TABLE).data("config", argsMerge);
                CanvasSetEvents(TABLE, argsMerge);
            });

            function CanvasSetEvents(table, config) {
                CanvasSetEventLoad(table, config.callBackMethod);
                CanvasSetEvetClickRow(table);
            }

            function GetFilter(TABLE) {
                var Filters = [];
                $("thead input[type='text']", TABLE).each(function () {
                    var val = $.trim($(this).val());
                    if (val != '') {
                        Filters.push({ KeyFilter: $(this).closest("th").attr("data-key"), ValueFilter: val });
                    }
                });
                return Filters;
            }

            function CanvasSetEventLoad(table, MethodLoad) {

                $("thead th input[type='text']", table).unbind("keypress");
                $("thead th input[type='text']", table).bind("keypress", function (event) {
                    var key;
                    if (window.event)
                        key = window.event.keyCode; //IE
                    else
                        key = event.which; //firefox     
                    if (key == 13) {
                        var data = $(table).data("config");
                        data.Filters = GetFilter(table);
                        data.CurrentPageIndex = 1;
                        $(table).data("config", data);
                        eval(MethodLoad + "();");
                    }
                });

                $("thead th a", table).unbind("click");
                $("thead th a", table).bind("click", function (event) {
                    event.stopPropagation();
             
                    if ($(this).closest("th").attr("data-key") != '') {
                        if ($(this).attr("data-sordirection") != 'Ascending') {
                            $("a", $(this).closest("th")).attr("data-sordirection", "");
                            $(this).attr("data-sordirection", "Ascending");
                        }
                        else {
                            $("a", $(this).closest("th")).attr("data-sordirection", "");
                            $(this).attr("data-sordirection", "Descending");
                        }

                        var data = $(table).data("config");
                        data.OrderBy = $(this).closest("th").attr("data-key");
                        data.DirectionOrder = $(this).attr("data-sordirection");
                        $(table).data("config", data);

                        eval(MethodLoad + "();");
                    }
                });

                $("tfoot input[type='text']", table).unbind("keypress");
                $("tfoot input[type='text']", table).bind("keypress", function (event) {
                    IsNumberKeyPress(event);
                });

                $("tfoot input[type='text']", table).unbind("keyup");
                $("tfoot input[type='text']", table).bind("keyup", function (event) {
                    var key;
                    if (window.event)
                        key = window.event.keyCode; //IE
                    else
                        key = event.which; //firefox     
                    if (key == 13) {
                        var data = $(table).data("config");
                        data.CurrentPageIndex = $(this).val();
                        if (data.CurrentPageIndex > data.TotalPage)
                            data.CurrentPageIndex = data.TotalPage;
                        $(table).data("config", data);
                        eval(MethodLoad + "();");
                    }
                });

                $("tfoot input[id='FirstPage']", table).unbind("click");
                $("tfoot input[id='FirstPage']", table).bind("click", function () {
                    var data = $(table).data("config");
                    data.CurrentPageIndex = 1;
                    $(table).data("config", data);
                    eval(MethodLoad + "();");
                });

                $("tfoot input[id='PreviusPage']", table).unbind("click");
                $("tfoot input[id='PreviusPage']", table).bind("click", function () {
                    var data = $(table).data("config");
                    data.CurrentPageIndex = data.CurrentPageIndex - 1;
                    if (data.CurrentPageIndex < 1)
                        data.CurrentPageIndex = 1;
                    $(table).data("config", data);
                    eval(MethodLoad + "();");
                });

                $("tfoot input[id='NextPage']", table).unbind("click");
                $("tfoot input[id='NextPage']", table).bind("click", function () {
                    var data = $(table).data("config");
                    data.CurrentPageIndex = data.CurrentPageIndex + 1;
                    if (data.CurrentPageIndex > data.TotalPage)
                        data.CurrentPageIndex = data.TotalPage;
                    $(table).data("config", data);
                    eval(MethodLoad + "();");
                });

                $("tfoot input[id='LastPage']", table).unbind("click");
                $("tfoot input[id='LastPage']", table).bind("click", function () {
                    var data = $(table).data("config");
                    data.CurrentPageIndex = data.TotalPage;
                    $(table).data("config", data);
                    eval(MethodLoad + "();");
                });
            }

            function CanvasSetEvetClickRow(table) {
                $("tbody tr", table).unbind("click");
                $("tbody tr", table).bind("click", function () {
                    if ($(this).hasClass("selected_row")) {
                        $(this).removeClass("selected_row");
                    }
                    else {
                        $("tr", $(this).closest("tbody")).removeClass("selected_row");
                        $(this).addClass("selected_row");
                    }
                });
            }

        },
        CanvasDeserialice: function () {
            var TABLE = this;
            return $(TABLE).data("config");
        },
        RemoveFilter: function () {
            var TABLE = this;
            $(TABLE).data("config").Filters = [];
        },
        AddFilter: function (KeyFilter, ValueFilter) {
            var TABLE = this;
            var bolFindFilter = false;
            if ($(TABLE).data("config").Filters != null) {
                for (var j = 0; j < $(TABLE).data("config").Filters.length; j++) {
                    if ($(TABLE).data("config").Filters[j].KeyFilter == KeyFilter) {
                        $(TABLE).data("config").Filters[j].ValueFilter = ValueFilter;
                        bolFindFilter = true;
                    }
                }
            }
            if (!bolFindFilter) {
                NewFilter = {
                    KeyFilter: KeyFilter,
                    ValueFilter: ValueFilter
                }
                if ($(TABLE).data("config").Filters == null) {
                    $(TABLE).data("config").Filters = [];
                }
                $(TABLE).data("config").Filters.push(NewFilter);
            }
            //return data;
        }
    });
})(jQuery);

