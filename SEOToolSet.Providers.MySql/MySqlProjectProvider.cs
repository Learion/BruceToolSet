using System;
using System.Collections.Generic;
using System.Text;
using SEOToolSet.DAL;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System.Configuration;
using SEOToolSet.Entities;

namespace SEOToolSet.Providers.MySql
{
    public class MySqlProjectProvider : ProjectProviderBase
    {
        private string _connName = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
        private string _providerName = "MySqlProjectProvider";

        public override string Name
        {
            get { return _providerName; }
        }

        //public override void Initialize(string name, NameValueCollection config)
        //{
        //    if (config == null)
        //        throw new ArgumentNullException("config");

        //    if (name.Length == 0)
        //        name = "ProjectProvider";

        //    base.Initialize(name, config);

        //    _providerName = name;

        //    _connName = ConfigHelper.ExtractConfigValue(config, "connectionStringName", null);

        //    if (config.Count == 0)
        //        return;

        //    // Throw an exception if unrecognized attributes remain
        //    var attr = config.GetKey(0);
        //    if (!String.IsNullOrEmpty(attr))
        //        throw new ProviderException("Unrecognized attribute: " + attr);
        //}
        public override IList<SEOToolSet.Entities.Project> GetProjectsForUser(string username)
        {
            var ds = DSMySqlProject.Create(_connName);
            return ds.FindByUser(username, null);
        }

        public override IList<SEOToolSet.Entities.Project> GetProjectsForUser(string username, bool? includeInactive)
        {
            var ds = DSMySqlProject.Create(_connName);
            return ds.FindByUser(username, includeInactive);
        }

        public override void CreateProject(out int id, string name, string domain, string clientName, string contactEmail, string contactName, string contactPhone,string createBy, SEOToolSet.Entities.Account account)
        {
            //using (var tran = new TransactionScope(_connName))
            //{
                var ds = DSMySqlProject.Create(_connName);

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

                //TODO:Save createby field
                if (createBy != null)
                {
                    ce.CreatedBy = createBy;
                }

                ds.Insert(ce);

                //tran.Commit();

                id = ce.Id;
            //}

        }

        public override void UpdateProject(int id, string name, string domain, string clientName, string contactEmail, string contactName, string contactPhone, bool? enabled,string updateBy, SEOToolSet.Entities.Account account)
        {
            var ds = DSMySqlProject.Create(_connName);

            var ce = new Project();

            ce.Id = id;

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
            if (enabled.HasValue)
            {
                ce.Enabled = enabled;
            }
            //TODO: Check if we require to throw an exception if Account is null
            if (account != null)
                ce.Account = account;

            //TODO:Save createby field
            if (updateBy != null)
            {
                ce.UpdatedBy = updateBy;
            }

            ds.Update(ce);

            //tran.Commit();
        }

        public override void DeleteProject(int id)
        {
            var ds = DSMySqlProject.Create(_connName);
            ds.SetDisable(id);
        }

        public override IList<SEOToolSet.Entities.Project> GetProjectsByAccount(SEOToolSet.Entities.Account account, bool? includeInactive)
        {
            return new List<SEOToolSet.Entities.Project>();
        }

        public override IList<SEOToolSet.Entities.Project> GetProjectsByAccount(SEOToolSet.Entities.Account account, bool? includeInactive, int PageSize, int CurrentPage, out int Count)
        {
            throw new NotImplementedException();
        }

        public override IList<SEOToolSet.Entities.Project> FindProjects(SEOToolSet.Entities.Account account, bool? includeInactive, string name, string domain, string clientName)
        {
            throw new NotImplementedException();
        }

        public override IList<SEOToolSet.Entities.Project> FindProjects(SEOToolSet.Entities.Account account, bool? includeInactive, string name, string domain, string clientName, int PageSize, int CurrentPage, out int Count)
        {
            throw new NotImplementedException();
        }

