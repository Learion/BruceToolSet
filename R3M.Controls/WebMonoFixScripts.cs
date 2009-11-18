#region Using Directives

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using R3M.Controls.Properties;

#endregion

namespace R3M.Controls
{
    [ToolboxData("<{0}:WebMonoFixScripts runat=server></{0}:WebMonoFixScripts>")]
    public class WebMonoFixScripts : WebControl
    {
        public String JsFile { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            if (!Common.IsMono) return;
            Common.AddFileToPageHeader(Page, ResolveClientUrl(JsFile ?? Settings.Default.MonoWebFormJS),
                                       FileType.Javascript);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            String template;
            if (Common.IsMono)
            {
                template =
                    @"
<script type='text/javascript'>
//<![CDATA[                                                                                                
      $(document).ready(function () {                                    
       if (typeof(WebForm_DoPostback) == 'undefined') {
            if (typeof (WebForm_Initialize) != 'undefined') 
                WebForm_Initialize(window);                                    
       }
	
       if (!$.browser.msie) return;
 	   
		// Attach a handler to the load event.
	   if (typeof(Sys) != 'undefined') 
	   {
            var _monoApplicationHandler = function(sender, args) 
			{			   
			   if (typeof (window.Page_ClientValidate) == 'function') 
			   {	                                                                             
			        var old_pcv = window.Page_ClientValidate;			         

			        window.Page_ClientValidate = function(group) 
					{	
                         		             
			             window.ValidatorSetFocus = function(val, event) 
                         {
			                 return;
			             }		
                         
			             old_pcv(group);								
			         }        	
			    } 
			};
			Sys.Application.add_load(_monoApplicationHandler);            
            return; // Ajax present so no need to use the next override for the problematic function
		}                      

       if (typeof (window.WebForm_DoPostBackWithOptions) == 'function') {	
           window.old_wdp = window.WebForm_DoPostBackWithOptions;
           
           window.WebForm_DoPostBackWithOptions = function(options) { 			                                        
               
               window.overridenValidatorFocus = true;
               window.OldValidatorSetFocus = window.ValidatorSetFocus;		
               window.ValidatorSetFocus = function(val, event) {
                   try {
                       if (Sys.WebForms.PageRequestManager.getInstance()) {
                           return;
                       }
                   }
                   catch(e) {}
                   window.OldValidatorSetFocus(val, event);
               }
           
               window.old_wdp(options);								
           }        	
      }	  		
   });
 
//]]>
</script>";

                writer.Write(template);
            }

            template =
                @"

<script type='text/javascript'>
//<![CDATA[                                                                                                
      $(document).ready(function () { 
        var _validatorAjaxFix = function(sender, args) 
		{                               
		    window.ValidatorUpdateDisplay = function(v, valid) {                
                var aux = $.byId(v.id);
                if (aux.length == 0) return;                                                           
				var display = v.display;                            
				
				if (display == 'None') {            
				    return;
				}            
                valid = valid || v.isvalid;

				//aux[0].style.visibility = (valid == true ? 'hidden' : 'visible');
                aux.css({ visibility : (valid == true ? 'hidden' : 'visible') });
				if (display == 'Dynamic') {
				    //aux.get(0).style.display = (valid == true ? 'none' : 'inline');                        
                    if (!valid) {
                        aux.fadeIn('slow').css({ display:'inline' });
                    }
                    else {
                        aux.fadeOut('slow').css({ display:'none' });
                    }
				}
			}			
	    };

        if (typeof(Sys) != 'undefined') 
		{
		    Sys.Application.add_load(_validatorAjaxFix); 
            //if not is ie we need to call the _validatorAjaxFix manually because the load_event was raised before and the function won't be called
            if (!$.browser.msie) _validatorAjaxFix();
        }
        else {
            _validatorAjaxFix();            
        }
			
	      
	  });
//]]>
</script>
";

            writer.Write(template);
        }
    }
}