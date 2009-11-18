using System;
using System.Web;

namespace SEOToolSet.WebApp.Helper
{
    public class ProfileHelper
    {
        public static Int32 SelectedIdProject
        {
            get
            {
                return (HttpContext.Current.Profile["IdProjectSelected"] is int) ? (int)HttpContext.Current.Profile["IdProjectSelected"] : 0;
            }
            set
            {
                HttpContext.Current.Profile["IdProjectSelected"] = value;
                HttpContext.Current.Profile.Save();
            }
        }
    }
}
