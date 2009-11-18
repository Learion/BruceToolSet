<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="MainMenu.ascx.cs" Inherits="SEOToolSet.WebApp.MainMenu" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>

<div class="MainMenu">
    <ul>
        <asp:Repeater ID="MenuRepeater" OnItemDataBound="OnMenuItemDataBound" runat="server">
            <ItemTemplate>
                <li id="liItem" runat="server"><a title='<%# Eval("Title") %>' class='<%# MarkAsCurrent(Container.DataItem) %>'
                    href='<%# (Container.DataItem) != null? ((SiteMapNode)Container.DataItem)["MenuUrl"] ?? Eval("Url") : "#"  %>'>
                    <span class="Left"><span class="Right"><span class="Center">
                        <%# ((String)( (Container.DataItem) != null? ((SiteMapNode)Container.DataItem)["MenuTitle"]  : Eval("Title"))).Trim().Replace(" ","&nbsp;") %>
                    </span></span></span></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

<script type="text/javascript">
    $(function() {
        /*
        var centerSpanCol = $('a.button.MenuItem span.Center');                
        centerSpanCol.hide();
        centerSpanCol.each(function () {            
            var centerSpan = $(this);
            centerSpan.html($.trim(centerSpan.html()).replace(/\s/g,'&nbsp;'));            
        });
        centerSpanCol.show();*/
		
		function setSizes(cUL) {
			if (!cUL || !$.browser.msie || cUL.data('sized')) return							
			    cUL.data('sized',true);												
			    var uls = cUL.find('ul');        
			    uls.each(function (i, ele) {				
				    var max_width = 0;
				    $(this)
					    .find('>li')
					    .each(function () {
						    var w = $(this).outerWidth();
						    if (w > max_width) {
							    max_width = w;
						    }
					    })
					    .width(max_width);
			    });           
		}
		
		
        
        var currentUL = $('.SubMenu>ul:visible');        
        setSizes(currentUL);
        var timerToShowSubMenu = null;
        
        if (currentUL.length > 0) {        
            $('a.button.MenuItem')
                .each(function (i, val) {
                    $(this).data('_index',i);
                })
                .hover(function() {   
						var ele = $(this);
						clearInterval(timerToShowSubMenu);
						timerToShowSubMenu = setTimeout(function () {              
							$('.SubMenu>ul').hide();
							var cUL = $('.SubMenu>ul:eq('+ ele.data('_index') + ')')
										.show();	
							setSizes(cUL);														
						}, 400);
				}, function () {                
                    
				});
				
			var t = null;
                
            $('.SubMenu')
                .hover(function () {
					clearTimeout(t);
                    clearInterval(timerToShowSubMenu);
                }, function () {
                    
                });
            
            
            $('#hd .Menu').hover(function () {}
            , function () {
                clearTimeout(t);
                t = setTimeout(function () {
                    $('.SubMenu>ul').hide();                
                    currentUL.show();
                    
                }, 500);
            });
        }
        
        
        $('a.button.MenuItem, .SubMenu li a').each(function () {
            var current = $(this);
            current.attr('href',current.attr('href').replace(/\?s*m/g,'') );
        });
        
       
        
        
        $('a.not_available, a[is_valid=not_available]').click(function() {
            $.showBigDialog('<%= GetGlobalResourceObject("CommonTerms","DenyAccess") %>', { icon: $.MessageIco.Warning, title: '<%= GetGlobalResourceObject("CommonTerms","DenyAccessTitle") %>', showCancel: false });
            return false;
        });
    });
</script>

