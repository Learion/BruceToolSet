using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using NHibernateDataStore.Common;

namespace SEOToolSet.Providers.NHibernate
{
    public class NHibernateProjectUserProfileProvider : ProjectUserProfileProviderBase, IRequiresSessionState
    {
        private const string ProjectUserProfilesPropertyName = "ProjectUserProfileCollection";

        #region Properties

        public string ProviderName { get; set; }

        public string ApplicationName { get; set; }

        #endregion

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (string.IsNullOrEmpty(name))
                name = "NHibernateProjectUserProfileProvider";

            base.Initialize(name, config);


            ProviderName = name;
            ApplicationName = ExtractConfigValue(config, "applicationName", ConnectionParameters.DEFAULT_APP); //System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath

            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0)
            {
                var attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new System.Configuration.Provider.ProviderException("Unrecognized attribute: " +
                                                                              attr);
            }
        }

        private static string ExtractConfigValue(System.Collections.Specialized.NameValueCollection config, string key, string defaultValue)
        {
            var val = config[key];
            if (val == null)
                return defaultValue;
            config.Remove(key);

            return val;
        }

        public override ProjectUserProfile GetProfileByProject(int idProject)
        {
            var userProfile = GetProjectUserProfiles();
            if (userProfile.ProjectUserProfiles == null)
                userProfile.ProjectUserProfiles = new List<ProjectUserProfile>();
            var projectUserProfiles = userProfile.ProjectUserProfiles;
            Predicate<ProjectUserProfile> predicate =
                projectUserProfile => projectUserProfile.IdProject == idProject;
            return projectUserProfiles.Exists(
                      predicate)
                ? projectUserProfiles.Find(predicate)
                : null;
        }

        public override void SetProjectProfile(ProjectUserProfile projectUserProfile)
        {
            var userProfile = GetProjectUserProfiles();
            var projectUserProfiles = userProfile.ProjectUserProfiles;
            if (!projectUserProfiles.Exists(pup => pup.IdProject == projectUserProfile.IdProject))
                projectUserProfiles.Add(projectUserProfile);
            HttpContext.Current.Profile[ProjectUserProfilesPropertyName] = userProfile;
            HttpContext.Current.Profile.Save();
        }

        public override string GetRankingMonitorReportDefaultEngines(int idProject)
        {
            var userProfile = GetProjectUserProfiles();
            var project = userProfile.ProjectUserProfiles.Find(projectUserProfile => projectUserProfile.IdProject == idProject);
            return project == null
                ? null
                : project.jSonDefaultEngines;
        }

        public override void SetRankingMonitorReportDefaultEngines(int idProject, string engines)
        {
            if (HttpContext.Current.Profile.IsAnonymous) return;
            var userProfile = GetProjectUserProfiles();
            var project = userProfile.ProjectUserProfiles.Find(projectUserProfile => projectUserProfile.IdProject == idProject);
            if (project == null)
            {
                userProfile.ProjectUserProfiles.Add(new ProjectUserProfile { IdProject = idProject, jSonDefaultEngines = engines });
            }
            else
            {
                project.jSonDefaultEngines = engines;
                userProfile.ProjectUserProfiles[userProfile.ProjectUserProfiles.IndexOf(project)] = project;
            }
            HttpContext.Current.Profile[ProjectUserProfilesPropertyName] = userProfile;
            HttpContext.Current.Profile.Save();
        }

        public override string GetRankingMonitorConfiguration(int idProject)
        {
            var userProfile = GetProjectUserProfiles();
            var project = userProfile.ProjectUserProfiles.Find(pup => pup.IdProject == idProject);
            return project == null ? null : project.configuration;
        }

        public override void SetRankingMonitorConfiguration(int idProject, string configuration)
        {
            var userProfile = GetProjectUserProfiles();
            var project = userProfile.ProjectUserProfiles.Find(pup => pup.IdProject == idProject);
            if (project == null)
            {
                userProfile.ProjectUserProfiles.Add(new ProjectUserProfile
                    {
                        IdProject = idProject,
                        configuration = configuration
                    });
            }
            else
            {
                project.configuration = configuration;
                userProfile.ProjectUserProfiles[userProfile.ProjectUserProfiles.IndexOf(project)] = project;
            }
            HttpContext.Current.Profile[ProjectUserProfilesPropertyName] = userProfile;
            HttpContext.Current.Profile.Save();
        }

        private static ProjectUserProfileCollection GetProjectUserProfiles()
        {
            /*var session = Context.Session;
            var projectUserprofiles = (ProjectUserProfileCollection)session[ProjectUserProfilesPropertyName];
            if (projectUserprofiles == null)
            { */
            var projectUserprofiles = (ProjectUserProfileCollection)HttpContext.Current.Profile[ProjectUserProfilesPropertyName];
            //session[ProjectUserProfilesPropertyName] = projectUserprofiles;
            /*}*/
            return projectUserprofiles;
        }
    }
}
