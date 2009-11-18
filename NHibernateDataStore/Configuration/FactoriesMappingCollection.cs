#region Using Directives

using System.Configuration;

#endregion

namespace NHibernateDataStore.Configuration
{
    public class FactoriesMappingCollection : ConfigurationElementCollection
    {
        public FactoryMappingElement this[int index]
        {
            get { return base.BaseGet(index) as FactoryMappingElement; }
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
            return new FactoryMappingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FactoryMappingElement) element).ConnectionStringName;
        }
    }
}