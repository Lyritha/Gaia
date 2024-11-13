using Gaia.Components;
using Gaia.Utility.CustomVariables;

namespace Gaia.Scripts
{
    internal class Astroid : PhysicsGameObject
    {

        protected override void Move()
        {
            physics.AddForce(transform.Forward() * speed, ForceType.VelocityChange);
            base.Move();
        }
    }
}
