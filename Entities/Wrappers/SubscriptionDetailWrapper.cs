namespace SEOToolSet.Entities.Wrappers
{
    ///<summary>
    ///Wrap the subscription detail entity
    ///</summary>
    public class SubscriptionDetailWrapper
    {
        public int Id { get; set; }

        public string PropertyValue { get; set; }

        public string SubscriptionPropertyName { get; set; }

        public  static implicit operator SubscriptionDetailWrapper(SubscriptionDetail detail)
        {
            return detail == null ? null : new SubscriptionDetailWrapper
                                               {
                                                   Id = detail.Id,
                                                   PropertyValue = detail.PropertyValue,
                                                   SubscriptionPropertyName = detail.SubscriptionProperty.Name
                                               };
        }
    }
}
