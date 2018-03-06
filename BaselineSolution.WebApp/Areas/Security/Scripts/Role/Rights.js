$(function () {
    // variables

    var labelSuccess = "badge-success";
    var labelImportant = "badge-danger";

    var rightsList = $("#rights-list");
    var saveUrl = rightsList.attr('data-url');
    var classActive = "active";

    var collapseRights = $("#collapse-rights");
    var expandRights = $("#expand-rights");

    // onload

    rightsList.collapsible({ autoCollapse: true });
    collapseRights.hide();


    // listeners

    $(".role-right-editor").change(function () {
        var allow = $(this).val();
        var rightId = $(this).attr('data-right-id');
        var roleId = $(this).attr('data-role-id');

        $.ajax({
            type: 'POST',
            url: saveUrl,
            data: {
                rightId: rightId,
                roleId: roleId,
                allow: allow
            }
        });

        var title = $(this).next();
        title.removeClass(labelImportant).removeClass(labelSuccess);
        if (allow == 'True') {
            title.addClass(labelSuccess);
        } else if (allow == 'False') {
            title.addClass(labelImportant);
        }

    });

    collapseRights.click(function () {
        $('.opened').children('.node-title').trigger('click');
        collapseRights.hide();
        expandRights.show();
    });

    expandRights.click(function () {
        $('.collapsed').children('.node-title').trigger('click');
        expandRights.hide();
        collapseRights.show();
    });

});