function isDefined(value) {
    return typeof value != 'undefined';
}


function isLocalStorageSupported() {
    return isDefined(localStorage) && isDefined(localStorage.getItem) && isDefined(localStorage.setItem);
}

function startLoadingAnimation() {
    var loadingDiv = $(document.createElement('div')).addClass('ajax-loading').text("Loading ...");
    $('body').append(loadingDiv);
}

function stopLoadingAnimation() {
    $('body').children('.ajax-loading').remove();
}

function getDefaultRemoteDatatableSettings($datatable) {
    var $columnHeaders = $datatable.find('thead tr.datatable-column-headers th');
    var dataProperties = _.map($columnHeaders, getDatatableHeaderColumnData);
    var datatableId = $datatable.prop('id');
    var showPagination = $datatable.data('show-pagination');
    return {
        "aoColumns": dataProperties,
        "bAutoWidth": false,
        "bProcessing": true,
        "bStateSave": true,
        "bPaginate": true,
        "iCookieDuration": 3600,
        "bServerSide": true,
        "fnInitComplete": function() {
            // load search filters from state into input fields
            var oSettings = $datatable.dataTable().fnSettings();
            for (var i = 0; i < oSettings.aoPreSearchCols.length; i++) {
                if (oSettings.aoPreSearchCols[i].sSearch.length > 0) {
                    var cachedValue = oSettings.aoPreSearchCols[i].sSearch;
                    var $propertyColumn = $($("thead tr.datatable-column-filters td")[i]);
                    $propertyColumn.find("input.datatable-column-filter").val(cachedValue);
                    $propertyColumn.find("select.datatable-column-filter").select2("val", cachedValue);
                }
            }
        },
        "fnServerData": function(sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": aoData,
                "success": fnCallback
            });
        },
        "fnServerParams": function(aoData) {
            aoData.push({ name: "sDatatableId", value: datatableId });
        },
        "fnStateSave": function(oSettings, oData) {
            setDatatableState(datatableId, oData);
        },
        "fnStateLoad": function(oSettings) {
            return getDatatableState(datatableId);
        },
        "sAjaxSource": $datatable.attr('data-url')
    };

    function getDefaultLocalDatatableSettings($datatable) {

        var $columnHeaders = $datatable.find('thead tr.datatable-column-headers th');
        var dataProperties = _.map($columnHeaders, getDatatableHeaderColumnData);
        var showPagination = $datatable.data('show-pagination');
        return {
            "aoColumns": dataProperties,
            "aaSorting": [],
            "bAutoWidth": false,
            "bPaginate": showPagination,
            "bProcessing": false,
            "bStateSave": false,
            "iCookieDuration": 3600,
            "bServerSide": false,
            "fnInitComplete": function() {
                var oSettings = $datatable.dataTable().fnSettings();
                for (var i = 0; i < oSettings.aoPreSearchCols.length; i++) {
                    if (oSettings.aoPreSearchCols[i].sSearch.length > 0) {
                        var cachedValue = oSettings.aoPreSearchCols[i].sSearch;
                        var $propertyColumn = $($("thead tr.datatable-column-filters td")[i]);
                        $propertyColumn.find("input.datatable-column-filter").val(cachedValue);
                        $propertyColumn.find("select.datatable-column-filter").select2("val", cachedValue);
                    }
                }
            },
        };
    }

    function getDatatableState(datatableId) {
        var storageKey = 'DatatableStorage_' + datatableId;
        var stringified = isLocalStorageSupported()
            ? localStorage.getItem(storageKey)
            : $.cookie(storageKey);
        var oData = JSON.parse(stringified || null);
        return oData;
    }

    function setDatatableState(datatableId, oData) {
        var storageKey = 'DatatableStorage_' + datatableId;
        var stringified = JSON.stringify(oData);
        if (isLocalStorageSupported())
            localStorage.setItem(storageKey, stringified);
        else
            $.cookie(storageKey, stringified);
    }

    function getDatatableHeaderColumnData(datatableHeader) {
        var $datatableHeader = $(datatableHeader);
        var property = $datatableHeader.data('property');
        var sSearchable = $datatableHeader.data('searchable');
        var bSortable = $datatableHeader.data('sortable');
        var sWidth = $datatableHeader.data('width');
        var bVisible = $datatableHeader.data('visible');
        var sClass = $datatableHeader.data('class');
        var sDefaultContent = $datatableHeader.data('default-content');

        var columnData = {
            mDataProp: property
        };

        if (isDefined(sWidth) && sWidth != null && sWidth.length > 0)
            columnData.sWidth = sWidth;

        if (isDefined(sSearchable) && sSearchable != null)
            columnData.bSearchable = sSearchable;

        if (isDefined(bSortable) && bSortable != null)
            columnData.bSortable = bSortable;

        if (isDefined(bVisible) && bVisible != null)
            columnData.bVisible = bVisible;

        if (isDefined(sClass) && sClass != null && sClass.length > 0)
            columnData.sClass = sClass;

        if (isDefined(sDefaultContent) && sDefaultContent != null && sDefaultContent.length > 0)
            columnData.sDefaultContent = sDefaultContent;

        return columnData;
    }
}

function getDefaultRemoteMultiSelectSettings($input) {
    var optionsUrl = $input.data('options-url');
    var settings = getDefaultRemoteSelectSettings($input);
    settings.multiple = true;
    settings.initSelection = function (element, callback) {
        var $element = $(element);
        var id = $element.val();
        var ids = id.split(',');
        var data = {};
        for (var i = 0; i < ids.length; i++) {
            data["ids[" + i + "]"] = ids[i];
        }
        data.page = 0;
        data.pageSize = 1;
        if (id !== "") {
            $.ajax({
                url: optionsUrl,
                type: 'GET',
                data: data
            }).done(function (response) {
                callback(response.results);
            });
        }
    };
    return settings;
}

function getDefaultRemoteSelectSettings($input) {
    var optionsUrl = $input.data('options-url');
    var placeholder = $input.data('placeholder');
    var type = $input.data('type');
    var format = function (state) {
        if (isDefined( state.results)) return "" + state.results[0].text;
        return "" + state.text;
    };
    return {
        placeholder: placeholder,
        minimumInputLength: 0,
        width: '100%',
        allowClear: true,
        selectOnBlur: true,
        formatResult: format,
        formatSelection: format,
        ajax: {
            url: optionsUrl,
            dataType:
            'json',
            quietMillis:
            10,
            // data is a transformation function that returns an object containing the proper url parameters
            data: function (term, page) {
                // page is the one-based page number tracked by Select2
                return {
                    term: term, //search term
                    pageSize: 10, // page size
                    page: page - 1, // page number
                    type: type // (optional) for ISelectable selectlists
                };
            },
            results: function (data, page) {
                // notice we return the value of more so Select2 knows if more results can be loaded
                return { results: data.results, more: data.more, page: page };
            }
        },
        escapeMarkup: function (m) {
            return m;
        },
        initSelection: function (element, callback) {
            var $element = $(element);
            var id = $element.val();
            if (id !== "" && id != 0) {
                $.ajax({
                    url: optionsUrl,
                    type: 'GET',
                    data: {
                        id: id,
                        type: type,
                        page: 0,
                        pageSize: 1
                    }
                }).done(function (data) {
                    if (isDefined(data.results) && data.results.length == 0)
                        return;
                    callback(data);
                });
            }
        }
    };
}