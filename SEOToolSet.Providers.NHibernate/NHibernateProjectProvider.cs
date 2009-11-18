#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Threading;
using NHibernateDataStore.Common;
using SEOToolSet.DAL;
using SEOToolSet.Entities;
using SEOToolSet.Providers.Exceptions;

#endregion

namespace SEOToolSet.Providers.NHibernate
{
    public class NHibernateProjectProvider : ProjectProviderBase
    {
        private string _connName;
        private string _providerName;

        public override string Name
        {
            get { return _providerName; }
        }


        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "ProjectProvider";

            base.Initialize(name, config);

            _providerName = name;

            _connName = ConfigHelper.ExtractConfigValue(config, "connectionStringName", null);

            if (config.Count == 0)
                return;

            // Throw an exception if unrecognized attributes remain
            var attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
                throw new ProviderException("Unrecognized attribute: " + attr);
        }

        #region Overrides of ProjectProviderBase

        public override IList<Project> GetProjectsForUser(string username)
        {
            return GetProjectsForUser(username, false);
        }

        public override IList<Project> GetProjectsForUser(string userName, bool? includeInactive)
        {
            var ds = DSProject.Create(_connName);
            return ds.FindByUser(userName, includeInactive);
        }

        public override void CreateProject(out int id, String name,
                                           string domain, string clientName,
                                           string contactEmail, string contactName,
                                           string contactPhone, string createBy, Account account)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSProject.Create(_connName);

                var ce = new Project();

                if (domain != null)
                    ce.Domain = domain;
                if (name != null)
                    ce.Name = name;
                if (clientName != null)
                    ce.ClientName = clientName;
                if (contactEmail != null)
                    ce.ContactEmail = contactEmail;
                if (contactName != null)
                    ce.ContactName = contactName;
                if (contactPhone != null)
                    ce.ContactPhone = contactPhone;
                ce.Enabled = true;
                //TODO: Check if we require to throw an exception if Account is null
                if (account != null)
                    ce.Account = account;
                if (createBy != null)
                {
                    ce.CreatedBy = createBy;
                }

                ds.Insert(ce);

                tran.Commit();

