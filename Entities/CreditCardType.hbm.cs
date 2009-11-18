#region

using System;

#endregion

namespace SEOToolSet.Entities
{
    [Serializable]
    public class AbstractCreditCardType
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Url { get; set; }
    }

    [Serializable]
    public class CreditCardType : AbstractCreditCardType
    {
    }
}