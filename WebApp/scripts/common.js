/**
* @author Roy
*/
//http://phrogz.net/JS/Classes/OOPinJS2.html

/**
* Enables the Object inheritance
* @param {Object} parentClassOrObject
*/
Function.prototype.inherits = function(classOrObject) {
    if (classOrObject.constructor == Function) {
        var instance = new classOrObject;
        var instancePrototype = instance.constructor.prototype;
        $.extend(instancePrototype, instance);

        this.prototype = instance;
        this.prototype.constructor = this;

        this.prototype.parent = instancePrototype;
        this.prototype.parent.constructor = classOrObject;
    }
    else {
        this.prototype = classOrObject;
        this.prototype.constructor = this;
        this.prototype.parent = classOrObject;
    }
    return this;
};

jQuery.extend(
    {
        ellipsis: function(str, opts) {
            var options =
                {
                    ellipsisText: "...",
                    max: 150,
                    truncateWords: true
                };

            $.extend(options, opts);

            //TODO: check that options.max > options.ellipsisText
            if ((str.length <= options.max) || (options.max - options.ellipsisText.length < 0)) {
                return str;
            }

            str = str.substring(0, options.max - options.ellipsisText.length) + options.ellipsisText;

            return str;
        },
		roundNumber : function (num, dec) {
	        var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	        return result;
        },
        onDomReady: function(fn) {
            if (typeof (Sys) != 'undefined') {
                Sys.Application.add_load(fn);
                return;
            }
            $(document).ready(fn);
        },

        arrayIndexOf: function(array, element, options) {
            if (!$.isArray(array)) {
                return -1;
            }
            var opts =
                {
                    fEquals: null,
                    startIndex: 0
                };

            $.extend(opts, options);

            if (!$.isFunction(opts.fEquals)) {
                if (Array.prototype.indexOf) {
                    return array.indexOf(element, opts.startIndex);
                }
            }
            var comparer = opts.fEquals ||
            function(eleA, eleB) {
                return (eleB === eleA);
            };
            var ini = opts.startIndex;
            if (ini < 0) {
                ini = array.length + ini;
            }
            for (var ix = ini; ix < array.length; ix++) {
                if (comparer(array[ix], element)) {
                    return ix;
                }
            }
            return -1;
        },
        arrayMergeUnique: function(arrayA, arrayB, fEquals) {
            var tempArrA, tempArrB, returnArr;
            if ($.isArray(arrayA) && $.isArray(arrayB)) {
                var isALonger = (arrayA.length >= arrayB.length);
                tempArrA = isALonger ? arrayA : arrayB;
                tempArrB = isALonger ? arrayB : arrayA;

                returnArr = $.map(tempArrA, function(val, i) {
                    return val;
                });

                $.each(tempArrB, function(i, val) {
                    if (!$.arrayContains(tempArrA, val, fEquals)) {
                        returnArr.push(val);
                    }
                });
                return returnArr;
            }
            return null;
        },

        arrayRemove: function(array, element, fEquals) {
            var index = $.arrayIndexOf(array, element,
                {
                    "fEquals": fEquals
                });
            if (index > -1) {
                array.splice(index, 1);
            }
        },
        arrayContains: function(array, element, fEquals) {
            return $.arrayIndexOf(array, element, {
                "fEquals": fEquals
            }) >
            -1;
        },
        arrayUnique: function(array, fEquals) {
            var unique = [];
            $.each(array, function(i, ele) {
                if (!$.arrayContains(unique, ele, fEquals)) {
                    unique.push(ele);
                }
            });
            return unique;
        },

        arrayFilter: function(array, testFunction) {
            if (!$.isFunction(testFunction)) {
                return array;
            }
            if (Array.prototype.filter) {
                return array.filter(testFunction);
            }
            var arr = [];
            $.each(array, function(i, val) {
                if (testFunction(val, i)) {
                    arr.push(val);
                }
            });
            return arr;
        },

        arrayFind: function(array, testFunction) {
            var value = null;
            if ($.isFunction(testFunction)) {
                $.each(array, function(i, val) {
                    if (testFunction(val)) {
                        value = val;
                        return false;
                    }
                });
            }

            return value;

        },

        getJsonResponse: function(url, onSuccess, onError, timeout, method, data) {

            var requestSuccess = false;

            var opts =
                {
                    url: url,
                    dataType: "jsonp",
                    "data": data,
                    success: function(dt) {
                        requestSuccess = true;
                        if (typeof onSuccess == 'function') {
                            try {
                                onSuccess(dt);
                            }
                            catch (ex) {
                                console.log(ex);
                            }
                        }
                    },
                    error: onError,
                    type: method || "GET"
                };

            if (timeout) {
                opts.timeout = timeout;
            }

            var interval = null;

            var raiseTimeout = function() {
                window.clearTimeout(interval);
                if (typeof onError == 'function' && !requestSuccess) {
                    onError(null, "timeout", "timeout");
                }
            };

            var req = $.ajax(opts);
            if (req === undefined) {
                //req should be undefined if we are trying to do a cross site call
                interval = window.setTimeout(raiseTimeout, opts.timeout || 20000); //20 seconds or rise the timeout!
                //return interval;				
            }
            return req;
        },
        RegexHelper:
            {
                HtmlTags: new RegExp("<[a-zA-Z\/][^>]*>", "g")
            },

        /**
        * Obtains the Current Time
        */
        getCurrentTime: function() {
            return (new Date()).getTime();
        },

        TimeSpan:
            {
                timeElapsed: null,
                start: function() {
                    $.TimeSpan.timeElapsed = $.getCurrentTime();
                },
                end: function() {
                    return ($.getCurrentTime() - $.TimeSpan.timeElapsed);
                }
            },
        /**
        * Creates a DummyConsole object in case the user doesn't have the firebug plugin or is using IE
        */
        console:
            {
                verboseMode: true
            },
        createDummyConsole: function() {
            try {
                var names = ["debug", "info", "warn", "error", "assert", "dir", "dirxml", "group", "groupEnd", "time", "timeEnd", "count", "trace", "profile", "profileEnd", "log"];
                if (!window.console || !console.firebug) {
                    window.console = {};
                    for (var i = 0; i < names.length; ++i) {
                        window.console[names[i]] = function() {

                        };
                    }
                }

                var oldConsoleLog = window.console.log;

                window.console.log = function() {
                    if ($.console.verboseMode) {
                        oldConsoleLog.apply(window.console, arguments);
                    }
                };
            }
            catch (ex) {

            }

        },
        /**
        * erase the new lines characters from the string
        * @param {Object} str
        */
        stripNewLineCharacters: function(str) {
            return str.replace(/\r|\n|\r\n/g, "");
        },

        createDelegate: function(obj, func) {
            arguments.slice = Array.prototype.slice;
            var f = function() {
                return f.func.apply(f.target, f.args);
            };

            f.args = (arguments.length > 2) ? arguments.slice(2) : [];
            f.target = obj;
            f.func = func;
            return f;
        },
        createDelegateExpanded: function(obj, func) {
            arguments.slice = Array.prototype.slice;
            var f = function() {
                var args = [];
                for (var ix = 0; ix < arguments.length; ix++) {
                    args.push(arguments[ix]);
                }
                args = args.concat(f.args);

                return f.func.apply(f.target, args);
            };

            f.args = (arguments.length > 2) ? arguments.slice(2) : [];
            f.target = obj;
            f.func = func;
            return f;

        },
        /**
        * get an Element by Id only!!!!
        * @param {String} str_id
        */
        byId: function(str_id) {
            return $("#" + str_id);
        },
        /**
        * Create a namespace with the provided string
        * @param {String} str_ns
        */
        Namespace: function(str_ns) {
            var nsParts = str_ns.split(".");
            var root = window;
            for (var i = 0; i < nsParts.length; i++) {
                if (typeof root[nsParts[i]] == "undefined") {
                    root[nsParts[i]] = {};
                }
                root = root[nsParts[i]];
            }
        },
        stringFormat: function(s) {
            for (var i = 1; i < arguments.length; i++) {
                var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
                s = s.replace(re, arguments[i]);
            }
            return s;
        },

        getResourceString: function(resourceKey, defaultMessage) {
            if (typeof (__resources) != 'undefined' && __resources[resourceKey] && __resources[resourceKey] !== "") {
                return __resources[resourceKey];
            }
            else {
                return defaultMessage;
            }
        },

        formatNumber: function(number, deep, decimalSeparator) {
            var numberFixed = number.toFixed(deep);
            return numberFixed.replace(/\./, decimalSeparator);
        },
        formatNumberWithSeparator: function(number, separator) {
            function pad(n, length) {
                n = n.toString();
                while (n.length < length) {
                    n = '0' + n;
                }
                return n;
            }
            if (!separator) separator = ',';
            var formattedNumberString = pad(number % 1000, 3);
            var x = parseInt(number / 1000);
            while (x > 0) {
                formattedNumberString = pad(x % 1000, 3) + separator + formattedNumberString;
                x = parseInt(x / 1000);
            }
            var triples = formattedNumberString.split(separator);
            if (triples.length) {
                triples[0] = new Number(triples[0]);
            }
            return triples.join(separator);
        }


    });

    jQuery.fn.extend(
    {
        alignTo: function(selector, options) {
            var opts = {
                horizontal: 'LEFT', //'LEFT', 'RIGHT'
                vertical: 'BOTTOM', //'BOTTOM', 'TOP',
                offsetHorizontal: 0,
                offsetVertical: 0
            };
            $.extend(opts, options);
            var pos = $(selector).position();

            var h, v;

            if (opts.horizontal == 'LEFT') {
                h = { left: pos.left + opts.offsetHorizontal };
            }

            if (opts.horizontal == 'RIGHT') {
                var excess = 0;
                var cWidth = this.outerWidth();
                var oWidth = $(selector).outerWidth();
                excess = cWidth - oWidth;
                if (excess < 0) {
                    excess = 0;
                }
                h = { left: pos.left - excess + opts.offsetHorizontal };
            }


            if (opts.vertical == 'TOP') {
                v = { top: pos.top + opts.offsetVertical };
            }

            if (opts.vertical == 'BOTTOM') {
                v = { top: pos.top + $(selector).outerHeight() + opts.offsetVertical };
            }

            if (h) this.css(h);
            if (v) this.css(v);
            return this;
        },
        disableTextSelection: function() {
            return this.each(function() {
                var target = this;
                if ($(target).data('disable_text_selection_applied')) return;
                if (typeof target.onselectstart != "undefined") {//IE route 				
                    target.onselectstart = function() { return false };
                }
                else if (typeof target.style.MozUserSelect != "undefined") { //Firefox route
                    target.style.MozUserSelect = "none";
                }
                else { //All other route (ie: Opera)
                    target.onmousedown = function() { return false }
                    target.style.cursor = "default";
                }
                $(target).data('disable_text_selection_applied', true);
            });
        },
        fixTabIndex: function(opts) {
            var options = {
				forceTabIndex : true,
				excludeSelector : null				
            };
            $.extend(options, opts);

            var elements = this;
            $.R3M = $.R3M || {};
            $.R3M.tabIndex = $.R3M.tabIndex || 1;            
            return elements.each(function() {
                var ele = $(this);
				if (options.excludeSelector && ele.is(options.excludeSelector)) return true; //continue
				var autoTab = options.forceTabIndex || !ele.attr('tabindex');								
									
				if (autoTab) {
					ele.attr('tabindex', $.R3M.tabIndex++);
				}
            });
        },		
		checkAllBehavior : function (options) {
			var opts = {
				chkSelector : 'span.Chk input[type=checkbox]',
				chkAllSelector : 'span.ChkAll input[type=checkbox]'
			};
			
			$.extend(opts, options);
			
			return this.each(function() {
                var grid = $(this);
                var childChecks = grid.find(opts.chkSelector);
                grid.find(opts.chkAllSelector).click(function() {
                    var checked = $(this).attr('checked');
                    childChecks.attr('checked', checked ? 'checked' : '');
                });
			});			
		},	
		disableFocus : function () 
		{		
			return this.each(function() {
                if ($(this).attr('disabled_focus') == 'true') return;
                $(this).focus(function() {
                    $(this).blur();
                    return false;
                });
                $(this).attr('disabled_focus', 'true');
            });
		},		
        useWidthFromWidestDescendant: function(selector, options) {
            var opts = {
                excess: 0,
                useOuterWidth: false,
                includeMargin: true,
                forceEvaluateIfHidden: false
            };

            $.extend(opts, options);
            if (selector) {
                var originalMargin = this.css('marginLeft');
                if (opts.forceEvaluateIfHidden && this.is(':hidden')) {
                    this
                        .css('marginLeft', -20000)
                        .show();
                }
                var maxWidth = 0;
                var elements = this
                    .find(selector)
                    .each(function(i, val) {
                        var w = opts.useOuterWidth ? $(this).width() : $(this).outerWidth(opts.includeMargin);
                        if (w > maxWidth) maxWidth = w;
                    });

                if (maxWidth > 0 && maxWidth > $(this).width()) {
                    this.setAbsoluteWidth(maxWidth + opts.excess);
                }

                if (opts.forceEvaluateIfHidden) {
                    this
                        .css('marginLeft', originalMargin)
                        .hide();
                }
            }

            return this;

        },
        fitToChildrenWidth: function(options) {
            var opts = {
                excess: 0,
                useAnimation: true
            };

            $.extend(opts, options);

            return this.each(function() {			
                var ele = $(this);
                if (ele.hasClass('NoForce')) {
                    $(ele).show();
                    return;
                }                
                if (!ele.parent().is(':visible')) return;
                if (ele.data('sizeCalculated') == 'true') return;
                
                var children = ele.children();

                ele.css({ marginLeft: '-20000px' });
                ele.show();
                var widthForEle = 0;

                if (children.is('ul')) {
                    children.find('>li').each(function() {
                        widthForEle += $(this).outerWidth(true);
                    });
                }
                else {
                    widthForEle = children.width();
                }

                ele.css({ marginLeft: 'auto' });
                if (opts.useAnimation) children.hide();

                ele.width(widthForEle + opts.excess);

                if (opts.useAnimation) children.fadeIn();
                
                ele.data('sizeCalculated','true')

            });
        },
        useMaximumWidth: function(hasMinWidth) {
            var maxChoiceWidth = 0;
            var elementsObject = $(this);
            var minWidth = 0;
            if (hasMinWidth == true) {
                minWidth = elementsObject.css('min-width') ? elementsObject.css('min-width').replace(/\D/g, '') : 0;
            }
            $.each(elementsObject, function(i, val) {
                var choiceWidth = $(val).outerWidth(true);
                if (choiceWidth > maxChoiceWidth) {
                    maxChoiceWidth = choiceWidth;
                }
            });
            if (maxChoiceWidth > minWidth)
                elementsObject.width(maxChoiceWidth);
            return this;
        },        

        blockContent: function(options) {
            var opts = {
                opacity: .5,
                bgColor: '#fff'
            };
            $.extend(opts, options);



            return this.each(function(i, val) {
                var ele = $(this);
                if (ele.data('IsBlocked')) return
                var bUI = ele.find('.BlockUI');
                if (bUI.length === 0) {
                    bUI = $('<div class="BlockUI">&nbsp;</div>').appendTo(ele);
                }
                try {
                    bUI.css({
                        'display': 'none',
                        'position': 'absolute',
                        'background-color': opts.bgColor,
                        'z-index': 10000,
                        'opacity': opts.opacity,
                        'top': 0,
                        'left': 0
                    })
                    bUI.setAbsoluteWidth(ele.outerWidth(true));
                    bUI.setAbsoluteHeight(ele.outerHeight(true));
                    bUI.show();

                    var pos = ele.position();
                    bUI.css({ 'left': pos.left, 'top': pos.top });

                    ele.data('IsBlocked', true);
                    $(window)
					    .bind('resize.blockContent', function () {
					        var pos = ele.position();
                            bUI.css({ 'left': pos.left, 'top': pos.top });
					        bUI.setAbsoluteWidth(ele.outerWidth(true));
                            bUI.setAbsoluteHeight(ele.outerHeight(true));   
					     } );
                }
                catch (e) {

                }
            });
        },

        unblockContent: function() {
            return this.each(function(i, val) {
                var ele = $(this);
                ele
					.find('.BlockUI')
					.hide()
					.end()
					.removeData('IsBlocked', false);
				$(window)
					    .unbind('resize.blockContent');
            });
        },

        toCSV: function(opts) {
            var options =
                {
                    separator: ',',
                    noInclude: '.NotCSV',
                    stripSpaceCharacters: true,
					wrapperCharacter : '"'
                }

            $.extend(options, opts);

            var rows = [];
            this.find('tr').each(function(i) {
                var tds = [];
                $(this).find('th, td').not(options.noInclude).each(function(j) {
                    var text = $(this).text();
                    text = options.stripSpaceCharacters ? text.replace(/\s\s+/gi, '') : text;
                    tds.push(options.wrapperCharacter + text + options.wrapperCharacter);
                });
                rows.push(tds.join(options.separator));
            });

            return rows.join('\n');

        },
        getSelectedItem: function() {
            var item = null;                                                            
            if (this.length && this[0].options) {
                item = this[0].options[this[0].selectedIndex];
            }
            return item;
        },
        getItemsCount: function() {
            var count = 0;
            if (this.length && this[0].options) {
                count = this[0].options.length;
            }

            return count;
        },
        enterPressed: function(callBackEnter, CallBackEsc, data, discriminator) {
            var _enterCallBack = function(e) {
                var callbackResult = true;
				var args = { doBlur : false};
                if (e.keyCode == 13) {
                    if (typeof (callBackEnter) == 'function') {
                        callbackResult = callBackEnter(e, args);
                    }
                    if (args.doBlur) $(e.target).blur();

                }
                if (e.keyCode == 27) {
                    if (typeof (CallBackEsc) == 'function') {
                        callbackResult = CallBackEsc(e, args);
                    }
                    if (args.doBlur) $(e.target).blur();
                }
                return callbackResult;
            };
			if (!discriminator) 
				return this.bind('keypress.enterPressed', data, _enterCallBack);
			else 
				return this.bind('keypress.enterPressed_' + discriminator, data, _enterCallBack);
        },

        unbindEnterPressed: function(discriminator) {
			if (!discriminator)
				return this.unbind('keypress.enterPressed');
			else 
				return this.unbind('keypress.enterPressed_' + discriminator);
        },

        findById: function(str_id) {
            var parent_id = this[0].id;
            return $.byId(this[0].id + '_' + str_id);
        },

        mergeCells: function() {
            if (this.length > 1) {
                this.css('display', 'none');
                $(this[0]).attr('rowspan', this.length).css('display', '');
            }

            return this;
        },

        enableIfChangeHappens: function(area, opts) {

            if (this.length == 0)
                return;

            var options =
                {
                    disableClass: "DisabledButton",
                    onCalculate: function() {
                    }
                };

            options = $.extend(options, opts);

            var buttonForActivation = this;

            this.disable(true, options);

            var sw = new StateWatcher(area);

            sw.addEventListener('OnMD5Calculate', function(args) {
                options.onCalculate(args);
            });

            sw.addEventListener('ReturnedToInitialState', function() {
                buttonForActivation.disable(true, options);
            });
            sw.addEventListener('StateChanged', function(args) {

                buttonForActivation.disable(false);

            });

            return sw;
        },

        setAbsoluteWidth: function(w) {
            var dif = this.outerWidth() - this.width();
            return this.width(w - dif);
        },

        setAbsoluteHeight: function(h) {
            var dif = this.outerHeight() - this.height();
            return this.height(h - dif);
        }


    });

