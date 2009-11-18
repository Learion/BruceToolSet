#region Using Directives

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace R3M.Controls
{
    [ToolboxData("<{0}:AjaxCallback runat=server></{0}:AjaxCallback>")]
    public class AjaxCallback : WebControl, ICallbackEventHandler
    {
        private string _result;

        public Boolean UseParentClientIdAsPrefix { get; set; }

        public String ClientMethodName
        {
            get { return ClientID + "_doServerCall"; }
        }

        public String OnClientError { get; set; }

        public String OnClientCallback { get; set; }

        public Boolean NotifyErrors { get; set; }

        #region ICallbackEventHandler Members

        public string GetCallbackResult()
        {
            return _result;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            if (ScriptCallback == null) return;

            var arg = new CallbackEventArgs(eventArgument);
            ScriptCallback(this, arg);

            _result = arg.Result;
        }

        #endregion

        public event EventHandler<CallbackEventArgs> ScriptCallback;

        protected override void Render(HtmlTextWriter writer)
        {
            var scriptsTemplate = GetScripts();

            writer.Write(scriptsTemplate);
        }

        private string GetScripts()
        {
            var scriptsTemplate =
                @"
<script type='text/javascript'>
//<![CDATA[ 

[TRACE_FUNCTION] = function (message, ctx) {
    if ([NOTIFY_ERRORS]) {
			if ((typeof(console) != 'undefined')) 
			{
				console.log('[' + ctx + '] : ' + message);
			}
			else 
			{
				alert('[' + ctx + '] : ' + message);
			}
		} 
}

[ON_CALLBACK] = function (result, context)
{ 
	try {
        var old_i;
		if (typeof(i) != 'undefined')  old_i = i; 
		[ON_CLIENT_CALLBACK_FUNCTION](result, context);
		if (typeof(i) != 'undefined') i = old_i; 
	} 
	catch(e) 
	{        
		[TRACE_FUNCTION](e.message, '[ON_CALLBACK]');
	}    
}; 
[ON_ERROR] = function(message) {      
	if ([NOTIFY_ERRORS]) 
	{
		[ON_CLIENT_ERROR_FUNCTION](message);
	}
}; 

[CLIENT_METHOD_NAME] = function (context , arg) { 
    try {
        __theFormPostData = '';
        __theFormPostData = [];
             
		if (typeof(WebForm_InitCallback) == 'function') {
            WebForm_InitCallback();                
        }			 		
    }
    catch(e) {     
        [TRACE_FUNCTION](e.message, '[CLIENT_METHOD_NAME]');         
    }
    [CALL_TO_SERVER_METHOD]
};
//]]>
</script>";


            var traceFunction = String.Format("{0}_Trace", ClientID);

            scriptsTemplate = scriptsTemplate.Replace("[TRACE_FUNCTION]", traceFunction);

            var onCallback = string.Format("{0}_Callback", ClientID);

            scriptsTemplate = scriptsTemplate.Replace("[ON_CALLBACK]", onCallback);

            var prefix = UseParentClientIdAsPrefix ? Parent.ClientID + "_" : "";

            scriptsTemplate = scriptsTemplate.Replace("[ON_CLIENT_CALLBACK_FUNCTION]",
                                                      string.Format("{0}{1}", prefix, OnClientCallback));

            scriptsTemplate = scriptsTemplate.Replace("[NOTIFY_ERRORS]", NotifyErrors.ToString().ToLowerInvariant());

            var onError = string.Format("{0}_Error", ClientID);

            scriptsTemplate = scriptsTemplate.Replace("[ON_ERROR]", onError);

            scriptsTemplate = scriptsTemplate.Replace("[ON_CLIENT_ERROR_FUNCTION]",
                                                      string.Format("{0}{1}", prefix, OnClientError));

            scriptsTemplate = scriptsTemplate.Replace("[CLIENT_METHOD_NAME]", ClientMethodName);

            scriptsTemplate = scriptsTemplate.Replace("[CALL_TO_SERVER_METHOD]",
                                                      Page.ClientScript.GetCallbackEventReference(this, "arg",
                                                                                                  onCallback, "context",
                                                                                                  onError, true));

            return scriptsTemplate;
        }
    }
}