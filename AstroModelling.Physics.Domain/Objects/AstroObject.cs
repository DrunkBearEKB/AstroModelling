using System;
using AstroModelling.Physics.Utils;

namespace AstroModelling.Physics.Domain.Objects
{
    public class AstroObject
    {
        private string _name;
        private Coordinates _position;
        private Coordinates _positionNew;
        private Vector _movement;
        private double _mass;
        private double _radius;
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public Coordinates Position
        {
            get => _position;
            set => _position = value;
        }
        
        public Coordinates PositionNew
        {
            get => _positionNew;
            set => _positionNew = value;
        }
        
        public Vector Movement
        {
            get => _movement;
            set => _movement = value;
        }
        
        public double Mass
        {
            get => _mass;
            set => _mass = value;
        }
        
        public double Radius
        {
            get => _radius;
            set => _radius = value;
        }
        
        public AstroObject(string name, Coordinates position, Vector movement, double mass, double radius)
        {
            if (name == null || position == null || movement == null || mass == null || radius == null)
            {
                throw new NullReferenceException();
            }

            _name = name;
            _position = position;
            _movement = movement;
            _mass = mass;
            _radius = radius;
        }

        public double DistanceTo(AstroObject astroObject)
        {
            return Math.Sqrt(
                Math.Pow(Position.X - astroObject.Position.X, 2) + 
                Math.Pow(Position.Y - astroObject.Position.Y, 2) + 
                Math.Pow(Position.Z - astroObject.Position.Z, 2));
        }

        public override string ToString()
        {
            return $"AstroObject({_name})";
        }
    }
}