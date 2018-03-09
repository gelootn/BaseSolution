$(function() {

    var defaultDatepickerInputType = "datetime";


    initDefaults();
    $(document).ajaxComplete(initDefaults());


    function initDefaults() {
        initDefaultSelectTransformation();
        initDefaultDatepickerInputType();
        initFormating();
    }

    function initDefaultSelectTransformation() {
        $("select").not(".custom-initialize")
            .not('.select2-offscreen')
            .not('.ui-datepicker-month')
            .not('.ui-datepicker-year')
            .each(function () {
                var $this = $(this);
                /* Disabled: placeholder niet altijd juist ingeladen bij edit
                var firstOption = $(this).children('option').first();
                var placeholder = firstOption.text();*/
                var placeholder = $this.data("placeholder");
                var width = $this.data("width") || "100%";
                $this.select2({
                    placeholder: placeholder,
                    minimumResultsForSearch: 10,
                    width: width,
                    allowClear: true,
                    selectOnBlur: true
                });
            });
    }
    function initDefaultDatepickerInputType() {
        /*if (!isDefined($.datepicker))
            return;*/

        $("input[type=" + defaultDatepickerInputType + "]")
            .not('.hasDatePicker')
            .not('.custom-initialize')
            .each(function () {
                $(this).datepicker({
                    format: "dd/mm/yyyy",
                    weekStart: 1,
                    todayBtn: "linked",
                    language: "nl-BE",
                    daysOfWeekDisabled: "0,6",
                    calendarWeeks: true,
                    todayHighlight: true,
                    onSelect: function () {
                        $(this).trigger('change');
                    }
                });
            });
    }
    function initFormating() {
        $("input[type=search]").addClass("form-control");

    }
    function initDefaultDatatableSettings() {
        if (!isDefined($.fn.dataTable))
            return;

        $.extend(true, $.fn.dataTable.defaults, {
            "sDom": '<"table-container"<t><"table-bottom"lipr>>',
            "bAutoWidth": "false",
            "oLanguage": {
                "sProcessing": "<div class='loadingText'><img src='../../Images/Shared/ajax-loading-mini.gif' /></div>",
                "sLengthMenu": "_MENU_",
                "sZeroRecords": "Geen resultaten gevonden",
                "sInfo": "_START_ tot _END_ van _TOTAL_ resultaten",
                "sInfoEmpty": "Geen resultaten om weer te geven",
                "sInfoFiltered": " (gefilterd uit _MAX_ resultaten)",
                "sInfoPostFix": "",
                "sSearch": "Zoeken:",
                "sEmptyTable": "Geen resultaten gevonden",
                "sInfoThousands": ".",
                "sLoadingRecords": "Een moment geduld aub - bezig met laden...",
                "oPaginate": {
                    "sFirst": "",
                    "sLast": "",
                    "sNext": "",
                    "sPrevious": ""
                }
            }
        });

    }
});