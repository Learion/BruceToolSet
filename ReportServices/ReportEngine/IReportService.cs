#region Using Directives

using System.Collections.Specialized;

#endregion

namespace SEOToolSetReportServices.ReportEngine
{
    public interface IReportService
    {
        string getReport(NameValueCollection parameters);
    }
}