using Gaia.Components;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;

namespace Gaia.Scripts.Objects
{
    internal class Projectile : PhysicsGameObject
    {
        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            base.Initialize(tag, position, rotation, scale, textureName);
            physics.Mass = 1f;
        }

        public override void OnCollisionStarted(CollisionData collisionData)
        {
            base.OnCollisionStarted(collisionData);
        }

        public override void OnCollisionStopped()
        {
            base.OnCollisionStopped();
        }
    }
}
