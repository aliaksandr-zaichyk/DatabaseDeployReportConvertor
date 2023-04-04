using System.Text.RegularExpressions;
using DatabaseDeployReportConvertor.Entities;

namespace DatabaseDeployReportConvertor.Providers
{
    public static class TxtParsingProvider
    {
        public static List<Change> GetChanges(string filePath)
        {   const string TITLE_PARAM = "title";
            const string TYPE_PARAM = "type";
            const string pattern = $@"\s*(?<{TITLE_PARAM}>.+\])\s\((?<{TYPE_PARAM}>.+)\)";

            var wasAction = false;
            string previousLine = string.Empty, action = string.Empty;
            var changes = new List<Change>();

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (Regex.IsMatch(line, pattern))
                {
                    if (!wasAction)
                    {
                        wasAction = true;
                        action = previousLine.Trim();
                    }

                    var matches = Regex.Matches(line, pattern);

                    var title = matches[0].Groups[TITLE_PARAM].Value;
                    var type = matches[0].Groups[TYPE_PARAM].Value;

                    changes.Add(new Change { Action = action, Title = title, Type = type });
                }
                else
                {
                    wasAction = false;
                    previousLine = line;
                }

            }

            return changes;
        }
    }
}
