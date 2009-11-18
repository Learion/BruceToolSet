#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Security;
using LoggerFacade;
using NHibernate;
using NHibernateDataStore;
using NHibernateDataStore.Common;
using SEOToolSet.DAL;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers.NHibernate
{
    public class NHibernateRoleProvider : RoleProvider
    {
        private string _connName;
        private string _providerName;

        public override string Name
        {
            get { return _providerName; }
        }

        private ITransaction BeginTransaction()
        {
            return NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(_connName).BeginTransaction();
        }

        private void LogException(Exception exception, string action)
        {
            Log.Error(GetType(), "Exception on " + action, exception);
        }


        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "NHibernateRoleProvider";

            base.Initialize(name, config);

            _providerName = name;

            _connName = ConfigHelper.ExtractConfigValue(config, "connectionStringName", null);


            if (config.Count == 0) return;

            // Throw an exception if unrecognized attributes remain
            var attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
                throw new ProviderException("Unrecognized attribute: " + attr);
        }

        #region Overrides of RoleProvider

        /// <summary>
        /// Gets or sets the name of the application to store and retrieve role information for.
        /// </summary>
        /// <returns>
        /// The name of the application to store and retrieve role information for.
        /// </returns>
        public override string ApplicationName { get; set; }

        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <returns>
        /// true if the specified user is in the specified role for the configured applicationName; otherwise, false.
        /// </returns>
        /// <param name="username">The user name to search for.</param>
        /// <param name="roleName">The role to search in.</param>
        public override bool IsUserInRole(string username, string roleName)
        {
            try
            {
                var ds = DSSEOToolsetUser.Create(_connName);

                var user = ds.FindByRolAndName(username, roleName);

                return (user != null);
            }
            catch (Exception ex)
            {
                LogException(ex, "IsUserInRole");
                return false;
            }
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for the configured applicationName.
        /// </returns>
        /// <param name="username">The user to return a list of roles for.</param>
        public override string[] GetRolesForUser(string username)
        {
            try
            {
                var ds = DSSEOToolsetUser.Create(_connName);

                var user = ds.FindByName(username);

                var output = new string[1];
                output[0] = user.UserRole.Name;
                return output;
            }
            catch (Exception ex)
            {
                LogException(ex, "IsUserInRole");
                return null;
            }
        }

        /// <summary>
        /// Adds a new role to the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        public override void CreateRole(string roleName)
        {
            var tran = BeginTransaction();
            try
            {
                var dsUserRole = DSRole.Create(_connName);

                if (dsUserRole.FindByName(roleName) != null) throw new DuplicatedEntityException("RoleName already Exists");
                //IdRoleType = 2 ---> User Roles
                var role = new Role { Name = roleName, Description = roleName, IdRoleType = 2, Configurable = true, Enabled = true };

                dsUserRole.Insert(role);

                tran.Commit();
            }
            catch (Exception ex)
            {
                LogException(ex, "CreateRole");
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Removes a role from the data source for the configured applicationName.
        /// </summary>
        /// <returns>
        /// true if the role was successfully deleted; otherwise, false.
        /// </returns>
        /// <param name="roleName">The name of the role to delete.</param>
        /// <param name="throwOnPopulatedRole">If true, throw an exception if <paramref name="roleName" /> has one or more members and do not delete <paramref name="roleName" />.</param>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            var tran = BeginTransaction();
            try
            {
                var dsUserRole = DSRole.Create(_connName);

                dsUserRole.DeleteByNameAndType(roleName, 2); //2 is the IdRoleType for User Roles
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, ex.Source);
                tran.Rollback();
                if (throwOnPopulatedRole) throw;
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <returns>
        /// true if the role name already exists in the data source for the configured applicationName; otherwise, false.
        /// </returns>
        /// <param name="roleName">The name of the role to search for in the data source.</param>
        public override bool RoleExists(string roleName)
        {
            var dsUserRole = DSRole.Create(_connName);
            var role = dsUserRole.FindByName(roleName);
            return role != null && role.IdRoleType == 2;
        }

        /// <summary>
        /// Adds the specified user names to the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be added to the specified roles. </param>
        /// <param name="roleNames">A string array of the role names to add the specified user names to.</param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            Check.Ensure(roleNames.Length > 0, "At least one role must be provided");
            Check.Ensure(usernames.Length > 0, "At least one username must be provided");

            var tran = BeginTransaction();
            try
            {
                var dsUser = DSSEOToolsetUser.Create(_connName);
                var dsRole = DSRole.Create(_connName);

                for (var i = 0; i < usernames.Length; i++)
                {
                    var user = dsUser.FindByName(usernames[i]);
                    if (user == null) continue;
                    var role = dsRole.FindByNameAndtype(roleNames[0], 2);
                    if (role != null)
                        user.UserRole = role;
                    dsUser.Update(user);
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                LogException(ex, ex.Source);
                tran.Rollback();
                throw;
            }
        }


        /// <summary>
        /// Removes the specified user names from the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be removed from the specified roles. </param>
        /// <param name="roleNames">A string array of role names to remove the specified user names from.</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            Check.Ensure(roleNames.Length > 0, "At least one role must be provided");
            Check.Ensure(usernames.Length > 0, "At least one username must be provided");

            var tran = BeginTransaction();
            try
            {
                var dsUser = DSSEOToolsetUser.Create(_connName);

                for (var i = 0; i < usernames.Length; i++)
                {
                    var user = dsUser.FindByName(usernames[i]);

                    if (user == null) continue;
                    if (user.UserRole.Name == roleNames[0])
                    {
                        user.UserRole = null;
                    }
                    dsUser.Update(user);
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                LogException(ex, ex.Source);
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Gets a list of users in the specified role for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the users who are members of the specified role for the configured applicationName.
        /// </returns>
        /// <param name="roleName">The name of the role to get the list of users for.</param>
        public override string[] GetUsersInRole(string roleName)
        {
            var ds = DSRole.Create(_connName);
            var userRole = ds.FindByName(roleName);

            if (userRole.IdRoleType != 2)
                return new string[] { };

            var users = userRole.SEOToolsetUser;

            var list = new List<String>();

            foreach (var user in users)
            {
                list.Add(user.Login);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data source for the configured applicationName.
        /// </returns>
        public override string[] GetAllRoles()
        {
            var ds = DSRole.Create(_connName);
            var roles = ds.FindAllByType(2);

            var list = new List<String>();
            foreach (var role in roles)
            {
                list.Add(role.Name);
            }
            return list.ToArray();
        }

        /// <summary>
        /// Gets an array of user names in a role where the user name contains the specified user name to match.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the users where the user name matches <paramref name="usernameToMatch" /> and the user is a member of the specified role.
        /// </returns>
        /// <param name="roleName">The role to search in.</param>
        /// <param name="usernameToMatch">The user name to search for.</param>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var dsUser = DSSEOToolsetUser.Create(_connName);

            var users = dsUser.FindUsersInRoleWithNameLike(usernameToMatch, roleName);

            var list = new List<String>();

            foreach (var user in users)
            {
                list.Add(user.Login);
            }

            return list.ToArray();
        }

        #endregion
    }
}