$(document).ready(function() {
    $.createDummyConsole();
});

/*MOZILLA OUTER_HTML*/


if (typeof (HTMLElement) != 'undefined') {
    var _emptyTags =
        {
            "IMG": true,
            "BR": true,
            "INPUT": true,
            "META": true,
            "LINK": true,
            "PARAM": true,
            "HR": true
        };

    HTMLElement.prototype.__defineGetter__("outerHTML", function() {
        var attrs = this.attributes;
        var str = "<" + this.tagName;
        for (var i = 0; i < attrs.length; i++) {
            str += " " + attrs[i].name + "=\"" + attrs[i].value + "\"";
        }

        if (_emptyTags[this.tagName]) {
            return str + ">";
        }

        return str + ">" + this.innerHTML + "</" + this.tagName + ">";
    });

    HTMLElement.prototype.__defineSetter__("outerHTML", function(sHTML) {
        var r = this.ownerDocument.createRange();
        r.setStartBefore(this);
        var df = r.createContextualFragment(sHTML);
        this.parentNode.replaceChild(df, this);
    });
}


jQuery.extend(
    {
        EventDispatcher: function() {
        }
    });

jQuery.extend($.EventDispatcher.prototype,
    {
        buildListenerChain: function() {
            if (!this.listenerChain) {
                this.listenerChain = {};
            }
        },
        addEventListener: function(type, listener) {
            if (!listener instanceof Function) {
                throw (
                    {
                        message: "Listener isn't a function"
                    });
            }

            this.buildListenerChain();

            if (!this.listenerChain[type]) {
                this.listenerChain[type] = [listener];
            }
            else {
                this.listenerChain[type].push(listener);
            }

        },
        hasEventListener: function(type) {
            return (this.listenerChain && this.listenerChain[type] != null);
        },
        removeEventListener: function(type, listener) {
            if (!this.hasEventListener(type)) {
                return false;
            }

            for (var i = 0; i < (this.listenerChain[type].length || 0); i++) {
                if (this.listenerChain[type][i] == listener) {
                    this.listenerChain.splice(i, 1);
                }
            }

        },
        removeEventsOfType: function(type) {
            if (!this.hasEventListener(type)) {
                return false;
            }

            for (var i = 0; i < this.listenerChain[type].length; i++) {
                this.listenerChain[type][i] = null;
            }
            this.listenerChain[type] = null;

        },
        dispatchEvent: function(o) {
            this.buildListenerChain();

            if (!this.hasEventListener(o.type)) {
                return false;
            }
            if (this.listenerChain[o.type] && this.listenerChain[o.type].length) {
                $.each(this.listenerChain[o.type], function(i, f) {
                    return (f(o) === false ? false : true);
                });
            }

        }
    });



