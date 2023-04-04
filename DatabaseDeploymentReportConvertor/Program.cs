using ClosedXML.Excel;
using DatabaseDeployReportConvertor.Entities;
using DatabaseDeployReportConvertor.Providers;


Console.WriteLine("Input the title of the file please:");
var filePath = Console.ReadLine();

if (string.IsNullOrEmpty(filePath))
{
    return;
}

var changes = new List<Change>();

if (filePath.EndsWith(".xml"))
{
    changes = XmlParsingProvider.GetChanges(filePath);
}
else
{
    changes = TxtParsingProvider.GetChanges(filePath);
}

var book = new XLWorkbook();
var ws = book.AddWorksheet("DatabaseChanges");

ws.Cell(Consts.TITLE_ROW_INDEX, Consts.TYPE_COLUMN_INDEX).AddTitleOfColumn(Consts.TYPE_COLUMN_TITLE, XLColor.Orange, 18);
ws.Cell(Consts.TITLE_ROW_INDEX, Consts.TITLE_COLUMN_INDEX).AddTitleOfColumn(Consts.TITLE_COLUMN_TITLE, XLColor.Blue, 70);
ws.Cell(Consts.TITLE_ROW_INDEX, Consts.ACTION_COLUMN_INDEX).AddTitleOfColumn(Consts.ACTION_COLUMN_TITLE, XLColor.Green, 15);
ws.Cell(Consts.TITLE_ROW_INDEX, Consts.EXPECTED_COLUMN_INDEX).AddTitleOfColumn(Consts.EXPECTED_COLUMN_TITLE, XLColor.Amber, 10);
ws.Cell(Consts.TITLE_ROW_INDEX, Consts.DESCRIPTION_COLUMN_INDEX).AddTitleOfColumn(Consts.DESCRIPTION_COLUMN_TITLE, XLColor.BabyPink, 30);

for (var i = 0; i < changes.Count(); i++)
{
    ws.Cell(i + 2, Consts.TYPE_COLUMN_INDEX).AddValueToCell(changes[i].Type);
    ws.Cell(i + 2, Consts.TITLE_COLUMN_INDEX).AddValueToCell(changes[i].Title);

    var actionColor = changes[i].Action switch
    {
        Consts.DROP_ACTION => XLColor.UpMaroon,
        Consts.ALTER_ACTION => XLColor.PineGreen,
        Consts.CREATE_ACTION => XLColor.Green,
        Consts.REFRESH_ACTION => XLColor.RoyalBlueTraditional,
        _ => XLColor.Black,
    };

    ws.Cell(i + 2, Consts.ACTION_COLUMN_INDEX).AddValueToCellWithBackgroundColor(changes[i].Action, actionColor);
    ws.Cell(i + 2, Consts.EXPECTED_COLUMN_INDEX).AddValueToCellWithBackgroundColor("No", XLColor.Rufous);
}


book.SaveAs("DeploymentReport.xlsx");