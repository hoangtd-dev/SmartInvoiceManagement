#nullable disable
namespace SIM.Core.DTOs.Common
{
    public class DataTableViewModel
    {
        public List<string> Headers { get; set; } = new List<string>();
        public List<TableRow> Rows { get; set; } = new List<TableRow>();
        public bool ShowActions { get; set; } = false;
    }

    public class TableRow
    {
        public List<TableCell> Cells { get; set; } = new List<TableCell>();
        public List<TableAction> Actions { get; set; } = new List<TableAction>();
    }

    public class TableCell
    {
        public dynamic Value { get; set; }
        public string CssClass { get; set; }
    }

    public class TableAction
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public string CssClass { get; set; }
        public string Icon { get; set; }
    }
}