function StateWatcher(str_selector) {
    var me = this;
    //only saves the val!!!!! not other properties!!!!
    var initialMD5 = getMD5FromValues();
    var checkInterval = null;

    var atLeastOnceHasChanged = false;

    $(str_selector).find(':input').bind('change keypress click', function(e) {
        if (e.type == 'change') {
            checkChanges();
        }
        else {
            clearTimeout(checkInterval);
            checkInterval = setTimeout(function() {
                checkChanges();
            }, 200);
        }
    });

    function getMD5FromValues() {
        values = "";
        $(str_selector).find(':input').each(function() {
            var val = $(this).val();
            if ($(this).is(':checkbox')) {
                val = $(this).attr('checked');
            }
            values += val;
        });

        var args =
            {
                type: 'OnMD5Calculate',
                extraValue: '',
                target: me
            };
        me.dispatchEvent(args);
        return $.md5(values + args.extraValue);
    }
    function checkChanges() {
        var newMD5 = getMD5FromValues();
        if (newMD5 != initialMD5) {
            atLeastOnceHasChanged = true;
            me.dispatchEvent(
                {
                    type: 'StateChanged',
                    target: me
                });
        }
        else {
            if (atLeastOnceHasChanged) {
                atLeastOnceHasChanged = false;
                me.dispatchEvent(
                {
                    type: 'ReturnedToInitialState',
                    target: me
                });
            }
        }

    }
    this.doCheckChanges = function() {
        checkChanges();
    };
    this.acceptChanges = function() {
        initialMD5 = getMD5FromValues();
    };
}

