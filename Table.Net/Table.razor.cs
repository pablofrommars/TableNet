using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace Table.Net
{
    public partial class Table<TItem> : ComponentBase, ITable
    {
        [Parameter]
        public Func<Task<IList<TItem>>> Loader { get; set; }

        [Parameter]
        public RenderFragment Loading { get; set; }

        [Parameter]
        public RenderFragment Header { get; set; }

        [Parameter]
        public RenderFragment<TItem> Row { get; set; }

        [Parameter]
        public EventCallback<TItem> OnRowClick { get; set; }

        [Parameter]
        public bool Small { get; set; } = true;

        [Parameter]
        public bool Dark { get; set; } = false;

        [Parameter]
        public bool HeadDark { get; set; } = false;

        [Parameter]
        public bool Striped { get; set; } = false;

        [Parameter]
        public bool Hover { get; set; } = false;

        [Parameter]
        public bool Rounded { get; set; } = false;

        private Type type = typeof(TItem);

        private IList<TItem> items;

        private Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();

        private List<IColumn> filters = new List<IColumn>();

        protected override async Task OnInitializedAsync()
        {
            items = await Loader();
        }

        public void Register(IColumn column)
        {
            if (column.Sortable || column.Filterable)
            {
                var info = type.GetProperty(column.Field);

                if (column.Filterable && info.PropertyType.IsEnum)
                {
                    column.InitFilter(Enum.GetValues(info.PropertyType).Cast<object>());

                    filters.Add(column);
                }

                properties[column.Field] = info;
            }
        }

        public void Refresh()
        {
            StateHasChanged();
        }

        public void Sort(IColumn column)
        {
            if (CurrentSorting.column == column)
            {
                if (CurrentSorting.descending)
                {
                    CurrentSorting = (null, false);
                }
                else
                {
                    CurrentSorting = (column, true);
                }
            }
            else
            {
                CurrentSorting = (column, false);
            }

            Refresh();
        }

        public (IColumn column, bool descending) CurrentSorting { get; protected set; }

        private IEnumerable<TItem> Filter(IEnumerable<TItem> source)
        {
            foreach (var item in source)
            {
                var include = true;

                foreach (var column in filters)
                {
                    if (!column.Include(properties[column.Field].GetValue(item)))
                    {
                        include = false;
                        break;
                    }
                }

                if (include)
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<TItem> Data
        {
            get
            {
                var data = Filter(items);

                if (CurrentSorting.column == null)
                {
                    return data;
                }
                else
                {
                    var property = properties[CurrentSorting.column.Field];

                    if (CurrentSorting.descending)
                    {
                        return data.OrderByDescending(o => property.GetValue(o));
                    }
                    else
                    {
                        return data.OrderBy(o => property.GetValue(o));
                    }
                }
            }
        }
    }
}
