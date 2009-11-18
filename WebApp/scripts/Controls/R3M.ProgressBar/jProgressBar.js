(function($) {
    $.Namespace('$.R3M');

    $.R3M.ProgressBar = function(selector, opts) {
    
        var options = {
           processingText : '' 
        }; 
    
        $.extend(options, opts);
        
        var me = this;

        _createProgressbar();

        me.setValue = function(val) {
            _updateValue(val);
        };

        function _updateValue(val) {
            $(selector)
                .find('.PBar-Bar')
	            .animate({ width: val + '%' }, $.easing.swing);
        }

        function _createProgressbar() {
            $(selector)
                .empty()
                .append('<div class="PBar-Container"><span class="PBar-Indicator">'+options.processingText+'</span><div class="PBar-Bar"></div></div>');
            $(selector)
                .find('.PBar-Bar')
                .css({ width: '0px' });
        }

    };

})(jQuery);