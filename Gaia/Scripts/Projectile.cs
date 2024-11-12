using Gaia.Components;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;

namespace Gaia.Scripts
{
    internal class Projectile : GameObject
    {
        private Physics2D physics;
        public float speed = 50;

        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            base.Initialize(tag, position, rotation, scale, textureName);
            physics = new(transform);
        }

        protected override void Update(float deltaTime)
        {
            Move();

            base.Update(deltaTime);
        }

        public void Move()
        {
            physics.AddForce(transform.Forward() * speed, ForceType.VelocityChange);
        }
    }
}
