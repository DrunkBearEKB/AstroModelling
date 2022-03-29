using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace IniParser
{
    public class IniFile
    {
        protected static string ParsingPattern = @"(\[[\w\d]*\](?:\s*\w+\s*=\s*[\w(),.\-\d ]*)*)";

        protected static string ParsingSectionPattern =
            @"\[([\w\d]*)\](?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?" +
            @"(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?" +
            @"(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?" +
            @"(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?" +
            @"(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?" +
            @"(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?" +
            @"(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?" +
            @"(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?" +
            @"(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?" +
            @"(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?(?:\s+([\w\d]*)\s*=\s*([\w(),.\-\d ]*))?";
            
        private string _path;

        public readonly Dictionary<string, IniSection> Sections;

        public IniFile(string fileName)
        {
            _path = new FileInfo(fileName).FullName;
            Sections = new Dictionary<string, IniSection>();

            Parse();
        }

        private void Parse()
        {
            var matches = Regex.Matches(File.ReadAllText(_path), ParsingPattern);
            foreach (Match match in matches)
            {
                var values = Regex.Split(match.Value, ParsingSectionPattern)
                    .Where(s => s.Length != 0);
                var title = values.First();
                values = values.Skip(1).ToList();
                
                List<Tuple<string, string>> res = new List<Tuple<string, string>>();
                for (int i = 0; i < values.Count(); i += 2)
                {
                    res.Add(new Tuple<string, string>(values.ElementAt(i), values.ElementAt(i + 1)));
                }
                
                Sections.Add(title, new IniSection(title, res));
            }
        }
    }
}