using Gaia.Components;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;

namespace Gaia.Scripts
{
    internal class Projectile : PhysicsGameObject
    {
        protected override void Move()
        {
            physics.AddForce(transform.Forward() * speed, ForceType.VelocityChange);
            base.Move();
        }
    }
}
