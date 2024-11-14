using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;

namespace Gaia.Components
{
    internal class PhysicsGameObject : GameObject
    {
        public Physics2D physics;
        public float speed;

        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            base.Initialize(tag, position, rotation, scale, textureName);
            physics = new(transform);
        }

        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Move();
        }

        protected virtual void Move() { }
    }
}
