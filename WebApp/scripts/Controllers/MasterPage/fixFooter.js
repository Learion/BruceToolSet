/**
*  relocate the footer at the bottom of the page when the height of the document is minor than the height of the client window.
*  NOTE: Mozilla tooks a little more of time to apply this patch, causing an ugly flickering to appear. In Mozilla the footer needs to be hidden at first.
*  After the calculation is done, the footer will appear using a fading effect
*/
function _putFooterAtBottomWhenNeeded(useAnimation) {
    
    var $ = jQuery;
    var ft = $('#ft');    
    if (ft.css('position') == 'fixed') { 
        //Try to fix the ugly flickering in Mozilla when the footer is relocated
        if ((!ft.is(':visible'))) ft.fadeIn();
        return;
    }
	var clientHeight = $(window).height();
	
	var documentHeight = $('#mc_container').height();	
	if (documentHeight < clientHeight) {	
		var hdheight = $('#hd').height() || 139;
		//NOTE: take in mind that if the height of the footer changes this default value should also be updated.
		var ftheight = ft.height() || 80;		
		var bdHeight = clientHeight - (ftheight + hdheight);				
		
		if (useAnimation) {
		    $('#bd').animate({ minHeight: bdHeight });
		}
		else {
		    $('#bd')
		        .css({minHeight: bdHeight});;
		}							    					    
	}
	//Try to fix the ugly flickering in Mozilla when the footer is relocated		        
    if ((!ft.is(':visible'))) {         
        ft.fadeIn();		
    }
}

$(function () {
    //this function will try to locate the footer at the bottom of the page when the clientheight > documentHeight
    try {
	    _putFooterAtBottomWhenNeeded();        
	}
	catch(ex) {
	    console.log(ex.message);
	}
	$(window)	        
        .bind('resize.relocateFooter', function () {	             	                     
             //to be sure this method is only executed once per resize event.
             //IE raise up to 3 resize events per actual window resize. one for width, one for height and one per body resize 
             //http://snook.ca/archives/javascript/ie6_fires_onresize/
             //Several fixes were tried but the most we could achieve is only be sure this event will be rised up to 2 times maximum.	             	             
             clearTimeout($.footerInterval);
             $.footerInterval = setTimeout(function () { clearTimeout($.footerInterval); _putFooterAtBottomWhenNeeded(true); }, 100);
	    });
});