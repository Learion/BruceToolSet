using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEOToolSet.WebApp
{
    public partial class MonitorTool : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SilverlightReportMonitor.InitParameters = String.Format("ParentHost={0}_SilverLightHost",
                                                                         SilverlightReportMonitor.ClientID);
        }
    }
}
