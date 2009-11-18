/**
* @author rriojas
*/
//TODO: Move the Renderer Methods to a separate file
//TODO: Create an ASP.NET wrapper to this component

$.Namespace('$.R3M');

$.GetObjectValueByKey = function(row, token) {
    var expression, pattern, numberFormat, numberReadable = null;
    //The idea is to extract both the default value expression and stringFormat Expression, StringFormat must be 
    //once the above regular expression is applied using match we get up to two strings, so we have to parse and 
    //try to obtain the command and to assing it to expression an to pattern vars
    //The next expression is lazy, so it will match as few characters as possible before satisfying the next token.
    var re = /\|\w+:'.*?'/g;
    var m = token.match(re);

    if (m) {
        //save its values into expression and pattern
        for (var i = 0; i < m.length; i++) {
            //var reAux = m[i].match(/'([\w\s\.\[\]\?-]*)'/);
            var reAux = m[i].match(/'(.*?)'/);
            if (m[i].match(/\|default:'/) && reAux !== null) {
                expression = reAux[1];
            } else if (m[i].match(/\|format:'/) && reAux !== null) {
                pattern = reAux[1];
            } else if (m[i].match(/\|numberFormat:'/) && reAux !== null) {
                numberFormat = reAux[1];
            } else if (m[i].match(/\|numberReadable:'/) && reAux !== null) {
                    numberReadable = reAux[1];
                }
        }
        //clean the token
        token = token.replace(re, '');
    }

    var tokens = token.replace(/\{|\}/g, '').split(".") || [];
    var obj = row;

    for (var ix = 0; ix < tokens.length; ix++) {
        try {
            obj = obj[tokens[ix]];
        }
        catch (ex) {
            console.log('[Error _getMatchkey] : ' + ex.message);
            obj = null;
            break;
        }
    }
    //if the object  has no value, then the default expression will be used
    if (expression !== null && (!obj || obj === '' || obj === 0)) {
        obj = expression || "";
        //if there is a pattern, we have to fit the obj to it		
    }
    else
        if (obj) {
        if (numberFormat) {
            obj = $.formatNumber(obj, numberFormat, $.getResourceString('DecimalSeparator','.'));
        }
        if ( numberReadable) {
            obj = $.formatNumberWithSeparator(obj, numberReadable);
        }    
        if (pattern) {
            obj = pattern.replace(/\[0\]/, obj);
        }
        
    }
    return obj;
};


$.R3M.ArrayRenderer = function(opts) {
    var me = this;
    var options =
        {
            dataSource: [],
            itemTemplate: "",
            onRendering: null,
            onItemRendering: null,
            onItemRendered: null,
            onRendered: null,
			encodeBlanks : true			
        };

    var _tokenPattern = /\{[\w\S]+?(?:(?:\|\w+\:\'){1}.+?\')*\}/g;
    var _propertyTokens = [];

    $.extend(options, opts);

    me.render = function() {
        _propertyTokens = options.itemTemplate.match(_tokenPattern) || [];
        var rowsRendered = "";
        for (var ix = 0; ix < options.dataSource.length; ix++) {
            var result = options.itemTemplate;
            var row = options.dataSource[ix];

            args = {};
            args.me = me;
            args.row = row;
            args.skip = false;

            if (options.onItemRendering) {
                options.onItemRendering(args);
            }

            if (!args.skip) {
                for (var ij = 0; ij < _propertyTokens.length; ij++) {
                    var token = _propertyTokens[ij];
                    var tokenRegex = token.replace(/[\|\[\]\(\)\.\$\%\+\-\*\/\?\!\:]/g, "\\$&");
                    var rgex = new RegExp(tokenRegex, 'g');
                    var objValue = $.GetObjectValueByKey(row, token);
					
					if (objValue && options.encodeBlanks && objValue.replace) {
						objValue = objValue.replace(/\s/gi,'&nbsp;');
					}					
                    result = result.replace(rgex, typeof objValue == 'undefined' ? "" : objValue);
                }
                rowsRendered += result;
            }
        }
        return rowsRendered;
    };

    me.setDataSource = function(dataSource) {
        options.dataSource = dataSource;
    };

    me.setItemTemplate = function(itemTemplate) {
        options.itemTemplate = itemTemplate;
    };

};

$.R3M.Combobox = function(selector, opts) {
    var me = this;
    var options =
        {
            dataSource: null,
            width: null,
            itemTemplate: "<div cbx_value='[VALUE]' title='[TEXT]' >[TEXT]</div>",
            textField: '{text}',
            valueField: '{value}',
            extraCssClass: null,
            dropDownListHeight: 350,
            emptyDataTemplate: '<div>-</div>',
            optionsAlignmentHorizontal: 'LEFT',
            optionsAlignmentVertical: 'BOTTOM'
        };

    var defaultTemplate = "<li cbx_value='[VALUE]' title='[TEXT]' class='CbxItem'>{0}</li>";

    $.extend(options, opts);

    var _isDirty = false;
    
    //enabled by default
    var _isDisabled = false;

    if (!options.dataSource) {
        options.dataSource = getDefaultArrayFromSelector();
    }
    else {
        _isDirty = true;
    }


    var cssDdl = null;
    var cssDdl_ulCtnr = null;
    var cssDdl_displayValueCtnr = null;

    var _lastItemTraversedWithKey = null;

    _createCSSDropDown();

    _dataBind();

    _updateSelectedValue();

    _bindEvents();

    function _bindEvents() {
        cssDdl
			.keydown(function(e) {
			    e.preventDefault();
			    if (!_isDisabled)
			        container_onKeyDown(e);
			});

        cssDdl_ulCtnr
			.blur(function(e) {
			    if (isDropDownVisible()) {
			        toggleDropDownList(false);
			    }
			});

        _bindEventsOnItems();

        cssDdl_displayValueCtnr
			.click(function(e) {
			    if (!_isDisabled)  toggleDropDownList(!isDropDownVisible());
			    e.stopPropagation();
			})
			.hover(function() {
			    cssDdl.addClass('over');
			},
			function() {
			    cssDdl.removeClass('over');
			});

        $(document).bind('click.combobox', function() { cssDdl_ulCtnr.blur(); });


    }

    function _bindEventsOnItems() {
        cssDdl_ulCtnr
			.find('li.CbxItem')
			.hover(function() {
			    _itemOver($(this));
			},
			function() {
			    _itemOut($(this));
			})
			.mousedown(function(e) {
			    _itemClicked($(this));
			});
    }

    function _itemOver(jNode) {
        if (!jNode) return;
        jNode.addClass("over");
    }

    function _itemOut(jNode) {
        if (!jNode) return;
        jNode.removeClass("over");
    }

    function _itemClicked(jNode) {
        var evtClick = { type: "ItemClick", itemJNode: jNode, cancel: false };
        me.dispatchEvent(evtClick);

        if (!evtClick.cancel) {
            var evtChanging = { type: "Changing", itemJNode: jNode, cancel: false };
            me.dispatchEvent(evtChanging);
            if (!evtChanging.cancel) {
                $(selector).val(jNode.attr('cbx_value'));
                _updateSelectedValue();
                raiseChangedEvent(jNode.attr('cbx_value'));
            }

        }
        toggleDropDownList(false);
    }

    function raiseChangedEvent(newValue) {
        var evtChanged = { type: "Changed", 'newValue': newValue, select: $(selector), cbx: me };
        me.dispatchEvent(evtChanged);
    }

    function container_onKeyDown(keyEvent) {
        switch (keyEvent.which) {
            case 33:
                //Page Up
            case 36:
                //Home
                selectValue(":first");
                break;

            case 34:
                //Page Down
            case 35:
                //End
                selectValue(":last");
                break;

            case 37:
                //Left
                selectValue(":previous");
                break;

            case 38:
                //Up
                if (keyEvent.altKey) {
                    // alt-up
                    // If DDL is hidden, then it is shown and vice-versa
                    toggleDropDownList(!(isDropDownVisible()));
                }
                else {
                    selectValue(":previous");
                }
                break;

            case 39:
                //Right
                selectValue(":next");
                break;

            case 40:
                //Down
                if (keyEvent.altKey) {
                    // alt-down
                    // If DDL is hidden, then it is shown and vice-versa
                    toggleDropDownList(!(isDropDownVisible()));
                }
                else {
                    selectValue(":next");
                }
                break;

            case 27:
                // Escape
                toggleDropDownList(false);
                break;
            case 13:
                selectValue(":current");
                toggleDropDownList(false);
                break;
            case 9:
                // Tab
                //TODO: Support alt-tab
                //TODO: Does not truly leave the Combobox if the DropDown is visible
				cssDdl.blur();
                cssDdl_ulCtnr.blur();

                // This is required in Internet Explorer as the blur() order is different
                /*$(document).focus();*/

                break;
        }

        keyEvent.cancelBubble = true;
    }

    var firstShow = true;
    function toggleDropDownList(show) {
        //setDropdownButtonState
        //put the DropDown below the selectedvalue
        var shadow = cssDdl
						.find('div.shadow');
        if (show) {

            if ($.browser.msie && firstShow) {
                cssDdl_ulCtnr.useWidthFromWidestDescendant('ul>li', { forceEvaluateIfHidden: true });
                firstShow = false;
                cssDdl_ulCtnr.find('ul>li:last').css('border', 'none');
            }

            cssDdl_ulCtnr.alignTo(cssDdl, { vertical: options.optionsAlignmentVertical, horizontal: options.optionsAlignmentHorizontal });

            var pos = cssDdl_ulCtnr.position();

            shadow
				.css({
				    width: cssDdl_ulCtnr.outerWidth() + 8,
				    height: cssDdl_ulCtnr.outerHeight() + 4
				})
					.data('inited', true)
					.alignTo(cssDdl, { vertical: options.optionsAlignmentVertical, horizontal: options.optionsAlignmentHorizontal, offsetHorizontal: options.optionsAlignmentHorizontal == 'LEFT' ? -4 : 4 })
					.fadeTo(0, 0.1);




            cssDdl_ulCtnr.fadeIn(300);
            shadow.fadeIn(300);


        }
        else {
            _itemOut(_lastItemTraversedWithKey);
            _lastItemTraversedWithKey = null;
            cssDdl_ulCtnr.fadeOut(200);
            shadow.fadeOut(200);
        }

    }

    function isDropDownVisible() {
        return cssDdl_ulCtnr.is(":visible");
    }



    ///<summary>
    ///	Selects a value from the list of options from the original Select options.
    ///	Does not use JQuery Selectors ':last' and ':first' because they take optgroup elements into
    ///	account.
    ///</summary>					
    function selectValue(subSelector) {
        var current = _lastItemTraversedWithKey ||
					cssDdl_ulCtnr.find('li.CbxItem.selected');
        var mustScroll = false;
        switch (subSelector) {
            case ":next":
                var next = current.next();
                if (next.length > 0) {
                    _itemOut(current);
                    _lastItemTraversedWithKey = next;
                    _itemOver(_lastItemTraversedWithKey);
                    mustScroll = true;
                }
                break;
            case ":previous":
                var prev = current.prev();
                if (prev.length > 0) {
                    _itemOut(current);
                    _lastItemTraversedWithKey = prev;
                    _itemOver(_lastItemTraversedWithKey);
                    mustScroll = true;
                }
                break;
            case ":current":
                _itemClicked(current);
                break;
            case ":first":
                var first = cssDdl_ulCtnr.find('li.CbxItem:first');
                if (first.length > 0) {
                    _itemOut(current);
                    _lastItemTraversedWithKey = first;
                    _itemOver(_lastItemTraversedWithKey);
                    mustScroll = true;
                }
                break;
            case ":last":
                var last = cssDdl_ulCtnr.find('li.CbxItem:last');
                if (last.length > 0) {
                    _itemOut(current);
                    _lastItemTraversedWithKey = last;
                    _itemOver(_lastItemTraversedWithKey);
                    mustScroll = true;
                }
                break;
        }
        if (mustScroll) { cssDdl_ulCtnr.scrollTo(_lastItemTraversedWithKey); }

    }



    function _updateSelectedValue() {
        var val = $(selector).val();
        cssDdl_displayValueCtnr.find("div.CbxValueContent").empty().append(cssDdl_ulCtnr.find('ul>li[cbx_value=' + val + ']').clone().html());
        cssDdl_ulCtnr.find('ul>li').removeClass('selected');
        cssDdl_ulCtnr.find('ul>li[cbx_value=' + val + ']').addClass('selected');
    }

    function _createCSSDropDown() {
        //create main container
        cssDdl = $("<div class='CbxContainer TabMePlease'></div>");
        cssDdl.addClass(options.extraCssClass);


        var select = $(selector);
        cssDdl.setAbsoluteWidth(options.width || select.width());
        select.before(cssDdl);
        cssDdl.append(select);
        select.hide();

        //create DisplayValue
        cssDdl_displayValueCtnr = $('<div class="CbxValueContainer"><div class="CbxValueContent"></div><div class="CbxDropDownButton"></div></div>');
        cssDdl.append(cssDdl_displayValueCtnr);

        cssDdl_ulCtnr = $('<div class="CbxDropDownWrapper"><ul class="CbxDropDownContainer TabMePlease"></ul><div>');

        cssDdl.append(cssDdl_ulCtnr);
        cssDdl_ulCtnr.after('<div class="shadow"></div>');


        var valContent = cssDdl_displayValueCtnr
			.find('.CbxValueContent');

        var w = cssDdl.width() - cssDdl_displayValueCtnr.find('.CbxDropDownButton').outerWidth();


        //set the width of the valueContent 
        valContent
			.setAbsoluteWidth(w);
        //console.log($(selector).val());
    }

    function getDefaultArrayFromSelector() {
        var arr = [];
        $(selector).find('option').each(function() {
            var item = {};
            item.value = $(this).attr('value');
            item.text = $(this).html();
            arr.push(item);
        });
        return arr;

    }

    function _dataBind() {
        if (_isDirty) {
            _updateSelectElement();
            _isDirty = false;
        }
        var ds = options.dataSource || [];
        var renderer = new $.R3M.ArrayRenderer(
            {
                dataSource: ds,
                itemTemplate: $.stringFormat(defaultTemplate, options.itemTemplate).replace(/\[VALUE\]/g, options.valueField).replace(/\[TEXT\]/g, options.textField),
                onItemRendering: function(args) {
                    var evt = {};
                    evt.type = 'ItemRendering';
                    evt.target = me;
                    evt.args = args;
                    me.dispatchEvent(evt);
                }
            });

        var htmlOptions = renderer.render();
        if (cssDdl_ulCtnr) {
            cssDdl_ulCtnr.find('ul').find('li').remove().end().append(htmlOptions);
        }

        if (cssDdl_ulCtnr.outerWidth() < cssDdl.outerWidth()) {
            cssDdl_ulCtnr.setAbsoluteWidth(cssDdl.outerWidth());
        }

        _setDropDownHeight();

        _raiseItemsRendered();

    }

    function _raiseItemsRendered(args) {
        var evtRendered = {};
        evtRendered.type = 'ItemsRendered';
        evtRendered.target = me;
        evtRendered.args = args || {};
        me.dispatchEvent(evtRendered);
    }


    function _setDropDownHeight() {
        if (options.dropDownListHeight) {
            cssDdl_ulCtnr.css('max-height', options.dropDownListHeight);
        }
    }



    function _updateSelectElement() {
        tpl = '<option value="[VALUE]" >[TEXT]</option>';
        tpl = tpl.replace(/\[VALUE\]/g, options.valueField).replace(/\[TEXT\]/g, options.textField);

        var optsRenderer = new $.R3M.ArrayRenderer( { encodeBlanks: false });
        optsRenderer.setItemTemplate(tpl);
        optsRenderer.setDataSource(options.dataSource);

        $(selector).find('option').remove().end().append(optsRenderer.render());
    }

    me.dataBind = function() {
        _dataBind();
        _updateSelectedValue();
        _bindEventsOnItems();
        return me;
    };

    me.setValueTo = function(str_value, raiseChanged) {
        $(selector).val(str_value);
        _updateSelectedValue();
        if (raiseChanged) {
            raiseChangedEvent(str_value);
        }
        return me;
    };

    me.setDataSource = function(dataSource) {

        options.dataSource = dataSource;
        _isDirty = true;
        return me;
    };

    me.getVisibleItemsCount = function() {
        return cssDdl_ulCtnr.find('li.CbxItem ').length;
    };

    me.getItemsCount = function() {
        return $(selector).getItemsCount();
    };

    me.setItemTemplate = function(itemTemplate) {
        options.itemTemplate = itemTemplate;
        return me;
    };

    me.setTextField = function(textField) {
        options.textField = textField;
        return me;
    };

    me.setValueField = function(valueField) {
        options.valueField = valueField;
        return me;
    };

    me.getCbxValueContainer = function() {
        return cssDdl_displayValueCtnr;
    };

    me.getDropDown = function() {
        return $(selector);
    };

    me.getSelectedValue = function() {
        return $(selector).val();
    };

    me.getSelectedItemText = function() {
        var item = $(selector).getSelectedItem();
        if (item) {
            return item.text;
        }
    };
    
    me.disable = function ()  {                
       toggleDropDownList(false);
       _isDisabled = true;  
	   cssDdl.addClass('Disabled');
    };
    me.enable = function () {
       _isDisabled = false;    
	   cssDdl.removeClass('Disabled');
    };

};

$.R3M.Combobox.inherits($.EventDispatcher);
