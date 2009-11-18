using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Configuration;
using NHibernateDataStore.Common;

namespace Eucalypto.Roles
{
  /// <summary>
  /// A implementation of a System.Web.Security.RoleProvider class that use the Eucalypto classes.
  /// See MSDN System.Web.Security.RoleProvider documentation for more informations about RoleProvider.
  /// </summary>
  public class EucalyptoRoleProvider : System.Web.Security.RoleProvider
  {
    private ConnectionParameters mConfiguration;

    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
    {
      if (config == null)
        throw new ArgumentNullException("config");

      if (name == null || name.Length == 0)
        name = "EucalyptoRoleProvider";

      base.Initialize(name, config);


      this.mProviderName = name;
      this.mApplicationName = ExtractConfigValue(config, "applicationName", ConnectionParameters.DEFAULT_APP); //System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath

      string connName = ExtractConfigValue(config, "connectionStringName", null);
      mConfiguration = ConfigurationHelper.Create(connName);


      // Throw an exception if unrecognized attributes remain
      if (config.Count > 0)
      {
        string attr = config.GetKey(0);
        if (!String.IsNullOrEmpty(attr))
          throw new System.Configuration.Provider.ProviderException("Unrecognized attribute: " +
          attr);
      }
    }

    /// <summary>
    /// A helper function to retrieve config values from the configuration file and remove the entry.
    /// </summary>
    /// <returns></returns>
    private string ExtractConfigValue(System.Collections.Specialized.NameValueCollection config, string key, string defaultValue)
    {
      string val = config[key];
      if (val == null)
        return defaultValue;
      else
      {
        config.Remove(key);

        return val;
      }
    }


    #region Properties
    private string mProviderName;
    public string ProviderName
    {
      get { return mProviderName; }
      set { mProviderName = value; }
    }
    private string mApplicationName;
    public override string ApplicationName
    {
      get { return mApplicationName; }
      set { mApplicationName = value; }
    }
    #endregion

    #region Methods
    private void LogException(Exception exception, string action)
    {
      LoggerFacade.Log.Error(GetType(), "Exception on " + action, exception);
    }

    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        RoleDataStore roleStore = new RoleDataStore(transaction);

        //Find the roles
        Role[] roles = new Role[roleNames.Length];
        for (int i = 0; i < roleNames.Length; i++)
        {
          //Find the role
          Role role = roleStore.FindByName(ApplicationName, roleNames[i]);
          if (role == null)
            throw new RoleNotFoundException(roleNames[i]);

          roles[i] = role;
        }


        UserInRoleDataStore usersInRolesStore = new UserInRoleDataStore(transaction);
        foreach (string userName in usernames)
        {
          foreach (Role role in roles)
          {
            UserInRole userInRole = new UserInRole(ApplicationName, userName, role.Name);

            usersInRolesStore.Insert(userInRole);
          }
        }

        transaction.Commit();
      }
    }

    public override void CreateRole(string roleName)
    {
      //Check required for MSDN
      if (roleName == null || roleName == "")
        throw new System.Configuration.Provider.ProviderException("Role name cannot be empty or null.");
      if (roleName.IndexOf(',') > 0)
        throw new ArgumentException("Role names cannot contain commas.");
      if (RoleExists(roleName))
        throw new System.Configuration.Provider.ProviderException("Role name already exists.");

      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        RoleDataStore roleStore = new RoleDataStore(transaction);

        roleStore.Insert(new Role(ApplicationName, roleName));

        transaction.Commit();
      }
    }

    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        RoleDataStore roleStore = new RoleDataStore(transaction);
        UserInRoleDataStore userInRoleDataStore = new UserInRoleDataStore(transaction);

        //Find role
        Role role = roleStore.FindByName(ApplicationName, roleName);
        if (role == null)
          throw new RoleNotFoundException(roleName);

        IList<UserInRole> listUserInRole = userInRoleDataStore.FindForRole(ApplicationName, roleName);

        if (throwOnPopulatedRole && listUserInRole.Count > 0)
          throw new System.Configuration.Provider.ProviderException("Cannot delete a populated role.");

        foreach (UserInRole ur in listUserInRole)
          userInRoleDataStore.Delete(ur.Id);

        roleStore.Delete(role.Id);

        transaction.Commit();
      }

      return true;
    }

    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
      List<string> userNames = new List<string>();

      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        UserInRoleDataStore userInRoleDataStore = new UserInRoleDataStore(transaction);

        IList<UserInRole> listUserInRole = userInRoleDataStore.FindForRole(ApplicationName, roleName, usernameToMatch);

        foreach (UserInRole ur in listUserInRole)
        {
          userNames.Add(ur.UserName);
        }
      }

      return userNames.ToArray();
    }

    public override string[] GetAllRoles()
    {
      List<string> rolesNames = new List<string>();

      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        RoleDataStore roleDataStore = new RoleDataStore(transaction);

        IList<Role> list = roleDataStore.FindAll(ApplicationName);

        foreach (Role ur in list)
        {
          rolesNames.Add(ur.Name);
        }
      }

      return rolesNames.ToArray();
    }

    public override string[] GetRolesForUser(string username)
    {
      List<string> roleNames = new List<string>();

      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        UserInRoleDataStore userInRoleDataStore = new UserInRoleDataStore(transaction);

        IList<UserInRole> listUserInRole = userInRoleDataStore.FindForUser(ApplicationName, username);

        foreach (UserInRole ur in listUserInRole)
        {
          roleNames.Add(ur.RoleName);
        }
      }

      return roleNames.ToArray();
    }

    public override string[] GetUsersInRole(string roleName)
    {
      List<string> userNames = new List<string>();

      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        UserInRoleDataStore userInRoleDataStore = new UserInRoleDataStore(transaction);

        IList<UserInRole> listUserInRole = userInRoleDataStore.FindForRole(ApplicationName, roleName);

        foreach (UserInRole ur in listUserInRole)
        {
          userNames.Add(ur.UserName);
        }
      }

      return userNames.ToArray();
    }

    public override bool IsUserInRole(string username, string roleName)
    {
      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        UserInRoleDataStore userInRoleDataStore = new UserInRoleDataStore(transaction);

        if (userInRoleDataStore.Find(ApplicationName, username, roleName) != null)
          return true;
        else
          return false;
      }
    }

    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        UserInRoleDataStore usersInRolesStore = new UserInRoleDataStore(transaction);

        foreach (string userName in usernames)
        {
          foreach (string roleName in roleNames)
          {
            UserInRole userInRole = usersInRolesStore.Find(ApplicationName, userName, roleName);
            if (userInRole == null)
              throw new UserInRoleNotFoundException(userName, roleName);

            usersInRolesStore.Delete(userInRole.Id);
          }
        }

        transaction.Commit();
      }
    }

    public override bool RoleExists(string roleName)
    {
      using (TransactionScope transaction = new TransactionScope(mConfiguration))
      {
        RoleDataStore store = new RoleDataStore(transaction);
        if (store.FindByName(ApplicationName, roleName) != null)
          return true;
        else
          return false;
      }
    }

    #endregion
  }
}