StateWatcher.inherits($.EventDispatcher);

(function ($) {
    $.fn.extend({
        collapsePanel: function (options) {
            var opts = {
                headerElement : '.Legend',
                contentElement : '.CollapsibleArea',
                useAnimation : false
            };
            
            $.extend(opts, options);                                                            
            
            return this.each(function () {            
                var me = $(this);
                me
                    .find(opts.headerElement)
                    .hover(function() {
                        if (!me.find(opts.contentElement).is(':visible')) {
                            $(this).addClass('Highlight');
                        }
                    }, function() {                
                         $(this).removeClass('Highlight');                
                    })
                    .click(function() {
                        var cte = me.find(opts.contentElement);
                        if (cte.is(':visible')) {
                            if (opts.useAnimation) {
                                cte.hide("blind", 
                                    {
                                        direction: "vertical"
                                    }, 500, function() { me.toggleClass('Collapsed'); });                          
                            }
                            else {
                                cte.hide();                                
                                me.toggleClass('Collapsed');
                            }
                        }
                        else {
                            if (opts.useAnimation) {
                                cte.show("blind", 
                                    {
                                        direction: "vertical"
                                    }, 500, function() { me.toggleClass('Collapsed');});
                            }
                            else {
                                cte.show();
                                me.toggleClass('Collapsed');
                            }
                        }
                    })
                
            });
            
        }
    });
})(jQuery);



