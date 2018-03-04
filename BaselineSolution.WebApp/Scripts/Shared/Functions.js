function isDefined(value) {
    return typeof value != 'undefined';
}


function isLocalStorageSupported() {
    return isDefined(localStorage) && isDefined(localStorage.getItem) && isDefined(localStorage.setItem);
}

function getDefaultRemoteDatatableSettings($datatable) {
    /*-------------------------------
        getDefaultRemoteDatatableSettings (Alex)
    ---------------------------------
    
        parameters:
            datatable
        summary
            Gets the default remote settings
    
    */
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