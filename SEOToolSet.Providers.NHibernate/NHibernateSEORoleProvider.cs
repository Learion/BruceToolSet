#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using NHibernateDataStore;
using NHibernateDataStore.Common;
using SEOToolSet.DAL;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers.NHibernate
{
    public class NHibernateSEORoleProvider : SEORoleProviderBase
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
                name = "NHibernateSEORoleProvider";

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



        #region Overrides of SEORoleProviderBase

        public override bool IsUserInRole(string userName, string roleName)
        {
            var ds = DSSEOToolsetUser.Create(_connName);

            return ds.FindByRolAndName(userName, roleName) != null;
        }

        public override bool IsUserInProjectRole(string userName, string roleName, Project project)
        {
            var ds = DSProjectUser.Create(_connName);
            var projectUser = ds.FindByUserNameAndProject(userName, project);
            return projectUser != null && projectUser.ProjectRole.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool RoleHasPermission(string roleName, string permission)
        {
            var ds = DSPermissionRole.Create(_connName);
            return ds.FindByRoleAndPermission(roleName, permission) != null;
        }

        public override void AddUserToRole(string userName, string roleName)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var dsRole = DSRole.Create(_connName);
                var dsUser = DSSEOToolsetUser.Create(_connName);

                var user = dsUser.FindByName(userName);

                if (user == null)
                {
                    tran.Rollback();
                    throw new Exception("User Not Found");
                }

                user.UserRole = dsRole.FindByName(roleName);
                dsUser.Update(user);
                tran.Commit();
            }
        }


        public override void CreateAccountRole(string roleName, string description)
        {
            CreateRole(roleName, description, RoleType.AccountRole);
        }

        public override void CreateProjectRole(string roleName, string description)
        {
            CreateRole(roleName, description, RoleType.ProjectRole);
        }

        public override void CreateUserRole(string roleName, string description)
        {
            CreateRole(roleName, description, RoleType.UserRole);
        }

        public override IList<Role> GetUserRoles()
        {
            var ds = DSRole.Create(_connName);
            return ds.FindAllByType((int)RoleType.UserRole, true);
        }

        private void CreateRole(String roleName, String description, RoleType type)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var dsUserRole = DSRole.Create(_connName);

                var role = new Role
                {
                    Name = roleName,
                    Description = description,
                    Configurable = true,
                    Enabled = true,
                    IdRoleType = (int)type//IdRoleType 2 ==> User Role

                };

                dsUserRole.Insert(role);

                tran.Commit();
            }
        }

        public override void DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var dsUserRole = DSRole.Create(_connName);

                dsUserRole.DeleteByName(roleName);
                tran.Commit();
            }
        }

        public override bool RoleExists(string roleName)
        {
            var dsUserRole = DSRole.Create(_connName);
            return dsUserRole.FindByName(roleName) != null;
        }

        public override void AddUsersToRole(string[] userNames, string roleName)
        {
            Check.Ensure(!String.IsNullOrEmpty(roleName), "roleName must not be null nor Empty");
            Check.Ensure(userNames.Length > 0, "At least one username must be provided");

            using (var tran = new TransactionScope(_connName))
            {
                var dsUser = DSSEOToolsetUser.Create(_connName);
                var dsRole = DSRole.Create(_connName);

                for (var i = 0; i < userNames.Length; i++)
                {
                    var user = dsUser.FindByName(userNames[i]);

                    if (user != null)
                    {
                        user.UserRole = dsRole.FindByName(roleName);
                    }

                    dsUser.Update(user);
                }
                tran.Commit();
            }
        }

        public override void RemoveUsersFromRole(string[] userNames, string roleName)
        {
            Check.Ensure(!String.IsNullOrEmpty(roleName), "roleName must not be null nor Empty");
            Check.Ensure(userNames.Length > 0, "At least one username must be provided");

            using (var tran = new TransactionScope(_connName))
            {
                var dsUser = DSSEOToolsetUser.Create(_connName);
                var dsRole = DSRole.Create(_connName);

                for (var i = 0; i < userNames.Length; i++)
                {
                    var user = dsUser.FindByName(userNames[i]);

                    if (user != null)
                    {
                        user.UserRole = dsRole.FindByName(roleName);
                    }

                    dsUser.Update(user);
                }
                tran.Commit();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string nameToMatch)
        {
            throw new NotImplementedException();
        }

        public override void AddPermissionToRole(string role, string permission)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var dsPermission = DSPermission.Create(_connName);
                var dsRole = DSRole.Create(_connName);
                var dsRolePermission = DSPermissionRole.Create(_connName);

                dsRolePermission.Insert(new PermissionRole
                                            {
                                                Permission = dsPermission.FindByName(permission),
                                                Role = dsRole.FindByName(role)
                                            });

                tran.Commit();
            }
        }

        public override void RemovePermissionFromRole(string role, string permission)
        {
            throw new NotImplementedException();
        }

        public override string[] GetPermissionsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public override bool PermissionExist(string permission)
        {
            var dsPermission = DSPermission.Create(_connName);
            return dsPermission.FindByName(permission) != null;
        }

        public override void CreatePermission(string permission, string description)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var dsPermission = DSPermission.Create(_connName);

                var permissionObj = new Permission
                                        {
                                            Name = permission,
                                            Description = description
                                        };

                dsPermission.Insert(permissionObj);

                tran.Commit();
            }
        }

        public override void DeletePermission(string permission)
        {
            throw new NotImplementedException();
        }

        public override String[] GetAllPermissions()
        {
            throw new NotImplementedException();
        }

        public override IList<Role> GetRoles()
        {
            var ds = DSRole.Create(_connName);
            return ds.FindAll();
        }

        public override Role GetRoleById(int id)
        {
            var ds = DSRole.Create(_connName);
            return ds.FindByKey(id);
        }

        #endregion
    }
}