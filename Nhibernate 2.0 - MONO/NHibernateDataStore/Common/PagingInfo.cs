using System;


namespace NHibernateDataStore.Common
{
    /// <summary>
    /// a helper Class to manage the Paging Information
    /// </summary>
    public class PagingInfo : IPagingInfo
    {
        /// <summary>
        /// Return a new instance of a PageInfo Object that return all the objects
        /// </summary>
        public static PagingInfo All
        {
            get { return new PagingInfo(0, 0); }
        }

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

        private long _PageSize;
        public long PageSize
        {
            get { return _PageSize; }
        }

        private long _CurrentPage;
        public long CurrentPage
        {
            get { return _CurrentPage; }
        }

        private long _RowCount;
        public long RowCount
        {
            get { return _RowCount; }
            set { _RowCount = value; }
        }

        public long PagesCount
        {
            get
            {
                return (long)Math.Ceiling(RowCount / (double)PageSize);
            }
        }
    }
}