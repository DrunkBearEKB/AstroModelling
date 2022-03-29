using System;
using AstroModelling.Physics.Domain.Objects.System;
using AstroModelling.Physics.Utils;

namespace AstroModelling.Physics.Domain.Models.Newton
{
    public class NewtonModel : PhysicsModel
    {
        public override string GetName()
        {
            return "Newton";
        }   
        
        public override void Update(AstroSystem system)
        {
            foreach (var obj1 in system.AstroObjects)
            {
                foreach (var obj2 in system.AstroObjects)
                {
                    if (obj1 == obj2)
                    {
                        continue;
                    }
                    
                    var dist = obj1.DistanceTo(obj2);
                    if (dist < obj1.Radius + obj2.Radius)
                    {
                        if (obj1.Mass > obj2.Mass)
                        {
                            obj1.Mass += obj2.Mass;
                            obj1.Movement = (obj1.Movement * obj1.Mass + obj2.Movement * obj2.Mass) /
                                            (obj1.Mass + obj2.Mass);
                            obj1.Radius = Math.Pow(Math.Pow(obj1.Radius, 3) + Math.Pow(obj2.Radius, 3), 1.0 / 3);
                            system.AstroObjects.Remove(obj2);
                        }
                        else
                        {
                            obj2.Mass += obj1.Mass;
                            obj2.Movement = (obj1.Movement * obj1.Mass + obj2.Movement * obj2.Mass) /
                                            (obj1.Mass + obj2.Mass);
                            obj2.Radius = Math.Pow(Math.Pow(obj1.Radius, 3) + Math.Pow(obj2.Radius, 3), 1.0 / 3);
                            system.AstroObjects.Remove(obj1);
                        }
                    }
                }
            }

            foreach (var obj1 in system.AstroObjects)
            {
                var a = new Vector(0, 0, 0);

                foreach (var obj2 in system.AstroObjects)
                {
                    if (obj1 == obj2)
                    {
                        continue;
                    }
                    
                    var dist = obj1.DistanceTo(obj2);
                    var k = AstroSystem.G * obj2.Mass / Math.Pow(dist, 3);
                    
                    a += (obj2.Position - obj1.Position) * k as Vector;
                }
                
                obj1.Movement += a * system.PhysicsModel.TimeInterval;
                obj1.PositionNew += obj1.Movement * system.PhysicsModel.TimeInterval;
            }

            foreach (var obj in system.AstroObjects)
            {
                obj.Position -= system.CentralAstroObject.Position;
                obj.Movement -= system.CentralAstroObject.Movement;
            }
        }
    }
}