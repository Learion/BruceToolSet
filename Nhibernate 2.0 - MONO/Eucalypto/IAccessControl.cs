using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto
{
    /// <summary>
    /// The interface used to define the permissions associated for a given entity.
    /// Not all the permissions can be used for all entities.
    /// For example the InsertPermissions is not used for FileAttach.
    /// Each property can contains a set of roles separated by a comma ','.
    /// There are 2 special roles: '*' indicates authenticated users and '?' indicates all users.
    /// </summary>
    public interface IAccessControl
    {
        string ReadPermissions
        {
            get;
        }

        string InsertPermissions
        {
            get;
        }

        string EditPermissions
        {
            get;
        }

        string DeletePermissions
        {
            get;
        }
    }
}
