/*global $, console*/

/*
* Enumeration for the renderer types
*/
var RendererType = {
    FreeRenderer: "Free",
    GridRenderer: "Grid",
    TableRenderer: "Table"
};

/**
* Render the content of the array like a Gridview Control in ASP.NET using templates (xHeader y xItemTemplate)
* @param {String} str_id the id of the div that has the templates
* @param {Object} opts configuration options
*/
function ArrayRenderer(str_id, opts) {
    var _options = {
        headerTemplatePath: 'table tbody',
        itemTemplatePath: 'table tbody',
        tagName: 'table'
    };

    $.extend(_options, opts);

    var _me = this;
    var _id = str_id;

    _me._divEle = $.byId(str_id); //jquery reference;
    //CSS ClassNames	
    var _itemClass = _me._divEle.attr('row_class');
    var _alternatingItemClass = _me._divEle.attr('alternating_row_class');
    var _headerClass = _me._divEle.attr('header_class');

    //Find the Templates - Header Template / Item Template 
    //TODO: Footer Template maybe?????
    var _selectorHeader = $.stringFormat('.xHeader {0}', _options.headerTemplatePath);
    var _selectorItem = $.stringFormat('.xItemTemplate {0}', _options.itemTemplatePath);

    var _header = _me._divEle.find(_selectorHeader);
    var _item = _me._divEle.find(_selectorItem);

    var _headerTemplate = _header.html();
    var _rowTemplate = _item.html();

    //fields for sorting feature
    _me.fields = {};

    //last order criteria used
    var _lastOrderCriteria = null;

    _me._dataSource = null;

    //EventHandlers
    _me.onDataBinding = null;

    _me.onDataBound = null;

    _me.onRowDataBound = null;

    _me.onRowDataBinding = null;
    _me.onRendered = null;
    _me.onRowsFinishedRendered = null;

    var _lastOrderCriteriaUsed = null;

    var _tokenPattern = /\{[\w\S]+?(?:(?:\|\w+\:\'){1}.+?\')*\}/g;

    _me._propertyTokens = _rowTemplate.match(_tokenPattern) || [];

    _me.setHeaderTemplate = function(tpl) {
        _headerTemplate = tpl;
    };

    _me.getHeaderTemplate = function() {
        return _headerTemplate;
    };

    _me.setRowTemplate = function(tpl) {
        _rowTemplate = tpl;
        _me._propertyTokens = _rowTemplate.match(_tokenPattern) || [];
    };

    _me.getRowTemplate = function() {
        return _rowTemplate;
    };

    function _getMatchkey(row, token) {
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
            obj = expression;
            //if there is a pattern, we have to fit the obj to it		
        } else if (obj) {
            if (numberReadable) {
                obj = $.formatNumberWithSeparator(new Number(obj), numberReadable);
            }
            if (numberFormat) {
                obj = $.formatNumber(obj, numberFormat, $.getResourceString('DecimalSeparator','.'));
            }
            if (pattern) {
                obj = pattern.replace(/\[0\]/, obj);
            }


        }
        return obj;
    }

    function _orderDataSourceBy(fieldName, orderType, dataType) {
        if (!_me._dataSource) {
            return;
        }
        try {
            _me._dataSource = _me._dataSource.sort(function(a, b) {
                var keyA = "";
                var keyB = "";
                //console.log("FieldName was : " + fieldName);
                var ValueForA = _getMatchkey(a, fieldName) || "";
                var ValueForB = _getMatchkey(b, fieldName) || "";

                if (!dataType || dataType == 'String') {
                    ValueForA = ValueForA.replace($.RegexHelper.HtmlTags, "");
                    ValueForB = ValueForB.replace($.RegexHelper.HtmlTags, "");

                    keyA = (ValueForA && ValueForA.length > 0) ? ValueForA.toUpperCase() : "";
                    keyB = (ValueForB && ValueForB.length > 0) ? ValueForB.toUpperCase() : "";
                }
                if (dataType == 'Number') {
                    keyA = ValueForA ? ValueForA * 1 : 0;
                    keyB = ValueForB ? ValueForB * 1 : 0;
                }
                if (keyA < keyB) {
                    return (orderType == "ASC") ? -1 : 1;
                }
                if (keyA > keyB) {
                    return (orderType == "ASC") ? 1 : -1;
                }
                return 0;
            });
        }
        catch (ex) {
            console.log('[Error] : ' + ex.message);
        }
    }

    _me.getMatchkey = function(row, token) {
        return _getMatchkey(row, token);
    };

    _me.orderBy = function(fieldName, orderType, dataType) {
        _lastOrderCriteriaUsed = _lastOrderCriteriaUsed || {};
        _lastOrderCriteriaUsed.fieldName = fieldName;
        _lastOrderCriteriaUsed.orderType = orderType;
        _lastOrderCriteriaUsed.dataType = dataType;

        //_orderDataSourceBy(fieldName, orderType, dataType);
        _me.dataBind();
    };

    function _removeListenerInHeader() {
        _me._divEle.find(".Sortable").unbind();
    }
    function _addListenerToHeader() {
        _me._divEle.find(".Sortable").click(function() {
            var node = $(this);

            var fieldName = node.attr('field_name');
            var orderType = _me.fields[fieldName];

            if (!orderType) {
                orderType = "ASC";
            }

            if (orderType == "DESC") {
                _me.fields[fieldName] = "ASC";
            }
            if (orderType == "ASC") {
                _me.fields[fieldName] = "DESC";
            }

            if (fieldName != _lastOrderCriteria) {
                _me.fields[_lastOrderCriteria] = null;
            }

            _lastOrderCriteria = fieldName;

            _lastOrderCriteriaUsed = _lastOrderCriteriaUsed || {};
            _lastOrderCriteriaUsed.fieldName = fieldName;
            _lastOrderCriteriaUsed.orderType = orderType;
            _lastOrderCriteriaUsed.dataType = node.attr('data_type');

            //_orderDataSourceBy(fieldName, orderType, node.attr('data_type'));
            _me.dataBind();

        }).mouseover(function() {
            var node = $(this);
            node.addClass("Over");
        }).mouseout(function() {
            var node = $(this);
            node.removeClass("Over");
        }).css({
            cursor: 'pointer'
        }).each(function(i) {
            var node = $(this);
            var fieldName = node.attr('field_name');
            var orderType = _me.fields[fieldName];
            if (orderType == "DESC") {
                node.addClass("ASC");
                return;
            }
            if (orderType == "ASC") {
                node.addClass("DESC");
                return;
            }
            if (!orderType && (_lastOrderCriteriaUsed)) {
                if (_lastOrderCriteriaUsed.fieldName == fieldName) {
                    node.addClass(_lastOrderCriteriaUsed.orderType);
                }
                return;
            }

        }).prepend("<div class='SortIndicator'></div>");
    }

    /*
    * Paints the data source
    */
    _me._doPaint = function(ds, useAnimation) {

        //Find the Item Template                    
        var headerRendered = _headerTemplate ? _headerTemplate.replace('[CLASS_NAME]', _headerClass) : '';
        var rowsRendered = '';
        var renderedHTML = $.stringFormat('<{0}>{1}{2}</{0}>', _options.tagName);
        var o = {};

        //Make this visible
        _me.setVisible(true, useAnimation);
        //Iterate over the retrieved rows
        if (!ds) 
        {
            ds = [];
        }
        
        for (var i = 0; i < ds.length; i++) {
            var result = _rowTemplate;
            var row = ds[i];

            if (typeof _me.onRowDataBinding == 'function') {
                _me.onRowDataBinding(_me, row);
            }

            for (var ix = 0; ix < _me._propertyTokens.length; ix++) {
                var token = _me._propertyTokens[ix];
                // It is included in the format pattern (in the next order): |, [, ], (, ), ., $, %, +, -, *, /, ?, !, :
                var tokenRegex = token.replace(/[\|\[\]\(\)\.\$\%\+\-\*\/\?\!\:]/g, "\\$&");
                var rgex = new RegExp(tokenRegex, 'g');
                result = result.replace(rgex, _getMatchkey(row, token));
            }
            //Format the rows
            var row_class = ((i % 2) === 0) ? _itemClass : _alternatingItemClass;
            var objResult = {};

            result = result.replace('[CLASS_NAME]', row_class);
            objResult.renderer = _me;
            objResult.rowClass = row_class;
            objResult.result = result;
            objResult.row = row;

            if (typeof _me.onRowDataBound == 'function') {
                _me.onRowDataBound(objResult);
            }
            //Add the result to the rows list to render
            rowsRendered += objResult.result;
        }

        var args = {};
        args.rows = rowsRendered;
        args.header = headerRendered;
        args.me = _me;
        if (typeof _me.onRowsFinishedRendered == 'function') {
            _me.onRowsFinishedRendered(args);
        }

        renderedHTML = renderedHTML.replace("{1}", args.header);
        renderedHTML = renderedHTML.replace('{2}', args.rows);

        o.renderedHTML = renderedHTML;
        o.renderer = _me;

        if (typeof _me.onDataBound == 'function') {
            _me.onDataBound(o);
        }
        _me._divEle.html(o.renderedHTML);

        if (typeof _me.onRendered == 'function') {
            _me.onRendered(o);
        }
    };

    _me.getId = function() {
        return _id;
    };

    /*
    * Set if the ArrayRenderer is visible
    */
    _me.setVisible = function(b_visible, useAnimation) {
        if (!useAnimation) {
            _me._divEle.css('display', b_visible ? 'block' : 'none');
        }
        else {
            if (b_visible) {
                _me._divEle.show("slide",
					{
					    direction: 'up'
					}, 400);
            }
            else {
                _me._divEle.hide("slide",
					{
					    direction: 'up'
					}, 400);
            }
        }

    };

    /*
    * Bind it with their data source
    */
    _me.dataBind = function(useAnimation) {
        var o = {};
        o.dataSource = _me._dataSource;
        if (typeof _me.onDataBinding == 'function') //transform the data in any way
        {
            _me.onDataBinding(o);
        }
        if (_lastOrderCriteriaUsed) {
            _orderDataSourceBy(_lastOrderCriteriaUsed.fieldName, _lastOrderCriteriaUsed.orderType, _lastOrderCriteriaUsed.dataType);
        }

        _removeListenerInHeader();
        _me._doPaint(o.dataSource, useAnimation);

        /* 
        * When a token is put inside the src attribute of an Image element (e.g <img src='{ImageUrl}' />)
        * the src field is changed by the browser to urlencode version for the token string (e.g <img src='%21ImageUrl%21') />
        * so the parser can't find that token, in order to avoid this we use an extra attribute for the <img> tags
        *
        * so the right way to put a value inside the src attribute should be  <img class='DynamicImage' image_src='{ImageUrl}'>
        */
        _me._findDynamicImages();
        _addListenerToHeader();
    };


    _me._findDynamicImages = function() {
        _me._divEle.find('img.DynamicImage').each(function(i) {
            $(this).attr('src', $(this).attr('image_src'));
        });
    };

    /*
    * Sets the data source
    */
    _me.setDataSource = function(dataSource) {
        _me._dataSource = dataSource;
        //console.log(" _me.getId in datasource" + _me.getId());
        return _me;
    };
}


/*
* Control and manage the ArrayRenderer in a Report
*/
function ArrayRendererManager() {
    var _me = this;
    /*
    * Store the registered renderers
    */
    _me._arrayRenderers = {};

    /*
    * Register the Array Renderer
    */
    var _registerArrayRenderer = function(obj_ArrayRenderer) {
        var arrayRendererId = obj_ArrayRenderer.getId();
        _me._arrayRenderers[arrayRendererId] = obj_ArrayRenderer;
    };

    /**
    * Find the ArrayRenderes in the Report, register them and return the count of ArrayRenderers
    * @param {Object} divReport
    */
    _me.findArrayRenderers = function(divReport) {
        if (!divReport || !divReport.length) {
            return;
        }

        var jReports = divReport.find('.ArrayRenderer').each(function(i) {
            var arrayRenderer;
            var element = $(this).attr('renderer_type') ? $(this).attr('renderer_type') : ""; //;
            // Instantiate the corresponding type

            var opts = {};

            //console.log('creating element -->' + element);
            if (element == RendererType.FreeRenderer) {

                opts.headerTemplatePath = $(this).attr('header_path') || "";
                opts.itemTemplatePath = $(this).attr('item_path') || "";
                opts.tagName = $(this).attr('container_tag_name');
                if (!opts.tagName || opts.tagName.length === 0) {
                    opts.tagName = 'div';
                }
            }
            if (element == RendererType.TableRenderer) {
                opts.headerTemplatePath = 'table tbody tr';
                opts.itemTemplatePath = 'table tbody tr';
                opts.tagName = 'table';
            }

            //console.log('this.id --> ' + this.id);
            arrayRenderer = new ArrayRenderer(this.id, opts);

            arrayRenderer.setVisible(false);



            _registerArrayRenderer(arrayRenderer);
        });

        return jReports.length;
    };

    /*
    * Return all the ArrayRenderers in report
    */
    _me.getArrayRenderers = function() {
        return _me._arrayRenderers;
    };

    /**
    * Get the ArrayRenderer by Id
    * @param {String} str_id the Id of the ArrayRenderer
    */
    _me.getArrayRendererById = function(str_id) {
        return _me._arrayRenderers[str_id];
    };

    /**
    * return the ArrayRenderer by Index
    * @param {Integer} index
    */
    _me.getArrayRendererByIndex = function(index) {
        var count = 0;
        for (var key in _me._arrayRenderers) {
            if (_me._arrayRenderers.hasOwnProperty(key)) {
                if (count == index) {
                    return _me._arrayRenderers[key];
                }
                count++;
            }
        }
        return null;
    };
}
