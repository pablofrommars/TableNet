namespace Table.Net
{
    public interface ITable
    {
        void Register(IColumn column);

        void Refresh();

        void Sort(IColumn column);

        public (IColumn column, bool descending) CurrentSorting { get; }
    }
}
