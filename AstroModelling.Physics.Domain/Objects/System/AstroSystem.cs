using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AstroModelling.Physics.Domain.Models;

namespace AstroModelling.Physics.Domain.Objects.System
{
    public class AstroSystem
    {
        public static double G = 0.01;  // 6.674301515151515151515 / Math.Pow(10, 11);

        private PhysicsModel _model;
        public PhysicsModel PhysicsModel
        {
            get => _model;
            set => _model = value;
        }
        public bool IsModelSet => _model != null;

        private ICollection<AstroObject> _collectionAstroObjects;
        private AstroObject _centralAstroObject;
        public AstroObject CentralAstroObject
        {
            get => _centralAstroObject;
            private set => _centralAstroObject = value;
        }

        public ICollection<AstroObject> AstroObjects => _collectionAstroObjects;

        public AstroSystem()
        {
            _collectionAstroObjects = new List<AstroObject>();
            _centralAstroObject = null;
        }
        
        public AstroSystem(PhysicsModel model) : this()
        {
            _model = model;
        }

        public void AddNewObject(AstroObject obj)
        {
            if (_collectionAstroObjects.Select(o => o.Name).Contains(obj.Name))
            {
                throw new ArgumentException("Already contains AstroObject with the same name!");
            }
            _collectionAstroObjects.Add(obj);
        }

        public void SetTimeInterval(double time)
        {
            _model.TimeInterval = time;
        }

        public AstroSystem UpdateState()
        {
            if (!IsModelSet)
            {
                throw new NullReferenceException("Model is not set!");
            }
            
            _model.Update(this);
            return this;
        }
    }
}