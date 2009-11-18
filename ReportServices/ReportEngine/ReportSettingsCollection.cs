#region Using Directives

using System.Configuration;

#endregion

namespace SEOToolSetReportServices.ReportEngine
{
    public class ReportSettingsCollection : ConfigurationElementCollection
    {
        #region Overrides of ConfigurationElementCollection

        public ReportMappingElement this[int index]
        {
            get { return BaseGet(index) as ReportMappingElement; }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new ReportMappingElement this[string name]
        {
            get { return BaseGet(name) as ReportMappingElement; }
        }

        /// <summary>
        ///                     When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        ///                     A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ReportMappingElement();
        }

        /// <summary>
        ///                     Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <returns>
        ///                     An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        /// <param name="element">
        ///                     The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for. 
        ///                 </param>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ReportMappingElement) element).Name;
        }

        #endregion
    }
}