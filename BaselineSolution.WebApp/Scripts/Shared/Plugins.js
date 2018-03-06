/*******************************

    PLUGINS 

    Plugins are special functions you can execute
    with jquery selectors. These functions extend the existing jQuery framework.
    Example usage: $('mySelector').myPlugin();

*******************************/

(function ($) {
    $.fn.collapsible = function (options) {
        /*-------------------------------
            collapsible (Alex)
        ---------------------------------
    
        Plugin that enables recursive subcontent to be collapsible. (such as lists inside lists)
    
        Options:
        - autoCollapse (bool)
            default :  
                false
            summary :
                Collapses the tree when configuration is done
        - classOpened (string)
            default : 
                "opened"
            summary :
                Class that is added to nodes that are opened
    
        - classCollapsed (string)
            default : 
                "collapsed"
            summary :
                Class that is added to nodes that are collapsed
    
        - classNode (string)
            default : 
                "node"
            summary :
                Class that is used to mark components that contain subcontent
    
        - classLeaf (string)
            default : 
                "leaf"
            summary :
                Class that is used to mark components that contain subcontent
    
        - classNodeTitle (string)
            default : 
                "node-title"
            summary : 
                Class that is used to discover node-titles. 
                Node titles that are clicked will open their proper subcontent
    
        - classNodeContent (string)
            default : 
                "node-content"
            summary : 
                Class that is used to discover node-content.
                Node-content will be opened or collapsed when the corresponding node title is clicked
    
        - classIconNodeCollapsed (string)
            default : 
                "icon-plus-sign"
            summary : 
                When a node is collapsed, this icon is added to the node title.
    
        - classIconNodeOpened (string)
            default : 
                "icon-minus-sign"
            summary : 
                When a node is opened, this icon is added to the node title.
    
        - classIconLeaf (string)
            default : 
                "icon-leaf"
            summary : 
                Icon that is automatically added to all elements which don't contain node-content
    
        - selectorToggleContent ([selector])
            default : 
                null
            summary : 
                Selector to filter the node-title content. Only items matching the selector
                will trigger the open/collapse events
    
        - toggleAnimation (object)
            default :
                {
                    duration: 300,
                    easing: 'swing'
                }
            summary : 
                Options for the animation that should be used when opening or collapsing nodes.
                Note that these are simply passed along to the corresponding jQuery show() and hide() methods.
    
        */
        // variables

        var settings = $.extend({
            autoCollapse: false,
            classOpened: "opened",
            classCollapsed: "collapsed",
            classNode: "node",
            classLeaf: "leaf",
            classNodeTitle: "node-title",
            classNodeContent: "node-content",
            classIconNodeCollapsed: "fa fa-plus-circle",
            classIconNodeOpened: "fa fa-minus-circle",
            classIconLeaf: "fa fa-leaf",
            selectorToggleContent: null,
            toggleAnimation: {
                duration: 300,
                easing: 'swing'
            }
        }, options);


        var $this = $(this);
        var nodes = $this.find('li').has('.' + settings.classNodeContent);
        var leafs = $this.find('li').not(nodes);

        // onload 

        if (settings.autoCollapse)
            $(this).hide();

        nodes.each(function () {
            if (!$(this).hasClass(settings.classNode))
                $(this).addClass(settings.classNode);
            if (settings.autoCollapse)
                collapse($(this));
            else
                open($(this));
        });

        if (settings.autoCollapse)
            $(this).show();

        leafs.each(function () {
            if (!$(this).hasClass(settings.classLeaf))
                $(this).addClass(settings.classLeaf);
            setIcon($(this), settings.classIconLeaf);
        });

        // listeners

        var triggers = $('.' + settings.classNodeTitle);
        if (settings.selectorToggleContent)
            triggers = triggers.children(settings.selectorToggleContent);
        triggers.click(nodeClickHandler);

        // functions

        function nodeClickHandler() {
            toggle($(this).parents('li').first());
        }

        function isCollapsed(node) {
            return getContent(node).is(':hidden');
        }

        function toggle(node) {
            if (isCollapsed(node))
                open(node);
            else
                collapse(node);
        }

        function open(node) {
            setIcon(node, settings.classIconNodeOpened).click(nodeClickHandler);
            var content = getContent(node);
            content.show({
                duration: settings.toggleAnimation.duration,
                easing: settings.toggleAnimation.easing,
                complete: function () {
                    node.removeClass(settings.classCollapsed);
                    node.addClass(settings.classOpened);
                }
            });
        }

        function collapse(node) {
            setIcon(node, settings.classIconNodeCollapsed).click(nodeClickHandler);
            var content = getContent(node);
            content.hide({
                duration: settings.toggleAnimation.duration,
                easing: settings.toggleAnimation.easing,
                complete: function () {
                    node.removeClass(settings.classOpened);
                    node.addClass(settings.classCollapsed);
                }
            });
        }

        function getContent(node) {
            return $(node).children('.' + settings.classNodeContent);
        }

        function setIcon(elem, iconClass) {
            elem.children("i").first().remove();
            var icon = makeIcon(iconClass);
            elem.prepend(icon);
            return icon;
        }

        function makeIcon(iconClass) {
            var icon = $(document.createElement('i'));
            icon.addClass(iconClass);
            return icon;
        }
    };
})(jQuery);

