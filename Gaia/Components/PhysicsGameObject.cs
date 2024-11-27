using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Gaia.Components
{
    internal class PhysicsGameObject : GameObject
    {
        public List<ObjectTags> ignoreCollissions = new();
        public bool isTrigger = false;
        public Physics2D physics;

        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            base.Initialize(tag, position, rotation, scale, textureName);
            physics = new(transform);

            //add object to collision handling
            CollisionHandler.collisionObjects.Add(this);
        }

        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Move();
        }

        protected virtual void Move() { }

        public override void Dispose()
        {
            // Remove from collision handler
            CollisionHandler.RemoveCollisionObject(this);

            physics?.Dispose();

            base.Dispose();
        }
    }
}
