#region Using Directives

using System;
using System.Collections.Specialized;

#endregion

namespace SEOToolSetReportServices.ReportEngine
{
    public class ReportService : IReportService
    {
        #region IReportService Members

        public virtual string getReport(NameValueCollection parameters)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}