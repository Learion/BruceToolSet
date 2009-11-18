#region Using Directives

using System;

#endregion

namespace NHibernateDataStore.Common
{
    /// <summary>
    /// a helper Class to manage the Paging Information
    /// </summary>
    public class PagingInfo : IPagingInfo
    {
        private readonly long _CurrentPage;
        private readonly long _PageSize;

        ///<summary>
        /// Creates an instance of the PagingInfo Class
        ///</summary>
        ///<param name="pPageSize">the number of elements per page</param>
        ///<param name="pCurrentPage">the current page</param>
        public PagingInfo(long pPageSize, long pCurrentPage)
        {
            _PageSize = pPageSize;
            _CurrentPage = pCurrentPage;
        }

        /// <summary>
        /// Return a new instance of a PageInfo Object that return all the objects
        /// </summary>
        public static PagingInfo All
        {
            get { return new PagingInfo(0, 0); }
        }

        #region IPagingInfo Members

        public long PageSize
        {
            get { return _PageSize; }
        }

        public long CurrentPage
        {
            get { return _CurrentPage; }
        }

        public long RowCount { get; set; }

        public long PagesCount
        {
            get { return (long) Math.Ceiling(RowCount/(double) PageSize); }
        }

        #endregion
    }
}