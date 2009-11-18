namespace SEOToolSet.Entities.Wrappers
{
    public class StatusWrapper
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public static implicit operator StatusWrapper(Status status)
        {
            return status == null
                       ? null
                       : new StatusWrapper
                             {
                                 Name = status.Name,
                                 Description = status.Description
                             };
        }
    }
}