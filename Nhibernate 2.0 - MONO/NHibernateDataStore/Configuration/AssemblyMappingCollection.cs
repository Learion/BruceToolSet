using System.Configuration;

namespace NHibernateDataStore.Configuration
{
    public class AssemblyMappingCollection : ConfigurationElementCollection
    {
        public AssemblyMappingElement this[int index]
        {
            get
            {
                return BaseGet(index) as AssemblyMappingElement;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AssemblyMappingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AssemblyMappingElement)element).Assembly;
        }
    }
}