        public override void AddUserToProject(out int id, string username, string projectRole, SEOToolSet.Entities.Project project)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUserProjectRole(int id, string projectRole, bool? monitorEmails)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUserFromProject(string username, SEOToolSet.Entities.Project project)
        {
            throw new NotImplementedException();
        }

        public override void CreateKeywordList(out int id, string name, SEOToolSet.Entities.Project project)
        {
            throw new NotImplementedException();
        }

        public override void DeleteKeywordList(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<SEOToolSet.Entities.KeywordList> GetKeywordLists(int idProject)
        {
            var ds = DSMySqlKeywordList.Create(_connName);
            return ds.FindByIdProject(idProject);
        }

        public override SEOToolSet.Entities.KeywordList GetKeywordListById(int id)
        {
            throw new NotImplementedException();
        }

        public override SEOToolSet.Entities.Keyword GetKeywordById(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<SEOToolSet.Entities.Keyword> GetKeywords(int idKeywrodList)
        {
            throw new NotImplementedException();
        }

        public override void CreateKeyword(out int id, string keyword, SEOToolSet.Entities.KeywordList keywordList)
        {
            throw new NotImplementedException();
        }

        public override void UpdateKeyword(int id, string keyword)
        {
            throw new NotImplementedException();
        }

        public override void DeleteKeyword(int id)
        {
            throw new NotImplementedException();
        }

        public override SEOToolSet.Entities.Project GetProjectById(int id)
        {
            var ds = DSMySqlProject.Create(_connName);
            return ds.FindByKey(id);
        }

        public override void AddCompetitor(out int id, string name, string url, string description, SEOToolSet.Entities.Project project)
        {
            throw new NotImplementedException();
        }

        public override void DeleteCompetitor(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<SEOToolSet.Entities.Competitor> GetCompetitorsByIdProject(int idProject)
        {
            var ds = DSMySqlCompetitor.Create(_connName);

            return ds.FindByIdProject(idProject);
        }

        public override void UpdateKeywordList(int id, string name, bool? enabled)
        {
            throw new NotImplementedException();
        }

        public override IList<SEOToolSet.Entities.Role> GetProjectsRoles()
        {
            throw new NotImplementedException();
        }

        public override SEOToolSet.Entities.Role GetProjectRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public override SEOToolSet.Entities.Role GetProjectRoleByName(string role)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUserFromProject(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<SEOToolSet.Entities.ProjectUser> GetUsersInProject(int idProject)
        {
            throw new NotImplementedException();
        }

        public override void UpdateCompetitor(int id, string name, string url, string description)
        {
            throw new NotImplementedException();
        }

        public override SEOToolSet.Entities.ProjectUser GetProjectUser(string userName, SEOToolSet.Entities.Project project)
        {
            throw new NotImplementedException();
        }

        public override IList<SEOToolSet.Entities.SEOToolsetUser> GetUsersNotInProject(int project)
        {
            throw new NotImplementedException();
        }

        public override void RemoveProjectUserById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool UserIsAllowedInProject(int idProject, string userName)
        {
            throw new NotImplementedException();
        }

        public override void CreateKeywordListBulk(int idProject, out int idKeywordlist, string keywordListName, string keywords)
        {
            throw new NotImplementedException();
        }

        public override IList<SEOToolSet.Entities.Project> GetProjectsForUserWithinAccount(string userName, SEOToolSet.Entities.Account accountId, bool? includeInactive)
        {
            var ds = DSMySqlProject.Create(_connName);
            return ds.FindByUserAndAccount(userName,accountId,includeInactive);
        }

        public override string GetProjectRoleForUser(string userName, SEOToolSet.Entities.Project project)
        {
            return "ProjectAdmin";
        }

        public override Project GetProjectByAccountAndName(Account account, Project project)
        {
            var ds = DSMySqlProject.Create(_connName);
            return ds.FindProjectByAccountAndName(account, project);
        }
    }
}
