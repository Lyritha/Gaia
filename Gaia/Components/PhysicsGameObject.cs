using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;

namespace Gaia.Components
{
    internal class PhysicsGameObject : GameObject
    {
        public Physics2D physics;
        public bool affectedByCollision = true;

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
            CollisionHandler.collisionObjects.Remove(this);

            physics?.Dispose();
            physics = null;

            base.Dispose();
        }
    }
}
