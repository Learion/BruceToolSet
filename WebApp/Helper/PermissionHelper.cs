using System;
using System.Collections.Generic;
using System.Web;
using SEOToolSet.Providers;

namespace SEOToolSet.WebApp.Helper
{
    public class PermissionHelper
    {
        /// <summary>
        /// Retrive the Permission Mode for a Page
        /// </summary>
        /// <param name="permissionRequired">the Permission Required by the page</param>
        /// <param name="skipProjectPermission">some pages are not shown in the context of a project, or they are valid for all projects so when this happens setting this flag to true will skip the project role</param>
        /// <returns>ShowInNav, Read, Execute if the user has the permission or Null if the user doesn't have the permission</returns>
        public static PermissionMode? GetPermissionMode(string permissionRequired, bool skipProjectPermission)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return PermissionMode.Deny;
            }
            var userName = HttpContext.Current.User.Identity.Name;
            var hashOfPermissions = HttpContext.Current.Session["hashOfPermissions"] as Dictionary<String, PermissionMode?>;
            hashOfPermissions = hashOfPermissions ?? new Dictionary<string, PermissionMode?>();
            /*var storedMode = hashOfPermissions.ContainsKey(string.Format("{0}_{1}", permissionRequired, skipProjectPermission))
                                 ? hashOfPermissions[permissionRequired]
                                 : null;*/

            PermissionMode? storedMode;
            hashOfPermissions.TryGetValue(string.Format("{0}_{1}_{2}", permissionRequired, skipProjectPermission, userName),
                                          out storedMode);

            if (storedMode == null)
            {

                int idSelectedProject;

                Int32.TryParse(HttpContext.Current.Request["IdProject"], out idSelectedProject);

                if (idSelectedProject == 0)
                {
                    //When you run on behave of another user this portion of code will need to retrieve the Profile for that user instead   
                    idSelectedProject = ProfileHelper.SelectedIdProject;
                    if (idSelectedProject == 0)
                    {
                        //try to load the firstProject
                        var projectsForUser = ProjectManager.GetProjectsForUser(userName);
                        if (projectsForUser != null && projectsForUser.Count > 0)
                            idSelectedProject = projectsForUser[0].Id;
                    }
                }

                //If you're running on behalf Another User the userName should be replaced with the userName you're intended to run into
                var projectRoleName = skipProjectPermission ? string.Empty : ProjectManager.GetProjectRoleForUser(userName, idSelectedProject) ?? String.Empty;

                storedMode =
                    SEORolesManager.UserHasPermission(userName, permissionRequired,
                                                      skipProjectPermission, projectRoleName) ??
                    PermissionMode.Deny;

                hashOfPermissions[string.Format("{0}_{1}_{2}", permissionRequired, skipProjectPermission, userName)] = storedMode;
                HttpContext.Current.Session["hashOfPermissions"] = hashOfPermissions;
            }
            return storedMode;
        }

        /// <summary>
        /// Retrieve the PermissionMode of a Page. This method is useful to evaluate if the Page should be displayed in Read or Execute Mode.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static PermissionMode? GetPermissionModeFromNode(SiteMapNode node)
        {
            var currentPagePermission = node["PermissionRequired"];
            var skipProjectRole = node["SkipProjectRole"] == "True";

            if (String.IsNullOrEmpty(currentPagePermission))
                return PermissionMode.Read;

            var mode = GetPermissionMode(currentPagePermission, skipProjectRole);

            return mode;
        }

        /// <summary>
        /// Check if the user could access a page. This method could be used even before arrive to the page.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool UserHasAccessToPage(SiteMapNode node)
        {
            var mode = GetPermissionModeFromNode(node);
            return mode != null && mode.Value > PermissionMode.ShowInNav;
        }

        /// <summary>
        /// Check if the user should had this item in his Nav Bar
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool ShouldViewThePageInNav(SiteMapNode node)
        {
            var mode = GetPermissionModeFromNode(node);
            return mode != null && mode.Value >= PermissionMode.ShowInNav;
        }
    }



}
