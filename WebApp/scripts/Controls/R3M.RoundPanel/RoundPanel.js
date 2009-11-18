/**
 * @author rriojas
 */
(function($)
{
    // Alias to jQuery Object.
    $.fn.roundPanel = function()
    {    
        var _roundElement = function(ele, ix)
        {
            var round_div = ele;
            
            var top = round_div.hasClass('DiscardTop') ? '' : '<div class="hd"><div class="tl"></div><div class="tr"></div><div style="clear:both;"></div></div>';
            var middle = '<div class="bl"><div class="br ContentHolder">{TOKEN}<div style="clear:both;"></div></div></div>';
            var bottom = round_div.hasClass('DiscardBottom') ? '' : '<div class="ft"><div class="fl"></div><div class="fr"></div><div class="fc"></div><div style="clear:both;"></div></div>';
            
            var envelope = '<div id="ctr_' + ix + '" class="round_ctr">{0}</div>';
            
            envelope = $.stringFormat(envelope, top + middle + bottom);
            
            if (round_div.length) 
            {
                if (!round_div.hasClass('bd')) 
				{
					round_div.addClass('bd');
				}
                round_div.get(0).outerHTML = envelope.replace('{TOKEN}', round_div.get(0).outerHTML);
            }
        };
        
        
        this.each(function(ix)
        {
            if ($(this).attr('rounded') == 'true') return;            
            _roundElement($(this), ix);
            $(this).attr('rounded','true');            
        });
    };
}(jQuery));