(function($) {
    $.fn.extend(
        {
            disable: function(disable, opts) {
                if (arguments.length === 0) {
                    disable = true;
                }
                var options =
                    {
                        disableClass: 'DisabledButton'
                    };

                $.extend(options, opts);

                if (disable && !this.data("disabledClass")) {
                    this.addClass(options.disableClass);
                    this.each(function() {
                        var evts = {};
                        var events = $(this).data('events') ||
                        {};
                        for (var type in events) {
                            evts[type] = {};
                            for (var handler in events[type]) {
                                evts[type][handler] = events[type][handler];
                                evts[type][handler].data = events[type][handler].data;
                            }
                        }

                        $(this).data("_old_events", evts);
                        $(this).data("disabledClass", options.disableClass);
                        $.event.remove(this);
                        $(this).bind('click keydown', function() {
                            return false;
                        });
                    });
                }
                else {
                    if (!this.data("disabledClass") || disable) {
                        return;
                    }

                    this.removeClass(this.data('disabledClass') || options.disableClass);
                    this.each(function() {
                        $.event.remove(this);
                        var events = $(this).data('_old_events') ||
                        {};
                        for (var type in events) {
                            for (var handler in events[type]) {
                                $.event.add(this, type, events[type][handler], events[type][handler].data);
                            }
                        }
                    });

                    this.removeData('_old_events');
                    this.removeData("disabledClass");
                }
                return this;
            }
        });
})(jQuery);

