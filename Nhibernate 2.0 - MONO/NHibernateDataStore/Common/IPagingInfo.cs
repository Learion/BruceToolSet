namespace NHibernateDataStore.Common
{
    public interface IPagingInfo
    {
        long PageSize { get; }
        long CurrentPage { get; }
        long RowCount { get; set; }
        long PagesCount { get; }
    }
}