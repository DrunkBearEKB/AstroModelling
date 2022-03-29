using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AstroModelling.Physics.Utils
{
    public class Coordinates : IEnumerable<double>
    {
        protected static double CalculatingAccuracy = 0.000000001;
        protected static int NumberDigitsForRounding = 6;
        protected static string ParsingPattern =
            @"\(\s*(-?\d+\.?\d+|\d+)\s*,\s*(-?\d+\.?\d+|\d+)\s*,\s*(-?\d+\.?\d+|\d+)\s*\)";
        
        protected double _x;
        protected double _y;
        protected double _z;

        public double X
        {
            get => this._x;
            set => this._x = value;
        }
        
        public double Y
        {
            get => _y;
            set => _y = value;
        }
        
        public double Z
        {
            get => _z;
            set => _z = value;
        }
        
        public Coordinates(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return _x;
            yield return _y;
            yield return _z;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static bool operator ==(Coordinates coords1, Coordinates coords2)
        {
            if (coords1 is null || coords2 is null)
            {
                return false;
            }

            return Math.Abs(coords1.X - coords2.X) < CalculatingAccuracy &&
                   Math.Abs(coords1.Y - coords2.Y) < CalculatingAccuracy &&
                   Math.Abs(coords1.Z - coords2.Z) < CalculatingAccuracy;
        }
        
        public static bool operator !=(Coordinates coords1, Coordinates coords2)
        {
            return !(coords1 == coords2);
        }

        public static Coordinates operator +(Coordinates coords1, Coordinates coords2)
        {
            return new Coordinates(coords1.X + coords2.X, 
                coords1.Y + coords2.Y, coords1.Z + coords2.Z);
        }

        public static Coordinates operator -(Coordinates coords1, Coordinates coords2)
        {
            return new Coordinates(coords1.X - coords2.X, 
                coords1.Y - coords2.Y, coords1.Z - coords2.Z);
        }

        public static Coordinates operator *(Coordinates coords1, Coordinates coords2)
        {
            return new Coordinates(coords1.X * coords2.X, 
                coords1.Y * coords2.Y, coords1.Z * coords2.Z);
        }

        public static Coordinates operator *(Coordinates coords1, double value)
        {
            return new Coordinates(coords1.X * value, 
                coords1.Y * value, coords1.Z * value);
        }

        public static Coordinates operator /(Coordinates coords1, Coordinates coords2)
        {
            if (coords2.Any(value => value == 0))
            {
                throw new DivideByZeroException();
            }
            
            return new Coordinates(coords1.X / coords2.X, 
                coords1.Y / coords2.Y, coords1.Z / coords2.Z);
        }

        public static Coordinates operator /(Coordinates coords1, double value)
        {
            if (value == 0)
            {
                throw new DivideByZeroException();
            }
            
            return new Coordinates(coords1.X / value, 
                coords1.Y / value, coords1.Z / value);
        }

        public static Coordinates FromString(string str)
        {
            var matches = Regex.Split(str, ParsingPattern)
                .Where(s => s.Length != 0);
            if (matches.Count() != 4 && matches.ElementAt(0) != "Coordinates")
            {
                throw new ArgumentException("Can not parse string!");
            }
            var coords = matches.Skip(1).Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            
            return new Coordinates(coords[0], coords[1], coords[2]);
        }

        public override string ToString()
        {
            return $"({Math.Round(X, NumberDigitsForRounding)};" +
                   $" {Math.Round(Y, NumberDigitsForRounding)};" +
                   $" {Math.Round(Z, NumberDigitsForRounding)})";
        }
    }
}