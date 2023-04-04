using DatabaseDeployReportConvertor.Entities;
using System.Xml;

namespace DatabaseDeployReportConvertor.Providers
{
    public static class XmlParsingProvider
    {
        public static List<Change> GetChanges(string filePath)
        {
            var changes = new List<Change>();
            var xDoc = new XmlDocument();

            xDoc.Load(filePath);

            var operations = xDoc.GetElementsByTagName("Operation");

            foreach (XmlNode operation in operations)
            {
                var action = operation.Attributes["Name"].Value;
                var items = operation.ChildNodes;

                foreach (XmlNode item in items)
                {
                    var type = item.Attributes["Type"].Value;
                    var title = item.Attributes["Value"].Value;

                    type = type
                        .Replace("Sql", string.Empty)
                        .Replace("Scalar", string.Empty); 

                    changes.Add(new Change() {Action = action, Title = title, Type = type});
                }
            }

            return changes;
        }
    }
}
