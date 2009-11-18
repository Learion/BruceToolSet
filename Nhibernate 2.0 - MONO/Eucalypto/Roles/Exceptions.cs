using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Roles
{
    [Serializable]
    public class RoleNotFoundException : System.Configuration.Provider.ProviderException //EucalyptoException (MSDN reccomand to use the Provider exception in this case)
    {
        public RoleNotFoundException(string role)
            : base("Role " + role + " not found")
        {

        }
    }

    [Serializable]
    public class UserInRoleNotFoundException : System.Configuration.Provider.ProviderException //EucalyptoException (MSDN reccomand to use the Provider exception in this case)
    {
        public UserInRoleNotFoundException(string user, string role)
            : base("User " + user + " in role " + role + " not found")
        {

        }
    }
}
