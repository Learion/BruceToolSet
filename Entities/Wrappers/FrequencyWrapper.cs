namespace SEOToolSet.Entities.Wrappers
{
    public class FrequencyWrapper
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static implicit operator FrequencyWrapper(Frequency frequency)
        {
            if (frequency == null) return null;
            return new FrequencyWrapper
                       {
                           Id = frequency.Id,
                           Name = frequency.Name
                       };
        }
    }
}