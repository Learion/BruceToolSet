#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers
{
    public abstract class ProjectProviderBase : ProviderBase
    {
        public abstract IList<Project> GetProjectsForUser(string username);
        public abstract IList<Project> GetProjectsForUser(string username, bool? includeInactive);

        public abstract void CreateProject(out Int32 id
                                           , String name
                                           , String domain
                                           , String clientName
                                           , String contactEmail
                                           , String contactName
                                           , String contactPhone
                                           , String createBy
                                           , Account account);
        
        public abstract void UpdateProject(Int32 id
                                           , String name
                                           , String domain
                                           , String clientName
                                           , String contactEmail
                                           , String contactName
                                           , String contactPhone
                                           , bool? enabled 
                                           , String updateBy
                                           , Account account);

        public abstract void DeleteProject(Int32 id);

        public abstract IList<Project> GetProjectsByAccount(Account account, bool? includeInactive);

        public abstract IList<Project> GetProjectsByAccount(Account account, bool? includeInactive, Int32 PageSize,
                                                            Int32 CurrentPage,
                                                            out Int32 Count);

        public abstract IList<Project> FindProjects(Account account, bool? includeInactive, String name, String domain,
                                                    String clientName);

        public abstract IList<Project> FindProjects(Account account, bool? includeInactive, String name, String domain,
                                                    String clientName, Int32 PageSize, Int32 CurrentPage,
                                                    out Int32 Count);

        public abstract void AddUserToProject(out Int32 id, string username, string projectRole, Project project);

        public abstract void UpdateUserProjectRole(Int32 id, string projectRole, bool? monitorEmails);

        public abstract void RemoveUserFromProject(string username, Project project);

        public abstract void CreateKeywordList(out Int32 id
                                               , String name
                                               , Project project);

        public abstract void DeleteKeywordList(Int32 id);

        public abstract IList<KeywordList> GetKeywordLists(Int32 idProject);

        public abstract KeywordList GetKeywordListById(Int32 id);

        public abstract Keyword GetKeywordById(Int32 id);

        public abstract IList<Keyword> GetKeywords(Int32 idKeywrodList);

        public abstract void CreateKeyword(out Int32 id
                                           , String keyword
                                           , KeywordList keywordList);

        public abstract void UpdateKeyword(Int32 id
                                           , String keyword);

        public abstract void DeleteKeyword(Int32 id);

        public abstract Project GetProjectById(int id);

        public abstract void AddCompetitor(out int id, string name, string url, string description, Project project);

        public abstract void DeleteCompetitor(int id);

        public abstract IList<Competitor> GetCompetitorsByIdProject(Int32 idProject);

        public abstract void UpdateKeywordList(int id, string name, bool? enabled);

        public abstract IList<Role> GetProjectsRoles();

        public abstract Role GetProjectRoleById(int id);
        public abstract Role GetProjectRoleByName(string role);
        public abstract void RemoveUserFromProject(int id);

        public abstract IList<ProjectUser> GetUsersInProject(int idProject);


        public abstract void UpdateCompetitor(int id, string name, string url, string description);


        public abstract ProjectUser GetProjectUser(string userName, Project project);

        public abstract IList<SEOToolsetUser> GetUsersNotInProject(int project);

        public abstract void RemoveProjectUserById(int id);

        public abstract bool UserIsAllowedInProject(int idProject, string userName);

        public abstract void CreateKeywordListBulk(int idProject, out int idKeywordlist, string keywordListName,
                                                   string keywords);

        public abstract IList<Project> GetProjectsForUserWithinAccount(string userName, Account accountId, bool? includeInactive);

        public abstract string GetProjectRoleForUser(string userName, Project project);

        public abstract Project GetProjectByAccountAndName(Account account,Project project);
    }
}
