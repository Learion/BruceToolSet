$(function() {
    function InitMask() {
        $('input.MASKED').each(function() {
            var current = $(this);
            if (current.attr('masked') == 'masked') {
                return;
            }
            else {
                var input_mask = current.attr('input_mask');
                
                current.mask(input_mask);
                current.attr('masked', 'masked');
            }
        });

    }
    
    if (typeof (Sys) != 'undefined') {
        Sys.Application.add_load(InitMask);
        
        if (!$.browser.msie) InitMask();
    }
    else {
        InitMask();
    }

});