$(function() {
    // variables

    var contentRights = $("#content-rights");
    var rightsList = $("#rights-list");
    var rightsUrl = contentRights.attr('data-url');

    var btnGenerateRights = $("#btn-generate-rights");
    var generateRightsUrl = btnGenerateRights.attr('href');

    var btnSaveRights = $("#btn-save-rights");

    var collapseRights = $("#collapse-rights");
    var expandRights = $("#expand-rights");
    

    // onload

    startLoadingAnimation();
    
    $.getJSON(rightsUrl, {}, function (result) {
        stopLoadingAnimation();
        loadRights(result);
        rightsList.collapsible({
            selectorToggleContent: '.node-title-text',
            autoCollapse: true
        });
    });

    collapseRights.hide();
    
    // listeners

    btnGenerateRights.click(function(event) {
        event.preventDefault();
        rightsList.html('');
        startLoadingAnimation();
        $.getJSON(generateRightsUrl, function (result) {
            stopLoadingAnimation();
            loadRights(result);
            rightsList.collapsible({ autoCollapse: true });
        });
    });

    btnSaveRights.click(function(event) {
        event.preventDefault();
        var rights = new Array();
        var nodes = rightsList.children();
        for (var i = 0; i < nodes.length; i++) {
            rights[i] = getRight($(nodes[i]));
        }

        rightsList.html('');
        startLoadingAnimation();

        $.ajax({
            type: 'POST',
            url: btnSaveRights.attr('href'),
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(rights),
            success: function(result) {
                if (result.success) {
                    $.getJSON(rightsUrl, {}, function (response) {
                        stopLoadingAnimation();
                        loadRights(response);
                        rightsList.collapsible({ autoCollapse: true });
                    });
                }
            }
        });
    });

    rightsList.on('click', '.btn-right-delete', function (event) {
        event.preventDefault();
        var $this = $(this);
        var parent = $this.parents('li').first();
        parent.hide({
            duration: 300,
            easing: 'swing',
            complete: function() {
                parent.remove();
            }
        });
    });

    collapseRights.click(function() {
        $('.opened').children('.node-title').children('.node-title-text').trigger('click');
        collapseRights.hide();
        expandRights.show();
    });
    
    expandRights.click(function () {
        $('.collapsed').children('.node-title').children('.node-title-text').trigger('click');
        expandRights.hide();
        collapseRights.show();
    });

    // functions

    function loadRights(rights) {
        rightsList.html('');
        for (var i = 0; i < rights.length; i++) {
            rightsList.append(parseRight(rights[i]));
        }
    }
    
    function parseRight(right) {
        var li = $(document.createElement('li'));
        var title = $(document.createElement('p'));
        var titleText = $(document.createElement('span'));
        titleText.html(right.Key);
        titleText.attr('data-right-id', right.Id);
        titleText.attr('data-right-parentid', right.ParentId);
        titleText.addClass('node-title-text');
        title.html(titleText);
        li.html(title);
        title.append(makeEditButton(right));
        title.append(makeDeleteButton(right));
        if (!isDefined(right.Children)) {
            title.addClass('leaf-title');
            return li;
        } else {
            title.addClass('node-title');
            var ul = $(document.createElement('ul'));
            ul.addClass('node-content');
            for (var i = 0; i < right.Children.length; i++) {
                ul.append(parseRight(right.Children[i]));
            }
            li.append(ul);
            return li;
        }
    }
    
    function getRight(node) {
        var title = node.find('.node-title-text').first();
        var id = title.attr('data-right-id');
        var parentId = title.attr('data-right-parentid');
        var key = title.text();
        if (node.hasClass('leaf')) {
            return {
                Id: id,
                Key: key,
                ParentId : parentId
            };
        } else if (node.hasClass('node')) {
            var children = new Array();
            var subnodes = node.children("ul.node-content").first().children();
            for (var i = 0; i < subnodes.length; i++) {
                children[i] = getRight($(subnodes[i]));
            }
            return {
                Id: id,
                Key: key,
                ParentId : parentId,
                Children: children
            };
        }
        throw "This is not a node or a leaf!";
    }
    
    function makeDeleteButton(right) {
        var a = $(document.createElement('a'));
        a.attr('href', contentRights.attr('data-url-delete'));
        a.attr('data-right-id', right.Id);
        a.addClass('btn btn-danger btn-sm btn-right-delete no-border');
        var i = $(document.createElement('i'));
        i.addClass('fa fa-times');
        a.html(i);
        return a;
    }
    
    function makeEditButton(right) {
        var a = $(document.createElement('a'));
        a.attr('href', contentRights.attr('data-url-edit') + '/' + right.Id);
        a.addClass('btn btn-primary btn-sm no-border');
        var i = $(document.createElement('i'));
        i.addClass('fa fa-edit');
        a.html(i);
        return a;
    }


});