/*
http://www.JSON.org/json2.js
2008-11-19
Public Domain.
NO WARRANTY EXPRESSED OR IMPLIED. USE AT YOUR OWN RISK.
See http://www.JSON.org/js.html
This file creates a global JSON object containing two methods: stringify
and parse.
JSON.stringify(value, replacer, space)
value       any JavaScript value, usually an object or array.
replacer    an optional parameter that determines how object
values are stringified for objects. It can be a
function or an array of strings.
space       an optional parameter that specifies the indentation
of nested structures. If it is omitted, the text will
be packed without extra whitespace. If it is a number,
it will specify the number of spaces to indent at each
level. If it is a string (such as '\t' or '&nbsp;'),
it contains the characters used to indent at each level.
This method produces a JSON text from a JavaScript value.
When an object value is found, if the object contains a toJSON
method, its toJSON method will be called and the result will be
stringified. A toJSON method does not serialize: it returns the
value represented by the name/value pair that should be serialized,
or undefined if nothing should be serialized. The toJSON method
will be passed the key associated with the value, and this will be
bound to the object holding the key.
For example, this would serialize Dates as ISO strings.
Date.prototype.toJSON = function (key) {
function f(n) {
// Format integers to have at least two digits.
return n < 10 ? '0' + n : n;
}
return this.getUTCFullYear()   + '-' +
f(this.getUTCMonth() + 1) + '-' +
f(this.getUTCDate())      + 'T' +
f(this.getUTCHours())     + ':' +
f(this.getUTCMinutes())   + ':' +
f(this.getUTCSeconds())   + 'Z';
};
You can provide an optional replacer method. It will be passed the
key and value of each member, with this bound to the containing
object. The value that is returned from your method will be
serialized. If your method returns undefined, then the member will
be excluded from the serialization.
If the replacer parameter is an array of strings, then it will be
used to select the members to be serialized. It filters the results
such that only members with keys listed in the replacer array are
stringified.
Values that do not have JSON representations, such as undefined or
functions, will not be serialized. Such values in objects will be
dropped; in arrays they will be replaced with null. You can use
a replacer function to replace those with JSON values.
JSON.stringify(undefined) returns undefined.
The optional space parameter produces a stringification of the
value that is filled with line breaks and indentation to make it
easier to read.
If the space parameter is a non-empty string, then that string will
be used for indentation. If the space parameter is a number, then
the indentation will be that many spaces.
Example:
text = JSON.stringify(['e', {pluribus: 'unum'}]);
// text is '["e",{"pluribus":"unum"}]'
text = JSON.stringify(['e', {pluribus: 'unum'}], null, '\t');
// text is '[\n\t"e",\n\t{\n\t\t"pluribus": "unum"\n\t}\n]'
text = JSON.stringify([new Date()], function (key, value) {
return this[key] instanceof Date ?
'Date(' + this[key] + ')' : value;
});
// text is '["Date(---current time---)"]'
JSON.parse(text, reviver)
This method parses a JSON text to produce an object or array.
It can throw a SyntaxError exception.
The optional reviver parameter is a function that can filter and
transform the results. It receives each of the keys and values,
and its return value is used instead of the original value.
If it returns what it received, then the structure is not modified.
If it returns undefined then the member is deleted.
Example:
// Parse the text. Values that look like ISO date strings will
// be converted to Date objects.
myData = JSON.parse(text, function (key, value) {
var a;
if (typeof value === 'string') {
a =
/^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)Z$/.exec(value);
if (a) {
return new Date(Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4],
+a[5], +a[6]));
}
}
return value;
});
myData = JSON.parse('["Date(09/09/2001)"]', function (key, value) {
var d;
if (typeof value === 'string' &&
value.slice(0, 5) === 'Date(' &&
value.slice(-1) === ')') {
d = new Date(value.slice(5, -1));
if (d) {
return d;
}
}
return value;
});
This is a reference implementation. You are free to copy, modify, or
redistribute.
This code should be minified before deployment.
See http://javascript.crockford.com/jsmin.html
USE YOUR OWN COPY. IT IS EXTREMELY UNWISE TO LOAD CODE FROM SERVERS YOU DO
NOT CONTROL.
*/
/*jslint evil: true */

