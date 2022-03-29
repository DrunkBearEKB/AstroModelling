using AstroModelling.Physics.Domain.Objects.System;

namespace AstroModelling.Physics.Domain.Models.NewtonEinstein
{
    public class NewtonEinsteinModel : PhysicsModel
    {
        public override string GetName()
        {
            return "Newton-Einstein";
        }

        public override void Update(AstroSystem system)
        {
            
        }
    }
}