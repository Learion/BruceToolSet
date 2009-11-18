/**
 * jQuery brTip plugin
 * This jQuery plugin was inspired and based on various other plugins of tooltip, but this is better =)
 * @name jquery-brtip-1.1.js
 * @author Gabriel Sobrinho - gabriel.sobrinho@gmail.com
 * @version 1.1
 * @date March 04, 2008
 * @category jQuery plugin, User Interface
 * @copyright (c) 2008 Gabriel Sobrinho (gabriel.sobrinho@gmail.com)
 * @license CC Attribution 3.0 Unported - http://creativecommons.org/licenses/by/3.0/deed.en_US
 * @example Visit http://plugins.jquery.com/project/brTip for more informations about this jQuery plugin
 */

/** 
 * Modified By RRRM to include 
 *  
 * 1. option to enable the tooltip to move with the mouse
 * 2. not to render the tooltip container when the title attribute is not set
 * 3. allow to hide an show the tooltip
 * 
 */

(function($) {
    // Alias to jQuery Object.
    $.fn.brTip = function(opts) {
        // Merge default and user options.
        opts = $.extend({
            // Related to animation.
            fadeIn: '',
            fadeOut: '',

            // Related to speed.
            toShow: 100,
            toHide: 0,

            // Related to box.
            opacity: 0.8,
            top: 15,
            left: 15,
            title: 'Help',

            // Don't alter these variables in any way.
            box: null,
            delayToShow: null,
            delayToHide: null,
            txt: '',
            showTitle: false,
            moveWithMouse: true
        }, opts);

        // Clear the timeouts.
        function _clearTimes() {
            clearTimeout(opts.delayToShow);
            clearTimeout(opts.delayToHide);
        }

        // Create the box of brTip.
        function _create() {
            // Clear timeouts.
            _clearTimes();

            if (!opts.box) {
                // Create the box of brTip.
                opts.box = $('<div class="brTip-box"><div class="brTip-title">&nbsp;</div><div class="brTip-content">&nbsp;</div></div>').appendTo(document.body);
                opts.box.css('opacity', opts.opacity);
            }
            if (!opts.showTitle) {
                opts.box.find('div.brTip-title').css("display", "none");
            }
            else {
                // Set content.
                opts.box.find('div.brTip-title').html(opts.title);
            }
            opts.box.find('div.brTip-content').html(opts.txt);

            // Delay to show.

            if (opts.txt === '') {
                return;
            }
            opts.delayToShow = setTimeout(function() {
                opts.box.fadeIn(opts.fadeIn);
            }, opts.toShow);

        }

        // Hide the box of brTip.
        function _hide() {
            _clearTimes();
            opts.delayToHide = setTimeout(function() {
                if (opts.box) {
                    opts.box.fadeOut(opts.fadeOut);
                }
            }, opts.toHide);
        }

        // Set the position of the box of brTip.
        function _setPos(top, left) {
            if (opts.box) {
                left = left >= ($(window).width() - opts.box.width() - opts.left) ? left - opts.box.width() - opts.left : left + opts.left;
                top = top >= ($(window).height() - opts.box.height() - opts.top) ? top - opts.box.height() - opts.top : top + opts.top;

                opts.box.css({
                    top: top,
                    left: left
                });
            }
            else {
                // In no move mouse, check again.
                setTimeout(function() {
                    _setPos(top, left);
                }, 100);
            }
        }
        return this.each(function() {
            // Self is alias to jQuery Object of actual element.
            var self = $(this);

            if (self.attr('tooltip_inited') == 'true') return;

            if (self.attr('title')) {
                self.attr('Jtitle', self.attr('title'));
                self.attr('title', '');

                // Set events.
                self.bind('mouseenter', function(e) {
                    // Set content.
                    if (self.attr('show_tooltip') == 'true') {

                        opts.txt = self.attr('Jtitle');
                        _create();
                        _setPos(e.pageY, e.pageX);
                    }
                    else {
                        _clearTimes();
                        if (opts.box) {
                            opts.box.css('display', 'none');
                        }
                    }
                }).bind('mouseleave', function(e) {
                    // Restore content.
                    //self.attr('title', opts.txt);                                        

                    opts.txt = '';

                    // Hide the box.
                    _hide();
                }).mousemove(function(e) {
                    if (opts.moveWithMouse && self.attr('show_tooltip') == 'true') {
                        _setPos(e.pageY, e.pageX);
                    }
                }).focus(function() {
                    self.trigger('mouseleave');

                    // Set the pos based on element pos.
                    var pos = self.offset();
                    _setPos(pos.top + (self.width() / 2), pos.left + (self.height() / 2));
                }).blur(function() {
                    self.trigger('mouseout');
                });

                self.attr('tooltip_inited', 'true');
            }
        });
    };
}
(jQuery));
