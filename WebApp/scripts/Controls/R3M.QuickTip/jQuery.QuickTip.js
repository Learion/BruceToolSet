$.fn.quickTips = function (options) {
    //TODO: use the opts parameters
    var opts = {
            tooltipWidth : 180,
            reuseExistingTipHolder : true,
            QuickTipExtraClass : null
        };            
        
    var offsetTop = -15;
        
    if ($('#__QuickTip').length == 0 ) {
            $('<div style="display:none;position:absolute" class="QuickTipHolder" id="__QuickTip"><div class="Arrow"></div><div class="content"></div></div>')
                .appendTo($('body'));
    }    
    
    var qdiv = $('#__QuickTip');
    
    $.extend(opts, options);
    
    if (opts.QuickTipExtraClass) qdiv.addClass(opts.QuickTipExtraClass);
    
    return this.each(function() {
        $(this)
            .focus(function () {                        
                var pos = $(this).offset();
                var ow =  $(this).outerWidth(true);
                qdiv.find('.content').html($(this).attr('title'));
                qdiv.css({top: pos.top + offsetTop, left: pos.left + ow });
                qdiv.fadeIn();
            })
            .blur(function() {
                qdiv.find('.content').html('');
                qdiv.hide();
            });
        });
};