                id = ce.Id;
            }
        }

        public override void UpdateProject(int id, string name, string domain, string clientName, string contactEmail,
                                           string contactName, string contactPhone, bool? enabled, string updateBy, Account account)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSProject.Create(_connName);

                var ce = ds.FindByKey(id);

                if (domain != null)
                    ce.Domain = domain;
                if (name != null)
                    ce.Name = name;
                if (clientName != null)
                    ce.ClientName = clientName;
                if (contactEmail != null)
                    ce.ContactEmail = contactEmail;
                if (contactName != null)
                    ce.ContactName = contactName;
                if (contactPhone != null)
                    ce.ContactPhone = contactPhone;
                if (enabled != null)
                    ce.Enabled = enabled;
                if (account != null)
                    ce.Account = account;
                if (updateBy != null)
                {
                    ce.UpdatedBy = updateBy;
                }
                ds.Update(ce);

                tran.Commit();
            }
        }

        public override void DeleteProject(int id)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSProject.Create(_connName);
                ds.SetDisable(id);
                tran.Commit();
            }
        }

        public override IList<Project> GetProjectsByAccount(
            Account account,
            bool? includeInactive)
        {
            var ds = DSProject.Create(_connName);

            return ds.FindByAccount(account, includeInactive);
        }

        public override IList<Project> GetProjectsByAccount(Account account, bool? includeInactive, int PageSize,
                                                            int CurrentPage, out int Count)
        {
            throw new NotImplementedException();
        }

        public override IList<Project> FindProjects(Account account, bool? includeInactive, string name, string domain,
                                                    string clientName)
        {
            throw new NotImplementedException();
        }

        public override IList<Project> FindProjects(Account account, bool? includeInactive, string name, string domain,
                                                    string clientName, int PageSize, int CurrentPage, out int Count)
        {
            throw new NotImplementedException();
        }

        public override void AddUserToProject(out int id, string username, string projectRole, Project project)
        {
            var user = SEOMembershipManager.GetUser(username);
            var projectRoleObj = GetProjectRoleByName(projectRole);

            if (projectRoleObj == null) throw new ProjectRoleNameMustExistException("the project role must exist " + projectRole);
            if (projectRoleObj.IdRoleType != (Int32)RoleType.ProjectRole) throw new ProjectRoleNameMustExistException("the project role must exist " + projectRole);

            using (var tran = new TransactionScope(_connName))
            {
                var projectUser = new ProjectUser
                                      {
                                          Project = project,
                                          ProjectRole = projectRoleObj,
                                          SEOToolsetUser = user,
                                          Enabled = true,
                                          MonitorEmails = false
                                      };

                var ds = DSProjectUser.Create(_connName);

                ds.Insert(projectUser);
                tran.Commit();

                id = projectUser.Id;
            }
        }

        public override Role GetProjectRoleByName(string role)
        {
            var ds = DSRole.Create(_connName);
            return ds.FindByName(role);
        }

        public override void UpdateUserProjectRole(Int32 id, string projectRole, bool? monitorEmails)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSProjectUser.Create(_connName);

                var projectUser = ds.FindByKey(id);

                if (projectUser != null)
                {
                    if (projectRole != null)
                        projectUser.ProjectRole = GetProjectRoleByName(projectRole);
                    if (monitorEmails != null)
                        projectUser.MonitorEmails = monitorEmails;
                }


                ds.Update(projectUser);

                tran.Commit();
            }
        }

        public override void RemoveUserFromProject(Int32 id)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSProjectUser.Create(_connName);

                ds.Delete(id);

                tran.Commit();
            }
        }

        public override IList<ProjectUser> GetUsersInProject(int idProject)
        {
            var ds = DSProjectUser.Create(_connName);
            return ds.FindByIdProject(idProject);
        }

        public override void UpdateCompetitor(int id, string name, string url, string description)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSCompetitor.Create(_connName);

                var competitor = ds.FindByKey(id);

                if (competitor == null)
                    throw new Exception("Competitor Not Found");
                if (name != null)
                    competitor.Name = name;
                if (url != null)
                    competitor.Url = url;
                if (description != null)
                    competitor.Description = description;

                ds.Update(competitor);

                tran.Commit();
            }
        }

        public override void RemoveUserFromProject(string username, Project project)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSProjectUser.Create(_connName);
                var projectUser = ds.FindByUserNameAndProject(username, project);

                if (projectUser == null)
                    return;
                ds.Delete(projectUser);
                tran.Commit();
            }
        }

        public override void CreateKeywordList(out int id, string name, Project project)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSKeywordList.Create(_connName);

                var ce = new KeywordList();

                if (name != null)
                    ce.Name = name;
                if (project != null)
                    ce.Project = project;


                ds.Insert(ce);

                tran.Commit();

                id = ce.Id;
            }
        }

        public override void UpdateKeywordList(int id, string name, bool? enabled)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSKeywordList.Create(_connName);

                var ce = ds.FindByKey(id);

                if (name != null)
                    ce.Name = name;
                if (enabled != null)
                    ce.Enabled = enabled;

                ds.Update(ce);

                tran.Commit();
            }
        }

        public override IList<Role> GetProjectsRoles()
        {
            var ds = DSRole.Create(_connName);
            return ds.FindAll();
        }

        public override Role GetProjectRoleById(int id)
        {
            var ds = DSRole.Create(_connName);
            return ds.FindByKey(id);
        }

        public override void DeleteKeywordList(int id)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSKeywordList.Create(_connName);

                ds.DeleteIncludingChilds(id);

                tran.Commit();
            }
        }

        public override IList<KeywordList> GetKeywordLists(int idProject)
        {
            var ds = DSKeywordList.Create(_connName);
            return ds.FindByIdProject(idProject);
        }

        public override KeywordList GetKeywordListById(int id)
        {
            var ds = DSKeywordList.Create(_connName);
            return ds.FindByKey(id);
        }

        public override Keyword GetKeywordById(int id)
        {
            var ds = DSKeyword.Create(_connName);
            return ds.FindByKey(id);
        }

        public override IList<Keyword> GetKeywords(int idKeywrodList)
        {
            return GetKeywordListById(idKeywrodList).Keyword;
        }

        public override void CreateKeyword(out int id, string keyword, KeywordList keywordList)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSKeyword.Create(_connName);

                var ce = new Keyword();

                if (keyword != null)
                    ce.Keyword = keyword;
                if (keywordList != null)
                    ce.KeywordList = keywordList;


                ds.Insert(ce);

                tran.Commit();

                id = ce.Id;
            }
        }

        public override void UpdateKeyword(int id, string keyword)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSKeyword.Create(_connName);

                var ce = ds.FindByKey(id);

                if (keyword != null)
                    ce.Keyword = keyword;

                ds.Update(ce);

                tran.Commit();
            }
        }

        public override void DeleteKeyword(int id)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSKeyword.Create(_connName);

                ds.Delete(id);

                tran.Commit();
            }
        }

        public override Project GetProjectById(int id)
        {
            var ds = DSProject.Create(_connName);
            return ds.FindByKey(id);
        }

        public override void AddCompetitor(out int id, string name, string url, string description, Project project)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSCompetitor.Create(_connName);

                var ce = new Competitor();

                if (name != null)
                    ce.Name = name;
                if (url != null)
                    ce.Url = url;
                if (description != null)
                    ce.Description = description;
                if (project != null)
                    ce.Project = project;

                ds.Insert(ce);

                tran.Commit();

                id = ce.Id;
            }
        }

        public override IList<Competitor> GetCompetitorsByIdProject(int idProject)
        {
            var ds = DSCompetitor.Create(_connName);

            return ds.FindByIdProject(idProject);
        }

        public override void DeleteCompetitor(int id)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSCompetitor.Create(_connName);

                ds.Delete(id);

                tran.Commit();
            }
        }

        public override ProjectUser GetProjectUser(string userName, Project project)
        {
            var ds = DSProjectUser.Create(_connName);
            return ds.FindByUserNameAndProject(userName, project);
        }

        public override IList<SEOToolsetUser> GetUsersNotInProject(int idProject)
        {
            var ds = DSSEOToolsetUser.Create(_connName);

            var dsProject = DSProject.Create(_connName);

            var project = dsProject.FindByKey(idProject);

            return ds.FindUsersNotInProject(idProject, project.Account.Id);
        }

        public override void RemoveProjectUserById(int id)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSProjectUser.Create(_connName);
                ds.Delete(id);
                tran.Commit();
            }
        }

        public override bool UserIsAllowedInProject(int idProject, string userName)
        {
            var project = DSProject.Create(_connName).FindByKey(idProject);
            return project != null && project.Enabled != false &&
                DSProjectUser.Create(_connName).FindByUserNameAndProject(userName, project) != null;
        }

        public override void CreateKeywordListBulk(int idProject, out int idKeywordlist, string keywordListName, string keywords)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var dsProject = DSProject.Create(_connName);
                var project = dsProject.FindByKey(idProject);

                CreateKeywordList(out idKeywordlist, keywordListName, project);

                var dsKl = DSKeywordList.Create(_connName);

                var keywordList = dsKl.FindByKey(idKeywordlist);

                var kList = keywords.Split('\n');

                foreach (var keyword in kList)
                {
                    if (String.IsNullOrEmpty(keyword))
                        continue;
                    int idKeyword;
                    CreateKeyword(out idKeyword, keyword.Trim(), keywordList);
                }

                tran.Commit();
            }
        }

        public override IList<Project> GetProjectsForUserWithinAccount(string userName, Account account, bool? includeInactive)
        {
            return DSProject.Create(_connName).FindByUserAndAccount(userName, account, includeInactive);
        }

        public override string GetProjectRoleForUser(string userName, Project project)
        {
            var user = SEOMembershipManager.GetUser(userName);
            var dsProjectUser = DSProjectUser.Create(_connName);
            if (project == null) return null;
            var projectUser = dsProjectUser.FindByIdProjectAndIdUser(project.Id, user.Id);

            if (projectUser != null) return projectUser.ProjectRole != null ? projectUser.ProjectRole.Name : null;
            return null;
        }

        public override Project GetProjectByAccountAndName(Account account, Project project)
        {
            return null;
        }

        #endregion
    }


}
