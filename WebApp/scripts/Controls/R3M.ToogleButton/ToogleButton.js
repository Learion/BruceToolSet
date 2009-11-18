/**
 * @author rriojas
 * Toogle Button is a jQuery plugin that requires the user has a custom markup in the site
 * custom markup needed is
 * <a href='#'><span state01_text="Run" state02_text="Cancel">DefaultInitialText</span></a>
 * 
 */
(function($)
{
    // Alias to jQuery Object.
    $.fn.convertToToggle = function(options)
    {                    		
		var default_options = {
			state01_text : null,
			state02_text : null,
			state01_click: null,
			state02_click: null
		};		
		
		$.extend(default_options, options);
		
		function _convertInToggle(ele, index) {
			
			var currentClickFunction = default_options.state01_click;
			
			function _doClickState(e){
				if (typeof currentClickFunction == 'function') 
				{
					var obj = { allowChangeState : true };

					currentClickFunction(e, obj);					
					
					if (obj.allowChangeState) {
						if (currentClickFunction == default_options.state01_click) {
							currentClickFunction = default_options.state02_click;
							ele.find('span').html(default_options.state02_text || ele.find('span').attr('state02_text'));
						}
						else {
							currentClickFunction = default_options.state01_click;
							ele.find('span').html(default_options.state01_text || ele.find('span').attr('state01_text'));
						}
					}
				}
				return false; //avoid default
			}
			
			ele.get(0).changeState = function (state) {
				if (state == 'state01') {
					currentClickFunction = default_options.state01_click;
					ele.find('span').html(default_options.state01_text || ele.find('span').attr('state01_text'));
				}
				if (state == 'state02') {
					currentClickFunction = default_options.state02_click;
					ele.find('span').html(default_options.state02_text || ele.find('span').attr('state02_text'));
				}
			};
			
			ele.find('span').html(default_options.state01_text || ele.find('span').attr('state01_text'));
			ele.bind('click.Toogle', _doClickState);											
		}
		
        this.each(function(ix)
        {
            // Self is alias to jQuery Object of actual element.
            _convertInToggle($(this), ix);
            
        });
    };
}(jQuery));