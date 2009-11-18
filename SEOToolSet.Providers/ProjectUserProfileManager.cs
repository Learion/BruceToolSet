#region Using Directives

using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

#endregion

namespace SEOToolSet.Providers
{
    public class ProjectUserProfileManager
    {
        private static readonly ProjectUserProfileProviderBase _defaultProvider;

        private static readonly ProjectUserProfileProviderCollection _providerCollection =
            new ProjectUserProfileProviderCollection();

        static ProjectUserProfileManager()
        {
            var providerConfiguration =
                ConfigurationManager.GetSection("ProjectUserProfileProvider") as ProjectUserProfileProviderConfiguration;

            if (providerConfiguration == null || providerConfiguration.DefaultProvider == null ||
                providerConfiguration.Providers == null || providerConfiguration.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for ProjectUserProfileProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(providerConfiguration.Providers, _providerCollection,
                                                 typeof (ProjectUserProfileProviderBase));
            _providerCollection.SetReadOnly();
            _defaultProvider = _providerCollection[providerConfiguration.DefaultProvider];

            if (_defaultProvider != null) return;

            PropertyInformation defaultProviderProp =
                providerConfiguration.ElementInformation.Properties["defaultProvider"];

            if (defaultProviderProp != null)
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the ProjectUserProfileProvider.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            throw new ConfigurationErrorsException(
                "You must specify a default Provider for the ProjectUserProfileProvider");
        }

        public static ProjectUserProfileProviderBase Provider
        {
            get
            {
                if (_defaultProvider != null) return _defaultProvider;
                throw new ProviderException("You must specify a valid default provider for ProjectUserProfileProvider.");
            }
        }

        public static ProjectUserProfileProviderCollection Providers
        {
            get { return _providerCollection; }
        }

        public static ProjectUserProfile GetProfileByProject(int idProject)
        {
            return Provider.GetProfileByProject(idProject);
        }

        public static void SetProjectProfile(ProjectUserProfile projectUserProfile)
        {
            Provider.SetProjectProfile(projectUserProfile);
        }

        public static void SetRankingMonitorReportDefaultEngines(int idProject, string engines)
        {
            Provider.SetRankingMonitorReportDefaultEngines(idProject, engines);
        }

        public static string GetRankingMonitorReportDefaultEngines(int idProject)
        {
            return Provider.GetRankingMonitorReportDefaultEngines(idProject);
        }

        public static void SetRankingMonitorConfiguration(int idProject, string configuration)
        {
            Provider.SetRankingMonitorConfiguration(idProject, configuration);
        }

        public static string GetRankingMonitorConfiguration(int idProject)
        {
            return Provider.GetRankingMonitorConfiguration(idProject);
        }
    }
}