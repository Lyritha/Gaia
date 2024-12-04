using Gaia.Components;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Gaia.Scripts.Objects
{
    internal class Projectile : PhysicsGameObject
    {
        private Random random = new();

        private Texture2D projectile_1;
        private Texture2D projectile_2;
        private Texture2D projectile_3;
        private float textureChangeTimer = 0;


        protected override void Update(float deltaTime)
        {
            textureChangeTimer += deltaTime;

            if (textureChangeTimer > .1f)
            {
                if (random == null) return;

                switch (random.Next(0, 3))
                {
                    case 0:
                        renderedTexture = projectile_1;
                        break;

                    case 1:
                        renderedTexture = projectile_2;
                        break;

                    case 2:
                        renderedTexture = projectile_3;
                        break;
                }

                textureChangeTimer = 0;
            }

            base.Update(deltaTime);
        }

        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            projectile_1 = GraphicsManager.Content.Load<Texture2D>(textureName+"_1");
            projectile_2 = GraphicsManager.Content.Load<Texture2D>(textureName + "_2");
            projectile_3 = GraphicsManager.Content.Load<Texture2D>(textureName + "_3");

            isTrigger = true;

            ignoreCollissions.Add(tag);
            ignoreCollissions.Add(ObjectTags.Player);
            base.Initialize(tag, position, rotation, scale, textureName + "_1");
        }

        public override void OnCollisionStarted(CollisionData collisionData)
        {
            base.OnCollisionStarted(collisionData);
        }

        public override void OnCollisionStopped()
        {
            base.OnCollisionStopped();
        }

        public override void Dispose()
        {
            // Call the base class Dispose
            base.Dispose();

            projectile_1 = null;
            projectile_2 = null;
            projectile_3 = null;

            random = null;
        }
    }
}
