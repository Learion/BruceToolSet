using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto
{
    /// <summary>
    /// Use this interface to define an entity with an owner (user).
    /// </summary>
    public interface IOwner
    {
        string Owner
        {
            get;
        }
    }
}
