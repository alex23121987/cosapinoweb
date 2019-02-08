/// Copyright (c) 2015.
/// All rights reserved.
/// <summary>
/// Libreria para la creación de grillas
/// </summary>
/// <remarks>
/// Creacion: 	Canvas(JLRR) 20150107 <br />
/// </remarks>
ns('Pe.Canvas.UI.Web.Components.Grid');
Pe.Canvas.UI.Web.Components.Grid = function (opts) {
    this.Init(opts);
};

Pe.Canvas.UI.Web.Components.Grid.prototype = {
    id: null,
    renderTo: null,
    columns: null,
    columnDefs: null,
    hasPaging: null,
    hasPagingServer: null,
    hasSelectionRows: null,
    hasTypeSelectionRows: null,
    selectionRowsEvent: null,
    selectionRowsCallback: null,
    instancia: null,
    searching: null,
    searchingText: null,
    pageLength: null,
    height: null,
    proxy: null,
    core: null,
    table: null,
    events: null,
    inlinetable: null,
    scrollY: null,
    scrollCollapse: null,
    data: null,
    async: null,
    dataPagination: null,
    returnCallbackComplete: null,
    ordering: null,
    filterColumn: null,
    fnRowCallback: null,
    fnCreatedRow: null,
    ///callback: null,
    Init: function (opts) {

        this.renderTo = $('#' + opts.renderTo);
        if (this.renderTo && this.renderTo.length == 0) {
            alert('', 'ERROR: Not renderTo defined');
            return;
        }
        this.async = opts.async == false ? false : true;
        this.columns = opts.columns && opts.columns != null ? opts.columns : null;
        this.columnDefs = opts.columnDefs && opts.columnDefs != null ? opts.columnDefs : null;
        this.proxy = opts.proxy && opts.proxy != null ? opts.proxy : null;
        this.events = opts.events && opts.events != null ? opts.events : null;
        this.hasPaging = opts.hasPaging != null ? opts.hasPaging : true;
        this.hasPagingServer = opts.hasPagingServer != null ? opts.hasPagingServer : true;
        this.height = opts.height != null ? opts.height : null;
        this.scrollY = opts.scrollY != null ? opts.scrollY : null;
        this.scrollX = opts.scrollX != null ? opts.scrollX : null;
        this.searching = opts.searching != null ? opts.searching : false;
        this.searchingText = opts.searchingText != null ? opts.searchingText : "";
        this.scrollCollapse = opts.scrollCollapse != null ? opts.scrollCollapse : null;
        this.hasSelectionRows = opts.hasSelectionRows != null ? opts.hasSelectionRows : true;
        this.hasTypeSelectionRows = opts.hasTypeSelectionRows != null ? opts.hasTypeSelectionRows : "checkbox";
        this.data = opts.data != null ? opts.data : null;
        this.pageLength = opts.pageLength != null ? opts.pageLength : 10;
        //this.callback = opts.callback != null ? opts.callback : null;
        this.selectionRowsEvent = opts.selectionRowsEvent != null ? opts.selectionRowsEvent : null;
        this.selectionRowsCallback = opts.selectionRowsCallback != null ? opts.selectionRowsCallback : null;
        this.instancia = opts.instancia != null ? opts.instancia : null;
        this.returnCallbackComplete = opts.returnCallbackComplete != null ? opts.returnCallbackComplete : null;
        this.ordering = opts.ordering != null ? opts.ordering : false;
        this.inlinetable = opts.inlinetable != null ? opts.inlinetable : null;
        this.filterColumn = opts.filterColumn != null ? opts.filterColumn : false;
        this.fnRowCallback = opts.fnRowCallback != null ? opts.fnRowCallback : null;
        this.fnCreatedRow = opts.fnCreatedRow != null ? opts.fnCreatedRow : null;


        if (this.hasPagingServer) {
            this.dataPagination = {
                NumeroPagina: 0,
                RegistrosPagina: this.pageLength
            }
        }
        if (this.columns == null) {
            alert('', 'ERROR: Not columms defined');
            return;
        }
        this.id = opts.id && opts.id != null ? opts.id : 'Canvas-Grid-';
        this._privateFunction.ImplementTableElement.apply(this, [this.renderTo]);
        this._privateFunction.ProcessSelectionRows.apply(this);

        if (this.data != null) {
            this._privateFunction.CreateGrid.apply(this, [this.data]);
        }
    },
    Load: function (params) {
        //debugger


        this.proxy.params = params;
        if (this.hasPagingServer == true) {
            this._privateFunction.CreateGrid.apply(this);
        }
        else {
            this._privateFunction.CallProxyWithoutPaginServer.apply(this);
        }
    },
    LoadAsync: function (params) {
        this._privateFunction.CreateGridAsync.call(this, params);
    },

    RecordsTotal: function () {
        return this.core.context[0]._iRecordsTotal;
    },
    ExportToExcel: function (name) {
        this.table.tableExport({ type: 'excel', escape: 'false', tableName: "shieldui-grid", documentName: name });

    },
    DestroyGrid: function () {
        if (this.core != null) {
            this.core.destroy();
            this.core = null;
        }
    },
    GetSelectedData: function () {
        var data = this.core.rows('.selected').data();
        var lista = new Array();
        for (var i = 0; i < data.length; i++) {
            lista.push(data[i]);
        }
        return lista;
    },
    SetSelectedData: function (compareKey, Key) {
        var me = this;
        var data = this.core.rows().data();
        var lista = new Array();
        for (var i = 0; i < data.length; i++) {
            if (data[i][compareKey] == Key) {
                //if (this.hasTypeSelectionRows == "checkbox") {
                //    $(this.core.rows().context[0].aoData[i].anCells[0]).find('input[type=checkbox]').prop("checked", "checked");
                //    $(this.core.rows().context[0].aoData[i].anCells[0]).find('input[type=checkbox]').closest("tr").addClass("selected");
                //} else {
                $(this.core.rows().context[0].aoData[i].anCells[0]).find('input[type=' + this.hasTypeSelectionRows + ']').prop("checked", "checked");
                $(this.core.rows().context[0].aoData[i].anCells[0]).find('input[type=' + this.hasTypeSelectionRows + ']').closest("tr").addClass("selected");
                //}
            }
        }
        if (this.hasTypeSelectionRows == "checkbox") {
            if (me.core.data().length != 0) {
                var idCheckHeader = 'chkHeader-' + me.core.rows().context[0].sTableId;
                $('#' + idCheckHeader).prop('checked', (me.core.data().length == me.core.rows('.selected').data().length));
            }
        }
        return lista;
    },
    SelectionRowsEvent: function () {
        var me = this;
        var data = me.core.rows().data();
        var idCheckHeader = "";
        var idCheckRow = "";
        if (this.hasTypeSelectionRows == "checkbox") {
            idCheckHeader = 'chkHeader-' + me.core.rows().context[0].sTableId;
            idCheckRow = 'chkRow-' + me.core.rows().context[0].sTableId;

            $('#' + idCheckHeader).off("click");
            $('#' + idCheckHeader).on("click", function () {
                var chk = $(this);
                var rows = me.table.find('tr');
                rows.removeClass('selected');
                if (chk.is(':checked')) {
                    rows.addClass('selected');
                }
                me.table.find('.' + idCheckRow).prop('checked', chk.is(':checked'));

                if (me.selectionRowsEvent != null) {
                    var lista = [];
                    for (var i = 0; i < data.length; i++) {
                        lista.push(data[i]);
                    }
                    me.selectionRowsEvent.callBack.call(this, this, lista);
                }
            })
        } else {
            idCheckRow = 'rdRow-' + me.core.rows().context[0].sTableId;
        }

        me.table.off("click", '.' + idCheckRow);
        me.table.on('click', '.' + idCheckRow, function () {
            var row = $(this).parents('tr');
            if (me.hasTypeSelectionRows != "checkbox") {
                $(this).closest("table").find('tr').removeClass("selected");
            }
            row.toggleClass('selected');
            if (idCheckHeader != "") {
                //var chkHeader = $(me.table.find('.' + idCheckHeader));
                $('#' + idCheckHeader).prop('checked', (me.core.data().length == me.core.rows('.selected').data().length));
            }

            if (me.selectionRowsEvent != null) {
                me.selectionRowsEvent.callBack.call(this, this, me.core.rows(row).data())
            }

        });

        //$('.' + idCheckHeader).off("clickPaginate");
        //$('.' + idCheckHeader).on("clickPaginate", function () {
        //    if (me.selectionRowsEvent != null) {
        //        var lista = [];
        //        for (var i = 0; i < data.length; i++) {
        //            lista.push(data[i]);
        //        }
        //        me.selectionRowsEvent.callBack.call(this, this, lista);
        //    }
        //})

        //if ($('.' + idCheckHeader).prop("checked")) {
        //    $('.' + idCheckHeader).trigger("clickPaginate");
        //}
    },
    _privateFunction: {
        CreateGrid: function (dataSource) {
            $("#" + $(this)[0].id).data("instancia", this);

            if (this.core != null) {
                this.core.destroy();
            }

            if (this.proxy != null) {
                $("#" + $(this)[0].id).data("paginateUrl", this.proxy.url);
                $("#" + $(this)[0].id).data("paginateSource", this.proxy.source);
                if (this.proxy.params != null) {
                    if (this.hasPagingServer) {
                        this.proxy.params.NumeroPagina = this.dataPagination.NumeroPagina;
                        this.proxy.params.RegistrosPagina = this.dataPagination.RegistrosPagina;
                    }
                    $("#" + $(this)[0].id).data("paginateData", this.proxy.params);
                }
            }

            var options = {
                columns: this.columns,
                filterColumn: this.filterColumn,
                autoWidth: false,
                data: this.hasPagingServer == false ? dataSource : null,
                paging: this.hasPaging,
                serverSide: this.hasPagingServer,
                ordering: this.ordering,
                searching: this.searching,
                columnDefs: this.columnDefs,
                ajax: this.hasPagingServer == true ? this._privateFunction.CallProxyPaginServer : null,
                pageLength: this.pageLength,
                inlinetable: this.inlinetable != null ? this.inlinetable : false,
                fnRowCallback: this.fnRowCallback,
                fnCreatedRow: this.fnCreatedRow
            }
            if (this.scrollY != null) {
                options.scrollY = this.scrollY;
            }
            if (this.scrollX != null) {
                options.scrollX = this.scrollX;
            }
            if (this.scrollCollapse != null) {
                options.scrollCollapse = this.scrollCollapse;
            }

            this.core = this.table.DataTable(options);

            if (!this.hasPagingServer) {
                if (this.hasSelectionRows) {
                    this.SelectionRowsEvent();
                    if (this.selectionRowsCallback != null) {
                        this.selectionRowsCallback();
                    }
                }
            }
        },
        CreateGridAsync: function (dataSource) {
            $("#" + $(this)[0].id).data("instancia", this);

            if (this.core != null) {
                this.core.destroy();
            }

            if (this.proxy != null) {
                $("#" + $(this)[0].id).data("paginateUrl", this.proxy.url);
                $("#" + $(this)[0].id).data("paginateSource", this.proxy.source);
                if (this.proxy.params != null) {
                    if (this.hasPagingServer) {
                        this.proxy.params.NumeroPagina = this.dataPagination.NumeroPagina;
                        this.proxy.params.RegistrosPagina = this.dataPagination.RegistrosPagina;
                    }
                    $("#" + $(this)[0].id).data("paginateData", this.proxy.params);
                }
            }
            this.core = this.table.DataTable({
                columns: this.columns,
                filterColumn: this.filterColumn,
                autoWidth: false,
                data: dataSource,
                paging: this.hasPaging,
                serverSide: false,
                ordering: this.ordering,
                searching: this.searching,
                columnDefs: this.columnDefs,
                //ajax: this.hasPagingServer == true ? this._privateFunction.CallProxyPaginServer : null,
                pageLength: this.pageLength,
                inlinetable: this.inlinetable != null ? this.inlinetable : false,
                fnRowCallback: this.fnRowCallback,
                fnCreatedRow: this.fnCreatedRow
            });

            //var instancia = $(that).data("instancia");

            if (this.hasSelectionRows) {
                this.SelectionRowsEvent();
                if (this.selectionRowsCallback != null) {
                    this.selectionRowsCallback();
                }
            }
            if (this.returnCallbackComplete != null) {
                this.returnCallbackComplete(this, records);
            }
        },
        ImplementTableElement: function (renderTo) {
            this.table = $('<table />').uniqueId();
            this.id = this.id + this.table.attr('id');
            this.table.attr('id', this.id);
            this.table.width('100%');
            this.table.attr('cellspacing', '0');

            this.table.addClass('table table-bordered table-hover text-center text-middle');
            renderTo.append(this.table);
            this._privateFunction.ProcessColumns.apply(this);

            if (this.events != null) {
                var me = this;
                $.each(this.events, function (index, event) {
                    if (event.isRowEvent) {
                        me.table.on(event.type, event.selector, function () {
                            var row = me.core.row($(this).parents('tr'))
                            var data = row.data();
                            event.callBack.apply(this, [row, data]);
                        });
                    }
                    else {
                        me.table.on(event.type, event.selector, event.callBack);
                    }
                });
            }
            return this.table;
        },
        ProcessColumns: function () {
            var me = this;
            this.events = this.events || new Array();
            $.each(this.columns, function (index, column) {
                if (column.actionButtons) {
                    $.each(column.actionButtons, function (index, action) {
                        me.events.push({
                            type: action.event.on,
                            selector: '.' + action.type.Icon + '-' + me.id + '-' + index,
                            callBack: action.event.callBack,
                            isRowEvent: true
                        });

                    });
                    column.mRender = function (data, type, full) {
                        var htmlSource = '';
                        $.each(column.actionButtons, function (index, action) {
                            var renderAction = action.validateRender ? action.validateRender(data, type, full) : true;
                            if (renderAction) {
                                htmlSource += action.type.Source(me.id, action.toolTip, index);
                            }
                        });
                        return htmlSource;
                    };
                };
            });
        },
        ProcessSelectionRows: function () {
            var me = this;
            if (this.hasSelectionRows) {
                if (this.hasTypeSelectionRows == "checkbox") {
                    var idCheckHeader = 'chkHeader-' + this.id;
                    var idCheckRow = 'chkRow-' + this.id;

                    this.columns.splice(0, 0, {
                        width: "1%",
                        filter: false,
                        orderable: false,
                        data: '', title: '<input class="' + idCheckHeader + '" id="' + idCheckHeader + '" type="checkbox">',
                        'mRender': function (data, type, full) {
                            var html = '';
                            html += '<input class="' + idCheckRow + '" type="checkbox">';
                            return html;
                        }
                    });
                } else if (this.hasTypeSelectionRows == "radio") {
                    var idCheckRow = 'rdRow-' + this.id;
                    this.columns.splice(0, 0, {
                        width: "1%",
                        filter: false,
                        orderable: false,
                        data: '', title: '',
                        'mRender': function (data, type, full) {
                            var html = '';
                            html += '<input class="' + idCheckRow + '" type="radio" name="rdItemGrid">';
                            return html;
                        }
                    });
                }
            }
        },
        CallProxyPaginServer: function (that, request, callback, settings) {
            var that = this;
            var instancia = $(that).data("instancia");
            if (instancia.proxy.params != null) {
                if (request.search != undefined) {
                    if (instancia.searching) {
                        instancia.proxy.params[instancia.searchingText] = request.search.value;
                    }
                    instancia.proxy.params.NumeroPagina = request.start / request.length;
                    instancia.proxy.params.RegistrosPagina = request.length;
                    instancia.proxy.params.Filtros = settings.Filtros;
                    $.each(settings.Filtros, function (index, value) {
                        if (value.Valor != null) {
                            eval("instancia.proxy.params." + value.Columna + "='" + value.Valor + "'");
                        } else {
                            eval("instancia.proxy.params." + value.Columna + "=null");
                        }
                    })
                    if (request.order.length != 0 && instancia.proxy.params.ColumnaOrden == undefined) {
                        if (request.order[0].columnName != "") {
                            instancia.proxy.params.ColumnaOrden = request.order[0].columnName;
                            instancia.proxy.params.TipoOrden = request.order[0].dir;
                        }
                    }
                }
                ////debugger
                var ajaxBuscarPaginadoServer = new Pe.Canvas.UI.Web.Components.Ajax({
                    action: instancia.proxy.url,
                    data: instancia.proxy.params,
                    onSuccess: function (data) {
                        //debugger
                        var records = data["Result"]; //data[instancia.proxy.source];
                        callback({
                            'data': records,
                            "iTotalRecords": records.length > 0 ? records[0].TotalRegistro : "0",
                            "iDisplayLength": records.length > 0 ? records[0].NumeroRegistro : "0",
                            "iTotalDisplayRecords": records.length > 0 ? records[0].TotalRegistro : "0"
                        });

                        if (instancia.hasSelectionRows) {
                            instancia.SelectionRowsEvent();
                            if (instancia.selectionRowsCallback != null) {
                                instancia.selectionRowsCallback();
                            }
                        }
                        if (instancia.returnCallbackComplete != null) {
                            instancia.returnCallbackComplete(instancia, records);
                        }
                    }
                });
            }

        },
        CallProxyWithoutPaginServer: function () {
            var me = this;
            var ajaxBuscar = new Pe.Canvas.UI.Web.Components.Ajax({
                action: this.proxy.url,
                data: this.proxy.params,
                async: this.async,
                onSuccess: function (data) {
                    //me._privateFunction.CreateGrid.apply(me, [data[me.proxy.source]]);
                    me._privateFunction.CreateGrid.apply(me, [data["Result"]]);
                    var instancia = $("#" + me.id).data("instancia");
                    if (instancia.hasSelectionRows) {
                        instancia.SelectionRowsEvent();
                        if (instancia.selectionRowsCallback != null) {
                            instancia.selectionRowsCallback();
                        }
                    }
                    if (instancia.returnCallbackComplete != null) {
                        //instancia.returnCallbackComplete(me, data[me.proxy.source]);
                        instancia.returnCallbackComplete(me, data["Result"]);
                    }
                }
            });
        }
    }
};
ns('Pe.Canvas.UI.Web.Components.GridAction');
Pe.Canvas.UI.Web.Components.GridAction = {
    Edit: {
        Class: 'edit',
        Icon: 'fa-edit',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : "Editar";
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Edit, tooltip, index);
        }
    },
    Search: {
        Class: 'search',
        Icon: 'fa-search',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : "Buscar";
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Search, tooltip, index);
        }
    },
    Delete: {
        Class: 'delete',
        Icon: 'fa-trash',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : 'Eliminar';
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Delete, tooltip, index);
        }
    },
    Seleccionar: {
        Class: 'seleccionar',
        Icon: 'fa-share',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : 'Seleccionar';//0Pe.Canvas.UI.Web.Components.GenericoResource.EtiquetaEliminar;
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Seleccionar, tooltip, index);
        }
    },
    DetalleBandeja: {
        Class: 'detalleBandeja',
        Icon: 'fa-tasks',
        Source: function (id, tooltip, index) {
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.DetalleBandeja, tooltip, index);
        }
    },
    Detail: {
        Class: 'detail',
        Icon: 'fa-tasks',
        Source: function (id, tooltip, index) {
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Detail, tooltip, index);
        }
    },
    View: {
        Class: 'view',
        Icon: 'fa-eye',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : Pe.Canvas.UI.Web.Components.GenericoResource.EtiquetaVer;
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.View, tooltip, index);
        }
    },
    Check: {
        Class: 'print',
        Icon: 'fa-check-square-o',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : Pe.Canvas.UI.Web.Components.GenericoResource.EtiquetaCheck;
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Check, tooltip, index);
        }
    },
    Print: {
        Class: 'print',
        Icon: 'fa-print',
        Source: function (id, tooltip, index) {

            tooltip = tooltip ? tooltip : Pe.Canvas.UI.Web.Components.GenericoResource.EtiquetaImprimir;
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Print, tooltip, index);
        }
    },
    //Upload: {
    //    Class: 'upload',
    //    Icon: 'fa-upload',
    //    Source: function (id, tooltip, index) {
    //        tooltip = tooltip ? tooltip : Pe.Canvas.UI.Web.Components.GenericoResource.EtiquetaCargarDocumento;
    //        return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Upload, tooltip, index);
    //    }
    //},

    Upload: {
        Class: 'upload',
        Icon: 'fa-upload',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : Pe.Canvas.UI.Web.Components.GenericoResource.EtiquetaCargarDocumento;
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Upload, tooltip, index);
        }
    },

    Download: {
        Class: 'download',
        Icon: 'fa-download',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : "Descargar Documento";
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.Download, tooltip, index);
        }
    },


    PaperClip: {
        Class: 'paperclip',
        Icon: 'fa-paperclip',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : Pe.Canvas.UI.Web.Components.GenericoResource.EtiquetaArchivo;
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.PaperClip, tooltip, index);
        }
    },
    File: {
        Class: 'file',
        Icon: 'fa-file-o',
        Source: function (id, tooltip, index) {
            tooltip = tooltip ? tooltip : Pe.Canvas.UI.Web.Components.GenericoResource.EtiquetaFile;
            return Pe.Canvas.UI.Web.Components.GridAction.Source(id, Pe.Canvas.UI.Web.Components.GridAction.File, tooltip, index);
        }
    },

    Source: function (id, atributos, tooltip, index) {
        var selector = atributos.Icon + '-' + id + '-' + index;
        return Pe.Canvas.UI.Web.Components.Util.RenderIcono(atributos.Class, atributos.Icon + ' ' + selector, tooltip);
    }
};