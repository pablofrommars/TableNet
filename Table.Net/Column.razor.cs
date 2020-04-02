using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;

namespace Table.Net
{
    public partial class Column : ComponentBase, IColumn
    {
        [CascadingParameter]
        public ITable Table { get; set; }

        [Parameter]
        public string Field { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Sortable { get; set; }

        [Parameter]
        public bool Filterable { get; set; }

        [Parameter]
        public int Sort { get; set; }

        [Parameter]
        public string Tooltip { get; set; }

        [Parameter]
        public string TooltipPlacement { get; set; } = "center";

        private bool show = false;

        private Dictionary<object, bool> filters;

        public void InitFilter(IEnumerable<object> values)
        {
            filters = values.ToDictionary(k => k, v => true);
        }

        public bool Include(object value) => filters[value];

        protected override void OnInitialized()
        {
            Table.Register(this);
        }

        private void Click()
        {
            if (Sortable)
            {
                Table.Set(this);
            }
        }

        private void Filter()
        {
            Table.Refresh();
        }

        private bool all = true;

        private void Filter(object value)
        {
            filters[value] = !filters[value];

            all = filters.All(kv => kv.Value);

            Filter();
        }

        private void All()
        {
            all = !all;

            foreach (var key in filters.Keys.ToList())
            {
                filters[key] = all;
            }

            Filter();
        }
    }
}
