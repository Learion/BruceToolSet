/**
 * @author rriojas
 */
function TreeHelper(options)
{		
	
    var me = this;
    
    var opts = 
        {
            selector: null,
            onEditorBlur: false,
            onNodeElementAdding: false,
            onNodeListAdding: false,
            onNodeElementAdded: false,
            onNodeListAdded: false,
            onNodeElementUpdating: false,
            onNodeDblClicking: false,
            onNodeClick: false,
            onNodeDeleting: false,
            onEditorEscape: false,
			addNewElementString : 'Add New Element',
			addNewElementListString : 'Add New List'
        };
    
    $.extend(opts, options);
    
    function encodeVal(val)
    {
        if (!val) 
        { return ""; }
        
        if (val.indexOf('<') > 0 || val.indexOf('>') > 0) 
        { return val.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;'); }
        return val;
    }
    
    me.enconde = function(val)
    {
        return encodeVal(val);
    };
    
	
	
    function createInlineEditor(node, textForNewElement, onBlur, Tree)
    {
        node.addClass("inline-editing");
        Tree.lastEditingElement =
        $('<input type="text" class="inline-edit" />')
			.val(textForNewElement)
			.appendTo(node)
			.enterPressed(function() {
			    if (Tree.lastEditingElement) {
			        Tree.lastEditingElement.blur();
			    }
			}, function(e, args) {
			    if (Tree.lastEditingElement) {
			        Tree.lastEditingElement.attr('dontBlur', 'true');
			    }
			    if (opts.onEditorEscape) {
			        opts.onEditorEscape(this, {});
			    }
			    args.doBlur = true;
			    return true;
			})
            .blur(function() {
                console.log('bluring');
                if (this.bluredNow) {
                    return;
                }
                if (onBlur && !Tree.lastEditingElement.attr('dontBlur')) {
                    if (opts.onEditorBlur) {
                        var onBlurArgs = { cancel: false, "node": node, value: this.value, "Tree": Tree, editorElement: this };
                        opts.onEditorBlur(this, onBlurArgs);
                        if (onBlurArgs.cancel) {
                            var inputTextBox = $(Tree).find('input.inline-edit');
                            setTimeout(function() { inputTextBox.focus().select(); }, 30);
                            return;
                        }
                        onBlur(node, this);
                        this.bluredNow = true;
                    }
                    else {
                        onBlur(node, this);
                        this.bluredNow = true;
                    }
                }
                $(this).remove();
                Tree.removeInLineEditing();
            })
            .focus()
            .select();
        }
    function _addNode(node, val, type, Tree, callbackElementAdded)
    {
        if (!type) 
        {
            type = "Element";
        }
        if (val) 
        {
            val = $.trim(encodeVal(val));
        }
        switch (type)
        {
            case "Element":
                Tree.addNodeTo(1, val, node.parent().parent(), function(o, d)
                {
                    //d.addClass(className).attr('competitor_id',id); 
                    if (opts.onNodeElementAdded) 
                    {
                        opts.onNodeElementAdded(me, 
                            {
								oNode: node, //original node
                                node: d,
                                tree: Tree
                            });
						me.lostFocus();
                    }
                    
                    if (callbackElementAdded) 
                    {
                        callbackElementAdded(me, 
                            {
                                node: d,
                                tree: Tree
                            });
                    }
                });
                break;
            case "ElementList":
                var html = $("<ul><li class='open'><span>" + val + "</span><ul><li class='AddCommandElement'><span>"+ opts.addNewElementString+ "</span></li></ul></li></ul>").appendTo(node.parent().parent());
                Tree.setTreeNodes(html, false);
                if (opts.onNodeListAdded) 
                {
                    opts.onNodeListAdded(me, 
                        {
                            node: html,
                            tree: Tree
                        });
                }
                if (callbackElementAdded) 
                {
                    callbackElementAdded(me, 
                        {
                            node: html,
                            tree: Tree
                        });
                }
                break;
        }
    }
	
	//$(opts.selector).parent().bind('keypress', function() { alert('keypress over the tree ' + opts.selector ); } );	
	
	if (!TreeHelper.EventAdded) {
		var eType = "keydown";
		$(document).bind( eType, 
			function(e) { 			
				if (!TreeHelper.CurrentTree) 
				{ return; }
				
				var aTextNodeSelected = TreeHelper.CurrentTree.getCurrentNode().find('>span.active').is(':visible');

				if (aTextNodeSelected) {
					//Up pressed
					if (e.keyCode == 38) {
						TreeHelper.CurrentTree.moveToPreviousItem();
						return false;
					}
					//Down pressed 
					if (e.keyCode == 40) {
						TreeHelper.CurrentTree.moveToNextItem();
						return false;
					}
					//F2
					if (e.keyCode == 113) {
						TreeHelper.CurrentTree.getCurrentNode().find('>span.active').trigger('dblclick');
						return false;
					}															
				}
			} );
		TreeHelper.EventAdded = true;
	}
	
	
    var tree = $(opts.selector).simpleTree(
        {
            autoclose: false,
            afterClick: function(_node, Tree)
            {    
			
				if (TreeHelper.CurrentTree && 
						TreeHelper.CurrentTree.lostFocus &&
							TreeHelper.CurrentTree !== me) {
					TreeHelper.CurrentTree.lostFocus();
				}
			
				TreeHelper.CurrentTree = me;        
				

				
                if (opts.onNodeClick) 
                {
                    opts.onNodeClick(me, 
                        {
                            node: _node,
                            tree: Tree
                        });
                }							            
               if (Tree.lastEditingElement) 
	            {
	                Tree.lastEditingElement.attr('dontBlur', 'true');
					Tree.lastEditingElement.blur();
					if (opts.onEditorEscape) {
						opts.onEditorEscape(this, {});
					}
	            }                
            },
            beforeDblClick: function(node, Tree, obj)
            {				
				            
                if (node.get(0).className.indexOf('folder-close') >= 0) 
                {
                    obj.cancel = true;
                }
                
                if (opts.onNodeDblClicking) 
                {
                    obj.node = node;
                    obj.tree = Tree;
                    opts.onNodeDblClicking(me, obj);
                    if (obj.cancel) 
                    { return; }
                }
            },
            afterDblClick: function(node, Tree)
            {
                if (node.hasClass('AddCommandElementList')) 
                {
                    createInlineEditor(node, "", function(node, editorElement)
                    {
                        var val = $.trim(editorElement.value);
                        if (val === '') 
                        { return; }
                        
                        
                        var obj = 
                            {
                                cancel: false
                            };
                        obj.node = node;
                        obj.editorElement = editorElement;
                        
                        if (opts.onNodeListAdding) 
                        {
                            opts.onNodeListAdding(me, obj);
                        }
                        if (obj.cancel) 
                        { return; }
                        _addNode(node, $.trim(editorElement.value), "ElementList", Tree);
                    }, Tree);
                    return;
                }
                if (node.hasClass('AddCommandElement')) 
                {
                    createInlineEditor(node, "", function(node, editorElement)
                    {
                        var val = $.trim(editorElement.value);
                        if (val === '') 
                        { return; }
                        
                        var obj = 
                            {
                                cancel: false
                            };
                        obj.node = node;
                        obj.editorElement = editorElement;
                        
                        if (opts.onNodeElementAdding) 
                        {
                            opts.onNodeElementAdding(me, obj);
                        }
                        if (obj.cancel) 
                        { return; }
                        
                        _addNode(node, val, "Element", Tree);
                    }, Tree);
                    
                    return;
                }
                
                
                if (Tree.lastEditingElement) 
                {
                    Tree.lastEditingElement.remove();
                }                
              
                createInlineEditor(node, $.trim(node.find("span:first").text()), function(node, editorElement)
                {
                    var val = $.trim(editorElement.value);
                    if (val === '') 
                    { return; }
                    
                    
                    var obj = 
                        {
                            cancel: false
                        };
                    obj.node = node;
                    obj.editorElement = editorElement;
                    
                    if (opts.onNodeElementUpdating) 
                    {
                        opts.onNodeElementUpdating(me, obj);
                    }
                    if (obj.cancel) 
                    { return; }
                    me.updateNodeText(node, editorElement.value);
                }, Tree);
            },
            animate: false
        });
    
    
    me.addNodeList = function(node, val, callbackElementAdded)
    {
        _addNode(node, val, "ElementList", tree.get(0), callbackElementAdded);
    };
    
    me.addNodeElement = function(node, val, callbackElementAdded)
    {
        _addNode(node, val, "Element", tree.get(0), callbackElementAdded);
    };
    
    me.updateNodeText = function(node, val)
    {
        node.find("span:first").text($.trim(encodeVal(val)));
    };
    
    me.getCurrentNode = function()
    {
        return tree.get(0).getSelected();
    };
    me.deleteCurrentNode = function()
    {
        tree.get(0).delNode();
    };
	
	me.createEditor = function (str_type) {
		//var q = tree.find(str_query);		
	};
	
	me.moveToNextItem = function() {
		var node = me.getCurrentNode().next().next();
		if (node.length === 0) 
		{
			node = me.getCurrentNode().parent().next().find('>li').eq(1);
		}
		console.log('html -> ' + node.html());
		node.find('>span').trigger('click');		
	};
	
	me.moveToPreviousItem = function() {
		var node = me.getCurrentNode().prev().prev();
		if (node.length === 0) 
		{
			node = me.getCurrentNode().parent().prev().find('>li').eq(1);
		}		
		node.find('>span').trigger('click');		
	}; 
	
	me.getCurrentTreeSelector = function () {
		return opts.selector;
	};
	
	me.lostFocus = function() {
		me.getCurrentNode().find('>span.active').removeClass('active').addClass('text').end().blur();
	}; 
}

TreeHelper.CurrentTree = null;
TreeHelper.EventAdded = false;