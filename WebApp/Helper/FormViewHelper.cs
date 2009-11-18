using System;

namespace SEOToolSet.WebApp.Helper
{
    public class FormViewHelper
    {
        public static String GetCountryNameById(Int32 id, string defaultId)
        {
            var country = Providers.SEOMembershipManager.GetCountryById(id);
            return (country != null) ? country.Name : defaultId;
        }
    }
}
