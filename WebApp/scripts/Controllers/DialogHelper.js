jQuery.extend(
    {
        showBigDialog: function(msg, opts) {
            var options =
                {
                    extraClass: 'BigDialogs'
                };

            $.extend(options, opts);
            var oldOnShow = options.onShow || function() { };
            options.onShow = function(dlg) {

                //var w = 0;
                dlg
                    .find('a.button span.Center')
                    .each(function() {
                        var ele = $(this);
                        ele.html(ele.html().replace(/\s/g, '&nbsp;'));
                    })
                    .end();
                /* .find('.DialogCommands ul li').each(function()
                {
                w += $(this).outerWidth();
                });*/
                dlg
					.find('.DialogCommands')
                //.css('width', w + 10)
					.end()
					.find('h2.dlg-extra-title')
					.html(dlg.data('opts').title || "");

                dlg
					.draggable(
                    {
                        handle: '.dlg-dragHandler'
                    });


                $.centerInScreen(dlg);
                oldOnShow(dlg);
            };
            $.showDialog(msg, options);
        }
    });

jQuery.extend({
    confirmNavigateAway: function(options) {
        var opts = {
            title: 'Please Confirm',
            txtMessage: 'Confirm you want to navigate away',
            stayText: 'stay',
            leaveText: 'leave',
            beforeShowConfirmation: null
        };

        $.extend(opts, options);

        function findParentElement(ele, comparisonDelegate, times) {
            if (!(comparisonDelegate && $.isFunction(comparisonDelegate)) || !ele) return null;
            if (!times) times = 3;
            var currentEle = ele;
            while (times > 0) {
                currentEle = currentEle.parent();
                if (comparisonDelegate(currentEle)) return currentEle;
                times--;
            }

        }

        $(window.document).bind('click',
				function(e) {
				    var ele = $(e.target);

				    if (!ele.is('a')) {
				        ele = findParentElement(ele, function(elem) { return elem.is('a'); }, 3);
				    }

				    if (ele && ((ele.is('a[href]') && ele.attr('href').indexOf('#') != 0 && ele.attr('href').indexOf('javascript:') != 0) || ele.is('.External'))) {
				        if (opts.beforeShowConfirmation) {
				            var args = {};
				            args.cancel = false;
				            opts.beforeShowConfirmation(args);
				            if (args.cancel) return;
				        }
				        window.old_before_unload = window.onbeforeunload;
				        window.onbeforeunload = null;
				        $.showBigDialog(opts.txtMessage, {
				            title: opts.title,
				            icon: $.MessageIco.Warning,
				            buttons: [{ text: opts.leaveText, command: 'leaveCmd' }, { text: opts.stayText, command: 'stayCmd'}],
				            showOk: false,
				            showCancel: false,
				            onCommand: function(args) {
				                if (args.command == 'leaveCmd') {
				                    window.location.href = ele.attr('href');
				                }
				                else {
				                    window.onbeforeunload = window.old_before_unload;
				                }
				            }
				        });
				        return false;
				    }
				}
			);

    }
});
