var dtSearchValue = "";
function selectListItem(text, value, checked) {
    this.Text = text;
    this.Value = value;
    this.Checked = checked != undefined && checked;
}
function ServerParams(ctrlname, value, expression) {
    this.CtrlName = name;
    this.Value = value;
    this.Expression = expression;
}

function BindGrid(oSetting) {
    var _columnDataType = { int: "int", decimal: "decimal", date: "date", bool: "bool", custom: "custom" };
    var _filterBy = { Contains: "contains", Equal: "=", LessThan: "<", LessThanOrEqual: "<=", GreaterThan: ">", GreaterThanOrEqual: ">=", NotEqual: "!=" };
    var _fninitDefaultExpression = function () {
        var _self = this;
        if (_self.Expressions && _self.Expressions.length === 0) {
            for (var index in Object.keys(_filterBy)) {
                var key = Object.keys(_filterBy)[index];
                var value = _filterBy[key];
                _self.Expressions.push({ Name: key, Expression: value });
            }
        }
    };
    var _fnExpression = function (dataType) {
        _fninitDefaultExpression();
        var _self = this;
        var exp = [];
        if (dataType) {
            switch (dataType.toLowerCase()) {
                case _columnDataType.int:
                case _columnDataType.decimal:
                    exp = _self.Expressions.filter(function (item) { return item.Expression !== _filterBy.Contains; });
                    break;
                case _columnDataType.bool:
                    break;

                case _columnDataType.date:
                    break;

                default:
                    exp = _self.Expressions.filter(function (item) { return item.Expression === _filterBy.Contains || item.Expression === _filterBy.Equal; });
                    break;
            }
            return exp;
        }

        return arrExpression;
    };
    var _fnEnableExpression = function (dataType) {
        var is = true;
        if (dataType) {
            switch (dataType.toLowerCase()) {
                case _columnDataType.bool:
                case _columnDataType.date:
                case _columnDataType.custom:
                    is = false;
                    break;
                default:
            }
        }

        return is;
    };

    var _fnCreatefilters = function () {
        var _self = this,
            html = '';
        if (!_self.oSetting.filterByCtrls) {
            if ($(document).data("bulkselectable") != undefined && $(document).data("bulkselectable")) {
                var _selectAllColumn = {
                    data: null,
                    name: "",
                    orderable: false,
                    search: { value: "", regex: false },
                    searchable: true,
                    title: '<div class="text-center custom-checkbox"><input id="select-all" onclick="_fnSelectAll(this)" class="skip-iCheck" type="checkbox"></input> <label for="select-all"></label> </div>',
                    visible: true,
                    width: "40px",
                    'targets': 0,
                    'render': function (data, type, record) {
                        var recordType = record.hasOwnProperty("Type") ? (record.Type == undefined || record.Type == null ? '' : record.Type) : '';
                        var recordCancel = record.hasOwnProperty("IsCancelled") ? (record.IsCancelled == undefined || record.IsCancelled == false ? 'false' : record.IsCancelled) : 'false';
                        var ckeckBoxchecked = selectedCheckBoxes.indexOf(record.Id) > -1 ? 'checked="checked"' : '';
                        return '<div class="text-center custom-checkbox"><input type="checkbox" data-record-type="' + recordType + '" data-record-cancel="' + recordCancel + '" onclick="_fnSelect(this)" class="skip-iCheck" ' + ckeckBoxchecked + ' id="' + record.Id + '"  value="' + record.Id + '"> <label for="' + record.Id + '"></label> </div>';
                    }
                };

                _self.oSetting.columns.splice(0, 0, _selectAllColumn);
            }
            for (var index in _self.oSetting.columns) {
                var column = _self.oSetting.columns[index];
                var key = column.data;
                // column data or type is valid
                if (column.data && column.type) {
                    var sTitle = column.title ? column.title : key;
                    html += '<div class="form-group col-xs-12 ">';
                    html += '<label class="control-label" for="search_' + key + '" >' + sTitle + '</label>';
                    html += _fnHtmlCtrl(column);
                    html += '</div>';
                }
                if (html !== '') {
                    $(_self.selector.dvfilterBody).html('').html(html);
                    _self.oSetting.filterByCtrls = true;
                }

            }
        }
    };

    var _fnGetDropdown = function (items, name) {
        var html = '';
        var option = '<option value="">- Select -</option>';
        for (var index in items) {
            var text = items[index].Text;
            var value = items[index].Value;
            var checked = items[index].Checked;
            option += '<option value="' + value + '" ' + (checked && checked === true ? "checked='checked'" : "") + '>' + text + ' </options>';
        }

        html += '<select name="' + name + '" data-table="filters"  class="no-right-padd form-control"  >';

        html += option;
        html += '</select>';
        var selectedValue = "";;
        if (items && items.length > 0) {
            var selected = items.filter(function (item) {
                return item.Selected === true;
            });
            if (selected.length > 0) {
                selectedValue = selected[0].Value;
            }
        }

        html += '<input type="hidden" name="search.' + name + '" value="' + selectedValue + '"  />';
        return html;
    };

    var _fnGetRadio = function (items, name) {
        var radio = '';
        for (var index in items) {
            var text = items[index].Text;
            var value = items[index].Value;
            var checked = items[index].Checked;
            radio += '<div class="icheck-outer">';
            radio += '<input data-table="filters" class="iCheck"  type="radio" ' + (checked ? "data-default='" + value + "'" : "") + '  name="' + name + '" value="' + value + '" ' + (checked && checked == true ? "checked='checked'" : "") + '  /> &nbsp;' + text;
            radio += '</div>';
        }
        if (items && items.length > 0) {
            var selected = items.filter(function (item) {
                return item.Checked === true;
            });

            if (selected.length > 0) {
                radio += '<input type="hidden" data-type="radio" name="search.' + name + '" value="' + selected[0].Value + '" />';
            }
        }

        return radio;
    };

    var _fnHtmlCtrl = function (column) {
        var html = '';
        if (column && column.data && column.type) {
            var key = column.data;
            var className = "", colmd = "col-xs-12";
            var isExpression = _fnEnableExpression(column.type);

            html += '<div class="col-md-12 no-left-padd">';
            // drpdown expression
            var condition = _fnExpression(column.type);
            if (condition && condition.length && isExpression) {
                var option = '';
                for (var index in condition) {
                    option += '<option value="' + condition[index].Expression + '">' + condition[index].Name + '</option>';
                }
                html += '<div class="col-md-3 no-left-padd"  style="padding-right: 3px;">';
                html += '<select class="form-control col-md-3" data-table="expression" data-key="' + key + '" >';
                html += option;
                html += '</select>';
                html += '</div>';
                colmd = " col-md-9 ";
            }

            switch (column.type.toLowerCase()) {
                case _columnDataType.int:
                    className = 'numericvalues';
                    break;
                case _columnDataType.decimal:
                    className = 'decimalvalues';
                    break;
            }

            html += '<div class="no-left-padd ' + colmd + ' ">';

            switch (column.type.toLowerCase()) {
                case _columnDataType.date:

                    html += '<div class="col-md-6 no-left-padd ">';
                    html += '<div class="input-group date">';
                    html += '    <input type="text" data-table="filters"  class="form-control dob"   data-filter=">="   name="search.' + key + '" placeholder="From Date" >';
                    html += '    <span class="input-group-addon">';
                    html += '        <span class="fa fa-calendar"></span>';
                    html += '    </span>';
                    html += '</div>';
                    html += '</div>';
                    html += '<div class="input-group date">';
                    html += '    <input type="text" data-table="filters"  class="form-control dob" data-filter="<=" name="search.' + key + '"  placeholder="To Date">';
                    html += '    <span class="input-group-addon">';
                    html += '        <span class="fa fa-calendar"></span>';
                    html += '    </span>';
                    html += '</div>';
                    html += '</div>';

                    break;
                case _columnDataType.custom:
                    if (column.bind && column.type && column.type == "custom" && column.bind.ctrl && column.bind.data) {
                        if (column.bind.ctrl == "radio") {
                            html += _fnGetRadio(column.bind.data, key);
                        }
                        else if (column.bind.ctrl == "selectlist") {
                            html += _fnGetDropdown(column.bind.data, key);
                        }
                    }

                    break;
                case _columnDataType.bool:
                    // simple bool condition
                    if (!column.bind) {
                        var items = [new selectListItem("Yes", "true"), new selectListItem("No", "false"), new selectListItem("Both", "", true)];
                        html += _fnGetRadio(items, key)
                    } else if (column.bind && column.bind.type && column.bind.data) {
                        html += _fnGetRadio(column.bind.data, key)
                    }

                    break;
                default:
                    html += '<input data-table="filters" class="no-right-padd form-control ' + className + '" name="search.' + key + '" type="text" value="" id="search_' + key + '">';
                    break;
            }
            html += '</div>';
            html += '</div>';
        }
        return html;
    };

    var _funResetCtrl = function () {
        $(this.selector.filter).filter(":text").val('');
        $(this.selector.filter).filter("select").each(function () {
            $(this).find("option:first").prop("selected", true);
            $(this).change();
        });
        $(this.selector.filter).filter(":radio[data-default]").each(function () {
            var name = $(this).attr("name");
            var value = $(this).data("default") || "";
            var $radio = $(':radio[name="' + name + '"][data-default="' + value + '"]');
            if ($radio.length > 0) {
                $radio.iCheck("check", true);
                $('[name="search.' + name + '"]').val(value);
            }
        });
    };

    var _fnRandomString = function (length) {
        var chars = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
        var result = '';
        for (var i = length; i > 0; --i) result += chars[Math.floor(Math.random() * chars.length)];
        return result;
    }

    var _fnModal = function (isShow) {
        var visible = isShow || 'hide';
        $(this.selector.dvfilterBox).modal(visible);
    };

    var _fnUpdateTitles = function (_self) {
        var oColumns = _self.oSetting.columns;
        var sColumns = _self.DataTable.api().ajax.params().columns;
        for (var index in sColumns) {
            var data = sColumns[index].data;

            var column = oColumns.filter(function (item) {
                return item.data === data;
            });
            if (column && column.length > 0) {
                sColumns[index].name = column[0].sTitle;
            }
        }
    }

    this.Table = [];

    this.Filters = [];

    this.Expressions = [];

    this.oSetting = $.extend(oSetting, {
        export: "",
        pageSize: oSetting.pageSize == undefined ? [10, 25, 50, 100, 500] : oSetting.pageSize,
        filterByCtrls: false,
        ajaxUrl: oSetting.url || host + "/",
        fnCallBack: oSetting.fnCallBack || null,
        dataKeys: oSetting.dataKeys || []
    });

    this.on = { search: '[data-table="search"]', expression: '[data-table="expression"]', reset: '[data-table="reset"]', togglefilter: '[data-table="togglefilter"]', export: { excel: "#excellExport", pdf: '[data-table="export-pdf"]' } };
    this.selector = { dvfilterBox: '[data-table="dvfilterBox"]', dvfilterBody: '[data-table="dvfilterBody"]', table: '[data-table="grid"]', filter: '[data-table="filters"]' };
    this.searchGrid = function (keyword) {
        var $list = $(this.selector.table + ' tbody').find('tr');
        $list.sort(function (a, b) {
            if (keyword == "")
                return $(a).data('index') > $(b).data('index');
            else
                return $(a).text().indexOf(keyword) < $(b).text().indexOf(keyword);
        });
        $list.detach().appendTo($(this.selector.table + ' tbody'));
        $list.each(function () {
            if ($(this).text().indexOf(keyword) != -1 && keyword != "")
                $(this).removeClass('not-found').addClass('found');
            else
                $(this).removeClass('found').addClass('not-found');
        });
        $(this.selector.table + ' tbody tr:odd').removeClass('even').addClass('odd');
        $(this.selector.table + ' tbody tr:even').removeClass('odd').addClass('even');
    };

    this.fnServerParams = function (aoData) {
        var _self = this;

        _self.Filters = [];

        var x = aoData;
        for (var i in aoData.columns) {
            if (aoData.columns[i])
                aoData.columns[i].visible = this.oSetting.columns[i].hasOwnProperty("visible") ? this.oSetting.columns[i].visible : true;
            aoData.columns[i].title = this.oSetting.columns[i].hasOwnProperty("title") && this.oSetting.columns[i].title != null && this.oSetting.columns[i].title.length > 0 ?
                this.oSetting.columns[i].title : this.oSetting.columns[i].data;

            var ctrlName = '[name="search.' + aoData.columns[i].data + '"]';
            if ($(ctrlName).length > 0) {
                var search_value = "";
                var search_filter_name = "";
                $(ctrlName).each(function () {
                    var value = $(this).val().trim();

                    var required = $(this).data('required') != undefined;
                    if (value != '') {
                        search_value += "@#$" + value;
                        search_filter_name += "@#$" + ($(this).data('filter') == undefined ? "=" : $(this).data('filter'));
                    } else if (required) {
                        search_value += "@#$" + _self._fnRandomString(10);
                        search_filter_name += "@#$" + ($(this).data('filter') == undefined ? "=" : $(this).data('filter'));
                    }

                    var $element = $("[name='search." + aoData.columns[i].data + "']:hidden");
                    var hiddenType = $element.data("type");

                    var isfxSearch = $element.length > 0 && hiddenType != undefined && hiddenType != "radio";
                    if (value != '' && !isfxSearch) {
                        _self.Filters.push(new ServerParams(aoData.columns[i].data, value, ($(this).data('filter') == undefined ? "=" : $(this).data('filter'))));
                    }
                });
                aoData.columns[i].search.value = search_value;
                aoData.columns[i].name = search_filter_name;
                aoData.columns[i].orderable = false;
            }
        }
        aoData.search.value = dtSearchValue;

        if (_self.oSetting.dataKeys instanceof Array && _self.oSetting.dataKeys.length > 0) {
            for (var j = 0; j < _self.oSetting.dataKeys.length; j++) {
                $(document).removeData(_self.oSetting.dataKeys[j]);
            }
        }
    };

    this.Bind = (function () {
        var _self = this;

        // init default filters
        _fnCreatefilters();

        // DataTable pagging info
        $.fn.dataTableExt.oApi.fnPagingInfo = function (oSettings) {
            return {
                "iStart": oSettings._iDisplayStart,
                "iEnd": oSettings.fnDisplayEnd(),
                "iLength": oSettings._iDisplayLength,
                "iTotal": oSettings.fnRecordsTotal(),
                "iFilteredTotal": oSettings.fnRecordsDisplay(),
                "iPage": Math.ceil(oSettings._iDisplayStart / oSettings._iDisplayLength),
                "iTotalPages": Math.ceil(oSettings.fnRecordsDisplay() / oSettings._iDisplayLength)
            };
        }

        //export grid
        $(document).on("click", _self.on.export.excel, function (e) {
            var row = $('#DataTables_Table_0 tbody').find('td');
            if (row.length == 1) {
                $.notify("There is no data for download", "warn");
                return false;
            }
            e.preventDefault();
            _fnUpdateTitles(_self);
            var $frm = $("#frmExportExcel");
            var $excelReplaceUrl = $(".replaceurl[data-for='excel']").eq(0);
            if ($excelReplaceUrl.length > 0 && $excelReplaceUrl.data("url")) {
                $frm.attr("action", $excelReplaceUrl.data("url"));
            }
            _self.fnServerParams(_self.DataTable.api().ajax.params())
            $frm.find(":hidden").val(JSON.stringify(_self.DataTable.api().ajax.params()));
            $frm.submit();
        });

        //export grid as pdf
        $(document).on('click', "#pdfExport", function (e) {
            var row = $('#DataTables_Table_0 tbody').find('td');
            if (row.length == 1) {
                $.notify("There is no data for download", "warn");
                return false;
            }

            e.preventDefault();
            _fnUpdateTitles(_self);
            var $frm = $("#frmExportPdf");
            _self.fnServerParams(_self.DataTable.api().ajax.params())
            $frm.find(":hidden").val(JSON.stringify(_self.DataTable.api().ajax.params()));
            $frm.submit();
        });

        //DataTable Configuratio info
        _self.DataTable = $(_self.selector.table).dataTable({
            "fixedHeader": {
                header: true
            },
            "order": _self.oSetting.order || [],
            "scroll": false,
            "sDom": _self.oSetting.sDom == undefined ? 'Tlfrtip' : _self.oSetting.sDom,
            "stateSave": false,
            "colVis": _self.oSetting.colVis,
            "processing": true,
            "oLoadingSrc": "<img src='" + gifSpinner + "' class='vertical-align' />",
            "oRender": _self.oSetting.render || "grid",
            "serverSide": true,
            "oLanguage": { "sProcessing": spinnerLoading },
            "bFilter": false,
            "bAutoWidth": false,
            "pagingType": "full_numbers",
            "lengthMenu": _self.oSetting.pageSize,
            "pageLength": _self.oSetting.pageLength == undefined ? _self.oSetting.pageSize[0] : _self.oSetting.pageLength,
            "oActionButtons": '[data-buttons="filter"]',
            "ajax": {
                "url": _self.oSetting.ajaxUrl, "type": "POST",
                error: function (req, xht, response) { }
            },
            "columns": _self.oSetting.columns,
            "fnDrawCallback": function (response, json) { },
            "initComplete": function (settings, json) { },
            "fnServerParams": function (aoData) {
                _self.fnServerParams(aoData);
            }
        });
        if ($.isFunction(_self.oSetting.fnCallBack)) {
            _self.DataTable.fnSettings().aoDrawCallback.push({
                "fn": _self.oSetting.fnCallBack, "sName": "fnCallBack"
            });
        }

        // DataTable draw
        _self.DataTable.on('draw.dt', function (response) {
            var $fixedSearch = $('[data-table="fixed-search"]');
            if (dtSearchValue != "") {
                dtSearchValue = "";
            }
            $(_self.on.togglefilter).toggle(_self.oSetting.filterByCtrls);

            $(_self.on.reset).toggle(_self.Filters.length > 0);

            if (typeof initPluggins !== 'initPluggins' && $.isFunction(initPluggins)) {
                initPluggins()
            }
            if ($.fn.tooltip != undefined) {
                $('[data-toggle="tooltip"]').tooltip();
            }
             
            changeActiveStatus();
        });

        // record custom search on button search
        $('html').on('click', _self.on.search, function (e) {
            e.preventDefault();
            _fnModal('hide');
            _self.DataTable.fnDraw();
        });
        // hide model
        $('html').on('click', '[data-table="close-filter"]', function (e) {
            e.preventDefault();
            _fnModal('hide');
        });

        // Reset Custom Searching on reset button
        $('html').on('click', _self.on.reset, function (e) {
            e.preventDefault();
            _funResetCtrl();
            _self.DataTable.fnDraw();
        });

        //assign custom expression on search controls
        $(document).on("change", _self.on.expression, function () {
            var key = $(this).data('key');
            if (key) {
                var $ctrl = $('[name="search.' + key + '"]');
                if ($ctrl.length) {
                    $ctrl.data("filter", $(this).val());
                }
            }
        });
        $(_self.on.expression).each(function () {
            $(this).change();
        });

        //Search records in loaded records
        $(document).on('change', ".local-search-grid", function () {
            _self.searchGrid($(this).val());
        });

        //toggle filter
        $(document).on('click', _self.on.togglefilter, function () {
            if ($(_self.selector.dvfilterBox).length == 0) {
                return
            }
            // $(this).toggleClass("btn-default").toggleClass("btn-primary");
            if (typeof initSelect2 !== 'undefined' && $.isFunction(initSelect2)) {
                initSelect2()
                $(_self.selector.dvfilterBox).find(".select2-search").addClass("hide");
            }

            _fnModal('show');


        });

        //assigning radio
        $(document).on('ifChanged', ':radio[data-table="filters"]', function () {
            var name = $(this).attr("name");
            $('[name="search.' + name + '"]').val($(this).val());
            if ($(document).data("report-filter") != undefined) {
                _self.DataTable.fnDraw();
            }
        });
        $(document).on('change', '[data-table="filters"]select', function () {
            var name = $(this).attr("name");
            $('[name="search.' + name + '"]').val($(this).val());
        });

        $(document).on("click", '[data-closemodal]', function () {
            var modal = $(this).data('closemodal');
            $(modal).find(".modal-body").html('');
            $(modal).modal('hide');
        });

        // append local search ctrl
        if (_self.oSetting.localSearch) {
            var ca = $('.create-action');
            if (ca.length != 0) {
                ca.prepend('<input type="text" class="local-search-grid form-control" placeholder="search in loaded results" />');
            }
        }
    })();
}

