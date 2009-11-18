<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgressMonitorAjaxRequest.ascx.cs"
    Inherits="SEOToolSet.WebApp.Controls.ProgressMonitorAjaxRequest" %>
<%@ Register Src="~/Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<uc1:IncludeFile ID="IncludeFile2" TypeOfFile="Css" FilePath="~/css/jquery.jgrowl.css"
    runat="server" />
<uc1:IncludeFile ID="IncludeFile3" TypeOfFile="Javascript" FilePath="~/scripts/plugins/jquery.jgrowl.js"
    runat="server" />

<script type="text/javascript"> 
//TODO: remove the ClientID from this functions, this control must only call a function that register the plugin with the page using an external js
var entre_<%=ClientID %> = 0; //in firefox the add_beginRequest and add_pageLoaded Methods register the handler twice!!!!!!!!!!!
var timeout_<%=ClientID %> = null;
function beginRequest_<%=ClientID %>(send, args){
     clearTimeout(timeout_<%=ClientID %>);
    timeout_<%=ClientID %> = setTimeout(function () {
     $.jGrowl('<asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:JavascriptMessages, RetrievingData %>"></asp:Localize>',
        { 
            header: '<asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:CommonTerms, Loading %>"></asp:Localize>...',
            sticky: true, 
            easing: 'swing',
            closer: false,
            check : 1000
        });
     }, <%= (TimeOut > 0) ? TimeOut : 500 %>);
}

function endRequest_<%=ClientID %>(send, args){     
    clearTimeout(timeout_<%=ClientID %>);
    $.jGrowlHide();                                     
    
}
                                             
$(document).ready(        
    function(){        
        if (entre_<%=ClientID %>++ == 0) { 
            $()
            .ajaxError(endRequest_<%=ClientID %>)
            .ajaxStart(beginRequest_<%=ClientID %>)
            .ajaxStop(endRequest_<%=ClientID %>);
            
            try {
                if(Sys && Sys.WebForms.PageRequestManager) Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequest_<%=ClientID %>);        
                if(Sys && Sys.WebForms.PageRequestManager) Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest_<%=ClientID %>);
            }
            catch(e) {
                alert("[<%= GetType().FullName %>] " + e.message);
            }
        }
    }
);
</script>

