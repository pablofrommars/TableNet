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
        public int Sort { get; set; } = 1;

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

        private readonly Type type = typeof(TItem);

        private IList<TItem> items;

        private readonly Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();

        private readonly List<IColumn> filters = new List<IColumn>();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        protected override async Task OnInitializedAsync()
        {
            items = await Loader();
        }

        private IColumn defaultColumn;

        public void Register(IColumn column)
        {
            if (column.Sortable || column.Sort != 0 || column.Filterable)
            {
                var info = type.GetProperty(column.Field);

                if (column.Sort != 0)
                {
                    defaultColumn = column;
                    CurrentSorting = (defaultColumn, defaultColumn.Sort == -1);
                }

                if (column.Filterable && info.PropertyType.IsEnum)
                {
                    column.InitFilter(Enum.GetValues(info.PropertyType).Cast<object>());

                    filters.Add(column);
                }

                properties[column.Field] = info;
            }
        }

        private bool first = true;

        public void Refresh() => StateHasChanged();

        public void Set(IColumn column)
        {
            if (column == defaultColumn)
            {
                CurrentSorting = (defaultColumn, !CurrentSorting.descending);
            }
            else
            {
                if (CurrentSorting.column == column)
                {
                    if (Sort == -1)
                    {
                        if (!CurrentSorting.descending)
                        {
                            if (defaultColumn == null)
                            {
                                CurrentSorting = (null, false);
                            }
                            else
                            {
                                CurrentSorting = (defaultColumn, defaultColumn.Sort == -1);
                            }
                        }
                        else
                        {
                            CurrentSorting = (column, false);
                        }
                    }
                    else
                    {
                        if (CurrentSorting.descending)
                        {
                            if (defaultColumn == null)
                            {
                                CurrentSorting = (null, false);
                            }
                            else
                            {
                                CurrentSorting = (defaultColumn, defaultColumn.Sort == -1);
                            }
                        }
                        else
                        {
                            CurrentSorting = (column, true);
                        }
                    }
                }
                else
                {
                    CurrentSorting = (column, Sort == -1);
                }
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
