using AstroModelling.Physics.Domain.Objects.System;

namespace AstroModelling.Physics.Domain.Models
{
    public abstract class PhysicsModel
    {
        private double _timeInterval = 0;
        public double TimeInterval
        {
            get => _timeInterval;
            set => _timeInterval = value;
        }

        public abstract string GetName();
        
        public abstract void Update(AstroSystem system);
    }
}