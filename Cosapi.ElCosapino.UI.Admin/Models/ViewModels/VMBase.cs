using System.Collections.Generic;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public abstract class VMBase
    {
        public string ResultType { get; set; }
        public string ResultMessage { get; set; }

        public int Page { get; set; }
        public int RowsByPage { get; set; }
        public int RowsTotal { get; set; }
        public int PagesTotal { get; set; }

        public string Ordering { get; set; }
        public string OrderingDirection { get; set; }

        public KeyValuePair<string, string> Filters { get; set; }

    }
}