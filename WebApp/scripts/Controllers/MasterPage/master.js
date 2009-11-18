$(document)
    .ready(function() {                        
        //$(document).pngFix();
        //$('div.PanelRounded').roundPanel();            

        //search for this elements in the page and set the tab index property in the order they're found in the page
        //TODO: check if it is working wrong in some place. 
        //If a desired tabIndex order is needed use options.excludeSelector to filter the items that should be excluded from the autotab
        $('a, :input, .TabMePlease').fixTabIndex();
        
        //Disable selection over buttons, 
        //TODO: Check if it is possible to include this instruction inside an external js
        $('a.button').disableTextSelection();
        
        //Make the .CenterWrapper divs' width equal to the either the sum of the widths of the buttons it contains or equals to 
        //the first children element width. For the first case the desired structure is <div class="CenterWrapper"><ul><li></li><li></li></ul></div>
        $('.CenterWrapper').fitToChildrenWidth();                       
            
        //This plugin should be applied to a div that internally has a gridview or table or some kind of repetitive list of items, 
        //that need to have the checkAll/CheckNone Behavior
        //TODO: check if would be necessary to raise events of this kind CheckedState/CheckedAll/CheckedNone         
        $('.GridView').checkAllBehavior();          
    });
        
//OnDomReady Events are raised the first time the page loads, and every time an ASP.NET ajax call modifies the DOM
$.onDomReady(function() {
    $('input.Disabled').disableFocus();
});