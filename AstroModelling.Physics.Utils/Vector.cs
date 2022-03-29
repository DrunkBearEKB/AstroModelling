using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AstroModelling.Physics.Utils
{
    public class Vector : Coordinates
    {
        public Vector(double x, double y, double z) : base(x, y, z)
        {
            
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        
        public static Vector operator +(Vector coords1, Vector coords2)
        {
            return new Vector(coords1.X + coords2.X, 
                coords1.Y + coords2.Y, coords1.Z + coords2.Z);
        }

        public static Vector operator -(Vector coords1, Vector coords2)
        {
            return new Vector(coords1.X - coords2.X, 
                coords1.Y - coords2.Y, coords1.Z - coords2.Z);
        }

        public static Vector operator *(Vector coords1, Vector coords2)
        {
            return new Vector(coords1.X * coords2.X, 
                coords1.Y * coords2.Y, coords1.Z * coords2.Z);
        }

        public static Vector operator *(Vector coords1, double value)
        {
            return new Vector(coords1.X * value, 
                coords1.Y * value, coords1.Z * value);
        }

        public static Vector operator /(Vector coords1, Vector coords2)
        {
            if (coords2.Any(value => value == 0))
            {
                throw new DivideByZeroException();
            }
            
            return new Vector(coords1.X / coords2.X, 
                coords1.Y / coords2.Y, coords1.Z / coords2.Z);
        }

        public static Vector operator /(Vector coords1, double value)
        {
            if (value == 0)
            {
                throw new DivideByZeroException();
            }
            
            return new Vector(coords1.X / value, 
                coords1.Y / value, coords1.Z / value);
        }

        public static Vector FromString(string str)
        {
            var matches = Regex.Split(str, ParsingPattern)
                .Where(s => s.Length != 0);
            if (matches.Count() != 4 && matches.ElementAt(0) != "Vector")
            {
                throw new ArgumentException("Can not parse string!");
            }
            var coords = matches.Skip(1).Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            
            return new Vector(coords[0], coords[1], coords[2]);
        }
    }
}