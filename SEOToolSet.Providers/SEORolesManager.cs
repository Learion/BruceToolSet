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
    public static class SEORolesManager
    {
        private static readonly SEORoleProviderBase _defaultProvider;

        private static readonly SEORoleProviderCollection _providerCollection = new SEORoleProviderCollection();

        static SEORolesManager()
        {
            var seoRoleProviderConfiguration =
                ConfigurationManager.GetSection("SEORoleProvider") as SEORoleProviderConfiguration;

            if (seoRoleProviderConfiguration == null || seoRoleProviderConfiguration.DefaultProvider == null ||
                seoRoleProviderConfiguration.Providers == null || seoRoleProviderConfiguration.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for SEORoleProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(seoRoleProviderConfiguration.Providers, _providerCollection,
                                                 typeof(SEORoleProviderBase));
            _providerCollection.SetReadOnly();
            _defaultProvider = _providerCollection[seoRoleProviderConfiguration.DefaultProvider];

            if (_defaultProvider != null)
                return;

            var defaultProviderProp =
                seoRoleProviderConfiguration.ElementInformation.Properties["defaultProvider"];

            if (defaultProviderProp != null)
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the SEORoleProvider.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            throw new ConfigurationErrorsException("You must specify a default Provider for the SEORoleProvider");
        }

        #region SEORoleProviderBase

        public static IList<Role> GetRoles()
        {
            return Provider.GetRoles();
        }

        public static IList<Role> GetUserRoles()
        {
            return Provider.GetUserRoles();
        }

        public static Role GetRoleById(int id)
        {
            return Provider.GetRoleById(id);
        }

        public static bool IsUserInRole(string userName, string roleName)
        {
            return Provider.IsUserInRole(userName, roleName);
        }

        public static bool RoleHasPermission(string role, string permission)
        {
            return Provider.RoleHasPermission(role, permission);
        }

        public static void AddUserToRole(string userName, string roleName)
        {
            Provider.AddUserToRole(userName, roleName);
        }

        public static void CreateUserRole(string roleName, string description)
        {
            Provider.CreateUserRole(roleName, description);
        }

        public static void DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            Provider.DeleteRole(roleName, throwOnPopulatedRole);
        }

        public static bool RoleExists(string roleName)
        {
            return Provider.RoleExists(roleName);
        }

        public static void AddUsersToRole(string[] userNames, string roleName)
        {
            Provider.AddUsersToRole(userNames, roleName);
        }

        public static void RemoveUsersFromRole(string[] userNames, string roleName)
        {
            Provider.RemoveUsersFromRole(userNames, roleName);
        }



        public static string[] GetUsersInRole(string roleName)
        {
            return Provider.GetUsersInRole(roleName);
        }

        public static string[] GetAllRoles()
        {
            return Provider.GetAllRoles();
        }

        public static string[] FindUsersInRole(string roleName, string nameToMatch)
        {
            return Provider.FindUsersInRole(roleName, nameToMatch);
        }

        public static void AddPermissionToRole(string role, string permission)
        {
            Provider.AddPermissionToRole(role, permission);
        }

        public static void RemovePermissionFromRole(string role, string permission)
        {
            Provider.RemovePermissionFromRole(role, permission);
        }

        public static string[] GetPermissionsInRole(string role)
        {
            return Provider.GetPermissionsInRole(role);
        }

        public static void CreatePermission(string permission, string description)
        {
            Provider.CreatePermission(permission, description);
        }

        public static void DeletePermission(string permission)
        {
            Provider.DeletePermission(permission);
        }

        public static String[] GetAllPermissions()
        {
            return Provider.GetAllPermissions();
        }

        public static bool PermissionExist(string permission)
        {
            return Provider.PermissionExist(permission);
        }

        public static bool IsUserInProjectRole(string userName, string roleName, Project project)
        {
            return Provider.IsUserInProjectRole(userName, roleName, project);
        }

        #endregion

        public static SEORoleProviderBase Provider
        {
            get
            {
                if (_defaultProvider != null)
                    return _defaultProvider;
                throw new ProviderException("You must specify a valid default provider for SEORoleProvider.");
            }
        }

        public static SEORoleProviderCollection Providers
        {
            get { return _providerCollection; }
        }

        public static PermissionMode? UserHasPermission(string userName, string permissionRequired, bool skipProjectPermission, string projectRoleName)
        {
            var user = SEOMembershipManager.GetUser(userName);

            var userRoleName = user.UserRole.Name;
            var accountRoleName = user.Account.SubscriptionLevel.Role.Name;

            var pModeUserRole = GetPermissionModeForRole(userRoleName, permissionRequired);
            var pModeAccountRole = GetPermissionModeForRole(accountRoleName, permissionRequired);
            if (skipProjectPermission)
            {
                //return (pModeUserRole == pModeAccountRole) ? pModeUserRole : null;
                return pModeUserRole < pModeAccountRole ? pModeUserRole : pModeAccountRole;
            }

            var pModeProjectRole = GetPermissionModeForRole(projectRoleName, permissionRequired);

            var listOfnums = new List<Int32>
                                 {
                                     (Int32) pModeProjectRole.Value,
                                     (Int32) pModeUserRole.Value,
                                     (Int32) pModeAccountRole.Value
                                 };

            listOfnums.Sort((pMode1, pMode2) => pMode1.CompareTo(pMode2));

            return (PermissionMode?)listOfnums[0];

            //return pModeUserRole == pModeAccountRole && pModeAccountRole == pModeProjectRole ? pModeUserRole : null;

        }

        private static PermissionMode? GetPermissionModeForRole(string roleName, string permissionRequired)
        {
            var pmodeShowInNav = string.Format("{0}ShowInNav", permissionRequired);
            var pmodeRead = string.Format("{0}Read", permissionRequired);
            var pmodeExecute = string.Format("{0}Execute", permissionRequired);

            return Provider.RoleHasPermission(roleName, pmodeExecute)
                       ? PermissionMode.Execute
                       : (Provider.RoleHasPermission(roleName, pmodeRead)
                              ? PermissionMode.Read
                              : (Provider.RoleHasPermission(roleName, pmodeShowInNav)
                                     ? PermissionMode.ShowInNav
                                     : PermissionMode.Deny));
        }

        public static void CreateAccountRole(string roleName, string description)
        {
            Provider.CreateAccountRole(roleName, description);
        }

        public static void CreateProjectRole(string roleName, string description)
        {
            Provider.CreateProjectRole(roleName, description);
        }
    }
}