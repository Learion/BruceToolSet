#region

using System;
using System.Collections.Generic;
using SEOToolSet.Entities.Wrappers;

#endregion

namespace SEOToolSet.Entities
{
    [Serializable]
    public class SEOProfile
    {
        protected SEOProfile()
        {
            LastActivityDate = DateTime.Now;
            LastPropertyChangedDate = DateTime.Now;
        }

        public SEOProfile(string applicationName, string userName, ProfileType profileType)
        {
            LastActivityDate = DateTime.Now;
            LastPropertyChangedDate = DateTime.Now;
            ApplicationName = applicationName;
            Name = userName;
            ProfileType = profileType;
        }


        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual DateTime InsertDate { get; set; }

        public virtual DateTime UpdateDate { get; set; }

        public virtual string ApplicationName { get; set; }

        public virtual ProfileType ProfileType { get; set; }

        /// <summary>
        /// Changes when calling SetPropertyValues and GetPropertyValues method
        /// </summary>
        public virtual DateTime LastActivityDate { get; set; }

        /// <summary>
        /// This property differs from the UpdateDate because change only when calling SetPropertyValues method
        /// </summary>
        public virtual DateTime LastPropertyChangedDate { get; set; }

        /// <summary>
        /// List used for cascading rules
        /// </summary>
        protected IList<ProfileProperty> Properties { get; set; }
    }
}