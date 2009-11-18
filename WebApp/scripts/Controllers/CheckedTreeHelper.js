$.Namespace('$.R3M.Helpers');

$.R3M.Helpers.CheckedTree = function(selector, opts) {
    var me = this;
    var options = {
        dataSource: []
    };

    $.extend(options, opts);

    var mainUl = '<ul class="tree">{0}</ul>';
    var mainLi = '<li item_value="{0}"><span>{1}</span>{2}';
    var subLevelUl = '<ul class="noCheck">{0}</ul>';
    var subLevelLi = '<li><span>{0}</span></li>';

    function _render() {
        var renderedTree = _createTreeFromDataSource();
        var tree = $(selector)
						.empty()
						.append(renderedTree)
						.find('ul.tree');

        tree.checkTree({
            onCheck: function(el) {
                if (el.attr('item_value')) {
                    var checks = tree.find(">li[item_value]>.checkbox.checked");
                    var all = tree.find(">li[item_value]>.checkbox");
                    me.dispatchEvent({ type: 'CheckedStateChanged', action: 'checked', node: el, checked: checks.length, unChecked: all.length - checks.length });
                }
            },
            onUnCheck: function(el) {
                if (el.attr('item_value')) {
                    var checks = tree.find(">li[item_value] >.checkbox.checked");
                    var all = tree.find(">li[item_value]>.checkbox");
                    me.dispatchEvent({ type: 'CheckedStateChanged', action: 'unChecked', node: el, checked: checks.length, unChecked: all.length - checks.length });
                }
            }
        });
    }
    function _renderSecondLevelChildren(arr) {
        if (arr.length === 0) {
            return "";
        }
        var secondLevelLi = "";
        for (var ix = 0; ix < arr.length; ix++) {
            var item = arr[ix];
            secondLevelLi += $.stringFormat(subLevelLi, item.Name);
        }

        return $.stringFormat(subLevelUl, secondLevelLi);
    }
    function _createTreeFromDataSource() {
        var level01_lis = "";
        for (var ix = 0; ix < options.dataSource.length; ix++) {
            var item = options.dataSource[ix];
            level01_lis += $.stringFormat(mainLi, item.IdKeywordList, item.Name, _renderSecondLevelChildren(item.Keywords || []));
        }
        return $.stringFormat(mainUl, level01_lis);
    }

    me.setDataSource = function(ds) {
        options.dataSource = ds;
    };

    me.render = function() {
        _render();
    };

    me.checkAll = function(raiseChanged) {
        var tree = $(selector).find('ul.tree');
        var all = tree.find(">li[item_value]>.checkbox");
        $.each(all, function(i, val) {
            $(val).addClass("checked");
        });
        var checks = tree.find(">li[item_value] >.checkbox.checked");
        if (raiseChanged == true) {
            me.dispatchEvent({ type: 'CheckedStateChanged', action: 'checked', checked: checks.length, unChecked: all.length - checks.length });
        }
        return me;
    }
    me.checkNone = function(raiseChanged) {
        var tree = $(selector).find('ul.tree');
        var all = tree.find(">li[item_value]>.checkbox");
        $.each(all, function(i, val) {
            $(val).removeClass("checked");
        });
        var checks = tree.find(">li[item_value] >.checkbox.checked");
        if (raiseChanged == true) {
            me.dispatchEvent({ type: 'CheckedStateChanged', action: 'unChecked', checked: checks.length, unChecked: all.length - checks.length });
        }
        return me;
    }
    me.getSelected = function() {
        var tree = $(selector).find('ul.tree');
        var checks = tree.find(">li[item_value] >.checkbox.checked");
        var checkedItemsValue = [];
        $.each(checks, function(i, val) {
            checkedItemsValue.push(parseInt($(val).parent().attr("item_value")));
        });
        return checkedItemsValue;
    }
    me.setCheckedItems = function(values) {
        var tree = $(selector).find('ul.tree');
        var lis = tree.find(">li[item_value]");
        $.each(lis, function(i, li) {
            $.each(values, function(i, val) {
                if (parseInt($(li).attr("item_value")) == val)
                    $(li).find(">.checkbox").addClass("checked");
            });
        });
        return me;
    }
};


$.R3M.Helpers.CheckedTree.inherits($.EventDispatcher);
