using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto
{
    /// <summary>
    /// Static class with some helper methods to check security properties of an entity.
    /// This class is used to check the permissions defined in the entity category. 
    /// See the MatchPermissions method for informations about the syntax.
    /// </summary>
    public class SecurityHelper
    {
        public const string ALL_USERS = "?";
        public const string AUTHENTICATED_USERS = "*";
        public const string NEGATIVE = "!";
        public const string NONE = "";

        private static bool MatchRole(System.Security.Principal.IPrincipal user, string role)
        {
            if (role == ALL_USERS)
                return true;
            else if (role == AUTHENTICATED_USERS)
                return user.Identity.IsAuthenticated;
            else
                return user.IsInRole(role);
        }

        private static IEnumerable<string> GetPositiveRoles(string permissions)
        {
            string[] roles = permissions.Split(',');
            for (int i = 0; i < roles.Length; i++)
            {
                string role = roles[i];
                role = role.Trim();
                
                if (role.Length > 0 && 
                    role.StartsWith(NEGATIVE) == false)
                    yield return role;
            }
        }
        private static IEnumerable<string> GetNegativeRoles(string permissions)
        {
            string[] roles = permissions.Split(',');
            for (int i = 0; i < roles.Length; i++)
            {
                string role = roles[i];
                role = role.Trim();

                //Return the role without the ! character and trimmed
                if (role.Length > 1 &&
                    role.StartsWith(NEGATIVE))
                    yield return role.Substring(1).Trim();
            }
        }

        /// <summary>
        /// A generic method to check a predefined permission string. 
        /// The permission string can contains a list of roles or 
        ///  some common constants like * to define authenticated users or ? for all users.
        /// You can also deny a specific role using the prefix !.
        /// Each role must be separated by a comma.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public static bool MatchPermissions(System.Security.Principal.IPrincipal user, string permissions)
        {
            //Match negative roles
            foreach (string role in GetNegativeRoles(permissions))
            {
                if (MatchRole(user, role))
                    return false;
            }

            //Match positive roles
            foreach (string role in GetPositiveRoles(permissions))
            {
                if (MatchRole(user, role))
                    return true;
            }

            return false;
        }

        public static bool MatchUser(System.Security.Principal.IPrincipal user, IOwner entity)
        {
            return entity != null && MatchUser(user, entity.Owner);
        }
        public static bool MatchUser(System.Security.Principal.IPrincipal user, string owner)
        {
            return string.Equals(user.Identity.Name, owner, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool CanDelete(System.Security.Principal.IPrincipal user, IAccessControl accessControl, IOwner entity)
        {
            if (MatchUser(user, entity))
                return true;
            else if (accessControl != null)
                return MatchPermissions(user, accessControl.DeletePermissions);
            else
                return true;
        }

        public static bool CanEdit(System.Security.Principal.IPrincipal user, IAccessControl accessControl, IOwner entity)
        {
            if (MatchUser(user, entity))
                return true;
            else if (accessControl != null)
                return MatchPermissions(user, accessControl.EditPermissions);
            else
                return true;
        }

        public static bool CanRead(System.Security.Principal.IPrincipal user, IAccessControl accessControl, IOwner entity)
        {
            if (MatchUser(user, entity))
                return true;
            else if (accessControl != null)
                return MatchPermissions(user, accessControl.ReadPermissions);
            else
                return true;
        }

        public static bool CanInsert(System.Security.Principal.IPrincipal user, IAccessControl accessControl)
        {
            if (accessControl != null)
                return MatchPermissions(user, accessControl.InsertPermissions);
            else
                return true;
        }
    }
}
