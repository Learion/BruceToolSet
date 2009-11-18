using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using WebDemo.code;

namespace WebDemo
{
    using System;
    using System.Web;

    public partial class SiteMaster : MasterPage , WebDemo.code.IErrorMessage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            sectionError.Visible = false;

            Page.Title = "Eucalypto - " + Page.Title;
        }

        public void SetError(Type context, string message)
        {
            Eucalypto.LoggerFacade.Log.Error(context, message);
            sectionError.Text = message;

            sectionError.Visible = true;
        }

        public void SetError(Type context, Exception ex)
        {
            //Don't log ThreadAbortException because is fired each time a Redirect is called
            if ((ex is System.Threading.ThreadAbortException)) return;

            Eucalypto.LoggerFacade.Log.Error(context, "Error", ex);

            if (ex is HttpUnhandledException && ex.InnerException != null)
                sectionError.Text = Utilities.FormatException(ex.InnerException);
            else
                sectionError.Text = Utilities.FormatException(ex);

            sectionError.Visible = true;
        }
    }

}
