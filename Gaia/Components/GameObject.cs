using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Gaia.Components
{
    internal class GameObject : IDisposable
    {
        public ObjectTags Tag { get; private set; }

        public Transform transform;

        public Texture2D texture;
        public Color spriteColor = Color.White;

        public bool isColliding = false;
        public float colliderRadius { get; private set; } = 0;

        public virtual void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            //set default values
            Tag = tag;
            texture = GraphicsManager.Content.Load<Texture2D>(textureName);
            transform = new(position, rotation, scale * GraphicsManager.ResolutionScaling);

            //calculate the collider radius
            float medianDimension = (texture.Width + texture.Height) / 2f;
            float uniformScale = (transform.scale.X + transform.scale.Y) / 2f;
            colliderRadius = (medianDimension * uniformScale) / 2f;

            //start update cycle
            GlobalEvents.OnUpdate += Update;
            GlobalEvents.OnDraw += DrawSelf;
        }

        protected virtual void Update(float deltaTime) 
        {
        }

        /// <summary>
        /// Draws this object on the screen
        /// </summary>
        /// <param name="pSpriteBatch"></param>
        private void DrawSelf(SpriteBatch pSpriteBatch)
        {
            //get pivot point of object
            Vector2 pivotPoint = new(texture.Width / 2, texture.Height / 2);

            //draw the sprite
            pSpriteBatch.Draw(
                texture, 
                transform.position, 
                null, 
                spriteColor, 
                transform.rotation, 
                pivotPoint,
                transform.scale, 
                SpriteEffects.None, 
                1
                );
        }

        // Dispose method for cleanup
        public virtual void Dispose()
        {
            // Unsubscribe from global events
            GlobalEvents.OnUpdate -= Update;
            GlobalEvents.OnDraw -= DrawSelf;

            // Do NOT dispose of the texture here; let ContentManager handle it
            texture = null;  // Set to null so the reference is cleared
        }

        public virtual void OnCollisionStarted(CollisionData collisionData)
        {
            isColliding = true;
        }

        public virtual void OnColliding(CollisionData collisionData)
        {
            isColliding = true;
        }

        public virtual void OnCollisionStopped()
        {
            isColliding = false;
        }
    }
}
