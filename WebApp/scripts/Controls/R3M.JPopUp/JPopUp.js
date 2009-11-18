jQuery.extend(
    {
        MessageIco:
            {
                Alert: 'Alert',
                Info: 'Info',
                Error: 'Error',
                Warning: 'Warning',
                Confirm: 'Confirm',
                Wait: 'Wait'
            },

        /**
        * Hide the element with the Id, using a fadeOut effect
        * (This function is used to hide an element that was first shown using showPopUp, do nothing instead)
        */
        hidePopUp: function(callback) {
            $.hideOverlay(callback);
        },
        /**
        *
        * @param {Object} panel
        */
        centerInScreen: function(panel) {
            var w = $(window);
            var win_width = w.width();
            var scrollToLeft = w.scrollLeft();
            var win_height = w.height();

            panel.css(
                {
                    left: (((win_width / 2 - panel.width() / 2)) + scrollToLeft) + 'px',
                    top: (((win_height / 2 - panel.height() / 2)) + $(document).scrollTop()) + 'px',
                    position: 'absolute',
                    'z-index': $.DimScreenManager.getInstance().getLastZIndex() + 5
                });
        },
        keepInCenter: function(panel, str_id) {
            var eventResize = 'resize' + ((str_id) ? ('.' + str_id) : '');
            var eventScroll = 'scroll' + ((str_id) ? ('.' + str_id) : '');

            $(window).bind(eventResize, function(e) {
                $.centerInScreen(panel);
            }).bind(eventScroll, function(e) {
                $.centerInScreen(panel);
            });
        },
        hideMessage: function(callback) {
            $.hideOverlay(callback);
        },

        hideOverlay: function(callback) {
            $.overlays = $.overlays || [];
            var dlg = $.overlays.pop();
            if (dlg) {
                $(window)
					.unbind('resize.' + dlg.attr('id'))
					.unbind('scroll.' + dlg.attr('id'))
					.unbindEnterPressed();

                dlg.fadeOut(100, function() {
                    var cback = function () {
                        var onHide = dlg.data('opts').onHide;
                        if (typeof (onHide) == 'function') {
                            onHide();
                        }
                        if (typeof (callback) == 'function') {
                            callback();
                        }
                    };
                    
                    if (dlg.data('opts').modal && $.dimScreenStop) {
                        $.dimScreenStop(cback, dlg.attr('id'));
                    }
                    else {
                        cback();
                    }
                   
                });
            }
        },

        showOverlayDialog: function(content, opts) {
            var options =
                {
                    width: 380,
                    height: null,
                    modal: true,
                    beforeShow: null,
                    onShow: null,
                    onHide: null,
                    useEscToHide: true,
                    draggable: true,
                    closeable: true,
                    title: '',
                    containerTemplate: '<div class="PopupPanel" style="display:none"></div>',
                    wrapperTemplate: "<div class='p_hd dlg-dragHandler'> <div class='p_hd_lt'> </div> <div class='p_hd_rt'> </div> <div class='p_hd_c'> <p class='dlg-title'> :.</p> <a title='{CLOSE}' href='javascript:void(0);' class='dlg-closePopUp ClosePopUpBtn '><span>{CLOSE}</span></a> </div> </div> <div class='round_ctr'> <div class='bl'> <div class='br'> <div class='bd'> <div class='p_bd dlg-content'> </div> </div> </div> </div> <div class='ft'> <div class='fl'> </div> <div class='fr'> </div> <div class='fc'> </div> </div> </div>",
                    resizeable: false,
                    extraClass: ''
                };

            $.extend(options, opts);

            $.overlays = $.overlays || [];

            var jContent = $(content);

            var overlayId = $.stringFormat('r3m_overlay_{0}', jContent.attr('id'));

            var dlg = $.byId(overlayId);

            if (dlg.length === 0) {

                dlg = $(options.containerTemplate);
                dlg
					.attr('id', overlayId)
					.appendTo($('form.MainForm'));

                jContent.hide();

                if (options.wrapperTemplate) {
                    options.wrapperTemplate = options.wrapperTemplate.replace(/\{CLOSE\}/g, $.getResourceString("CloseCommand", "Close"));
                }

                var ctn = $(options.wrapperTemplate || '<div class="dlg-content"></div>');

                dlg.append(ctn).find('a.dlg-closePopUp').click(function() {
                    $.hideOverlay();
                    return false;
                });
                ctn
					.find('div.dlg-content')
					.append(jContent);

                jContent.show();
            }
            dlg
				.data('opts', options)
				.css({
				    height: options.height,
				    width: options.width
				});

            try {
                if (!options.extraClass) {
                    dlg[0].className = 'PopupPanel';
                }
                else {
                    dlg[0].className = 'PopupPanel ' + options.extraClass;
                }
            }
            catch (e) {
                console.log('error : ' + e.message);
            }

            if (options.draggable) {
                dlg.draggable(
                    {
                        handle: '.dlg-dragHandler',
                        opacity: 0.5
                    });
            }
            if (options.resizeable) {
                dlg.find('div.dlg-content').resizable();
            }
            if (!options.closeable) {
                dlg.find('a.ClosePopUpBtn').hide();
            }
            else {
                dlg.find('a.ClosePopUpBtn').show();
            }

            if (options.useEscToHide) {
                $(window.document).enterPressed(function(e,args) { }, function(e, args) {
                    $.hideOverlay();
                    args.doBlur = true;
                    return true;
                }, null, 'dialog');
            }
            else {
                $(window.document)
					.unbindEnterPressed();
            }

            $.centerInScreen(dlg);

            $.keepInCenter(dlg, dlg.attr('id'));

            if (typeof options.beforeShow == 'function') {
                options.beforeShow(dlg);
            }

            var raiseOnShow = function() {
                dlg.focus().find('p.dlg-title').html(options.title);

                if (typeof (options.onShow) == 'function') {
                    options.onShow(dlg);
                }

                //this will apply the center calculations for the buttons.
                dlg.find('.CenterWrapper').fitToChildrenWidth({
                    excess: $.JPopUpDefaultExcess || 10
                });

                $('a.button').disableTextSelection();

            };

            if (options.modal) {
                if ($.dimScreen) {
                    $.dimScreen(300, 0.3, function() {
                        dlg.show().css('z-index', $.DimScreenManager.getInstance().getLastZIndex() + 5);
                        raiseOnShow();
                    }, dlg.attr('id'));
                }
                else {
                    dlg.fadeIn(300, raiseOnShow);
                }
            }
            else {
                dlg.fadeIn(300, raiseOnShow);
            }

            $.overlays.push(dlg);
        },


        /* USAGE
        <script type="text/javascript">
        $(function() {
        $.showDialog("hola new Dialog",
        { onCommand: function(args) { console.log('command received ' + args.command); if (args.command == 'Restore_Defaults') alert('restoring defaults'); },
        buttons: [{ text: "Restore Defaults", command: "Restore_Defaults", cssClass: "Restore" }]
        });
        });
        </script>
        */
        showDialog: function(str_msg, options) {
            var opts =
                {
                    title: ":.",
                    useEscToHide: false,
                    closeable: false,
                    id: "_dialog_window",
                    buttons: null,
                    onCommand: null,
                    showOk: true,
                    showCancel: true,
                    icon: $.MessageIco.Info,
                    defaultButtonsBefore: true
                };

            $.extend(opts, options);

            var useButtons = [];

            if (opts.showOk) {
                useButtons.push(
                    {
                        text: $.getResourceString("Ok", "Ok"),
                        command: "Ok",
                        cssClass: "Ok"
                    });
            }
            if (opts.showCancel) {
                useButtons.push(
                    {
                        text: $.getResourceString("Cancel", "Cancel"),
                        command: "Cancel",
                        cssClass: "Cancel"
                    });
            }

            if (opts.buttons && opts.buttons.length > 0) {
                if (opts.defaultButtonsBefore) {
                    useButtons = opts.buttons.concat(useButtons);
                }
                else {
                    useButtons = useButtons.concat(opts.buttons);
                }
            }

            var buttons_html = "<div class='CenterWrapper DialogCommands'><ul> {BUTTONS_HTML}	</ul><div style='clear:both;'></div></div> ";
            var singlebutton_html = "<li><a href='#' class='button short {CSS_CLASS}' style='{CSS_STYLE}' command='{COMMAND}' ><span class='Left'><span class='Right'><span class='Center'> {TEXT}</span></span></span></a></li>";

            var currentButtons = "";
            for (var ix = 0; ix < useButtons.length; ix++) {
                var item = useButtons[ix];
                currentButtons += singlebutton_html
									.replace("{CSS_CLASS}", item.cssClass || "")
									.replace("{COMMAND}", item.command || "noCommand")
									.replace("{TEXT}", item.text)
									.replace("{CSS_STYLE}", item.cssStyle || "");
            }

            buttons_html = buttons_html.replace("{BUTTONS_HTML}", currentButtons);
            var old_onShow = opts.onShow || function() { };
            opts.onShow = function(panel) {
                panel.find('a.button').click(function(e) {
                    var args =
                            {
                                doOnDialogClose: function() { }
                            };
                    if (opts.onCommand) {
                        args.command = $(this).attr('command');
                        opts.onCommand(args);
                    }
                    $.hideMessage(args.doOnDialogClose);
                    return false;
                });

                old_onShow(panel);
            };

            $.showMessage(str_msg + buttons_html, opts);
        },

        showConfirm: function(str_msg, options) {
            var opts =
                {
                    title: $.getResourceString("PleaseConfirm", "Please Confirm"),
                    useEscToHide: false,
                    closeable: false,
                    id: "_confirm_message",
                    OkText: $.getResourceString("Ok", "Ok"),
                    CancelText: $.getResourceString("Cancel", "Cancel"),
                    onOkClicked: null,
                    onCancelClicked: null,
                    icon: $.MessageIco.Confirm,
                    defaultButton : 'Ok'
                };

            $.extend(opts, options);
            var buttons = "<div class='DialogCommands'><ul><li><a href='#' class='button short Cancel'><span class='Left'><span class='Right'><span class='Center'>{Cancel}</span></span></span></a></li><li><a href='#' class='button short Ok'><span class='Left'><span class='Right'><span class='Center'>{Ok}</span></span></span></a></li></ul><div style='clear:both;'></div></div>";
            buttons = buttons.replace("{Ok}", opts.OkText);
            buttons = buttons.replace("{Cancel}", opts.CancelText);

            var msg = '<div class="dlg-message-inner">{0}</div>'.replace('{0}', str_msg) + buttons;

            var old_bs = opts.beforeShow || function() { };
            var odl_show = opts.onShow || function () {};

            opts.beforeShow = function(panel) {
               var ok = panel.find('a.button.short.Ok').click(function() {                   
                    if (opts.onOkClicked) {
                        opts.onOkClicked();
                    }
                    $.hideMessage();
                    return false;
                });

              var cancel = panel.find('a.button.short.Cancel').click(function() {
                    if (opts.onCancelClicked) {
                        opts.onCancelClicked();
                    }
                    $.hideMessage();
                    return false;
                });                                
               

                old_bs(panel);
                
               if (opts.defaultButton == 'Ok') {
                    ok.focus();    
               }
               else {
                    cancel.focus();   
               }

            };
            
            opts.onShow = function (panel) {            
               odl_show(panel);
               
               var ok = panel.find('a.button.short.Ok');
               var cancel = panel.find('a.button.short.Cancel');                                              
               if (opts.defaultButton == 'Ok') {
                    ok.focus();    
               }
               else {
                    cancel.focus();   
               }
            
            }
            
            

            $.showMessage(msg, opts);
        },

        showMessage: function(str_msg, options) {
            var opts =
                {
                    title: ':.',
                    width: 380,
                    height: null,
                    modal: true,
                    onShow: null,
                    onHide: null,
                    allowDrag: true,
                    closeable: true,
                    icon: $.MessageIco.Info,
                    useEscToHide: true,
                    messageTemplate: '<div class="dlg-ico dlg-dragHandler"><div class="dlg-extra"></div><h2 class="dlg-extra-title"></h2></div> <div class="dlg-message"></div><div class="dlg-doClear"></div>'
                };

            $.extend(opts, options);

            var old_beforeShow = opts.beforeShow || function() { };

            var panelId = '__message__panel__';
            var panel = $.byId(panelId);
            if (!panel.length) {
                panel = $($.stringFormat('<div id="{0}" style="display:none"></div>', panelId))
				.appendTo($('form.MainForm'))
				.append($(opts.messageTemplate));
            }

            opts.beforeShow = function(dlg) {
                dlg
					.find('div.dlg-ico')
					.each(function() { this.className = 'dlg-ico dlg-dragHandler Ico ' + opts.icon })
					.end()
					.find('div.dlg-message')
					.html(str_msg);
                old_beforeShow(dlg);
            };

            $.showOverlayDialog(panel, opts);

        },
        showPopUp: function(str_id, options) {
            var oldshow = options.onShow ||
            function() {
            };
            options.onShow = function(dlg) {
                dlg.find('a.ClosePopUp').click(function() {
                    $.hideOverlay();
                    return false;
                });

                dlg.find('input:eq(0)').focus();

                oldshow(dlg);
            }
            $.showOverlayDialog($.byId(str_id), options);
        },

        hideInLineMessage: function(ele) {
            try {
                if (!ele.get(0).opts)
                { return; }
                var divToHide = ele.find('div.' + ele.get(0).opts.className);
                if (divToHide.length > 0 && divToHide.is(':visible')) {
                    divToHide.hide('blind',
                        {
                            direction: "vertical"
                        }, 500);
                }
            }
            catch (ex) {
                console.log(ex);
            }
        },
        showInlineMessage: function(ele, msg, opts) {

            if (!ele || !ele.get(0))
            { return; }

            var options =
                {
                    fadeTimeout: 5000,
                    sticky: false,
                    append: true,
                    className: "InlineErrorMessage",
                    highlight: true,
                    highlightColor: "red"
                };

            $.extend(options, opts);

            var template = '<div style="display:none;" class="' + options.className + '"><div class="InlineWrapper"><p>{Message}</p><a class="CloseInlineMessage"><span>' + $.getResourceString('HideCommand', '[ Hide ]') + '</span><a></div>';

            ele.find('div.' + options.className).remove();

            ele.get(0).opts = options;

            template = template.replace(new RegExp('{Message}', 'g'), msg);
            if (options.append) {
                ele.append(template);
            }
            else {
                ele.html(template);
            }
            ele.find('a.CloseInlineMessage').click(function(evt) {
                $.hideInLineMessage(ele);
            });

            ele.find('div.' + options.className).fadeIn(500, function() {
                if (options.highlight) {
                    $(this).effect("highlight",
                        {
                            color: options.highlightColor
                        }, 500);
                }

            });

            clearInterval($.lastInterval);
            if (!options.sticky) {
                $.lastInterval = setTimeout($.hideInLineMessage, options.fadeTimeout, ele);
            }
        }

    });