/*global JSON */

/*members "", "\b", "\t", "\n", "\f", "\r", "\"", JSON, "\\", apply,
call, charCodeAt, getUTCDate, getUTCFullYear, getUTCHours,
getUTCMinutes, getUTCMonth, getUTCSeconds, hasOwnProperty, join,
lastIndex, length, parse, prototype, push, replace, slice, stringify,
test, toJSON, toString, valueOf
*/
// Create a JSON object only if one does not already exist. We create the
// methods in a closure to avoid creating global variables.

if (!this.JSON) {
    JSON = {};
}
(function() {

    function f(n) {
        // Format integers to have at least two digits.
        return n < 10 ? '0' + n : n;
    }

    if (typeof Date.prototype.toJSON !== 'function') {

        Date.prototype.toJSON = function(key) {

            return this.getUTCFullYear() + '-' +
            f(this.getUTCMonth() + 1) +
            '-' +
            f(this.getUTCDate()) +
            'T' +
            f(this.getUTCHours()) +
            ':' +
            f(this.getUTCMinutes()) +
            ':' +
            f(this.getUTCSeconds()) +
            'Z';
        };

        String.prototype.toJSON = Number.prototype.toJSON = Boolean.prototype.toJSON = function(key) {
            return this.valueOf();
        };
    }

    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, gap, indent, meta =
        { // table of character substitutions
            '\b': '\\b',
            '\t': '\\t',
            '\n': '\\n',
            '\f': '\\f',
            '\r': '\\r',
            '"': '\\"',
            '\\': '\\\\'
        }, rep;


    function quote(string) {

        // If the string contains no control characters, no quote characters, and no
        // backslash characters, then we can safely slap some quotes around it.
        // Otherwise we must also replace the offending characters with safe escape
        // sequences.

        escapable.lastIndex = 0;
        return escapable.test(string) ? '"' +
        string.replace(escapable, function(a) {
            var c = meta[a];
            return typeof c === 'string' ? c : '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
        }) +
        '"' : '"' + string + '"';
    }


    function str(key, holder) {

        // Produce a string from holder[key].

        var i, // The loop counter.
 k, // The member key.
 v, // The member value.
 length, mind = gap, partial, value = holder[key];

        // If the value has a toJSON method, call it to obtain a replacement value.

        if (value && typeof value === 'object' &&
        typeof value.toJSON === 'function') {
            value = value.toJSON(key);
        }

        // If we were called with a replacer function, then call the replacer to
        // obtain a replacement value.

        if (typeof rep === 'function') {
            value = rep.call(holder, key, value);
        }

        // What happens next depends on the value's type.

        switch (typeof value) {
            case 'string':
                return quote(value);

            case 'number':

                // JSON numbers must be finite. Encode non-finite numbers as null.

                return isFinite(value) ? String(value) : 'null';

            case 'boolean':
            case 'null':

                // If the value is a boolean or null, convert it to a string. Note:
                // typeof null does not produce 'null'. The case is included here in
                // the remote chance that this gets fixed someday.

                return String(value);

                // If the type is 'object', we might be dealing with an object or an array or
                // null.

            case 'object':

                // Due to a specification blunder in ECMAScript, typeof null is 'object',
                // so watch out for that case.

                if (!value) {
                    return 'null';
                }

                // Make an array to hold the partial results of stringifying this object value.

                gap += indent;
                partial = [];

                // Is the value an array?

                if (Object.prototype.toString.apply(value) === '[object Array]') {

                    // The value is an array. Stringify every element. Use null as a placeholder
                    // for non-JSON values.

                    length = value.length;
                    for (i = 0; i < length; i += 1) {
                        partial[i] = str(i, value) || 'null';
                    }

                    // Join all of the elements together, separated with commas, and wrap them in
                    // brackets.

                    v = partial.length === 0 ? '[]' : gap ? '[\n' + gap +
                    partial.join(',\n' + gap) +
                    '\n' +
                    mind +
                    ']' : '[' + partial.join(',') + ']';
                    gap = mind;
                    return v;
                }

                // If the replacer is an array, use it to select the members to be stringified.

                if (rep && typeof rep === 'object') {
                    length = rep.length;
                    for (i = 0; i < length; i += 1) {
                        k = rep[i];
                        if (typeof k === 'string') {
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (gap ? ': ' : ':') + v);
                            }
                        }
                    }
                }
                else {

                    // Otherwise, iterate through all of the keys in the object.

                    for (k in value) {
                        if (Object.hasOwnProperty.call(value, k)) {
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (gap ? ': ' : ':') + v);
                            }
                        }
                    }
                }

                // Join all of the member texts together, separated with commas,
                // and wrap them in braces.

                v = partial.length === 0 ? '{}' : gap ? '{\n' + gap + partial.join(',\n' + gap) + '\n' +
                mind +
                '}' : '{' + partial.join(',') + '}';
                gap = mind;
                return v;
        }
    }

    // If the JSON object does not yet have a stringify method, give it one.

    if (typeof JSON.stringify !== 'function') {
        JSON.stringify = function(value, replacer, space) {

            // The stringify method takes a value and an optional replacer, and an optional
            // space parameter, and returns a JSON text. The replacer can be a function
            // that can replace values, or an array of strings that will select the keys.
            // A default replacer method can be provided. Use of the space parameter can
            // produce text that is more easily readable.

            var i;
            gap = '';
            indent = '';

            // If the space parameter is a number, make an indent string containing that
            // many spaces.

            if (typeof space === 'number') {
                for (i = 0; i < space; i += 1) {
                    indent += ' ';
                }

                // If the space parameter is a string, it will be used as the indent string.

            }
            else
                if (typeof space === 'string') {
                indent = space;
            }

            // If there is a replacer, it must be a function or an array.
            // Otherwise, throw an error.

            rep = replacer;
            if (replacer && typeof replacer !== 'function' &&
            (typeof replacer !== 'object' ||
            typeof replacer.length !== 'number')) {
                throw new Error('JSON.stringify');
            }

            // Make a fake root object containing our value under the key of ''.
            // Return the result of stringifying the value.

            return str('', {
                '': value
            });
        };
    }


    // If the JSON object does not yet have a parse method, give it one.

    if (typeof JSON.parse !== 'function') {
        JSON.parse = function(text, reviver) {

            // The parse method takes a text and an optional reviver function, and returns
            // a JavaScript value if the text is a valid JSON text.

            var j;

            function walk(holder, key) {

                // The walk method is used to recursively walk the resulting structure so
                // that modifications can be made.

                var k, v, value = holder[key];
                if (value && typeof value === 'object') {
                    for (k in value) {
                        if (Object.hasOwnProperty.call(value, k)) {
                            v = walk(value, k);
                            if (v !== undefined) {
                                value[k] = v;
                            }
                            else {
                                delete value[k];
                            }
                        }
                    }
                }
                return reviver.call(holder, key, value);
            }


            // Parsing happens in four stages. In the first stage, we replace certain
            // Unicode characters with escape sequences. JavaScript handles many characters
            // incorrectly, either silently deleting them, or treating them as line endings.

            cx.lastIndex = 0;
            if (cx.test(text)) {
                text = text.replace(cx, function(a) {
                    return '\\u' +
                    ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
                });
            }

            // In the second stage, we run the text against regular expressions that look
            // for non-JSON patterns. We are especially concerned with '()' and 'new'
            // because they can cause invocation, and '=' because it can cause mutation.
            // But just to be safe, we want to reject all unexpected forms.

            // We split the second stage into 4 regexp operations in order to work around
            // crippling inefficiencies in IE's and Safari's regexp engines. First we
            // replace the JSON backslash pairs with '@' (a non-JSON character). Second, we
            // replace all simple value tokens with ']' characters. Third, we delete all
            // open brackets that follow a colon or comma or that begin the text. Finally,
            // we look to see that the remaining characters are only whitespace or ']' or
            // ',' or ':' or '{' or '}'. If that is so, then the text is safe for eval.

            if (/^[\],:{}\s]*$/.test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {

                // In the third stage we use the eval function to compile the text into a
                // JavaScript structure. The '{' operator is subject to a syntactic ambiguity
                // in JavaScript: it can begin a block or an object literal. We wrap the text
                // in parens to eliminate the ambiguity.

                j = eval('(' + text + ')');

                // In the optional fourth stage, we recursively walk the new structure, passing
                // each name/value pair to a reviver function for possible transformation.

                return typeof reviver === 'function' ? walk({
                    '': j
                }, '') : j;
            }

            // If the text is not JSON parseable, then a SyntaxError is thrown.

            throw new SyntaxError('JSON.parse');
        };
    }
})();


jQuery.delegate = function(rules) {
    return function(e) {
        var target = $(e.target);
        for (var selector in rules)
            if (target.is(selector))
            return rules[selector].apply(this, $.makeArray(arguments));
    }
}




 