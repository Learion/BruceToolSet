//by Brandon Goldman
jQuery.extend(
    {
        DimScreenManager: function()
        {
            var me = this;            
            var _dimScreens = {};            
            var _dimScreenCount = 0;            
            var _resizeEventBounded = false;            
            var _lastKey = null;
            
            
            function _boundResizeEvent()
            {
                $(window)
					.bind('resize.DimscreemManager', _onWindowResize)
					.bind('scroll.DimscreemManager', _onWindowScroll);
                _resizeEventBounded = true;
            }
            
            function _unBoundResizeEvent()
            {
                $(window)
					.unbind('resize.DimscreemManager', _onWindowResize)
					.unbind('scroll.DimscreemManager', _onWindowScroll);
                _resizeEventBounded = false;
            }
            
            function _onWindowScroll(e)
            {
                _onWindowResize(e);
            }
            function _onWindowResize(e)
            {
            
                var wW = $(window).width();
                var wH = $(window).height();
                
                for (var key in _dimScreens) 
                {
                    if (_dimScreens[key]) 
                    {
                        _dimScreens[key].dockInClientWindow(wW, wH);
                    }
                }
                
                
            }
            
            this.getLastZIndex = function()
            {
                return _dimScreenCount * 1000;
            };
            
            this.dimScreen = function(associateId, speed, opacity, callback)
            {
                if (!_dimScreens[associateId]) 
                {
                    _dimScreenCount++;
                    _dimScreens[associateId] = new $.DimScreen(associateId);
                    _lastKey = associateId;
                }
                _dimScreens[associateId].show(speed, opacity, function() { if (callback) callback (); _onWindowResize() }).css(
                    {
                        zIndex: me.getLastZIndex()
                    });
                
                if (_dimScreenCount > 0 && !_resizeEventBounded) 
                {
                    _boundResizeEvent();
                }
            };
            
            this.unDimScreen = function(associateId, callback)
            {
                if (!associateId) 
                {
                    associateId = _lastKey;
                }
                
                if (_dimScreens[associateId]) 
                {
                    _dimScreenCount--;
                    _dimScreens[associateId].hide(callback);
                    _dimScreens[associateId] = null;
                    delete (_dimScreens[associateId]);
                }
            };
            
            if (_dimScreenCount === 0 && _resizeEventBounded) 
            {
                _unBoundResizeEvent();
            }
            
        },
        
        DimScreen: function(associateId)
        {
            var curr_id = '__dimScreen_' + associateId;
            var me = this;
            var visible = false;
            
            var _fade_opacity = null;
            var _speed = null;
            
            var dimDiv = $('<div></div>')
				.attr(
                {
                    id: curr_id
                })
				.css(
                {
                    background: '#000',
                    left: '0px',
                    opacity: 0,
                    position: 'absolute',
                    top: '0px'
                });
            
            
            this.show = function(speed, opacity, callback)
            {            
                var placeholder;
                
                if (visible) 
                { return; }
                
                if (typeof speed == 'function') 
                {
                    callback = speed;
                    speed = null;
                }
                
                if (typeof opacity == 'function') 
                {
                    callback = opacity;
                    opacity = null;
                }
                
                if (speed < 1) 
                {
                    placeholder = opacity;
                    opacity = speed;
                    speed = placeholder;
                }
                
                if (opacity >= 1) 
                {
                    placeholder = speed;
                    speed = opacity;
                    opacity = placeholder;
                }
                
                speed = (speed > 0) ? speed : 500;
                opacity = (opacity > 0) ? opacity : 0.5;
                
                
                if ($.browser.msie && $.browser.version.substr(0, 1) == '6') 
                {
                    $('select').each(function(index)
                    {
                        if ($(this).is(":visible")) 
                        {
                            $(this).attr('dim_hide', "true").hide();
                        }
                    });
                }
                
                _fade_opacity = opacity;
                _speed = speed;
                
                var wW = $(window).width();
                var wH = $(window).height();
                
                dimDiv.css(
                    {
                        width: wW,
                        height: wH,
                        zIndex: $.DimScreenManager.getInstance().getLastZIndex()
                    }).appendTo(document.body).fadeTo(speed, opacity, callback);                
				
				this.dockInClientWindow(wW, wH);
                return dimDiv;                                
            };
            
            this.hide = function(callback)
            {
                var dimDiv = $.byId(curr_id);
                if (!dimDiv.length) 
                { return; }
                dimDiv.fadeOut(_speed, function()
                {
                    dimDiv.remove();
                    if (jQuery.browser.msie && jQuery.browser.version.substr(0, 1) == '6') 
                    {
                        $('select').each(function(index)
                        {
                            if ($(this).attr("dim_hide") == "true") 
                            {
                                $(this).attr("dim_hide", "false").show();
                            }
                        });
                    }
                    if (typeof callback == 'function') 
                    {
                        callback();
                    }
                });
            };
            this.dockInClientWindow = function(wWidth, wHeight)
            {
                var dimDiv = $.byId(curr_id);
                
                if (!dimDiv.length) 
                { return; }
                
				console.log('$(document).scrollTop() : ' + $(document).scrollTop());
                dimDiv.css(
                    {
                        top: $(document).scrollTop(),
                        width: wWidth,
                        height: wHeight
                    });
					
				console.log('dimDiv.css.top : 	' + dimDiv.css('top'));
            };
            
        },
        
        dimScreenCount: 0,
        
        //dims the screen
        dimScreen: function(speed, opacity, callback, id)
        {
        
            if (!id) 
            {
                id = '__dim__screen_';
            }
            
            $.DimScreenManager.getInstance().dimScreen(id, speed, opacity, callback);
            
            
        /*
         if ($.dimScreenCount < 0)
         $.dimScreenCount = 0;
         
         $.dimScreenCount++;
         
         console.log('$.dimScreenCount = ' + $.dimScreenCount);
         
         if (typeof speed == 'function') {
         callback = speed;
         speed = null;
         }
         
         if (typeof opacity == 'function') {
         callback = opacity;
         opacity = null;
         }
         
         if (speed < 1) {
         var placeholder = opacity;
         opacity = speed;
         speed = placeholder;
         }
         
         if (opacity >= 1) {
         var placeholder = speed;
         speed = opacity;
         opacity = placeholder;
         }
         
         speed = (speed > 0) ? speed : 500;
         opacity = (opacity > 0) ? opacity : 0.5;
         
         var wH = $(window).height();
         
         var wW = $(window).width();
         
         if (jQuery.browser.msie && jQuery.browser.version.substr(0, 1) == '6') {
         $('select').css({
         display: 'none'
         });
         };
         
         var jq = jQuery('<div></div>').attr({
         id: '__dimScreen_' + $.dimScreenCount,
         fade_opacity: opacity,
         speed: speed
         }).css({
         background: '#000',
         height: wH,
         left: '0px',
         opacity: 0,
         position: 'absolute',
         top: '0px',
         width: wW,
         zIndex: 1000 * $.dimScreenCount
         }).appendTo(document.body).fadeTo(speed, opacity, callback);
         
         $(window).bind('resize.dimmer-overlay_' + $.dimScreenCount, jq, $._dockInClientWindow);
         
         return jq;
         */
        },
        
        /*
     _dockInClientWindow: function(e){
     
     if (e.data) {
     var jq = e.data;
     
     var wH = $(window).height();
     var wW = $(window).width();
     
     jq.css({
     width: wW,
     height: wH
     });
     
     }
     },
     
     */
        //stops current dimming of the screen
        dimScreenStop: function(callback, id)
        {
        
            if (!id) 
            {
                id = '__dim__screen_';
            }
            
            $.DimScreenManager.getInstance().unDimScreen(id, callback);
        /*
         var x = $('#__dimScreen_' + $.dimScreenCount);
         
         //check if is better to throw an exception
         if (!x.length)
         return;
         
         var opacity = x.attr('fade_opacity');
         var speed = parseInt(x.attr('speed'));
         
         x.fadeOut(speed, function(){
         x.remove();
         if (jQuery.browser.msie && jQuery.browser.version.substr(0, 1) == '6') {
         $('select').css({
         display: 'block'
         });
         }
         
         $(window).unbind('resize.dimmer-overlay_' + $.dimScreenCount, $._dockInClientWindow);
         $.dimScreenCount--;
         if (typeof callback == 'function')
         callback();
         });
         */
        }
    });

$.DimScreenManager._Instance = null;
$.DimScreenManager.getInstance = function()
{
    if (!$.DimScreenManager._Instance) 
    {
        $.DimScreenManager._Instance = new $.DimScreenManager();
    }
    return $.DimScreenManager._Instance;
};
