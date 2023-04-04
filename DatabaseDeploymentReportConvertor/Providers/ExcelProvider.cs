using ClosedXML.Excel;

namespace DatabaseDeployReportConvertor.Providers
{
    public static class ExcelProvider
    {
        public static IXLCell AddTitleOfColumn(this IXLCell cell, string title, XLColor color, double columnWidth)
        {
            cell.AddValueToCellWithBackgroundColor(title, color);
            cell.Style.Font.Bold = true;
            cell.WorksheetColumn().Width = columnWidth;

            return cell;
        }

        public static IXLCell AddValueToCellWithBackgroundColor(this IXLCell cell, string value, XLColor color)
        {
            cell.AddValueToCell(value);
            cell.Style.Font.FontColor = XLColor.White;
            cell.Style.Fill.BackgroundColor = color;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            return cell;
        }

        public static IXLCell AddValueToCell(this IXLCell cell, string value)
        {
            cell.Value = value;
            cell.Style.Font.Italic = true;

            return cell;
        }
    }
}
