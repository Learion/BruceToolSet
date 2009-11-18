using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.News
{
    [Serializable]
    public class ItemNotFoundException : EucalyptoException
    {
        public ItemNotFoundException(string id)
            : base("Item " + id + " not found")
        {

        }
    }

    [Serializable]
    public class NewsCategoryNotFoundException : EucalyptoException
    {
        public NewsCategoryNotFoundException(string id)
            : base("News category " + id + " not found")
        {

        }
    }

    [Serializable]
    public class CategoryNotSpecifiedException : EucalyptoException
    {
        public CategoryNotSpecifiedException()
            : base("Categories list not specified")
        {

        }
    }
}
