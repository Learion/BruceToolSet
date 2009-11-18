#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers
{
    public abstract class SEORoleProviderBase : ProviderBase
    {
        public abstract bool IsUserInRole(String userName, String roleName);
        public abstract bool IsUserInProjectRole(string userName, string roleName, Project project);
        public abstract bool RoleHasPermission(string roleName, string permission);
        public abstract void AddUserToRole(string userName, string roleName);
        
        public abstract void DeleteRole(String roleName, bool throwOnPopulatedRole);
        public abstract bool RoleExists(String roleName);
        public abstract void AddUsersToRole(string[] userNames, string roleName);

        public abstract void RemoveUsersFromRole(string[] userNames, string roleName);
        public abstract string[] GetUsersInRole(string roleName);
        public abstract string[] GetAllRoles();
        public abstract string[] FindUsersInRole(string roleName, string nameToMatch);

        public abstract void AddPermissionToRole(string role, string permission);
        public abstract void RemovePermissionFromRole(string role, string permission);
        public abstract string[] GetPermissionsInRole(string role);

        public abstract bool PermissionExist(string permission);
        public abstract void CreatePermission(string permission, string description);
        public abstract void DeletePermission(string permission);
        public abstract String[] GetAllPermissions();

        public abstract IList<Role> GetRoles();
        public abstract Role GetRoleById(int id);
        public abstract void CreateAccountRole(string roleName, string description);
        public abstract void CreateProjectRole(string roleName, string description);
        public abstract void CreateUserRole(string roleName, string description);
        public abstract IList<Role> GetUserRoles();
    }
}