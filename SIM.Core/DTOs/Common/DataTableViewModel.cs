#nullable disable
namespace SIM.Core.DTOs.Common
{
    public class DataTableViewModel
    {
        public List<string> Headers { get; set; } = new List<string>();
        public List<TableRow> Rows { get; set; } = new List<TableRow>();
    }

    public class TableRow
    {
        public List<TableCell> Cells { get; set; } = new List<TableCell>();
    }

    public class TableCell
    {
        public dynamic Value { get; set; }
        public string CssClass { get; set; }
    }
}
