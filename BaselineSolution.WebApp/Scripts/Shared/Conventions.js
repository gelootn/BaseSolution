

// Represents a table with local data which should have the datatables plugin initialized on it
var datatableLocalConvention = "enable-local-datatable";

// Represents a table with remote data which should have the datatables plugin initialized on it
var datatableRemoteConvention = "enable-remote-datatable";


$(function() {
    initConventions();
    $(document).ajaxComplete(initConventions);

    function initConventions() {
        initLocalDatatableConvention();
        initRemoteDatatableConvention();
    }

    function initLocalDatatableConvention() {
        if (!isDefined($.fn.dataTable))
            return;

        $("table." + datatableLocalConvention).each(function () {
            var $this = $(this);

            // no need to do anything if this is already a datatable
            if ($this.hasClass('dataTable'))
                return;

            var localDatatable;
            if ($this.attr('data-custom-initialize') != 'true')
                localDatatable = $this.dataTable(getDefaultLocalDatatableSettings($this));

            var $filters = $this.find('thead tr.datatable-column-filters');
            $filters.on('change keyup', "input", search);
            $filters.on('change', "select", search);

            var rowClickHandlerColumnName = $this.data('row-click-handler-column-name');
            if (rowClickHandlerColumnName != null) {
                $this.on('click', 'tbody tr', function (event) {
                    var target = isDefined(event.target) ? event.target : event.srcElement;
                    if (target != null && isDefined(target.tagName) && target.tagName != null && target.tagName.toLowerCase() != "td") { return; }
                    if (!localDatatable)
                        localDatatable = $this.dataTable();
                    var data = localDatatable.fnGetData(this);
                    if (data == null)
                        return;
                    var url = data[rowClickHandlerColumnName];
                    window.location = url;
                });
            }

            function search() {
                if (!localDatatable)
                    localDatatable = $this.dataTable();
                localDatatable.fnFilter($(this).val(), $filters.find("td").index($(this).parents('td').first()));
            }
        });
    }

    function initRemoteDatatableConvention() {
        if (!isDefined($.fn.dataTable))
            return;

        $("table." + datatableRemoteConvention).each(function () {
            var $this = $(this);

            // no need to do anything if this is already a datatable
            if ($this.hasClass('dataTable'))
                return;

            var remoteDatatable;
            if ($this.attr('data-custom-initialize') != 'true')
                remoteDatatable = $this.dataTable(getDefaultRemoteDatatableSettings($this));

            var $filters = $this.find('thead tr.datatable-column-filters');
            $filters.on('change keyup', "input", search);
            $filters.on('change', "select", search);

            var rowClickHandlerColumnName = $this.data('row-click-handler-column-name');
            var rowCssColumnName = $this.data('row-css-class-column-name');
            if (rowClickHandlerColumnName != null) {
                $this.on('click', 'tbody tr', function (event) {
                    var target = isDefined(event.target) ? event.target : event.srcElement;
                    var $target = $(target);

                    if (target != null && isDefined(target.tagName) && target.tagName != null && target.tagName.toLowerCase() != "td" && !$target.hasClass("cellrow")) { return; }
                    if (!remoteDatatable)
                        remoteDatatable = $this.dataTable();
                    var data = remoteDatatable.fnGetData(this);
                    if (data == null)
                        return;
                    var url = data[rowClickHandlerColumnName];
                    window.location = url;
                });
            }
            if (rowCssColumnName != null) {
                if (!remoteDatatable)
                    remoteDatatable = $this.dataTable();
                remoteDatatable.on('draw.dt', function (event, settings) {
                    if (!remoteDatatable)
                        remoteDatatable = $this.dataTable();
                    console.log('Redraw occurred at: ' + new Date().getTime());
                });
            }
            function search() {
                if (!remoteDatatable)
                    remoteDatatable = $this.dataTable();
                remoteDatatable.fnFilter($(this).val(), $filters.find("td").index($(this).parents('td').first()));
            }
        });
    }
});