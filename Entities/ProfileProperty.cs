namespace SEOToolSet.Entities
{
    public class ProfileProperty
    {
        protected ProfileProperty()
        {
        }

        public ProfileProperty(SEOProfile pUser, string pName)
        {
            User = pUser;
            Name = pName;
        }

        public virtual int Id { get; protected set; }

        public virtual SEOProfile User { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string StringValue { get; protected set; }

        public virtual byte[] BinaryValue { get; protected set; }

        public virtual void SetValue(byte[] val)
        {
            StringValue = null;
            BinaryValue = val;
        }

        public virtual void SetValue(string val)
        {
            BinaryValue = null;
            StringValue = val;
        }

        public virtual void SetNull()
        {
            BinaryValue = null;
            StringValue = null;
        }
    }
}