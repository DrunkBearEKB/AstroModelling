using AstroModelling.Physics.Domain.Models.Newton;
using AstroModelling.Physics.Utils;
using IniParser;

namespace AstroModelling.Physics.Domain.Objects.System
{
    public static class AstroSystemConfigurationParser
    {
        public static AstroSystem Parse(string fileName)
        {
            var system = new AstroSystem();
            
            IniFile iniFile = new IniFile(fileName);
            foreach (var (name, section) in iniFile.Sections)
            {
                system.AddNewObject(new AstroObject(
                    name,
                    Coordinates.FromString(section.Values["position"]),
                    Vector.FromString(section.Values["movement"]),
                    double.Parse(section.Values["mass"]),
                    double.Parse(section.Values["radius"]))
                );
            }
            
            return system;
        }
    }
}