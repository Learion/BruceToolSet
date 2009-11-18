#region Using Directives

using System.Collections.Generic;
using System.Xml.Serialization;

#endregion

namespace SEOToolSet.Providers
{
    public class ProjectUserProfileCollection
    {
        private List<ProjectUserProfile> _projectUserProfiles = new List<ProjectUserProfile>();

        [XmlElement(typeof (ProjectUserProfile))]
        public List<ProjectUserProfile> ProjectUserProfiles
        {
            get { return _projectUserProfiles; }
            set { _projectUserProfiles = value; }
        }
    }
}