function BindGridPartial(ajaxUrl, dtKey, modal, modalTitle) {
    var key = dtKey == undefined ? "xyz" : dtKey;
    if ($(document).data(key) == undefined) {
        $(document).data(key, "value");
        if (modalTitle != undefined) {
            $(modal).find(".box-title").html(modalTitle);
        }

        $(modal).find('.modal-body').html(spinnerLoading);
        $(modal).modal({ keyboard: true }).show();
        $.ajax({
            url: ajaxUrl, success: function (html) {
                if ($(modal).length > 0) {
                    $(modal).find('.modal-body').html(html);
                }
                $(document).removeData(key);
            }
        });
    }
}

function getBoolenListItem() {
    var items = new Array();
    item.push(new selectListItem("Yes", "true"));
    item.push(new selectListItem("No", "true"));
    item.push(new selectListItem("Both", "", true));
    return items;
}

function appendToggleCheckBox(url, checked, checkedMessage, unCheckedMesage, oSettings) {
    var json = JSON.stringify($.extend(oSettings || {}, {
        ajaxUrl: url,
        status: checked,
        checkedMessage: checkedMessage || 'Status Successfully Changed',
        unCheckedMesage: unCheckedMesage || 'Status Successfully Changed'
    }));

    return "<input type='checkbox' data-toggle='toggle'" + (checked ? 'checked' : '') + " data-toggle-options='" + json + "' />";
}

function changeActiveStatus() {    
    $(document).on("change", ':checkbox[data-toggle="toggle"]', function (e) {   
        e.stopImmediatePropagation();
        var oSettings = $(this).data('toggle-options') || {};
        if (oSettings.ajaxUrl) {
            $.ajax({
                type: "POST",
                url: oSettings.ajaxUrl,
                dataType: 'JSON',
                success: function (json) {
                     
                    if (json.Status) {
                        this.status = !this.status;
                        $.bootstrapGrowl(
                            (this.status ? this.checkedMessage : this.unCheckedMesage), {
                                type: 'success',
                                delay: 2000,
                            });
                    }
                }.bind(oSettings),
                error: function (xhr, textStatus, errorThrown) {

                }
            });
        }
    });
}