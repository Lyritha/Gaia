using Gaia.Components;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Gaia.Scripts.Objects
{
    internal class Astroid : PhysicsGameObject
    {
        SpawnAstroids spawnAstroids;

        private Random random = new();

        public Astroid(SpawnAstroids parentObject) => spawnAstroids = parentObject;

        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            string name = $"{textureName}_{random.Next(1,4)}";

            base.Initialize(tag, position, rotation, scale, name);
            spriteColor = Color.Gray;
        }

        public override void OnCollisionStarted(CollisionData collisionData)
        {
            base.OnCollisionStarted(collisionData);
            if (collisionData.tag == ObjectTags.PlayerProjectile)
                GlobalEvents.RaiseOnEarnScore(5);
                

            if (collisionData.tag == ObjectTags.PlayerProjectile || collisionData.tag == ObjectTags.Player)
                spawnAstroids?.RemoveAstroid(this);
        }

        public override void Dispose()
        {
            base.Dispose();
            spawnAstroids = null;
        }
    }
}
