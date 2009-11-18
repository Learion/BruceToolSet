jQuery.fn.addConfirm = function(message, opts) {
    var options = {
        useConfirmMessageFromElement: true,
		useItemNameInElement : true
    };
    $(options, opts);
    return this.each(
		function(i, ele) {
		    var jEle = $(ele);		    
		    
		    //TODO: Support for click on other elements?
		    if (!jEle.attr("href")) return;
		    if (jEle.attr('confirm_added') == 'true' || !jEle.is('a')) {
		        return true;  //continue
		    }

		    jEle
				.clone().attr('id', ele.id + '__clone')
				.insertAfter(jEle)
				.click(function() {
				    options.onOkClicked = function() {
				        if (ele.onclick) {
				            if (!ele.onclick.apply) {
				                ele.onclick = new Function(ele.onclick);
				            }
				        }
				        jEle.trigger('click');
				    };

				    options.onCancelClicked = function() {
				        //do nothing?
				    };

				    if (options.useConfirmMessageFromElement) {
				        message = jEle.attr('confirm_message') || message;
				    }
					
					if (options.useItemNameInElement) {
						var item_name = jEle.attr('item_name');
						if (item_name && message.indexOf('{0}') > -1)
							message = message.replace('{0}', item_name);
					}

				    $.showConfirm(message, options);

				    //do nothing in the clone
				    return false;
				});


		    jEle
				.hide()
				.attr('confirm_added', 'true');
		    if (jEle.attr("href").indexOf('javascript:' > -1)) {
		        jEle.attr('onclick', jEle.attr('href').replace('javascript:', ''));
		    }
		}
	 );
};
