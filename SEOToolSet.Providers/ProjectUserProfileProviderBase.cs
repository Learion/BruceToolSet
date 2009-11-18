#region Using Directives

using System.Configuration.Provider;

#endregion

namespace SEOToolSet.Providers
{
    public abstract class ProjectUserProfileProviderBase : ProviderBase
    {
        public abstract ProjectUserProfile GetProfileByProject(int idProject);

        public abstract void SetProjectProfile(ProjectUserProfile projectUserProfile);

        public abstract string GetRankingMonitorReportDefaultEngines(int project);

        public abstract void SetRankingMonitorReportDefaultEngines(int idProject, string engines);

        public abstract string GetRankingMonitorConfiguration(int idProject);

        public abstract void SetRankingMonitorConfiguration(int idProject, string configuration);
    }
}