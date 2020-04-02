namespace Table.Net
{
    public interface ITable
    {
        void Register(IColumn column);

        void Refresh();

        void Set(IColumn column);

        public (IColumn column, bool descending) CurrentSorting { get; }
    }
}
