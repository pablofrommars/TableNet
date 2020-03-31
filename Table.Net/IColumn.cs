using System.Collections.Generic;

namespace Table.Net
{
    public interface IColumn
    {
        #region Parameters

        string Field { get; }

        string Label { get; }

        bool Sortable { get; }

        bool Filterable { get; }

        int Sort { get; }

        #endregion

        #region Filter

        void InitFilter(IEnumerable<object> values);

        bool Include(object value);

        #endregion

    }
}