(function ($) {
    $.fn.enableModal = function (options) {
        /*-------------------------------
            enableModal (Alex)
        ---------------------------------

        Plugin that binds a button to load a modal dialog

        Options:

        - onModalLoadFunction (function)
            default : 
                function() { }
                
            parameters:
                - result :
                    the response from the server
            
            summary: 
                Function that is executed after the dialog is loaded. Useful for binding forms.

        */

        // variables

        var settings = $.extend({
            onModalLoadFunction: function () { }
        }, options);

        var $this = $(this);

        // onload 

        var body = $('body');
        var modalDiv = $(document.createElement('div'));
        modalDiv.addClass('modal hide');

        body.append(modalDiv);
        $this.click(function (event) {
            event.preventDefault();
            var url = $(this).attr('href');
            $.ajax({
                url: url,
                type: 'GET',

                success: function (response) {
                    modalDiv.html(response);
                    modalDiv.modal();
                    settings.onModalLoadFunction(response);
                }
            });
        });
    };
})(jQuery);

(function ($) {
    $.fn.enableDatepicker = function (options) {

        /*-------------------------------
            enableDatepicker (Alex)
        ---------------------------------

        Plugin that configures a datepicker on an input element

        parameters:
            - Options
                Options for the datepicker. See http://api.jqueryui.com/datepicker/ for a comprehensive guide on the datepicker options
        summary
            Enables a datepicker for the element with the provided options
            Note that this only applies to elements that are currently loaded in the DOM
        */
        var $this = $(this);
        if (!$this.hasClass('hasDatepicker')) {
            $this.datepicker(options);
        }
        $this.on('click focus', function () {
            $this.datepicker("show");
        });
        return $this;
    };
})(jQuery);

(function ($) {
    $.fn.loading = function (imageUrl) {

        /*-------------------------------
            loading (Alex)
        ---------------------------------
        
            parameters:
                - imageUrl
                    Link to an image
            summary
                Makes an image component for the imageUrl and sets it as the html content
                for the current element.
        
        Makes an image component for the imageUrl and sets it as the html content
        for the current element.
        
        */
        if (!isDefined(imageUrl))
            throw "imageUrl is null! Can't show image.";
        var img = $(document.createElement('img'));
        img.attr('src', imageUrl);
        $(this).html(img);
        return $(this);
    };
})(jQuery);

(function ($) {
    $.fn.setNumericValue = function (value, precision) {

        /*-------------------------------
            setNumericValue (Alex)
        ---------------------------------
        
            parameters:
                - value (string)
                    The numeric value to set as the value of this input element

                
                - [optional] precision (int) 
                    The precision that needs to be used to set the value
            summary
                Sets a numeric value in an input field by calling toString and replacing any possible decimal points by commas
        
        Sets the value of an input with the parsed numeric value
        
        */
        if (!isDefined(value))
            throw "Value is not defined, cannot set numeric value " + value;

        if (!$.isNumeric(value)) {
            $(this).val(0);
            return;
        }

        if (precision != null) {
            value = parseFloat(value).toFixed(precision);
        } else {
            value = value.toString();
        }

        value = value.replace('.', ',');

        $(this).val(value);
    };
})(jQuery);

(function ($) {
    $.fn.addLoadingAnimation = function () {
        /*-------------------------------
            addLoadingAnimation (Alex)
        ---------------------------------
        
            parameters:
                none
            summary
                Adds a loading image to $(this)
        
        
        */
        var imgContainer = $(document.createElement('div'))
            .addClass('ajax-loading-small');
        var img = $(document.createElement('img'))
            .attr('src', '/Images/Shared/ajax-loading-small.gif');
        imgContainer.html(img);
        $(this).html(imgContainer);
        return $(this);
    };
})(jQuery);

(function ($) {
    $.fn.removeLoadingAnimation = function () {
        /*-------------------------------
            removeLoadingAnimation (Alex)
        ---------------------------------
        
            parameters:
                none
            summary
                Removes the loading animation of $(this), if any.
        
        
        */
        $(this).children('.ajax-loading-small').remove();
        return $(this);
    };
})(jQuery);

(function ($) {
    /*-------------------------------
        onFinishedTyping (Alex)
    ---------------------------------

        parameters:
            - action 
                Function that is called when the user stops typing 

        summary
            Calls a function after the user stops typing. This can be useful in scenarios where the function is expensive
            and you want to avoid the use of keyup or keydown


    */
    $.fn.onFinishedTyping = function (action) {
        var timeOut = 150;
        var typingTimer;
        $(this).keyup(function () {
            clearTimeout(typingTimer);
            var $thisInput = $(this);
            typingTimer = setTimeout(function () { action.call($thisInput); }, timeOut);
        });
    };
})(jQuery);

(function($) {
    $.fn.scrollTo = function (target, options, callback) {
        if (typeof options == 'function' && arguments.length == 2) { callback = options; options = target; }
        var settings = $.extend({
            scrollTarget: target,
            offsetTop: 50,
            duration: 500,
            easing: 'swing'
        }, options);
        return this.each(function () {
            var scrollPane = $(this);
            var scrollTarget = (typeof settings.scrollTarget == "number") ? settings.scrollTarget : $(settings.scrollTarget);
            var scrollY = (typeof scrollTarget == "number") ? scrollTarget : scrollTarget.offset().top + scrollPane.scrollTop() - parseInt(settings.offsetTop);
            scrollPane.animate({ scrollTop: scrollY }, parseInt(settings.duration), settings.easing, function () {
                if (typeof callback == 'function') { callback.call(this); }
            });
        });
    }
})(jQuery)