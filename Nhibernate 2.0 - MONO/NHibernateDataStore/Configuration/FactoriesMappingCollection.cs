
using System;
using System.Configuration;


namespace NHibernateDataStore.Configuration
{
    public class FactoriesMappingCollection : ConfigurationElementCollection
    {
        public FactoryMappingElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as FactoryMappingElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new FactoryMappingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FactoryMappingElement)element).ConnectionStringName;
        }
    }
}