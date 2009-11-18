#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers
{
    public static class ProjectManager
    {
        private static readonly ProjectProviderBase _defaultProvider;

        private static readonly ProjectProviderCollection _providerCollection = new ProjectProviderCollection();

        static ProjectManager()
        {
            var providerConfiguration =
                ConfigurationManager.GetSection("ProjectProvider") as ProjectProviderConfiguration;

            if (providerConfiguration == null || providerConfiguration.DefaultProvider == null ||
                providerConfiguration.Providers == null || providerConfiguration.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for ProjectProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(providerConfiguration.Providers, _providerCollection,
                                                 typeof(ProjectProviderBase));
            _providerCollection.SetReadOnly();
            _defaultProvider = _providerCollection[providerConfiguration.DefaultProvider];

            if (_defaultProvider != null) return;

            PropertyInformation defaultProviderProp =
                providerConfiguration.ElementInformation.Properties["defaultProvider"];

            if (defaultProviderProp != null)
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the ProjectProvider.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            throw new ConfigurationErrorsException("You must specify a default Provider for the ProjectProvider");
        }

        #region ProviderMethods

        public static Project GetProjectById(int id)
        {
            return Provider.GetProjectById(id);
        }

        public static IList<Project> GetProjectsForUser(string username)
        {
            return Provider.GetProjectsForUser(username);
        }

        public static IList<Project> GetProjectsForUser(string username, bool? includeInactive)
        {
            return Provider.GetProjectsForUser(username, includeInactive);
        }

        public static void CreateProject(out Int32 id
                                         , String name
                                         , String domain
                                         , String clientName
                                         , String contactEmail
                                         , String contactName
                                         , String contactPhone
                                         , String createBy    
                                         , Account account)
        {
            Provider.CreateProject(out id, name, domain, clientName, contactEmail, contactName, contactPhone,createBy, account);
        }

        public static void UpdateProject(Int32 id
                                         , String name
                                         , String domain
                                         , String clientName
                                         , String contactEmail
                                         , String contactName
                                         , String contactPhone
                                         , bool? enabled
                                         , String updateBy
                                         , Account account)
        {
            Provider.UpdateProject(id, name, domain, clientName, contactEmail, contactName, contactPhone, enabled, updateBy,
                                   account);
        }

        public static void DeleteProject(Int32 id)
        {
            Provider.DeleteProject(id);
        }

        public static void AddCompetitor(out Int32 id
                                         , String name
                                         , String url
                                         , String description
                                         , Project project)
        {
            Provider.AddCompetitor(out id, name, url, description, project);
        }

        public static void UpdateCompetitor(Int32 id
                                            , String name
                                            , String url
                                            , String description
            )
        {
            Provider.UpdateCompetitor(id, name, url, description);
        }

        public static IList<Project> GetProjectsByAccount(Account account, bool? includeInactive)
        {
            return Provider.GetProjectsByAccount(account, includeInactive);
        }

        public static IList<Project> GetProjectsByAccount(Account account, bool? includeInactive, Int32 PageSize,
                                                          Int32 CurrentPage,
                                                          out Int32 Count)
        {
            return Provider.GetProjectsByAccount(account, includeInactive, PageSize, CurrentPage, out Count);
        }

        public static IList<Project> FindProjects(Account account, bool? includeInactive, string name, String domain,
                                                  String clientName)
        {
            return Provider.FindProjects(account, includeInactive, name, domain, clientName);
        }

        public static IList<Project> FindProjects(Account account, bool? includeInactive, string name, String domain,
                                                  String clientName, Int32 PageSize, Int32 CurrentPage,
                                                  out Int32 Count)
        {
            return Provider.FindProjects(account, includeInactive, name, domain, clientName, PageSize, CurrentPage,
                                         out Count);
        }

        public static void AddUserToProject(out Int32 id, string username, string projectRole, Project project)
        {
            Provider.AddUserToProject(out id, username, projectRole, project);
        }

        public static void UpdateUserProjectRole(int id, string projectRole, bool? monitorEmails)
        {
            Provider.UpdateUserProjectRole(id, projectRole, monitorEmails);
        }

        public static void RemoveUserFromProject(string username, Project project)
        {
            Provider.RemoveUserFromProject(username, project);
        }

        public static void CreateKeywordList(out Int32 id
                                             , String name
                                             , Project project)
        {
            Provider.CreateKeywordList(out id, name, project);
        }


        public static void UpdateKeywordList(Int32 id
                                             , String name, bool? enabled)
        {
            Provider.UpdateKeywordList(id, name, enabled);
        }

        public static void DeleteKeywordList(Int32 id)
        {
            Provider.DeleteKeywordList(id);
        }

        public static IList<KeywordList> GetKeywordLists(Int32 idProject)
        {
            return Provider.GetKeywordLists(idProject);
        }

        public static KeywordList GetKeywordListById(Int32 id)
        {
            return Provider.GetKeywordListById(id);
        }

        public static Keyword GetKeywordById(Int32 id)
        {
            return Provider.GetKeywordById(id);
        }

        public static IList<Keyword> GetKeywords(Int32 idKeywrodList)
        {
            return Provider.GetKeywords(idKeywrodList);
        }

        public static void CreateKeyword(out Int32 id
                                         , String keyword
                                         , KeywordList keywordList)
        {
            Provider.CreateKeyword(out id, keyword, keywordList);
        }

        public static void UpdateKeyword(Int32 id
                                         , String keyword)
        {
            Provider.UpdateKeyword(id, keyword);
        }

        public static void DeleteKeyword(Int32 id)
        {
            Provider.DeleteKeyword(id);
        }

        public static IList<Role> GetProjectRoles()
        {
            return Provider.GetProjectsRoles();
        }

        public static Role GetProjectRoleById(int idProjectRole)
        {
            return Provider.GetProjectRoleById(idProjectRole);
        }

        public static IList<Competitor> GetCompetitorsByProjectId(int idProject)
        {
            return Provider.GetCompetitorsByIdProject(idProject);
        }


        public static void CreateKeywordListBulk(int idProject, out int idKeywordList, string keywordListName, string keywords)
        {
            Provider.CreateKeywordListBulk(idProject, out idKeywordList, keywordListName, keywords);
        }


        #endregion

        public static ProjectProviderBase Provider
        {
            get
            {
                if (_defaultProvider != null) return _defaultProvider;
                throw new ProviderException("You must specify a valid default provider for ProjectProvider.");
            }
        }

        public static ProjectProviderCollection Providers
        {
            get { return _providerCollection; }
        }

        public static IList<ProjectUser> GetUsersInProject(Int32 idProject)
        {
            return Provider.GetUsersInProject(idProject);
        }

        public static IList<SEOToolsetUser> GetUsersNotInProject(Int32 idProject)
        {
            return Provider.GetUsersNotInProject(idProject);
        }

        public static void DeleteCompetitor(int id)
        {
            Provider.DeleteCompetitor(id);
        }

        public static ProjectUser GetProjectUser(string userName, Project project)
        {
            return Provider.GetProjectUser(userName, project);
        }

        public static void RemoveProjectUserById(int idProject)
        {
            Provider.RemoveProjectUserById(idProject);
        }

        public static bool UserIsAllowedInProject(int result, string name)
        {
            return Provider.UserIsAllowedInProject(result, name);
        }

        public static IList<Project> GetProjectsForUserWithinAccount(string userName, Account account, bool? includeInactive)
        {
            return Provider.GetProjectsForUserWithinAccount(userName, account, includeInactive);
		}
        public static string GetProjectRoleForUser(string name, int idProject)
        {
            return Provider.GetProjectRoleForUser(name, GetProjectById(idProject));
        }

        public static Project GetProjectByAccountAndName(Account account, Project project)
        {
            return Provider.GetProjectByAccountAndName(account, project);
        }
    }
}
