using Gaia.Components;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;

namespace Gaia.Scripts.Objects
{
    internal class Astroid : PhysicsGameObject
    {
        SpawnAstroids spawnAstroids;

        public Astroid(SpawnAstroids parentObject) => spawnAstroids = parentObject;

        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            base.Initialize(tag, position, rotation, scale, textureName);
            spriteColor = Color.Gray;
        }

        public override void OnCollisionStarted(CollisionData collisionData)
        {
            base.OnCollisionStarted(collisionData);

            if (collisionData.tag == ObjectTags.PlayerProjectile || collisionData.tag == ObjectTags.Player)
            {
                spawnAstroids.astroids.Remove(this);
                Dispose();
            }
        }
    }
}
