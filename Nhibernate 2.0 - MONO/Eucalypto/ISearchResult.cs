using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto
{
    /// <summary>
    /// Interface used to implement search specific features. 
    /// Currently this class is used on the user interface as a standard way to show search results.
    /// </summary>
    public interface ISearchResult
    {
        string Title
        {
            get;
        }

        string Owner
        {
            get;
        }

        string Description
        {
            get;
        }

        DateTime Date
        {
            get;
        }

        string Category
        {
            get;
        }
    }
}
