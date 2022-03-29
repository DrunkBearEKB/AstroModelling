using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IniParser
{
    public class IniSection
    {
        public string Name { get; set; }
        public Dictionary<string, string> Values { get; }

        public IniSection(string name)
        {
            Name = name;
            Values = new Dictionary<string, string>();
        }
        
        public IniSection(string name, ICollection<Tuple<string, string>> values)
        {
            Name = name;
            Values = values.ToDictionary(value => value.Item1, value => value.Item2);
        }
    }
}